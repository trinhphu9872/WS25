using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;

namespace WatchStore25.Controllers
{

    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult TaiKhoan()
        {   
            return View();
        } 
    }
}