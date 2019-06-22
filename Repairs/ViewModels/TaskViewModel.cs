using Repairs.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repairs.ViewModels
{
    public class TaskViewModel
    {
        public IList<Task> Tasks { get; set; }

        public string TaskCategory { get; set; }

        public string TaskSubCategory { get; set; }
    }
}