using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Projects;
using Nop.Core.Domain.Security;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Seo;


namespace Nop.Services.Catalog
{
    public partial class ProductHowToService : IProductHowToService
    {
        #region Fields

        private readonly IRepository<ProductHowTo> _producthowtoRepository;
        private readonly IRepository<ProductHowToVideo> _producthowtoVideoRepository;
        private readonly IRepository<ProductHowToBundle> _producthowtoBundleRepository;
        private readonly IRepository<ProductHowToPicture> _producthowtoPictureRepository;
        private readonly IRepository<ProductHowToManufacturer> _producthowtoManufacturerRepository;
        private readonly IRepository<ProductHowToCategory> _producthowtoCategoryRepository;
        private readonly IRepository<ProductHowToTechnique> _producthowtoTechniqueRepository;
        private readonly IRepository<ProductHowToProject> _producthowtoProjectRepository;
        private readonly IRepository<ProductHowToProduct> _producthowtoProductRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Bundle> _bundleRepository;
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<Technique> _techniqueRepository;
        private readonly IRepository<Product> _productRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="producthowtoRepository">ProductHowTo repository</param>
        /// <param name="relatedProductHowToRepository">Related producthowto repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo picture repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo bundle repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo  video repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo manufacturer repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo category repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo technique repository</param>
        /// <param name="producthowtoPictureRepository">ProductHowTo project repository</param>

        public ProductHowToService(
            IRepository<ProductHowTo> producthowtoRepository,
            IRepository<ProductHowToVideo> producthowtoVideoRepository,
            IRepository<ProductHowToBundle> producthowtoBundleRepository,
            IRepository<ProductHowToPicture> producthowtoPictureRepository,
            IRepository<ProductHowToProject> producthowtoProjectRepository,
            IRepository<ProductHowToManufacturer> producthowtoManufacturerRepository,
            IRepository<ProductHowToCategory> producthowtoCategoryRepository,
            IRepository<ProductHowToTechnique> producthowtoTechniqueRepository,
            IRepository<ProductHowToProduct> producthowtoProductRepository,
            IRepository<Project> projectRepository,
             IRepository<Bundle> bundleRepository,
            IRepository<Video> videoRepository,
             IRepository<Category> categoryRepository,
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<Technique> techniqueRepository,
            IRepository<Product> productRepository)
        {
            this._producthowtoRepository = producthowtoRepository;
            this._producthowtoVideoRepository = producthowtoVideoRepository;
            this._producthowtoBundleRepository = producthowtoBundleRepository;
            this._producthowtoPictureRepository = producthowtoPictureRepository;
            this._producthowtoProjectRepository = producthowtoProjectRepository;
            this._producthowtoManufacturerRepository = producthowtoManufacturerRepository;
            this._producthowtoCategoryRepository = producthowtoCategoryRepository;
            this._producthowtoTechniqueRepository = producthowtoTechniqueRepository;
            this._producthowtoProductRepository = producthowtoProductRepository;
            this._projectRepository = projectRepository;
            this._bundleRepository = bundleRepository;
            this._videoRepository = videoRepository;
            this._categoryRepository = categoryRepository;
            this._manufacturerRepository = manufacturerRepository;
            this._techniqueRepository = techniqueRepository;
            this._productRepository = productRepository;
        }

        #endregion

        #region Products Howto

        /// <summary>
        /// Delete a producthowto
        /// </summary>
        /// <param name="product">ProductHowTo</param>
        public virtual void DeleteProductHowTo(ProductHowTo producthowto)
        {
            if (producthowto == null)
                throw new ArgumentNullException("producthowto");

            producthowto.Deleted = true;
            //delete product
            UpdateProductHowTo(producthowto);

        }

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        public virtual IList<ProductHowTo> GetAllProductHowTo(bool showHidden = false)
        {
            var query = from p in _producthowtoRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            var producthowto = query.ToList();
            return producthowto;
        }

        /// <summary>
        /// Gets all products displayed on the home page
        /// </summary>
        /// <returns>Product collection</returns>
        public virtual IList<ProductHowTo> GetAllProductHowToDisplayedOnHomePage()
        {
            var query = from p in _producthowtoRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted
                        select p;
            var producthowto = query.ToList();
            return producthowto;
        }

        public virtual IList<ProductHowTo> GetAllSpecialOfferProductHowToDisplayedOnHomePage()
        {
            var query = from p in _producthowtoRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted
                        select p;
            var producthowto = query.ToList();
            return producthowto;
        }

        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual ProductHowTo GetProductHowToById(int producthowtoId)
        {
            if (producthowtoId == 0)
                return null;

            var product = _producthowtoRepository.GetById(producthowtoId);
            return product;
        }

