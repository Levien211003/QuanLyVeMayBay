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
    public class ChuyenBaysController : Controller
    {
        BookingAirLineEntities1 database = new BookingAirLineEntities1();

        // GET: Admin/Hotels
        public ActionResult Index()
        {
            List<ChuyenBay> chuyenBays = GetChuyenBays(); // Lấy danh sách khách sạn từ nguồn dữ liệu

            return View(chuyenBays);
        }
        private List<ChuyenBay> GetChuyenBays()
        {
            return database.ChuyenBays.OrderByDescending(r => r.MaCB).ToList();

        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCB,MaMB,MaTBay,NgayGio, ThoiGianBay,SoLuongGheHang1,SoLuongGheHang2,SoLuongGheHang3,TinhTrang, HinhAnh")] ChuyenBay chuyenBay, HttpPostedFileBase HinhAnh)
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
                    chuyenBay.HinhAnh = fileName;
                }



                database.ChuyenBays.Add(chuyenBay);
                 database.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(chuyenBay   );
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = database.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChuyenBay chuyenBay = database.ChuyenBays.Find(id);
            database.ChuyenBays.Remove(chuyenBay);
            database.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = database.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);


        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChuyenBay chuyenBay = database.ChuyenBays.Find(id);
            if (chuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(chuyenBay);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCB,MaMB,MaTBay,NgayGio, ThoiGianBay,SoLuongGheHang1,SoLuongGheHang2,SoLuongGheHang3,TinhTrang")] ChuyenBay chuyenBay)
        {
            if (ModelState.IsValid)
            {
                database.Entry(chuyenBay).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chuyenBay);
        }

    }

}