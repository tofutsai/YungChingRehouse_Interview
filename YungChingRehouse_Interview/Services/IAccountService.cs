using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YungChingRehouse_Interview.Models;

namespace YungChingRehouse_Interview.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// 新增member到資料庫
        /// </summary>
        /// <returns>是否儲存成功</returns>
        bool CreateToDatabase(member mem);
        /// <summary>
        /// 驗證帳密是否正確
        /// </summary>
        /// <param name="formData">會員輸入的帳密</param>
        /// <returns></returns>
        bool ValidateUser(member formData);
        /// <summary>
        /// 產生cookie
        /// </summary>
        /// <param name="email">用戶帳號</param>
        /// <returns></returns>
        HttpCookie GenCookie(string email);
    }
}
