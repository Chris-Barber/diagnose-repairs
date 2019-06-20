namespace Repairs.Models.Tasks
{
    using System.Collections.Generic;

    public class TaskCategory
    {
        public List<TaskSubCategory> SubCategories = new List<TaskSubCategory>();

        public string Notes { get; set; }

        public string Title { get; set; }
    }
}