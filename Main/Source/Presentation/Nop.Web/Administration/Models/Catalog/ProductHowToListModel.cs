﻿using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;


namespace Nop.Admin.Models.Catalog
{
    public class ProductHowToListModel: BaseNopModel
    {
        public ProductHowToListModel()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
        }
        public List<ProductHowToModel> ProductsHowTo { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.List.SearchProductName")]
        [AllowHtml]
        public string SearchProductHowToName { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.List.SearchCategory")]
        public int SearchCategoryId { get; set; }

        [NopResourceDisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
        public int SearchManufacturerId { get; set; }

        //[NopResourceDisplayName("Admin.Catalog.Products.List.GoDirectlyToSku")]
        //[AllowHtml]
        //public string GoDirectlyToSku { get; set; }

        public bool DisplayProductPictures { get; set; }
        public bool DisplayPdfDownloadCatalog { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }
    }
}