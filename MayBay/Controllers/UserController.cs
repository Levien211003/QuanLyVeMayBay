using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using MayBay.Models;

namespace MayBay.Controllers
{
    public class UserController : Controller
    {
        BookingAirLightEntities db = new BookingAirLightEntities();

        // GET: User
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                // Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["TaiKhoan"] != null)
            {
                // Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(KhachHang tk)
        {
            if (ModelState.IsValid)
            {
                var taikhoan = db.KhachHangs.FirstOrDefault(k => k.UserName == tk.UserName && k.Password == tk.Password);

                if (taikhoan != null)
                {
                    Session["TaiKhoan"] = taikhoan;

                    return RedirectToAction("TrangChu", "KhachHang");
                }
                else
                {
                    ViewBag.ThongBao = "Username hoặc mật khẩu không chính xác";
                }
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            if (Session["TaiKhoan"] != null)
            {
                // Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(KhachHang account)
        {
            if (ModelState.IsValid)
            {
                var existingAccount = db.KhachHangs.FirstOrDefault(a => a.UserName == account.UserName) ;
                if (existingAccount != null)
                {
                    ModelState.AddModelError("", "Username đã tồn tại");
                    return View();
                }
                account.Password = account.Password;

                db.KhachHangs.Add(account);
                db.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return View(account);
        }
    }
}