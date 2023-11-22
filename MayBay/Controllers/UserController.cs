using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;
using MayBay.Models;
using System.Diagnostics;

namespace MayBay.Controllers
{
    public class UserController : Controller
    {
        BookingAirLineEntities4 db = new BookingAirLineEntities4();

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
        public ActionResult Login(Account taikhoan)
        {
            if (ModelState.IsValid)
            {
                var taikhoanAdmin = db.Accounts.FirstOrDefault(k => k.username == taikhoan.username && k.password == taikhoan.password && k.is_admin == true);

                if (taikhoanAdmin != null && taikhoanAdmin.is_admin.HasValue && taikhoanAdmin.is_admin.Value)
                {
                    Session["TaiKhoan"] = taikhoanAdmin;
                    return RedirectToAction("Index", "Admin/ChuyenBays");
                }

                var account = db.Accounts.FirstOrDefault(k => k.username == taikhoan.username && k.password == taikhoan.password );
                if (account != null)
                {
                    var customer = db.Customers.FirstOrDefault(c => c.customer_id == account.customer_id);
                    if (customer != null)
                    {
                        Session["customer_id"] = customer.customer_id;
                        Debug.WriteLine($"user: {customer.customer_id}");

                        var accountInfo = new AccountInfo
                        {
                            FullName = customer.full_name,
                            NgaySinh = (DateTime)customer.NgaySinh,
                            PhoneNumber = customer.phone_number,
                            Email = customer.email
                        };

                        Session["TaiKhoan"] = accountInfo;
                        return RedirectToAction("TrangChu", "KhachHang");
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Tài Khoản hoặc mật khẩu không chính xác";
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult SignUp(Account account)
        {
            if (ModelState.IsValid)
            {
                var existingAccount = db.Accounts.FirstOrDefault(a => a.username == account.username);
                if (existingAccount != null)
                {
                    ModelState.AddModelError("", "Username đã tồn tại");
                    return View();
                }

                // Tạo một đối tượng Customer từ thông tin trong đối tượng Account
                Customer customer = new Customer
                {
                    customer_id = account.customer_id ,
                    full_name = account.HoTen,
                    NgaySinh = account.NgaySinh,
                    email = account.Email,
                    phone_number = account.SoDienThoai,

                };
                account.is_admin = false;


                db.Accounts.Add(account);
                db.Customers.Add(customer);
                db.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return View(account);
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            if (Session["TaiKhoan"] != null)
            {
                // Đã đăng nhập, chuyển hướng đến trang chính
                return RedirectToAction("TrangChu", "KhachHang");
            }

            return View();
        }
        public ActionResult LogOut()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("TrangChu", "KhachHang");
        }
    }
} 
//// Lấy thông tin khách hàng từ CSDL dựa trên customer_id
        //var customer = db.Customers.FirstOrDefault(c => c.customer_id == account.customer_id);
        //if (customer != null)
        //{
        //    // Tạo một đối tượng AccountInfo để lưu trữ thông tin tài khoản và thông tin khách hàng
        //    //var accountInfo = new AccountInfo
        //    //{
        //    //    FullName = customer.full_name,
        //    //    Address = customer.address,
        //    //    PhoneNumber = customer.phone_number,
        //    //    Email = customer.email
        //    //};

        //    //// Lưu thông tin tài khoản vào session
        //    //Session["TaiKhoan"] = accountInfo;
        //}