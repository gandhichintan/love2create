using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Nop.Admin.Models.Catalog;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Projects
{
    public partial class ProjectModel : BaseNopEntityModel
    {
        public ProjectModel()
        {
            ProjectPictureModels = new List<ProjectPictureModel>();
        }

        public string PictureThumbnailUrl { get; set; }

        [AllowHtml]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string Caption { get; set; }

        //[AllowHtml]
        //public string Instructions { get; set; }
        
        [AllowHtml]
        public string Notes { get; set; }

        [AllowHtml]
        public string Keywords { get; set; }

        [UIHint("DateNullable")]
        public DateTime? Date { get; set; }

        [UIHint("DateNullable")]
        public DateTime? ProjectOfTheDay { get; set; }

        [UIHint("DateNullable")]
        public DateTime? PublishedDate { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        //[AllowHtml]
        //public string AudioFilePath { get; set; }

        public bool Published { get; set; }

        public bool IsArchived { get; set; }

        public bool IsArticle { get; set; }

        public bool IsTechnique { get; set; }

        public bool IsRoundup  { get; set; }

        public bool Featured { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool ShowOnCommunity { get; set; }

        public string ProjectTags { get; set; }

        public int NumberOfAvailableCategories { get; set; }

        public int NumberOfAvailableVideos { get; set; }

        public int NumberOfAvailablePatterns { get; set; }

        public int NumberOfAvailableArtists { get; set; }
        
        public int NumberOfAvailableTechniques { get; set; }

        //pictures
        public ProjectPictureModel AddPictureModel { get; set; }
        public IList<ProjectPictureModel> ProjectPictureModels { get; set; }

        public ProjectInstructionsModel projectInstructionsModel { get; set; }

        public ProjectMiscModel projectMiscModel { get; set; }

        public ProjectMaterialModel projectMaterialModel { get; set; }

        public ProjectTechniquesModel projectTechniquesModel { get; set; }

        public partial class ProjectPictureModel : BaseNopEntityModel
        {
            public int ProjectId { get; set; }

            [UIHint("Picture")]
            public int PictureId { get; set; }

            public string PictureUrl { get; set; }

            public int DisplayOrder { get; set; }
        }

        public partial class ProjectCategoryMappingModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.Category")]
            [UIHint("ProjectCategoryMapping")]
            public string ProjectCategory { get; set; }

            public int ProjectId { get; set; }

            public int ProjectCategoryId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
            public int DisplayOrder1 { get; set; }
        }

        public partial class ProjectProductModel : BaseNopEntityModel
        {
            public int ProjectId { get; set; }

            public int ProductId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Categories.Products.Fields.Product")]
            public string ProductName { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Categories.Products.Fields.DisplayOrder")]
            public int DisplayOrder2 { get; set; }
        }

        public partial class AddProjectProductModel : BaseNopModel
        {
            public AddProjectProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
            }

            public List<ProductModel> Products { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [NopResourceDisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }

            public int ProjectId { get; set; }

            public int[] SelectedProductIds { get; set; }
        }

        public partial class RelatedProjectModel : BaseNopEntityModel
        {
            public int ProjectId1 { get; set; }

            public int ProjectId2 { get; set; }

            [NopResourceDisplayName("Project")]
            public string Project2Name { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.RelatedProducts.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }
        }

        public partial class AddRelatedProjectModel : BaseNopModel
        {
            public AddRelatedProjectModel()
            {
                
            }

            public List<ProjectModel> Projects { get; set; }

            public int ProjectId { get; set; }

            [NopResourceDisplayName("Project Name")]
            [AllowHtml]
            public string SearchProjectName { get; set; }

            public int[] SelectedProjectIds { get; set; }
        }

        public partial class ProjectInstructionsModel : BaseNopEntityModel
        {
            public int ProjectId { get; set; }
            public string Title { get; set; }
            public string PictureUrl { get; set; }
            public string InstructionDescription { get; set; }
            [UIHint("Picture")]
            public int PictureId { get; set; }
            public string InstructionVideo { get; set; }
            public int DisplayOrder { get; set; }
            public bool Published { get; set; }
        }

        public partial class ProjectMiscModel : BaseNopEntityModel
        {
            public int ProjectId { get; set; }
            public string Description { get; set; }
            public int DisplayOrder { get; set; }
            public bool Published { get; set; }
        }

        public partial class ProjectMaterialModel : BaseNopEntityModel
        {
            public ProjectMaterialModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableProducts = new List<SelectListItem>();
            }

            public int ProjectId { get; set; }

            public int CategoryId { get; set; }
            public string Category { get; set; }

            public int ProductId { get; set; }
            public string Product { get; set; }

            public int DisplayOrder { get; set; }
            public bool IsFeatured { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableProducts { get; set; }
        }

        public partial class ProjectCustomerModel : BaseNopEntityModel
        {
            [UIHint("ProjectCustomer")]
            public string Customer { get; set; }

            public int CustomerId { get; set; }
            public int ProjectId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }
        }

        public partial class ProjectTechniquesModel : BaseNopEntityModel
        {
            [UIHint("ProjectTechnique")]
            public string ProjectTechnique { get; set; }

            public int ProjectId { get; set; }

            public int TechniqueId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
            public int DisplayOrder5 { get; set; }
        }
    }
}