namespace Repairs.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RazorAjaxViewModel
    {
        [Required]
        [Display(Name = "SubCategory")]
        public string SubCategory
        {
            get;
            set;
        }
    }
}
