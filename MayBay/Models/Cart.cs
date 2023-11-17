using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MayBay.Models
{
    public class Cart
    {
        BookingAirLineEntities1 db = new BookingAirLineEntities1();
        public int MaChuyenBay { get; set; }
        public int MaMayBay { get; set; }
        public int MaTuyenBay { get; set; }

        public string TinhTrang { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }


        public DateTime NgayGio { get; set; } // Thêm thuộc tính NgayNhanPhong

        public Booking Booking { get; set; }

        public Cart(int MaCB)
        {
            this.MaChuyenBay = MaCB;
            var chuyenbay = db.ChuyenBays.Single(s => s.MaCB == this.MaChuyenBay);
            this.MaMayBay = (int)chuyenbay.MaMB;
            this.HinhAnh = chuyenbay.HinhAnh;
            this.SoLuong = 1;
        }
    }
}