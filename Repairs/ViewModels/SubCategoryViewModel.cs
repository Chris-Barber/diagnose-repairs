namespace Repairs.ViewModels
{
    using System.Collections.Generic;

    using Repairs.Models.Tasks;

    public class SubCategoryViewModel
    {
        public IList<TaskSubCategory> SubCategories { get; set; }
    }
}