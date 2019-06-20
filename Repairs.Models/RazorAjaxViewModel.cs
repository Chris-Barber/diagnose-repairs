namespace Repairs.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RazorAjaxViewModel
    {
        [Required]
        [Display(Name = "Text")]
        public string Text
        {
            get;
            set;
        }
    }
}
