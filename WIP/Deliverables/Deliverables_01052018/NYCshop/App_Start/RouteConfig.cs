using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NYCshop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // hiển thị danh sách các sản phẩm trên 1 slide (chỉ giới hạn bao nhiêu sản phẩm thôi)
            routes.MapRoute(
                name: "AllProductsInCategory",
                url: "{controller}/{action}/{categoryID}",
                defaults: new { controller = "Home", action = "Index", categoryID = UrlParameter.Optional }
            );

            // hiển thị chi tiết tất cả các sản phẩm trên 1 trang (phân trang)
            routes.MapRoute(
                name: "CategoryDetail",
                url: "{controller}/{action}/{categoryID}",
                defaults: new { controller = "Product", action = "Index", categoryID = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductDetail",
                url: "{controller}/{action}/{productID}",
                defaults: new { controller = "Product", action = "Detail", productID = UrlParameter.Optional }
            );
        }
    }
}
