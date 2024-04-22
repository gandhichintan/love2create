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
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Core.Domain.Projects;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Category service
    /// </summary>
    public partial class CategoryService : ICategoryService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : category ID
        /// </remarks>
        private const string CATEGORIES_BY_ID_KEY = "Nop.category.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent category ID
        /// {1} : show hidden records?
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        private const string CATEGORIES_BY_PARENT_CATEGORY_ID_KEY = "Nop.category.byparent-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : category ID
        /// {2} : page index
        /// {3} : page size
        /// {4} : current customer ID
        /// {5} : store ID
        /// </remarks>
        private const string PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY = "Nop.productcategory.allbycategoryid-{0}-{1}-{2}-{3}-{4}-{5}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// {1} : product ID
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        private const string PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY = "Nop.productcategory.allbyproductid-{0}-{1}-{2}-{3}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CATEGORIES_PATTERN_KEY = "Nop.category.";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTCATEGORIES_PATTERN_KEY = "Nop.productcategory.";

        #endregion

        #region Fields

        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICacheManager _cacheManager;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IAclService _aclService;
        private readonly CatalogSettings _catalogSettings;
        private readonly IRepository<BlogsWeLove> _blogsWeLoveRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<SiteWeLove> _siteWeLoveRepository;
        private readonly IRepository<Sponsorship> _sponsorshipRepository;
        private readonly IRepository<SurveyCategory> _surveyCategoryRepository;
        private readonly IRepository<SurveyRegistration> _surveyRegistrationRepoitory;
        private readonly IRepository<SurveyCategoryRegistration> _surveyCategoryRegistrationRepository;
        private readonly IRepository<Event> _eventRepository;
        private readonly IRepository<CategoryVideo> _categoryVideoRepository;
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<ProjectCat> _projectCatRepository;
        private readonly IRepository<Project> _projectRepository;

        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="categoryRepository">Category repository</param>
        /// <param name="productCategoryRepository">ProductCategory repository</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="aclRepository">ACL record repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="storeMappingService">Store mapping service</param>
        /// <param name="aclService">ACL service</param>
        /// <param name="catalogSettings">Catalog settings</param>
        public CategoryService(ICacheManager cacheManager,
            IRepository<Category> categoryRepository,
            IRepository<ProductCategory> productCategoryRepository,
            IRepository<Product> productRepository,
            IRepository<AclRecord> aclRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IWorkContext workContext,
            IStoreContext storeContext,
            IEventPublisher eventPublisher,
            IStoreMappingService storeMappingService,
            IAclService aclService,
            CatalogSettings catalogSettings,
            IRepository<BlogsWeLove> blogsWeLoveRepository,
            IRepository<Book> bookRepository,
            IRepository<SiteWeLove> siteweloveRepository,
            IRepository<Sponsorship> sponsorshipRepository,
            IRepository<SurveyCategory> surveyCategoryRepository,
            IRepository<SurveyRegistration> surveyRegistrationRepoitory,
            IRepository<SurveyCategoryRegistration> surveyCategoryRegistrationRepository,
            IRepository<Event> eventRepository,
            IRepository<CategoryVideo> categoryVideoRepository,
            IRepository<Video> videoRepository,
            IRepository<ProjectCat> projectCatRepository,
            IRepository<Project> projectRepository)
        {
            this._cacheManager = cacheManager;
            this._categoryRepository = categoryRepository;
            this._productCategoryRepository = productCategoryRepository;
            this._productRepository = productRepository;
            this._aclRepository = aclRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._eventPublisher = eventPublisher;
            this._storeMappingService = storeMappingService;
            this._aclService = aclService;
            this._catalogSettings = catalogSettings;
            this._blogsWeLoveRepository = blogsWeLoveRepository;
            this._bookRepository = bookRepository;
            this._siteWeLoveRepository = siteweloveRepository;
            this._sponsorshipRepository = sponsorshipRepository;
            this._surveyCategoryRepository = surveyCategoryRepository;
            this._surveyRegistrationRepoitory = surveyRegistrationRepoitory;
            this._surveyCategoryRegistrationRepository = surveyCategoryRegistrationRepository;
            this._eventRepository = eventRepository;
            this._categoryVideoRepository = categoryVideoRepository;
            this._videoRepository = videoRepository;
            this._projectCatRepository = projectCatRepository;
            this._projectRepository = projectRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.Deleted = true;
            UpdateCategory(category);

            //reset a "Parent category" property of all child subcategories
            var subcategories = GetAllCategoriesByParentCategoryId(category.Id, true);
            foreach (var subcategory in subcategories)
            {
                subcategory.ParentCategoryId = 0;
                UpdateCategory(subcategory);
            }
        }
        
        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<Category> GetAllCategories(string categoryName = "", 
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _categoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);
            
            if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
            {
                if (!_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                        .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                    query = from c in query
                            join acl in _aclRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                            from acl in c_acl.DefaultIfEmpty()
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select c;
                }
                if (!_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    var currentStoreId = _storeContext.CurrentStore.Id;
                    query = from c in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                            from sm in c_sm.DefaultIfEmpty()
                            where !c.LimitedToStores || currentStoreId == sm.StoreId
                            select c;
                }

                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);
            }
            
            var unsortedCategories = query.ToList();

            //sort categories
            var sortedCategories = unsortedCategories.SortCategoriesForTree();

            //paging
            return new PagedList<Category>(sortedCategories, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        public virtual IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false)
        {
            string key = string.Format(CATEGORIES_BY_PARENT_CATEGORY_ID_KEY, parentCategoryId, showHidden, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryRepository.Table;
                if (!showHidden)
                    query = query.Where(c => c.Published);
                query = query.Where(c => c.ParentCategoryId == parentCategoryId);
                query = query.Where(c => !c.Deleted);
                query = query.OrderBy(c => c.DisplayOrder);

                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from c in query
                                join acl in _aclRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                                from acl in c_acl.DefaultIfEmpty()
                                where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select c;
                    }
                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from c in query
                                join sm in _storeMappingRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                                from sm in c_sm.DefaultIfEmpty()
                                where !c.LimitedToStores || currentStoreId == sm.StoreId
                                select c;
                    }
                    //only distinct categories (group by ID)
                    query = from c in query
                            group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                    query = query.OrderBy(c => c.DisplayOrder);
                }

                var categories = query.ToList();
                return categories;
            });

        }
        
        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false)
        {
            var query = from c in _categoryRepository.Table
                        orderby c.DisplayOrder
                        where c.Published &&
                        !c.Deleted && 
                        c.ShowOnHomePage
                        select c;

            var categories = query.ToList();
            if (!showHidden)
            {
                categories = categories
                    .Where(c => _aclService.Authorize(c) && _storeMappingService.Authorize(c))
                    .ToList();
            }

            return categories;
        }
                
        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        public virtual Category GetCategoryById(int categoryId)
        {
            if (categoryId == 0)
                return null;
            
            string key = string.Format(CATEGORIES_BY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () => { return _categoryRepository.GetById(categoryId); });
        }

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(category);
        }

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            //validate category hierarchy
            var parentCategory = GetCategoryById(category.ParentCategoryId);
            while (parentCategory != null)
            {
                if (category.Id == parentCategory.Id)
                {
                    category.ParentCategoryId = 0;
                    break;
                }
                parentCategory = GetCategoryById(parentCategory.ParentCategoryId);
            }

            _categoryRepository.Update(category);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(category);
        }
        
        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void UpdateHasDiscountsApplied(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.HasDiscountsApplied = category.AppliedDiscounts.Count > 0;
            UpdateCategory(category);
        }

        /// <summary>
        /// Deletes a product category mapping
        /// </summary>
        /// <param name="productCategory">Product category</param>
        public virtual void DeleteProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Delete(productCategory);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(productCategory);
        }

        /// <summary>
        /// Gets product category mapping collection
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product a category mapping collection</returns>
        public virtual IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (categoryId == 0)
                return new PagedList<ProductCategory>(new List<ProductCategory>(), pageIndex, pageSize);

            string key = string.Format(PRODUCTCATEGORIES_ALLBYCATEGORYID_KEY, showHidden, categoryId, pageIndex, pageSize, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = from pc in _productCategoryRepository.Table
                            join p in _productRepository.Table on pc.ProductId equals p.Id
                            where pc.CategoryId == categoryId &&
                                  !p.Deleted &&
                                  (showHidden || p.Published)
                            orderby pc.DisplayOrder
                            select pc;

                if (!showHidden && (!_catalogSettings.IgnoreAcl || !_catalogSettings.IgnoreStoreLimitations))
                {
                    if (!_catalogSettings.IgnoreAcl)
                    {
                        //ACL (access control list)
                        var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                            .Where(cr => cr.Active).Select(cr => cr.Id).ToList();
                        query = from pc in query
                                join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                                join acl in _aclRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into c_acl
                                from acl in c_acl.DefaultIfEmpty()
                                where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                                select pc;
                    }
                    if (!_catalogSettings.IgnoreStoreLimitations)
                    {
                        //Store mapping
                        var currentStoreId = _storeContext.CurrentStore.Id;
                        query = from pc in query
                                join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                                join sm in _storeMappingRepository.Table
                                on new { c1 = c.Id, c2 = "Category" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into c_sm
                                from sm in c_sm.DefaultIfEmpty()
                                where !c.LimitedToStores || currentStoreId == sm.StoreId
                                select pc;
                    }
                    //only distinct categories (group by ID)
                    query = from c in query
                            group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                    query = query.OrderBy(pc => pc.DisplayOrder);
                }

                var productCategories = new PagedList<ProductCategory>(query, pageIndex, pageSize);
                return productCategories;
            });
        }

        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId"> Product identifier</param>
        /// <param name="showHidden"> A value indicating whether to show hidden records</param>
        /// <returns> Product category mapping collection</returns>
        public virtual IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductCategory>();

            string key = string.Format(PRODUCTCATEGORIES_ALLBYPRODUCTID_KEY, showHidden, productId, _workContext.CurrentCustomer.Id, _storeContext.CurrentStore.Id);
            return _cacheManager.Get(key, () =>
            {
                var query = from pc in _productCategoryRepository.Table
                            join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                            where pc.ProductId == productId &&
                                  !c.Deleted &&
                                  (showHidden || c.Published)
                            orderby pc.DisplayOrder
                            select pc;

                var allProductCategories = query.ToList();
                var result = new List<ProductCategory>();
                if (!showHidden)
                {
                    foreach (var pc in allProductCategories)
                    {
                        //ACL (access control list) and store mapping
                        var category = pc.Category;
                        if (_aclService.Authorize(category) && _storeMappingService.Authorize(category))
                            result.Add(pc);
                    }
                }
                else
                {
                    //no filtering
                    result.AddRange(allProductCategories);
                }
                return result;
            });
        }

        /// <summary>
        /// Gets a product category mapping 
        /// </summary>
        /// <param name="productCategoryId">Product category mapping identifier</param>
        /// <returns>Product category mapping</returns>
        public virtual ProductCategory GetProductCategoryById(int productCategoryId)
        {
            if (productCategoryId == 0)
                return null;

            return _productCategoryRepository.GetById(productCategoryId);
        }

        /// <summary>
        /// Inserts a product category mapping
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        public virtual void InsertProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");
            
            _productCategoryRepository.Insert(productCategory);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(productCategory);
        }

        /// <summary>
        /// Updates the product category mapping 
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        public virtual void UpdateProductCategory(ProductCategory productCategory)
        {
            if (productCategory == null)
                throw new ArgumentNullException("productCategory");

            _productCategoryRepository.Update(productCategory);

            //cache
            _cacheManager.RemoveByPattern(CATEGORIES_PATTERN_KEY);
            _cacheManager.RemoveByPattern(PRODUCTCATEGORIES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(productCategory);
        }

        #endregion

        #region Blogs We Love Methods

        public virtual void DeleteBlogsWeLove(BlogsWeLove blogsWeLove)
        {
            if (blogsWeLove == null)
                throw new ArgumentNullException("blogsWeLove");

            blogsWeLove.Deleted = true;
            UpdateBlogsWeLove(blogsWeLove);
        }

        public virtual void UpdateBlogsWeLove(BlogsWeLove blogsWeLove)
        {
            if (blogsWeLove == null)
                throw new ArgumentNullException("blogsWeLove");

            _blogsWeLoveRepository.Update(blogsWeLove);

            //event notification
            _eventPublisher.EntityUpdated(blogsWeLove);
        }

        public virtual void InsertBlogsWeLove(BlogsWeLove blogsWeLove)
        {
            if (blogsWeLove == null)
                throw new ArgumentNullException("blogsWeLove");

            _blogsWeLoveRepository.Insert(blogsWeLove);

            //event notification
            _eventPublisher.EntityInserted(blogsWeLove);
        }

        public virtual BlogsWeLove GetBlogsWeLoveById(int blogsWeLoveId)
        {
            if (blogsWeLoveId == 0)
                return null;

            var blogsWeLove = _blogsWeLoveRepository.GetById(blogsWeLoveId);
            return blogsWeLove;
        }

        public virtual IPagedList<BlogsWeLove> GetAllBlogsWeLoves(string BlogName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _blogsWeLoveRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            if (!String.IsNullOrEmpty(BlogName))
                query = query.Where(c => c.Title.Contains(BlogName));
            query = query.OrderByDescending(c => c.CreatedOn).ThenByDescending(c => c.Id);

            //paging
            return new PagedList<BlogsWeLove>(query.ToList(), pageIndex, pageSize);
        }

        #endregion

        #region Book Methods

        public virtual void DeleteBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            book.Deleted = true;
            UpdateBook(book);
        }

        public virtual void UpdateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            _bookRepository.Update(book);

            //event notification
            _eventPublisher.EntityUpdated(book);
        }

        public virtual void InsertBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            _bookRepository.Insert(book);

            //event notification
            _eventPublisher.EntityInserted(book);
        }

        public virtual Book GetBookById(int bookId)
        {
            if (bookId == 0)
                return null;

            var book = _bookRepository.GetById(bookId);
            return book;
        }

        public virtual IPagedList<Book> GetAllBooks(string BookName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _bookRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            if (!String.IsNullOrEmpty(BookName))
                query = query.Where(c => c.Title.Contains(BookName));
            query = query.OrderByDescending(c => c.CreatedOnUtc).ThenByDescending(c => c.Id);

            //paging
            return new PagedList<Book>(query.ToList(), pageIndex, pageSize);
        }

        public virtual List<Book> GetAllBooksByAuthorId(int authorId)
        {
            var query = _bookRepository.Table;
            query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            query = query.Where(c => c.AuthorId == authorId);
            query = query.OrderByDescending(c => c.CreatedOnUtc).ThenByDescending(c => c.Id);

            return query.ToList();
        }

        #endregion

        #region Site We Love Methods

        public virtual void DeleteSiteWeLove(SiteWeLove sitewelove)
        {
            if (sitewelove == null)
                throw new ArgumentNullException("sitewelove");

            sitewelove.Deleted = true;
            UpdateSiteWeLove(sitewelove);
        }

        public virtual void UpdateSiteWeLove(SiteWeLove sitewelove)
        {
            if (sitewelove == null)
                throw new ArgumentNullException("sitewelove");

            _siteWeLoveRepository.Update(sitewelove);

            //event notification
            _eventPublisher.EntityUpdated(sitewelove);
        }

        public virtual void InsertSiteWeLove(SiteWeLove sitewelove)
        {
            if (sitewelove == null)
                throw new ArgumentNullException("sitewelove");

            _siteWeLoveRepository.Insert(sitewelove);

            //event notification
            _eventPublisher.EntityInserted(sitewelove);
        }

        public virtual SiteWeLove GetSiteWeLoveById(int siteweloveId)
        {
            if (siteweloveId == 0)
                return null;

            var sitewelove = _siteWeLoveRepository.GetById(siteweloveId);
            return sitewelove;
        }

        public virtual IPagedList<SiteWeLove> GetAllSiteWeLoves(string SiteName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _siteWeLoveRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            if (!String.IsNullOrEmpty(SiteName))
                query = query.Where(c => c.Title.Contains(SiteName));
            query = query.OrderByDescending(c => c.CreatedOnUtc).ThenByDescending(c => c.Id);

            //paging
            return new PagedList<SiteWeLove>(query.ToList(), pageIndex, pageSize);
        }

        #endregion

        #region Sponsorship Method

        public virtual void InsertSponsorship(Sponsorship sponsorship)
        {
            if (sponsorship == null)
                throw new ArgumentNullException("sponsorship");

            _sponsorshipRepository.Insert(sponsorship);

            //event notification
            _eventPublisher.EntityInserted(sponsorship);
        }

        public virtual void DeleteSponsorship(Sponsorship sponsorship)
        {
            if (sponsorship == null)
                throw new ArgumentNullException("sponsorship");

            sponsorship.Deleted = true;
            UpdateSponsorship(sponsorship);
        }

        public virtual void UpdateSponsorship(Sponsorship sponsorship)
        {
            if (sponsorship == null)
                throw new ArgumentNullException("sponsorship");

            _sponsorshipRepository.Update(sponsorship);

            //event notification
            _eventPublisher.EntityUpdated(sponsorship);
        }
        
        public virtual IPagedList<Sponsorship> GetAllSponsorships(string SponsorName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _sponsorshipRepository.Table;
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.Id);
            if (!String.IsNullOrWhiteSpace(SponsorName))
                query = query.Where(x => x.FirstName.Contains(SponsorName));
            //paging
            return new PagedList<Sponsorship>(query.ToList(), pageIndex, pageSize);
        }

        public virtual Sponsorship GetSponsorshipById(int sponsorshipId)
        {
            if (sponsorshipId == 0)
                return null;

            var sponsorship = _sponsorshipRepository.GetById(sponsorshipId);
            return sponsorship;
        }

        #endregion

        #region SurveyCategoryRegistration

        public virtual void InsertSurveyRegistration(SurveyRegistration surveyregistration)
        {
            if (surveyregistration == null)
                throw new ArgumentNullException("surveyregistration");

            _surveyRegistrationRepoitory.Insert(surveyregistration);

            //event notification
            _eventPublisher.EntityInserted(surveyregistration);
        }

        public virtual void UpdateSurveyRegistration(SurveyRegistration surveyregistration)
        {
            if(surveyregistration==null )
                throw new ArgumentNullException("surveyregistration");

            _surveyRegistrationRepoitory.Update(surveyregistration);

            _eventPublisher.EntityUpdated(surveyregistration);
        }

        public virtual void DeleteSurveyRegistration(SurveyRegistration surveyregistration)
        {
            if (surveyregistration == null)
                throw new ArgumentNullException("surveyregistration");

            surveyregistration.Deleted = true;
            UpdateSurveyRegistration(surveyregistration);
        }

        public virtual List<SurveyCategory> GetAllSurveyCategories()
        {
            var query = _surveyCategoryRepository.Table;
            
            query = query.OrderBy(c => c.Name);

            return query.ToList();
        }
                
        public virtual void InsertSurveyCategoryRegistration(SurveyCategoryRegistration surveycategoryregistration)
        {
            if (surveycategoryregistration == null)
                throw new ArgumentNullException("surveycategoryregistration");

            _surveyCategoryRegistrationRepository.Insert(surveycategoryregistration);

            _eventPublisher.EntityInserted(surveycategoryregistration);
        }

        public virtual void UpdateSurveyCategoryRegistration(SurveyCategoryRegistration surveycategoryregistration)
        {
            if (surveycategoryregistration == null)
                throw new ArgumentNullException("surveycategoryregistration");

            _surveyCategoryRegistrationRepository.Update(surveycategoryregistration);

            _eventPublisher.EntityInserted(surveycategoryregistration);
        }

        public virtual void DeleteCategoryRegistration(SurveyCategoryRegistration surveyCategoryRegistration)
        {

            if (surveyCategoryRegistration == null)
                throw new ArgumentNullException("surveyCategoryRegistration");

            _surveyCategoryRegistrationRepository.Delete(surveyCategoryRegistration);

            //_cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(surveyCategoryRegistration);

            //var qurey = _surveyCategoryRegistrationRepository.Table;
            //qurey = qurey.OrderBy(c => c.RegistrationId);
            
            ////event notification
            //_eventPublisher.EntityDeleted(surveyCategoryRegistrationId);
        }
                
        public virtual SurveyCategoryRegistration GetSurveyCategoryRegistrationById(int surveyCategoryRegistrationId)
        {
            if (surveyCategoryRegistrationId == 0)
                return null;

            var surveycategoryRegistration = _surveyCategoryRegistrationRepository.GetById(surveyCategoryRegistrationId);
            return surveycategoryRegistration;
        }

        public virtual IPagedList<SurveyRegistration> GetAllSurveys(string SurveyName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _surveyRegistrationRepoitory.Table;

            query = query.Where(c => !c.Deleted);
            if (!String.IsNullOrWhiteSpace(SurveyName))
                query = query.Where(x => x.FirstName.Contains(SurveyName));
            query = query.OrderBy(c => c.Id);
            //paging
            return new PagedList<SurveyRegistration>(query.ToList(), pageIndex, pageSize);
        }

        public virtual SurveyRegistration GetSurveyById(int surveyRegistrationId)
        {
            if (surveyRegistrationId == 0)
                return null;

            var surveyRegistration = _surveyRegistrationRepoitory.GetById(surveyRegistrationId);
            return surveyRegistration;
        }

        public virtual IList<SurveyCategoryRegistration> GetsurveyCategorybyId(int surveyRegistrationId)
        {
            var query = from p in _surveyCategoryRegistrationRepository.Table

                        where p.RegistrationId == surveyRegistrationId
                        select p;

            return query.ToList();
        }

        #endregion

        #region Events

        public virtual void InsertEvents(Event Events)
        {
            if (Events == null)
                throw new ArgumentNullException("Events");

            _eventRepository.Insert(Events);

            //event notification
            _eventPublisher.EntityInserted(Events);
        }

        public virtual void UpdateEvent(Event events)
        {
            if (events == null)
                throw new ArgumentNullException("events");

            _eventRepository.Update(events);

            //event notification
            _eventPublisher.EntityUpdated(events);
        }

        public virtual void DeleteEvent(Event events)
        {
            if (events == null)
                throw new ArgumentNullException("events");

            events.Deleted = true;
            UpdateEvent(events);
        }

        public virtual List<Event> GetEventsDisplayedOnCommunityPage()
        {
            var query = _eventRepository.Table;
            query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            query = query.Where(c => c.ShowOnCommunity);

            query = query.OrderBy(c => c.Id);

            return query.ToList();
        }

        public virtual Event GetEventById(int eventId)
        {
            if (eventId == 0)
                return null;

            var events = _eventRepository.GetById(eventId);
                return events;
        }

        public virtual IPagedList<Event> GetAllEvents(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _eventRepository.Table;
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.Id);

            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));

            //paging
            return new PagedList<Event>(query.ToList(), pageIndex, pageSize);
        }

        #endregion

        #region Category Video Mapping Methods

        public virtual void DeleteCategoryVideo(CategoryVideo categoryVideo)
        {
            if (categoryVideo == null)
                throw new ArgumentNullException("categoryVideo");

            _categoryVideoRepository.Delete(categoryVideo);
        }

        public virtual IPagedList<CategoryVideo> GetCategoryVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (videoId == 0)
                return new PagedList<CategoryVideo>(new List<CategoryVideo>(), pageIndex, pageSize);

            var query = from pc in _categoryVideoRepository.Table
                        join p in _categoryRepository.Table on pc.CategoryId equals p.Id
                        where pc.VideoId == videoId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var categoryVideos = new PagedList<CategoryVideo>(query, pageIndex, pageSize);
            return categoryVideos;
        }

        public virtual IList<CategoryVideo> GetCategoryVideosByCategoryId(int categoryId, bool showHidden = false)
        {
            if (categoryId == 0)
                return new List<CategoryVideo>();

            var query = from pc in _categoryVideoRepository.Table
                        join c in _videoRepository.Table on pc.VideoId equals c.Id
                        where pc.CategoryId == categoryId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var categoryVideos = query.ToList();
            return categoryVideos;
        }

        public virtual CategoryVideo GetCategoryVideoById(int categoryVideoId)
        {
            if (categoryVideoId == 0)
                return null;

            return _categoryVideoRepository.GetById(categoryVideoId);
        }

        public virtual void InsertCategoryVideo(CategoryVideo categoryVideo)
        {
            if (categoryVideo == null)
                throw new ArgumentNullException("categoryVideo");

            _categoryVideoRepository.Insert(categoryVideo);
        }

        public virtual void UpdateCategoryVideo(CategoryVideo categoryVideo)
        {
            if (categoryVideo == null)
                throw new ArgumentNullException("categoryVideo");

            _categoryVideoRepository.Update(categoryVideo);
        }

        #endregion

        #region Project Cat

        public virtual void DeleteProjectCat(ProjectCat projectCat)
        {
            if (projectCat == null)
                throw new ArgumentNullException("projectCat");

            _projectCatRepository.Delete(projectCat);

            //event notification
            _eventPublisher.EntityDeleted(projectCat);
        }

        public virtual IPagedList<ProjectCat> GetProjectCatByCategoryId(int categoryId,
            int pageIndex, int pageSize, bool showHidden = false)
        {
            if (categoryId == 0)
                return new PagedList<ProjectCat>(new List<ProjectCat>(), pageIndex, pageSize);

            var query = from pm in _projectCatRepository.Table
                        join p in _projectRepository.Table on pm.ProjectId equals p.Id
                        where pm.CategoryId == categoryId &&
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
                        join m in _categoryRepository.Table on pm.CategoryId equals m.Id
                        join acl in _aclRepository.Table on m.Id equals acl.EntityId into m_acl
                        from acl in m_acl.DefaultIfEmpty()
                        where
                            !m.SubjectToAcl ||
                            (acl.EntityName == "Category" && allowedCustomerRolesIds.Contains(acl.CustomerRoleId))
                        select pm;

                //only distinct manufacturers (group by ID)
                query = from pm in query
                        group pm by pm.Id
                            into pmGroup
                            orderby pmGroup.Key
                            select pmGroup.FirstOrDefault();
                query = query.OrderBy(pm => pm.DisplayOrder);
            }

            var projectCat = new PagedList<ProjectCat>(query, pageIndex, pageSize);
            return projectCat;
        }

        public virtual IList<ProjectCat> GetProjectCatByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectCat>();

            var query = from pm in _projectCatRepository.Table
                        join m in _categoryRepository.Table on pm.CategoryId equals m.Id
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
                        join m in _categoryRepository.Table on pm.CategoryId equals m.Id
                        join acl in _aclRepository.Table on m.Id equals acl.EntityId into m_acl
                        from acl in m_acl.DefaultIfEmpty()
                        where !m.SubjectToAcl || (acl.EntityName == "Category" && allowedCustomerRolesIds.Contains(acl.CustomerRoleId))
                        select pm;

                //only distinct manufacturers (group by ID)
                query = from pm in query
                        group pm by pm.Id
                            into mGroup
                            orderby mGroup.Key
                            select mGroup.FirstOrDefault();
                query = query.OrderBy(pm => pm.DisplayOrder);
            }

            var projectCat = query.ToList();
            return projectCat;
        }

        public virtual ProjectCat GetProjectCatById(int projectCatId)
        {
            if (projectCatId == 0)
                return null;
            return _projectCatRepository.GetById(projectCatId);
        }

        public virtual void InsertProjectCat(ProjectCat projectCat)
        {
            if (projectCat == null)
                throw new ArgumentNullException("projectCat");

            _projectCatRepository.Insert(projectCat);

            //event notification
            _eventPublisher.EntityInserted(projectCat);
        }

        public virtual void UpdateProjectCat(ProjectCat projectCat)
        {
            if (projectCat == null)
                throw new ArgumentNullException("projectCat");

            _projectCatRepository.Update(projectCat);

            //event notification
            _eventPublisher.EntityUpdated(projectCat);
        }

        #endregion
    }
}
