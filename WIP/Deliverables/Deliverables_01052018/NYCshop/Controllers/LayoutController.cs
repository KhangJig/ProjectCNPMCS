using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    public class LayoutController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();

        //
        // GET: /Layout/TopMenu
        public ActionResult TopMenu()
        {
            List<CategoryViewModel> model = new List<CategoryViewModel>();
            var categories = (from c in db.Categories
                              select c).ToList();

            foreach (Category cat in categories)
            {
                var subCategories = (from sc in db.SubCategories
                                     where sc.CategoryID == cat.CategoryID
                                     select sc).ToList();

                CategoryViewModel item = new CategoryViewModel();
                item.CategoryID = cat.CategoryID;
                item.CategoryName = cat.CategoryName;
                item.ListSubCats = subCategories;

                model.Add(item);
            }

            return PartialView("_MenuPartial", model);
        }
	}
}