using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MayBay.Areas.Admin.Controllers
{
    public class MayBaysController : Controller
    {
        // GET: Admin/MayBays
        public ActionResult Index()
        {
            return View();
        }
    }
}