        /// <summary>
        /// Get products by identifiers
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        public virtual IList<ProductHowTo> GetProductHowToByIds(int[] producthowtoIds)
        {
            if (producthowtoIds == null || producthowtoIds.Length == 0)
                return new List<ProductHowTo>();

            var query = from p in _producthowtoRepository.Table
                        where producthowtoIds.Contains(p.Id)
                        select p;
            var products = query.ToList();
            //sort by passed identifiers
            var sortedProducthowto = new List<ProductHowTo>();
            foreach (int id in producthowtoIds)
            {
                var product = products.Find(x => x.Id == id);
                if (product != null)
                    sortedProducthowto.Add(product);
            }
            return sortedProducthowto;
        }

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void InsertProductHowTo(ProductHowTo producthowto)
        {
            if (producthowto == null)
                throw new ArgumentNullException("producthowto");

            //insert
            _producthowtoRepository.Insert(producthowto);

        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateProductHowTo(ProductHowTo producthowto)
        {
            if (producthowto == null)
                throw new ArgumentNullException("producthowto");

            //update
            _producthowtoRepository.Update(producthowto);

        }

        public virtual IPagedList<ProductHowTo> SearchProductHowTo(List<int> categoryIds, int manufacturerId,
            String keywords, int pageIndex, int pageSize, bool showHidden = false)
        {
            //products
            var query = _producthowtoRepository.Table;
            query = query.Where(p => !p.Deleted);
            if (!showHidden)
            {
                query = query.Where(p => p.Published);

            }

            //searching by keyword
            if (!String.IsNullOrWhiteSpace(keywords))
            {
                query = from p in query
                        where p.Name.Contains(keywords)
                        select p;
            }

            //category filtering
            if (categoryIds != null && categoryIds.Count > 0)
            {
                //search in subcategories
                query = from p in query
                        from pc in p.ProductHowToCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                        select p;
            }

            //manufacturer filtering
            if (manufacturerId > 0)
            {
                query = from p in query
                        from pm in p.ProductHowToManufacturers.Where(pm => pm.ManufacturerId == manufacturerId)
                        select p;
            }

          
            //only distinct products (group by ID)
            //if we use standard Distinct() method, then all fields will be compared (low performance)
            //it'll not work in SQL Server Compact when searching products by a keyword)
            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();
            query = query.OrderBy(p => p.Name);



            var productsHowTo = new PagedList<ProductHowTo>(query, pageIndex, pageSize);


            //return products
            return productsHowTo;
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="loadFilterableSpecificationAttributeOptionIds">A value indicating whether we should load the specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="filterableSpecificationAttributeOptionIds">The specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        //public virtual IPagedList<Product> SearchProductHowTo(IList<int> categoryIds,
        //    int manufacturerId, bool? featuredProducts,
        //    decimal? priceMin, decimal? priceMax, int productTagId,
        //    string keywords, bool searchDescriptions, bool searchProductTags, int languageId,
        //    IList<int> filteredSpecs, ProductSortingEnum orderBy,
        //    int pageIndex, int pageSize,
        //    bool loadFilterableSpecificationAttributeOptionIds, out IList<int> filterableSpecificationAttributeOptionIds,
        //    bool showHidden = false)
        //{
        //    filterableSpecificationAttributeOptionIds = new List<int>();
        //    bool searchLocalizedValue = false;
        //    if (languageId > 0)
        //    {
        //        if (showHidden)
        //        {
        //            searchLocalizedValue = true;
        //        }
        //        else
        //        {
        //            //ensure that we have at least two published languages
        //            //var totalPublishedLanguages = _languageService.GetAllLanguages(false).Count;
        //            searchLocalizedValue = totalPublishedLanguages >= 2;
        //        }
        //    }

        //    //Access control list. Allowed customer roles
        //    var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
        //        .Where(cr => cr.Active).Select(cr => cr.Id).ToList();


        //    if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
        //    {
        //        //stored procedures are enabled and supported by the database. 
        //        //It's much faster than the LINQ implementation below 

        //        #region Use stored procedure

        //        //pass category identifiers as comma-delimited string
        //        string commaSeparatedCategoryIds = "";
        //        if (categoryIds != null)
        //        {
        //            for (int i = 0; i < categoryIds.Count; i++)
        //            {
        //                commaSeparatedCategoryIds += categoryIds[i].ToString();
        //                if (i != categoryIds.Count - 1)
        //                {
        //                    commaSeparatedCategoryIds += ",";
        //                }
        //            }
        //        }


        //        //pass customer role identifiers as comma-delimited string
        //        string commaSeparatedAllowedCustomerRoleIds = "";
        //        for (int i = 0; i < allowedCustomerRolesIds.Count; i++)
        //        {
        //            commaSeparatedAllowedCustomerRoleIds += allowedCustomerRolesIds[i].ToString();
        //            if (i != allowedCustomerRolesIds.Count - 1)
        //            {
        //                commaSeparatedAllowedCustomerRoleIds += ",";
        //            }
        //        }


        //        //pass specification identifiers as comma-delimited string
        //        string commaSeparatedSpecIds = "";
        //        if (filteredSpecs != null)
        //        {
        //            ((List<int>)filteredSpecs).Sort();
        //            for (int i = 0; i < filteredSpecs.Count; i++)
        //            {
        //                commaSeparatedSpecIds += filteredSpecs[i].ToString();
        //                if (i != filteredSpecs.Count - 1)
        //                {
        //                    commaSeparatedSpecIds += ",";
        //                }
        //            }
        //        }

        //        //some databases don't support int.MaxValue
        //        if (pageSize == int.MaxValue)
        //            pageSize = int.MaxValue - 1;

        //        //prepare parameters
        //        var pCategoryIds = _dataProvider.GetParameter();
        //        pCategoryIds.ParameterName = "CategoryIds";
        //        pCategoryIds.Value = commaSeparatedCategoryIds != null ? (object)commaSeparatedCategoryIds : DBNull.Value;
        //        pCategoryIds.DbType = DbType.String;

        //        var pManufacturerId = _dataProvider.GetParameter();
        //        pManufacturerId.ParameterName = "ManufacturerId";
        //        pManufacturerId.Value = manufacturerId;
        //        pManufacturerId.DbType = DbType.Int32;

        //        var pProductTagId = _dataProvider.GetParameter();
        //        pProductTagId.ParameterName = "ProductTagId";
        //        pProductTagId.Value = productTagId;
        //        pProductTagId.DbType = DbType.Int32;

        //        var pFeaturedProducts = _dataProvider.GetParameter();
        //        pFeaturedProducts.ParameterName = "FeaturedProducts";
        //        pFeaturedProducts.Value = featuredProducts.HasValue ? (object)featuredProducts.Value : DBNull.Value;
        //        pFeaturedProducts.DbType = DbType.Boolean;

        //        var pPriceMin = _dataProvider.GetParameter();
        //        pPriceMin.ParameterName = "PriceMin";
        //        pPriceMin.Value = priceMin.HasValue ? (object)priceMin.Value : DBNull.Value;
        //        pPriceMin.DbType = DbType.Decimal;

        //        var pPriceMax = _dataProvider.GetParameter();
        //        pPriceMax.ParameterName = "PriceMax";
        //        pPriceMax.Value = priceMax.HasValue ? (object)priceMax.Value : DBNull.Value;
        //        pPriceMax.DbType = DbType.Decimal;

        //        var pKeywords = _dataProvider.GetParameter();
        //        pKeywords.ParameterName = "Keywords";
        //        pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
        //        pKeywords.DbType = DbType.String;

        //        var pSearchDescriptions = _dataProvider.GetParameter();
        //        pSearchDescriptions.ParameterName = "SearchDescriptions";
        //        pSearchDescriptions.Value = searchDescriptions;
        //        pSearchDescriptions.DbType = DbType.Boolean;

        //        var pSearchProductTags = _dataProvider.GetParameter();
        //        pSearchProductTags.ParameterName = "SearchProductTags";
        //        pSearchProductTags.Value = searchProductTags;
        //        pSearchProductTags.DbType = DbType.Boolean;

        //        var pUseFullTextSearch = _dataProvider.GetParameter();
        //        pUseFullTextSearch.ParameterName = "UseFullTextSearch";
        //        pUseFullTextSearch.Value = _commonSettings.UseFullTextSearch;
        //        pUseFullTextSearch.DbType = DbType.Boolean;

        //        var pFullTextMode = _dataProvider.GetParameter();
        //        pFullTextMode.ParameterName = "FullTextMode";
        //        pFullTextMode.Value = (int)_commonSettings.FullTextMode;
        //        pFullTextMode.DbType = DbType.Int32;

        //        var pFilteredSpecs = _dataProvider.GetParameter();
        //        pFilteredSpecs.ParameterName = "FilteredSpecs";
        //        pFilteredSpecs.Value = commaSeparatedSpecIds != null ? (object)commaSeparatedSpecIds : DBNull.Value;
        //        pFilteredSpecs.DbType = DbType.String;

        //        var pLanguageId = _dataProvider.GetParameter();
        //        pLanguageId.ParameterName = "LanguageId";
        //        pLanguageId.Value = searchLocalizedValue ? languageId : 0;
        //        pLanguageId.DbType = DbType.Int32;

        //        var pOrderBy = _dataProvider.GetParameter();
        //        pOrderBy.ParameterName = "OrderBy";
        //        pOrderBy.Value = (int)orderBy;
        //        pOrderBy.DbType = DbType.Int32;

        //        var pPageIndex = _dataProvider.GetParameter();
        //        pPageIndex.ParameterName = "PageIndex";
        //        pPageIndex.Value = pageIndex;
        //        pPageIndex.DbType = DbType.Int32;

        //        var pPageSize = _dataProvider.GetParameter();
        //        pPageSize.ParameterName = "PageSize";
        //        pPageSize.Value = pageSize;
        //        pPageSize.DbType = DbType.Int32;

        //        var pAllowedCustomerRoleIds = _dataProvider.GetParameter();
        //        pAllowedCustomerRoleIds.ParameterName = "AllowedCustomerRoleIds";
        //        pAllowedCustomerRoleIds.Value = commaSeparatedAllowedCustomerRoleIds;
        //        pAllowedCustomerRoleIds.DbType = DbType.String;

        //        var pShowHidden = _dataProvider.GetParameter();
        //        pShowHidden.ParameterName = "ShowHidden";
        //        pShowHidden.Value = showHidden;
        //        pShowHidden.DbType = DbType.Boolean;

        //        var pLoadFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
        //        pLoadFilterableSpecificationAttributeOptionIds.ParameterName = "LoadFilterableSpecificationAttributeOptionIds";
        //        pLoadFilterableSpecificationAttributeOptionIds.Value = loadFilterableSpecificationAttributeOptionIds;
        //        pLoadFilterableSpecificationAttributeOptionIds.DbType = DbType.Boolean;

        //        var pFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
        //        pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableSpecificationAttributeOptionIds";
        //        pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
        //        pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
        //        pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

        //        var pTotalRecords = _dataProvider.GetParameter();
        //        pTotalRecords.ParameterName = "TotalRecords";
        //        pTotalRecords.Direction = ParameterDirection.Output;
        //        pTotalRecords.DbType = DbType.Int32;

        //        //invoke stored procedure
        //        var products = _dbContext.ExecuteStoredProcedureList<Product>(
        //            "ProductLoadAllPaged",
        //            pCategoryIds,
        //            pManufacturerId,
        //            pProductTagId,
        //            pFeaturedProducts,
        //            pPriceMin,
        //            pPriceMax,
        //            pKeywords,
        //            pSearchDescriptions,
        //            pSearchProductTags,
        //            pUseFullTextSearch,
        //            pFullTextMode,
        //            pFilteredSpecs,
        //            pLanguageId,
        //            pOrderBy,
        //            pPageIndex,
        //            pPageSize,
        //            pAllowedCustomerRoleIds,
        //            pShowHidden,
        //            pLoadFilterableSpecificationAttributeOptionIds,
        //            pFilterableSpecificationAttributeOptionIds,
        //            pTotalRecords);
        //        //get filterable specification attribute option identifier
        //        string filterableSpecificationAttributeOptionIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
        //        if (loadFilterableSpecificationAttributeOptionIds &&
        //            !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
        //        {
        //            filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
        //               .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        //               .Select(x => Convert.ToInt32(x.Trim()))
        //               .ToList();
        //        }
        //        //return products
        //        int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
        //        return new PagedList<Product>(products, pageIndex, pageSize, totalRecords);

        //        #endregion
        //    }
        //    else
        //    {
        //        //stored procedures aren't supported. Use LINQ

        //        #region Search productHowTo

        //        //products
        //        var query = _producthowtoRepository.Table;
        //        query = query.Where(p => !p.Deleted);
        //        if (!showHidden)
        //        {
        //            query = query.Where(p => p.Published);
        //            query = query.Where(p => p.AvailableToBuy);
        //        }

        //        //searching by keyword
        //        if (!String.IsNullOrWhiteSpace(keywords))
        //        {
        //            query = from p in query
        //                    join lp in _localizedPropertyRepository.Table on p.Id equals lp.EntityId into p_lp
        //                    from lp in p_lp.DefaultIfEmpty()
        //                    from pv in p.ProductVariants.DefaultIfEmpty()
        //                    from pt in p.ProductTags.DefaultIfEmpty()
        //                    where (p.Name.Contains(keywords)) ||
        //                          (searchDescriptions && p.ShortDescription.Contains(keywords)) ||
        //                          (searchDescriptions && p.FullDescription.Contains(keywords)) ||
        //                          (pv.Name.Contains(keywords)) ||
        //                          (searchDescriptions && pv.Description.Contains(keywords)) ||
        //                          (searchProductTags && pt.Name.Contains(keywords)) ||
        //                        //localized values
        //                          (searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "Name" && lp.LocaleValue.Contains(keywords)) ||
        //                          (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "ShortDescription" && lp.LocaleValue.Contains(keywords)) ||
        //                          (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "FullDescription" && lp.LocaleValue.Contains(keywords))
        //                    //UNDONE search localized values in associated product tags
        //                    select p;
        //        }

        //        //ACL
        //        if (!showHidden)
        //        {
        //            query = from p in query
        //                    join acl in _aclRepository.Table on p.Id equals acl.EntityId into p_acl
        //                    from acl in p_acl.DefaultIfEmpty()
        //                    where !p.SubjectToAcl || (acl.EntityName == "Product" && allowedCustomerRolesIds.Contains(acl.CustomerRoleId))
        //                    select p;
        //        }

        //        //product variants
        //        //The function 'CurrentUtcDateTime' is not supported by SQL Server Compact. 
        //        //That's why we pass the date value
        //        var nowUtc = DateTime.UtcNow;
        //        query = from p in query
        //                from pv in p.ProductVariants.DefaultIfEmpty()
        //                where
        //                    //deleted
        //                    (showHidden || !pv.Deleted) &&
        //                    //published
        //                    (showHidden || pv.Published) &&
        //                    //price min
        //                    (
        //                        !priceMin.HasValue
        //                        ||
        //                    //special price (specified price and valid date range)
        //                        ((pv.SpecialPrice.HasValue && ((!pv.SpecialPriceStartDateTimeUtc.HasValue || pv.SpecialPriceStartDateTimeUtc.Value < nowUtc) && (!pv.SpecialPriceEndDateTimeUtc.HasValue || pv.SpecialPriceEndDateTimeUtc.Value > nowUtc))) && (pv.SpecialPrice >= priceMin.Value))
        //                        ||
        //                    //regular price (price isn't specified or date range isn't valid)
        //                        ((!pv.SpecialPrice.HasValue || ((pv.SpecialPriceStartDateTimeUtc.HasValue && pv.SpecialPriceStartDateTimeUtc.Value > nowUtc) || (pv.SpecialPriceEndDateTimeUtc.HasValue && pv.SpecialPriceEndDateTimeUtc.Value < nowUtc))) && (pv.Price >= priceMin.Value))
        //                    ) &&
        //                    //price max
        //                    (
        //                        !priceMax.HasValue
        //                        ||
        //                    //special price (specified price and valid date range)
        //                        ((pv.SpecialPrice.HasValue && ((!pv.SpecialPriceStartDateTimeUtc.HasValue || pv.SpecialPriceStartDateTimeUtc.Value < nowUtc) && (!pv.SpecialPriceEndDateTimeUtc.HasValue || pv.SpecialPriceEndDateTimeUtc.Value > nowUtc))) && (pv.SpecialPrice <= priceMax.Value))
        //                        ||
        //                    //regular price (price isn't specified or date range isn't valid)
        //                        ((!pv.SpecialPrice.HasValue || ((pv.SpecialPriceStartDateTimeUtc.HasValue && pv.SpecialPriceStartDateTimeUtc.Value > nowUtc) || (pv.SpecialPriceEndDateTimeUtc.HasValue && pv.SpecialPriceEndDateTimeUtc.Value < nowUtc))) && (pv.Price <= priceMax.Value))
        //                    ) &&
        //                    //available dates
        //                    (showHidden || (!pv.AvailableStartDateTimeUtc.HasValue || pv.AvailableStartDateTimeUtc.Value < nowUtc)) &&
        //                    (showHidden || (!pv.AvailableEndDateTimeUtc.HasValue || pv.AvailableEndDateTimeUtc.Value > nowUtc))
        //                select p;


        //        //search by specs
        //        if (filteredSpecs != null && filteredSpecs.Count > 0)
        //        {
        //            query = from p in query
        //                    where !filteredSpecs
        //                               .Except(
        //                                   p.ProductSpecificationAttributes.Where(psa => psa.AllowFiltering).Select(
        //                                       psa => psa.SpecificationAttributeOptionId))
        //                               .Any()
        //                    select p;
        //        }

        //        //category filtering
        //        if (categoryIds != null && categoryIds.Count > 0)
        //        {
        //            //search in subcategories
        //            query = from p in query
        //                    from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
        //                    where (!featuredProducts.HasValue || featuredProducts.Value == pc.IsFeaturedProduct)
        //                    select p;
        //        }

        //        //manufacturer filtering
        //        if (manufacturerId > 0)
        //        {
        //            query = from p in query
        //                    from pm in p.ProductManufacturers.Where(pm => pm.ManufacturerId == manufacturerId)
        //                    where (!featuredProducts.HasValue || featuredProducts.Value == pm.IsFeaturedProduct)
        //                    select p;
        //        }

        //        //related products filtering
        //        //if (relatedToProductId > 0)
        //        //{
        //        //    query = from p in query
        //        //            join rp in _relatedProductRepository.Table on p.Id equals rp.ProductId2
        //        //            where (relatedToProductId == rp.ProductId1)
        //        //            select p;
        //        //}

        //        //tag filtering
        //        if (productTagId > 0)
        //        {
        //            query = from p in query
        //                    from pt in p.ProductTags.Where(pt => pt.Id == productTagId)
        //                    select p;
        //        }

        //        //only distinct products (group by ID)
        //        //if we use standard Distinct() method, then all fields will be compared (low performance)
        //        //it'll not work in SQL Server Compact when searching products by a keyword)
        //        query = from p in query
        //                group p by p.Id
        //                    into pGroup
        //                    orderby pGroup.Key
        //                    select pGroup.FirstOrDefault();

        //        //sort products
        //        if (orderBy == ProductSortingEnum.Position && categoryIds != null && categoryIds.Count > 0)
        //        {
        //            //category position
        //            var firstCategoryId = categoryIds[0];
        //            query = query.OrderBy(p => p.ProductCategories.Where(pc => pc.CategoryId == firstCategoryId).FirstOrDefault().DisplayOrder);
        //        }
        //        else if (orderBy == ProductSortingEnum.Position && manufacturerId > 0)
        //        {
        //            //manufacturer position
        //            query =
        //                query.OrderBy(p => p.ProductManufacturers.Where(pm => pm.ManufacturerId == manufacturerId).FirstOrDefault().DisplayOrder);
        //        }
        //        //else if (orderBy == ProductSortingEnum.Position && relatedToProductId > 0)
        //        //{
        //        //    //sort by related product display order
        //        //    query = from p in query
        //        //            join rp in _relatedProductRepository.Table on p.Id equals rp.ProductId2
        //        //            where (relatedToProductId == rp.ProductId1)
        //        //            orderby rp.DisplayOrder
        //        //            select p;
        //        //}
        //        else if (orderBy == ProductSortingEnum.Position)
        //        {
        //            //sort by name (there's no any position if category or manufactur is not specified)
        //            query = query.OrderBy(p => p.Name);
        //        }
        //        else if (orderBy == ProductSortingEnum.NameAsc)
        //        {
        //            //Name: A to Z
        //            query = query.OrderBy(p => p.Name);
        //        }
        //        else if (orderBy == ProductSortingEnum.NameDesc)
        //        {
        //            //Name: Z to A
        //            query = query.OrderByDescending(p => p.Name);
        //        }
        //        else if (orderBy == ProductSortingEnum.PriceAsc)
        //        {
        //            //Price: Low to High
        //            query = query.OrderBy(p => p.ProductVariants.FirstOrDefault().Price);
        //        }
        //        else if (orderBy == ProductSortingEnum.PriceDesc)
        //        {
        //            //Price: High to Low
        //            query = query.OrderByDescending(p => p.ProductVariants.FirstOrDefault().Price);
        //        }
        //        else if (orderBy == ProductSortingEnum.CreatedOn)
        //        {
        //            //creation date
        //            query = query.OrderByDescending(p => p.CreatedOnUtc);
        //        }
        //        else
        //        {
        //            //actually this code is not reachable
        //            query = query.OrderBy(p => p.Name);
        //        }

        //        var products = new PagedList<Product>(query, pageIndex, pageSize);

        //        //get filterable specification attribute option identifier
        //        if (loadFilterableSpecificationAttributeOptionIds)
        //        {
        //            var querySpecs = from p in query
        //                             join psa in _productSpecificationAttributeRepository.Table on p.Id equals psa.ProductId
        //                             where psa.AllowFiltering
        //                             select psa.SpecificationAttributeOptionId;
        //            //only distinct attributes
        //            filterableSpecificationAttributeOptionIds = querySpecs
        //                .Distinct()
        //                .ToList();
        //        }

        //        //return products
        //        return products;

        //        #endregion
        //    }
        //}

        /// <summary>
        /// Update product review totals
        /// </summary>
        /// <param name="product">Product</param>
        //public virtual void UpdateProductHowToReviewTotals(ProductHowTo producthowto)
        //{
        //    if (producthowto == null)
        //        throw new ArgumentNullException("producthowto");

        //    int approvedRatingSum = 0;
        //    int notApprovedRatingSum = 0;
        //    int approvedTotalReviews = 0;
        //    int notApprovedTotalReviews = 0;
        //    var reviews = producthowto.ProducthowtoReviews;
        //    foreach (var pr in reviews)
        //    {
        //        if (pr.IsApproved)
        //        {
        //            approvedRatingSum += pr.Rating;
        //            approvedTotalReviews++;
        //        }
        //        else
        //        {
        //            notApprovedRatingSum += pr.Rating;
        //            notApprovedTotalReviews++;
        //        }
        //    }

        //    producthowto.ApprovedRatingSum = approvedRatingSum;
        //    producthowto.NotApprovedRatingSum = notApprovedRatingSum;
        //    producthowto.ApprovedTotalReviews = approvedTotalReviews;
        //    producthowto.NotApprovedTotalReviews = notApprovedTotalReviews;
        //    UpdateProductHowTo(producthowto);
        //}

        public virtual IList<ProductHowTo> GetProductHowToByCategoryId(int categoryId)
        {
            //products
            var query = _producthowtoRepository.Table;
            query = query.Where(p => !p.Deleted);

            //category filtering
            if (categoryId > 0)
            {
                //search in subcategories
                query = from p in query
                        from pc in p.ProductHowToCategories.Where(pc => categoryId == pc.CategoryId)
                        select p;
            }

            //only distinct products (group by ID)
            //if we use standard Distinct() method, then all fields will be compared (low performance)
            //it'll not work in SQL Server Compact when searching products by a keyword)
            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();

            return query.ToList();
        }

        #endregion

        #region Product How To Products

        public virtual void DeleteProductHowToProduct(ProductHowToProduct productHowToProduct)
        {
            if (productHowToProduct == null)
                throw new ArgumentNullException("productHowToProduct");

            _producthowtoProductRepository.Delete(productHowToProduct);
        }

        public virtual IPagedList<ProductHowToProduct> GetProductHowToProductsByProductHowToId(int productHowToId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (productHowToId == 0)
                return new PagedList<ProductHowToProduct>(new List<ProductHowToProduct>(), pageIndex, pageSize);

            var query = from pc in _producthowtoProductRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where pc.ProductHowToId == productHowToId &&
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

            var productHowToProducts = new PagedList<ProductHowToProduct>(query, pageIndex, pageSize);
            return productHowToProducts;
        }

        public virtual IList<ProductHowToProduct> GetProductHowToProductsByProductId(int productId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductHowToProduct>();

            var query = from pc in _producthowtoProductRepository.Table
                        join c in _producthowtoRepository.Table on pc.ProductHowToId equals c.Id
                        where pc.ProductId == productId &&
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

            var productHowToProducts = query.ToList();
            return productHowToProducts;
        }

        public virtual ProductHowToProduct GetProductHowToProductById(int productHowToProductId)
        {
            if (productHowToProductId == 0)
                return null;

            return _producthowtoProductRepository.GetById(productHowToProductId);
        }
        
        public virtual void InsertProductHowToProduct(ProductHowToProduct productHowToProduct)
        {
            if (productHowToProduct == null)
                throw new ArgumentNullException("productHowToProduct");

            _producthowtoProductRepository.Insert(productHowToProduct);
        }

        public virtual void UpdateProductHowToProduct(ProductHowToProduct productHowToProduct)
        {
            if (productHowToProduct == null)
                throw new ArgumentNullException("productHowToProduct");

            _producthowtoProductRepository.Update(productHowToProduct);
        }

        #endregion

        #region ProductHowTo pictures

        /// <summary>
        /// Deletes a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void DeleteProductHowToPicture(ProductHowToPicture producthowtoPicture)
        {
            if (producthowtoPicture == null)
                throw new ArgumentNullException("producthowtoPicture");

            _producthowtoPictureRepository.Delete(producthowtoPicture);

        }

        /// <summary>
        /// Gets a product pictures by product identifier
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <returns>Product pictures</returns>
        public virtual IList<ProductHowToPicture> GetProductHowToPicturesByProductHowToId(int producthowtoId)
        {
            var query = from pp in _producthowtoPictureRepository.Table
                        where pp.ProductHowToId == producthowtoId
                        orderby pp.DisplayOrder
                        select pp;
            var producthowtoPictures = query.ToList();
            return producthowtoPictures;
        }

        /// <summary>
        /// Gets a product picture
        /// </summary>
        /// <param name="productPictureId">Product picture identifier</param>
        /// <returns>Product picture</returns>
        public virtual ProductHowToPicture GetProductHowToPictureById(int producthowtoPictureId)
        {
            if (producthowtoPictureId == 0)
                return null;

            var pp = _producthowtoPictureRepository.GetById(producthowtoPictureId);
            return pp;
        }

        /// <summary>
        /// Inserts a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void InsertProductHowToPicture(ProductHowToPicture producthowtoPicture)
        {
            if (producthowtoPicture == null)
                throw new ArgumentNullException("producthowtoPicture");
            _producthowtoPictureRepository.Insert(producthowtoPicture);

        }

        /// <summary>
        /// Updates a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void UpdateProductHowToPicture(ProductHowToPicture producthowtoPicture)
        {
            if (producthowtoPicture == null)
                throw new ArgumentNullException("producthowtoPicture");

            _producthowtoPictureRepository.Update(producthowtoPicture);


        }

        #endregion

        #region Bundle ProductHowTo Mapping Methods

        public virtual void DeleteBundleProductHowTo(ProductHowToBundle bundleProductHowTo)
        {
            if (bundleProductHowTo == null)
                throw new ArgumentNullException("producthowtoBundle");

            _producthowtoBundleRepository.Delete(bundleProductHowTo);
        }

        public virtual IPagedList<ProductHowToBundle> GetBundleProductHowToByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new PagedList<ProductHowToBundle>(new List<ProductHowToBundle>(), pageIndex, pageSize);

            var query = from pc in _producthowtoBundleRepository.Table
                        join p in _bundleRepository.Table on pc.BundleId equals p.Id
                        where pc.ProductHowToId == producthowtoId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProducthowto = new PagedList<ProductHowToBundle>(query, pageIndex, pageSize);
            return bundleProducthowto;
        }

        public virtual IList<ProductHowToBundle> GetBundleProducthowtoByBundleId(int bundleId, bool showHidden = false)
        {
            if (bundleId == 0)
                return new List<ProductHowToBundle>();

            var query = from pc in _producthowtoBundleRepository.Table
                        join c in _producthowtoRepository.Table on pc.ProductHowToId equals c.Id
                        where pc.BundleId == bundleId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProductHowTo = query.ToList();
            return bundleProductHowTo;
        }

        public virtual ProductHowToBundle GetBundleProductHowToById(int bundleProductHowToId)
        {
            if (bundleProductHowToId == 0)
                return null;

            return _producthowtoBundleRepository.GetById(bundleProductHowToId);
        }

        public virtual void InsertBundleHowToProduct(ProductHowToBundle bundleProductHowTo)
        {
            if (bundleProductHowTo == null)
                throw new ArgumentNullException("bundleProductHowTo");

            _producthowtoBundleRepository.Insert(bundleProductHowTo);
        }

        public virtual void UpdateBundleProductHowTo(ProductHowToBundle bundleProductHowTo)
        {
            if (bundleProductHowTo == null)
                throw new ArgumentNullException("bundleProductHowTo");

            _producthowtoBundleRepository.Update(bundleProductHowTo);
        }

        public virtual IPagedList<ProductHowToBundle> GetBundleProducthowtoByBundleId(int bundleId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (bundleId == 0)
                return new PagedList<ProductHowToBundle>(new List<ProductHowToBundle>(), pageIndex, pageSize);

            var query = from pc in _producthowtoBundleRepository.Table
                        join p in _producthowtoRepository.Table on pc.ProductHowToId equals p.Id
                        where pc.BundleId == bundleId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProductsHowTo = new PagedList<ProductHowToBundle>(query, pageIndex, pageSize);
            return bundleProductsHowTo;
        }

        #endregion

        #region ProductHowTo Project Mapping Methods

        public virtual void DeleteproductHowToProject(ProductHowToProject ProductHowToProject)
        {
            if (ProductHowToProject == null)
                throw new ArgumentNullException("producthowtoProject");

            _producthowtoProjectRepository.Delete(ProductHowToProject);
        }

        public virtual IPagedList<ProductHowToProject> GetProductsHowToProjectByProjectId(int projectId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (projectId == 0)
                return new PagedList<ProductHowToProject>(new List<ProductHowToProject>(), pageIndex, pageSize);

            var query = from pc in _producthowtoProjectRepository.Table
                        join p in _producthowtoRepository.Table on pc.ProductHowToId equals p.Id
                        where pc.ProjectId == projectId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoProjects = new PagedList<ProductHowToProject>(query, pageIndex, pageSize);
            return producthowtoProjects;
        }

        public virtual IList<ProductHowToProject> GetProductHowToProjectsByProductHowToId(int producthowtoId, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new List<ProductHowToProject>();

            var query = from pc in _producthowtoProjectRepository.Table
                        join c in _projectRepository.Table on pc.ProjectId equals c.Id
                        where pc.ProductHowToId == producthowtoId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoProjects = query.ToList();
            return producthowtoProjects;
        }

        public virtual ProductHowToProject GetProductHowToProjectById(int producthowtoProjectId)
        {
            if (producthowtoProjectId == 0)
                return null;

            return _producthowtoProjectRepository.GetById(producthowtoProjectId);
        }

        public virtual void InsertProductHowToProject(ProductHowToProject producthowtoProject)
        {
            if (producthowtoProject == null)
                throw new ArgumentNullException("producthowtoProject");

            _producthowtoProjectRepository.Insert(producthowtoProject);
        }

        public virtual void UpdateProductHowToProject(ProductHowToProject producthowtoProject)
        {
            if (producthowtoProject == null)
                throw new ArgumentNullException("producthowtoProject");

            _producthowtoProjectRepository.Update(producthowtoProject);
        }

        public virtual IPagedList<ProductHowToProject> GetProductHowToProjectsByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new PagedList<ProductHowToProject>(new List<ProductHowToProject>(), pageIndex, pageSize);

            var query = from pc in _producthowtoProjectRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.ProductHowToId == producthowtoId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoProjects = new PagedList<ProductHowToProject>(query, pageIndex, pageSize);
            return producthowtoProjects;
        }

