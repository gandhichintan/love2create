using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Services.Events;
using Nop.Core.Domain.Projects;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Manufacturer service
    /// </summary>
    public partial class ManufacturerService : IManufacturerService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : manufacturer ID
        /// </remarks>
        private const string MANUFACTURERS_BY_ID_KEY = "Nop.manufacturer.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : manufacturer ID
        /// {2} : page index
        /// {3} : page size
        /// {4} : current customer ID
        /// {5} : store ID
        /// </remarks>
        private const string PRODUCTMANUFACTURERS_ALLBYMANUFACTURERID_KEY = "Nop.productmanufacturer.allbymanufacturerid-{0}-{1}-{2}-{3}-{4}-{5}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : product ID
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        private const string PRODUCTMANUFACTURERS_ALLBYPRODUCTID_KEY = "Nop.productmanufacturer.allbyproductid-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string MANUFACTURERS_PATTERN_KEY = "Nop.manufacturer.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTMANUFACTURERS_PATTERN_KEY = "Nop.productmanufacturer.";

        #endregion

        #region Fields

        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<ProductManufacturer> _productManufacturerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IRepository<ProjectManufacturer> _projectManufacturerRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ManufacturerLike> _manufacturerLikeRepository;
        private readonly IRepository<ManufacturerVideo> _manufacturerVideoRepository;
        private readonly IRepository<Video> _videoRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly CatalogSettings _catalogSettings;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="manufacturerRepository">Category repository</param>
        /// <param name="productManufacturerRepository">ProductCategory repository</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="aclRepository">ACL record repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event published</param>
        public ManufacturerService(ICacheManager cacheManager,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<ProductManufacturer> productManufacturerRepository,
            IRepository<Product> productRepository,
            IRepository<AclRecord> aclRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IRepository<ProjectManufacturer> projectManufacturerRepository,
            IRepository<Project> projectRepository,
            IWorkContext workContext,
            IStoreContext storeContext,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher,
            IRepository<ManufacturerLike> manufacturerLikeRepository,
            IRepository<ManufacturerVideo> manufacturerVideoRepository,
            IRepository<Video> videoRepository)
        {
            this._cacheManager = cacheManager;
            this._manufacturerRepository = manufacturerRepository;
            this._productManufacturerRepository = productManufacturerRepository;
            this._productRepository = productRepository;
            this._aclRepository = aclRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._projectManufacturerRepository = projectManufacturerRepository;
            this._projectRepository = projectRepository;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
            this._manufacturerLikeRepository = manufacturerLikeRepository;
            this._manufacturerVideoRepository = manufacturerVideoRepository;
            this._videoRepository = videoRepository;
        }
        #endregion

        #region Methods

        public virtual void DeleteManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException("manufacturer");
            
            manufacturer.Deleted = true;
            UpdateManufacturer(manufacturer);
        }

        public virtual IPagedList<Manufacturer> GetAllManufacturers(string manufacturerName = "",
            int pageIndex = 0,
            int pageSize = int.MaxValue, 
            bool showHidden = false)
        {
            var query = _manufacturerRepository.Table;
            if (!showHidden)
                query = query.Where(m => m.Published);
            if (!String.IsNullOrWhiteSpace(manufacturerName))
                query = query.Where(m => m.Name.Contains(manufacturerName));
            query = query.Where(m => !m.Deleted);
            query = query.OrderBy(m => m.DisplayOrder);

            if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
            { 
                if (!_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                        .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                    query = from m in query
                            join acl in _aclRepository.Table
                            on new { c1 = m.Id, c2 = "Manufacturer" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into m_acl
                            from acl in m_acl.DefaultIfEmpty()
                            where !m.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select m;
                }
                if (!_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    var currentStoreId = _storeContext.CurrentStore.Id;
                    query = from m in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = m.Id, c2 = "Manufacturer" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into m_sm
                            from sm in m_sm.DefaultIfEmpty()
                            where !m.LimitedToStores || currentStoreId == sm.StoreId
                            select m;
                }
                //only distinct manufacturers (group by ID)
                query = from m in query
                        group m by m.Id
                            into mGroup
                            orderby mGroup.Key
                            select mGroup.FirstOrDefault();
                query = query.OrderBy(m => m.DisplayOrder);
            }

            return new PagedList<Manufacturer>(query, pageIndex, pageSize);
        }

        public virtual Manufacturer GetManufacturerById(int manufacturerId)
        {
            if (manufacturerId == 0)
                return null;
            
            string key = string.Format(MANUFACTURERS_BY_ID_KEY, manufacturerId);
            return _cacheManager.Get(key, () => { return _manufacturerRepository.GetById(manufacturerId); });
        }

        public virtual void InsertManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException("manufacturer");

            _manufacturerRepository.Insert(manufacturer);

            //cache
            _cacheManager.RemoveByPattern(MANUFACTURERS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTMANUFACTURERS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(manufacturer);
        }

        public virtual void UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException("manufacturer");

            _manufacturerRepository.Update(manufacturer);

            //cache
            _cacheManager.RemoveByPattern(MANUFACTURERS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTMANUFACTURERS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(manufacturer);
        }

        public virtual void DeleteProductManufacturer(ProductManufacturer productManufacturer)
        {
            if (productManufacturer == null)
                throw new ArgumentNullException("productManufacturer");

            _productManufacturerRepository.Delete(productManufacturer);

            //cache
            _cacheManager.RemoveByPattern(MANUFACTURERS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTMANUFACTURERS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(productManufacturer);
        }

        public virtual IPagedList<ProductManufacturer> GetProductManufacturersByManufacturerId(int manufacturerId,
            int pageIndex, int pageSize, bool showHidden = false)
        {
            if (manufacturerId == 0)
                return new PagedList<ProductManufacturer>(new List<ProductManufacturer>(), pageIndex, pageSize);

            string key = string.Format(PRODUCTMANUFACTURERS_ALLBYMANUFACTURERID_KEY, showHidden, manufacturerId, pageIndex, pageSize, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = from pm in _productManufacturerRepository.Table
                            join p in _productRepository.Table on pm.ProductId equals p.Id
                            where pm.ManufacturerId == manufacturerId &&
                                  !p.Deleted &&
                                  (showHidden || p.Published)
                            orderby pm.DisplayOrder
                            select pm;

                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from pm in query
                                join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                                join acl in _aclRepository.Table
                                on new { c1 = m.Id, c2 = "Manufacturer" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into m_acl
                                from acl in m_acl.DefaultIfEmpty()
                                where !m.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select pm;
                    }
                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from pm in query
                                join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                                join sm in _storeMappingRepository.Table
                                on new { c1 = m.Id, c2 = "Manufacturer" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into m_sm
                                from sm in m_sm.DefaultIfEmpty()
                                where !m.LimitedToStores || currentStoreId == sm.StoreId
                                select pm;
                    }

                    //only distinct manufacturers (group by ID)
                    query = from pm in query
                            group pm by pm.Id
                            into pmGroup
                            orderby pmGroup.Key
                            select pmGroup.FirstOrDefault();
                    query = query.OrderBy(pm => pm.DisplayOrder);
                }

                var productManufacturers = new PagedList<ProductManufacturer>(query, pageIndex, pageSize);
                return productManufacturers;
            });
        }

        public virtual IList<ProductManufacturer> GetProductManufacturersByProductId(int productId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductManufacturer>();

            string key = string.Format(PRODUCTMANUFACTURERS_ALLBYPRODUCTID_KEY, showHidden, productId, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = from pm in _productManufacturerRepository.Table
                            join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                            where pm.ProductId == productId &&
                                !m.Deleted &&
                                (showHidden || m.Published)
                            orderby pm.DisplayOrder
                            select pm;


                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from pm in query
                                join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                                join acl in _aclRepository.Table
                                on new { c1 = m.Id, c2 = "Manufacturer" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into m_acl
                                from acl in m_acl.DefaultIfEmpty()
                                where !m.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select pm;
                    }

                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from pm in query
                                join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                                join sm in _storeMappingRepository.Table
                                on new { c1 = m.Id, c2 = "Manufacturer" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into m_sm
                                from sm in m_sm.DefaultIfEmpty()
                                where !m.LimitedToStores || currentStoreId == sm.StoreId
                                select pm;
                    }

                    //only distinct manufacturers (group by ID)
                    query = from pm in query
                            group pm by pm.Id
                            into mGroup
                            orderby mGroup.Key
                            select mGroup.FirstOrDefault();
                    query = query.OrderBy(pm => pm.DisplayOrder);
                }

                var productManufacturers = query.ToList();
                return productManufacturers;
            });
        }

        public virtual ProductManufacturer GetProductManufacturerById(int productManufacturerId)
        {
            if (productManufacturerId == 0)
                return null;

            return _productManufacturerRepository.GetById(productManufacturerId);
        }

        public virtual void InsertProductManufacturer(ProductManufacturer productManufacturer)
        {
            if (productManufacturer == null)
                throw new ArgumentNullException("productManufacturer");

            _productManufacturerRepository.Insert(productManufacturer);

            //cache
            _cacheManager.RemoveByPattern(MANUFACTURERS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTMANUFACTURERS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(productManufacturer);
        }

        public virtual void UpdateProductManufacturer(ProductManufacturer productManufacturer)
        {
            if (productManufacturer == null)
                throw new ArgumentNullException("productManufacturer");

            _productManufacturerRepository.Update(productManufacturer);

            //cache
            _cacheManager.RemoveByPattern(MANUFACTURERS_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTMANUFACTURERS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(productManufacturer);
        }

        #endregion

        #region Project Manufacturer

        public virtual void DeleteProjectManufacturer(ProjectManufacturer projectManufacturer)
        {
            if (projectManufacturer == null)
                throw new ArgumentNullException("projectManufacturer");

            _projectManufacturerRepository.Delete(projectManufacturer);

            //event notification
            _eventPublisher.EntityDeleted(projectManufacturer);
        }

        public virtual IPagedList<ProjectManufacturer> GetProjectManufacturersByManufacturerId(int manufacturerId,
            int pageIndex, int pageSize, bool showHidden = false)
        {
            if (manufacturerId == 0)
                return new PagedList<ProjectManufacturer>(new List<ProjectManufacturer>(), pageIndex, pageSize);

            var query = from pm in _projectManufacturerRepository.Table
                        join p in _projectRepository.Table on pm.ProjectId equals p.Id
                        where pm.ManufacturerId == manufacturerId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pm.DisplayOrder
                        select pm;

            //ACL (access control list)
            if (!showHidden)
            {
                var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

                query = from pm in query
                        join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                        join acl in _aclRepository.Table on m.Id equals acl.EntityId into m_acl
                        from acl in m_acl.DefaultIfEmpty()
                        where
                            !m.SubjectToAcl ||
                            (acl.EntityName == "Manufacturer" && allowedCustomerRolesIds.Contains(acl.CustomerRoleId))
                        select pm;

                //only distinct manufacturers (group by ID)
                query = from pm in query
                        group pm by pm.Id
                            into pmGroup
                            orderby pmGroup.Key
                            select pmGroup.FirstOrDefault();
                query = query.OrderBy(pm => pm.DisplayOrder);
            }

            var projectManufacturers = new PagedList<ProjectManufacturer>(query, pageIndex, pageSize);
            return projectManufacturers;
        }

        public virtual IList<ProjectManufacturer> GetProjectManufacturersByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectManufacturer>();

            var query = from pm in _projectManufacturerRepository.Table
                        join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                        where pm.ProjectId == projectId &&
                            !m.Deleted &&
                            (showHidden || m.Published)
                        orderby pm.DisplayOrder
                        select pm;


            //ACL (access control list)
            if (!showHidden)
            {
                var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

                query = from pm in query
                        join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                        join acl in _aclRepository.Table on m.Id equals acl.EntityId into m_acl
                        from acl in m_acl.DefaultIfEmpty()
                        where !m.SubjectToAcl || (acl.EntityName == "Manufacturer" && allowedCustomerRolesIds.Contains(acl.CustomerRoleId))
                        select pm;

                //only distinct manufacturers (group by ID)
                query = from pm in query
                        group pm by pm.Id
                            into mGroup
                            orderby mGroup.Key
                            select mGroup.FirstOrDefault();
                query = query.OrderBy(pm => pm.DisplayOrder);
            }

            var projectManufacturers = query.ToList();
            return projectManufacturers;
        }

        public virtual ProjectManufacturer GetProjectManufacturerById(int projectManufacturerId)
        {
            if (projectManufacturerId == 0)
                return null;

            return _projectManufacturerRepository.GetById(projectManufacturerId);
        }

        public virtual void InsertProjectManufacturer(ProjectManufacturer projectManufacturer)
        {
            if (projectManufacturer == null)
                throw new ArgumentNullException("projectManufacturer");

            _projectManufacturerRepository.Insert(projectManufacturer);

            //event notification
            _eventPublisher.EntityInserted(projectManufacturer);
        }

        public virtual void UpdateProjectManufacturer(ProjectManufacturer projectManufacturer)
        {
            if (projectManufacturer == null)
                throw new ArgumentNullException("projectManufacturer");

            _projectManufacturerRepository.Update(projectManufacturer);

            //event notification
            _eventPublisher.EntityUpdated(projectManufacturer);
        }

        #endregion

        #region Manufacturer Like Methods

        public virtual void DeleteManufacturerLike(ManufacturerLike manufacturerLike)
        {
            if (manufacturerLike == null)
                throw new ArgumentNullException("manufacturerLike");

            _manufacturerLikeRepository.Delete(manufacturerLike);
        }

        public virtual IPagedList<Manufacturer> GetManufacturerLikesByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from pf in _manufacturerLikeRepository.Table
                        join p in _manufacturerRepository.Table on pf.ManufacturerId equals p.Id
                        where pf.CustomerId == customerId && p.Published
                        orderby pf.CreatedOn descending
                        select p;

            var manufacturers = new PagedList<Manufacturer>(query, 0, 100000);

            //return manufacturers
            return manufacturers;
        }

        public virtual void InsertManufacturerLike(ManufacturerLike manufacturerLike)
        {
            if (manufacturerLike == null)
                throw new ArgumentNullException("manufacturerLike");

            _manufacturerLikeRepository.Insert(manufacturerLike);
        }

        public virtual ManufacturerLike GetManufacturerLikeByCustomerId(int customerId, int manufacturerId)
        {
            if (manufacturerId == 0 || customerId == 0)
                return null;

            var query = from pf in _manufacturerLikeRepository.Table
                        where pf.CustomerId == customerId && pf.ManufacturerId == manufacturerId
                        select pf;

            return query.FirstOrDefault();
        }

        public virtual int GetManufacturerLikesByManufacturerId(int manufacturerId)
        {
            if (manufacturerId == 0)
                return 0;

            var query = from pf in _manufacturerLikeRepository.Table
                        where pf.ManufacturerId == manufacturerId
                        select pf;

            return query.ToList().Count();
        }

        #endregion

        #region Manufacturer Video Mapping Methods

        public virtual void DeleteManufacturerVideo(ManufacturerVideo manufacturerVideo)
        {
            if (manufacturerVideo == null)
                throw new ArgumentNullException("manufacturerVideo");

            _manufacturerVideoRepository.Delete(manufacturerVideo);
        }

        public virtual IPagedList<ManufacturerVideo> GetManufacturerVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (videoId == 0)
                return new PagedList<ManufacturerVideo>(new List<ManufacturerVideo>(), pageIndex, pageSize);

            var query = from pc in _manufacturerVideoRepository.Table
                        join p in _manufacturerRepository.Table on pc.ManufacturerId equals p.Id
                        where pc.VideoId == videoId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var manufacturerVideos = new PagedList<ManufacturerVideo>(query, pageIndex, pageSize);
            return manufacturerVideos;
        }

        public virtual IList<ManufacturerVideo> GetManufacturerVideosByManufacturerId(int manufacturerId, bool showHidden = false)
        {
            if (manufacturerId == 0)
                return new List<ManufacturerVideo>();

            var query = from pc in _manufacturerVideoRepository.Table
                        join c in _videoRepository.Table on pc.VideoId equals c.Id
                        where pc.ManufacturerId == manufacturerId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var manufacturerVideos = query.ToList();
            return manufacturerVideos;
        }

        public virtual ManufacturerVideo GetManufacturerVideoById(int manufacturerVideoId)
        {
            if (manufacturerVideoId == 0)
                return null;

            return _manufacturerVideoRepository.GetById(manufacturerVideoId);
        }

        public virtual void InsertManufacturerVideo(ManufacturerVideo manufacturerVideo)
        {
            if (manufacturerVideo == null)
                throw new ArgumentNullException("manufacturerVideo");

            _manufacturerVideoRepository.Insert(manufacturerVideo);
        }

        public virtual void UpdateManufacturerVideo(ManufacturerVideo manufacturerVideo)
        {
            if (manufacturerVideo == null)
                throw new ArgumentNullException("manufacturerVideo");

            _manufacturerVideoRepository.Update(manufacturerVideo);
        }

        #endregion
    }
}
