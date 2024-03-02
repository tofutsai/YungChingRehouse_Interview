using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.Services;

namespace YungChingRehouse_Interview.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

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
                bool isSuccess = _accountService.ValidateUser(formData);
                if (isSuccess)
                {
                    HttpCookie cookie = _accountService.GenCookie(formData.email);
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["message"] = "帳號或密碼錯誤!";
                    return View(formData);
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
                if (!ModelState.IsValid)
                {
                    return View(formData);
                }
                bool isSucess = _accountService.CreateToDatabase(formData);
                if (isSucess)
                {
                    ViewData["message"] = "註冊成功!";
                }
                else
                {
                    ViewData["message"] = "此帳號已申請過!";
                }
                
                return View();
            }
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Authorize]//設定此Action須登入
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
        }
    }
}