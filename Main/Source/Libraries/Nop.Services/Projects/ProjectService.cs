using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Projects;
using Nop.Core.Domain.Security;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Media;

namespace Nop.Services.Projects
{
    public partial class ProjectService : IProjectService
    {
        #region Fields

        private readonly IRepository<ProjectCategory> _projectCategoryRepository;
        private readonly IRepository<ProjectCategoryMapping> _projectCategoryMappingRepository;
        private readonly IRepository<ProjectProduct> _projectProductRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<ProjectReview> _projectReviewRepository;
        private readonly IRepository<RelatedProject> _relatedProjectRepository;
        private readonly IRepository<ProjectPictureMapping> _projectPictureMappingRepository;
        private readonly IRepository<ProjectInstruction> _projectInstructionRepository;
        private readonly IRepository<ProjectMisc> _projectMiscRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IWorkContext _workContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly HttpContextBase _httpContext;
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<ProjectVideo> _projectVideoRepository;
        private readonly IRepository<Pattern> _patternRepository;
        private readonly IRepository<ProjectPattern> _projectPatternRepository;
        private readonly IRepository<ProjectFavorite> _projectFavoriteRepository;
        private readonly IRepository<ProjectLike> _projectLikeRepository;
        private readonly IRepository<ProjectCustomer> _projectCustomerRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Follower> _followerRepository;
        private readonly IRepository<Technique> _techniqueRepository;
        private readonly IRepository<TechniqueDetail> _techniqueDetailRepository;
        private readonly IRepository<ProjectTechnique> _projectTechniqueRepository;
        private readonly IRepository<UserGallery> _userGalleryRepository;
        private readonly IRepository<ProjectTag> _projectTagRepository;
        private readonly IRepository<ProjectMaterial> _projectMaterialRepository;
        private readonly IRepository<Workshop> _workshopRepository;
        private readonly IRepository<BusinessBuilderProduct> _businessBuilderProductRepository;
        private readonly IRepository<BusinessBuilderCategory> _businessBuilderCategoryRepository;
        private readonly IRepository<BusinessBuilderProductCategory> _businessBuilderProductCategoryRepository;
        private readonly IRepository<AmbassadorLinksProduct> _ambassadorLinksProductRepository;
        private readonly IRepository<AmbassadorLinksCategory> _ambassadorLinksCategoryRepository;
        private readonly IRepository<AmbassadorLinksProductCategory> _ambassadorLinksProductCategoryRepository;
        private readonly IRepository<DocumentCenter> _documentCenterRepository;
        private readonly IRepository<GalleryProduct> _galleryProductRepository;
        private readonly IRepository<GalleryCategory> _galleryCategoryRepository;
        private readonly IRepository<GalleryProductCategory> _galleryProductCategoryRepository;
        private readonly IRepository<DistributorLink> _distributorLinkRepository;
        private readonly IRepository<ArticleComment> _articleCommentsRepository;
        private readonly IRepository<ArticleCommentMapping> _articleCommentsMappingRepository;

        #endregion

        #region Ctor

        public ProjectService(IRepository<ProjectCategory> projectCategoryRepository,
            IRepository<ProjectCategoryMapping> projectCategoryMappingRepository,
            IRepository<Project> projectRepository, IRepository<ProjectProduct> projectProductRepository,
            IRepository<Picture> pictureRepository, IRepository<Product> productRepository,
            IRepository<ProjectPictureMapping> projectPictureMappingRepository,
            IRepository<ProjectReview> projectReviewRepository, IRepository<ProjectInstruction> projectInstructionRepository,
            IRepository<RelatedProject> relatedProjectRepository, IRepository<ProjectMisc> projectMiscRepository,
            IRepository<AclRecord> aclRepository, IWorkContext workContext,
            IDataProvider dataProvider, IDbContext dbContext, IEventPublisher eventPublisher, HttpContextBase httpContext,
            IRepository<Video> videoRepository, IRepository<ProjectVideo> projectVideoRepository,
            IRepository<Pattern> patternRepository, IRepository<ProjectPattern> projectPatternRepository,
            IRepository<ProjectFavorite> projectFavoriteRepository, IRepository<ProjectLike> projectLikeRepository,
            IRepository<ProjectCustomer> projectCustomerRepository, IRepository<Customer> customerRepository,
            IRepository<Follower> followerRepository, IRepository<Technique> techniqueRepository,
            IRepository<TechniqueDetail> techniqueDetailRepository, IRepository<ProjectTechnique> projectTechniqueRepository,
            IRepository<UserGallery> userGalleryRepository, IRepository<ProjectTag> projectTagRepository,
            IRepository<ProjectMaterial> projectMaterialRepository, IRepository<Workshop> workshopRepository,
            IRepository<BusinessBuilderProduct> businessBuilderProductRepository,
            IRepository<BusinessBuilderCategory> businessBuilderCategoryRepository,
            IRepository<BusinessBuilderProductCategory> businessBuilderProductCategoryRepository,
            IRepository<AmbassadorLinksProduct> ambassadorLinksProductRepository,
            IRepository<AmbassadorLinksCategory> ambassadorLinksCategoryRepository,
            IRepository<AmbassadorLinksProductCategory> ambassadorLinksProductCategoryRepository,
            IRepository<DocumentCenter> documentCenterRepository,
            IRepository<GalleryProduct> galleryProductRepository,
            IRepository<GalleryCategory> galleryCategoryRepository,
            IRepository<GalleryProductCategory> galleryProductCategoryRepository,
            IRepository<DistributorLink> distributorLinkRepository,
            IRepository<ArticleComment> articleCommentsRepository, IRepository<ArticleCommentMapping> articleCommentsMappingRepository)
        {
            this._projectCategoryRepository = projectCategoryRepository;
            this._projectCategoryMappingRepository = projectCategoryMappingRepository;
            this._projectRepository = projectRepository;
            this._projectProductRepository = projectProductRepository;
            this._pictureRepository = pictureRepository;
            this._productRepository = productRepository;
            this._projectPictureMappingRepository = projectPictureMappingRepository;
            this._projectReviewRepository = projectReviewRepository;
            this._projectInstructionRepository = projectInstructionRepository;
            this._relatedProjectRepository = relatedProjectRepository;
            this._projectMiscRepository = projectMiscRepository;
            this._aclRepository = aclRepository;
            this._workContext = workContext;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._eventPublisher = eventPublisher;
            this._httpContext = httpContext;
            this._videoRepository = videoRepository;
            this._projectVideoRepository = projectVideoRepository;
            this._patternRepository = patternRepository;
            this._projectPatternRepository = projectPatternRepository;
            this._projectFavoriteRepository = projectFavoriteRepository;
            this._projectLikeRepository = projectLikeRepository;
            this._projectCustomerRepository = projectCustomerRepository;
            this._customerRepository = customerRepository;
            this._followerRepository = followerRepository;
            this._techniqueRepository = techniqueRepository;
            this._techniqueDetailRepository = techniqueDetailRepository;
            this._projectTechniqueRepository = projectTechniqueRepository;
            this._userGalleryRepository = userGalleryRepository;
            this._projectTagRepository = projectTagRepository;
            this._projectMaterialRepository = projectMaterialRepository;
            this._workshopRepository = workshopRepository;
            this._businessBuilderProductRepository = businessBuilderProductRepository;
            this._businessBuilderCategoryRepository = businessBuilderCategoryRepository;
            this._businessBuilderProductCategoryRepository = businessBuilderProductCategoryRepository;
            this._ambassadorLinksProductRepository = ambassadorLinksProductRepository;
            this._ambassadorLinksCategoryRepository = ambassadorLinksCategoryRepository;
            this._ambassadorLinksProductCategoryRepository = ambassadorLinksProductCategoryRepository;
            this._documentCenterRepository = documentCenterRepository;
            this._galleryProductRepository = galleryProductRepository;
            this._galleryCategoryRepository = galleryCategoryRepository;
            this._galleryProductCategoryRepository = galleryProductCategoryRepository;
            this._distributorLinkRepository = distributorLinkRepository;
            this._articleCommentsRepository = articleCommentsRepository;
            this._articleCommentsMappingRepository = articleCommentsMappingRepository;
        }

        #endregion

        #region Category Methods

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void DeleteProjectCategory(ProjectCategory category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            category.Deleted = true;
            UpdateProjectCategory(category);
        }

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual IPagedList<ProjectCategory> GetAllProjectCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _projectCategoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(categoryName))
                query = query.Where(c => c.Name.Contains(categoryName));
            query = query.Where(c => !c.Deleted);
            query = query.OrderBy(c => c.ParentCategoryId).ThenBy(c => c.DisplayOrder);

            var unsortedCategories = query.ToList();

