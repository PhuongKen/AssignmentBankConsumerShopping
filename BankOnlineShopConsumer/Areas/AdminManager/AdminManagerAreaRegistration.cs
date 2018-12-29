using System.Web.Mvc;

namespace BankOnlineShopConsumer.Areas.AdminManager
{
    public class AdminManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "danh sach san pham",
                url: "danh-sach-san-pham",
                defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BankOnlineShopConsumer.Areas.AdminManager.Controllers" }
            );

            context.MapRoute(
                name: "danh sach user",
                url: "danh-sach-user",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BankOnlineShopConsumer.Areas.AdminManager.Controllers" }
            );

            context.MapRoute(
                "AdminManager_default",
                "AdminManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "BankOnlineShopConsumer.Areas.AdminManager.Controllers" }
            );
        }
    }
}