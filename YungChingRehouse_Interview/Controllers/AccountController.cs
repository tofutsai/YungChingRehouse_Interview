using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YungChingRehouse_Interview.Models;

namespace YungChingRehouse_Interview.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// 登入頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //判斷使用者是否已經過登入驗證
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        /// <summary>
        /// 登入邏輯
        /// </summary>
        /// <param name="formData">登入表單資訊</param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(member formData, FormCollection form)
        {
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(formData);
                }
                else
                {
                    return View();
                }
            }

        }
        /// <summary>
        /// 註冊頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            //判斷使用者是否已經過登入驗證
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }
        /// <summary>
        /// 註冊邏輯
        /// </summary>
        /// <param name="formData">註冊表單資訊</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(member formData)
        {
            //判斷使用者是否已經過登入驗證
            if (User?.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            else
            {
                
                ViewBag.message = "註冊成功!";
                return View();
            }
        }
    }
}