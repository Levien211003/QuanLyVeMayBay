using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MayBay.Models
{
    public class Cart
    {
        BookingAirLineEntities3 db = new BookingAirLineEntities3();
        public int MaChuyenBay { get; set; }
        public int MaHangBay { get; set; }
        public int MaTuyenBay { get; set; }
        public double Gia { get; set; }

        public string TinhTrang { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien()
        {
            return SoLuong * Gia;
        }
        public DateTime NgayGio { get; set; } 

        public Booking Booking { get; set; }

        public Cart(int MaCB)
        {
            this.MaChuyenBay = MaCB;
            var chuyenbay = db.ChuyenBays.Single(s => s.MaCB == this.MaChuyenBay);
            this.MaHangBay = (int)chuyenbay.MaHangHang;
            this.MaTuyenBay = (int)chuyenbay.MaTBay;
            this.NgayGio = (DateTime)chuyenbay.NgayGio;
            this.TinhTrang = chuyenbay.TinhTrang;
            this.HinhAnh = chuyenbay.HinhAnh;
            this.Gia = double.Parse(chuyenbay.Gia.ToString());

            this.SoLuong = 1;
        }
    }
}