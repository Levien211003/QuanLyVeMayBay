using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MayBay.Models
{
    
    public class Cart
    {
        
        BookingAirLightEntities db = new BookingAirLightEntities();
        public string MaCB { get; set; }
        public int MaMB { get; set; }
        public int MaTBay { get; set; }
        public string ThoiGianBay { get; set; }
        public string NgayGio { get; set; }
        public string TrangThai { get; set; }
    


    

        public Cart(string MaCB)
        {
            this.MaCB = MaCB;
            var CB = db.ChuyenBays.Single(s => s.MaCB == this.MaCB.ToString());
            this.ThoiGianBay = CB.ThoiGianBay;
            this.NgayGio = CB.NgayGio.ToString();
            this.TrangThai = CB.TinhTrang;

          
        }
    }
}