        #endregion

        #region ProductHowTo Video Mapping Methods

        public virtual void DeleteProductHowToVideo(ProductHowToVideo producthowtoVideo)
        {
            if (producthowtoVideo == null)
                throw new ArgumentNullException("producthowtoVideo");

            _producthowtoVideoRepository.Delete(producthowtoVideo);
        }

        public virtual IPagedList<ProductHowToVideo> GetProductHowToVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (videoId == 0)
                return new PagedList<ProductHowToVideo>(new List<ProductHowToVideo>(), pageIndex, pageSize);

            var query = from pc in _producthowtoVideoRepository.Table
                        join p in _producthowtoRepository.Table on pc.ProductHowToId equals p.Id
                        where pc.VideoId == videoId
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoVideos = new PagedList<ProductHowToVideo>(query, pageIndex, pageSize);
            return producthowtoVideos;
        }

        public virtual IList<ProductHowToVideo> GetProductHowToVideosByProductId(int producthowtoId, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new List<ProductHowToVideo>();

            var query = from pc in _producthowtoVideoRepository.Table
                        join c in _videoRepository.Table on pc.VideoId equals c.Id
                        where pc.ProductHowToId == producthowtoId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoVideos = query.ToList();
            return producthowtoVideos;
        }

        public virtual ProductHowToVideo GetProductHowToVideoById(int producthowtoVideoId)
        {
            if (producthowtoVideoId == 0)
                return null;

            return _producthowtoVideoRepository.GetById(producthowtoVideoId);
        }

