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
            return this.View(new CategoryViewModel()
            {
                Categories = this.GetTasks()
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TaskCategory category)
        {
            var subCategories = this.GetTasks()
                .First(x => x.Title == category.Title)
                .SubCategories;

            return PartialView("SubCategoryStep", new SubCategoryViewModel()
            {
                SubCategories = subCategories,
                TaskCategory = category.Title
            });
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SubCategoryStep(TaskSubCategory subCategory, string taskSubCategory)
        {
            var subCategories = this.GetTasks()
                .First(x => x.Title == taskSubCategory)
                .SubCategories;

            var tasks = subCategories
                .First(x => x.Title == subCategory.Title)
                .Tasks;

            return PartialView("TaskStep", new TaskViewModel()
            {
                Tasks = tasks,
                SubCategory = subCategory.Title
            });
        }

        private IList<TaskCategory> GetTasks()
        {
            var repairTasksRepository = new RepairTaskRepository();
            var jsonTasksService = new JsonTasksService(repairTasksRepository);
            return jsonTasksService.GetTaskCategoriesFiltered(
                DirectoryHelper.MapPath("~/App_Data/jsonData.json"),
                "P");
        }
    }
}