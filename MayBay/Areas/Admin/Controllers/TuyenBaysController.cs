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


namespace Demo1.Areas.Admin.Controllers
{

    public class TuyenBaysController : Controller
    {
        BookingAirLineEntities1 database = new BookingAirLineEntities1();

        public ActionResult Index()
        {
            List<TuyenBay> tuyenBays = GetTuyenBays(); // Lấy danh sách khách sạn từ nguồn dữ liệu

            return View(tuyenBays);
        }
        private List<TuyenBay> GetTuyenBays()
        {
            // Triển khai logic để lấy danh sách khách sạn từ nguồn dữ liệu
            // Ví dụ:
            return database.TuyenBays.OrderByDescending(r => r.MaTBay).ToList();

        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTBay, SanBayDi, SanBayDen")] TuyenBay tuyenBay)
        {
            if (ModelState.IsValid)
            {
               

                database.TuyenBays.Add(tuyenBay);
                database.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(tuyenBay);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenBay tuyenBay = database.TuyenBays.Find(id);
            if (tuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(tuyenBay);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TuyenBay tuyenBay = database.TuyenBays.Find(id);
            database.TuyenBays.Remove(tuyenBay);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenBay tuyenBay = database.TuyenBays.Find(id);
            if (tuyenBay == null)
            {
                return HttpNotFound();
            }
            return View(tuyenBay);


        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenBay room = database.TuyenBays.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTBay, SanBayDi, SanBayDen")] TuyenBay tuyenBay)
        {
            if (ModelState.IsValid)
            {
                database.Entry(tuyenBay).State = EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tuyenBay);
        }

    }

}