using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
        /// 該用戶產品列表
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="formData">搜尋條件</param>
        /// <returns></returns>
        public List<product> ReadList(int userId, FormSearch formData)
        {
            List<product> products = new List<product>();
            if (string.IsNullOrEmpty(formData.productName))
            {
                products = _repository.Reads(x => x.accountId == userId && x.price >= formData.price).ToList();
            }
            else
            {
                products = _repository.Reads(x => x.accountId == userId && x.productName.Contains(formData.productName) && x.price >= formData.price).ToList();
            }
            

            return products;
        }
        /// <summary>
        /// 取得特定用戶下的特定產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="productId">產品id</param>
        /// <returns></returns>
        public productView Read(int userId, int productId)
        {
            product p = _repository.Read(x => x.accountId == userId && x.productId == productId);

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<product, productView>()); // 註冊Model間的對映
            var mapper = config.CreateMapper(); // 建立 Mapper
            productView result = mapper.Map<productView>(p);

            return result;
        }

        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="formData">產品資訊</param>
        /// <param name="image">產品照片</param>
        /// <returns></returns>
        public bool createProduct(int userId, productView formData, HttpPostedFileBase image)
        {
            bool isSucess = false;
            string photoName = string.Empty;

            if (image != null)
            {
                int index = image.FileName.IndexOf(".");
                string extention = image.FileName.Substring(index, image.FileName.Length - index);
                photoName = Guid.NewGuid().ToString("N") + extention;
                string contentDir = getImageDirectoryPath();
                image.SaveAs(contentDir + photoName);
            }
            else
            {
                photoName = "default.jpg";
            }
            product product = new product
            {
                accountId = userId,
                productName = formData.productName,
                price = formData.price,
                amount = formData.amount,
                archived = formData.archived,
                createdDate = DateTime.Now,
                fImagePath = photoName
            };
            _repository.Create(product);
            if (_repository.SaveChanges() != 0)
            {
                isSucess = true;
            };

            return isSucess;
        }

        /// <summary>
        /// 更新指定產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="formData">產品資訊</param>
        /// <param name="image">產品照片</param>
        /// <returns></returns>
        public bool updateProduct(int userId, productView formData, HttpPostedFileBase image)
        {
            bool isSucess = false;
            string photoName = string.Empty;
            //取得產品資訊
            product existingProduct = _repository.Read(x => x.accountId == userId && x.productId == formData.productId);
            if (image != null)
            {
                int index = image.FileName.IndexOf(".");
                string extention = image.FileName.Substring(index, image.FileName.Length - index);
                photoName = Guid.NewGuid().ToString("N") + extention;
                string contentDir = getImageDirectoryPath();
                image.SaveAs(contentDir + photoName);
                formData.fImagePath = photoName;
                //刪除產品舊照片
                if (File.Exists(contentDir + existingProduct.fImagePath))
                {
                    File.Delete(contentDir + existingProduct.fImagePath);
                }
            }
            else
            {
                formData.fImagePath = existingProduct.fImagePath;
            }
            formData.updatedDate = DateTime.Now;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<productView, product>()
                .ForMember(x => x.accountId, y => y.Ignore())
                .ForMember(x => x.createdDate, y => y.Ignore())); // 註冊Model間的對映
            var mapper = config.CreateMapper(); // 建立 Mapper
            mapper.Map(formData, existingProduct);
            _repository.Update(existingProduct);

            if (_repository.SaveChanges() != 0)
            {
                isSucess = true;
            };

            return isSucess;
        }
        /// <summary>
        /// 刪除指定產品
        /// </summary>
        /// <param name="userId">用戶id</param>
        /// <param name="productId">產品id</param>
        /// <returns></returns>
        public bool deleteProduct(int userId, int productId)
        {
            bool isSucess = false;
            //取得產品資訊
            product existingProduct = _repository.Read(x => x.accountId == userId && x.productId == productId);
            if (existingProduct != null)
            {
                _repository.Delete(existingProduct);
                if (_repository.SaveChanges() != 0)
                {
                    isSucess = true;
                };
            }
            return isSucess;
        }
        /// <summary>
        /// 取得image目錄路徑
        /// </summary>
        /// <returns></returns>
        private string getImageDirectoryPath()
        {
            var execAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            string codeBase = execAssembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            // 獲取bin目錄的上一級目錄
            string baseDir = Directory.GetParent(Path.GetDirectoryName(path)).FullName;
            string contentDir = Path.Combine(baseDir, @"Content\images\");
            return contentDir;
        }


    }
}