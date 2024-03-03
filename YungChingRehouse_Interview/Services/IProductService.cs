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
        /// 該用戶產品列表
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="formData">搜尋條件</param>
        /// <returns></returns>
        List<product> ReadList(int userId, FormSearch formData);
        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="formData">產品資訊</param>
        /// <param name="image">產品照片</param>
        /// <returns></returns>
        bool createProduct(int userId, productView formData, HttpPostedFileBase image);
        /// <summary>
        /// 取得特定用戶下的特定產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="productId">產品id</param>
        /// <returns></returns>
        productView Read(int userId, int productId);
        /// <summary>
        /// 更新指定產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="formData">產品資訊</param>
        /// <param name="image">產品照片</param>
        /// <returns></returns>
        bool updateProduct(int userId, productView formData, HttpPostedFileBase image);
        /// <summary>
        /// 刪除指定產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="productId">產品id</param>
        /// <returns></returns>
        bool deleteProduct(int userId, int productId);
    }
}