        public virtual void InsertProductHowToVideo(ProductHowToVideo producthowtoVideo)
        {
            if (producthowtoVideo == null)
                throw new ArgumentNullException("producthowtoVideo");

            _producthowtoVideoRepository.Insert(producthowtoVideo);
        }

        public virtual void UpdateProductHowToVideo(ProductHowToVideo producthowtoVideo)
        {
            if (producthowtoVideo == null)
                throw new ArgumentNullException("producthowtoVideo");

            _producthowtoVideoRepository.Update(producthowtoVideo);
        }

        #endregion

        #region ProductHowToTechnique Mapping Method


        public virtual void DeleteproductHowToTechnique(ProductHowToTechnique producthowtoTechnique)
        {
            if (producthowtoTechnique == null)
                throw new ArgumentNullException("producthowtoTechnique");

            _producthowtoTechniqueRepository.Delete(producthowtoTechnique);
        }

        public virtual IPagedList<ProductHowToTechnique> GetProductsHowToTechniqueByTechniqueId(int techniqueId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (techniqueId == 0)
                return new PagedList<ProductHowToTechnique>(new List<ProductHowToTechnique>(), pageIndex, pageSize);

            var query = from pc in _producthowtoTechniqueRepository.Table
                        join p in _producthowtoRepository.Table on pc.ProductHowToId equals p.Id
                        where pc.TechniqueId == techniqueId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoTechnique = new PagedList<ProductHowToTechnique>(query, pageIndex, pageSize);
            return producthowtoTechnique;
        }

