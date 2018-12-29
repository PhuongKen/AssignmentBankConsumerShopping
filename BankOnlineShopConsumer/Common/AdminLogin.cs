using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankOnlineShopConsumer.Common
{
    [Serializable]
    public class AdminLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
    }
}