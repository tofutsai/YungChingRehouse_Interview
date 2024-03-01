using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YungChingRehouse_Interview.Models;

namespace YungChingRehouse_Interview.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// 新增member到資料庫
        /// </summary>
        /// <returns>是否儲存成功</returns>
        void CreateToDatabase(member mem);
    }
}
