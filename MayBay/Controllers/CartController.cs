using MayBay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Web.Mvc;

namespace MayBay.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            List<Cart> carts = GetCarts();
            return View(carts);
        }

        public List<Cart> GetCarts()
        {
            List<Cart> carts = Session["Cart"] as List<Cart>;
            if (carts == null)
            {
                carts = new List<Cart>();
                Session["Cart"] = carts;
            }
            return carts;
        }

        public ActionResult AddTicketToCart(string MaCB)
        {
            List<Cart> carts = GetCarts();
            Cart chuyenBay = carts.FirstOrDefault(s => s.MaCBay == MaCB);
            if (chuyenBay == null)
            {
                chuyenBay = new Cart(MaCB);
                carts.Add(chuyenBay);
            }
            else
            {
                chuyenBay.SoLuong++;
            }
            return RedirectToAction("HienThiGioHang", "Cart", new { id = chuyenBay.MaCBay });
        }

        private double TongSL()
        {
            List<Cart> carts = GetCarts();
            double tongSL = 0;
            if (carts != null)
            {
                tongSL = carts.Sum(sp => sp.SoLuong);
            }
            return tongSL;
        }

        private double TongTien()
        {
            List<Cart> carts = GetCarts();
            double tongTien = 0;
            if (carts != null)
            {
                tongTien = carts.Sum(sp => sp.ThanhTien());
            }
            return tongTien;
        }


        public ActionResult HienThiGioHang()
        {
            List<Cart> carts = GetCarts();
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return View(carts);
        }

        public ActionResult CartPartial()
        {
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }



        public ActionResult DatHang()
        {
            if (Session["UserKH"] == null)
                return RedirectToAction("Login", "LoginUser");

            List<Cart> carts = GetCarts();
            if (carts == null || carts.Count == 0)
                return RedirectToAction("Index", "BookHotel");



            return View(carts);
        }


        BookingAirLightEntities database = new BookingAirLightEntities();

        public ActionResult DongYDatHang()
        {
            Customer customer = Session["UserKH"] as Customer;
            List<Cart> carts = GetCarts();

            PhieuDatCho phieudatcho = new PhieuDatCho();
            phieudatcho.NgayDat = DateTime.Now;



            database.PhieuDatChoes.Add(phieudatcho);
            database.SaveChanges();

            Session["Cart"] = null;
            return RedirectToAction("HoanThanhDonHang");
        }

        public ActionResult HoanThanhDonHang()
        {
            return View();
        }




    }
}