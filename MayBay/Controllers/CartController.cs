using MayBay.Models;
using System.Collections.Generic;
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
            Cart chuyenBay = carts.FirstOrDefault(s => s.MaCB == MaCB);
            if (chuyenBay == null)
            {
                chuyenBay = new Cart(MaCB);
                carts.Add(chuyenBay);
            }
            //else
            //{
            //    chuyenBay.SoLuong++;
            //}
            return RedirectToAction("HienThiGioHang", "Cart", new { id = chuyenBay.MaCB });
        }

        //private double TongSL()
        //{
        //    List<Cart> carts = GetCarts();
        //    double tongSL = 0;
        //    if (carts != null)
        //    {
        //        tongSL = carts.Sum(sp => sp.SoLuong);
        //    }
        //    return tongSL;
        //}

        //private double TongTien()
        //{
        //    List<Cart> carts = GetCarts();
        //    double tongTien = 0;
        //    if (carts != null)
        //    {
        //        tongTien = carts.Sum(sp => sp.ThanhTien());
        //    }
        //    return tongTien;
        //}

        public ActionResult HienThiGioHang()
        {
            List<Cart> carts = GetCarts();
            //ViewBag.TongSL = TongSL();
            //ViewBag.TongTien = TongTien();
            return View(carts);
        }

        public ActionResult CartPartial()
        {
            //ViewBag.TongSL = TongSL();
            //ViewBag.TongTien = TongTien();
            return PartialView();
        }
    }
}