        public virtual IList<ProductHowToTechnique> GetProductHowToTechniqueByProductHowToId(int producthowtoId, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new List<ProductHowToTechnique>();

            var query = from pc in _producthowtoTechniqueRepository.Table
                        join c in _techniqueRepository.Table on pc.TechniqueId equals c.Id
                        where pc.ProductHowToId == producthowtoId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoTechnique = query.ToList();
            return producthowtoTechnique;
        }

        public virtual ProductHowToTechnique GetProductHowTotechniqueById(int producthowtoTechniqueId)
        {
            if (producthowtoTechniqueId == 0)
                return null;

            return _producthowtoTechniqueRepository.GetById(producthowtoTechniqueId);
        }
        public virtual Technique GetTechniqueById(int techniqueId)
        {
            if (techniqueId == 0)
                return null;

            var technique = _techniqueRepository.GetById(techniqueId);
            return technique;
        }
        public virtual void InsertProductHowToTechnique(ProductHowToTechnique producthowtoTechnique)
        {
            if (producthowtoTechnique == null)
                throw new ArgumentNullException("producthowtoTechnique");

            _producthowtoTechniqueRepository.Insert(producthowtoTechnique);
        }

        public virtual void UpdateProductHowToTechnique(ProductHowToTechnique producthowtoTechnique)
        {
            if (producthowtoTechnique == null)
                throw new ArgumentNullException("producthowtoTechnique");

            _producthowtoTechniqueRepository.Update(producthowtoTechnique);
        }

