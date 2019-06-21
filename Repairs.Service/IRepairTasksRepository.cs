namespace Repairs.Service
{
    using System.Collections.Generic;

    using Repairs.Models.Tasks;

    public interface IRepairTasksRepository
    {
        IList<TaskExtended> GetTaskCategoriesNoHierarchy(string path);

        IList<TaskCategory> GetTaskCategories(string path);
    }
}
