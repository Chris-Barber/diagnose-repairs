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
        public ActionResult SubCategoryStep(
            TaskSubCategory taskSubCategory, 
            string taskCategory)
        {
            var subCategories = this.GetTasks()
                .First(x => x.Title == taskCategory)
                .SubCategories;

            var tasks = subCategories
                .First(x => x.Title == taskSubCategory.Title)
                .Tasks;

            return PartialView("TaskStep", new TaskViewModel()
            {
                Tasks = tasks,
                TaskCategory = taskCategory,
                TaskSubCategory = taskSubCategory.Title
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult TaskStep(
            Task task, 
            string taskSubCategory, 
            string taskCategory)
        {
            var subCategories = this.GetTasks()
                .First(x => x.Title == taskCategory)
                .SubCategories;

            var tasks = subCategories
                .First(x => x.Title == taskSubCategory)
                .Tasks;

            var selectedTask = tasks
                .First(x => x.TaskDescription == task.TaskDescription);
           
            return PartialView("ExtraQuestionsStep", new ExtraQuestionsViewModel()
            {
                TaskCategory = taskCategory,
                TaskSubCategory = taskSubCategory,
                Task = selectedTask.TaskDescription,
                TaskCode = selectedTask.TaskCode,
                Question1 = "Please give details on the location (e.g. which floor, which door, room)",
                Question2 = "Extra question 2 that is specific to the task in question",
                Question3 = "Extra question 3 asked to get more detail"
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExtraQuestionsStep(ExtraQuestionsViewModel vm)
        {
            return View("Summary", vm);
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