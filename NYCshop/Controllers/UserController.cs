using NYCshop.Assets;
using NYCshop.Attributes;
using NYCshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NYCshop.Controllers
{
    [CheckUser]
    public class UserController : Controller
    {
        private ExLoverShopDb db = new ExLoverShopDb();
        private MD5Assets md5 = new MD5Assets();
        private GetListAndDict listAndDict = new GetListAndDict();

        // trang thông tin tài khoản được ưu tiên hàng đầu
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
	}
}