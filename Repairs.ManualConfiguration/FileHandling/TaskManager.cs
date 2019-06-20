namespace Repairs.ManualConfiguration.FileHandling
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Repairs.Models.Tasks;

    public static class TaskManager
    {
        public static IList<TaskExtended> GetRepairTaskTemplateData(string pathToPropServicesData)
        {
            var taskRows = Utils.ReadCsv(pathToPropServicesData);

            IList<TaskExtended> tasks = new List<TaskExtended>();

            var firstRow = true;

            foreach (var taskRow in taskRows)
            {
                if (!firstRow)
                {
                    var template = new TaskExtended
                    {
                        Code = taskRow[0].Trim(),
                        StanhopePFI = taskRow[1].Trim(),
                        Category = taskRow[2].Trim(),
                        RepairArea = taskRow[3].Trim(),
                        Problem = taskRow[4].Trim(),
                        Item = taskRow[5].Trim(),
                        Priority = taskRow[6].Trim(),
                        Communal = FormatBool(taskRow[7].Trim()),
                        Unit = FormatBool(taskRow[8].Trim()),
                        TenureSharedOwner = FormatBool(taskRow[9].Trim()),
                        TenureRented = FormatBool(taskRow[10].Trim()),
                        TenureSupported = FormatBool(taskRow[11].Trim()),
                        BookOnline = FormatBool(taskRow[12].Trim()),
                        CategoryTip = taskRow[13].Trim(),
                        RepairAreaTip = taskRow[14].Trim(),
                        ItemTip = taskRow[15].Trim(),
                        SpecialistContractor = FormatBool(taskRow[16].Trim())
                    };

                    tasks.Add(template);
                }

                firstRow = false;
            }

            return tasks;
        }
       
        public static IList<TaskCategory> CreateRepairTasks(string pathToTasks)
        {
            var extendedTasks = GetRepairTasks(pathToTasks);

            var categories = extendedTasks.Select(x => x.Category).Distinct();

            var fullList = new List<TaskCategory>();

            // Category level
            foreach (var category in categories)
            {
                var taskCategory = new TaskCategory
                {
                    Title = category,
                    Notes = extendedTasks.FirstOrDefault(x => x.Category == category)?.CategoryTip
                };

                fullList.Add(taskCategory);

                var tasksForCategory = extendedTasks.Where(x => x.Category == category).ToList();
                var repairAreas = tasksForCategory.Select(x => x.RepairArea).Distinct();

                // SubCategory level
                foreach (var repairArea in repairAreas)
                {
                    var taskSubCat = new TaskSubCategory
                    {
                        Title = repairArea,
                        Notes = extendedTasks.FirstOrDefault(x => x.RepairArea == repairArea)?.RepairAreaTip
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

        private static List<TaskExtended> GetRepairTasks(string pathToJsonData)
        {
            using (var r = new StreamReader(pathToJsonData))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TaskExtended>>(json);
            }
        }

        private static bool FormatBool(string input)
        {
            return input.ToUpper().Contains("TRUE");
        }
    }
}
