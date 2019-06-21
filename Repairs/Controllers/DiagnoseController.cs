using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repairs.Controllers
{
    using Moat.Api.Helpers;

    using Repairs.Models;
    using Repairs.Models.Tasks;
    using Repairs.Service;
    using Repairs.ViewModels;

    public class DiagnoseController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                IRepairTasksRepository repairTasksRepository = new RepairTaskRepository();
                IJsonTasksService jsonTasksService = new JsonTasksService(repairTasksRepository);
                var filtered = jsonTasksService.GetTaskCategoriesFiltered(
                    DirectoryHelper.MapPath("~/App_Data/jsonData.json"),
                    "P");

                var vm = new CategoryViewModel() { Categories = filtered };
                return this.View(vm);
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }

            return this.HttpNotFound();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TaskCategory category)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    return this.Json(new
                    {
                        EnableSuccess = true,
                        NextStepName = "SubCategoryStep",
                        SelectedName = "category.Title",
                        SelectedValue = category.Title,
                        FormId = "CategoryStepForm"
                    });
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            // Info  
            return this.Json(new
            {
                EnableError = true,
                ErrorTitle = "Error",
                ErrorMsg = "Something goes wrong, please try again later"
            });
        }

        public ActionResult SubCategoryStep(string selectedValue)
        {
            IRepairTasksRepository repairTasksRepository = new RepairTaskRepository();
            IJsonTasksService jsonTasksService = new JsonTasksService(repairTasksRepository);
            var filtered = jsonTasksService.GetTaskCategoriesFiltered(
                DirectoryHelper.MapPath("~/App_Data/jsonData.json"),
                "P");

            var subCategories = filtered.First(x => x.Title == selectedValue).SubCategories;

            var vm = new SubCategoryViewModel() { SubCategories = subCategories };

            return PartialView(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SubCategoryStep(TaskSubCategory subCategory)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    return this.Json(new
                    {
                        EnableSuccess = true,
                        NextStepName = "SubCategoryStep",
                        SelectedName = "Category",
                        //SelectedValue = category.Title
                    });
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            // Info  
            return this.Json(new
            {
                EnableError = true,
                ErrorTitle = "Error",
                ErrorMsg = "Something goes wrong, please try again later"
            });
        }
    }
}