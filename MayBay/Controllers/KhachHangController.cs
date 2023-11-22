﻿using MayBay.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;

using System.Linq;

namespace MayBay.Controllers
{
    public class KhachHangController : Controller
    {
        BookingAirLineEntities4 database = new BookingAirLineEntities4();

        // GET: KhachHang
        public ActionResult TrangChu()
        {
            return View();
        }
   
        public ActionResult DSachCB(int? page)
        {
            int PageSize = 9;
            int PageNum = (page ?? 1);


            var chuyenBays = GetChuyenBays();
            return View(chuyenBays.ToPagedList(PageNum, PageSize));
        }

        private List<ChuyenBay> GetChuyenBays()
        {
            return database.ChuyenBays.OrderBy(r => r.MaCB).ToList();
        }

        public ActionResult Details(int id)
        {
            ChuyenBay chuyenBay = database.ChuyenBays.FirstOrDefault(r => r.MaCB == id);

            if (chuyenBay == null)
            {
                return HttpNotFound();
            }

            return View(chuyenBay);
        }
    }
}