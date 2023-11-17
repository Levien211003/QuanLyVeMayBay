using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MayBay.Models;

namespace MayBay.Areas.Admin.Controllers
{
    public class QLDHController : Controller
    {
        private BookingAirLineEntities1 db = new BookingAirLineEntities1();

        public ActionResult Index()
        {
            var bookings = (from b in db.Bookings
                            join c in db.Customers on b.customer_id equals c.customer_id
                            select new BookingViewModel
                            {
                                booking_id = b.booking_id,
                                MaCB = (int)b.MaCB,
                                customer_name = c.full_name,
                             
                                trang_thai = b.trang_thai
                            }).ToList();

            foreach (var b in bookings)
            {
                int intValue = (int)b.MaCB;
            }

            return View(bookings);
        }

        [HttpPost]
        public ActionResult UpdateStatus()
        {
            var bookingIdValue = Request.Form["bookingId"];
            Debug.WriteLine($"Received bookingId from form: {bookingIdValue}");

            if (int.TryParse(bookingIdValue, out int bookingId))
            {
                Debug.WriteLine($"Parsed bookingId: {bookingId}");

                var booking = db.Bookings.Find(bookingId);

                if (booking != null)
                {
                    booking.trang_thai = "Da Xac Nhan";
                    db.SaveChanges();

                    // Tải lại trang hiện tại
                    return RedirectToAction("Index");
                }
            }

            // Trả về một giá trị trong trường hợp không tìm thấy đơn đặt phòng hoặc giá trị bookingId không hợp lệ
            return RedirectToAction("Index");
        }


    }
}