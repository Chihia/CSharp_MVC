using MvcAppTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using Dapper;
using System.Data.SqlClient;
using Newtonsoft.Json;

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
        /// <summary>
        /// 呈現表單
        /// </summary>
        /// <returns></returns>
        public ActionResult SentFormData()
        {
            return View();
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SentFormData(Form1Data model, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (files != null)
                    {
                        if (files.ContentLength > 0)
                        {
                            model.File_Info = FilesController.GetFilesInfo(files);
                            if (model.File_Info != string.Empty)
                            {
                                FilesController.ToSaveFile(files);
                            }
                        }
                    }
                    bool result = Form1Data.ToInsForm1Data(model);
                    if (result)
                    {
                        return RedirectToAction("ViewShowData");
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception err)
                {
                    ModelState.AddModelError("Error", err.Message);
                    return View(err.Message);
                }
            }
            return View(model);
        }

        public ActionResult SentForm2Data()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SentForm2Data(Form1Data model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = Form1Data.ToInsForm1Data(model);
                    if (result)
                    {
                        return RedirectToAction("ViewShowData");
                    }
                    else
                    {
                        return View();
                    }
                }
                catch (Exception err)
                {
                    ViewBag.ErrMsg = err.Message;
                    return View();
                }
            }
            return View(model);
        }

        /// <summary>
        /// 呈現列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewShowData()
        {
            try
            {
                var formdatas = Form1Data.GetForm1Datas();
                return View(formdatas.ToList());
            }
            catch (Exception err)
            {
                ViewBag.ErrMsg = err.Message;
                return View();
            }
        }

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewEditData(int? id)
        {
            try
            {
                var formdata = Form1Data.GetForm1Data(id);
                return View(formdata);
            }
            catch (Exception err)
            {
                ViewBag.ErrMsg = err.Message;
                return View();
            }
        }

        /// <summary>
        /// 編輯資料
        /// </summary>
        /// <param name="model"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ViewEditData(Form1Data model, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool result = false;
                    result = Form1Data.ToUptForm1Data(model);
                    if (result)
                    {
                        return RedirectToAction("ViewShowData");
                    }
                    return View(model);
                }
                catch (Exception err)
                {
                    ViewBag.ErrMsg = err.Message;
                    return View();
                }
            }
            return View(model);
        }
        
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteFormData(int? id)
        {
            try
            {
                if (id != null)
                {
                    bool result = Form1Data.ToDelForm1Data(id);
                    if (result)
                    {
                        return RedirectToAction("ViewShowData");
                    }
                }
            }
            catch (Exception err)
            {
                ModelState.AddModelError("Error", err.Message);
                return View(err.Message);
            }
            return RedirectToAction("ViewShowData");
        }

        [HttpPost]
        public virtual ActionResult AjaxFileUpload(HttpPostedFileBase files)
        {
            AjaxApiResult apiresult = new AjaxApiResult();
            apiresult.status = "0";
            apiresult.msg = string.Empty;
            try
            {
                if (files != null)
                {
                    if (files.ContentLength > 0)
                    {
                        FilesController fileCon =  FilesController.ToSaveFile(files);
                        apiresult.status = "1";
                        apiresult.msg = JsonConvert.SerializeObject(fileCon);
                    }
                }
            }
            catch (Exception err)
            {
                apiresult.msg = string.Format("上傳失敗: {0}", err.Message);
            }
            return Json(apiresult, "application/json");
        }
    }
}
