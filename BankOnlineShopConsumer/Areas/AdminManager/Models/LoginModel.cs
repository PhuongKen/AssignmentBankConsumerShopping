using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankOnlineShopConsumer.Areas.AdminManager.Models
{
    public partial class LoginModel
    {
        [Required(ErrorMessage = "Mời bạn nhập User Name")]
        public string UserName { set; get; }


        [Required(ErrorMessage = "Mời bạn nhập Password")]
        public string PassWord { set; get; }

        public bool RememberMe { set; get; }
    }
}