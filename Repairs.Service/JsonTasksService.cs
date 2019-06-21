namespace Repairs.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Repairs.Models.Tasks;

    public class JsonTasksService : IJsonTasksService
    {
        private readonly IRepairTasksRepository repairTaskRepository;

        public JsonTasksService(IRepairTasksRepository repairTaskRepository)
        {
            this.repairTaskRepository = repairTaskRepository;
        }

        public IList<TaskExtended> GetTasks(string path, PropertyRepairConfiguration config, string locationType)
        {
            var list = this.repairTaskRepository.GetTaskCategoriesNoHierarchy(path);

            list = locationType == "P" ? list.Where(x => x.Unit).ToList() : list.Where(x => x.Communal).ToList();

            if (config.IsGeneralNeeds)
            {
                list = list.Where(x => x.TenureRented).ToList();
            }

            if (config.IsLeaseholdOrSharedOwner && !config.IsInDefects)
            {
                list = list.Where(x => x.TenureSharedOwner).ToList();
            }

            if (config.IsShelteredOrSupported)
            {
                list = list.Where(x => x.TenureSupported).ToList();
            }

            return list;
        }


        public IList<TaskCategory> GetTaskCategories(string path)
        {
            return this.repairTaskRepository.GetTaskCategories(path);
        }

        public IList<TaskCategory> GetTaskCategoriesFiltered(string path, string locationType)
        {
            PropertyRepairConfiguration config = new PropertyRepairConfiguration() { IsGeneralNeeds = true };
            var allTasks = this.GetTasks(path, config, locationType);

            var filtered = this.CreateCategories(allTasks);

            return filtered;
        }

        public TaskExtended GetTask(string path, string code, PropertyRepairConfiguration config, string locationType)
        {
            var list = this.GetTasks(path, config, locationType);

            list = list.Where(x => x.Code == code).ToList();

            if (list.Count == 0)
            {
                throw new ApplicationException($"Task not found:{code}");
            }

            return list[0];
        }

        public IList<TaskExtended> Dedupe(IList<TaskExtended> items)
        {
            var codes = new List<string>();
            var distinct = new List<TaskExtended>();

            foreach (var taskExtended in items)
            {
                var key = $"{taskExtended.Code}|{taskExtended.Item}";

                if (!codes.Contains(key))
                {
                    codes.Add(key);
                    distinct.Add(taskExtended);
                }
            }

            return distinct;
        }

        private IList<TaskCategory> CreateCategories(IList<TaskExtended> items)
        {
            var distinctItems = this.Dedupe(items);

            var categories = distinctItems.Select(x => x.Category).Distinct();

            List<TaskCategory> fullList = new List<TaskCategory>();

            // Category level
            foreach (var category in categories)
            {
                var taskCategory = new TaskCategory
                {
                    Title = category,
                    Notes = distinctItems.FirstOrDefault(x => x.Category == category)?.CategoryTip
                };
                fullList.Add(taskCategory);

                var tasksForCategory = distinctItems.Where(x => x.Category == category).ToList();
                var repairAreas = tasksForCategory.Select(x => x.RepairArea).Distinct();

                // SubCategory level
                foreach (var repairArea in repairAreas)
                {
                    var taskSubCat = new TaskSubCategory
                    {
                        Title = repairArea,
                        Notes = distinctItems.FirstOrDefault(x => x.RepairArea == repairArea)?.RepairAreaTip
                    };
                    taskCategory.SubCategories.Add(taskSubCat);

                    var webTasksForSubCategory = tasksForCategory.Where(x => x.RepairArea == repairArea).ToList();
                    var tasks = webTasksForSubCategory.Select(x => x.Item).Distinct();

                    // Item level
                    foreach (var task in tasks)
                    {
                        var webTasks = webTasksForSubCategory.Where(x => x.Item == task);
                        foreach (var webTask in webTasks)
                        {
                            var config = new ConfigurationOptions();
                            var taskTask = new Task
                            {
                                TaskDescription = task,
                                TaskCode = webTask.Code,
                                StanhopeMapping = webTask.StanhopePFI,
                                HelpTip = webTask.ItemTip
                            };
                            config.GeneralNeeds = webTask.TenureRented;
                            config.Shared = webTask.TenureSharedOwner;
                            config.Sheltered = webTask.TenureSupported;
                            config.IsCommunal = webTask.Communal;
                            config.IsUnit = webTask.Unit;
                            config.Priority = webTask.Priority.Trim();
                            taskTask.Configuration = config;
                            taskSubCat.Tasks.Add(taskTask);
                        }
                    }
                }
            }

            return fullList;
        }
    }
}
