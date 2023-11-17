
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MayBay.Models
{
    public class BookingViewModel
    {
        public int booking_id { get; set; }
        public int MaCB { get; set; }
        public string customer_name { get; set; }

        public string trang_thai { get; set; }
    }
}