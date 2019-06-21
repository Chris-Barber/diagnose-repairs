namespace Repairs.Service
{
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    using Repairs.Models.Tasks;

    public class RepairTaskRepository : IRepairTasksRepository
    {
        public IList<TaskExtended> GetTaskCategoriesNoHierarchy(string path)
        {
            using (var r = new StreamReader(path))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<TaskExtended>>(json);
            }
        }

        public IList<TaskCategory> GetTaskCategories(string path)
        {
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<IList<TaskCategory>>(json);
        }
    }
}
