using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Projects;
using Nop.Services.Media;

namespace Nop.Services.Projects
{
    public partial interface IProjectService
    {
        #region Project Category

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteProjectCategory(ProjectCategory category);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IPagedList<ProjectCategory> GetAllProjectCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        IList<ProjectCategory> GetAllProjectCategoriesByParentCategoryId(int parentProjectCategoryId,
            bool showHidden = false);

        IList<ProjectCategory> GetAllProjectCategoriesInMenuByParentCategoryId(int parentProjectCategoryId,
            bool showHidden = false);

        IList<ProjectCategory> GetAllProjectCategoriesInSidebarByParentCategoryId(int parentProjectCategoryId,
            bool showHidden = false);

        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <returns>Categories</returns>
        IList<ProjectCategory> GetAllProjectCategoriesDisplayedOnHomePage();

        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        ProjectCategory GetProjectCategoryById(int projectCategoryId);

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        void InsertProjectCategory(ProjectCategory projectCategory);

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateProjectCategory(ProjectCategory projectCategory);

        int GetCategoryIdByName(string p);

        #endregion

        #region Project Category Mapping

        void DeleteProjectCategoryMapping(ProjectCategoryMapping projectCategoryMapping);

        IPagedList<ProjectCategoryMapping> GetProjectCategoryMappingsByProjectCategoryId(int projectCategoryId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectCategoryMapping> GetProjectCategoryMappingsByProjectId(int projectId, bool showHidden = false);

        ProjectCategoryMapping GetProjectCategoryMappingById(int projectCategoryMappingId);

        void InsertProjectCategoryMapping(ProjectCategoryMapping projectCategoryMapping);

        void UpdateProjectCategoryMapping(ProjectCategoryMapping projectCategoryMapping);

        #endregion

        #region Project Product Mapping

        void DeleteProjectProduct(ProjectProduct projectProduct);

        IPagedList<ProjectProduct> GetProjectProductsByProductId(int productId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectProduct> GetProjectProductsByProjectId(int projectId, bool showHidden = false);

        IList<Project> GetProjectsByProductId(int productId, bool showHidden = false);

        ProjectProduct GetProjectProductById(int projectProductId);

        void InsertProjectProduct(ProjectProduct projectProduct);

        void UpdateProjectProduct(ProjectProduct projectProduct);

        IPagedList<ProjectProduct> GetProjectProductsByProjectId(int projectId, int pageIndex, int pageSize, bool showHidden = false);

        #endregion

        #region projects

        void UpdateProjectReviewTotals(Project project);

        void DeleteProject(Project project);

        IList<Project> GetAllFeaturedProjectsDisplayedOnHomePage();

        IList<Project> GetAllFeaturedProjectsDisplayedOnCommunityPage();

        Project GetProjectById(int projectId);

        IList<Project> GetProjectsByIds(int[] projectIds);

        void InsertProject(Project project);

        void UpdateProject(Project project);

        Picture GetDefaultProjectPicture(int projectId, IPictureService pictureService);

        IList<Project> GetAllProjects(bool showHidden = false);

        IList<Project> GetAllProjects(string projectName, bool showHidden = false);

        IPagedList<Project> GetAllProjects(string query, int pageIndex, int pageSize);

        IList<Project> GetAllProjectsDisplayedOnHomePage();

        IPagedList<Project> GetAllProjects(int pageIndex, int pageSize, int manufacturerId = 0, int categoryId = 0, bool showHidden = false);

        IPagedList<Project> GetAllProjects(string projectName, int pageIndex, int pageSize, bool showHidden = false);
        
        IPagedList<Project> GetAllProjects(IList<int> categoryIds, string keywords, int pageIndex, int pageSize, out IList<int> filterableCatIds);

        void DeleteProjectPicture(ProjectPictureMapping projectPicture);

        IList<ProjectPictureMapping> GetProjectPicturesByProjectId(int projectId);

        ProjectPictureMapping GetProjectPictureById(int projectPictureId);

        void InsertProjectPicture(ProjectPictureMapping projectPicture);

        void UpdateProjectPicture(ProjectPictureMapping projectPicture);

        IList<Picture> GetPicturesByProjectId(int projectId, int recordsToReturn);

        #endregion

        #region Project Reviews

        void InsertProjectReview(ProjectReview projectReview);

        #endregion

        #region Related projects

        void DeleteRelatedProject(RelatedProject relatedProject);

        IList<RelatedProject> GetRelatedProjectsByProjectId1(int projectId1, bool showHidden = false);

        RelatedProject GetRelatedProjectById(int relatedProjectId);

        void InsertRelatedProject(RelatedProject relatedProject);

        void UpdateRelatedProject(RelatedProject relatedProject);

        #endregion

        #region Project Instructions

        void InsertProjectInstruction(ProjectInstruction projectInstruction);

        void UpdateProjectInstruction(ProjectInstruction projectInstruction);

        void DeleteProjectInstruction(ProjectInstruction projectInstruction);

        IList<ProjectInstruction> GetProjectInstructionsByProjectId(int projectId, bool showHidden = false);

        ProjectInstruction GetProjectInstructionById(int projectInstructionId);

        #endregion

        #region Project Miscellaneous

        void InsertProjectMisc(ProjectMisc projectMisc);

        void UpdateProjectMisc(ProjectMisc projectMisc);

        void DeleteProjectMisc(ProjectMisc projectMisc);

        IList<ProjectMisc> GetProjectMiscByProjectId(int projectId, bool showHidden = false);

        ProjectMisc GetProjectMiscById(int projectMiscId);

        #endregion

        #region Recently Viewed Projects

        IList<Project> GetRecentlyViewedProjects(int number);

        void AddProjectToRecentlyViewedList(int projectId, int maxProjects = 0);

        #endregion

        #region Video

        void InsertVideo(Video video);

        void UpdateVideo(Video video);

        void DeleteVideo(Video video);

        Video GetVideoById(int videoId);

        IPagedList<Video> GetAllVideos(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Project Video Mapping

        void DeleteProjectVideo(ProjectVideo projectVideo);

        IPagedList<ProjectVideo> GetProjectVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectVideo> GetProjectVideosByProjectId(int projectId, bool showHidden = false);

        ProjectVideo GetProjectVideoById(int projectVideoId);

        void InsertProjectVideo(ProjectVideo projectVideo);

        void UpdateProjectVideo(ProjectVideo projectVideo);

        #endregion

        #region Pattern Methods

        void InsertPattern(Pattern pattern);

        void UpdatePattern(Pattern pattern);

        void DeletePattern(Pattern pattern);

        Pattern GetPatternById(int patternId);

        IPagedList<Pattern> GetAllPatterns(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Project Pattern Mapping

        void DeleteProjectPattern(ProjectPattern projectPattern);

        IPagedList<ProjectPattern> GetProjectPatternsByPatternId(int patternId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectPattern> GetProjectPatternsByProjectId(int projectId, bool showHidden = false);

        ProjectPattern GetProjectPatternById(int projectPatternId);

        void InsertProjectPattern(ProjectPattern projectPattern);

        void UpdateProjectPattern(ProjectPattern projectPattern);

        #endregion

        #region Project Favorite

        void DeleteProjectFavorite(ProjectFavorite projectFavorite);

        IPagedList<Project> GetProjectFavoritesByCustomerId(int CustomerId);

        void InsertProjectFavorite(ProjectFavorite projectFavorite);

        ProjectFavorite GetProjectFavoriteByCustomerId(int customerId, int projectId);

        int GetProjectFavoritesCountByProjectId(int projectId);

        #endregion

        #region Project Like

        void DeleteProjectLike(ProjectLike projectLike);

        IPagedList<Project> GetProjectLikesByCustomerId(int customerId);

        void InsertProjectLike(ProjectLike projectLike);

        ProjectLike GetProjectLikeByCustomerId(int customerId, int projectId);

        int GetProjectLikesByProjectId(int projectId);

        #endregion

        #region Project Customer Mapping

        void DeleteProjectCustomer(ProjectCustomer projectCustomer);

        IPagedList<ProjectCustomer> GetProjectCustomersByCustomerId(int customerId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectCustomer> GetProjectCustomersByProjectId(int projectId, bool showHidden = false);

        ProjectCustomer GetProjectCustomerById(int projectCustomerId);

        void InsertProjectCustomer(ProjectCustomer projectCustomer);

        void UpdateProjectCustomer(ProjectCustomer projectCustomer);

        #endregion

        #region Follower

        void InsertFollower(Follower follower);

        void DeleteFollower(Follower follower);

        Follower GetFollower(int artistId, int followerId);

        List<Follower> GetFollowers(int artistId);

        #endregion

        #region Technique

        void InsertTechnique(Technique technique);

        void UpdateTechnique(Technique technique);

        void DeleteTechnique(Technique technique);

        Technique GetTechniqueById(int techniqueId);

        IPagedList<Technique> GetAllTechniques(string name = "",
            int pageIndex = 0, int pageSize = int.MaxValue, int manufacturerId = 0, bool showHidden = false);

        #endregion

        #region Technique Detail

        void InsertTechniqueDetail(TechniqueDetail techniqueDetail);

        void UpdateTechniqueDetail(TechniqueDetail techniqueDetail);

        void DeleteTechniqueDetail(TechniqueDetail techniqueDetail);

        IList<TechniqueDetail> GetTechniqueDetailsByTechniqueId(int techniqueId);

        TechniqueDetail GetTechniqueDetailById(int techniqueDetailId);

        #endregion

        #region Project Technique

        void DeleteProjectTechnique(ProjectTechnique projectTechnique);

        IPagedList<ProjectTechnique> GetProjectTechniquesByTechniqueId(int techniqueId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectTechnique> GetProjectTechniquesByProjectId(int projectId, bool showHidden = false);

        ProjectTechnique GetProjectTechniqueById(int projectTechniqueId);

        void InsertProjectTechnique(ProjectTechnique projectTechnique);

        void UpdateProjectTechnique(ProjectTechnique projectTechnique);

        #endregion

        #region User Gallery

        void InsertUserGallery(UserGallery userGallery);

        void UpdateUserGallery(UserGallery userGallery);

        void DeleteUserGallery(UserGallery userGallery);

        UserGallery GetUserGalleryById(int userGalleryId);

        IPagedList<UserGallery> GetAllUserGalleries(string GalleryName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        IPagedList<UserGallery> GetUserGalleriesByCustomerId(int customerId, int pageIndex, int pageSize);

        #endregion

        #region Project Tag Methods

        void DeleteProjectTag(ProjectTag projectTag);

        IList<ProjectTag> GetAllProjectTags(bool showHidden = false);

        ProjectTag GetProjectTagById(int projectTagId);

        ProjectTag GetProjectTagByName(string name);

        void InsertProjectTag(ProjectTag projectTag);

        void UpdateProjectTag(ProjectTag projectTag);

        void UpdateProjectTagTotals(ProjectTag projectTag);

        #endregion

        #region Project Material Methods

        void InsertProjectMaterial(ProjectMaterial projectMaterial);

        void UpdateProjectMaterial(ProjectMaterial projectMaterial);

        void DeleteProjectMaterial(ProjectMaterial projectMaterial);

        IList<ProjectMaterial> GetProjectMaterialByProjectId(int projectId);

        ProjectMaterial GetProjectMaterialById(int projectMaterialId);

        #endregion

        #region Workshop Methods

        void DeleteWorkshop(Workshop workshop);

        void UpdateWorkshop(Workshop workshop);

        void InsertWorkshop(Workshop workshop);

        Workshop GetWorkshopById(int workshopId);

        IPagedList<Workshop> GetAllWorkshops(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Document Center Methods

        void DeleteDocumentCenter(DocumentCenter documentCenter);

        void UpdateDocumentCenter(DocumentCenter documentCenter);

        void InsertDocumentCenter(DocumentCenter documentCenter);

        DocumentCenter GetDocumentCenterById(int documentCenterId);

        IPagedList<DocumentCenter> GetAllDocumentCenters(string DocumentName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region BusinessBuilderProduct Methods

        void DeleteBusinessBuilderProduct(BusinessBuilderProduct businessBuilderProduct);

        IList<BusinessBuilderProduct> GetAllBusinessBuilderProducts(bool showHidden = false);

        IPagedList<BusinessBuilderProduct> GetAllBusinessBuilderProducts(string BuilderProductName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        BusinessBuilderProduct GetBusinessBuilderProductById(int businessBuilderProductId);

        IList<BusinessBuilderProduct> GetBusinessBuilderProductsByIds(int[] businessBuilderProductIds);

        void InsertBusinessBuilderProduct(BusinessBuilderProduct businessBuilderProduct);

        void UpdateBusinessBuilderProduct(BusinessBuilderProduct businessBuilderProduct);

        #endregion

        #region Business Builder Category Methods

        void DeleteBusinessBuilderCategory(BusinessBuilderCategory businessBuilderCategory);

        IPagedList<BusinessBuilderCategory> GetAllBusinessBuilderCategories(string BuilderCatName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        BusinessBuilderCategory GetBusinessBuilderCategoryById(int businessBuilderCategoryId);

        void InsertBusinessBuilderCategory(BusinessBuilderCategory businessBuilderCategory);

        void UpdateBusinessBuilderCategory(BusinessBuilderCategory businessBuilderCategory);

        #endregion

        #region Business Builder Product Category Methods

        void DeleteBusinessBuilderProductCategory(BusinessBuilderProductCategory businessBuilderProductCategory);

        IPagedList<BusinessBuilderProductCategory> GetBusinessBuilderProductCategoriesByCategoryId(int businessBuilderCategoryId, int pageIndex, int pageSize, bool showHidden = false);

        IPagedList<BusinessBuilderProduct> GetAllBusinessBuilderProducts(int categoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        IList<BusinessBuilderProductCategory> GetBusinessBuilderProductCategoriesByProductId(int businessBuilderProductId, bool showHidden = false);

        BusinessBuilderProductCategory GetBusinessBuilderProductCategoryById(int businessBuilderProductCategoryId);

        void InsertBusinessBuilderProductCategory(BusinessBuilderProductCategory businessBuilderProductCategory);

        void UpdateBusinessBuilderProductCategory(BusinessBuilderProductCategory businessBuilderProductCategory);

        #endregion

        #region Ambassador Links Product Methods

        void DeleteAmbassadorLinksProduct(AmbassadorLinksProduct ambassadorLinksProduct);

        IList<AmbassadorLinksProduct> GetAllAmbassadorLinksProducts(bool showHidden = false);

        IPagedList<AmbassadorLinksProduct> GetAllAmbassadorLinksProducts(string AmbassadorLinkProductName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        IPagedList<AmbassadorLinksProduct> GetAllAmbassadorLinksProducts(int categoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        AmbassadorLinksProduct GetAmbassadorLinksProductById(int ambassadorLinksProductId);

        IList<AmbassadorLinksProduct> GetAmbassadorLinksProductsByIds(int[] ambassadorLinksProductIds);

        void InsertAmbassadorLinksProduct(AmbassadorLinksProduct ambassadorLinksProduct);

        void UpdateAmbassadorLinksProduct(AmbassadorLinksProduct ambassadorLinksProduct);

        #endregion

        #region Ambassador Links Category Methods

        void DeleteAmbassadorLinksCategory(AmbassadorLinksCategory ambassadorLinksCategory);

        IPagedList<AmbassadorLinksCategory> GetAllAmbassadorLinksCategories(string AmbassadorLinkCatName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        AmbassadorLinksCategory GetAmbassadorLinksCategoryById(int ambassadorLinksCategoryId);

        void InsertAmbassadorLinksCategory(AmbassadorLinksCategory ambassadorLinksCategory);

        void UpdateAmbassadorLinksCategory(AmbassadorLinksCategory ambassadorLinksCategory);

        #endregion

        #region Ambassador Links Product Category Methods

        void DeleteAmbassadorLinksProductCategory(AmbassadorLinksProductCategory ambassadorLinksProductCategory);

        IPagedList<AmbassadorLinksProductCategory> GetAmbassadorLinksProductCategoriesByCategoryId(int businessBuilderCategoryId, int pageIndex, int pageSize, bool showHidden = false);

        IList<AmbassadorLinksProductCategory> GetAmbassadorLinksProductCategoriesByProductId(int businessBuilderProductId, bool showHidden = false);

        AmbassadorLinksProductCategory GetAmbassadorLinksProductCategoryById(int ambassadorLinksProductCategoryId);

        void InsertAmbassadorLinksProductCategory(AmbassadorLinksProductCategory ambassadorLinksProductCategory);

        void UpdateAmbassadorLinksProductCategory(AmbassadorLinksProductCategory ambassadorLinksProductCategory);

        #endregion

        #region Gallery Category Methods

        void DeleteGalleryCategory(GalleryCategory galleryCategory);

        IPagedList<GalleryCategory> GetAllGalleryCategories(string GalleryCatName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        IList<GalleryCategory> GetAllGalleryCategoriesByParentId(int parentId, bool showHidden = false);

        GalleryCategory GetGalleryCategoryById(int galleryCategoryId);

        void InsertGalleryCategory(GalleryCategory galleryCategory);

        void UpdateGalleryCategory(GalleryCategory galleryCategory);

        void DeleteGalleryProductCategory(GalleryProductCategory galleryProductCategory);

        IPagedList<GalleryProductCategory> GetGalleryProductCategoriesByCategoryId(int galleryCategoryId, int pageIndex, int pageSize, bool showHidden = false);

        IList<GalleryProductCategory> GetGalleryProductCategoriesByProductId(int galleryProductId, bool showHidden = false);

        GalleryProductCategory GetGalleryProductCategoryById(int galleryProductCategoryId);

        void InsertGalleryProductCategory(GalleryProductCategory galleryProductCategory);

        void UpdateGalleryProductCategory(GalleryProductCategory galleryProductCategory);

        #endregion

        #region Gallery Product Methods

        void DeleteGalleryProduct(GalleryProduct galleryProduct);

        IList<GalleryProduct> GetAllGalleryProducts(bool showHidden = false);

        IPagedList<GalleryProduct> GetAllGalleryProducts(string GalleryProductName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        GalleryProduct GetGalleryProductById(int galleryProductId);

        IList<GalleryProduct> GetGalleryProductsByIds(int[] galleryProductIds);
        
        void InsertGalleryProduct(GalleryProduct galleryProduct);

        void UpdateGalleryProduct(GalleryProduct galleryProduct);

        IPagedList<GalleryProduct> GetAllGalleryProducts(int galleryCategoryId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Distributor Link Product Methods

        void DeleteDistributorLink(DistributorLink distributorLink);

        IPagedList<DistributorLink> GetAllDistributorLinks(string DistributorLinkName = "",int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        DistributorLink GetDistributorLinkById(int distributorLinkId);

        void InsertDistributorLink(DistributorLink distributorLink);

        void UpdateDistributorLink(DistributorLink distributorLink);

        #endregion

        #region Article Comments

        void InsertArticleComments(ArticleComment articleComments);

        void InsertArticleCommentsMapping(ArticleCommentMapping articleCommentsMapping);

        IList<ArticleCommentMapping> GetArticleCommentsByProjectId(int projectId, int parentId = 0);

        IList<ArticleCommentMapping> GetAllArticleCommentsByProjectId(int projectId);

        #endregion
    }
}
