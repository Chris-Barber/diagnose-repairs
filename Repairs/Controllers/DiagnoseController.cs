using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repairs.Controllers
{
    using Repairs.Models;

    public class DiagnoseController : Controller
    {
        //// GET: Diagnose
        //public ActionResult Index()
        //{
        //    return View();
        //}
        
        /// <summary>  
        /// Get: /RazorAjax/Index method.  
        /// </summary>  
        /// <returns>Return index view</returns>  
        public ActionResult Index()
        {
            try
            {
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            // Info.  
            return this.View();
        }
        
        /// <summary>  
        /// POST: /RazorAjax/Index  
        /// </summary>  
        /// <param name="model">Model parameter</param>  
        /// <returns>Return - RazorAjax content</returns>  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(RazorAjaxViewModel model)
        {
            try
            {
                // Verification  
                if (ModelState.IsValid)
                {
                    // Info.  
                    return this.Json(new
                    {
                        EnableSuccess = true,
                        NextStepName = "SubCategoryStep",
                        SelectedName = "Category",
                        SelectedValue = model.Category
                    });
                }
            }
            catch (Exception ex)
            {
                // Info  
                Console.Write(ex);
            }
            // Info  
            return this.Json(new
            {
                EnableError = true,
                ErrorTitle = "Error",
                ErrorMsg = "Something goes wrong, please try again later"
            });
        }

        public ActionResult SubCategoryStep(string selectedValue)
        {
            return PartialView();
        }
    }
}