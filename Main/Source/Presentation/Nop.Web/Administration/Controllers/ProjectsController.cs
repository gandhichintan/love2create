using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Nop.Admin.Models.Catalog;
using Nop.Admin.Models.Projects;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Projects;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Media;
using Nop.Services.Projects;
using Nop.Services.Security;
using Nop.Web.Framework.Controllers;
using Nop.Services.Localization;
using Nop.Services.Seo;
using Nop.Web.Framework.Kendoui;

namespace Nop.Admin.Controllers
{
    [AdminAuthorize]
    public partial class ProjectsController : BaseAdminController
    {
        #region Fields

        private readonly IUrlRecordService _urlRecordService;
        private readonly ILocalizationService _localizationService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProjectService _projectService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IWorkContext _workContext;
        private readonly IPictureService _pictureService;
        private readonly IPermissionService _permissionService;
        private readonly ICurrencyService _currencyService;
        private readonly CurrencySettings _currencySettings;
        private readonly IMeasureService _measureService;
        private readonly MeasureSettings _measureSettings;
        private readonly AdminAreaSettings _adminAreaSettings;
        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructors

        public ProjectsController(IUrlRecordService urlRecordService, ILocalizationService localizationService, IManufacturerService manufacturerService,
            IProjectService projectService, IProductService productService,
            ICustomerService customerService, IWorkContext workContext,
            IPictureService pictureService, IPermissionService permissionService,
            ICurrencyService currencyService, CurrencySettings currencySettings,
            IMeasureService measureService, MeasureSettings measureSettings,
            PdfSettings pdfSettings, AdminAreaSettings adminAreaSettings,
            ICategoryService categoryService)
        {
            this._urlRecordService = urlRecordService;
            this._localizationService = localizationService;
            this._manufacturerService = manufacturerService;
            this._projectService = projectService;
            this._customerService = customerService;
            this._workContext = workContext;
            this._pictureService = pictureService;
            this._permissionService = permissionService;
            this._currencyService = currencyService;
            this._currencySettings = currencySettings;
            this._measureService = measureService;
            this._measureSettings = measureSettings;
            this._adminAreaSettings = adminAreaSettings;
            this._productService = productService;
            this._categoryService = categoryService;
        }

        #endregion

        #region Utitilies

        [NonAction]
        private void PrepareAddProjectPictureModel(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.AddPictureModel == null)
                model.AddPictureModel = new ProjectModel.ProjectPictureModel();
        }

        [NonAction]
        private void PrepareCategoryMapping(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.NumberOfAvailableCategories = _projectService.GetAllProjectCategories(showHidden: true).Count;
        }

        [NonAction]
        private void PrepareProjectVideos(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.NumberOfAvailableVideos = _projectService.GetAllVideos(showHidden: true).Count;
        }

        [NonAction]
        private void PrepareProjectPatterns(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.NumberOfAvailablePatterns = _projectService.GetAllPatterns(showHidden: true).Count;
        }

        [NonAction]
        //private void PrepareProjectCustomers(ProjectModel model)
        //{
        //    if (model == null)
        //        throw new ArgumentNullException("model");

        //    model.NumberOfAvailableArtists = _customerService.GetAllArtists().Count;
        //}

        //[NonAction]
        //private void PrepareProductPictureThumbnailModel(ProjectModel model, Project project)
        //{
        //    if (model == null)
        //        throw new ArgumentNullException("model");

        //    if (project != null)
        //    {
        //        if (_adminAreaSettings.DisplayProductPictures)
        //        {
        //            var defaultProductPicture = _projectService.GetDefaultProjectPicture(project.Id, _pictureService);
        //            model.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
        //        }
        //    }
        //}

        //[NonAction]
        //private string[] ParseProjectTags(string projectTags)
        //{
        //    var result = new List<string>();
        //    if (!String.IsNullOrWhiteSpace(projectTags))
        //    {
        //        string[] values = projectTags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        //        foreach (string val1 in values)
        //            if (!String.IsNullOrEmpty(val1.Trim()))
        //                result.Add(val1.Trim());
        //    }
        //    return result.ToArray();
        //}

        //[NonAction]
        //private void PrepareTags(ProjectModel model, Project project)
        //{
        //    if (model == null)
        //        throw new ArgumentNullException("model");

        //    if (project != null)
        //    {
        //        var result = new StringBuilder();
        //        for (int i = 0; i < project.ProjectTags.Count; i++)
        //        {
        //            var pt = project.ProjectTags.ToList()[i];
        //            result.Append(pt.Name);
        //            if (i != project.ProjectTags.Count - 1)
        //                result.Append(", ");
        //        }
        //        model.ProjectTags = result.ToString();
        //    }
        //}

        //[NonAction]
        //private void SaveProjectTags(Project project, string[] projectTags)
        //{
        //    if (project == null)
        //        throw new ArgumentNullException("project");

        //    //product tags
        //    var existingProjectTags = project.ProjectTags.OrderByDescending(pt => pt.ProjectCount).ToList();
        //    var projectTagsToRemove = new List<ProjectTag>();
        //    foreach (var existingProjectTag in existingProjectTags)
        //    {
        //        bool found = false;
        //        foreach (string newProjectTag in projectTags)
        //        {
        //            if (existingProjectTag.Name.Equals(newProjectTag, StringComparison.InvariantCultureIgnoreCase))
        //            {
        //                found = true;
        //                break;
        //            }
        //        }
        //        if (!found)
        //        {
        //            projectTagsToRemove.Add(existingProjectTag);
        //        }
        //    }
        //    foreach (var projectTag in projectTagsToRemove)
        //    {
        //        project.ProjectTags.Remove(projectTag);
        //        //ensure product is saved before updating totals
        //        _projectService.UpdateProject(project);
        //        _projectService.UpdateProjectTagTotals(projectTag);
        //    }
        //    foreach (string projectTagName in projectTags)
        //    {
        //        ProjectTag projectTag = null;
        //        var projectTag2 = _projectService.GetProjectTagByName(projectTagName);
        //        if (projectTag2 == null)
        //        {
        //            //add new product tag
        //            projectTag = new ProjectTag()
        //            {
        //                Name = projectTagName,
        //                ProjectCount = 0
        //            };
        //            _projectService.InsertProjectTag(projectTag);
        //        }
        //        else
        //        {
        //            projectTag = projectTag2;
        //        }
        //        if (!project.ProjectTagExists(projectTag.Id))
        //        {
        //            project.ProjectTags.Add(projectTag);
        //            //ensure product is saved before updating totals
        //            _projectService.UpdateProject(project);
        //        }
        //        //update product tag totals 
        //        _projectService.UpdateProjectTagTotals(projectTag);
        //    }
        //}

