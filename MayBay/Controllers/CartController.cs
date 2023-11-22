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

        public ActionResult AddTicketToCart(int MaCB)
        {
            List<Cart> carts = GetCarts();
            Cart room = carts.FirstOrDefault(s => s.MaChuyenBay == MaCB);
            if (room == null)
            {
                room = new Cart(MaCB);
                carts.Add(room);
            }
            else
            {
                room.SoLuong++;
            }
            return RedirectToAction("HienThiGioHang", "Cart", new { id = room.MaChuyenBay });
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
            ViewBag.TongTien = TongTien();
            return View(carts);
        }

        public ActionResult CartPartial()
        {
            ViewBag.TongTien = TongTien();


            return PartialView();
        }

        public ActionResult XoaMatHang(int MaCB)
        {
            List<Cart> carts = GetCarts();
            var sanpham = carts.FirstOrDefault(s => s.MaChuyenBay == MaCB);
            if (sanpham != null)
            {
                carts.RemoveAll(s => s.MaChuyenBay == MaCB);
                return RedirectToAction("HienThiGioHang");
            }
            if (carts.Count == 0)
                return RedirectToAction("DSachCB", "KhachHang");
            return RedirectToAction("HienThiGioHang");
        }

        public ActionResult CapNhat(int MaCB, int SoLuong)
        {
            List<Cart> carts = GetCarts();

            var sanpham = carts.FirstOrDefault(s => s.MaChuyenBay == MaCB);

            if (sanpham != null)
            {
                sanpham.SoLuong = SoLuong;
            }
            return RedirectToAction("HienThiGioHang");


        }


        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Login", "User");

            List<Cart> carts = GetCarts();
            if (carts == null || carts.Count == 0)
                return RedirectToAction("Index", "Home");



            return View(carts);
        }

        BookingAirLineEntities4 database = new BookingAirLineEntities4();

        public ActionResult DongYDatHang()
        {
            AccountInfo accountInfo = Session["TaiKhoan"] as AccountInfo;
            if (accountInfo != null)
            {
                Customer customer = database.Customers.FirstOrDefault(c => c.full_name == accountInfo.FullName);
                if (customer != null)
                {
                    List<Cart> carts = GetCarts();

                    foreach (var cart in carts)
                    {
                        Booking booking = new Booking();
                        booking.MaCB = cart.MaChuyenBay;
                        booking.customer_id = customer.customer_id;
                        booking.trang_thai = "Dang Cho Xac Nhan";

                        database.Bookings.Add(booking);

                        Console.WriteLine(booking.booking_id);
                    }


                    database.SaveChanges();

                    Session["Cart"] = null;
                    return RedirectToAction("HoanThanhDonHang");
                }
            }


            return View();
        }
        public ActionResult HoanThanhDonHang()
        {

            return View();
        }

        public ActionResult LichSuDatHang()
        {
            int? customer_id = Session["customer_id"] as int?;

            Debug.WriteLine($"Customer ID: {customer_id}");

            if (customer_id.HasValue)
            {
                var lichSuDatHang = database.Bookings
                    .Where(b => b.customer_id == customer_id)
                    .ToList();

                return View(lichSuDatHang);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


    }
}