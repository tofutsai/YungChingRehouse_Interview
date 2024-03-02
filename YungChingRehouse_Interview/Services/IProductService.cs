using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.ViewModels;

namespace YungChingRehouse_Interview.Services
{
    public interface IProductService
    {
        /// <summary>
        /// 產品列表
        /// </summary>
        /// <returns></returns>
        List<product> ReadList();
        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="operId">用戶id</param>
        /// <param name="formData">產品資訊</param>
        /// <param name="image">產品照片</param>
        /// <returns></returns>
        bool createProduct(int operId, productView formData, HttpPostedFileBase image);
    }
}