            //paging
            return new PagedList<ProjectCategory>(unsortedCategories, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        public IList<ProjectCategory> GetAllProjectCategoriesByParentCategoryId(int parentProjectCategoryId,
            bool showHidden = false)
        {
            var query = from pc in _projectCategoryRepository.Table
                        orderby pc.DisplayOrder
                        where !pc.Deleted && pc.Published && pc.ParentCategoryId == parentProjectCategoryId
                        select pc;

            var categories = query.ToList();
            return categories;
        }

        public IList<ProjectCategory> GetAllProjectCategoriesInMenuByParentCategoryId(int parentProjectCategoryId,
            bool showHidden = false)
        {
            var query = from pc in _projectCategoryRepository.Table
                        orderby pc.DisplayOrder
                        where !pc.Deleted &&
                               pc.Published &&
                               pc.ParentCategoryId == parentProjectCategoryId &&
                               pc.ShowInMenu == true
                        select pc;

            var categories = query.ToList();
            return categories;
        }

        public IList<ProjectCategory> GetAllProjectCategoriesInSidebarByParentCategoryId(int parentProjectCategoryId,
            bool showHidden = false)
        {
            var query = from pc in _projectCategoryRepository.Table
                        orderby pc.DisplayOrder
                        where !pc.Deleted &&
                               pc.Published &&
                               pc.ParentCategoryId == parentProjectCategoryId &&
                               pc.ShowInSidebar == true
                        select pc;

            var categories = query.ToList();
            return categories;
        }

        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <returns>Categories</returns>
        public virtual IList<ProjectCategory> GetAllProjectCategoriesDisplayedOnHomePage()
        {
            var query = from c in _projectCategoryRepository.Table
                        orderby c.DisplayOrder
                        where c.Published &&
                        !c.Deleted &&
                        c.ShowOnHomePage
                        select c;

            var categories = query.ToList();
            return categories;
        }

        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        public virtual ProjectCategory GetProjectCategoryById(int projectCategoryId)
        {
            if (projectCategoryId == 0)
                return null;

            var category = _projectCategoryRepository.GetById(projectCategoryId);
            return category;
        }

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void InsertProjectCategory(ProjectCategory projectCategory)
        {
            if (projectCategory == null)
                throw new ArgumentNullException("category");

            _projectCategoryRepository.Insert(projectCategory);
        }

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        public virtual void UpdateProjectCategory(ProjectCategory projectCategory)
        {
            if (projectCategory == null)
                throw new ArgumentNullException("category");

            //validate category hierarchy
            var parentCategory = GetProjectCategoryById(projectCategory.ParentCategoryId);
            while (parentCategory != null)
            {
                if (projectCategory.Id == parentCategory.Id)
                {
                    projectCategory.ParentCategoryId = 0;
                    break;
                }
                parentCategory = GetProjectCategoryById(parentCategory.ParentCategoryId);
            }

            _projectCategoryRepository.Update(projectCategory);
        }

        public int GetCategoryIdByName(string p)
        {
            var query = from c in _projectCategoryRepository.Table
                        where c.Published &&
                        !c.Deleted && c.Name == p
                        select c;

            var category = query.FirstOrDefault();
            if (category != null)
            {
                return category.Id;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region Project Category Mapping Methods

        public virtual void DeleteProjectCategoryMapping(ProjectCategoryMapping projectCategoryMapping)
        {
            if (projectCategoryMapping == null)
                throw new ArgumentNullException("projectCategory");

            _projectCategoryMappingRepository.Delete(projectCategoryMapping);
        }

        public virtual IPagedList<ProjectCategoryMapping> GetProjectCategoryMappingsByProjectCategoryId(int projectCategoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (projectCategoryId == 0)
                return new PagedList<ProjectCategoryMapping>(new List<ProjectCategoryMapping>(), pageIndex, pageSize);

            var query = from pc in _projectCategoryMappingRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.ProjectCategoryId == projectCategoryId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectCategoryMappings = new PagedList<ProjectCategoryMapping>(query, pageIndex, pageSize);
            return projectCategoryMappings;
        }

        public virtual IList<ProjectCategoryMapping> GetProjectCategoryMappingsByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectCategoryMapping>();

            var query = from pc in _projectCategoryMappingRepository.Table
                        join c in _projectCategoryRepository.Table on pc.ProjectCategoryId equals c.Id
                        where pc.ProjectId == projectId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectCategoryMappings = query.ToList();
            return projectCategoryMappings;
        }

        public virtual ProjectCategoryMapping GetProjectCategoryMappingById(int projectCategoryMappingId)
        {
            if (projectCategoryMappingId == 0)
                return null;

            return _projectCategoryMappingRepository.GetById(projectCategoryMappingId);
        }

        public virtual void InsertProjectCategoryMapping(ProjectCategoryMapping projectCategoryMapping)
        {
            if (projectCategoryMapping == null)
                throw new ArgumentNullException("projectCategory");

            _projectCategoryMappingRepository.Insert(projectCategoryMapping);
        }

        public virtual void UpdateProjectCategoryMapping(ProjectCategoryMapping projectCategoryMapping)
        {
            if (projectCategoryMapping == null)
                throw new ArgumentNullException("projectCategory");

            _projectCategoryMappingRepository.Update(projectCategoryMapping);
        }

        #endregion

        #region Project Product Mapping Methods

        public virtual void DeleteProjectProduct(ProjectProduct projectProduct)
        {
            if (projectProduct == null)
                throw new ArgumentNullException("productCategory");

            _projectProductRepository.Delete(projectProduct);
        }

        public virtual IPagedList<ProjectProduct> GetProjectProductsByProductId(int productId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (productId == 0)
                return new PagedList<ProjectProduct>(new List<ProjectProduct>(), pageIndex, pageSize);

            var query = from pc in _projectProductRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.ProductId == productId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectProducts = new PagedList<ProjectProduct>(query, pageIndex, pageSize);
            return projectProducts;
        }

        public virtual IList<ProjectProduct> GetProjectProductsByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectProduct>();

            var query = from pc in _projectProductRepository.Table
                        join c in _productRepository.Table on pc.ProductId equals c.Id
                        where pc.ProjectId == projectId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectProducts = query.ToList();
            return projectProducts;
        }

        public virtual IList<Project> GetProjectsByProductId(int productId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<Project>();

            var query = from pc in _projectProductRepository.Table
                        join c in _productRepository.Table on pc.ProductId equals c.Id
                        where pc.ProductId == productId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc.Project;

            var projects = query.ToList();
            return projects;
        }

        public virtual ProjectProduct GetProjectProductById(int projectProductId)
        {
            if (projectProductId == 0)
                return null;

            return _projectProductRepository.GetById(projectProductId);
        }

        public virtual void InsertProjectProduct(ProjectProduct projectProduct)
        {
            if (projectProduct == null)
                throw new ArgumentNullException("productCategory");

            _projectProductRepository.Insert(projectProduct);
        }

        public virtual void UpdateProjectProduct(ProjectProduct projectProduct)
        {
            if (projectProduct == null)
                throw new ArgumentNullException("productCategory");

            _projectProductRepository.Update(projectProduct);
        }

        public virtual IPagedList<ProjectProduct> GetProjectProductsByProjectId(int projectId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (projectId == 0)
                return new PagedList<ProjectProduct>(new List<ProjectProduct>(), pageIndex, pageSize);

            var query = from pc in _projectProductRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where pc.ProjectId == projectId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectProducts = new PagedList<ProjectProduct>(query, pageIndex, pageSize);
            return projectProducts;
        }

        #endregion

        #region Projects Methods

        public virtual void UpdateProjectReviewTotals(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            int approvedRatingSum = 0;
            int notApprovedRatingSum = 0;
            int approvedTotalReviews = 0;
            int notApprovedTotalReviews = 0;
            var reviews = project.ProjectReviews;
            foreach (var pr in reviews)
            {
                if (pr.IsApproved)
                {
                    approvedRatingSum += pr.Rating;
                    approvedTotalReviews++;
                }
                else
                {
                    notApprovedRatingSum += pr.Rating;
                    notApprovedTotalReviews++;
                }
            }

            project.ApprovedRatingSum = approvedRatingSum;
            project.NotApprovedRatingSum = notApprovedRatingSum;
            project.ApprovedTotalReviews = approvedTotalReviews;
            project.NotApprovedTotalReviews = notApprovedTotalReviews;
            UpdateProject(project);
        }

        public virtual void DeleteProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            project.Deleted = true;
            //delete project
            UpdateProject(project);
        }

        public virtual IList<Project> GetAllFeaturedProjectsDisplayedOnHomePage()
        {
            var query = from p in _projectRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.Featured
                        select p;

            var projects = query.ToList();
            return projects;
        }

        public virtual IList<Project> GetAllFeaturedProjectsDisplayedOnCommunityPage()
        {
            var query = from p in _projectRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnCommunity
                        select p;

            var projects = query.ToList();
            return projects;
        }

        public virtual Project GetProjectById(int projectId)
        {
            if (projectId == 0)
                return null;

            var project = _projectRepository.GetById(projectId);
            return project;
        }

        public virtual IList<Project> GetProjectsByIds(int[] projectIds)
        {
            if (projectIds == null || projectIds.Length == 0)
                return new List<Project>();

            var query = from p in _projectRepository.Table
                        where projectIds.Contains(p.Id)
                        select p;
            var projects = query.ToList();
            //sort by passed identifiers
            var sortedProjects = new List<Project>();
            foreach (int id in projectIds)
            {
                var project = projects.Find(x => x.Id == id);
                if (project != null)
                    sortedProjects.Add(project);
            }
            return sortedProjects;
        }

