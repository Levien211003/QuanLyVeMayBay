using MayBay.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;

using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;


namespace MayBay.Areas.Admin.Controllers
{
    public class HangHangKhongsController : Controller
    {
        BookingAirLineEntities4 database = new BookingAirLineEntities4();

        // GET: Admin/Hotels
        // GET: Admin/Hotels
        public ActionResult Index()
        {
            List<HangHangKhong> hangHangKhongs = GetHangHangs(); // Lấy danh sách khách sạn từ nguồn dữ liệu

            return View(hangHangKhongs);
        }
        private List<HangHangKhong> GetHangHangs()
        {
            return database.HangHangKhongs.OrderByDescending(r => r.MaHangHang).ToList();

        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHangHang, HinhAnh, TenHH")] HangHangKhong hangHangKhong, HttpPostedFileBase HinhAnh)
        { 
            if (ModelState.IsValid)
            {
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    // Xử lý lưu trữ tệp tin vào thư mục hoặc cơ sở dữ liệu
                    string fileName = Path.GetFileName(HinhAnh.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/hinh"), fileName);
                    HinhAnh.SaveAs(filePath);

                    // Gán tên tệp tin cho thuộc tính ImageName của đối tượng Hotel
                    hangHangKhong.HinhAnh = fileName;
                }



                database.HangHangKhongs.Add(hangHangKhong);
                database.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(hangHangKhong);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHangKhong hangHangKhong = database.HangHangKhongs.Find(id);
            if (hangHangKhong == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HangHangKhong hangHangKhong = database.HangHangKhongs.Find(id);
            database.HangHangKhongs.Remove(hangHangKhong);
            database.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHangKhong hangHangKhong = database.HangHangKhongs.Find(id);
            if (hangHangKhong == null)
            {
                return HttpNotFound();
            }
            return View(hangHangKhong);


        }

        public ActionResult Edit(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HangHangKhong hangHangKhong = database.HangHangKhongs.Find(id);
            if (hangHangKhong == null)
            {
                return HttpNotFound();
            }
            return View(hangHangKhong);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHangHang, HinhAnh, TenHH")] HangHangKhong hangHangKhong)
        {
            if (ModelState.IsValid)
            {
                database.Entry(hangHangKhong).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hangHangKhong);
        }

    }

}