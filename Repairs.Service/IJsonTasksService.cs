namespace Repairs.Service
{
    using System.Collections.Generic;

    using Repairs.Models.Tasks;

    public interface IJsonTasksService
    {
        IList<TaskCategory> GetTaskCategories(string path);

        IList<TaskCategory> GetTaskCategoriesFiltered(
            string path,
            string locationType);

        TaskExtended GetTask(string path, string code, PropertyRepairConfiguration config, string locationType);

        IList<TaskExtended> Dedupe(IList<TaskExtended> items);

        IList<TaskExtended> GetTasks(string path, PropertyRepairConfiguration config, string locationType);
    }
}
