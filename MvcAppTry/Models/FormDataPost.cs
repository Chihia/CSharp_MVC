using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data.SqlClient;

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
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "檔案上傳")]
        public string File_Info { get; set; }

        [Display(Name="序號")]
        public string ID { get; set; }

        [Display(Name="建立日期")]
        public DateTime Create_Time { get; set; }

        /// <summary>
        /// 取得列表資料
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Form1Data> GetForm1Datas()
        {
            using (var conn = new SqlConnection(MainController.connStr))
            {
                try
                {
                    conn.Open();
                    string sqlStr = @"SELECT * FROM dt_formdata ORDER BY Create_Time DESC ";
                    var result = conn.Query<Form1Data>(sqlStr);
                    return result;
                }
                catch (Exception err)
                {
                    throw (err);
                }
            }
        }

        /// <summary>
        /// 取得單筆資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Form1Data GetForm1Data(int? id)
        {
            using (var conn = new SqlConnection(MainController.connStr))
            {
                try
                {
                    conn.Open();
                    string sqlStr = @"SELECT * FROM dt_formdata WHERE ID = @dc_ID";
                    var result = conn.Query<Form1Data>(sqlStr, new { dc_ID = id }).FirstOrDefault();

                    return result;
                }
                catch (Exception err)
                {
                    throw (err);
                }
            }
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="formdata"></param>
        /// <returns></returns>
        public static bool ToInsForm1Data(Form1Data formdata)
        {
            bool result = false;
            using (var conn = new SqlConnection(MainController.connStr))
            {
                try
                {
                    conn.Open();
                    string sqlStr = @"INSERT INTO dt_formdata (Title, File_Info, Create_Time) VALUES (@dc_title, @dc_fileinfo,  CURRENT_TIMESTAMP)";
                    conn.Execute(sqlStr, new[]{
                        new {dc_title = formdata.Title, dc_fileinfo = formdata.File_Info}
                    });
                    
                    result = true;
                }
                catch (Exception err)
                {
                    throw (err);
                }
            }
            return result;
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="formdata"></param>
        /// <returns></returns>
        public static bool ToUptForm1Data(Form1Data formdata)
        {
            bool result = false;
            using (var conn = new SqlConnection(MainController.connStr))
            {
                conn.Open();
                //SqlTransaction sqlTrans = conn.BeginTransaction();
                try
                {
                    string sqlStr = @"UPDATE dt_formdata SET Title = @dc_title, File_Info = @dc_fileinfo WHERE ID = @dc_ID";
                    conn.Execute(sqlStr, new { dc_title = formdata.Title, dc_fileinfo = formdata.File_Info, dc_ID = formdata.ID });
                    result = true;
                }
                catch (Exception err)
                {
                    throw (err);
                }
            }
            return result;
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool ToDelForm1Data(int? id)
        {
            bool result = false;
            using (var conn = new SqlConnection(MainController.connStr))
            {
                conn.Open();
                SqlTransaction sqlTrans = conn.BeginTransaction();
                try
                {                   
                    string sqlStr = "";
                    sqlStr = @"SELECT * FROM dt_formdata WHERE ID = @dc_ID ";
                    var que = conn.Query<Form1Data>(sqlStr, new { dc_ID = id }, sqlTrans).FirstOrDefault();

                    if (que.File_Info != string.Empty)
                    {
                        FilesController.ToDelFile(que.File_Info);
                    }

                    sqlStr = @"DELETE FROM dt_formdata WHERE ID = @dc_ID";
                    conn.Execute(sqlStr, new[] {
                        new { dc_ID = id}
                    }, sqlTrans);

                    sqlTrans.Commit();
                }
                catch (Exception err)
                {
                    sqlTrans.Rollback();
                    throw (err);
                }
            }
            return result;
        }
    }
}
