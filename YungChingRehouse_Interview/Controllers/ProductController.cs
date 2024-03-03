using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YungChingRehouse_Interview.Services;
using YungChingRehouse_Interview.ViewModels;

namespace YungChingRehouse_Interview.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private IProductService _productService;
        public ProductController(ICommonService commonService, IProductService productService) : base(commonService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 產品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult productList()
        {
            ProductViewModel productView = new ProductViewModel();
            FormSearch formData = new FormSearch
            {
                productName = string.Empty,
                price = 0
            };
            productView.formSearch = formData;
            productView.products = _productService.ReadList(userInfo.operId, formData);

            return View(productView);
        }
        /// <summary>
        /// 查詢條件產品列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult productList(ProductViewModel formData)
        {
            ProductViewModel productView = new ProductViewModel();
            productView.formSearch = formData.formSearch;
            productView.products = _productService.ReadList(userInfo.operId, formData.formSearch);

            return View(productView);
        }
        /// <summary>
        /// 新增產品頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult createProduct()
        {
            return View();
        }
        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="formData">產品資訊</param>
        /// <param name="Image">產品照片</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult createProduct(productView formData, HttpPostedFileBase Image)
        {
            if (!ModelState.IsValid)
            {
                return View(formData);
            }
            bool isSuccess = _productService.createProduct(userInfo.operId, formData, Image);
            if (isSuccess)
            {
                ViewData["message"] = "產品新增成功!";
                return View(formData);
            }
            else
            {
                return View(formData);
            }

        }
        /// <summary>
        /// 修改產品頁面
        /// </summary>
        /// <param name="productId">產品id</param>
        /// <returns></returns>
        public ActionResult updateProduct(int productId)
        {
            productView product = _productService.Read(userInfo.operId, productId);

            return View(product);
        }

        [HttpPost]
        public ActionResult updateProduct(productView formData, HttpPostedFileBase Image)
        {
            bool isSuccess = _productService.updateProduct(userInfo.operId, formData, Image);
            if (isSuccess)
            {
                ViewData["message"] = "產品修改成功!";
                return View(formData);
            }
            else
            {
                return View(formData);
            }
        }
        //[HttpDelete]
        public string deleteProduct(int productId)
        {
            bool status = _productService.deleteProduct(userInfo.operId, productId);
            string msg = status ? "刪除成功" : "刪除失敗";
            return msg;
        }
    }
}