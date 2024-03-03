using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YungChingRehouse_Interview.Models;

namespace YungChingRehouse_Interview.ViewModels
{
    public class FormSearch
    {
        public string productName { get; set; }
        public int price { get; set; }
    }

    public class ProductViewModel
    {
        public ProductViewModel()
        {
            productView p = new productView();
            product = p;
            products = new List<product>();

        }
        public FormSearch formSearch { get; set; }
        public List<product> products { get; set; }
        public productView product { get; set; }
    }

    public class productView
    {
        public int productId { get; set; }
        [DisplayName("產品名稱")]
        [Required(ErrorMessage = "請輸入產品名稱")]
        public string productName { get; set; }
        [DisplayName("價格")]
        [Required(ErrorMessage = "請輸入產品價格")]
        public int price { get; set; }
        [DisplayName("數量")]
        [Required(ErrorMessage = "請輸入產品數量")]
        public int amount { get; set; }
        [DisplayName("前台是否顯示")]
        public Nullable<bool> archived { get; set; } = true;
        [DisplayName("修改日期")]
        public Nullable<System.DateTime> updatedDate { get; set; }
        [DisplayName("建立日期")]
        public System.DateTime createdDate { get; set; }
        public string fImagePath { get; set; }
    }
}