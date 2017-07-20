using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppTry.Models
{
    /*
    public class FormDataPost : Controller
    {
        //
        // GET: /FormDataPost/

        public ActionResult Index()
        {
            return View();
        }

    }
     */

    public class Form1Data
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "標題")]
        public string FormTitle { get; set; }
    }
}
