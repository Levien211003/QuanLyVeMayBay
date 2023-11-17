using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MayBay.Models
{
    
    public class Cart
    {
        
        BookingAirLightEntities db = new BookingAirLightEntities();
        public string MaCBay { get; set; }
        public int MaMB { get; set; }
        public int MaTBay { get; set; }
        public string ThoiGianBay { get; set; }
        public string NgayGio { get; set; }
        public string TrangThai { get; set; }

        public double ThanhTien()
        {
            return SoLuong * Gia;
        }
        public double Gia { get; set; }
        public int SoLuong { get; set; }




        public Cart(string MaCB)
        {
            this.MaCBay = MaCB;
            var chuyenbay = db.ChuyenBays.Single(s => s.MaCB == this.MaCBay);
            this.ThoiGianBay = chuyenbay.ThoiGianBay;
            this.NgayGio = chuyenbay.NgayGio.ToString();
            this.TrangThai = chuyenbay.TinhTrang;

          

            this.SoLuong = 1;
        }
    }
}