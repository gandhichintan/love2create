using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Admin.Models.Catalog
{
    public class ProductHowToModel: BaseNopEntityModel
    {
        public ProductHowToModel()
        {
          //  Locales = new List<ProductLocalizedModel>();
           // ProductVariantModels = new List<ProductVariantModel>();
            ProductHowToPictureModels = new List<ProductHowToPictureModel>();
            //CopyProductHowToModel = new CopyProductHowToModel();
        }

        //picture thumbnail
        [NopResourceDisplayName("PictureThumbnailUrl")]
        public string PictureThumbnailUrl { get; set; }

        [NopResourceDisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [NopResourceDisplayName("ShortDescription")]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [NopResourceDisplayName("FullDescription")]
        [AllowHtml]
        public string FullDescription { get; set; }

        [NopResourceDisplayName("Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("How To Use")]
        [AllowHtml]
        public string HowToUse { get; set; }

        [NopResourceDisplayName("Tips And Tricks")]
        [AllowHtml]
        public string TipsAndTricks { get; set; }

        //public IList<ProductLocalizedModel> Locales { get; set; }

        public int NumberOfAvailableVideos { get; set; }

        //categories
        public int NumberOfAvailableCategories { get; set; }

        //public int NumberOfAvailableMaterials { get; set; }

        //manufacturers
        public int NumberOfAvailableManufacturers { get; set; }

        //pictures
        public ProductHowToPictureModel AddPictureModel { get; set; }
        public IList<ProductHowToPictureModel> ProductHowToPictureModels { get; set; }

        //add specification attribute model
       // public AddProductSpecificationAttributeModel AddSpecificationAttributeModel { get; set; }

        //copy product
       // public CopyProductHowToModel CopyProductModel { get; set; }
        
        #region Nested classes
        
        //public partial class AddProductHowToSpecificationAttributeModel : BaseNopEntityModel
        //{
        //    public AddProductHowToSpecificationAttributeModel()
        //    {
        //        AvailableAttributes = new List<SelectListItem>();
        //        AvailableOptions = new List<SelectListItem>();
        //    }
            
        //    [NopResourceDisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.SpecificationAttribute")]
        //    public int SpecificationAttributeId { get; set; }

        //    [NopResourceDisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.SpecificationAttributeOption")]
        //    public int SpecificationAttributeOptionId { get; set; }

        //    [NopResourceDisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.CustomValue")]
        //    public string CustomValue { get; set; }

        //    [NopResourceDisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.AllowFiltering")]
        //    public bool AllowFiltering { get; set; }

        //    [NopResourceDisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.ShowOnProductPage")]
        //    public bool ShowOnProductPage { get; set; }

        //    [NopResourceDisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.DisplayOrder")]
        //    public int DisplayOrder { get; set; }

        //    public IList<SelectListItem> AvailableAttributes { get; set; }
        //    public IList<SelectListItem> AvailableOptions { get; set; }
        //}
        
        public partial class ProductHowToPictureModel : BaseNopEntityModel
        {
            public int ProductHowToId { get; set; }

            [UIHint("Picture")]
            [NopResourceDisplayName("Pictures Fields")]
            public int PictureId { get; set; }

            [NopResourceDisplayName("Pictures Fields")]
            public string PictureUrl { get; set; }

            [NopResourceDisplayName("Pictures Fields DisplayOrder")]
            public int DisplayOrder1 { get; set; }
        }
        
        public partial class ProductHowToCategoryModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Categories Fields")]
            [UIHint("ProductHowToCategory")]
            public string Category { get; set; }

            public int ProductHowToId { get; set; }

            public int CategoryId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.ProductsHowTo.Categories.Fields.IsFeaturedProduct")]
            public bool IsFeaturedProduct { get; set; }

            [NopResourceDisplayName("Categories Fields DisplayOrder")]
            public int DisplayOrder2 { get; set; }
        }

        //public partial class ProductHowToMaterialModel : BaseNopEntityModel
        //{
        //    [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.Material")]
        //    [UIHint("ProductHowToMaterial")]
        //    public string Material { get; set; }

        //    public int ProductHowToId { get; set; }

        //    public int MaterialId { get; set; }

        //    [NopResourceDisplayName("Admin.Catalog.Products.Categories.Fields.DisplayOrder")]
        //    public int DisplayOrder { get; set; }
        //}

        public partial class ProductHowToManufacturerModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Manufacturers Fields")]
            [UIHint("ProductHowToManufacturer")]
            public string Manufacturer { get; set; }

            public int ProductHowToId { get; set; }

            public int ManufacturerId { get; set; }

            [NopResourceDisplayName("Manufacturers Fields IsFeaturedProduct")]
            public bool IsFeaturedProduct { get; set; }

            [NopResourceDisplayName("Manufacturers Fields DisplayOrder")]
            public int DisplayOrder3 { get; set; }
        }

        public partial class ProductHowToProductModel : BaseNopEntityModel
        {
            public int ProductHowToId { get; set; }

            public int ProductId { get; set; }

            [NopResourceDisplayName("Products Fields")]
            public string ProductName { get; set; }

            [NopResourceDisplayName("Products Fields DisplayOrder")]
            //we don't name it DisplayOrder because Telerik has a small bug 
            //"if we have one more editor with the same name on a page, it doesn't allow editing"
            //in our case it's category.DisplayOrder
            public int DisplayOrder4 { get; set; }
        }

        public partial class AddProductHowToProductModel : BaseNopModel
        {
            public AddProductHowToProductModel()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
            }
            public List<ProductModel> Products { get; set; }

            [NopResourceDisplayName("SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [NopResourceDisplayName("SearchCategory")]
            public int SearchCategoryId { get; set; }
            [NopResourceDisplayName("SearchManufacturer")]
            public int SearchManufacturerId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }

            public int ProductHowToId { get; set; }

            public int[] SelectedProductIds { get; set; }
        }

      
        public partial class ProductHowToVideoModel : BaseNopEntityModel
        {
            [UIHint("ProductHowToVideo")]
            public string Video { get; set; }

            public int ProductHowToId { get; set; }

            public int VideoId { get; set; }

            [NopResourceDisplayName("Video Fields DisplayOrder")]
            public int DisplayOrder6 { get; set; }
        }
        public partial class ProductHowToTechniqueModel : BaseNopEntityModel
        {
            [UIHint("ProductHowToTechnique")]
            public string Technique { get; set; }

            public int ProductHowToId { get; set; }

            public int TechniqueId { get; set; }

            [NopResourceDisplayName("Technique Fields DisplayOrder")]
            public int DisplayOrder7 { get; set; }
        }
     
        public partial class ProductHowToBundleModel : BaseNopEntityModel
        {
            [UIHint("ProductHowToBundle")]
            public string Bundle { get; set; }

            public int ProductHowToId { get; set; }

            public int BundleId { get; set; }

            [NopResourceDisplayName("Admin.Catalog.ProductsHowTo.Categories.Fields.DisplayOrder")]
            public int DisplayOrder8 { get; set; }
        }
        public partial class ProductHowToProjectModel : BaseNopEntityModel
        {
            [UIHint("ProductHowToProject")]
            public string Project { get; set; }

            public int ProductHowToId { get; set; }

            public int ProjectId { get; set; }

            [NopResourceDisplayName("Project Fields DisplayOrder")]
            public int DisplayOrder9 { get; set; }
        }

        #endregion

    }
}