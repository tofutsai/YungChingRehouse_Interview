using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YungChingRehouse_Interview.Models
{
    [MetadataType(typeof(memberMetadata))]
    public partial class member
    {
        public class memberMetadata
        {
            public int accountId { get; set; }
            [DisplayName("帳號")]
            [Required(ErrorMessage = "請輸入帳號email")]
            [EmailAddress]
            public string email { get; set; }
            [DisplayName("密碼")]
            [Required(ErrorMessage = "請輸入密碼")]
            public string password { get; set; }
            [DisplayName("姓名")]
            [Required(ErrorMessage = "請輸入姓名")]
            public string name { get; set; }
            public bool isAdmin { get; set; }
            public System.DateTime createdDate { get; set; }
            public Nullable<System.DateTime> updatedDate { get; set; }
        }

    }
}