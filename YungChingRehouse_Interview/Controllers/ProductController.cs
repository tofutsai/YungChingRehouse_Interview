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
            productView.products = _productService.ReadList();

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

            return RedirectToAction("productList");
        }
    }
}