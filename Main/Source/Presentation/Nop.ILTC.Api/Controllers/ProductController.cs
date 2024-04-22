
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Framework.UI.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nop.ILTC.Api.Controllers
{
    /// <summary>
    /// Represents Product Api
    /// </summary>
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        #region "Private Variable(s)"
        private readonly IProductService _productService;
        #endregion

        #region Constructors
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="productService"></param>
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        #endregion

        #region "Product"

        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet]
        [Route("ProductsDisplayedOnHomePage")]
        public IHttpActionResult GetAllProductsDisplayedOnHomePage()
        {
            try
            {
                var products = _productService.GetAllProductsDisplayedOnHomePage();
                return Ok(products);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProduct(Product product)
        {
            try
            {
                _productService.DeleteProduct(product);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
            }
        }

        [HttpGet]     
        public IHttpActionResult GetProductById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var product = _productService.GetProductById(id);

                    if (product == null)
                        return BadRequest(ErrorMessageConstants.ProductDoesNotExitsError);

                    return Ok(product);
                }
                else
                {
                    return BadRequest(ErrorMessageConstants.ProductDoesNotExitsError);
                }
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetProductsByIds(int[] ids)
        {
            try
            {
                if (ids != null)
                {
                    var products = _productService.GetProductsByIds(ids);
                    if (products != null)
                        return BadRequest(ErrorMessageConstants.ProductDoesNotExitsError);
                    return Ok(products);
                }
                else
                {
                    return BadRequest(ErrorMessageConstants.ProductDoesNotExitsError);
                }
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult InserOrUpdateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (product.Id == 0)
                        _productService.InsertProduct(product);
                    else
                        _productService.UpdateProduct(product);
                    return Ok(product);
                }
                else
                {
                    //Return model errors
                    return BadRequest(ModelState.Select(x => x.Value.Errors.ToList()).ToString());
                }
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetCategoryProductNumber(IList<int> categoryIds = null, int storeId = 0)
        {
            try
            {
                var categoryProductNumber = _productService.GetCategoryProductNumber(categoryIds, storeId);
                return Ok(categoryProductNumber);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult SearchProducts(int pageIndex = 0,
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
            try
            {
                var products = _productService.SearchProducts(pageIndex,
            pageSize,
            categoryIds,
            manufacturerId,
            storeId,
            vendorId,
            warehouseId,
            parentGroupedProductId,
            productType,
            visibleIndividuallyOnly,
            featuredProducts,
             priceMin,
            priceMax,
            productTagId,
            keywords,
            searchDescriptions,
            searchSku,
            searchProductTags,
            languageId,
            filteredSpecs,
            orderBy,
             showHidden);
                if (products.Count() == 0)
                    return BadRequest(ErrorMessageConstants.NoProductFoundError);
                return Ok(products);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetAssociatedProducts(int parentGroupedProductId,
            int storeId = 0, bool showHidden = false)
        {
            try
            {
                var associatedProducts = _productService.GetAssociatedProducts(parentGroupedProductId, storeId, showHidden);
                if (associatedProducts.Count() == 0)
                    return BadRequest(ErrorMessageConstants.NoProductFoundError);
                return Ok(associatedProducts);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProductReviewTotals(Product product)
        {
            try
            {
                _productService.UpdateProductReviewTotals(product);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetLowStockProducts(int venderId)
        {
            try
            {
                IList<Product> products = null;
                IList<ProductVariantAttributeCombination> combinations = null;
                _productService.GetLowStockProducts(venderId, out products, out combinations);
                return Ok(new Dictionary<string, dynamic> { { "Products", products }, { "ProductVariantAttributeCombination", combinations } });
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetProductBySku(string sku)
        {
            try
            {
                var product = _productService.GetProductBySku(sku);
                if (product == null)
                    return BadRequest(ErrorMessageConstants.NoProductFoundError);
                return Ok(product);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult AdjustInventory(Product product, bool decrease,
            int quantity, string attributesXml)
        {
            try
            {
                _productService.AdjustInventory(product, decrease, quantity, attributesXml);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateHasTierPricesProperty(Product product)
        {
            try
            {
                _productService.UpdateHasTierPricesProperty(product);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateHasDiscountsApplied(Product product)
        {
            try
            {
                _productService.UpdateHasDiscountsApplied(product);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion

        #region Related products
        [HttpPost]
        public IHttpActionResult DeleteRelatedProduct(RelatedProduct relatedProduct)
        {
            try
            {
                _productService.DeleteRelatedProduct(relatedProduct);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetRelatedProductsByProductId1(int productId1, bool showHidden = false)
        {
            try
            {
                var products = _productService.GetRelatedProductsByProductId1(productId1,showHidden);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetRelatedProductById(int relatedProductId)
        {
            try
            {
                var relatedProduct = _productService.GetRelatedProductById(relatedProductId);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult InsertRelatedProduct(RelatedProduct relatedProduct)
        {
            try
            {
                _productService.InsertRelatedProduct(relatedProduct);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateRelatedProduct(RelatedProduct relatedProduct)
        {
            try
            {
                _productService.UpdateRelatedProduct(relatedProduct);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion

        #region Cross-sell products

        [HttpPost]
        public IHttpActionResult DeleteCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            try
            {
                _productService.DeleteCrossSellProduct(crossSellProduct);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetCrossSellProductsByProductId1(int productId1, bool showHidden = false)
        {
            try
            {
                var crossSellProduct = _productService.GetCrossSellProductsByProductId1(productId1,showHidden);
                return Ok(crossSellProduct);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetCrossSellProductById(int crossSellProductId)
        {
            try
            {
                var crossSellProduct = _productService.GetCrossSellProductById(crossSellProductId);
                return Ok(crossSellProduct);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult InsertCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            try
            {
                _productService.InsertCrossSellProduct(crossSellProduct);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateCrossSellProduct(CrossSellProduct crossSellProduct)
        {
            try
            {
                _productService.UpdateCrossSellProduct(crossSellProduct);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetCrosssellProductsByShoppingCart(IList<ShoppingCartItem> cart, int numberOfProducts)
        {
            try
            {
                var products = _productService.GetCrosssellProductsByShoppingCart(cart,numberOfProducts);
                return Ok(products);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion

        #region Tier prices

        [HttpPost]
        public IHttpActionResult DeleteTierPrice(TierPrice tierPrice)
        {
            try
            {
                _productService.DeleteTierPrice(tierPrice);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetTierPriceById(int tierPriceId)
        {
            try
            {
                var tierPrice = _productService.GetTierPriceById(tierPriceId);
                return Ok(tierPrice);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult InsertTierPrice(TierPrice tierPrice)
        {
            try
            {
                _productService.InsertTierPrice(tierPrice);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateTierPrice(TierPrice tierPrice)
        {
            try
            {
                _productService.UpdateTierPrice(tierPrice);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion

        #region Product pictures

        [HttpPost]
        public IHttpActionResult DeleteProductPicture(ProductPicture productPicture)
        {
            try
            {
                _productService.DeleteProductPicture(productPicture);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetProductPicturesByProductId(int productId)
        {
            try
            {
                var productPicture = _productService.GetProductPictureById(productId);
                return Ok(productPicture);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetProductPictureById(int productPictureId)
        {
            try
            {
                var productPicture = _productService.GetProductPictureById(productPictureId);
                return Ok(productPictureId);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult InsertProductPicture(ProductPicture productPicture)
        {
            try
            {
                _productService.InsertProductPicture(productPicture);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateProductPicture(ProductPicture productPicture)
        {
            try
            {
                _productService.UpdateProductPicture(productPicture);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion

        #region Product reviews

        [HttpGet]
        public IHttpActionResult GetAllProductReviews(int customerId, bool? approved,
            DateTime? fromUtc = null, DateTime? toUtc = null,
            string message = null)
        {
            try
            {
                var productReviews = _productService.GetAllProductReviews(customerId,approved,fromUtc,toUtc);
                return Ok(productReviews);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpGet]
        public IHttpActionResult GetProductReviewById(int productReviewId)
        {
            try
            {
                var productReview = _productService.GetProductReviewById(productReviewId);
                return Ok(productReview);
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        [HttpPost]
        public IHttpActionResult DeleteProductReview(ProductReview productReview)
        {
            try
            {
                _productService.DeleteProductReview(productReview);
                return Ok();
            }
            catch (Exception exc)
            {
                return BadRequest(exc.Message);
                throw;
            }
        }

        #endregion
    }
}
