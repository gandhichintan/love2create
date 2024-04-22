using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Core.Domain.Projects;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Product service
    /// </summary>
    public partial class ProductService : IProductService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        private const string PRODUCTS_BY_ID_KEY = "Nop.product.id-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PRODUCTS_PATTERN_KEY = "Nop.product.";
        #endregion

        #region Fields

        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<RelatedProduct> _relatedProductRepository;
        private readonly IRepository<CrossSellProduct> _crossSellProductRepository;
        private readonly IRepository<TierPrice> _tierPriceRepository;
        private readonly IRepository<LocalizedProperty> _localizedPropertyRepository;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        private readonly IRepository<ProductSpecificationAttribute> _productSpecificationAttributeRepository;
        private readonly IRepository<ProductReview> _productReviewRepository;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly ILanguageService _languageService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CommonSettings _commonSettings;
        private readonly CatalogSettings _catalogSettings;
        private readonly IEventPublisher _eventPublisher;
        private readonly IAclService _aclService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IRepository<Bundle> _bundleRepository;
        private readonly IRepository<BundlePicture> _bundlePictureRepository;
        private readonly IRepository<BundleProduct> _bundleProductRepository;
        private readonly IRepository<BundleProject> _bundleProjectRepository;
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<FaqCategoryMapping> _faqCategoryMappingRepository;
        private readonly IRepository<FaqCategory> _faqCategoryRepository;
        private readonly IRepository<Faq> _faqRepository;
        private readonly IRepository<ProductFaq> _productFaqRepository;
        private readonly IRepository<ProductVideo> _productVideoRepository;
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<ProductLike> _productLikeRepository;
        private readonly IRepository<TrendReport> _trendReportRepository;
        private readonly IRepository<Trend> _trendRepository;
        private readonly IRepository<Subtopic> _subTopicRepository;
        private readonly IRepository<SiteContent> _siteContentRepository;
        private readonly IRepository<Site> _siteRepository;
        private readonly IRepository<SafetyCategory> _safetyCategoryRepository;
        private readonly IRepository<Safety> _safetyRepository;
        private readonly IRepository<PageName> _pageNameRepository;
        private readonly IRepository<MetaTags> _metaTagsRepository;
        private readonly IRepository<MediaAboutus> _mediaAboutusRepository;
        private readonly IRepository<CareerInformation> _careerInformationRepository;
        private readonly IRepository<CareerOpening> _careerOpeningRepository;
        private readonly IRepository<JobLink> _jobLinkRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="productRepository">Product repository</param>
        /// <param name="relatedProductRepository">Related product repository</param>
        /// <param name="crossSellProductRepository">Cross-sell product repository</param>
        /// <param name="tierPriceRepository">Tier price repository</param>
        /// <param name="localizedPropertyRepository">Localized property repository</param>
        /// <param name="aclRepository">ACL record repository</param>
        /// <param name="storeMappingRepository">Store mapping repository</param>
        /// <param name="productPictureRepository">Product picture repository</param>
        /// <param name="productSpecificationAttributeRepository">Product specification attribute repository</param>
        /// <param name="productReviewRepository">Product review repository</param>
        /// <param name="productAttributeService">Product attribute service</param>
        /// <param name="productAttributeParser">Product attribute parser service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="workflowMessageService">Workflow message service</param>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbContext">Database Context</param>
        /// <param name="workContext">Work context</param>
        /// <param name="storeContext">Store context</param>
        /// <param name="localizationSettings">Localization settings</param>
        /// <param name="commonSettings">Common settings</param>
        /// <param name="catalogSettings">Catalog settings</param>
        /// <param name="eventPublisher">Event published</param>
        /// <param name="aclService">ACL service</param>
        /// <param name="storeMappingService">Store mapping service</param>
        public ProductService(ICacheManager cacheManager,
            IRepository<Product> productRepository,
            IRepository<RelatedProduct> relatedProductRepository,
            IRepository<CrossSellProduct> crossSellProductRepository,
            IRepository<TierPrice> tierPriceRepository,
            IRepository<ProductPicture> productPictureRepository,
            IRepository<LocalizedProperty> localizedPropertyRepository,
            IRepository<AclRecord> aclRepository,
            IRepository<StoreMapping> storeMappingRepository,
            IRepository<ProductSpecificationAttribute> productSpecificationAttributeRepository,
            IRepository<ProductReview>  productReviewRepository,
            IProductAttributeService productAttributeService,
            IProductAttributeParser productAttributeParser,
            ILanguageService languageService,
            IWorkflowMessageService workflowMessageService,
            IDataProvider dataProvider, 
            IDbContext dbContext,
            IWorkContext workContext,
            IStoreContext storeContext,
            LocalizationSettings localizationSettings, 
            CommonSettings commonSettings,
            CatalogSettings catalogSettings,
            IEventPublisher eventPublisher,
            IAclService aclService,
            IStoreMappingService storeMappingService,
            IRepository<Bundle> bundleRepository,
            IRepository<BundlePicture> bundlePictureRepository,
            IRepository<BundleProduct> bundleProductRepository,
            IRepository<BundleProject> bundleProjectRepository,
            IRepository<Project> projectRepository,
            IRepository<FaqCategoryMapping> faqCategoryMappingRepository, 
            IRepository<FaqCategory> faqCategoryRepository, 
            IRepository<Faq> faqRepository,
            IRepository<ProductFaq> productFaqRepository,
            IRepository<ProductVideo> productVideoRepository,
            IRepository<ProductLike> productLikeRepository,
            IRepository<Video> videoRepository, IRepository<TrendReport> trendReportRepository, 
            IRepository<Trend> trendRepository,
            IRepository<Subtopic> subTopicRepository,
            IRepository<SiteContent> siteContentRepository,
            IRepository<Site> siteRepository,
            IRepository<SafetyCategory> safetyCategoryRepository,
            IRepository<Safety> safetyRepository, 
            IRepository<PageName> pageNameRepository,
             IRepository<MetaTags> metaTagsRepository,
            IRepository<MediaAboutus> mediaAboutusRepository,
            IRepository<CareerInformation> careerInformationRepository,
            IRepository<CareerOpening> careerOpeningRepository,
            IRepository<JobLink> jobLinkRepository)
        {
            this._cacheManager = cacheManager;
            this._productRepository = productRepository;
            this._relatedProductRepository = relatedProductRepository;
            this._crossSellProductRepository = crossSellProductRepository;
            this._tierPriceRepository = tierPriceRepository;
            this._productPictureRepository = productPictureRepository;
            this._localizedPropertyRepository = localizedPropertyRepository;
            this._aclRepository = aclRepository;
            this._storeMappingRepository = storeMappingRepository;
            this._productSpecificationAttributeRepository = productSpecificationAttributeRepository;
            this._productReviewRepository = productReviewRepository;
            this._productAttributeService = productAttributeService;
            this._productAttributeParser = productAttributeParser;
            this._languageService = languageService;
            this._workflowMessageService = workflowMessageService;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._workContext = workContext;
            this._storeContext= storeContext;
            this._localizationSettings = localizationSettings;
            this._commonSettings = commonSettings;
            this._catalogSettings = catalogSettings;
            this._eventPublisher = eventPublisher;
            this._aclService = aclService;
            this._storeMappingService = storeMappingService;
            this._bundleRepository = bundleRepository;
            this._bundlePictureRepository = bundlePictureRepository;
            this._bundleProductRepository = bundleProductRepository;
            this._bundleProjectRepository = bundleProjectRepository;
            this._projectRepository = projectRepository;
            this._faqCategoryMappingRepository = faqCategoryMappingRepository;
            this._faqCategoryRepository = faqCategoryRepository;
            this._faqRepository = faqRepository;
            this._productVideoRepository = productVideoRepository;
            this._productLikeRepository = productLikeRepository;
            this._videoRepository = videoRepository;
            this._trendReportRepository = trendReportRepository;
            this._safetyCategoryRepository = safetyCategoryRepository;
            this._safetyRepository = safetyRepository;
            this._careerInformationRepository = careerInformationRepository;
            this._careerOpeningRepository = careerOpeningRepository;
            this._metaTagsRepository = metaTagsRepository;
            this._mediaAboutusRepository = mediaAboutusRepository;
            this._pageNameRepository = pageNameRepository;
            this._jobLinkRepository = jobLinkRepository;
        }

        #endregion
        
        #region Methods

        #region Products

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <returns name="product">Product</returns>
        public virtual IList<Product> GetAllProducts()
        {
            var query = from p in _productRepository.Table
                        orderby p.DisplayOrder, p.Name
                        where p.Published &&
                        !p.Deleted
                        select p;
            var products = query.ToList();
            return products;
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void DeleteProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.Deleted = true;
            //delete product
            UpdateProduct(product);
        }

        /// <summary>
        /// Gets all products displayed on the home page
        /// </summary>
        /// <returns>Product collection</returns>
        public virtual IList<Product> GetAllProductsDisplayedOnHomePage()
        {
            var query = from p in _productRepository.Table
                        orderby p.DisplayOrder, p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage
                        select p;
            var products = query.ToList();
            return products;
        }
        
        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        public virtual Product GetProductById(int productId)
        {
            if (productId == 0)
                return null;
            
            string key = string.Format(PRODUCTS_BY_ID_KEY, productId);
            return _cacheManager.Get(key, () => { return _productRepository.GetById(productId); });
        }

        /// <summary>
        /// Get products by identifiers
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        public virtual IList<Product> GetProductsByIds(int[] productIds)
        {
            if (productIds == null || productIds.Length == 0)
                return new List<Product>();

            var query = from p in _productRepository.Table
                        where productIds.Contains(p.Id)
                        select p;
            var products = query.ToList();
            //sort by passed identifiers
            var sortedProducts = new List<Product>();
            foreach (int id in productIds)
            {
                var product = products.Find(x => x.Id == id);
                if (product != null)
                    sortedProducts.Add(product);
            }
            return sortedProducts;
        }

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //insert
            _productRepository.Insert(product);

            //clear cache
            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);
            
            //event notification
            _eventPublisher.EntityInserted(product);
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //update
            _productRepository.Update(product);

            //cache
            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(product);
        }

        /// <summary>
        /// Get (visible) product number in certain category
        /// </summary>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <returns>Product number</returns>
        public virtual int GetCategoryProductNumber(IList<int> categoryIds = null, int storeId = 0)
        {
            //validate "categoryIds" parameter
            if (categoryIds != null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            var query = _productRepository.Table;
            query = query.Where(p => !p.Deleted && p.Published && p.VisibleIndividually);

            //category filtering
            if (categoryIds != null && categoryIds.Count > 0)
            {
                query = from p in query
                        from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                        select p;
            }

            if (!_catalogSettings.IgnoreAcl)
            {
                //Access control list. Allowed customer roles
                var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                    .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

                query = from p in query
                        join acl in _aclRepository.Table
                        on new { c1 = p.Id, c2 = "Product" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into p_acl
                        from acl in p_acl.DefaultIfEmpty()
                        where !p.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                        select p;
            }

            if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
            {
                query = from p in query
                        join sm in _storeMappingRepository.Table
                        on new { c1 = p.Id, c2 = "Product" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into p_sm
                        from sm in p_sm.DefaultIfEmpty()
                        where !p.LimitedToStores || storeId == sm.StoreId
                        select p;
            }

            //only distinct products
            var result = query.Select(p => p.Id).Distinct().Count();
            return result;
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier; 0 to load all records</param>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products); 0 to load all records</param>
        /// <param name="productType">Product type; 0 to load all records</param>
        /// <param name="visibleIndividuallyOnly">A values indicating whether to load only products marked as "visible individually"; "false" to load all records; "true" to load "visible individually" only</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchSku">A value indicating whether to search by a specified "keyword" in product SKU</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="languageId">Language identifier (search for text searching)</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            int parentGroupedProductId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false)
        {
            IList<int> filterableSpecificationAttributeOptionIds = null;
            return SearchProducts(out filterableSpecificationAttributeOptionIds, false,
                pageIndex, pageSize, categoryIds, manufacturerId,
                storeId, vendorId, warehouseId,
                parentGroupedProductId, productType, visibleIndividuallyOnly, featuredProducts,
                priceMin, priceMax, productTagId, keywords, searchDescriptions, searchSku,
                searchProductTags, languageId, filteredSpecs, orderBy, showHidden);
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="filterableSpecificationAttributeOptionIds">The specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="loadFilterableSpecificationAttributeOptionIds">A value indicating whether we should load the specification attribute option identifiers applied to loaded products (all pages)</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="categoryIds">Category identifiers</param>
        /// <param name="manufacturerId">Manufacturer identifier; 0 to load all records</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="warehouseId">Warehouse identifier; 0 to load all records</param>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products); 0 to load all records</param>
        /// <param name="productType">Product type; 0 to load all records</param>
        /// <param name="visibleIndividuallyOnly">A values indicating whether to load only products marked as "visible individually"; "false" to load all records; "true" to load "visible individually" only</param>
        /// <param name="featuredProducts">A value indicating whether loaded products are marked as featured (relates only to categories and manufacturers). 0 to load featured products only, 1 to load not featured products only, null to load all products</param>
        /// <param name="priceMin">Minimum price; null to load all records</param>
        /// <param name="priceMax">Maximum price; null to load all records</param>
        /// <param name="productTagId">Product tag identifier; 0 to load all records</param>
        /// <param name="keywords">Keywords</param>
        /// <param name="searchDescriptions">A value indicating whether to search by a specified "keyword" in product descriptions</param>
        /// <param name="searchSku">A value indicating whether to search by a specified "keyword" in product SKU</param>
        /// <param name="searchProductTags">A value indicating whether to search by a specified "keyword" in product tags</param>
        /// <param name="languageId">Language identifier (search for text searching)</param>
        /// <param name="filteredSpecs">Filtered product specification identifiers</param>
        /// <param name="orderBy">Order by</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual IPagedList<Product> SearchProducts(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = 2147483647,  //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            int parentGroupedProductId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false)
        {
            filterableSpecificationAttributeOptionIds = new List<int>();

            //search by keyword
            bool searchLocalizedValue = false;
            if (languageId > 0)
            {
                if (showHidden)
                {
                    searchLocalizedValue = true;
                }
                else
                {
                    //ensure that we have at least two published languages
                    var totalPublishedLanguages = _languageService.GetAllLanguages(storeId: _storeContext.CurrentStore.Id).Count;
                    searchLocalizedValue = totalPublishedLanguages >= 2;
                }
            }

            //validate "categoryIds" parameter
            if (categoryIds !=null && categoryIds.Contains(0))
                categoryIds.Remove(0);

            //Access control list. Allowed customer roles
            var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

            if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            {
                //stored procedures are enabled and supported by the database. 
                //It's much faster than the LINQ implementation below 

                #region Use stored procedure
                
                //pass category identifiers as comma-delimited string
                string commaSeparatedCategoryIds = "";
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


                //pass customer role identifiers as comma-delimited string
                string commaSeparatedAllowedCustomerRoleIds = "";
                for (int i = 0; i < allowedCustomerRolesIds.Count; i++)
                {
                    commaSeparatedAllowedCustomerRoleIds += allowedCustomerRolesIds[i].ToString();
                    if (i != allowedCustomerRolesIds.Count - 1)
                    {
                        commaSeparatedAllowedCustomerRoleIds += ",";
                    }
                }


                //pass specification identifiers as comma-delimited string
                string commaSeparatedSpecIds = "";
                if (filteredSpecs != null)
                {
                    ((List<int>)filteredSpecs).Sort();
                    for (int i = 0; i < filteredSpecs.Count; i++)
                    {
                        commaSeparatedSpecIds += filteredSpecs[i].ToString();
                        if (i != filteredSpecs.Count - 1)
                        {
                            commaSeparatedSpecIds += ",";
                        }
                    }
                }

                //some databases don't support int.MaxValue
                if (pageSize == int.MaxValue)
                    pageSize = int.MaxValue - 1;
                
                //prepare parameters
                var pCategoryIds = _dataProvider.GetParameter();
                pCategoryIds.ParameterName = "CategoryIds";
                pCategoryIds.Value = commaSeparatedCategoryIds != null ? (object)commaSeparatedCategoryIds : DBNull.Value;
                pCategoryIds.DbType = DbType.String;
                
                var pManufacturerId = _dataProvider.GetParameter();
                pManufacturerId.ParameterName = "ManufacturerId";
                pManufacturerId.Value = manufacturerId;
                pManufacturerId.DbType = DbType.Int32;

                var pStoreId = _dataProvider.GetParameter();
                pStoreId.ParameterName = "StoreId";
                pStoreId.Value = !_catalogSettings.IgnoreStoreLimitations ? storeId : 0;
                pStoreId.DbType = DbType.Int32;

                var pVendorId = _dataProvider.GetParameter();
                pVendorId.ParameterName = "VendorId";
                pVendorId.Value = vendorId;
                pVendorId.DbType = DbType.Int32;

                var pWarehouseId = _dataProvider.GetParameter();
                pWarehouseId.ParameterName = "WarehouseId";
                pWarehouseId.Value = warehouseId;
                pWarehouseId.DbType = DbType.Int32;

                var pParentGroupedProductId = _dataProvider.GetParameter();
                pParentGroupedProductId.ParameterName = "ParentGroupedProductId";
                pParentGroupedProductId.Value = parentGroupedProductId;
                pParentGroupedProductId.DbType = DbType.Int32;

                var pProductTypeId = _dataProvider.GetParameter();
                pProductTypeId.ParameterName = "ProductTypeId";
                pProductTypeId.Value = productType.HasValue ? (object)productType.Value : DBNull.Value;
                pProductTypeId.DbType = DbType.Int32;

                var pVisibleIndividuallyOnly = _dataProvider.GetParameter();
                pVisibleIndividuallyOnly.ParameterName = "VisibleIndividuallyOnly";
                pVisibleIndividuallyOnly.Value = visibleIndividuallyOnly;
                pVisibleIndividuallyOnly.DbType = DbType.Int32;

                var pProductTagId = _dataProvider.GetParameter();
                pProductTagId.ParameterName = "ProductTagId";
                pProductTagId.Value = productTagId;
                pProductTagId.DbType = DbType.Int32;

                var pFeaturedProducts = _dataProvider.GetParameter();
                pFeaturedProducts.ParameterName = "FeaturedProducts";
                pFeaturedProducts.Value = featuredProducts.HasValue ? (object)featuredProducts.Value : DBNull.Value;
                pFeaturedProducts.DbType = DbType.Boolean;

                var pPriceMin = _dataProvider.GetParameter();
                pPriceMin.ParameterName = "PriceMin";
                pPriceMin.Value = priceMin.HasValue ? (object)priceMin.Value : DBNull.Value;
                pPriceMin.DbType = DbType.Decimal;
                
                var pPriceMax = _dataProvider.GetParameter();
                pPriceMax.ParameterName = "PriceMax";
                pPriceMax.Value = priceMax.HasValue ? (object)priceMax.Value : DBNull.Value;
                pPriceMax.DbType = DbType.Decimal;

                var pKeywords = _dataProvider.GetParameter();
                pKeywords.ParameterName = "Keywords";
                pKeywords.Value = keywords != null ? (object)keywords : DBNull.Value;
                pKeywords.DbType = DbType.String;

                var pSearchDescriptions = _dataProvider.GetParameter();
                pSearchDescriptions.ParameterName = "SearchDescriptions";
                pSearchDescriptions.Value = searchDescriptions;
                pSearchDescriptions.DbType = DbType.Boolean;

                var pSearchSku = _dataProvider.GetParameter();
                pSearchSku.ParameterName = "SearchSku";
                pSearchSku.Value = searchSku;
                pSearchSku.DbType = DbType.Boolean;

                var pSearchProductTags = _dataProvider.GetParameter();
                pSearchProductTags.ParameterName = "SearchProductTags";
                pSearchProductTags.Value = searchProductTags;
                pSearchProductTags.DbType = DbType.Boolean;

                var pUseFullTextSearch = _dataProvider.GetParameter();
                pUseFullTextSearch.ParameterName = "UseFullTextSearch";
                pUseFullTextSearch.Value = _commonSettings.UseFullTextSearch;
                pUseFullTextSearch.DbType = DbType.Boolean;

                var pFullTextMode = _dataProvider.GetParameter();
                pFullTextMode.ParameterName = "FullTextMode";
                pFullTextMode.Value = (int)_commonSettings.FullTextMode;
                pFullTextMode.DbType = DbType.Int32;

                var pFilteredSpecs = _dataProvider.GetParameter();
                pFilteredSpecs.ParameterName = "FilteredSpecs";
                pFilteredSpecs.Value = commaSeparatedSpecIds != null ? (object)commaSeparatedSpecIds : DBNull.Value;
                pFilteredSpecs.DbType = DbType.String;

                var pLanguageId = _dataProvider.GetParameter();
                pLanguageId.ParameterName = "LanguageId";
                pLanguageId.Value = searchLocalizedValue ? languageId : 0;
                pLanguageId.DbType = DbType.Int32;

                var pOrderBy = _dataProvider.GetParameter();
                pOrderBy.ParameterName = "OrderBy";
                pOrderBy.Value = (int)orderBy;
                pOrderBy.DbType = DbType.Int32;

                var pAllowedCustomerRoleIds = _dataProvider.GetParameter();
                pAllowedCustomerRoleIds.ParameterName = "AllowedCustomerRoleIds";
                pAllowedCustomerRoleIds.Value = commaSeparatedAllowedCustomerRoleIds;
                pAllowedCustomerRoleIds.DbType = DbType.String;

                var pPageIndex = _dataProvider.GetParameter();
                pPageIndex.ParameterName = "PageIndex";
                pPageIndex.Value = pageIndex;
                pPageIndex.DbType = DbType.Int32;

                var pPageSize = _dataProvider.GetParameter();
                pPageSize.ParameterName = "PageSize";
                pPageSize.Value = pageSize;
                pPageSize.DbType = DbType.Int32;

                var pShowHidden = _dataProvider.GetParameter();
                pShowHidden.ParameterName = "ShowHidden";
                pShowHidden.Value = showHidden;
                pShowHidden.DbType = DbType.Boolean;
                
                var pLoadFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
                pLoadFilterableSpecificationAttributeOptionIds.ParameterName = "LoadFilterableSpecificationAttributeOptionIds";
                pLoadFilterableSpecificationAttributeOptionIds.Value = loadFilterableSpecificationAttributeOptionIds;
                pLoadFilterableSpecificationAttributeOptionIds.DbType = DbType.Boolean;
                
                var pFilterableSpecificationAttributeOptionIds = _dataProvider.GetParameter();
                pFilterableSpecificationAttributeOptionIds.ParameterName = "FilterableSpecificationAttributeOptionIds";
                pFilterableSpecificationAttributeOptionIds.Direction = ParameterDirection.Output;
                pFilterableSpecificationAttributeOptionIds.Size = int.MaxValue - 1;
                pFilterableSpecificationAttributeOptionIds.DbType = DbType.String;

                var pTotalRecords = _dataProvider.GetParameter();
                pTotalRecords.ParameterName = "TotalRecords";
                pTotalRecords.Direction = ParameterDirection.Output;
                pTotalRecords.DbType = DbType.Int32;

                //invoke stored procedure
                var products = _dbContext.ExecuteStoredProcedureList<Product>(
                    "ProductLoadAllPaged",
                    pCategoryIds,
                    pManufacturerId,
                    pStoreId,
                    pVendorId,
                    pWarehouseId,
                    pParentGroupedProductId,
                    pProductTypeId,
                    pVisibleIndividuallyOnly,
                    pProductTagId,
                    pFeaturedProducts,
                    pPriceMin,
                    pPriceMax,
                    pKeywords,
                    pSearchDescriptions,
                    pSearchSku,
                    pSearchProductTags,
                    pUseFullTextSearch,
                    pFullTextMode,
                    pFilteredSpecs,
                    pLanguageId,
                    pOrderBy,
                    pAllowedCustomerRoleIds,
                    pPageIndex,
                    pPageSize,
                    pShowHidden,
                    pLoadFilterableSpecificationAttributeOptionIds,
                    pFilterableSpecificationAttributeOptionIds,
                    pTotalRecords);
                //get filterable specification attribute option identifier
                string filterableSpecificationAttributeOptionIdsStr = (pFilterableSpecificationAttributeOptionIds.Value != DBNull.Value) ? (string)pFilterableSpecificationAttributeOptionIds.Value : "";
                if (loadFilterableSpecificationAttributeOptionIds &&
                    !string.IsNullOrWhiteSpace(filterableSpecificationAttributeOptionIdsStr))
                {
                     filterableSpecificationAttributeOptionIds = filterableSpecificationAttributeOptionIdsStr
                        .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => Convert.ToInt32(x.Trim()))
                        .ToList();
                }
                //return products
                int totalRecords = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
                return new PagedList<Product>(products, pageIndex, pageSize, totalRecords);

                #endregion
            }
            else
            {
                //stored procedures aren't supported. Use LINQ

                #region Search products

                //products
                var query = _productRepository.Table;
                query = query.Where(p => !p.Deleted);
                if (!showHidden)
                {
                    query = query.Where(p => p.Published);
                }
                if (parentGroupedProductId > 0)
                {
                    query = query.Where(p => p.ParentGroupedProductId == parentGroupedProductId);
                }
                if (visibleIndividuallyOnly)
                {
                    query = query.Where(p => p.VisibleIndividually);
                }
                if (productType.HasValue)
                {
                    int productTypeId = (int) productType.Value;
                    query = query.Where(p => p.ProductTypeId == productTypeId);
                }

                //The function 'CurrentUtcDateTime' is not supported by SQL Server Compact. 
                //That's why we pass the date value
                var nowUtc = DateTime.UtcNow;
                if (priceMin.HasValue)
                {
                    //min price
                    query = query.Where(p =>
                                        //special price (specified price and valid date range)
                                        ((p.SpecialPrice.HasValue &&
                                          ((!p.SpecialPriceStartDateTimeUtc.HasValue ||
                                            p.SpecialPriceStartDateTimeUtc.Value < nowUtc) &&
                                           (!p.SpecialPriceEndDateTimeUtc.HasValue ||
                                            p.SpecialPriceEndDateTimeUtc.Value > nowUtc))) &&
                                         (p.SpecialPrice >= priceMin.Value))
                                        ||
                                        //regular price (price isn't specified or date range isn't valid)
                                        ((!p.SpecialPrice.HasValue ||
                                          ((p.SpecialPriceStartDateTimeUtc.HasValue &&
                                            p.SpecialPriceStartDateTimeUtc.Value > nowUtc) ||
                                           (p.SpecialPriceEndDateTimeUtc.HasValue &&
                                            p.SpecialPriceEndDateTimeUtc.Value < nowUtc))) &&
                                         (p.Price >= priceMin.Value)));
                }
                if (priceMax.HasValue)
                {
                    //max price
                    query = query.Where(p =>
                                        //special price (specified price and valid date range)
                                        ((p.SpecialPrice.HasValue &&
                                          ((!p.SpecialPriceStartDateTimeUtc.HasValue ||
                                            p.SpecialPriceStartDateTimeUtc.Value < nowUtc) &&
                                           (!p.SpecialPriceEndDateTimeUtc.HasValue ||
                                            p.SpecialPriceEndDateTimeUtc.Value > nowUtc))) &&
                                         (p.SpecialPrice <= priceMax.Value))
                                        ||
                                        //regular price (price isn't specified or date range isn't valid)
                                        ((!p.SpecialPrice.HasValue ||
                                          ((p.SpecialPriceStartDateTimeUtc.HasValue &&
                                            p.SpecialPriceStartDateTimeUtc.Value > nowUtc) ||
                                           (p.SpecialPriceEndDateTimeUtc.HasValue &&
                                            p.SpecialPriceEndDateTimeUtc.Value < nowUtc))) &&
                                         (p.Price <= priceMax.Value)));
                }
                if (!showHidden)
                {
                    //available dates
                    query = query.Where(p =>
                        (!p.AvailableStartDateTimeUtc.HasValue || p.AvailableStartDateTimeUtc.Value < nowUtc) &&
                        (!p.AvailableEndDateTimeUtc.HasValue || p.AvailableEndDateTimeUtc.Value > nowUtc));
                }

                //searching by keyword
                if (!String.IsNullOrWhiteSpace(keywords))
                {
                    query = from p in query
                            join lp in _localizedPropertyRepository.Table on p.Id equals lp.EntityId into p_lp
                            from lp in p_lp.DefaultIfEmpty()
                            from pt in p.ProductTags.DefaultIfEmpty()
                            where (p.Name.Contains(keywords)) ||
                                  (searchDescriptions && p.ShortDescription.Contains(keywords)) ||
                                  (searchDescriptions && p.FullDescription.Contains(keywords)) ||
                                  (searchProductTags && pt.Name.Contains(keywords)) ||
                                  //localized values
                                  (searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "Name" && lp.LocaleValue.Contains(keywords)) ||
                                  (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "ShortDescription" && lp.LocaleValue.Contains(keywords)) ||
                                  (searchDescriptions && searchLocalizedValue && lp.LanguageId == languageId && lp.LocaleKeyGroup == "Product" && lp.LocaleKey == "FullDescription" && lp.LocaleValue.Contains(keywords))
                                  //UNDONE search localized values in associated product tags
                            select p;
                }

                if (!showHidden && !_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    query = from p in query
                            join acl in _aclRepository.Table
                            on new { c1 = p.Id, c2 = "Product" } equals new { c1 = acl.EntityId, c2 = acl.EntityName } into p_acl
                            from acl in p_acl.DefaultIfEmpty()
                            where !p.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select p;
                }

                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    query = from p in query
                            join sm in _storeMappingRepository.Table
                            on new { c1 = p.Id, c2 = "Product" } equals new { c1 = sm.EntityId, c2 = sm.EntityName } into p_sm
                            from sm in p_sm.DefaultIfEmpty()
                            where !p.LimitedToStores || storeId == sm.StoreId
                            select p;
                }
                
                //search by specs
                if (filteredSpecs != null && filteredSpecs.Count > 0)
                {
                    query = from p in query
                            where !filteredSpecs
                                       .Except(
                                           p.ProductSpecificationAttributes.Where(psa => psa.AllowFiltering).Select(
                                               psa => psa.SpecificationAttributeOptionId))
                                       .Any()
                            select p;
                }

                //category filtering
                if (categoryIds != null && categoryIds.Count > 0)
                {
                    query = from p in query
                            from pc in p.ProductCategories.Where(pc => categoryIds.Contains(pc.CategoryId))
                            where (!featuredProducts.HasValue || featuredProducts.Value == pc.IsFeaturedProduct)
                            select p;
                }

                //manufacturer filtering
                if (manufacturerId > 0)
                {
                    query = from p in query
                            from pm in p.ProductManufacturers.Where(pm => pm.ManufacturerId == manufacturerId)
                            where (!featuredProducts.HasValue || featuredProducts.Value == pm.IsFeaturedProduct)
                            select p;
                }

                //vendor filtering
                if (vendorId > 0)
                {
                    query = query.Where(p => p.VendorId == vendorId);
                }

                //warehouse filtering
                if (warehouseId > 0)
                {
                    query = query.Where(p => p.WarehouseId == warehouseId);
                }

                //related products filtering
                //if (relatedToProductId > 0)
                //{
                //    query = from p in query
                //            join rp in _relatedProductRepository.Table on p.Id equals rp.ProductId2
                //            where (relatedToProductId == rp.ProductId1)
                //            select p;
                //}

                //tag filtering
                if (productTagId > 0)
                {
                    query = from p in query
                            from pt in p.ProductTags.Where(pt => pt.Id == productTagId)
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

                //sort products
                if (orderBy == ProductSortingEnum.Position && categoryIds != null && categoryIds.Count > 0)
                {
                    //category position
                    var firstCategoryId = categoryIds[0];
                    query = query.OrderBy(p => p.ProductCategories.FirstOrDefault(pc => pc.CategoryId == firstCategoryId).DisplayOrder);
                }
                else if (orderBy == ProductSortingEnum.Position && manufacturerId > 0)
                {
                    //manufacturer position
                    query = 
                        query.OrderBy(p => p.ProductManufacturers.FirstOrDefault(pm => pm.ManufacturerId == manufacturerId).DisplayOrder);
                }
                else if (orderBy == ProductSortingEnum.Position && parentGroupedProductId > 0)
                {
                    //parent grouped product specified (sort associated products)
                    query = query.OrderBy(p => p.DisplayOrder);
                }
                else if (orderBy == ProductSortingEnum.Position)
                {
                    //otherwise sort by name
                    query = query.OrderBy(p => p.Name);
                }
                else if (orderBy == ProductSortingEnum.NameAsc)
                {
                    //Name: A to Z
                    query = query.OrderBy(p => p.Name);
                }
                else if (orderBy == ProductSortingEnum.NameDesc)
                {
                    //Name: Z to A
                    query = query.OrderByDescending(p => p.Name);
                }
                else if (orderBy == ProductSortingEnum.PriceAsc)
                {
                    //Price: Low to High
                    query = query.OrderBy(p => p.Price);
                }
                else if (orderBy == ProductSortingEnum.PriceDesc)
                {
                    //Price: High to Low
                    query = query.OrderByDescending(p => p.Price);
                }
                else if (orderBy == ProductSortingEnum.CreatedOn)
                {
                    //creation date
                    query = query.OrderByDescending(p => p.CreatedOnUtc);
                }
                else
                {
                    //actually this code is not reachable
                    query = query.OrderBy(p => p.Name);
                }

                var products = new PagedList<Product>(query, pageIndex, pageSize);

                //get filterable specification attribute option identifier
                if (loadFilterableSpecificationAttributeOptionIds)
                {
                    var querySpecs = from p in query
                                     join psa in _productSpecificationAttributeRepository.Table on p.Id equals psa.ProductId
                                     where psa.AllowFiltering
                                     select psa.SpecificationAttributeOptionId;
                    //only distinct attributes
                    filterableSpecificationAttributeOptionIds = querySpecs
                        .Distinct()
                        .ToList();
                }

                //return products
                return products;

                #endregion
            }
        }

        /// <summary>
        /// Gets associated products
        /// </summary>
        /// <param name="parentGroupedProductId">Parent product identifier (used with grouped products)</param>
        /// <param name="storeId">Store identifier; 0 to load all records</param>
         /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Products</returns>
        public virtual IList<Product> GetAssociatedProducts(int parentGroupedProductId,
            int storeId = 0, bool showHidden = false)
        {
            var query = _productRepository.Table;
            query = query.Where(x => x.ParentGroupedProductId == parentGroupedProductId);
            if (!showHidden)
            {
                query = query.Where(x => x.Published);
            }
            if (!showHidden)
            {
                //The function 'CurrentUtcDateTime' is not supported by SQL Server Compact. 
                //That's why we pass the date value
                var nowUtc = DateTime.UtcNow;
                //available dates
                query = query.Where(p =>
                    (!p.AvailableStartDateTimeUtc.HasValue || p.AvailableStartDateTimeUtc.Value < nowUtc) &&
                    (!p.AvailableEndDateTimeUtc.HasValue || p.AvailableEndDateTimeUtc.Value > nowUtc));
            }
            query = query.Where(x => !x.Deleted);
            query = query.OrderBy(x => x.DisplayOrder);

            var products = query.ToList();

            //ACLmapping
            if (!showHidden)
            {
                products = products.Where(x => _aclService.Authorize(x)).ToList();
            }
            //Store mapping
            if (!showHidden && storeId > 0)
            {
                products = products.Where(x => _storeMappingService.Authorize(x, storeId)).ToList();
            }

            return products;
        }
        
        /// <summary>
        /// Update product review totals
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateProductReviewTotals(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            int approvedRatingSum = 0;
            int notApprovedRatingSum = 0; 
            int approvedTotalReviews = 0;
            int notApprovedTotalReviews = 0;
            var reviews = product.ProductReviews;
            foreach (var pr in reviews)
            {
                if (pr.IsApproved)
                {
                    approvedRatingSum += pr.Rating;
                    approvedTotalReviews ++;
                }
                else
                {
                    notApprovedRatingSum += pr.Rating;
                    notApprovedTotalReviews++;
                }
            }

            product.ApprovedRatingSum = approvedRatingSum;
            product.NotApprovedRatingSum = notApprovedRatingSum;
            product.ApprovedTotalReviews = approvedTotalReviews;
            product.NotApprovedTotalReviews = notApprovedTotalReviews;
            UpdateProduct(product);
        }

        /// <summary>
        /// Get low stock products
        /// </summary>
        /// <param name="vendorId">Vendor identifier; 0 to load all records</param>
        /// <param name="products">Low stock products</param>
        /// <param name="combinations">Low stock  attribute combinations</param>
        public virtual void GetLowStockProducts(int vendorId,
            out IList<Product> products, 
            out IList<ProductVariantAttributeCombination> combinations)
        {
            //Track inventory for product
            var query1 = from p in _productRepository.Table
                         orderby p.MinStockQuantity
                         where !p.Deleted &&
                         p.ManageInventoryMethodId == (int)ManageInventoryMethod.ManageStock &&
                         p.MinStockQuantity >= p.StockQuantity &&
                         (vendorId == 0 || p.VendorId == vendorId)
                         select p;
            products = query1.ToList();

            //Track inventory for product by product attributes
            var query2 = from p in _productRepository.Table
                         from pvac in p.ProductVariantAttributeCombinations
                         where !p.Deleted &&
                         p.ManageInventoryMethodId == (int)ManageInventoryMethod.ManageStockByAttributes &&
                         pvac.StockQuantity <= 0 &&
                         (vendorId == 0 || p.VendorId == vendorId)
                         select pvac;
            combinations = query2.ToList();
        }
        
        /// <summary>
        /// Gets a product by SKU
        /// </summary>
        /// <param name="sku">SKU</param>
        /// <returns>Product</returns>
        public virtual Product GetProductBySku(string sku)
        {
            if (String.IsNullOrEmpty(sku))
                return null;

            sku = sku.Trim();

            var query = from p in _productRepository.Table
                        orderby p.Id
                        where !p.Deleted &&
                        p.Sku == sku
                        select p;
            var product = query.FirstOrDefault();
            return product;
        }
        
        /// <summary>
        /// Adjusts inventory
        /// </summary>
        /// <param name="product">Product</param>
        /// <param name="decrease">A value indicating whether to increase or descrease product stock quantity</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="attributesXml">Attributes in XML format</param>
        public virtual void AdjustInventory(Product product, bool decrease,
            int quantity, string attributesXml)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            var prevStockQuantity = product.StockQuantity;

            switch (product.ManageInventoryMethod)
            {
                case ManageInventoryMethod.DontManageStock:
                    {
                        //do nothing
                    }
                    break;
                case ManageInventoryMethod.ManageStock:
                    {
                        int newStockQuantity = 0;
                        if (decrease)
                            newStockQuantity = product.StockQuantity - quantity;
                        else
                            newStockQuantity = product.StockQuantity + quantity;

                        bool newPublished = product.Published;
                        bool newDisableBuyButton = product.DisableBuyButton;
                        bool newDisableWishlistButton = product.DisableWishlistButton;

                        //check if minimum quantity is reached
                        if (decrease)
                        {
                            if (product.MinStockQuantity >= newStockQuantity)
                            {
                                switch (product.LowStockActivity)
                                {
                                    case LowStockActivity.DisableBuyButton:
                                        newDisableBuyButton = true;
                                        newDisableWishlistButton = true;
                                        break;
                                    case LowStockActivity.Unpublish:
                                        newPublished = false;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        product.StockQuantity = newStockQuantity;
                        product.DisableBuyButton = newDisableBuyButton;
                        product.DisableWishlistButton = newDisableWishlistButton;
                        product.Published = newPublished;
                        UpdateProduct(product);

                        //send email notification
                        if (decrease && product.NotifyAdminForQuantityBelow > newStockQuantity)
                            _workflowMessageService.SendQuantityBelowStoreOwnerNotification(product, _localizationSettings.DefaultAdminLanguageId);
                    }
                    break;
                case ManageInventoryMethod.ManageStockByAttributes:
                    {
                        var combination = _productAttributeParser.FindProductVariantAttributeCombination(product, attributesXml);
                        if (combination != null)
                        {
                            int newStockQuantity = 0;
                            if (decrease)
                                newStockQuantity = combination.StockQuantity - quantity;
                            else
                                newStockQuantity = combination.StockQuantity + quantity;

                            combination.StockQuantity = newStockQuantity;
                            _productAttributeService.UpdateProductVariantAttributeCombination(combination);
                        }
                    }
                    break;
                default:
                    break;
            }


            //bundled products
            var pvaValues = _productAttributeParser.ParseProductVariantAttributeValues(attributesXml);
            foreach (var pvaValue in pvaValues)
            {
                if (pvaValue.AttributeValueType == AttributeValueType.AssociatedToProduct)
                {
                    //associated product (bundle)
                    var associatedProduct = GetProductById(pvaValue.AssociatedProductId);
                    if (associatedProduct != null)
                    {
                        var totalQty = quantity*pvaValue.Quantity;
                        AdjustInventory(associatedProduct, decrease, totalQty, "");
                    }
                }
            }

            //TODO send back in stock notifications?
            //if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
            //    product.BackorderMode == BackorderMode.NoBackorders &&
            //    product.AllowBackInStockSubscriptions &&
            //    product.StockQuantity > 0 &&
            //    prevStockQuantity <= 0 &&
            //    product.Published &&
            //    !product.Deleted)
            //{
            //    //_backInStockSubscriptionService.SendNotificationsToSubscribers(product);
            //}
        }
        
        /// <summary>
        /// Update HasTierPrices property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateHasTierPricesProperty(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            product.HasTierPrices = product.TierPrices.Count > 0;
            UpdateProduct(product);
        }

        /// <summary>
        /// Update HasDiscountsApplied property (used for performance optimization)
        /// </summary>
        /// <param name="product">Product</param>
        public virtual void UpdateHasDiscountsApplied(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("productVariant");

            product.HasDiscountsApplied = product.AppliedDiscounts.Count > 0;
            UpdateProduct(product);
        }

        #endregion

        #region Related products

        /// <summary>
        /// Deletes a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        public virtual void DeleteRelatedProduct(RelatedProduct relatedProduct)
        {
            if (relatedProduct == null)
                throw new ArgumentNullException("relatedProduct");

            _relatedProductRepository.Delete(relatedProduct);

            //event notification
            _eventPublisher.EntityDeleted(relatedProduct);
        }

        /// <summary>
        /// Gets a related product collection by product identifier
        /// </summary>
        /// <param name="productId1">The first product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Related product collection</returns>
        public virtual IList<RelatedProduct> GetRelatedProductsByProductId1(int productId1, bool showHidden = false)
        {
            var query = from rp in _relatedProductRepository.Table
                        join p in _productRepository.Table on rp.ProductId2 equals p.Id
                        where rp.ProductId1 == productId1 &&
                        !p.Deleted &&
                        (showHidden || p.Published)
                        orderby rp.DisplayOrder
                        select rp;
            var relatedProducts = query.ToList();

            return relatedProducts;
        }

        /// <summary>
        /// Gets a related product
        /// </summary>
        /// <param name="relatedProductId">Related product identifier</param>
        /// <returns>Related product</returns>
        public virtual RelatedProduct GetRelatedProductById(int relatedProductId)
        {
            if (relatedProductId == 0)
                return null;
            
            return _relatedProductRepository.GetById(relatedProductId);
        }

        /// <summary>
        /// Inserts a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        public virtual void InsertRelatedProduct(RelatedProduct relatedProduct)
        {
            if (relatedProduct == null)
                throw new ArgumentNullException("relatedProduct");

            _relatedProductRepository.Insert(relatedProduct);

            //event notification
            _eventPublisher.EntityInserted(relatedProduct);
        }

        /// <summary>
        /// Updates a related product
        /// </summary>
        /// <param name="relatedProduct">Related product</param>
        public virtual void UpdateRelatedProduct(RelatedProduct relatedProduct)
        {
            if (relatedProduct == null)
                throw new ArgumentNullException("relatedProduct");

            _relatedProductRepository.Update(relatedProduct);

            //event notification
            _eventPublisher.EntityUpdated(relatedProduct);
        }

        #endregion

        #region Cross-sell products

        /// <summary>
        /// Deletes a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell identifier</param>
        public virtual void DeleteCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            if (crossSellProduct == null)
                throw new ArgumentNullException("crossSellProduct");

            _crossSellProductRepository.Delete(crossSellProduct);

            //event notification
            _eventPublisher.EntityDeleted(crossSellProduct);
        }

        /// <summary>
        /// Gets a cross-sell product collection by product identifier
        /// </summary>
        /// <param name="productId1">The first product identifier</param>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Cross-sell product collection</returns>
        public virtual IList<CrossSellProduct> GetCrossSellProductsByProductId1(int productId1, bool showHidden = false)
        {
            var query = from csp in _crossSellProductRepository.Table
                        join p in _productRepository.Table on csp.ProductId2 equals p.Id
                        where csp.ProductId1 == productId1 &&
                        !p.Deleted &&
                        (showHidden || p.Published)
                        orderby csp.Id
                        select csp;
            var crossSellProducts = query.ToList();
            return crossSellProducts;
        }

        /// <summary>
        /// Gets a cross-sell product
        /// </summary>
        /// <param name="crossSellProductId">Cross-sell product identifier</param>
        /// <returns>Cross-sell product</returns>
        public virtual CrossSellProduct GetCrossSellProductById(int crossSellProductId)
        {
            if (crossSellProductId == 0)
                return null;

            return _crossSellProductRepository.GetById(crossSellProductId);
        }

        /// <summary>
        /// Inserts a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell product</param>
        public virtual void InsertCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            if (crossSellProduct == null)
                throw new ArgumentNullException("crossSellProduct");

            _crossSellProductRepository.Insert(crossSellProduct);

            //event notification
            _eventPublisher.EntityInserted(crossSellProduct);
        }

        /// <summary>
        /// Updates a cross-sell product
        /// </summary>
        /// <param name="crossSellProduct">Cross-sell product</param>
        public virtual void UpdateCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            if (crossSellProduct == null)
                throw new ArgumentNullException("crossSellProduct");

            _crossSellProductRepository.Update(crossSellProduct);

            //event notification
            _eventPublisher.EntityUpdated(crossSellProduct);
        }

        /// <summary>
        /// Gets a cross-sells
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="numberOfProducts">Number of products to return</param>
        /// <returns>Cross-sells</returns>
        public virtual IList<Product> GetCrosssellProductsByShoppingCart(IList<ShoppingCartItem> cart, int numberOfProducts)
        {
            var result = new List<Product>();

            if (numberOfProducts == 0)
                return result;

            if (cart == null || cart.Count == 0)
                return result;

            var cartProductIds = new List<int>();
            foreach (var sci in cart)
            {
                int prodId = sci.ProductId;
                if (!cartProductIds.Contains(prodId))
                    cartProductIds.Add(prodId);
            }

            foreach (var sci in cart)
            {
                var crossSells = GetCrossSellProductsByProductId1(sci.ProductId);
                foreach (var crossSell in crossSells)
                {
                    //validate that this product is not added to result yet
                    //validate that this product is not in the cart
                    if (result.Find(p => p.Id == crossSell.ProductId2) == null &&
                        !cartProductIds.Contains(crossSell.ProductId2))
                    {
                        var productToAdd = GetProductById(crossSell.ProductId2);
                        //validate product
                        if (productToAdd == null || productToAdd.Deleted || !productToAdd.Published)
                            continue;

                        //add a product to result
                        result.Add(productToAdd);
                        if (result.Count >= numberOfProducts)
                            return result;
                    }
                }
            }
            return result;
        }
        #endregion
        
        #region Tier prices
        
        /// <summary>
        /// Deletes a tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        public virtual void DeleteTierPrice(TierPrice tierPrice)
        {
            if (tierPrice == null)
                throw new ArgumentNullException("tierPrice");

            _tierPriceRepository.Delete(tierPrice);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(tierPrice);
        }

        /// <summary>
        /// Gets a tier price
        /// </summary>
        /// <param name="tierPriceId">Tier price identifier</param>
        /// <returns>Tier price</returns>
        public virtual TierPrice GetTierPriceById(int tierPriceId)
        {
            if (tierPriceId == 0)
                return null;
            
            return _tierPriceRepository.GetById(tierPriceId);
        }

        /// <summary>
        /// Inserts a tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        public virtual void InsertTierPrice(TierPrice tierPrice)
        {
            if (tierPrice == null)
                throw new ArgumentNullException("tierPrice");

            _tierPriceRepository.Insert(tierPrice);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(tierPrice);
        }

        /// <summary>
        /// Updates the tier price
        /// </summary>
        /// <param name="tierPrice">Tier price</param>
        public virtual void UpdateTierPrice(TierPrice tierPrice)
        {
            if (tierPrice == null)
                throw new ArgumentNullException("tierPrice");

            _tierPriceRepository.Update(tierPrice);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(tierPrice);
        }

        #endregion

        #region Product pictures

        /// <summary>
        /// Deletes a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void DeleteProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException("productPicture");

            _productPictureRepository.Delete(productPicture);

            //event notification
            _eventPublisher.EntityDeleted(productPicture);
        }

        /// <summary>
        /// Gets a product pictures by product identifier
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <returns>Product pictures</returns>
        public virtual IList<ProductPicture> GetProductPicturesByProductId(int productId)
        {
            var query = from pp in _productPictureRepository.Table
                        where pp.ProductId == productId
                        orderby pp.DisplayOrder
                        select pp;
            var productPictures = query.ToList();
            return productPictures;
        }

        /// <summary>
        /// Gets a product picture
        /// </summary>
        /// <param name="productPictureId">Product picture identifier</param>
        /// <returns>Product picture</returns>
        public virtual ProductPicture GetProductPictureById(int productPictureId)
        {
            if (productPictureId == 0)
                return null;

            return _productPictureRepository.GetById(productPictureId);
        }

        /// <summary>
        /// Inserts a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void InsertProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException("productPicture");

            _productPictureRepository.Insert(productPicture);

            //event notification
            _eventPublisher.EntityInserted(productPicture);
        }

        /// <summary>
        /// Updates a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        public virtual void UpdateProductPicture(ProductPicture productPicture)
        {
            if (productPicture == null)
                throw new ArgumentNullException("productPicture");

            _productPictureRepository.Update(productPicture);

            //event notification
            _eventPublisher.EntityUpdated(productPicture);
        }

        #endregion

        #region Product reviews
        
        /// <summary>
        /// Gets all product reviews
        /// </summary>
        /// <param name="customerId">Customer identifier; 0 to load all records</param>
        /// <param name="approved">A value indicating whether to content is approved; null to load all records</param> 
        /// <param name="fromUtc">Item creation from; null to load all records</param>
        /// <param name="toUtc">Item item creation to; null to load all records</param>
        /// <param name="message">Search title or review text; null to load all records</param>
        /// <returns>Reviews</returns>
        public virtual IList<ProductReview> GetAllProductReviews(int customerId, bool? approved,
            DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = null)
        {
            var query = _productReviewRepository.Table;
            if (approved.HasValue)
                query = query.Where(c => c.IsApproved == approved);
            if (customerId > 0)
                query = query.Where(c => c.CustomerId == customerId);
            if (fromUtc.HasValue)
                query = query.Where(c => fromUtc.Value <= c.CreatedOnUtc);
            if (toUtc.HasValue)
                query = query.Where(c => toUtc.Value >= c.CreatedOnUtc);
            if (!String.IsNullOrEmpty(message))
                query = query.Where(c => c.Title.Contains(message) || c.ReviewText.Contains(message));

            query = query.OrderBy(c => c.CreatedOnUtc);
            var content = query.ToList();
            return content;
        }

        /// <summary>
        /// Gets product review
        /// </summary>
        /// <param name="productReviewId">Product review identifier</param>
        /// <returns>Product review</returns>
        public virtual ProductReview GetProductReviewById(int productReviewId)
        {
            if (productReviewId == 0)
                return null;

            return _productReviewRepository.GetById(productReviewId);
        }

        /// <summary>
        /// Deletes a product review
        /// </summary>
        /// <param name="productReview">Product review</param>
        public virtual void DeleteProductReview(ProductReview productReview)
        {
            if (productReview == null)
                throw new ArgumentNullException("productReview");

            _productReviewRepository.Delete(productReview);

            _cacheManager.RemoveByPattern(PRODUCTS_PATTERN_KEY);
        }

        #endregion

        #region Bundles

        public virtual void DeleteBundle(Bundle bundle)
        {
            if (bundle == null)
                throw new ArgumentNullException("bundle");

            bundle.Deleted = true;
            UpdateBundle(bundle);
        }

        public virtual IList<Bundle> GetAllBundles(bool showHidden = false)
        {
            var query = from p in _bundleRepository.Table
                        orderby p.Name
                        where (showHidden || p.Published) &&
                        !p.Deleted
                        select p;
            var bundles = query.ToList();
            return bundles;
        }

        public virtual IList<Bundle> GetAllBundlesDisplayedOnHomePage()
        {
            var query = from p in _bundleRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage
                        select p;
            var bundles = query.ToList();
            return bundles;
        }

        public virtual Bundle GetBundleById(int bundleId)
        {
            if (bundleId == 0)
                return null;

            var bundle = _bundleRepository.GetById(bundleId);
            return bundle;
        }

        public virtual IList<Bundle> GetBundlesByIds(int[] bundleIds)
        {
            if (bundleIds == null || bundleIds.Length == 0)
                return new List<Bundle>();

            var query = from p in _bundleRepository.Table
                        where bundleIds.Contains(p.Id)
                        select p;
            var bundles = query.ToList();
            //sort by passed identifiers
            var sortedBundles = new List<Bundle>();
            foreach (int id in bundleIds)
            {
                var bundle = bundles.Find(x => x.Id == id);
                if (bundle != null)
                    sortedBundles.Add(bundle);
            }
            return sortedBundles;
        }

        public virtual void InsertBundle(Bundle bundle)
        {
            if (bundle == null)
                throw new ArgumentNullException("bundle");

            //insert
            _bundleRepository.Insert(bundle);

            //event notification
            _eventPublisher.EntityInserted(bundle);
        }

        public virtual void UpdateBundle(Bundle bundle)
        {
            if (bundle == null)
                throw new ArgumentNullException("bundle");

            //update
            _bundleRepository.Update(bundle);

            //event notification
            _eventPublisher.EntityUpdated(bundle);
        }

        public virtual IPagedList<Bundle> SearchBundles(string keywords,
            ProductSortingEnum orderBy, int pageIndex, int pageSize, bool showHidden = false)
        {
            //Access control list. Allowed customer roles
            var allowedCustomerRolesIds = _workContext.CurrentCustomer.CustomerRoles
                .Where(cr => cr.Active).Select(cr => cr.Id).ToList();

            //products
            var query = _bundleRepository.Table;
            query = query.Where(p => !p.Deleted);
            if (!showHidden)
            {
                query = query.Where(p => p.Published);
            }

            //searching by keyword
            if (!String.IsNullOrWhiteSpace(keywords))
            {
                query = query.Where(p => p.Name.Contains(keywords));
            }

            //only distinct products (group by ID)
            //if we use standard Distinct() method, then all fields will be compared (low performance)
            //it'll not work in SQL Server Compact when searching products by a keyword)
            query = from p in query
                    group p by p.Id
                        into pGroup
                        orderby pGroup.Key
                        select pGroup.FirstOrDefault();

            //sort products
            if (orderBy == ProductSortingEnum.Position)
            {
                //sort by name (there's no any position if category or manufactur is not specified)
                query = query.OrderBy(p => p.Name);
            }
            else if (orderBy == ProductSortingEnum.NameAsc)
            {
                //Name: A to Z
                query = query.OrderBy(p => p.Name);
            }
            else if (orderBy == ProductSortingEnum.NameDesc)
            {
                //Name: Z to A
                query = query.OrderByDescending(p => p.Name);
            }
            else if (orderBy == ProductSortingEnum.CreatedOn)
            {
                //creation date
                query = query.OrderByDescending(p => p.CreatedOnUtc);
            }
            else
            {
                //actually this code is not reachable
                query = query.OrderBy(p => p.Name);
            }

            var bundles = new PagedList<Bundle>(query, pageIndex, pageSize);

            //return bundles
            return bundles;
        }

        public virtual IList<Bundle> GetAllBundleProductsDisplayedOnHomePage()
        {
            var query = from p in _bundleRepository.Table
                        orderby p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage
                        select p;
            var bundles = query.ToList();
            return bundles;
        }

        public virtual void UpdateBundleReviewTotals(Bundle bundle)
        {
            if (bundle == null)
                throw new ArgumentNullException("bundle");

            int approvedRatingSum = 0;
            int notApprovedRatingSum = 0;
            int approvedTotalReviews = 0;
            int notApprovedTotalReviews = 0;
            var reviews = bundle.BundleReviews;
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

            bundle.ApprovedRatingSum = approvedRatingSum;
            bundle.NotApprovedRatingSum = notApprovedRatingSum;
            bundle.ApprovedTotalReviews = approvedTotalReviews;
            bundle.NotApprovedTotalReviews = notApprovedTotalReviews;
            UpdateBundle(bundle);
        }

        #endregion

        #region Bundle pictures

        public virtual void DeleteBundlePicture(BundlePicture bundlePicture)
        {
            if (bundlePicture == null)
                throw new ArgumentNullException("bundlePicture");

            _bundlePictureRepository.Delete(bundlePicture);

            //event notification
            _eventPublisher.EntityDeleted(bundlePicture);
        }

        public virtual IList<BundlePicture> GetBundlePicturesByBundleId(int bundleId)
        {
            var query = from pp in _bundlePictureRepository.Table
                        where pp.BundleId == bundleId
                        orderby pp.DisplayOrder
                        select pp;
            var bundlePictures = query.ToList();
            return bundlePictures;
        }

        public virtual BundlePicture GetBundlePictureById(int bundlePictureId)
        {
            if (bundlePictureId == 0)
                return null;

            var pp = _bundlePictureRepository.GetById(bundlePictureId);
            return pp;
        }

        public virtual void InsertBundlePicture(BundlePicture bundlePicture)
        {
            if (bundlePicture == null)
                throw new ArgumentNullException("bundlePicture");

            _bundlePictureRepository.Insert(bundlePicture);

            //event notification
            _eventPublisher.EntityInserted(bundlePicture);
        }

        public virtual void UpdateBundlePicture(BundlePicture bundlePicture)
        {
            if (bundlePicture == null)
                throw new ArgumentNullException("bundlePicture");

            _bundlePictureRepository.Update(bundlePicture);

            //event notification
            _eventPublisher.EntityUpdated(bundlePicture);
        }

        #endregion

        #region Bundle Product Mapping Methods

        public virtual void DeleteBundleProduct(BundleProduct bundleProduct)
        {
            if (bundleProduct == null)
                throw new ArgumentNullException("productCategory");

            _bundleProductRepository.Delete(bundleProduct);
        }

        public virtual IPagedList<BundleProduct> GetBundleProductsByProductId(int productId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (productId == 0)
                return new PagedList<BundleProduct>(new List<BundleProduct>(), pageIndex, pageSize);

            var query = from pc in _bundleProductRepository.Table
                        join p in _bundleRepository.Table on pc.BundleId equals p.Id
                        where pc.ProductId == productId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProducts = new PagedList<BundleProduct>(query, pageIndex, pageSize);
            return bundleProducts;
        }

        public virtual IList<BundleProduct> GetBundleProductsByBundleId(int bundleId, bool showHidden = false)
        {
            if (bundleId == 0)
                return new List<BundleProduct>();

            var query = from pc in _bundleProductRepository.Table
                        join c in _productRepository.Table on pc.ProductId equals c.Id
                        where pc.BundleId == bundleId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProducts = query.ToList();
            return bundleProducts;
        }

        public virtual BundleProduct GetBundleProductById(int bundleProductId)
        {
            if (bundleProductId == 0)
                return null;

            return _bundleProductRepository.GetById(bundleProductId);
        }

        public virtual void InsertBundleProduct(BundleProduct bundleProduct)
        {
            if (bundleProduct == null)
                throw new ArgumentNullException("bundleProduct");

            _bundleProductRepository.Insert(bundleProduct);
        }

        public virtual void UpdateBundleProduct(BundleProduct bundleProduct)
        {
            if (bundleProduct == null)
                throw new ArgumentNullException("bundleProduct");

            _bundleProductRepository.Update(bundleProduct);
        }

        public virtual IPagedList<BundleProduct> GetBundleProductsByBundleId(int bundleId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (bundleId == 0)
                return new PagedList<BundleProduct>(new List<BundleProduct>(), pageIndex, pageSize);

            var query = from pc in _bundleProductRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where pc.BundleId == bundleId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProducts = new PagedList<BundleProduct>(query, pageIndex, pageSize);
            return bundleProducts;
        }

        #endregion

        #region Bundle Project Mapping Methods

        public virtual void DeleteBundleProject(BundleProject bundleProject)
        {
            if (bundleProject == null)
                throw new ArgumentNullException("projectCategory");

            _bundleProjectRepository.Delete(bundleProject);
        }

        public virtual IPagedList<BundleProject> GetBundleProjectsByProjectId(int projectId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (projectId == 0)
                return new PagedList<BundleProject>(new List<BundleProject>(), pageIndex, pageSize);

            var query = from pc in _bundleProjectRepository.Table
                        join p in _bundleRepository.Table on pc.BundleId equals p.Id
                        where pc.ProjectId == projectId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProjects = new PagedList<BundleProject>(query, pageIndex, pageSize);
            return bundleProjects;
        }

        public virtual IList<BundleProject> GetBundleProjectsByBundleId(int bundleId, bool showHidden = false)
        {
            if (bundleId == 0)
                return new List<BundleProject>();

            var query = from pc in _bundleProjectRepository.Table
                        join c in _projectRepository.Table on pc.ProjectId equals c.Id
                        where pc.BundleId == bundleId &&
                              !c.Deleted &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProjects = query.ToList();
            return bundleProjects;
        }

        public virtual BundleProject GetBundleProjectById(int bundleProjectId)
        {
            if (bundleProjectId == 0)
                return null;

            return _bundleProjectRepository.GetById(bundleProjectId);
        }

        public virtual void InsertBundleProject(BundleProject bundleProject)
        {
            if (bundleProject == null)
                throw new ArgumentNullException("bundleProject");

            _bundleProjectRepository.Insert(bundleProject);
        }

        public virtual void UpdateBundleProject(BundleProject bundleProject)
        {
            if (bundleProject == null)
                throw new ArgumentNullException("bundleProject");

            _bundleProjectRepository.Update(bundleProject);
        }

        public virtual IPagedList<BundleProject> GetBundleProjectsByBundleId(int bundleId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (bundleId == 0)
                return new PagedList<BundleProject>(new List<BundleProject>(), pageIndex, pageSize);

            var query = from pc in _bundleProjectRepository.Table
                        join p in _projectRepository.Table on pc.ProjectId equals p.Id
                        where pc.BundleId == bundleId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var bundleProjects = new PagedList<BundleProject>(query, pageIndex, pageSize);
            return bundleProjects;
        }

        #endregion

        #region Faq Category Mapping Methods

        public virtual Faq GetFaqCategoryById(int FaqCategoryId)
        {
            if (FaqCategoryId == 0)
                return null;

            return _faqRepository.GetById(FaqCategoryId);

        }

        public virtual IList<FaqCategory> GetFaqCategoryByHeadline(string Headline)
        {
            if (Headline == null)
            {
                return new List<FaqCategory>();
            }

            var query = from p in _faqCategoryRepository.Table
                        where p.Headline == Headline
                        select p;

            var faqcategory = query.ToList();
            return faqcategory;

        }

        public virtual IList<FaqCategoryMapping> GetFaqCatMapping(int FaqCatrgoryId)
        {
            var query = from p in _faqCategoryMappingRepository.Table
                        where p.FaqCategoryId == FaqCatrgoryId
                        select p;

            return query.ToList();
        }

        public virtual IList<FaqCategory> GetAllFaqCategory(bool showHidden = false)
        {
            var query = from p in _faqCategoryRepository.Table
                        orderby p.Name
                        select p;
            var faqcategory = query.ToList();
            return faqcategory;
        }

        #endregion

        #region Product Faq Methods

        public virtual IList<ProductFaq> GetProductFaqsByProductId(int productId)
        {
            if (productId == 0)
                return new List<ProductFaq>();

            var query = from pc in _productFaqRepository.Table
                        where pc.ProductId == productId
                        orderby pc.DisplayOrder
                        select pc;

            return query.ToList();
        }

        public virtual ProductFaq GetProductFaqById(int productId)
        {
            if (productId == 0)
                return null;

            var productfaqid = _productFaqRepository.GetById(productId);
            return productfaqid;
        }

        public virtual void InsertProductFaq(ProductFaq productfaq)
        {
            if (productfaq == null)
                throw new ArgumentNullException("productfaq");

            _productFaqRepository.Insert(productfaq);

            //event notification
            _eventPublisher.EntityInserted(productfaq);
        }

        public virtual void UpdateProductFaq(ProductFaq productfaq)
        {
            if (productfaq == null)
                throw new ArgumentNullException("productfaq");

            _productFaqRepository.Update(productfaq);

            //event notification
            _eventPublisher.EntityUpdated(productfaq);
        }

        public virtual void DeleteProductFaq(ProductFaq productfaq)
        {
            if (productfaq == null)
                throw new ArgumentNullException("projectInstruction");

            _productFaqRepository.Delete(productfaq);

            //event notification
            _eventPublisher.EntityDeleted(productfaq);
        }

        #endregion

        #region Product Video Mapping Methods

        public virtual void DeleteProductVideo(ProductVideo productVideo)
        {
            if (productVideo == null)
                throw new ArgumentNullException("productVideo");

            _productVideoRepository.Delete(productVideo);
        }

        public virtual IPagedList<ProductVideo> GetProductVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false)
        {
            if (videoId == 0)
                return new PagedList<ProductVideo>(new List<ProductVideo>(), pageIndex, pageSize);

            var query = from pc in _productVideoRepository.Table
                        join p in _productRepository.Table on pc.ProductId equals p.Id
                        where pc.VideoId == videoId &&
                              !p.Deleted &&
                              (showHidden || p.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var productVideos = new PagedList<ProductVideo>(query, pageIndex, pageSize);
            return productVideos;
        }

        public virtual IList<ProductVideo> GetProductVideosByProductId(int productId, bool showHidden = false)
        {
            if (productId == 0)
                return new List<ProductVideo>();

            var query = from pc in _productVideoRepository.Table
                        join c in _videoRepository.Table on pc.VideoId equals c.Id
                        where pc.ProductId == productId &&
                              (showHidden || c.Published)
                        orderby pc.DisplayOrder
                        select pc;

            var productVideos = query.ToList();
            return productVideos;
        }

        public virtual ProductVideo GetProductVideoById(int productVideoId)
        {
            if (productVideoId == 0)
                return null;

            return _productVideoRepository.GetById(productVideoId);
        }

        public virtual void InsertProductVideo(ProductVideo productVideo)
        {
            if (productVideo == null)
                throw new ArgumentNullException("productVideo");

            _productVideoRepository.Insert(productVideo);
        }

        public virtual void UpdateProductVideo(ProductVideo productVideo)
        {
            if (productVideo == null)
                throw new ArgumentNullException("productVideo");

            _productVideoRepository.Update(productVideo);
        }

        #endregion

        #region Project Like Methods

        public virtual void DeleteProductLike(ProductLike productLike)
        {
            if (productLike == null)
                throw new ArgumentNullException("productLike");

            _productLikeRepository.Delete(productLike);
        }

        public virtual IPagedList<Product> GetProductLikesByCustomerId(int customerId)
        {
            if (customerId == 0)
                return null;

            var query = from pf in _productLikeRepository.Table
                        join p in _productRepository.Table on pf.ProductId equals p.Id
                        where pf.CustomerId == customerId && p.Published
                        orderby pf.CreatedOn descending
                        select p;

            var products = new PagedList<Product>(query, 0, 100000);

            //return products
            return products;
        }

        public virtual void InsertProductLike(ProductLike productLike)
        {
            if (productLike == null)
                throw new ArgumentNullException("productLike");

            _productLikeRepository.Insert(productLike);
        }

        public virtual ProductLike GetProductLikeByCustomerId(int customerId, int productId)
        {
            if (productId == 0 || customerId == 0)
                return null;

            var query = from pf in _productLikeRepository.Table
                        where pf.CustomerId == customerId && pf.ProductId == productId
                        select pf;

            return query.FirstOrDefault();
        }

        public virtual int GetProductLikesByProductId(int productId)
        {
            if (productId == 0)
                return 0;

            var query = from pf in _productLikeRepository.Table
                        where pf.ProductId == productId
                        select pf;

            return query.ToList().Count();
        }

        #endregion

        #region Aboutus

        public virtual IList<Trend> GetAllTrend(bool showHidden = false)
        {
            var query = from p in _trendRepository.Table
                        orderby p.Id
                        select p;
            var Trend = query.ToList();
            return Trend;
        }

        public virtual IList<TrendReport> GetAllTrendReport(bool showHidden = false)
        {
            var query = from p in _trendReportRepository.Table
                        orderby p.Id
                        select p;
            var TrendReport = query.ToList();
            return TrendReport;
        }

        //public virtual IList<MediaAboutus> GetAllMediaAboutus(bool showHidden = false)
        //{
        //    var query = from p in _mediaAboutusRepository.Table
        //                orderby p.Id
        //                select p;
        //    var MediaAboutUs = query.ToList();
        //    return MediaAboutUs;
        //}


        public virtual IList<CareerInformation> GetAllCareerInformation(bool showHidden = false)
        {
            var query = from p in _careerInformationRepository.Table
                        orderby p.Id
                        select p;
            var CareerInformation = query.ToList();
            return CareerInformation;
        }


        public virtual IList<CareerOpening> GetAllCareerOpenning(bool showHidden = false)
        {
            var query = from p in _careerOpeningRepository.Table
                        orderby p.Id
                        select p;
            var CareerOpenning = query.ToList();
            return CareerOpenning;
        }

        //public virtual IList<MetaTags> GetAllMetaTags(bool showHidden = false)
        //{
        //    var query = from p in _metaTagsRepository.Table
        //                orderby p.Id
        //                select p;
        //    var MetaTags = query.ToList();
        //    return MetaTags;
        //}

        //public virtual IList<Subtopic> GetAllSubtopic(bool showHidden = false)
        //{
        //    var query = from p in _subTopicRepository.Table
        //                orderby p.Id
        //                select p;
        //    var Subtopic = query.ToList();
        //    return Subtopic;
        //}

        public virtual IList<Site> GetAllSite(bool showHidden = false)
        {
            var query = from p in _siteRepository.Table
                        orderby p.Id
                        select p;
            var Site = query.ToList();
            return Site;
        }

        public virtual IList<SiteContent> GetAllSiteContent(bool showHidden = false)
        {
            var query = from p in _siteContentRepository.Table
                        orderby p.Id
                        select p;
            var SiteContent = query.ToList();
            return SiteContent;
        }

        //public virtual IList<JobLink> GetAllJobLink(bool showHidden = false)
        //{
        //    var query = from p in _jobLinkRepository.Table
        //                orderby p.Id
        //                select p;
        //    var JobLink = query.ToList();
        //    return JobLink;
        //}

        //public virtual IList<PageName> GetAllPageName(bool showHidden = false)
        //{
        //    var query = from p in _pageNameRepository.Table
        //                orderby p.Id
        //                select p;
        //    var PageName = query.ToList();
        //    return PageName;
        //}


        public virtual IList<Safety> GetAllSafety(bool showHidden = false)
        {
            var query = from p in _safetyRepository.Table
                        orderby p.Id
                        select p;
            var Safety = query.ToList();
            return Safety;
        }


        public virtual IList<SafetyCategory> GetAllSafetyCategory(bool showHidden = false)
        {
            var query = from p in _safetyCategoryRepository.Table
                        orderby p.Id
                        select p;
            var SafetyCategory = query.ToList();
            return SafetyCategory;
        }

        #endregion

        #endregion
    }
}
