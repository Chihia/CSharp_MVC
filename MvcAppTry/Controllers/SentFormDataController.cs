using MvcAppTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MvcAppTry.Controllers
{
    public class SentFormDataController : Controller
    {
        //
        // GET: /SentFormData/

        /*
        public ActionResult Index()
        {
            return View();
        }
        */

        public ActionResult SentFormData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SentFormData(Form1Data model)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }
    }
}
