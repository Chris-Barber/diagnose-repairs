﻿namespace Repairs.ViewModels
{
    using System.Collections.Generic;

    using Repairs.Models.Tasks;

    public class CategoryViewModel
    {
        public IList<TaskCategory> Categories { get; set; }
    }
}