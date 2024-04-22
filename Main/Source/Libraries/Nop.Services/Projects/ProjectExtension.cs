using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Projects;

namespace Nop.Services.Projects
{
    public static class ProjectExtension
    {
        public static string GetCategoryBreadCrumb(this ProjectCategory projectCategory, IProjectService projectService)
        {
            string result = string.Empty;

            while (projectCategory != null && !projectCategory.Deleted)
            {
                if (String.IsNullOrEmpty(result))
                    result = projectCategory.Name;
                else
                    result = projectCategory.Name + " >> " + result;

                projectCategory = projectService.GetProjectCategoryById(projectCategory.ParentCategoryId);

            }
            return result;
        }

        /// <summary>
        /// Returns a ProductCategory that has the specified values
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="productId">Product identifier</param>
        /// <param name="categoryId">Category identifier</param>
        /// <returns>A ProductCategory that has the specified values; otherwise null</returns>
        public static ProjectCategoryMapping FindProjectCategoryMapping(this IList<ProjectCategoryMapping> source,
            int projectId, int projectCategoryId)
        {
            foreach (var projectCategoryMapping in source)
                if (projectCategoryMapping.ProjectId == projectId && projectCategoryMapping.ProjectCategoryId == projectCategoryId)
                    return projectCategoryMapping;

            return null;
        }

        public static ProjectProduct FindProjectProduct(this IList<ProjectProduct> source,
            int productId, int projectId)
        {
            foreach (var projectProduct in source)
                if (projectProduct.ProductId == productId && projectProduct.ProjectId == projectId)
                    return projectProduct;

            return null;
        }

        public static RelatedProject FindRelatedProject(this IList<RelatedProject> source, int projectId1, int projectId2)
        {
            foreach (RelatedProject relatedProject in source)
                if (relatedProject.ProjectId1 == projectId1 && relatedProject.ProjectId2 == projectId2)
                    return relatedProject;
            return null;
        }

        public static ProjectVideo FindProjectVideo(this IList<ProjectVideo> source, int projectId, int videoId)
        {
            foreach (var projectVideo in source)
                if (projectVideo.ProjectId == projectId && projectVideo.VideoId == videoId)
                    return projectVideo;

            return null;
        }

        public static ProjectPattern FindProjectPattern(this IList<ProjectPattern> source, int projectId, int patternId)
        {
            foreach (var projectPattern in source)
                if (projectPattern.ProjectId == projectId && projectPattern.PatternId == patternId)
                    return projectPattern;

            return null;
        }

        public static ProjectCustomer FindProjectCustomer(this IList<ProjectCustomer> source, int projectId, int customerId)
        {
            foreach (var projectCustomer in source)
                if (projectCustomer.ProjectId == projectId && projectCustomer.CustomerId == customerId)
                    return projectCustomer;

            return null;
        }

        public static ProjectTechnique FindProjectTechnique(this IList<ProjectTechnique> source,
            int projectId, int techniqueId)
        {
            foreach (var projectTechnique in source)
                if (projectTechnique.ProjectId == projectId && projectTechnique.TechniqueId == techniqueId)
                    return projectTechnique;

            return null;
        }

        public static bool ProjectTagExists(this Project project,
            int projectTagId)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            bool result = project.ProjectTags.ToList().Find(pt => pt.Id == projectTagId) != null;
            return result;
        }

        public static BusinessBuilderProductCategory FindBusinessBuilderProductCategory(this IList<BusinessBuilderProductCategory> source,
            int businessBuilderProductId, int businessBuilderCategoryId)
        {
            foreach (var businessBuilderProductCategory in source)
                if (businessBuilderProductCategory.BusinessBuilderProductId == businessBuilderProductId && businessBuilderProductCategory.BusinessBuilderCategoryId == businessBuilderCategoryId)
                    return businessBuilderProductCategory;

            return null;
        }

        public static AmbassadorLinksProductCategory FindAmbassadorLinksProductCategory(this IList<AmbassadorLinksProductCategory> source,
            int ambassadorLinksProductId, int ambassadorLinksCategoryId)
        {
            foreach (var ambassadorLinksProductCategory in source)
                if (ambassadorLinksProductCategory.AmbassadorLinksProductId == ambassadorLinksProductId && ambassadorLinksProductCategory.AmbassadorLinksCategoryId == ambassadorLinksCategoryId)
                    return ambassadorLinksProductCategory;

            return null;
        }

        public static GalleryProductCategory FindGalleryProductCategory(this IList<GalleryProductCategory> source,
            int galleryProductId, int galleryCategoryId)
        {
            foreach (var galleryProductCategory in source)
                if (galleryProductCategory.GalleryProductId == galleryProductId && galleryProductCategory.GalleryCategoryId == galleryCategoryId)
                    return galleryProductCategory;

            return null;
        }

        public static string GetGalleryCategoryBreadCrumb(this GalleryCategory galleryCategory, IProjectService projectService)
        {
            string result = string.Empty;

            while (galleryCategory != null && !galleryCategory.Deleted)
            {
                if (String.IsNullOrEmpty(result))
                    result = galleryCategory.Name;
                else
                    result = galleryCategory.Name + " >> " + result;

                galleryCategory = projectService.GetGalleryCategoryById(galleryCategory.ParentId);

            }
            return result;
        }
    }
}
