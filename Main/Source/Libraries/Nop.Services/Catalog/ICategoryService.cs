using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Catalog;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Category service interface
    /// </summary>
    public partial interface ICategoryService
    {
        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="category">Category</param>
        void DeleteCategory(Category category);

        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <param name="categoryName">Category name</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IPagedList<Category> GetAllCategories(string categoryName = "",
            int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        /// <summary>
        /// Gets all categories filtered by parent category identifier
        /// </summary>
        /// <param name="parentCategoryId">Parent category identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Category collection</returns>
        IList<Category> GetAllCategoriesByParentCategoryId(int parentCategoryId,
            bool showHidden = false);

        /// <summary>
        /// Gets all categories displayed on the home page
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        IList<Category> GetAllCategoriesDisplayedOnHomePage(bool showHidden = false);
                
        /// <summary>
        /// Gets a category
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>Category</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// Inserts category
        /// </summary>
        /// <param name="category">Category</param>
        void InsertCategory(Category category);

        /// <summary>
        /// Updates the category
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="category">Category</param>
        void UpdateHasDiscountsApplied(Category category);

        /// <summary>
        /// Deletes a product category mapping
        /// </summary>
        /// <param name="productCategory">Product category</param>
        void DeleteProductCategory(ProductCategory productCategory);

        /// <summary>
        /// Gets product category mapping collection
        /// </summary>
        /// <param name="categoryId">Category identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product a category mapping collection</returns>
        IPagedList<ProductCategory> GetProductCategoriesByCategoryId(int categoryId,
            int pageIndex, int pageSize, bool showHidden = false);

        /// <summary>
        /// Gets a product category mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product category mapping collection</returns>
        IList<ProductCategory> GetProductCategoriesByProductId(int productId, bool showHidden = false);

        /// <summary>
        /// Gets a product category mapping 
        /// </summary>
        /// <param name="productCategoryId">Product category mapping identifier</param>
        /// <returns>Product category mapping</returns>
        ProductCategory GetProductCategoryById(int productCategoryId);

        /// <summary>
        /// Inserts a product category mapping
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        void InsertProductCategory(ProductCategory productCategory);

        /// <summary>
        /// Updates the product category mapping 
        /// </summary>
        /// <param name="productCategory">>Product category mapping</param>
        void UpdateProductCategory(ProductCategory productCategory);

        #region Blogs We Love Methods

        void DeleteBlogsWeLove(BlogsWeLove blogsWeLove);

        void UpdateBlogsWeLove(BlogsWeLove blogsWeLove);

        void InsertBlogsWeLove(BlogsWeLove blogsWeLove);

        BlogsWeLove GetBlogsWeLoveById(int blogsWeLoveId);

        IPagedList<BlogsWeLove> GetAllBlogsWeLoves(string BlogName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Book Method

        void DeleteBook(Book book);

        void UpdateBook(Book book);

        void InsertBook(Book book);

        Book GetBookById(int bookId);

        IPagedList<Book> GetAllBooks(string BookName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        List<Book> GetAllBooksByAuthorId(int authorId);

        #endregion

        #region Site We Love Method

        void InsertSiteWeLove(SiteWeLove sitewelove);

        void UpdateSiteWeLove(SiteWeLove sitewelove);

        void DeleteSiteWeLove(SiteWeLove sitewelove);

        SiteWeLove GetSiteWeLoveById(int siteweloveId);

        IPagedList<SiteWeLove> GetAllSiteWeLoves(string SiteName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Sponsorship Method

        void InsertSponsorship(Sponsorship sponsorship);

        void UpdateSponsorship(Sponsorship sponsorship);

        void DeleteSponsorship(Sponsorship sponsorship);

        IPagedList<Sponsorship> GetAllSponsorships(string SponsorName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        Sponsorship GetSponsorshipById(int sponsorshipId);

        #endregion

        #region Survey Category Registration

        void InsertSurveyRegistration(SurveyRegistration surveyregistration);

        void UpdateSurveyRegistration(SurveyRegistration surveyregistration);

        void DeleteSurveyRegistration(SurveyRegistration surveyregistration);

        List<SurveyCategory> GetAllSurveyCategories();

        void InsertSurveyCategoryRegistration(SurveyCategoryRegistration surveycategoryregistration);

        void UpdateSurveyCategoryRegistration(SurveyCategoryRegistration surveycategoryregistration);

        IPagedList<SurveyRegistration> GetAllSurveys(string SurveyName = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        SurveyRegistration GetSurveyById(int surveyRegistrationId);

        IList<SurveyCategoryRegistration> GetsurveyCategorybyId(int surveyRegistrationId);

        void DeleteCategoryRegistration(SurveyCategoryRegistration surveyCategoryRegistration);

        SurveyCategoryRegistration GetSurveyCategoryRegistrationById(int surveyCategoryRegistrationId);

        #endregion

        #region Events

        void InsertEvents(Event Events);

        void UpdateEvent(Event events);

        void DeleteEvent(Event events);

        List<Event> GetEventsDisplayedOnCommunityPage();

        Event GetEventById(int eventId);

        IPagedList<Event> GetAllEvents(string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);

        #endregion

        #region Category Video Mapping Methods

        void DeleteCategoryVideo(CategoryVideo categoryVideo);

        IPagedList<CategoryVideo> GetCategoryVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false);

        IList<CategoryVideo> GetCategoryVideosByCategoryId(int categoryId, bool showHidden = false);

        CategoryVideo GetCategoryVideoById(int categoryVideoId);

        void InsertCategoryVideo(CategoryVideo categoryVideo);

        void UpdateCategoryVideo(CategoryVideo categoryVideo);

        #endregion

        #region Project Cat

        void DeleteProjectCat(ProjectCat projectCat);

        IPagedList<ProjectCat> GetProjectCatByCategoryId(int categoryId,
            int pageIndex, int pageSize, bool showHidden = false);

        IList<ProjectCat> GetProjectCatByProjectId(int projectId, bool showHidden = false);

        ProjectCat GetProjectCatById(int projectCatId);

        void InsertProjectCat(ProjectCat projectCat);

        void UpdateProjectCat(ProjectCat projectCat);

        #endregion
    }
}