        public virtual IPagedList<ProductHowToTechnique> GetProductHowToTechniqueByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new PagedList<ProductHowToTechnique>(new List<ProductHowToTechnique>(), pageIndex, pageSize);

            var query = from pc in _producthowtoTechniqueRepository.Table
                        join p in _techniqueRepository.Table on pc.TechniqueId equals p.Id
                        where pc.ProductHowToId == producthowtoId &&
                             (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoTechnique = new PagedList<ProductHowToTechnique>(query, pageIndex, pageSize);
            return producthowtoTechnique;
        }


        #endregion

        #region  ProductHowToCategory Mapping Method

        public virtual void DeleteproductHowToCategory(ProductHowToCategory producthowtoCategory)
        {
            if (producthowtoCategory == null)
                throw new ArgumentNullException("producthowtoCategory");

            _producthowtoCategoryRepository.Delete(producthowtoCategory);
        }

        public virtual IPagedList<ProductHowToCategory> GetProductsHowToCategoryByCategoryId(int categoryId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (categoryId == 0)
                return new PagedList<ProductHowToCategory>(new List<ProductHowToCategory>(), pageIndex, pageSize);

            var query = from pc in _producthowtoCategoryRepository.Table
                        join p in _producthowtoRepository.Table on pc.ProductHowToId equals p.Id
                        where pc.CategoryId == categoryId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoCategory = new PagedList<ProductHowToCategory>(query, pageIndex, pageSize);
            return producthowtoCategory;
        }

