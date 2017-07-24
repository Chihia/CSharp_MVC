using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MvcAppTry.Models
{
    public class MainController : Controller
    {
        //
        // GET: /Main/

        /// <summary>
        /// 資料庫連線
        /// </summary>
        public static string connStr = getConnStr();

        private static string getConnStr()
        {
            string result = string.Empty;
            try
            {
                System.Configuration.Configuration rootWebConfig = WebConfigurationManager.OpenWebConfiguration("/");
                System.Configuration.ConnectionStringSettings connString;
                if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
                {
                    connString = rootWebConfig.ConnectionStrings.ConnectionStrings["DBLink"];
                    if (connString != null)
                    {
                        result = connString.ConnectionString;
                    }
                    else
                    {
                        //Console.WriteLine("No Northwind connection string");
                    }
                }
                if (result == string.Empty)
                {
                    result = WebConfigurationManager.ConnectionStrings["DBLink"].ConnectionString.ToString();
                }
            }
            catch (Exception err)
            {
                throw (err);
            }
            return result;
        }

        
    }
}
