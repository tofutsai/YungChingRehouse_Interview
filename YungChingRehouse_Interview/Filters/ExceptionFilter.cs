﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace YungChingRehouse_Interview.Filters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            string errMessage = null;
            if (ex == null)
            {
                return;
            }
            if (ex.InnerException != null)
            {
                errMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {ex.Message} \r\n {ex.StackTrace} \r\n {ex.InnerException.Message} \r\n";
            }
            else
            {
                errMessage = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} {ex.Message} \r\n {ex.StackTrace} \r\n";
            }

            //獲取專案根路徑
            string root = HttpRuntime.AppDomainAppPath + @"\errorLog\";
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            //錯誤內容寫入errorLog
            File.AppendAllText(root + DateTime.Now.ToString("yyyy-MM-dd") + ".txt", errMessage);

            //var err = new { Message = "系統忙線中，請稍後再試。", Code = 500 };
            //string json = JsonConvert.SerializeObject(err);
            //回傳狀態碼及內容
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary()
                {
                    {"errorMsg", "系統忙線中，請稍後再試。"}
                }
            };
            filterContext.ExceptionHandled = true;
        }

    }
}