using MayBay.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace MayBay.Controllers
{
    public class KhachHangController : Controller
    {
        BookingAirLightEntities database = new BookingAirLightEntities();

        // GET: KhachHang
        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult DSachCB()
        {
            var recentlyUpdatedCB = GetChuyenBays();
            return View(recentlyUpdatedCB);
        }

        private List<ChuyenBay> GetChuyenBays()
        {
            return database.ChuyenBays.OrderBy(r => r.MaCB).ToList();
        }

        public ActionResult Details(string id)
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