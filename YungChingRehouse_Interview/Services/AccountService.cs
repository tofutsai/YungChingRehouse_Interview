using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.Models.DAL;

namespace YungChingRehouse_Interview.Services
{
    public class AccountService : IAccountService
    {
        private IRepository<member> _repository;
        private IEncryptService _encryptService;

        //public AccountService() : this(new EFGenericRepository<member>(new YCReHouseInterviewEntities()))
        //{

        //}
        public AccountService(IRepository<member> repository, IEncryptService encryptService)
        {
            _repository = repository;
            _encryptService = encryptService;
        }

        public class UserToken
        {
            public string PasswordHash { get; set; }
            public string PasswordSalt { get; set; }
        }

        /// <summary>
        /// 將會員資料寫入Database
        /// </summary>
        /// <param name="mem">會員資料</param>
        public bool CreateToDatabase(member mem)
        {
            bool isSucess = false;
            if (!HasUser(mem.email))
            {
                UserToken token = _encryptService.GenerateUserToken(mem.password);
                mem.password = token.PasswordHash;
                mem.passwordSalt = token.PasswordSalt;
                mem.createdDate = DateTime.Now;
                _repository.Create(mem);
                _repository.SaveChanges();
                isSucess = true;
            }

            return isSucess;
        }
        /// <summary>
        /// 判定帳號是否已被註冊
        /// </summary>
        /// <param name="email">會員email</param>
        /// <returns></returns>
        private bool HasUser(string email)
        {
            bool hasUser = false;
            member member = _repository.Read(x => x.email == email);
            if (member != null)
            {
                hasUser = true;
            }
            return hasUser;
        }
        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <param name="email">會員email</param>
        /// <returns></returns>
        private member GetUser(string email)
        {
            member member = _repository.Read(x => x.email == email);
            return member;
        }
        /// <summary>
        /// 驗證帳密是否正確
        /// </summary>
        /// <param name="viewModel">會員輸入的帳密</param>
        /// <returns></returns>
        public bool ValidateUser(member viewModel)
        {
            if (HasUser(viewModel.email))
            {
                member user = GetUser(viewModel.email);
                bool isValid =_encryptService.IsValidPassword(user.password, user.passwordSalt, viewModel.password);
                return isValid;
            }
            return false;
        }
        /// <summary>
        /// 產生cookie
        /// </summary>
        /// <param name="email">用戶帳號</param>
        /// <returns></returns>
        public HttpCookie GenCookie(string email)
        {
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: email, //可以放使用者Id
                issueDate: DateTime.UtcNow,//現在UTC時間
                expiration: DateTime.UtcNow.AddMinutes(30),//Cookie有效時間=現在時間往後+30分鐘
                isPersistent: true,// 是否要記住我 true or false
                userData: "", //可以放使用者角色名稱
                cookiePath: FormsAuthentication.FormsCookiePath
            );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {   // 重點！！避免 Cookie遭受攻擊、盜用或不當存取。請查詢關鍵字「」。
                HttpOnly = true,  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取
                Secure = true, // 需要搭配https（SSL）才行。
                SameSite = SameSiteMode.Lax
            };
            return cookie;
        }
    }
}