        #endregion

        #region Project list / create / edit / delete

        //list projects
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ProjectListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, ProjectListModel model)
        {
            var projects = _projectService.GetAllProjects(0, command.PageSize, 0, 0, true);
            var gridModel = new DataSourceResult
            {
                Data = projects.Select(x =>
                {
                    var projectModel = new ProjectModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Published = x.Published
                    };
                    return projectModel;
                }),
                Total = projects.Count
            };

            return Json(gridModel);
        }

        //create product
        public ActionResult Create()
        {
            var model = new ProjectModel();

            //product
            PrepareAddProjectPictureModel(model);
            PrepareCategoryMapping(model);
            PrepareProjectInstructionModel(model);
            PrepareProjectMiscModel(model);
            //PrepareProjectMaterialModel(model);
            PrepareProjectVideos(model);
            PrepareProjectPatterns(model);
            //PrepareProjectCustomers(model);
            PrepareProjectTechniqueModel(model);
            //default values
            model.Published = true;

            return View(model);
        }

        //[HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        //public ActionResult Create(ProjectModel model, bool continueEditing)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    if (ModelState.IsValid)
        //    {
        //        //project
        //        var project = model.ToEntity();
        //        project.Name = model.Name == null ? "" : model.Name;
        //        project.Description = model.Description == null ? "" : model.Description;
        //        project.Caption = model.Caption == null ? "" : model.Caption;
        //        project.Notes = model.Notes == null ? "" : model.Notes;
        //        project.Keywords = model.Keywords == null ? "" : model.Keywords;
        //        project.Date = model.Date;
        //        project.ProjectOfTheDay = model.ProjectOfTheDay;
        //        project.AudioFilePath = "";//model.AudioFilePath == null ? "" : model.AudioFilePath;
        //        project.ApprovedRatingSum = 0;
        //        project.ApprovedTotalReviews = 0;
        //        project.NotApprovedRatingSum = 0;
        //        project.NotApprovedTotalReviews = 0;
        //        project.Featured = model.Featured;
        //        project.ShowOnHomePage = model.ShowOnHomePage;
        //        project.Views = 0;
        //        project.Published = model.Published;
        //        project.IsArchived = model.IsArchived;
        //        project.IsArticle = model.IsArticle;
        //        project.IsTechnique = model.IsTechnique;
        //        project.IsRoundup = model.IsRoundup;
        //        project.PublishedDate = model.PublishedDate;
        //        project.Deleted = false;
        //        project.CreatedOn = DateTime.UtcNow;
        //        project.UpdatedOn = DateTime.UtcNow;
        //        project.ShowOnCommunity = model.ShowOnCommunity;
        //        _projectService.InsertProject(project);

        //        //search engine name
        //        model.SeName = project.ValidateSeName(model.SeName, project.Name, true);
        //        _urlRecordService.SaveSlug(project, model.SeName, 0);

        //        SaveProjectTags(project, ParseProjectTags(model.ProjectTags));

        //        SuccessNotification("Project added successfully");
        //        return continueEditing ? RedirectToAction("Edit", new { id = project.Id }) : RedirectToAction("List");
        //    }

        //    //If we got this far, something failed, redisplay form

        //    //product
        //    PrepareAddProjectPictureModel(model);
        //    PrepareCategoryMapping(model);
        //    PrepareProjectInstructionModel(model);
        //    PrepareProjectMiscModel(model);
        //    PrepareProjectMaterialModel(model);
        //    PrepareProjectVideos(model);
        //    PrepareProjectPatterns(model);
        //    PrepareProjectCustomers(model);
        //    PrepareProjectTechniqueModel(model);
        //    return View(model);
        //}

        //edit project

        public ActionResult Edit(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null || project.Deleted)
                //No product found with the specified id
                return RedirectToAction("List");

            var model = new ProjectModel();
            model.Name = project.Name;
            model.Description = project.Description;
            model.Notes = project.Notes;
            model.Caption = project.Caption;
            //model.Instructions = project.Instructions;
            model.Keywords = project.Keywords;
            model.Date = project.Date;
            model.ProjectOfTheDay = project.ProjectOfTheDay;
            //model.AudioFilePath = project.AudioFilePath;
            model.Featured = project.Featured;
            model.ShowOnHomePage = project.ShowOnHomePage;
            model.Published = project.Published;
            model.IsArchived = project.IsArchived;
            model.IsArticle = project.IsArticle;
            model.IsTechnique = project.IsTechnique;
            model.IsRoundup = project.IsRoundup;
            model.PublishedDate = project.PublishedDate;
            model.ShowOnCommunity = project.ShowOnCommunity;

            PrepareAddProjectPictureModel(model);
            PrepareCategoryMapping(model);
            PrepareProjectInstructionModel(model);
            PrepareProjectMiscModel(model);
            //PrepareProjectMaterialModel(model);
            PrepareProjectVideos(model);
            PrepareProjectPatterns(model);
            //PrepareProjectCustomers(model);
            PrepareProjectTechniqueModel(model);
            //PrepareTags(model, project);

            return View(model);
        }

        //[HttpPost, ParameterBasedOnFormNameAttribute("save-continue", "continueEditing")]
        //public ActionResult Edit(ProjectModel model, bool continueEditing)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var project = _projectService.GetProjectById(model.Id);
        //    if (project == null || project.Deleted)
        //        //No product found with the specified id
        //        return RedirectToAction("List");

        //    if (ModelState.IsValid)
        //    {
        //        //product
        //        project = model.ToEntity(project);
        //        project.Name = model.Name == null ? "" : model.Name;
        //        project.Description = model.Description == null ? "" : model.Description;
        //        project.Caption = model.Caption == null ? "" : model.Caption;
        //        project.Notes = model.Notes == null ? "" : model.Notes;
        //        project.Keywords = model.Keywords == null ? "" : model.Keywords;
        //        project.Date = model.Date;
        //        project.ProjectOfTheDay = model.ProjectOfTheDay;
        //        project.AudioFilePath = "";//model.AudioFilePath == null ? "" : model.AudioFilePath;
        //        project.Featured = model.Featured;
        //        project.ShowOnHomePage = model.ShowOnHomePage;
        //        project.Published = model.Published;
        //        project.UpdatedOn = DateTime.UtcNow;
        //        project.ShowOnCommunity = model.ShowOnCommunity;
        //        _projectService.UpdateProject(project);

        //        //search engine name
        //        model.SeName = project.ValidateSeName(model.SeName, project.Name, true);
        //        _urlRecordService.SaveSlug(project, model.SeName, 0);

        //        SaveProjectTags(project, ParseProjectTags(model.ProjectTags));

        //        SuccessNotification("Project updated successfully");
        //        return continueEditing ? RedirectToAction("Edit", new { id = project.Id }) : RedirectToAction("List");
        //    }

        //    //If we got this far, something failed, redisplay form
        //    PrepareAddProjectPictureModel(model);
        //    PrepareCategoryMapping(model);
        //    PrepareProjectInstructionModel(model);
        //    PrepareProjectMiscModel(model);
        //    PrepareProjectMaterialModel(model);
        //    PrepareProjectVideos(model);
        //    PrepareProjectPatterns(model);
        //    PrepareProjectCustomers(model);
        //    PrepareProjectTechniqueModel(model);
        //    return View(model);
        //}

        ////delete product
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var project = _projectService.GetProjectById(id);
        //    _projectService.DeleteProject(project);

        //    SuccessNotification("Project deleted successfully");
        //    return RedirectToAction("List");
        //}

        #endregion

        #region Project categories

        [HttpPost]
        public ActionResult ProjectCategoryList(DataSourceRequest command, int projectId)
        {
            var projectCategoryMappings = _projectService.GetProjectCategoryMappingsByProjectId(projectId, true);
            var projectCategoryMappingsModel = projectCategoryMappings
                .Select(x =>
                {
                    return new ProjectModel.ProjectCategoryMappingModel()
                    {
                        Id = x.Id,
                        ProjectCategory = _projectService.GetProjectCategoryById(x.ProjectCategoryId).GetCategoryBreadCrumb(_projectService),
                        ProjectId = x.ProjectId,
                        ProjectCategoryId = x.ProjectCategoryId,
                        DisplayOrder1 = x.DisplayOrder
                    };
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = projectCategoryMappingsModel,
                Total = projectCategoryMappingsModel.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult ProjectCategoryInsert(DataSourceRequest command, ProjectModel.ProjectCategoryMappingModel model)
        {
            var projectId = model.ProjectId;
            var projectCategoryId = Int32.Parse(model.ProjectCategory); //use Category property (not CategoryId) because appropriate property is stored in it

            var existingProjectCategoryMappings = _projectService.GetProjectCategoryMappingsByProjectCategoryId(projectCategoryId, 0, int.MaxValue, true);
            if (existingProjectCategoryMappings.FindProjectCategoryMapping(projectId, projectCategoryId) == null)
            {
                var projectCategoryMapping = new ProjectCategoryMapping()
                {
                    ProjectId = projectId,
                    ProjectCategoryId = projectCategoryId,
                    DisplayOrder = model.DisplayOrder1
                };
                _projectService.InsertProjectCategoryMapping(projectCategoryMapping);
            }

            return ProjectCategoryList(command, projectId);
        }

        [HttpPost]
        public ActionResult ProjectCategoryUpdate(DataSourceRequest command, ProjectModel.ProjectCategoryMappingModel model)
        {
            var projectCategoryMapping = _projectService.GetProjectCategoryMappingById(model.Id);
            if (projectCategoryMapping == null)
                throw new ArgumentException("No project category mapping found with the specified id");

            //use Category property (not CategoryId) because appropriate property is stored in it
            projectCategoryMapping.ProjectCategoryId = Int32.Parse(model.ProjectCategory);
            projectCategoryMapping.DisplayOrder = model.DisplayOrder1;
            _projectService.UpdateProjectCategoryMapping(projectCategoryMapping);

            return ProjectCategoryList(command, projectCategoryMapping.ProjectId);
        }

        [HttpPost]
        public ActionResult ProjectCategoryDelete(int id, DataSourceRequest command)
        {
            var projectCategoryMapping = _projectService.GetProjectCategoryMappingById(id);
            if (projectCategoryMapping == null)
                throw new ArgumentException("No product category mapping found with the specified id");

            var projectId = projectCategoryMapping.ProjectId;
            _projectService.DeleteProjectCategoryMapping(projectCategoryMapping);

            return ProjectCategoryList(command, projectId);
        }

        #endregion

        //#region Product pictures

        //public ActionResult ProjectPictureAdd(int pictureId, int displayOrder, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    if (pictureId == 0)
        //        throw new ArgumentException();

        //    var project = _projectService.GetProjectById(projectId);
        //    if (project == null)
        //        throw new ArgumentException("No project found with the specified id");

        //    _projectService.InsertProjectPicture(new ProjectPictureMapping()
        //    {
        //        PictureId = pictureId,
        //        ProjectId = projectId,
        //        DisplayOrder = displayOrder,
        //    });

        //    _pictureService.SetSeoFilename(pictureId, _pictureService.GetPictureSeName(project.Name));

        //    return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPictureList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var productPictures = _projectService.GetProjectPicturesByProjectId(projectId);
        //    var productPicturesModel = productPictures
        //        .Select(x =>
        //        {
        //            return new ProjectModel.ProjectPictureModel()
        //            {
        //                Id = x.Id,
        //                ProjectId = x.ProjectId,
        //                PictureId = x.PictureId,
        //                PictureUrl = _pictureService.GetPictureUrl(x.PictureId),
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectModel.ProjectPictureModel>
        //    {
        //        Data = productPicturesModel,
        //        Total = productPicturesModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPictureUpdate(ProjectModel.ProjectPictureModel model, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var productPicture = _projectService.GetProjectPictureById(model.Id);
        //    if (productPicture == null)
        //        throw new ArgumentException("No picture found with the specified id");

        //    productPicture.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectPicture(productPicture);

        //    return ProjectPictureList(command, productPicture.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPictureDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var productPicture = _projectService.GetProjectPictureById(id);
        //    if (productPicture == null)
        //        throw new ArgumentException("No picture found with the specified id");

        //    var productId = productPicture.ProjectId;
        //    _projectService.DeleteProjectPicture(productPicture);

        //    var picture = _pictureService.GetPictureById(productPicture.PictureId);
        //    _pictureService.DeletePicture(picture);

        //    return ProjectPictureList(command, productId);
        //}

        //#endregion

        //#region Project Products

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectProductList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectProducts = _projectService.GetProjectProductsByProjectId(projectId,
        //        command.Page - 1, command.PageSize, true);
        //    var model = new GridModel<ProjectModel.ProjectProductModel>
        //    {
        //        Data = projectProducts
        //        .Select(x =>
        //        {
        //            return new ProjectModel.ProjectProductModel()
        //            {
        //                Id = x.Id,
        //                ProjectId = x.ProjectId,
        //                ProductId = x.ProductId,
        //                ProductName = _productService.GetProductById(x.ProductId).Name,
        //                DisplayOrder2 = x.DisplayOrder
        //            };
        //        }),
        //        Total = projectProducts.TotalCount
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProductUpdate(GridCommand command, ProjectModel.ProjectProductModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectProduct = _projectService.GetProjectProductById(model.Id);
        //    if (projectProduct == null)
        //        throw new ArgumentException("No project product mapping found with the specified id");

        //    projectProduct.DisplayOrder = model.DisplayOrder2;
        //    _projectService.UpdateProjectProduct(projectProduct);

        //    return ProjectProductList(command, projectProduct.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProductDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectProduct = _projectService.GetProjectProductById(id);
        //    if (projectProduct == null)
        //        throw new ArgumentException("No project product mapping found with the specified id");

        //    var projectId = projectProduct.ProjectId;
        //    _projectService.DeleteProjectProduct(projectProduct);

        //    return ProjectProductList(command, projectId);
        //}

        //public ActionResult ProductAddPopup(int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    IList<int> filterableSpecificationAttributeOptionIds = null;
        //    var products = _productService.SearchProducts(0, 0, null, null, null, 0, string.Empty, false, false,
        //        _workContext.WorkingLanguage.Id, new List<int>(),
        //        ProductSortingEnum.Position, 0, _adminAreaSettings.GridPageSize,
        //        false, out filterableSpecificationAttributeOptionIds, false);

        //    var model = new ProjectModel.AddProjectProductModel();
        //    model.Products = new GridModel<ProductModel>
        //    {
        //        Data = products.Select(x => x.ToModel()),
        //        Total = products.TotalCount
        //    };

        //    //categories
        //    model.AvailableCategories.Add(new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
        //    foreach (var c in _categoryService.GetAllCategories(showHidden: true, availableToBuy: false))
        //        model.AvailableCategories.Add(new SelectListItem() { Text = c.GetCategoryNameWithPrefix(_categoryService), Value = c.Id.ToString() });

        //    //manufacturers
        //    model.AvailableManufacturers.Add(new SelectListItem() { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
        //    foreach (var m in _manufacturerService.GetAllManufacturers(true))
        //        model.AvailableManufacturers.Add(new SelectListItem() { Text = m.Name, Value = m.Id.ToString() });

        //    return View(model);
        //}

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProductAddPopupList(GridCommand command, ProjectModel.AddProjectProductModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var gridModel = new GridModel();
        //    IList<int> filterableSpecificationAttributeOptionIds = null;
        //    var products = _productService.SearchProducts(model.SearchCategoryId,
        //        model.SearchManufacturerId, null, null, null, 0, model.SearchProductName, false, false,
        //        _workContext.WorkingLanguage.Id, new List<int>(),
        //        ProductSortingEnum.Position, command.Page - 1, command.PageSize,
        //        false, out filterableSpecificationAttributeOptionIds, true);
        //    gridModel.Data = products.Select(x => x.ToModel());
        //    gridModel.Total = products.TotalCount;
        //    return new JsonResult
        //    {
        //        Data = gridModel
        //    };
        //}

        //[HttpPost]
        //[FormValueRequired("save")]
        //public ActionResult ProductAddPopup(string btnId, string formId, ProjectModel.AddProjectProductModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    if (model.SelectedProductIds != null)
        //    {
        //        foreach (int id in model.SelectedProductIds)
        //        {
        //            var product = _productService.GetProductById(id);
        //            if (product != null)
        //            {
        //                var existingProjectProducts = _projectService.GetProjectProductsByProjectId(model.ProjectId, 0, int.MaxValue, true);
        //                if (existingProjectProducts.FindProjectProduct(id, model.ProjectId) == null)
        //                {
        //                    _projectService.InsertProjectProduct(
        //                    new ProjectProduct()
        //                    {
        //                        ProjectId = model.ProjectId,
        //                        ProductId = id,
        //                        DisplayOrder = 1
        //                    });
        //                }
        //            }
        //        }
        //    }

        //    ViewBag.RefreshPage = true;
        //    ViewBag.btnId = btnId;
        //    ViewBag.formId = formId;
        //    model.Products = new GridModel<ProductModel>();
        //    return View(model);
        //}

        //#endregion

        //#region Related projects

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult RelatedProjectList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var relatedProjects = _projectService.GetRelatedProjectsByProjectId1(projectId, true);
        //    var relatedProjectsModel = relatedProjects
        //        .Select(x =>
        //        {
        //            return new ProjectModel.RelatedProjectModel()
        //            {
        //                Id = x.Id,
        //                ProjectId1 = x.ProjectId1,
        //                ProjectId2 = x.ProjectId2,
        //                Project2Name = _projectService.GetProjectById(x.ProjectId2).Name,
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectModel.RelatedProjectModel>
        //    {
        //        Data = relatedProjectsModel,
        //        Total = relatedProjectsModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult RelatedProjectUpdate(GridCommand command, ProjectModel.RelatedProjectModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var relatedProject = _projectService.GetRelatedProjectById(model.Id);
        //    if (relatedProject == null)
        //        throw new ArgumentException("No related project found with the specified id");

        //    relatedProject.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateRelatedProject(relatedProject);

        //    return RelatedProjectList(command, relatedProject.ProjectId1);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult RelatedProjectDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var relatedProject = _projectService.GetRelatedProjectById(id);
        //    if (relatedProject == null)
        //        throw new ArgumentException("No related project found with the specified id");

        //    var projectId = relatedProject.ProjectId1;
        //    _projectService.DeleteRelatedProject(relatedProject);

        //    return RelatedProjectList(command, projectId);
        //}

        //public ActionResult RelatedProjectAddPopup(int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projects = _projectService.GetAllProjects(0, _adminAreaSettings.GridPageSize, 0, 0, true);

        //    var model = new ProjectModel.AddRelatedProjectModel();
        //    model.Projects = new GridModel<ProjectModel>
        //    {
        //        Data = projects.Select(x => 
        //        {
        //            return new ProjectModel()
        //            {
        //                Id = x.Id,
        //                Name = x.Name,
        //                Published = x.Published,
        //            };
        //        }),
        //        Total = projects.TotalCount
        //    };

        //    return View(model);
        //}

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult RelatedProjectAddPopupList(GridCommand command, ProjectModel.AddRelatedProjectModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var gridModel = new GridModel();

        //    var projects = _projectService.GetAllProjects(model.SearchProjectName,0,_adminAreaSettings.GridPageSize, true);

        //    gridModel.Data = projects.Select(x => 
        //    {
        //        return new ProjectModel()
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            Published = x.Published,
        //        };
        //    });
        //    gridModel.Total = projects.TotalCount;
        //    return new JsonResult
        //    {
        //        Data = gridModel
        //    };
        //}

        //[HttpPost]
        //[FormValueRequired("save")]
        //public ActionResult RelatedProjectAddPopup(string btnId, string formId, ProjectModel.AddRelatedProjectModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    if (model.SelectedProjectIds != null)
        //    {
        //        foreach (int id in model.SelectedProjectIds)
        //        {
        //            var project = _projectService.GetProjectById(id);
        //            if (project != null)
        //            {
        //                var existingRelatedProjects = _projectService.GetRelatedProjectsByProjectId1(model.ProjectId);
        //                if (existingRelatedProjects.FindRelatedProject(model.ProjectId, id) == null)
        //                {
        //                    _projectService.InsertRelatedProject(
        //                        new RelatedProject()
        //                        {
        //                            ProjectId1 = model.ProjectId,
        //                            ProjectId2 = id,
        //                            DisplayOrder = 1
        //                        });
        //                }
        //            }
        //        }
        //    }

        //    ViewBag.RefreshPage = true;
        //    ViewBag.btnId = btnId;
        //    ViewBag.formId = formId;
        //    model.Projects = new GridModel<ProjectModel>();
        //    return View(model);
        //}

        //#endregion

        //#region Project Instruction

        [NonAction]
        private void PrepareProjectInstructionModel(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.projectInstructionsModel == null)
                model.projectInstructionsModel = new ProjectModel.ProjectInstructionsModel();


            model.projectInstructionsModel.PictureId = 0;
            model.projectInstructionsModel.DisplayOrder = 0;
            model.projectInstructionsModel.Published = true;
        }

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectInstructionList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectInstructions = _projectService.GetProjectInstructionsByProjectId(projectId, true);
        //    var projectInstructionsModel = projectInstructions
        //        .Select(x =>
        //        {
        //            return new ProjectModel.ProjectInstructionsModel()
        //            {
        //                Id = x.Id,
        //                ProjectId = x.ProjectId,
        //                Title = x.Title,
        //                PictureId = x.PictureId,
        //                PictureUrl = _pictureService.GetPictureUrl(x.PictureId),
        //                InstructionDescription = x.InstructionDescription,
        //                InstructionVideo = x.InstructionVideo,
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectModel.ProjectInstructionsModel>
        //    {
        //        Data = projectInstructionsModel,
        //        Total = projectInstructionsModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //[ValidateInput(false)]
        //public ActionResult ProjectInstructionUpdate(GridCommand command, ProjectModel.ProjectInstructionsModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectInstruction = _projectService.GetProjectInstructionById(model.Id);
        //    if (projectInstruction == null)
        //        throw new ArgumentException("No project instruction found with the specified id");

        //    if (model.PictureId != null)
        //        projectInstruction.PictureId = model.PictureId;
        //    if(!String.IsNullOrEmpty(model.Title))
        //        projectInstruction.Title = model.Title;
        //    if (!String.IsNullOrEmpty(model.InstructionDescription))
        //        projectInstruction.InstructionDescription = model.InstructionDescription;
        //    if (!String.IsNullOrEmpty(model.InstructionVideo))
        //        projectInstruction.InstructionVideo = model.InstructionVideo;

        //    projectInstruction.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectInstruction(projectInstruction);

        //    return ProjectInstructionList(command, projectInstruction.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectInstructionDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectInstruction = _projectService.GetProjectInstructionById(id);
        //    if (projectInstruction == null)
        //        throw new ArgumentException("No project instruction found with the specified id");

        //    var projectId = projectInstruction.ProjectId;
        //    _projectService.DeleteProjectInstruction(projectInstruction);

        //    return ProjectInstructionList(command, projectId);
        //}

        //[ValidateInput(false)]
        //public ActionResult ProjectInstructionAdd(string title, string description, int pictureId, string video, int displayOrder,
        //    bool published, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var pi = new ProjectInstruction()
        //    {
        //        ProjectId = projectId,
        //        Title = title,
        //        InstructionDescription = description,
        //        PictureId = pictureId,
        //        InstructionVideo = video,
        //        Published = published,
        //        DisplayOrder = displayOrder,
        //        CreatedOn = DateTime.Now,
        //    };
        //    _projectService.InsertProjectInstruction(pi);

        //    return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        //}


        //public ActionResult EditProjectInstructions(int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectInstructions = _projectService.GetProjectInstructionById(projectId);

        //    var model = new ProjectModel.ProjectInstructionsModel();

        //    model.Id = projectId;
        //    model.Title = projectInstructions.Title;
        //    model.ProjectId = projectInstructions.ProjectId;
        //    model.PictureId = projectInstructions.PictureId;
        //    model.InstructionVideo = projectInstructions.InstructionVideo;
        //    model.InstructionDescription = projectInstructions.InstructionDescription;
        //    model.DisplayOrder = projectInstructions.DisplayOrder;
        //    model.Published = projectInstructions.Published;


        //    return View(model);
        //}

        //[HttpPost,ValidateInput(false)]
        //[FormValueRequired("save")]
        //public ActionResult EditProjectInstructions(string btnId, string formId, ProjectModel.ProjectInstructionsModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectInstruction = _projectService.GetProjectInstructionById(model.Id);
        //    if (projectInstruction == null)
        //        throw new ArgumentException("No project instruction found with the specified id");

        //    if (model.PictureId != null)
        //        projectInstruction.PictureId = model.PictureId;
        //    if (!String.IsNullOrEmpty(model.Title))
        //        projectInstruction.Title = model.Title;
        //    if (!String.IsNullOrEmpty(model.InstructionDescription))
        //        projectInstruction.InstructionDescription = model.InstructionDescription;
        //    if (!String.IsNullOrEmpty(model.InstructionVideo))
        //        projectInstruction.InstructionVideo = model.InstructionVideo;

        //    projectInstruction.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectInstruction(projectInstruction);

        //    ViewBag.RefreshPage = true;
        //    ViewBag.btnId = btnId;
        //    ViewBag.formId = formId;

        //    return View(model);
        //}
        //#endregion

        //#region Project Misselaneous

        [NonAction]
        private void PrepareProjectMiscModel(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.projectMiscModel == null)
                model.projectMiscModel = new ProjectModel.ProjectMiscModel();

            model.projectMiscModel.DisplayOrder = 0;
            model.projectMiscModel.Published = true;
        }

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectMiscList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectMisc = _projectService.GetProjectMiscByProjectId(projectId, true);
        //    var projectMiscModel = projectMisc
        //        .Select(x =>
        //        {
        //            return new ProjectModel.ProjectMiscModel()
        //            {
        //                Id = x.Id,
        //                ProjectId = x.ProjectId,
        //                Description = x.Description,
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectModel.ProjectMiscModel>
        //    {
        //        Data = projectMiscModel,
        //        Total = projectMisc.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectMiscUpdate(GridCommand command, ProjectModel.ProjectMiscModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectMisc = _projectService.GetProjectMiscById(model.Id);
        //    if (projectMisc == null)
        //        throw new ArgumentException("No project miscellaneous found with the specified id");

        //    projectMisc.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectMisc(projectMisc);

        //    return ProjectMiscList(command, projectMisc.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectMiscDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectMisc = _projectService.GetProjectMiscById(id);
        //    if (projectMisc == null)
        //        throw new ArgumentException("No project miscellaneous found with the specified id");

        //    var projectId = projectMisc.ProjectId;
        //    _projectService.DeleteProjectMisc(projectMisc);

        //    return ProjectMiscList(command, projectId);
        //}

        //public ActionResult ProjectMiscAdd(string description, 
        //    int displayOrder, bool published, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var pi = new ProjectMisc()
        //    {
        //        ProjectId = projectId,
        //        Description = description,
        //        Published = published,
        //        DisplayOrder = displayOrder,
        //        CreatedOn = DateTime.Now,
        //    };
        //    _projectService.InsertProjectMisc(pi);

        //    return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        //#region Project Videos

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectVideoList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectVideos = _projectService.GetProjectVideosByProjectId(projectId, true);
        //    var projectVideosModel = projectVideos
        //        .Select(x =>
        //        {
        //            return new ProjectVideoModel()
        //            {
        //                Id = x.Id,
        //                Video = _projectService.GetVideoById(x.VideoId).Name,
        //                ProjectId = x.ProjectId,
        //                VideoId = x.VideoId,
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectVideoModel>
        //    {
        //        Data = projectVideosModel,
        //        Total = projectVideosModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectVideoInsert(GridCommand command, ProjectVideoModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectId = model.ProjectId;
        //    var videoId = Int32.Parse(model.Video);

        //    var existingProjectVideos = _projectService.GetProjectVideosByVideoId(videoId, 0, int.MaxValue, true);
        //    if (existingProjectVideos.FindProjectVideo(projectId, videoId) == null)
        //    {
        //        var projectVideo = new ProjectVideo()
        //        {
        //            ProjectId = projectId,
        //            VideoId = videoId,
        //            DisplayOrder = model.DisplayOrder
        //        };
        //        _projectService.InsertProjectVideo(projectVideo);
        //    }

        //    return ProjectVideoList(command, projectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectVideoUpdate(GridCommand command, ProjectVideoModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectVideo = _projectService.GetProjectVideoById(model.Id);
        //    if (projectVideo == null)
        //        throw new ArgumentException("No project video mapping found with the specified id");

        //    projectVideo.VideoId = Int32.Parse(model.Video);
        //    projectVideo.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectVideo(projectVideo);

        //    return ProjectVideoList(command, projectVideo.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectVideoDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectVideo = _projectService.GetProjectVideoById(id);
        //    if (projectVideo == null)
        //        throw new ArgumentException("No product video mapping found with the specified id");

        //    var projectId = projectVideo.ProjectId;
        //    _projectService.DeleteProjectVideo(projectVideo);

        //    return ProjectVideoList(command, projectId);
        //}

        //#endregion

        //#region Project Patterns

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPatternList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectPatterns = _projectService.GetProjectPatternsByProjectId(projectId, true);
        //    var projectPatternsModel = projectPatterns
        //        .Select(x =>
        //        {
        //            return new ProjectPatternModel()
        //            {
        //                Id = x.Id,
        //                Pattern = _projectService.GetPatternById(x.PatternId).Name,
        //                ProjectId = x.ProjectId,
        //                PatternId = x.PatternId,
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectPatternModel>
        //    {
        //        Data = projectPatternsModel,
        //        Total = projectPatternsModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPatternInsert(GridCommand command, ProjectPatternModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectId = model.ProjectId;
        //    var patternId = Int32.Parse(model.Pattern);

        //    var existingProjectPatterns = _projectService.GetProjectPatternsByPatternId(patternId, 0, int.MaxValue, true);
        //    if (existingProjectPatterns.FindProjectPattern(projectId, patternId) == null)
        //    {
        //        var projectPattern = new ProjectPattern()
        //        {
        //            ProjectId = projectId,
        //            PatternId = patternId,
        //            DisplayOrder = model.DisplayOrder
        //        };
        //        _projectService.InsertProjectPattern(projectPattern);
        //    }

        //    return ProjectPatternList(command, projectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPatternUpdate(GridCommand command, ProjectPatternModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectPattern = _projectService.GetProjectPatternById(model.Id);
        //    if (projectPattern == null)
        //        throw new ArgumentException("No project pattern mapping found with the specified id");

        //    projectPattern.PatternId = Int32.Parse(model.Pattern);
        //    projectPattern.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectPattern(projectPattern);

        //    return ProjectPatternList(command, projectPattern.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectPatternDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectPattern = _projectService.GetProjectPatternById(id);
        //    if (projectPattern == null)
        //        throw new ArgumentException("No product pattern mapping found with the specified id");

        //    var projectId = projectPattern.ProjectId;
        //    _projectService.DeleteProjectPattern(projectPattern);

        //    return ProjectPatternList(command, projectId);
        //}

        //#endregion

        //#region Project Customers

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectCustomerList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectCustomers = _projectService.GetProjectCustomersByProjectId(projectId, true);
        //    var projectCustomersModel = projectCustomers
        //        .Select(x =>
        //        {
        //            return new ProjectModel.ProjectCustomerModel()
        //            {
        //                Id = x.Id,
        //                Customer = _customerService.GetCustomerById(x.CustomerId).GetFullName(),
        //                ProjectId = x.ProjectId,
        //                CustomerId = x.CustomerId,
        //                DisplayOrder = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectModel.ProjectCustomerModel>
        //    {
        //        Data = projectCustomersModel,
        //        Total = projectCustomersModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectCustomerInsert(GridCommand command, ProjectModel.ProjectCustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectId = model.ProjectId;
        //    var customerId = Int32.Parse(model.Customer);

        //    var existingProjectCustomers = _projectService.GetProjectCustomersByCustomerId(customerId, 0, int.MaxValue, true);
        //    if (existingProjectCustomers.FindProjectCustomer(projectId, customerId) == null)
        //    {
        //        var projectCustomer = new ProjectCustomer()
        //        {
        //            CustomerId = customerId,
        //            ProjectId = projectId,
        //            DisplayOrder = model.DisplayOrder
        //        };
        //        _projectService.InsertProjectCustomer(projectCustomer);
        //    }

        //    return ProjectCustomerList(command, projectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectCustomerUpdate(GridCommand command, ProjectModel.ProjectCustomerModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectCustomer = _projectService.GetProjectCustomerById(model.Id);
        //    if (projectCustomer == null)
        //        throw new ArgumentException("No project artist mapping found with the specified id");

        //    projectCustomer.CustomerId = Int32.Parse(model.Customer);
        //    projectCustomer.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectCustomer(projectCustomer);

        //    return ProjectCustomerList(command, projectCustomer.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectCustomerDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectCustomer = _projectService.GetProjectCustomerById(id);
        //    if (projectCustomer == null)
        //        throw new ArgumentException("No product artist mapping found with the specified id");

        //    var projectId = projectCustomer.ProjectId;
        //    _projectService.DeleteProjectCustomer(projectCustomer);

        //    return ProjectCustomerList(command, projectId);
        //}

        //#endregion

        //#region Project Techniques

        [NonAction]
        private void PrepareProjectTechniqueModel(ProjectModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (model.projectTechniquesModel == null)
                model.projectTechniquesModel = new ProjectModel.ProjectTechniquesModel();

            model.NumberOfAvailableTechniques = _projectService.GetAllTechniques().Count;
        }

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectTechniqueList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectTechniques = _projectService.GetProjectTechniquesByProjectId(projectId, true);
        //    var projectTechniquesModel = projectTechniques
        //        .Select(x =>
        //        {
        //            return new ProjectModel.ProjectTechniquesModel()
        //            {
        //                Id = x.Id,
        //                ProjectTechnique = _projectService.GetTechniqueById(x.TechniqueId).Name,
        //                ProjectId = x.ProjectId,
        //                TechniqueId = x.TechniqueId,
        //                DisplayOrder5 = x.DisplayOrder
        //            };
        //        })
        //        .ToList();

        //    var model = new GridModel<ProjectModel.ProjectTechniquesModel>
        //    {
        //        Data = projectTechniquesModel,
        //        Total = projectTechniquesModel.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectTechniqueAdd(GridCommand command, ProjectModel.ProjectTechniquesModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectId = model.ProjectId;
        //    var projectTechniqueId = Int32.Parse(model.ProjectTechnique);

        //    var existingProjectTechniques = _projectService.GetProjectTechniquesByTechniqueId(projectTechniqueId, 0, int.MaxValue, true);
        //    if (existingProjectTechniques.FindProjectTechnique(projectId, projectTechniqueId) == null)
        //    {
        //        var projectTechnique = new ProjectTechnique()
        //        {
        //            ProjectId = projectId,
        //            TechniqueId = projectTechniqueId,
        //            DisplayOrder = model.DisplayOrder5
        //        };
        //        _projectService.InsertProjectTechnique(projectTechnique);
        //    }

        //    return ProjectTechniqueList(command, projectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectTechniqueUpdate(GridCommand command, ProjectModel.ProjectTechniquesModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectTechnique = _projectService.GetProjectTechniqueById(model.Id);
        //    if (projectTechnique == null)
        //        throw new ArgumentException("No project technique mapping found with the specified id");

        //    //use Category property (not CategoryId) because appropriate property is stored in it
        //    projectTechnique.TechniqueId = Int32.Parse(model.ProjectTechnique);
        //    projectTechnique.DisplayOrder = model.DisplayOrder5;
        //    _projectService.UpdateProjectTechnique(projectTechnique);

        //    return ProjectTechniqueList(command, projectTechnique.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectTechniqueDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectTechnique = _projectService.GetProjectTechniqueById(id);
        //    if (projectTechnique == null)
        //        throw new ArgumentException("No project technique mapping found with the specified id");

        //    var projectId = projectTechnique.ProjectId;
        //    _projectService.DeleteProjectTechnique(projectTechnique);

        //    return ProjectTechniqueList(command, projectId);
        //}

        //#endregion

        //#region Project Material

        //[NonAction]
        //private void PrepareProjectMaterialModel(ProjectModel model)
        //{
        //    if (model == null)
        //        throw new ArgumentNullException("model");

        //    if (model.projectMaterialModel == null)
        //        model.projectMaterialModel = new ProjectModel.ProjectMaterialModel();

        //    model.projectMaterialModel.DisplayOrder = 0;
        //    var allCategories = _categoryService.GetAllCategories(availableToBuy: false, hasProducts: true);

        //    for (int i = 0; i < allCategories.Count; i++)
        //    {
        //        model.projectMaterialModel.AvailableCategories.Add(new SelectListItem { Text = allCategories[i].Name, Value = allCategories[i].Id.ToString() });

        //        if (i == 0)
        //        {
        //            var products = _productService.GetProductsByCategoryId(Convert.ToInt32(allCategories[i].Id));
        //            foreach (var product in products)
        //                model.projectMaterialModel.AvailableProducts.Add(new SelectListItem { Text = product.Name, Value = product.Id.ToString() });
        //        }
        //    }
        //}

        //[HttpPost, GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectMaterialList(GridCommand command, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectMaterial = _projectService.GetProjectMaterialByProjectId(projectId);
        //    var projectMaterialModel = projectMaterial
        //    .Select(x =>
        //    {
        //        return new ProjectModel.ProjectMaterialModel()
        //        {
        //            Id = x.Id,
        //            Category = _categoryService.GetCategoryById(x.CategoryId).Name,
        //            Product = _productService.GetProductById(x.ProductId).Name,
        //            DisplayOrder = x.DisplayOrder,
        //            IsFeatured = x.IsFeatured
        //        };
        //    })
        //    .ToList();

        //    var model = new GridModel<ProjectModel.ProjectMaterialModel>
        //    {
        //        Data = projectMaterialModel,
        //        Total = projectMaterial.Count
        //    };

        //    return new JsonResult
        //    {
        //        Data = model
        //    };
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectMaterialUpdate(GridCommand command, ProjectModel.ProjectMaterialModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectMaterial = _projectService.GetProjectMaterialById(model.Id);
        //    if (projectMaterial == null)
        //        throw new ArgumentException("No project Material found with the specified id");

        //    projectMaterial.IsFeatured = model.IsFeatured;
        //    projectMaterial.DisplayOrder = model.DisplayOrder;
        //    _projectService.UpdateProjectMaterial(projectMaterial);

        //    return ProjectMaterialList(command, projectMaterial.ProjectId);
        //}

        //[GridAction(EnableCustomBinding = true)]
        //public ActionResult ProjectMaterialDelete(int id, GridCommand command)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var projectMaterial = _projectService.GetProjectMaterialById(id);
        //    if (projectMaterial == null)
        //        throw new ArgumentException("No project Materialel found with the specified id");

        //    var projectId = projectMaterial.ProjectId;
        //    _projectService.DeleteProjectMaterial(projectMaterial);

        //    return ProjectMaterialList(command, projectId);
        //}

        //public ActionResult ProjectMaterialAdd(string categoryId, string productId,
        //int displayOrder, bool isFeatured, int projectId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    var pi = new ProjectMaterial()
        //    {
        //        ProjectId = projectId,
        //        CategoryId = Int32.Parse(categoryId),
        //        ProductId = Int32.Parse(productId),
        //        DisplayOrder = displayOrder,
        //        IsFeatured = isFeatured
        //    };
        //    _projectService.InsertProjectMaterial(pi);

        //    return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        //}

        ////ajax
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult GetProductsByCategory(string categoryId)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageCatalog))
        //        return AccessDeniedView();

        //    // This action method gets called via an ajax request
        //    if (String.IsNullOrEmpty(categoryId))
        //        throw new ArgumentNullException("categoryId");

        //    var options = _productService.GetProductsByCategoryId(Convert.ToInt32(categoryId));
        //    var result = (from o in options
        //                  select new { id = o.Id, name = o.Name }).ToList();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //#endregion
    }
}