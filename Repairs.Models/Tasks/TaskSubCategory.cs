namespace Repairs.Models.Tasks
{
    using System.Collections.Generic;

    public class TaskSubCategory
    {
        public string Notes { get; set; }

        public List<Task> Tasks { get; set; } = new List<Task>();

        public string Title { get; set; }
    }
}