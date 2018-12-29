using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BankOnlineShopConsumer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Chi tiết sản phẩm",
                url: "chi-tiet/{MetaTitle}-{id}",
                defaults: new { controller = "ProductClient", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "BankOlineShopConsumer.Controllers" }
            );

            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] {"BankOlineShopConsumer.Controllers"}
            );

            routes.MapRoute(
               name: "Pay online",
               url: "dang-nhap-pay",
               defaults: new { controller = "PayLogin", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "BankOlineShopConsumer.Controllers" }
           );

            routes.MapRoute(
                name: "Payment Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "BankOlineShopConsumer.Controllers" }
            );

            routes.MapRoute(
                name: "Payment Failed",
                url: "loi-thanh-toan",
                defaults: new { controller = "Cart", action = "ErrorPayment", id = UrlParameter.Optional },
                namespaces: new[] { "BankOlineShopConsumer.Controllers" }
            );

            routes.MapRoute(
                name: "Payment pay",
                url: "thanh-toan-qua-pay",
                defaults: new { controller = "PayCheckout", action = "Checkout", id = UrlParameter.Optional },
                namespaces: new[] { "BankOlineShopConsumer.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BankOlineShopConsumer.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "BankOlineShopConsumer.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BankOnlineShopConsumer.Controllers" }
            );
            
        }
    }
}
