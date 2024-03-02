using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.Models.DAL;
using YungChingRehouse_Interview.ViewModels;

namespace YungChingRehouse_Interview.Services
{
    public class ProductService : IProductService
    {
        private IRepository<product> _repository;

        public ProductService(IRepository<product> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 產品列表
        /// </summary>
        /// <returns></returns>
        public List<product> ReadList()
        {
            List<product> products = _repository.Reads().ToList();

            return products;
        }

        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="operId">用戶id</param>
        /// <param name="formData">產品資訊</param>
        /// <param name="image">產品照片</param>
        /// <returns></returns>
        public bool createProduct(int operId, productView formData, HttpPostedFileBase image)
        {
            bool isSucess = false;
            string photoName = string.Empty;
            
            if (image != null)
            {
                int index = image.FileName.IndexOf(".");
                string extention = image.FileName.Substring(index, image.FileName.Length - index);
                photoName = Guid.NewGuid().ToString("N") + extention;
                var execAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                string execPath = execAssembly.Location;
                string execDir = Path.GetDirectoryName(execPath);
                string contentDir = Path.Combine(execDir, "Content");
                image.SaveAs(contentDir + photoName);
            }
            product product = new product
            {
                accountId = operId,
                productName = formData.productName,
                price = formData.price,
                amount = formData.amount,
                archived = formData.archived,
                createdDate = DateTime.Now,
                fImagePath = "../Content/" + photoName
            };
            _repository.Create(product);
            _repository.SaveChanges();

            return isSucess;
        }
    }
}