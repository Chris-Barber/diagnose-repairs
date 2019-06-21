namespace Repairs.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RazorAjaxViewModel
    {
        [Required]
        [Display(Name = "Category")]
        public string Category
        {
            get;
            set;
        }
    }
}