        public virtual void InsertProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            //insert
            _projectRepository.Insert(project);
        }

        public virtual void UpdateProject(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            //update
            _projectRepository.Update(project);
        }

        public virtual IList<Project> GetAllProjects(bool showHidden = false)
        {
            var query = from p in _projectRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            var projects = query.ToList();
            return projects;
        }

        public virtual IList<Project> GetAllProjectsDisplayedOnHomePage()
        {
            var query = from p in _projectRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage
                        select p;
            var projects = query.ToList();
            return projects;
        }

        public virtual IPagedList<Project> GetAllProjects(int pageIndex, int pageSize, int manufacturerId = 0, int categoryId = 0, bool showHidden = false)
        {
            //projects
            var query = _projectRepository.Table;
            query = query.Where(p => !p.Deleted);
            if (!showHidden)
            {
                query = query.Where(p => p.Published);
            }
            //only distinct projects (group by ID)
            //if we use standard Distinct() method, then all fields will be compared (low performance)
            //it'll not work in SQL Server Compact when searching projects by a keyword)
            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();

            //manufacturer filtering
            if (manufacturerId > 0)
            {
                query = from p in query
                        from pm in p.ProjectManufacturers.Where(pm => pm.ManufacturerId == manufacturerId)
                        select p;
            }

            //category filtering
            if (categoryId > 0)
            {
                query = from p in query
                        from pm in p.ProjectCat.Where(pm => pm.CategoryId == categoryId)
                        select p;
            }

            //sort projects
            query = query.OrderBy(p => p.Name);

            var projects = new PagedList<Project>(query, pageIndex, pageSize);

            //return projects
            return projects;
        }

        /// <summary>
        /// Gets all projects
        /// </summary>
        /// <param name="manufacturerName">project name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Manufacturers</returns>
        public virtual IPagedList<Project> GetAllProjects(string projectName,
            int pageIndex, int pageSize, bool showHidden = false)
        {
            var project = GetAllProjects(projectName, showHidden);
            return new PagedList<Project>(project, pageIndex, pageSize);
        }

        public virtual IList<Project> GetAllProjects(string projectName, bool showHidden = false)
        {
            var query = from p in _projectRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;

            if (!String.IsNullOrEmpty(projectName))
            {
                query = query.Where(p => p.Name.Contains(projectName));
            }

            var projects = query.ToList();
            return projects;
        }

        public virtual IPagedList<Project> GetAllProjects(IList<int> categoryIds, string keywords, int pageIndex, int pageSize, out IList<int> filterableCatIds)
        {
            string commaSeparatedCategoryIds = "";
            filterableCatIds = new List<int>();
            if (categoryIds != null)
            {
                for (int i = 0; i < categoryIds.Count; i++)
                {
                    commaSeparatedCategoryIds += categoryIds[i].ToString();
                    if (i != categoryIds.Count - 1)
                    {
                        commaSeparatedCategoryIds += ",";
                    }
                }
            }

            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare parameters
            var pCategoryIds = _dataProvider.GetParameter();
            pCategoryIds.ParameterName = "ProjectCategoryIds";
            pCategoryIds.Value = commaSeparatedCategoryIds != null ? (object)commaSeparatedCategoryIds : DBNull.Value;
            pCategoryIds.DbType = DbType.String;

            var pKeywords = _dataProvider.GetParameter();
            pKeywords.ParameterName = "Keywords";
            pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
            pKeywords.DbType = DbType.String;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            var pFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
            pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableProjectCategoryIds";
            pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
            pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
            pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecords";
            pTotalRecords.Direction = ParameterDirection.Output;
            pTotalRecords.DbType = DbType.Int32;

            //invoke stored procedure
            var projects = _dbContext.ExecuteStoredProcedureList<Project>(
                "ProjectLoadAllPaged",
                pCategoryIds,
                pKeywords,
                pPageIndex,
                pPageSize,
                pFilterableSpecificationAttributeOptionIds,
                pTotalRecords);

            //get filterable specification attribute option identifier
            string filterableCatIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
            if (!string.IsNullOrWhiteSpace(filterableCatIdsStr))
            {
                filterableCatIds = filterableCatIdsStr
                   .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(x => Convert.ToInt32(x.Trim()))
                   .ToList();
            }
            //return projects
            int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return new PagedList<Project>(projects, pageIndex, pageSize, totalRecords);
        }

        public virtual IPagedList<Project> GetAllProjects(string query, int pageIndex, int pageSize)
        {
            //some databases don't support int.MaxValue
            if (pageSize == int.MaxValue)
                pageSize = int.MaxValue - 1;

            //prepare parameters
            var pQuery = _dataProvider.GetParameter();
            pQuery.ParameterName = "Query";
            pQuery.Value = query != null ? (object)query : DBNull.Value;
            pQuery.DbType = DbType.String;

            var pPageIndex = _dataProvider.GetParameter();
            pPageIndex.ParameterName = "PageIndex";
            pPageIndex.Value = pageIndex;
            pPageIndex.DbType = DbType.Int32;

            var pPageSize = _dataProvider.GetParameter();
            pPageSize.ParameterName = "PageSize";
            pPageSize.Value = pageSize;
            pPageSize.DbType = DbType.Int32;

            //invoke stored procedure
            var projects = _dbContext.ExecuteStoredProcedureList<Project>(
                "ProjectLoadAllPagedQuery",
                pQuery,
                pPageIndex,
                pPageSize);

            //return projects
            return new PagedList<Project>(projects, pageIndex, pageSize);
        }

        public virtual void DeleteProjectPicture(ProjectPictureMapping projectPicture)
        {
            if (projectPicture == null)
                throw new ArgumentNullException("projectPicture");

            _projectPictureMappingRepository.Delete(projectPicture);
        }

        public virtual IList<ProjectPictureMapping> GetProjectPicturesByProjectId(int projectId)
        {
            var query = from pp in _projectPictureMappingRepository.Table
                        where pp.ProjectId == projectId
                        orderby pp.DisplayOrder
                        select pp;
            var projectPictures = query.ToList();
            return projectPictures;
        }

        public virtual ProjectPictureMapping GetProjectPictureById(int projectPictureId)
        {
            if (projectPictureId == 0)
                return null;

            var pp = _projectPictureMappingRepository.GetById(projectPictureId);
            return pp;
        }

        public virtual void InsertProjectPicture(ProjectPictureMapping projectPicture)
        {
            if (projectPicture == null)
                throw new ArgumentNullException("projectPicture");

            _projectPictureMappingRepository.Insert(projectPicture);
        }

        public virtual void UpdateProjectPicture(ProjectPictureMapping projectPicture)
        {
            if (projectPicture == null)
                throw new ArgumentNullException("projectPicture");

            _projectPictureMappingRepository.Update(projectPicture);
        }

        public Picture GetDefaultProjectPicture(int projectId, IPictureService pictureService)
        {
            if (projectId == 0)
                throw new ArgumentNullException("source");

            if (pictureService == null)
                throw new ArgumentNullException("pictureService");

            var picture = GetPicturesByProjectId(projectId, 1).FirstOrDefault();
            return picture;
        }

        public IList<Picture> GetPicturesByProjectId(int projectId, int recordsToReturn)
        {
            if (projectId == 0)
                return new List<Picture>();


            var query = from p in _pictureRepository.Table
                        join pp in _projectPictureMappingRepository.Table on p.Id equals pp.PictureId
                        orderby pp.DisplayOrder
                        where pp.ProjectId == projectId
                        select p;

            if (recordsToReturn > 0)
                query = query.Take(recordsToReturn);

            var pics = query.ToList();
            return pics;
        }

        #endregion

        #region Project Reviews Methods

        public virtual void InsertProjectReview(ProjectReview projectReview)
        {
            if (projectReview == null)
                throw new ArgumentNullException("project");

            //insert
            _projectReviewRepository.Insert(projectReview);
        }

        #endregion

        #region Related projects Methods

        public virtual void DeleteRelatedProject(RelatedProject relatedProject)
        {
            if (relatedProject == null)
                throw new ArgumentNullException("relatedProject");

            _relatedProjectRepository.Delete(relatedProject);

            //event notification
            _eventPublisher.EntityDeleted(relatedProject);
        }

        public virtual IList<RelatedProject> GetRelatedProjectsByProjectId1(int projectId1, bool showHidden = false)
        {
            var query = from rp in _relatedProjectRepository.Table
                        join p in _projectRepository.Table on rp.ProjectId2 equals p.Id
                        where rp.ProjectId1 == projectId1 &&
                        !p.Deleted &&
                        (showHidden || p.Published)
                        orderby rp.DisplayOrder
                        select rp;
            var relatedProjects = query.ToList();

            return relatedProjects;
        }

        public virtual RelatedProject GetRelatedProjectById(int relatedProjectId)
        {
            if (relatedProjectId == 0)
                return null;

            var relatedProject = _relatedProjectRepository.GetById(relatedProjectId);
            return relatedProject;
        }

        public virtual void InsertRelatedProject(RelatedProject relatedProject)
        {
            if (relatedProject == null)
                throw new ArgumentNullException("relatedProject");

            _relatedProjectRepository.Insert(relatedProject);

            //event notification
            _eventPublisher.EntityInserted(relatedProject);
        }

        public virtual void UpdateRelatedProject(RelatedProject relatedProject)
        {
            if (relatedProject == null)
                throw new ArgumentNullException("relatedProject");

            _relatedProjectRepository.Update(relatedProject);

            //event notification
            _eventPublisher.EntityUpdated(relatedProject);
        }

        #endregion

        #region Project Instructions Methods

        public virtual void InsertProjectInstruction(ProjectInstruction projectInstruction)
        {
            if (projectInstruction == null)
                throw new ArgumentNullException("projectInstruction");

            _projectInstructionRepository.Insert(projectInstruction);

            //event notification
            _eventPublisher.EntityInserted(projectInstruction);
        }

        public virtual void UpdateProjectInstruction(ProjectInstruction projectInstruction)
        {
            if (projectInstruction == null)
                throw new ArgumentNullException("projectInstruction");

            _projectInstructionRepository.Update(projectInstruction);

            //event notification
            _eventPublisher.EntityUpdated(projectInstruction);
        }

        public virtual void DeleteProjectInstruction(ProjectInstruction projectInstruction)
        {
            if (projectInstruction == null)
                throw new ArgumentNullException("projectInstruction");

            _projectInstructionRepository.Delete(projectInstruction);

            //event notification
            _eventPublisher.EntityDeleted(projectInstruction);
        }

        public virtual IList<ProjectInstruction> GetProjectInstructionsByProjectId(int projectId, bool showHidden = false)
        {
            var query = from pi in _projectInstructionRepository.Table
                        where pi.ProjectId == projectId &&
                        (showHidden || pi.Published)
                        orderby pi.DisplayOrder
                        select pi;
            var projectInstructions = query.ToList();

            return projectInstructions;
        }

        public virtual ProjectInstruction GetProjectInstructionById(int projectInstructionId)
        {
            if (projectInstructionId == 0)
                return null;

            var projectInstruction = _projectInstructionRepository.GetById(projectInstructionId);
            return projectInstruction;
        }

        #endregion

        #region Project Miscellaneous Methods

        public virtual void InsertProjectMisc(ProjectMisc projectMisc)
        {
            if (projectMisc == null)
                throw new ArgumentNullException("projectMisc");

            _projectMiscRepository.Insert(projectMisc);

            //event notification
            _eventPublisher.EntityInserted(projectMisc);
        }

        public virtual void UpdateProjectMisc(ProjectMisc projectMisc)
        {
            if (projectMisc == null)
                throw new ArgumentNullException("projectMisc");

            _projectMiscRepository.Update(projectMisc);

            //event notification
            _eventPublisher.EntityUpdated(projectMisc);
        }

        public virtual void DeleteProjectMisc(ProjectMisc projectMisc)
        {
            if (projectMisc == null)
                throw new ArgumentNullException("projectMisc");

            _projectMiscRepository.Delete(projectMisc);

            //event notification
            _eventPublisher.EntityDeleted(projectMisc);
        }

        public virtual IList<ProjectMisc> GetProjectMiscByProjectId(int projectId, bool showHidden = false)
        {
            var query = from pm in _projectMiscRepository.Table
                        where pm.ProjectId == projectId &&
                        (showHidden || pm.Published)
                        orderby pm.DisplayOrder
                        select pm;
            var projectMiscs = query.ToList();

            return projectMiscs;
        }

        public virtual ProjectMisc GetProjectMiscById(int projectMiscId)
        {
            if (projectMiscId == 0)
                return null;

            var projectMisc = _projectMiscRepository.GetById(projectMiscId);
            return projectMisc;
        }

        #endregion

        #region Recently Viewed Projects Methods

        public virtual IList<Project> GetRecentlyViewedProjects(int number)
        {
            var projects = new List<Project>();
            var projectIds = GetRecentlyViewedProjectsIds(number);
            foreach (var project in GetProjectsByIds(projectIds.ToArray()))
                if (project.Published && !project.Deleted)
                    projects.Add(project);
            return projects;
        }

        public virtual void AddProjectToRecentlyViewedList(int projectId, int maxProjects = 0)
        {
            var oldProjectIds = GetRecentlyViewedProjectsIds();
            var newProjectIds = new List<int>();
            newProjectIds.Add(projectId);
            foreach (int oldProjectId in oldProjectIds)
                if (oldProjectId != projectId)
                    newProjectIds.Add(oldProjectId);

            var recentlyViewedCookie = _httpContext.Request.Cookies.Get("NopCommerce.RecentlyViewedProjects");
            if (recentlyViewedCookie == null)
            {
                recentlyViewedCookie = new HttpCookie("NopCommerce.RecentlyViewedProjects");
                recentlyViewedCookie.HttpOnly = true;
            }
            recentlyViewedCookie.Values.Clear();
            if (maxProjects <= 0)
                maxProjects = 12;
            int i = 1;
            foreach (int newProjectId in newProjectIds)
            {
                recentlyViewedCookie.Values.Add("RecentlyViewedProjectIds", newProjectId.ToString());
                if (i == maxProjects)
                    break;
                i++;
            }
            recentlyViewedCookie.Expires = DateTime.Now.AddDays(10.0);
            _httpContext.Response.Cookies.Set(recentlyViewedCookie);
        }

        protected IList<int> GetRecentlyViewedProjectsIds()
        {
            return GetRecentlyViewedProjectsIds(int.MaxValue);
        }

        protected IList<int> GetRecentlyViewedProjectsIds(int number)
        {
            var projectIds = new List<int>();
            var recentlyViewedCookie = _httpContext.Request.Cookies.Get("NopCommerce.RecentlyViewedProjects");
            if ((recentlyViewedCookie == null) || (recentlyViewedCookie.Values == null))
                return projectIds;
            string[] values = recentlyViewedCookie.Values.GetValues("RecentlyViewedProjectIds");
            if (values == null)
                return projectIds;
            foreach (string projectId in values)
            {
                int prodId = int.Parse(projectId);
                if (!projectIds.Contains(prodId))
                {
                    projectIds.Add(prodId);
                    if (projectIds.Count >= number)
                        break;
                }

            }

            return projectIds;
        }

        #endregion

        #region Video Methods

        public virtual void InsertVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            _videoRepository.Insert(video);

            //event notification
            _eventPublisher.EntityInserted(video);
        }

        public virtual void UpdateVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            _videoRepository.Update(video);

            //event notification
            _eventPublisher.EntityUpdated(video);
        }

        public virtual void DeleteVideo(Video video)
        {
            if (video == null)
                throw new ArgumentNullException("video");

            _videoRepository.Delete(video);

            //event notification
            _eventPublisher.EntityDeleted(video);
        }

        public virtual Video GetVideoById(int videoId)
        {
            if (videoId == 0)
                return null;

            var video = _videoRepository.GetById(videoId);
            return video;
        }

        public virtual IPagedList<Video> GetAllVideos(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _videoRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name));

            var unsortedCategories = query.ToList();

            //paging
            return new PagedList<Video>(unsortedCategories, pageIndex, pageSize);
        }

        #endregion

        #region Project Video Mapping Methods

        public virtual void DeleteProjectVideo(ProjectVideo projectVideo)
        {
            if (projectVideo == null)
                throw new ArgumentNullException("projectVideo");

            _projectVideoRepository.Delete(projectVideo);
        }

        public virtual IPagedList<ProjectVideo> GetProjectVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (videoId == 0)
                return new PagedList<ProjectVideo>(new List<ProjectVideo>(), pageIndex, pageSize);

            var query = from pc in _projectVideoRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.VideoId == videoId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectVideos = new PagedList<ProjectVideo>(query, pageIndex, pageSize);
            return projectVideos;
        }

        public virtual IList<ProjectVideo> GetProjectVideosByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectVideo>();

            var query = from pc in _projectVideoRepository.Table
                        join c in _videoRepository.Table on pc.VideoId equals c.Id
                        where pc.ProjectId == projectId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectVideos = query.ToList();
            return projectVideos;
        }

        public virtual ProjectVideo GetProjectVideoById(int projectVideoId)
        {
            if (projectVideoId == 0)
                return null;

            return _projectVideoRepository.GetById(projectVideoId);
        }

        public virtual void InsertProjectVideo(ProjectVideo projectVideo)
        {
            if (projectVideo == null)
                throw new ArgumentNullException("projectVideo");

            _projectVideoRepository.Insert(projectVideo);
        }

        public virtual void UpdateProjectVideo(ProjectVideo projectVideo)
        {
            if (projectVideo == null)
                throw new ArgumentNullException("projectVideo");

            _projectVideoRepository.Update(projectVideo);
        }

        #endregion

        #region Pattern Methods

        public virtual void InsertPattern(Pattern pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            _patternRepository.Insert(pattern);

            //event notification
            _eventPublisher.EntityInserted(pattern);
        }

        public virtual void UpdatePattern(Pattern pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            _patternRepository.Update(pattern);

            //event notification
            _eventPublisher.EntityUpdated(pattern);
        }

        public virtual void DeletePattern(Pattern pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException("pattern");

            _patternRepository.Delete(pattern);

            //event notification
            _eventPublisher.EntityDeleted(pattern);
        }

        public virtual Pattern GetPatternById(int patternId)
        {
            if (patternId == 0)
                return null;

            var pattern = _patternRepository.GetById(patternId);
            return pattern;
        }

        public virtual IPagedList<Pattern> GetAllPatterns(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _patternRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.Contains(name));

            var unsortedCategories = query.ToList();

            //paging
            return new PagedList<Pattern>(unsortedCategories, pageIndex, pageSize);
        }

        #endregion

        #region Project Pattern Mapping Methods

        public virtual void DeleteProjectPattern(ProjectPattern projectPattern)
        {
            if (projectPattern == null)
                throw new ArgumentNullException("projectPattern");

            _projectPatternRepository.Delete(projectPattern);
        }

        public virtual IPagedList<ProjectPattern> GetProjectPatternsByPatternId(int patternId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (patternId == 0)
                return new PagedList<ProjectPattern>(new List<ProjectPattern>(), pageIndex, pageSize);

            var query = from pc in _projectPatternRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.PatternId == patternId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectPatterns = new PagedList<ProjectPattern>(query, pageIndex, pageSize);
            return projectPatterns;
        }

        public virtual IList<ProjectPattern> GetProjectPatternsByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectPattern>();

            var query = from pc in _projectPatternRepository.Table
                        join c in _patternRepository.Table on pc.PatternId equals c.Id
                        where pc.ProjectId == projectId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectPatterns = query.ToList();
            return projectPatterns;
        }

        public virtual ProjectPattern GetProjectPatternById(int projectPatternId)
        {
            if (projectPatternId == 0)
                return null;

            return _projectPatternRepository.GetById(projectPatternId);
        }

        public virtual void InsertProjectPattern(ProjectPattern projectPattern)
        {
            if (projectPattern == null)
                throw new ArgumentNullException("projectPattern");

            _projectPatternRepository.Insert(projectPattern);
        }

        public virtual void UpdateProjectPattern(ProjectPattern projectPattern)
        {
            if (projectPattern == null)
                throw new ArgumentNullException("projectPattern");

            _projectPatternRepository.Update(projectPattern);
        }

        #endregion

        #region Project Favorite Methods

        public virtual void DeleteProjectFavorite(ProjectFavorite projectFavorite)
        {
            if (projectFavorite == null)
                throw new ArgumentNullException("projectFavorite");

            _projectFavoriteRepository.Delete(projectFavorite);
        }

        public virtual IPagedList<Project> GetProjectFavoritesByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from pf in _projectFavoriteRepository.Table
                        join p in _projectRepository.Table on pf.ProjectId equals p.Id
                        where pf.CustomerId == customerId && p.Published
                        orderby pf.CreatedOn descending
                        select p;

            var projects = new PagedList<Project>(query, 0, 100000);

            //return projects
            return projects;
        }

        public virtual void InsertProjectFavorite(ProjectFavorite projectFavorite)
        {
            if (projectFavorite == null)
                throw new ArgumentNullException("projectFavorite");

            _projectFavoriteRepository.Insert(projectFavorite);
        }

        public virtual ProjectFavorite GetProjectFavoriteByCustomerId(int customerId, int projectId)
        {
            if (projectId == 0 || customerId == 0)
                return null;

            var query = from pf in _projectFavoriteRepository.Table
                        where pf.CustomerId == customerId && pf.ProjectId == projectId
                        select pf;

            return query.FirstOrDefault();
        }

        public virtual int GetProjectFavoritesCountByProjectId(int projectId)
        {
            if (projectId == 0)
                return 0;

            var query = from pf in _projectFavoriteRepository.Table
                        where pf.ProjectId == projectId
                        select pf;

            return query.ToList().Count;
        }

        #endregion

        #region Project Like Methods

        public virtual void DeleteProjectLike(ProjectLike projectLike)
        {
            if (projectLike == null)
                throw new ArgumentNullException("projectLike");

            _projectLikeRepository.Delete(projectLike);
        }

        public virtual IPagedList<Project> GetProjectLikesByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from pf in _projectLikeRepository.Table
                        join p in _projectRepository.Table on pf.ProjectId equals p.Id
                        where pf.CustomerId == customerId && p.Published
                        orderby pf.CreatedOn descending
                        select p;

            var projects = new PagedList<Project>(query, 0, 100000);

            //return projects
            return projects;
        }

        public virtual void InsertProjectLike(ProjectLike projectLike)
        {
            if (projectLike == null)
                throw new ArgumentNullException("projectLike");

            _projectLikeRepository.Insert(projectLike);
        }

        public virtual ProjectLike GetProjectLikeByCustomerId(int customerId, int projectId)
        {
            if (projectId == 0 || customerId == 0)
                return null;

            var query = from pf in _projectLikeRepository.Table
                        where pf.CustomerId == customerId && pf.ProjectId == projectId
                        select pf;

            return query.FirstOrDefault();
        }

        public virtual int GetProjectLikesByProjectId(int projectId)
        {
            if (projectId == 0)
                return 0;

            var query = from pf in _projectLikeRepository.Table
                        where pf.ProjectId == projectId
                        select pf;

            return query.ToList().Count();
        }

        #endregion

        #region Project Customer Mapping Methods

        public virtual void DeleteProjectCustomer(ProjectCustomer projectCustomer)
        {
            if (projectCustomer == null)
                throw new ArgumentNullException("projectCustomer");

            _projectCustomerRepository.Delete(projectCustomer);
        }

        public virtual IPagedList<ProjectCustomer> GetProjectCustomersByCustomerId(int customerId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (customerId == 0)
                return new PagedList<ProjectCustomer>(new List<ProjectCustomer>(), pageIndex, pageSize);

            var query = from pc in _projectCustomerRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.CustomerId == customerId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectCustomers = new PagedList<ProjectCustomer>(query, pageIndex, pageSize);
            return projectCustomers;
        }

        public virtual IList<ProjectCustomer> GetProjectCustomersByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectCustomer>();

            var query = from pc in _projectCustomerRepository.Table
                        join c in _customerRepository.Table on pc.CustomerId equals c.Id
                        where pc.ProjectId == projectId &&
                              (showHidden || c.Active)
                        orderby pc.DisplayOrder
                        select pc;

            var projectCustomers = query.ToList();
            return projectCustomers;
        }

        public virtual ProjectCustomer GetProjectCustomerById(int projectCustomerId)
        {
            if (projectCustomerId == 0)
                return null;

            return _projectCustomerRepository.GetById(projectCustomerId);
        }

        public virtual void InsertProjectCustomer(ProjectCustomer projectCustomer)
        {
            if (projectCustomer == null)
                throw new ArgumentNullException("projectCustomer");

            _projectCustomerRepository.Insert(projectCustomer);
        }

        public virtual void UpdateProjectCustomer(ProjectCustomer projectCustomer)
        {
            if (projectCustomer == null)
                throw new ArgumentNullException("projectCustomer");

            _projectCustomerRepository.Update(projectCustomer);
        }

        #endregion

        #region Follower Methods

        public virtual void InsertFollower(Follower follower)
        {
            if (follower == null)
                throw new ArgumentNullException("follower");

            _followerRepository.Insert(follower);

            //event notification
            _eventPublisher.EntityInserted(follower);
        }

        public virtual void DeleteFollower(Follower follower)
        {
            if (follower == null)
                throw new ArgumentNullException("follower");

            _followerRepository.Delete(follower);

            //event notification
            _eventPublisher.EntityDeleted(follower);
        }

        public virtual Follower GetFollower(int artistId, int followerId)
        {
            if (followerId == 0)
                return null;

            var query = from f in _followerRepository.Table
                        where f.ArtistId == artistId && f.FollowerId == followerId
                        select f;
            return query.FirstOrDefault();
        }

        public virtual List<Follower> GetFollowers(int artistId)
        {
            if (artistId == 0)
                return null;

            var query = from f in _followerRepository.Table
                        where f.ArtistId == artistId
                        select f;
            return query.ToList();
        }

        #endregion

        #region Technique Methods

        public virtual void InsertTechnique(Technique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique");

            _techniqueRepository.Insert(technique);

            //event notification
            _eventPublisher.EntityInserted(technique);
        }

        public virtual void UpdateTechnique(Technique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique");

            _techniqueRepository.Update(technique);

            //event notification
            _eventPublisher.EntityUpdated(technique);
        }

        public virtual void DeleteTechnique(Technique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique");

            _techniqueRepository.Delete(technique);

            //event notification
            _eventPublisher.EntityDeleted(technique);
        }

        public virtual Technique GetTechniqueById(int techniqueId)
        {
            if (techniqueId == 0)
                return null;

            var technique = _techniqueRepository.GetById(techniqueId);
            return technique;
        }

        public virtual IPagedList<Technique> GetAllTechniques(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, int manufacturerId = 0, bool showHidden = false)
        {
            var query = _techniqueRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            if (!String.IsNullOrWhiteSpace(name))
                query = query.Where(c => c.Name.ToLower().Contains(name));

            //manufacturer filtering
            if (manufacturerId > 0)
            {
                query = from t in query
                        from tm in t.TechniqueManufacturers.Where(tm => tm.ManufacturerId == manufacturerId)
                        select t;
            }

            var unsortedCategories = query.ToList();

            //paging
            return new PagedList<Technique>(unsortedCategories, pageIndex, pageSize);
        }

        #endregion

        #region Technique Detail Methods

        public virtual void InsertTechniqueDetail(TechniqueDetail techniqueDetail)
        {
            if (techniqueDetail == null)
                throw new ArgumentNullException("techniqueDetail");

            _techniqueDetailRepository.Insert(techniqueDetail);

            //event notification
            _eventPublisher.EntityInserted(techniqueDetail);
        }

        public virtual void UpdateTechniqueDetail(TechniqueDetail techniqueDetail)
        {
            if (techniqueDetail == null)
                throw new ArgumentNullException("techniqueDetail");

            _techniqueDetailRepository.Update(techniqueDetail);

            //event notification
            _eventPublisher.EntityUpdated(techniqueDetail);
        }

        public virtual void DeleteTechniqueDetail(TechniqueDetail techniqueDetail)
        {
            if (techniqueDetail == null)
                throw new ArgumentNullException("techniqueDetail");

            _techniqueDetailRepository.Delete(techniqueDetail);

            //event notification
            _eventPublisher.EntityDeleted(techniqueDetail);
        }

        public virtual IList<TechniqueDetail> GetTechniqueDetailsByTechniqueId(int techniqueId)
        {
            var query = from td in _techniqueDetailRepository.Table
                        where td.TechniqueId == techniqueId
                        orderby td.DisplayOrder
                        select td;
            var techniqueDetails = query.ToList();

            return techniqueDetails;
        }

        public virtual TechniqueDetail GetTechniqueDetailById(int techniqueDetailId)
        {
            if (techniqueDetailId == 0)
                return null;

            var techniqueDetail = _techniqueDetailRepository.GetById(techniqueDetailId);
            return techniqueDetail;
        }

        #endregion

        #region Project Technique Mapping Methods

        public virtual void DeleteProjectTechnique(ProjectTechnique projectTechnique)
        {
            if (projectTechnique == null)
                throw new ArgumentNullException("projectTechnique");

            _projectTechniqueRepository.Delete(projectTechnique);
        }

        public virtual IPagedList<ProjectTechnique> GetProjectTechniquesByTechniqueId(int techniqueId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (techniqueId == 0)
                return new PagedList<ProjectTechnique>(new List<ProjectTechnique>(), pageIndex, pageSize);

            var query = from pc in _projectTechniqueRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.TechniqueId == techniqueId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectTechniques = new PagedList<ProjectTechnique>(query, pageIndex, pageSize);
            return projectTechniques;
        }

        public virtual IList<ProjectTechnique> GetProjectTechniquesByProjectId(int projectId, bool showHidden = false)
        {
            if (projectId == 0)
                return new List<ProjectTechnique>();

            var query = from pc in _projectTechniqueRepository.Table
                        join c in _techniqueRepository.Table on pc.TechniqueId equals c.Id
                        where pc.ProjectId == projectId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var projectTechniques = query.ToList();
            return projectTechniques;
        }

        public virtual ProjectTechnique GetProjectTechniqueById(int projectTechniqueId)
        {
            if (projectTechniqueId == 0)
                return null;

            return _projectTechniqueRepository.GetById(projectTechniqueId);
        }

        public virtual void InsertProjectTechnique(ProjectTechnique projectTechnique)
        {
            if (projectTechnique == null)
                throw new ArgumentNullException("projectTechnique");

            _projectTechniqueRepository.Insert(projectTechnique);
        }

        public virtual void UpdateProjectTechnique(ProjectTechnique projectTechnique)
        {
            if (projectTechnique == null)
                throw new ArgumentNullException("projectTechnique");

            _projectTechniqueRepository.Update(projectTechnique);
        }

        #endregion

        #region User Gallery Methods

        public virtual void InsertUserGallery(UserGallery userGallery)
        {
            if (userGallery == null)
                throw new ArgumentNullException("userGallery");

            _userGalleryRepository.Insert(userGallery);

            //event notification
            _eventPublisher.EntityInserted(userGallery);
        }

        public virtual void UpdateUserGallery(UserGallery userGallery)
        {
            if (userGallery == null)
                throw new ArgumentNullException("userGallery");

            _userGalleryRepository.Update(userGallery);

            //event notification
            _eventPublisher.EntityUpdated(userGallery);
        }

        public virtual void DeleteUserGallery(UserGallery userGallery)
        {
            if (userGallery == null)
                throw new ArgumentNullException("userGallery");

            _userGalleryRepository.Delete(userGallery);

            //event notification
            _eventPublisher.EntityDeleted(userGallery);
        }

        public virtual UserGallery GetUserGalleryById(int userGalleryId)
        {
            if (userGalleryId == 0)
                return null;

            var userGallery = _userGalleryRepository.GetById(userGalleryId);
            return userGallery;
        }

        public virtual IPagedList<UserGallery> GetAllUserGalleries(string GalleryName = "",int pageIndex = 0, int pageSize = 0, bool showHidden = false)
        {
            //projects
            var query = _userGalleryRepository.Table;
            query = query.Where(p => !p.Deleted);
            if (!showHidden)
                query = query.Where(p => p.Published);
                query = query.Where(p => p.IsApproved);
            if (!String.IsNullOrEmpty(GalleryName))
                query = query.Where(p => p.Name.Contains(GalleryName));

            //sort projects
            query = query.OrderBy(p => p.Name);

            var userGalleries = new PagedList<UserGallery>(query, pageIndex, pageSize);

            //return projects
            return userGalleries;
        }

        public virtual IPagedList<UserGallery> GetUserGalleriesByCustomerId(int customerId, int pageIndex, int pageSize)
        {
            //projects
            var query = _userGalleryRepository.Table;
            query = query.Where(p => !p.Deleted);
            query = query.Where(p => p.Published);
            query = query.Where(p => p.CustomerId == customerId);

            //sort projects
            query = query.OrderBy(p => p.Name);

            var userGalleries = new PagedList<UserGallery>(query, pageIndex, pageSize);

            //return projects
            return userGalleries;
        }

        #endregion

        #region Project Tag Methods

        public virtual void DeleteProjectTag(ProjectTag projectTag)
        {
            if (projectTag == null)
                throw new ArgumentNullException("projectTag");

            _projectTagRepository.Delete(projectTag);

            //event notification
            _eventPublisher.EntityDeleted(projectTag);
        }

        public virtual IList<ProjectTag> GetAllProjectTags(bool showHidden = false)
        {
            var query = _projectTagRepository.Table;
            if (!showHidden)
                query = query.Where(pt => pt.ProjectCount > 0);
            query = query.OrderByDescending(pt => pt.ProjectCount);
            var projectTags = query.ToList();
            return projectTags;
        }

        public virtual ProjectTag GetProjectTagById(int projectTagId)
        {
            if (projectTagId == 0)
                return null;

            var projectTag = _projectTagRepository.GetById(projectTagId);
            return projectTag;
        }

        public virtual ProjectTag GetProjectTagByName(string name)
        {
            var query = from pt in _projectTagRepository.Table
                        where pt.Name == name
                        select pt;

            var projectTag = query.FirstOrDefault();
            return projectTag;
        }

        public virtual void InsertProjectTag(ProjectTag projectTag)
        {
            if (projectTag == null)
                throw new ArgumentNullException("projectTag");

            _projectTagRepository.Insert(projectTag);

            //event notification
            _eventPublisher.EntityInserted(projectTag);
        }

        public virtual void UpdateProjectTag(ProjectTag projectTag)
        {
            if (projectTag == null)
                throw new ArgumentNullException("projectTag");

            _projectTagRepository.Update(projectTag);

            //event notification
            _eventPublisher.EntityUpdated(projectTag);
        }

        public virtual void UpdateProjectTagTotals(ProjectTag projectTag)
        {
            if (projectTag == null)
                throw new ArgumentNullException("projectTag");

            int newTotal = projectTag.Projects
                .Where(p => !p.Deleted && p.Published)
                .Count();

            //we do not delete product tasg with 0 product count
            projectTag.ProjectCount = newTotal;
            UpdateProjectTag(projectTag);

        }

        #endregion

        #region Project Material Methods

        public virtual void InsertProjectMaterial(ProjectMaterial projectMaterial)
        {
            if (projectMaterial == null)
                throw new ArgumentNullException("projectMaterial");

            _projectMaterialRepository.Insert(projectMaterial);

            //event notification
            _eventPublisher.EntityInserted(projectMaterial);
        }

        public virtual void UpdateProjectMaterial(ProjectMaterial projectMaterial)
        {
            if (projectMaterial == null)
                throw new ArgumentNullException("projectMaterial");

            _projectMaterialRepository.Update(projectMaterial);

            //event notification
            _eventPublisher.EntityUpdated(projectMaterial);
        }

        public virtual void DeleteProjectMaterial(ProjectMaterial projectMaterial)
        {
            if (projectMaterial == null)
                throw new ArgumentNullException("projectMaterial");

            _projectMaterialRepository.Delete(projectMaterial);

            //event notification
            _eventPublisher.EntityDeleted(projectMaterial);
        }

        public virtual IList<ProjectMaterial> GetProjectMaterialByProjectId(int projectId)
        {
            var query = from pm in _projectMaterialRepository.Table
                        where pm.ProjectId == projectId
                        orderby pm.CategoryId, pm.DisplayOrder
                        select pm;
            var projectMaterials = query.ToList();

            return projectMaterials;
        }


        public virtual ProjectMaterial GetProjectMaterialById(int projectMaterialId)
        {
            if (projectMaterialId == 0)
                return null;

            var projectMaterial = _projectMaterialRepository.GetById(projectMaterialId);
            return projectMaterial;
        }

        #endregion

        #region Workshop Methods

        public virtual void DeleteWorkshop(Workshop workshop)
        {
            if (workshop == null)
                throw new ArgumentNullException("workshop");

            workshop.Deleted = true;
            UpdateWorkshop(workshop);
        }

        public virtual void UpdateWorkshop(Workshop workshop)
        {
            if (workshop == null)
                throw new ArgumentNullException("workshop");

            _workshopRepository.Update(workshop);

            //event notification
            _eventPublisher.EntityUpdated(workshop);
        }

        public virtual void InsertWorkshop(Workshop workshop)
        {
            if (workshop == null)
                throw new ArgumentNullException("workshop");

            _workshopRepository.Insert(workshop);

            //event notification
            _eventPublisher.EntityInserted(workshop);
        }

        public virtual Workshop GetWorkshopById(int workshopId)
        {
            if (workshopId == 0)
                return null;

            var workshop = _workshopRepository.GetById(workshopId);
            return workshop;
        }

        public virtual IPagedList<Workshop> GetAllWorkshops(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _workshopRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            query = query.OrderByDescending(c => c.CreatedOnUtc).ThenByDescending(c => c.Id);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Title.ToLower().Contains(name));

            //paging
            return new PagedList<Workshop>(query.ToList(), pageIndex, pageSize);
        }

        #endregion

        #region Document Center Methods

        public virtual void DeleteDocumentCenter(DocumentCenter documentCenter)
        {
            if (documentCenter == null)
                throw new ArgumentNullException("documentCenter");

            documentCenter.Deleted = true;
            UpdateDocumentCenter(documentCenter);
        }

        public virtual void UpdateDocumentCenter(DocumentCenter documentCenter)
        {
            if (documentCenter == null)
                throw new ArgumentNullException("documentCenter");

            _documentCenterRepository.Update(documentCenter);

            //event notification
            _eventPublisher.EntityUpdated(documentCenter);
        }

        public virtual void InsertDocumentCenter(DocumentCenter documentCenter)
        {
            if (documentCenter == null)
                throw new ArgumentNullException("documentCenter");

            _documentCenterRepository.Insert(documentCenter);

            //event notification
            _eventPublisher.EntityInserted(documentCenter);
        }

        public virtual DocumentCenter GetDocumentCenterById(int documentCenterId)
        {
            if (documentCenterId == 0)
                return null;

            var documentCenter = _documentCenterRepository.GetById(documentCenterId);
            return documentCenter;
        }

        public virtual IPagedList<DocumentCenter> GetAllDocumentCenters(string DocumentName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _documentCenterRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => !c.Deleted);
            if (!String.IsNullOrEmpty(DocumentName))
                query = query.Where(c => c.Name.Contains(DocumentName));
            query = query.OrderByDescending(c => c.CreatedOnUtc).ThenByDescending(c => c.Id);

            //paging
            return new PagedList<DocumentCenter>(query.ToList(), pageIndex, pageSize);
        }

        #endregion

        #region Business Builder Product Methods

        public virtual void DeleteBusinessBuilderProduct(BusinessBuilderProduct businessBuilderProduct)
        {
            if (businessBuilderProduct == null)
                throw new ArgumentNullException("businessBuilderProduct");

            businessBuilderProduct.Deleted = true;
            //delete businessBuilderProduct
            UpdateBusinessBuilderProduct(businessBuilderProduct);
        }

        public virtual IList<BusinessBuilderProduct> GetAllBusinessBuilderProducts(bool showHidden = false)
        {
            var query = from p in _businessBuilderProductRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            var businessBuilderProducts = query.ToList();
            return businessBuilderProducts;
        }

        public virtual IPagedList<BusinessBuilderProduct> GetAllBusinessBuilderProducts(string BuilderProductName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = from p in _businessBuilderProductRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            if (!String.IsNullOrEmpty(BuilderProductName))
                query = query.Where(p => p.Name.Contains(BuilderProductName));

            var businessBuilderProducts = query.ToList();

            //paging
            return new PagedList<BusinessBuilderProduct>(businessBuilderProducts, pageIndex, pageSize);
        }

        public virtual IPagedList<BusinessBuilderProduct> GetAllBusinessBuilderProducts(int categoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _businessBuilderProductRepository.Table;
            query = query.Where(p => !p.Deleted);
            query = query.Where(p => p.Published);

            //category filtering
            if (categoryId > 0)
            {
                //search in subcategories
                query = from p in query
                        from pc in p.BusinessBuilderProductCategories.Where(pc => categoryId == pc.BusinessBuilderCategoryId)
                        select p;
            }

            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();


            //paging
            return new PagedList<BusinessBuilderProduct>(query.ToList(), pageIndex, pageSize);
        }

        public virtual BusinessBuilderProduct GetBusinessBuilderProductById(int businessBuilderProductId)
        {
            if (businessBuilderProductId == 0)
                return null;

            var businessBuilderProduct = _businessBuilderProductRepository.GetById(businessBuilderProductId);
            return businessBuilderProduct;
        }

        public virtual IList<BusinessBuilderProduct> GetBusinessBuilderProductsByIds(int[] businessBuilderProductIds)
        {
            if (businessBuilderProductIds == null || businessBuilderProductIds.Length == 0)
                return new List<BusinessBuilderProduct>();

            var query = from p in _businessBuilderProductRepository.Table
                        where businessBuilderProductIds.Contains(p.Id)
                        select p;
            var businessBuilderProducts = query.ToList();
            //sort by passed identifiers
            var sortedProducts = new List<BusinessBuilderProduct>();
            foreach (int id in businessBuilderProductIds)
            {
                var businessBuilderProduct = businessBuilderProducts.Find(x => x.Id == id);
                if (businessBuilderProduct != null)
                    sortedProducts.Add(businessBuilderProduct);
            }
            return sortedProducts;
        }

        public virtual void InsertBusinessBuilderProduct(BusinessBuilderProduct businessBuilderProduct)
        {
            if (businessBuilderProduct == null)
                throw new ArgumentNullException("businessBuilderProduct");

            //insert
            _businessBuilderProductRepository.Insert(businessBuilderProduct);

            //event notification
            _eventPublisher.EntityInserted(businessBuilderProduct);
        }

        public virtual void UpdateBusinessBuilderProduct(BusinessBuilderProduct businessBuilderProduct)
        {
            if (businessBuilderProduct == null)
                throw new ArgumentNullException("businessBuilderProduct");

            //update
            _businessBuilderProductRepository.Update(businessBuilderProduct);

            //event notification
            _eventPublisher.EntityUpdated(businessBuilderProduct);
        }

        #endregion

        #region Business Builder Category Methods

        public virtual void DeleteBusinessBuilderCategory(BusinessBuilderCategory businessBuilderCategory)
        {
            if (businessBuilderCategory == null)
                throw new ArgumentNullException("businessBuilderCategory");

            businessBuilderCategory.Deleted = true;
            UpdateBusinessBuilderCategory(businessBuilderCategory);
        }

        public virtual IPagedList<BusinessBuilderCategory> GetAllBusinessBuilderCategories(string BuilderCatName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _businessBuilderCategoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);

            query = query.Where(c => !c.Deleted);

            if (!String.IsNullOrEmpty(BuilderCatName))
                query = query.Where(c => c.Name.Contains(BuilderCatName));
            var sortedCategories = query.ToList();

            //paging
            return new PagedList<BusinessBuilderCategory>(sortedCategories, pageIndex, pageSize);
        }

        public virtual BusinessBuilderCategory GetBusinessBuilderCategoryById(int businessBuilderCategoryId)
        {
            if (businessBuilderCategoryId == 0)
                return null;

            var businessBuilderCategory = _businessBuilderCategoryRepository.GetById(businessBuilderCategoryId);
            return businessBuilderCategory;
        }

        public virtual void InsertBusinessBuilderCategory(BusinessBuilderCategory businessBuilderCategory)
        {
            if (businessBuilderCategory == null)
                throw new ArgumentNullException("businessBuilderCategory");

            _businessBuilderCategoryRepository.Insert(businessBuilderCategory);

            //event notification
            _eventPublisher.EntityInserted(businessBuilderCategory);
        }

        public virtual void UpdateBusinessBuilderCategory(BusinessBuilderCategory businessBuilderCategory)
        {
            if (businessBuilderCategory == null)
                throw new ArgumentNullException("businessBuilderCategory");

            _businessBuilderCategoryRepository.Update(businessBuilderCategory);

            //event notification
            _eventPublisher.EntityUpdated(businessBuilderCategory);
        }

        #endregion

        #region Business Builder Product Category Methods

        public virtual void DeleteBusinessBuilderProductCategory(BusinessBuilderProductCategory businessBuilderProductCategory)
        {
            if (businessBuilderProductCategory == null)
                throw new ArgumentNullException("businessBuilderProductCategory");

            _businessBuilderProductCategoryRepository.Delete(businessBuilderProductCategory);

            //event notification
            _eventPublisher.EntityDeleted(businessBuilderProductCategory);
        }

        public virtual IPagedList<BusinessBuilderProductCategory> GetBusinessBuilderProductCategoriesByCategoryId(int businessBuilderCategoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (businessBuilderCategoryId == 0)
                return new PagedList<BusinessBuilderProductCategory>(new List<BusinessBuilderProductCategory>(), pageIndex, pageSize);

            var query = from pc in _businessBuilderProductCategoryRepository.Table
                        join p in _businessBuilderProductRepository.Table on pc.BusinessBuilderProductId equals p.Id
                        where pc.BusinessBuilderCategoryId == businessBuilderCategoryId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var businessBuilderProductCategories = new PagedList<BusinessBuilderProductCategory>(query, pageIndex, pageSize);
            return businessBuilderProductCategories;
        }

        public virtual IList<BusinessBuilderProductCategory> GetBusinessBuilderProductCategoriesByProductId(int businessBuilderProductId, bool showHidden = false)
        {
            if (businessBuilderProductId == 0)
                return new List<BusinessBuilderProductCategory>();

            var query = from pc in _businessBuilderProductCategoryRepository.Table
                        join c in _businessBuilderCategoryRepository.Table on pc.BusinessBuilderCategoryId equals c.Id
                        where pc.BusinessBuilderProductId == businessBuilderProductId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var businessBuilderProductCategories = query.ToList();
            return businessBuilderProductCategories;
        }

        public virtual BusinessBuilderProductCategory GetBusinessBuilderProductCategoryById(int businessBuilderProductCategoryId)
        {
            if (businessBuilderProductCategoryId == 0)
                return null;

            return _businessBuilderProductCategoryRepository.GetById(businessBuilderProductCategoryId);
        }

        public virtual void InsertBusinessBuilderProductCategory(BusinessBuilderProductCategory businessBuilderProductCategory)
        {
            if (businessBuilderProductCategory == null)
                throw new ArgumentNullException("businessBuilderProductCategory");

            _businessBuilderProductCategoryRepository.Insert(businessBuilderProductCategory);

            //event notification
            _eventPublisher.EntityInserted(businessBuilderProductCategory);
        }

        public virtual void UpdateBusinessBuilderProductCategory(BusinessBuilderProductCategory businessBuilderProductCategory)
        {
            if (businessBuilderProductCategory == null)
                throw new ArgumentNullException("businessBuilderProductCategory");

            _businessBuilderProductCategoryRepository.Update(businessBuilderProductCategory);

            //event notification
            _eventPublisher.EntityUpdated(businessBuilderProductCategory);
        }

        #endregion

        #region Ambassador Links Product Methods

        public virtual void DeleteAmbassadorLinksProduct(AmbassadorLinksProduct ambassadorLinksProduct)
        {
            if (ambassadorLinksProduct == null)
                throw new ArgumentNullException("ambassadorLinksProduct");

            ambassadorLinksProduct.Deleted = true;
            //delete ambassadorLinksProduct
            UpdateAmbassadorLinksProduct(ambassadorLinksProduct);
        }

        public virtual IList<AmbassadorLinksProduct> GetAllAmbassadorLinksProducts(bool showHidden = false)
        {
            var query = from p in _ambassadorLinksProductRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            var ambassadorLinksProducts = query.ToList();
            return ambassadorLinksProducts;
        }

        public virtual IPagedList<AmbassadorLinksProduct> GetAllAmbassadorLinksProducts(string AmbassadorLinkProductName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = from p in _ambassadorLinksProductRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            if (!String.IsNullOrEmpty(AmbassadorLinkProductName))
                query = query.Where(p => p.Name.Contains(AmbassadorLinkProductName));

            var ambassadorLinksProducts = query.ToList();

            //paging
            return new PagedList<AmbassadorLinksProduct>(ambassadorLinksProducts, pageIndex, pageSize);
        }

        public virtual IPagedList<AmbassadorLinksProduct> GetAllAmbassadorLinksProducts(int categoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _ambassadorLinksProductRepository.Table;
            query = query.Where(p => !p.Deleted);
            query = query.Where(p => p.Published);

            //category filtering
            if (categoryId > 0)
            {
                //search in subcategories
                query = from p in query
                        from pc in p.AmbassadorLinksProductCategories.Where(pc => categoryId == pc.AmbassadorLinksCategoryId)
                        select p;
            }

            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();


            //paging
            return new PagedList<AmbassadorLinksProduct>(query.ToList(), pageIndex, pageSize);
        }

        public virtual AmbassadorLinksProduct GetAmbassadorLinksProductById(int ambassadorLinksProductId)
        {
            if (ambassadorLinksProductId == 0)
                return null;

            var ambassadorLinksProduct = _ambassadorLinksProductRepository.GetById(ambassadorLinksProductId);
            return ambassadorLinksProduct;
        }

        public virtual IList<AmbassadorLinksProduct> GetAmbassadorLinksProductsByIds(int[] ambassadorLinksProductIds)
        {
            if (ambassadorLinksProductIds == null || ambassadorLinksProductIds.Length == 0)
                return new List<AmbassadorLinksProduct>();

            var query = from p in _ambassadorLinksProductRepository.Table
                        where ambassadorLinksProductIds.Contains(p.Id)
                        select p;
            var ambassadorLinksProducts = query.ToList();
            //sort by passed identifiers
            var sortedProducts = new List<AmbassadorLinksProduct>();
            foreach (int id in ambassadorLinksProductIds)
            {
                var ambassadorLinksProduct = ambassadorLinksProducts.Find(x => x.Id == id);
                if (ambassadorLinksProduct != null)
                    sortedProducts.Add(ambassadorLinksProduct);
            }
            return sortedProducts;
        }

        public virtual void InsertAmbassadorLinksProduct(AmbassadorLinksProduct ambassadorLinksProduct)
        {
            if (ambassadorLinksProduct == null)
                throw new ArgumentNullException("ambassadorLinksProduct");

            //insert
            _ambassadorLinksProductRepository.Insert(ambassadorLinksProduct);

            //event notification
            _eventPublisher.EntityInserted(ambassadorLinksProduct);
        }

        public virtual void UpdateAmbassadorLinksProduct(AmbassadorLinksProduct ambassadorLinksProduct)
        {
            if (ambassadorLinksProduct == null)
                throw new ArgumentNullException("ambassadorLinksProduct");

            //update
            _ambassadorLinksProductRepository.Update(ambassadorLinksProduct);

            //event notification
            _eventPublisher.EntityUpdated(ambassadorLinksProduct);
        }

        #endregion

        #region Ambassador Links Category Methods

        public virtual void DeleteAmbassadorLinksCategory(AmbassadorLinksCategory ambassadorLinksCategory)
        {
            if (ambassadorLinksCategory == null)
                throw new ArgumentNullException("ambassadorLinksCategory");

            ambassadorLinksCategory.Deleted = true;
            UpdateAmbassadorLinksCategory(ambassadorLinksCategory);
        }

        public virtual IPagedList<AmbassadorLinksCategory> GetAllAmbassadorLinksCategories(string AmbassadorLinkCatName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _ambassadorLinksCategoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);

            query = query.Where(c => !c.Deleted);

            if (!String.IsNullOrEmpty(AmbassadorLinkCatName))
                query = query.Where(c => c.Name.Contains(AmbassadorLinkCatName));

            var sortedCategories = query.ToList();

            //paging
            return new PagedList<AmbassadorLinksCategory>(sortedCategories, pageIndex, pageSize);
        }

        public virtual AmbassadorLinksCategory GetAmbassadorLinksCategoryById(int ambassadorLinksCategoryId)
        {
            if (ambassadorLinksCategoryId == 0)
                return null;

            var ambassadorLinksCategory = _ambassadorLinksCategoryRepository.GetById(ambassadorLinksCategoryId);
            return ambassadorLinksCategory;
        }

        public virtual void InsertAmbassadorLinksCategory(AmbassadorLinksCategory ambassadorLinksCategory)
        {
            if (ambassadorLinksCategory == null)
                throw new ArgumentNullException("ambassadorLinksCategory");

            _ambassadorLinksCategoryRepository.Insert(ambassadorLinksCategory);

            //event notification
            _eventPublisher.EntityInserted(ambassadorLinksCategory);
        }

        public virtual void UpdateAmbassadorLinksCategory(AmbassadorLinksCategory ambassadorLinksCategory)
        {
            if (ambassadorLinksCategory == null)
                throw new ArgumentNullException("ambassadorLinksCategory");

            _ambassadorLinksCategoryRepository.Update(ambassadorLinksCategory);

            //event notification
            _eventPublisher.EntityUpdated(ambassadorLinksCategory);
        }

        #endregion

        #region Ambassador Links Product Category Methods

        public virtual void DeleteAmbassadorLinksProductCategory(AmbassadorLinksProductCategory ambassadorLinksProductCategory)
        {
            if (ambassadorLinksProductCategory == null)
                throw new ArgumentNullException("ambassadorLinksProductCategory");

            _ambassadorLinksProductCategoryRepository.Delete(ambassadorLinksProductCategory);

            //event notification
            _eventPublisher.EntityDeleted(ambassadorLinksProductCategory);
        }

        public virtual IPagedList<AmbassadorLinksProductCategory> GetAmbassadorLinksProductCategoriesByCategoryId(int businessBuilderCategoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (businessBuilderCategoryId == 0)
                return new PagedList<AmbassadorLinksProductCategory>(new List<AmbassadorLinksProductCategory>(), pageIndex, pageSize);

            var query = from pc in _ambassadorLinksProductCategoryRepository.Table
                        join p in _businessBuilderProductRepository.Table on pc.AmbassadorLinksProductId equals p.Id
                        where pc.AmbassadorLinksCategoryId == businessBuilderCategoryId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var ambassadorLinksProductCategories = new PagedList<AmbassadorLinksProductCategory>(query, pageIndex, pageSize);
            return ambassadorLinksProductCategories;
        }

        public virtual IList<AmbassadorLinksProductCategory> GetAmbassadorLinksProductCategoriesByProductId(int businessBuilderProductId, bool showHidden = false)
        {
            if (businessBuilderProductId == 0)
                return new List<AmbassadorLinksProductCategory>();

            var query = from pc in _ambassadorLinksProductCategoryRepository.Table
                        join c in _businessBuilderCategoryRepository.Table on pc.AmbassadorLinksCategoryId equals c.Id
                        where pc.AmbassadorLinksProductId == businessBuilderProductId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var ambassadorLinksProductCategories = query.ToList();
            return ambassadorLinksProductCategories;
        }

        public virtual AmbassadorLinksProductCategory GetAmbassadorLinksProductCategoryById(int ambassadorLinksProductCategoryId)
        {
            if (ambassadorLinksProductCategoryId == 0)
                return null;

            return _ambassadorLinksProductCategoryRepository.GetById(ambassadorLinksProductCategoryId);
        }

        public virtual void InsertAmbassadorLinksProductCategory(AmbassadorLinksProductCategory ambassadorLinksProductCategory)
        {
            if (ambassadorLinksProductCategory == null)
                throw new ArgumentNullException("ambassadorLinksProductCategory");

            _ambassadorLinksProductCategoryRepository.Insert(ambassadorLinksProductCategory);

            //event notification
            _eventPublisher.EntityInserted(ambassadorLinksProductCategory);
        }

        public virtual void UpdateAmbassadorLinksProductCategory(AmbassadorLinksProductCategory ambassadorLinksProductCategory)
        {
            if (ambassadorLinksProductCategory == null)
                throw new ArgumentNullException("ambassadorLinksProductCategory");

            _ambassadorLinksProductCategoryRepository.Update(ambassadorLinksProductCategory);

            //event notification
            _eventPublisher.EntityUpdated(ambassadorLinksProductCategory);
        }

        #endregion

        #region Gallery Category Methods

        public virtual void DeleteGalleryCategory(GalleryCategory galleryCategory)
        {
            if (galleryCategory == null)
                throw new ArgumentNullException("galleryCategory");

            galleryCategory.Deleted = true;
            UpdateGalleryCategory(galleryCategory);
        }

        public virtual IPagedList<GalleryCategory> GetAllGalleryCategories(string GalleryCatName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _galleryCategoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);

            query = query.Where(c => !c.Deleted);

            if (!String.IsNullOrEmpty(GalleryCatName))
                query = query.Where(c => c.Name.Contains(GalleryCatName));

            query = query.OrderBy(c => c.ParentId).ThenBy(c => c.Name);

            //ACL (access control list)
            if (!showHidden)
            {
                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.ParentId).ThenBy(c => c.Name);
            }

            var sortedCategories = query.ToList();

            //paging
            return new PagedList<GalleryCategory>(sortedCategories, pageIndex, pageSize);
        }

        public IList<GalleryCategory> GetAllGalleryCategoriesByParentId(int parentId, bool showHidden = false)
        {
            var query = _galleryCategoryRepository.Table;
            if (!showHidden)
                query = query.Where(c => c.Published);
            query = query.Where(c => c.ParentId == parentId);
            query = query.Where(c => !c.Deleted);

            query = query.OrderBy(c => c.Name);

            //ACL (access control list)
            if (!showHidden)
            {
                //only distinct categories (group by ID)
                query = from c in query
                        group c by c.Id
                            into cGroup
                            orderby cGroup.Key
                            select cGroup.FirstOrDefault();
                query = query.OrderBy(c => c.Name);
            }

            var categories = query.ToList();
            return categories;
        }

        public virtual GalleryCategory GetGalleryCategoryById(int galleryCategoryId)
        {
            if (galleryCategoryId == 0)
                return null;

            var galleryCategory = _galleryCategoryRepository.GetById(galleryCategoryId);
            return galleryCategory;
        }

        public virtual void InsertGalleryCategory(GalleryCategory galleryCategory)
        {
            if (galleryCategory == null)
                throw new ArgumentNullException("galleryCategory");

            _galleryCategoryRepository.Insert(galleryCategory);

            //event notification
            _eventPublisher.EntityInserted(galleryCategory);
        }

        public virtual void UpdateGalleryCategory(GalleryCategory galleryCategory)
        {
            if (galleryCategory == null)
                throw new ArgumentNullException("category");

            //validate category hierarchy
            var parentCategory = GetGalleryCategoryById(galleryCategory.ParentId);
            while (parentCategory != null)
            {
                if (galleryCategory.Id == parentCategory.Id)
                {
                    galleryCategory.ParentId = 0;
                    break;
                }
                parentCategory = GetGalleryCategoryById(parentCategory.ParentId);
            }

            _galleryCategoryRepository.Update(galleryCategory);

            //event notification
            _eventPublisher.EntityUpdated(galleryCategory);
        }

        public virtual void DeleteGalleryProductCategory(GalleryProductCategory galleryProductCategory)
        {
            if (galleryProductCategory == null)
                throw new ArgumentNullException("galleryProductCategory");

            _galleryProductCategoryRepository.Delete(galleryProductCategory);

            //event notification
            _eventPublisher.EntityDeleted(galleryProductCategory);
        }

        public virtual IPagedList<GalleryProductCategory> GetGalleryProductCategoriesByCategoryId(int galleryCategoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (galleryCategoryId == 0)
                return new PagedList<GalleryProductCategory>(new List<GalleryProductCategory>(), pageIndex, pageSize);

            var query = from pc in _galleryProductCategoryRepository.Table
                        join p in _galleryProductRepository.Table on pc.GalleryProductId equals p.Id
                        where pc.GalleryCategoryId == galleryCategoryId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            //only distinct categories (group by ID)
            query = from c in query
                    group c by c.Id
                        into cGroup
                        orderby cGroup.Key
                        select cGroup.FirstOrDefault();
            query = query.OrderBy(pc => pc.DisplayOrder);

            var galleryProductCategories = new PagedList<GalleryProductCategory>(query, pageIndex, pageSize);
            return galleryProductCategories;
        }

        public virtual IList<GalleryProductCategory> GetGalleryProductCategoriesByProductId(int galleryProductId, bool showHidden = false)
        {
            if (galleryProductId == 0)
                return new List<GalleryProductCategory>();

            var query = from pc in _galleryProductCategoryRepository.Table
                        join c in _galleryCategoryRepository.Table on pc.GalleryCategoryId equals c.Id
                        where pc.GalleryProductId == galleryProductId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            //only distinct categories (group by ID)
            query = from pc in query
                    group pc by pc.Id
                        into pcGroup
                        orderby pcGroup.Key
                        select pcGroup.FirstOrDefault();
            query = query.OrderBy(pc => pc.DisplayOrder);

            var galleryProductCategories = query.ToList();
            return galleryProductCategories;
        }

        public virtual GalleryProductCategory GetGalleryProductCategoryById(int galleryProductCategoryId)
        {
            if (galleryProductCategoryId == 0)
                return null;

            return _galleryProductCategoryRepository.GetById(galleryProductCategoryId);
        }

        public virtual void InsertGalleryProductCategory(GalleryProductCategory galleryProductCategory)
        {
            if (galleryProductCategory == null)
                throw new ArgumentNullException("galleryProductCategory");

            _galleryProductCategoryRepository.Insert(galleryProductCategory);

            //event notification
            _eventPublisher.EntityInserted(galleryProductCategory);
        }

        public virtual void UpdateGalleryProductCategory(GalleryProductCategory galleryProductCategory)
        {
            if (galleryProductCategory == null)
                throw new ArgumentNullException("galleryProductCategory");

            _galleryProductCategoryRepository.Update(galleryProductCategory);

            //event notification
            _eventPublisher.EntityUpdated(galleryProductCategory);
        }

        #endregion

        #region Gallery Product Methods

        public virtual void DeleteGalleryProduct(GalleryProduct galleryProduct)
        {
            if (galleryProduct == null)
                throw new ArgumentNullException("galleryProduct");

            galleryProduct.Deleted = true;
            //delete galleryProduct
            UpdateGalleryProduct(galleryProduct);
        }

        public virtual IList<GalleryProduct> GetAllGalleryProducts(bool showHidden = false)
        {
            var query = from p in _galleryProductRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            var galleryProducts = query.ToList();
            return galleryProducts;
        }

        public virtual IPagedList<GalleryProduct> GetAllGalleryProducts(string GalleryProductName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = from p in _galleryProductRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;

            if (!String.IsNullOrEmpty(GalleryProductName))
                query = query.Where(c => c.Name.Contains(GalleryProductName));

            var galleryProducts = query.ToList();

            //paging
            return new PagedList<GalleryProduct>(galleryProducts, pageIndex, pageSize);
        }

        public virtual GalleryProduct GetGalleryProductById(int galleryProductId)
        {
            if (galleryProductId == 0)
                return null;

            var galleryProduct = _galleryProductRepository.GetById(galleryProductId);
            return galleryProduct;
        }

        public virtual IList<GalleryProduct> GetGalleryProductsByIds(int[] galleryProductIds)
        {
            if (galleryProductIds == null || galleryProductIds.Length == 0)
                return new List<GalleryProduct>();

            var query = from p in _galleryProductRepository.Table
                        where galleryProductIds.Contains(p.Id)
                        select p;
            var galleryProducts = query.ToList();
            //sort by passed identifiers
            var sortedProducts = new List<GalleryProduct>();
            foreach (int id in galleryProductIds)
            {
                var galleryProduct = galleryProducts.Find(x => x.Id == id);
                if (galleryProduct != null)
                    sortedProducts.Add(galleryProduct);
            }
            return sortedProducts;
        }

        public virtual void InsertGalleryProduct(GalleryProduct galleryProduct)
        {
            if (galleryProduct == null)
                throw new ArgumentNullException("galleryProduct");

            //insert
            _galleryProductRepository.Insert(galleryProduct);

            //event notification
            _eventPublisher.EntityInserted(galleryProduct);
        }

        public virtual void UpdateGalleryProduct(GalleryProduct galleryProduct)
        {
            if (galleryProduct == null)
                throw new ArgumentNullException("galleryProduct");

            //update
            _galleryProductRepository.Update(galleryProduct);

            //event notification
            _eventPublisher.EntityUpdated(galleryProduct);
        }

        public virtual IPagedList<GalleryProduct> GetAllGalleryProducts(int galleryCategoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = _galleryProductRepository.Table;
            query = query.Where(p => !p.Deleted);
            query = query.Where(p => p.Published);

            //category filtering
            if (galleryCategoryId > 0)
            {
                //search in subcategories
                query = from p in query
                        from pc in p.GalleryProductCategories.Where(pc => galleryCategoryId == pc.GalleryCategoryId)
                        select p;
            }

            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();


            //paging
            return new PagedList<GalleryProduct>(query.ToList(), pageIndex, pageSize);
        }

        #endregion

        #region Distributor Link Product Methods

        public virtual void DeleteDistributorLink(DistributorLink distributorLink)
        {
            if (distributorLink == null)
                throw new ArgumentNullException("distributorLink");

            distributorLink.Deleted = true;
            //delete distributorLink
            UpdateDistributorLink(distributorLink);
        }

        public virtual IPagedList<DistributorLink> GetAllDistributorLinks(string DistributorLinkName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            var query = from p in _distributorLinkRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;

            if (!String.IsNullOrWhiteSpace(DistributorLinkName))
                query = query.Where(c => c.Name.Contains(DistributorLinkName));

            var distributorLinks = query.ToList();

            //paging
            return new PagedList<DistributorLink>(distributorLinks, pageIndex, pageSize);
        }

        public virtual DistributorLink GetDistributorLinkById(int distributorLinkId)
        {
            if (distributorLinkId == 0)
                return null;

            var distributorLink = _distributorLinkRepository.GetById(distributorLinkId);
            return distributorLink;
        }

        public virtual void InsertDistributorLink(DistributorLink distributorLink)
        {
            if (distributorLink == null)
                throw new ArgumentNullException("distributorLink");

            //insert
            _distributorLinkRepository.Insert(distributorLink);

            //event notification
            _eventPublisher.EntityInserted(distributorLink);
        }

        public virtual void UpdateDistributorLink(DistributorLink distributorLink)
        {
            if (distributorLink == null)
                throw new ArgumentNullException("distributorLink");

            //update
            _distributorLinkRepository.Update(distributorLink);

            //event notification
            _eventPublisher.EntityUpdated(distributorLink);
        }

        #endregion

        #region Article Comments Methods

        public virtual void InsertArticleComments(ArticleComment articleComments)
        {
            if (articleComments == null)
                throw new ArgumentNullException("project");

            //insert
            _articleCommentsRepository.Insert(articleComments);

        }

        public virtual void InsertArticleCommentsMapping(ArticleCommentMapping articleCommentsMapping)
        {
            if (articleCommentsMapping == null)
                throw new ArgumentNullException("project");

            //insert
            _articleCommentsMappingRepository.Insert(articleCommentsMapping);

        }

        public virtual IList<ArticleCommentMapping> GetArticleCommentsByProjectId(int projectId, int parentId = 0)
        {
            if (projectId == 0)
                return new List<ArticleCommentMapping>();

            var query = from acm in _articleCommentsMappingRepository.Table
                        join ac in _articleCommentsRepository.Table on acm.CommentId equals ac.Id
                        where acm.ProjectId == projectId && (ac.IsApproved) && acm.ParentId.Equals(parentId)
                        orderby ac.PublishedDate
                        select acm;
            
            return query.ToList();
        }

        public virtual IList<ArticleCommentMapping> GetAllArticleCommentsByProjectId(int projectId)
        {
            if (projectId == 0)
                return new List<ArticleCommentMapping>();

            var query = from acm in _articleCommentsMappingRepository.Table
                        join ac in _articleCommentsRepository.Table on acm.CommentId equals ac.Id
                        where acm.ProjectId == projectId && (ac.IsApproved)
                        orderby ac.PublishedDate
                        select acm;

            return query.ToList();
        }

        #endregion
    }
}
