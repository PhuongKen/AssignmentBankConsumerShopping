using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankOnlineShopConsumer.Models
{
    public class PartnerAccount
    {
        [Required(ErrorMessage = "Mời bạn nhập Tài khoản Pay")]
        public long partnerAccount { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập password")]
        public string password { get; set; }
        public string salt { get; set; }
        public long accountNumber { get; set; }
        public int status { get; set; }
        public System.DateTime createAt { get; set; }
        public System.DateTime updateAt { get; set; }
    }
}