using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Projects;
using Nop.Services.Catalog;

namespace Nop.Services.Catalog
{
    public partial interface IProductHowToService
    {
        #region ProductHowTo

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="product">Product</param>
        void DeleteProductHowTo(ProductHowTo producthowto);

        /// <summary>
        /// Gets all products
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Product collection</returns>
        IList<ProductHowTo> GetAllProductHowTo(bool showHidden = false);

        /// <summary>
        /// Gets all products displayed on the home page
        /// </summary>
        /// <returns>Product collection</returns>
        IList<ProductHowTo> GetAllProductHowToDisplayedOnHomePage();

        IList<ProductHowTo> GetAllSpecialOfferProductHowToDisplayedOnHomePage();

        /// <summary>
        /// Gets product
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product</returns>
        ProductHowTo GetProductHowToById(int producthowtoId);

        /// <summary>
        /// Gets products by identifier
        /// </summary>
        /// <param name="productIds">Product identifiers</param>
        /// <returns>Products</returns>
        IList<ProductHowTo> GetProductHowToByIds(int[] producthowtoIds);

        /// <summary>
        /// Inserts a product
        /// </summary>
        /// <param name="product">Product</param>
        void InsertProductHowTo(ProductHowTo producthowto);

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="product">Product</param>
        void UpdateProductHowTo(ProductHowTo producthowto);


        IPagedList<ProductHowTo> SearchProductHowTo(List<int> categoryIds, int manufacturerId, String keywords,
              int pageIndex, int pageSize,
              bool showHidden = false);

        IList<ProductHowTo> GetProductHowToByCategoryId(int categoryId);

        #endregion

        #region Producthowto Products

        void DeleteProductHowToProduct(ProductHowToProduct productHowToProduct);

        IPagedList<ProductHowToProduct> GetProductHowToProductsByProductHowToId(int productHowToId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToProduct> GetProductHowToProductsByProductId(int productId, bool showHidden = false);

        ProductHowToProduct GetProductHowToProductById(int productHowToProductId);

        void InsertProductHowToProduct(ProductHowToProduct productHowToProduct);

        void UpdateProductHowToProduct(ProductHowToProduct productHowToProduct);

        #endregion

        #region ProductHowTo pictures

        /// <summary>
        /// Deletes a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        void DeleteProductHowToPicture(ProductHowToPicture producthowtoPicture);

        /// <summary>
        /// Gets a product pictures by product identifier
        /// </summary>
        /// <param name="productId">The product identifier</param>
        /// <returns>Product pictures</returns>
        IList<ProductHowToPicture> GetProductHowToPicturesByProductHowToId(int producthowtoId);

        /// <summary>
        /// Gets a product picture
        /// </summary>
        /// <param name="productPictureId">Product picture identifier</param>
        /// <returns>Product picture</returns>

        ProductHowToPicture GetProductHowToPictureById(int producthowtoPictureId);

        /// <summary>
        /// Inserts a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        void InsertProductHowToPicture(ProductHowToPicture producthowtoPicture);

        /// <summary>
        /// Updates a product picture
        /// </summary>
        /// <param name="productPicture">Product picture</param>
        void UpdateProductHowToPicture(ProductHowToPicture producthowtoPicture);

        #endregion

        #region ProductHowTo Video Mapping Methods

        void DeleteProductHowToVideo(ProductHowToVideo producthowtoVideo);

        IPagedList<ProductHowToVideo> GetProductHowToVideosByVideoId(int videoId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToVideo> GetProductHowToVideosByProductId(int producthowtoId, bool showHidden = false);

        ProductHowToVideo GetProductHowToVideoById(int producthowtoVideoId);

        void InsertProductHowToVideo(ProductHowToVideo producthowtoVideo);

        void UpdateProductHowToVideo(ProductHowToVideo producthowtoVideo);

        #endregion

        #region ProductHowTo Bundle Mapping Methods

        void DeleteBundleProductHowTo(ProductHowToBundle bundleProductHowTo);

        IPagedList<ProductHowToBundle> GetBundleProductHowToByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToBundle> GetBundleProducthowtoByBundleId(int bundleId, bool showHidden = false);

        ProductHowToBundle GetBundleProductHowToById(int bundleProductHowToId);

        void InsertBundleHowToProduct(ProductHowToBundle bundleProductHowTo);

        void UpdateBundleProductHowTo(ProductHowToBundle bundleProductHowTo);

        IPagedList<ProductHowToBundle> GetBundleProducthowtoByBundleId(int bundleId, int pageIndex, int pageSize, bool showHidden = false);

        #endregion

        #region ProductHowTo Project Mapping

        void DeleteproductHowToProject(ProductHowToProject ProductHowToProject);

        IPagedList<ProductHowToProject> GetProductsHowToProjectByProjectId(int projectId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToProject> GetProductHowToProjectsByProductHowToId(int producthowtoId, bool showHidden = false);

        ProductHowToProject GetProductHowToProjectById(int producthowtoProjectId);

        void InsertProductHowToProject(ProductHowToProject producthowtoProject);

        void UpdateProductHowToProject(ProductHowToProject producthowtoProject);

        IPagedList<ProductHowToProject> GetProductHowToProjectsByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false);

        #endregion

        #region ProductHowTo Technique Mapping


        void DeleteproductHowToTechnique(ProductHowToTechnique producthowtoTechnique);

        IPagedList<ProductHowToTechnique> GetProductsHowToTechniqueByTechniqueId(int techniqueId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToTechnique> GetProductHowToTechniqueByProductHowToId(int producthowtoId, bool showHidden = false);

        ProductHowToTechnique GetProductHowTotechniqueById(int producthowtoTechniqueId);

        Technique GetTechniqueById(int techniqueId);

        void InsertProductHowToTechnique(ProductHowToTechnique producthowtoTechnique);

        void UpdateProductHowToTechnique(ProductHowToTechnique producthowtoTechnique);

        IPagedList<ProductHowToTechnique> GetProductHowToTechniqueByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false);

        #endregion

        #region ProductHowTo Category Mapping

        void DeleteproductHowToCategory(ProductHowToCategory producthowtoCategory);

        IPagedList<ProductHowToCategory> GetProductsHowToCategoryByCategoryId(int categoryId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToCategory> GetProductHowToCategoryByProductHowToId(int producthowtoId, bool showHidden = false);

        ProductHowToCategory GetProductHowToCategoryById(int producthowtoCategoryId);

        void InsertProductHowToCategory(ProductHowToCategory producthowtoCategory);

        void UpdateProductHowToCategory(ProductHowToCategory producthowtoCategory);

        IPagedList<ProductHowToCategory> GetProductHowToCategoryByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false);


        #endregion

        #region ProductHowTo Manufacturer Mapping

        void DeleteproductHowToManufacturer(ProductHowToManufacturer producthowtoManufacturer);

        IPagedList<ProductHowToManufacturer> GetProductsHowToManufacturerByManufacturerId(int manufacturerId, int pageIndex, int pageSize, bool showHidden = false);

        IList<ProductHowToManufacturer> GetProductHowToManufacturerByProductHowToId(int producthowtoId, bool showHidden = false);

        ProductHowToManufacturer GetProductHowToManufacturerById(int producthowtoManufacturerId);

        void InsertProductHowToManufacturer(ProductHowToManufacturer producthowtoManufacturer);

        void UpdateProductHowToManufacturer(ProductHowToManufacturer producthowtoManufacturer);

        IPagedList<ProductHowToManufacturer> GetProductHowToManufacturerByProductHowToId(int producthowtoId, int pageIndex, int pageSize, bool showHidden = false);

        #endregion
    }
}