        public virtual IList<ProductHowToCategory> GetProductHowToCategoryByProductHowToId(int producthowtoId, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new List<ProductHowToCategory>();

            var query = from pc in _producthowtoCategoryRepository.Table
                        join c in _categoryRepository.Table on pc.CategoryId equals c.Id
                        where pc.ProductHowToId == producthowtoId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoCategory = query.ToList();
            return producthowtoCategory;
        }

        public virtual ProductHowToCategory GetProductHowToCategoryById(int producthowtoCategoryId)
        {
            if (producthowtoCategoryId == 0)
                return null;

            return _producthowtoCategoryRepository.GetById(producthowtoCategoryId);
        }

        public virtual void InsertProductHowToCategory(ProductHowToCategory producthowtoCategory)
        {
            if (producthowtoCategory == null)
                throw new ArgumentNullException("producthowtoCategory");

            _producthowtoCategoryRepository.Insert(producthowtoCategory);
        }

        public virtual void UpdateProductHowToCategory(ProductHowToCategory producthowtoCategory)
        {
            if (producthowtoCategory == null)
                throw new ArgumentNullException("producthowtoCategory");

            _producthowtoCategoryRepository.Update(producthowtoCategory);
        }

        public virtual IPagedList<ProductHowToCategory> GetProductHowToCategoryByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new PagedList<ProductHowToCategory>(new List<ProductHowToCategory>(), pageIndex, pageSize);

