using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Models.Projects;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public partial class BundleModel : BaseNopEntityModel
    {
        public BundleModel()
        {
            BundlePictureModels = new List<BundlePictureModel>();
        }

        //picture thumbnail
        [NopResourceDisplayName("Admin.Catalog.Products.Fields.PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.ShortDescription")]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.FullDescription")]
        [AllowHtml]
        public string FullDescription { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.ShowOnHomePage")]
        public bool ShowOnHomePage { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.Fields.Published")]
        public bool Published { get; set; }

        //pictures
        public BundlePictureModel AddPictureModel { get; set; }
        public IList<BundlePictureModel> BundlePictureModels { get; set; }

        #region Nested classes

        public partial class BundlePictureModel : BaseNopEntityModel
        {
            public int BundleId { get; set; }

            [UIHint("Picture")]
            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public int PictureId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public string PictureUrl { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }
        }

        public partial class BundleProductModel : BaseNopEntityModel
        {
            public int BundleId { get; set; }

            public int ProductId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Categories.Products.Fields.Product")]
            public string ProductName { get; set; }

            [NopResourceDisplayName("Admin.Catalog.Categories.Products.Fields.DisplayOrder")]
            public int DisplayOrder2 { get; set; }
        }

        public partial class AddBundleProductModel : BaseNopModel
        {
            public AddBundleProductModel()
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

            public int BundleId { get; set; }

            public int[] SelectedProductIds { get; set; }
        }

        public partial class BundleProjectModel : BaseNopEntityModel
        {
            public int BundleId { get; set; }

            public int ProjectId { get; set; }

            public string ProjectName { get; set; }

            public int DisplayOrder3 { get; set; }
        }

        public partial class AddBundleProjectModel : BaseNopModel
        {
            public AddBundleProjectModel()
            {

            }

            public List<ProjectModel> Projects { get; set; }

            public int BundleId { get; set; }

            [NopResourceDisplayName("Project Name")]
            [AllowHtml]
            public string SearchProjectName { get; set; }

            public int[] SelectedProjectIds { get; set; }
        }

        #endregion
    }
}