using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankOnlineShopConsumer.Models
{
    public class Transaction
    {
        public long id { get; set; }
        [Required(ErrorMessage ="Nhập tên giao dịch của bạn.")]
        public string name { get; set; }
        public decimal amount { get; set; }
        public decimal feeTransaction { get; set; }
        [Required(ErrorMessage = "Nhập nội dung giao dịch của bạn.")]
        public string content { get; set; }
        public long senderAccountNumber { get; set; }
        public long receiverAccountNumber { get; set; }
        public int type { get; set; }
        public System.DateTime createAt { get; set; }
        public System.DateTime updateAt { get; set; }
        public int status { get; set; }
        public string billId { get; set; }
    }
}