            var query = from pc in _producthowtoCategoryRepository.Table
                        join p in _categoryRepository.Table on pc.CategoryId equals p.Id
                        where pc.ProductHowToId == producthowtoId &&
                             (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoCategory = new PagedList<ProductHowToCategory>(query, pageIndex, pageSize);
            return producthowtoCategory;
        }


        #endregion

        #region ProducthowTo Manufacturer

        public virtual void DeleteproductHowToManufacturer(ProductHowToManufacturer producthowtoManufacturer)
        {
            if (producthowtoManufacturer == null)
                throw new ArgumentNullException("producthowtoCategory");

            _producthowtoManufacturerRepository.Delete(producthowtoManufacturer);
        }

        public virtual IPagedList<ProductHowToManufacturer> GetProductsHowToManufacturerByManufacturerId(int manufacturerId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (manufacturerId == 0)
                return new PagedList<ProductHowToManufacturer>(new List<ProductHowToManufacturer>(), pageIndex, pageSize);

            var query = from pc in _producthowtoManufacturerRepository.Table
                        join p in _producthowtoRepository.Table on pc.ProductHowToId equals p.Id
                        where pc.ManufacturerId == manufacturerId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoManufacturer = new PagedList<ProductHowToManufacturer>(query, pageIndex, pageSize);
            return producthowtoManufacturer;
        }

        public virtual IList<ProductHowToManufacturer> GetProductHowToManufacturerByProductHowToId(int producthowtoId, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new List<ProductHowToManufacturer>();

            var query = from pc in _producthowtoManufacturerRepository.Table
                        join c in _manufacturerRepository.Table on pc.ManufacturerId equals c.Id
                        where pc.ProductHowToId == producthowtoId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoManufacturer = query.ToList();
            return producthowtoManufacturer;
        }

        public virtual ProductHowToManufacturer GetProductHowToManufacturerById(int producthowtoManufacturerId)
        {
            if (producthowtoManufacturerId == 0)
                return null;

            return _producthowtoManufacturerRepository.GetById(producthowtoManufacturerId);
        }

        public virtual void InsertProductHowToManufacturer(ProductHowToManufacturer producthowtoManufacturer)
        {
            if (producthowtoManufacturer == null)
                throw new ArgumentNullException("producthowtoManufacturer");

            _producthowtoManufacturerRepository.Insert(producthowtoManufacturer);
        }

        public virtual void UpdateProductHowToManufacturer(ProductHowToManufacturer producthowtoManufacturer)
        {
            if (producthowtoManufacturer == null)
                throw new ArgumentNullException("producthowtoManufacturer");

            _producthowtoManufacturerRepository.Update(producthowtoManufacturer);
        }

        public virtual IPagedList<ProductHowToManufacturer> GetProductHowToManufacturerByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (producthowtoId == 0)
                return new PagedList<ProductHowToManufacturer>(new List<ProductHowToManufacturer>(), pageIndex, pageSize);

            var query = from pc in _producthowtoManufacturerRepository.Table
                        join p in _manufacturerRepository.Table on pc.ManufacturerId equals p.Id
                        where pc.ProductHowToId == producthowtoId &&
                             (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var producthowtoManufacturer = new PagedList<ProductHowToManufacturer>(query, pageIndex, pageSize);
            return producthowtoManufacturer;
        }


        #endregion
    }
}
