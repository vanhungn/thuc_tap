using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baitap1
{
    internal class SinhVien:Nguoi
    {
        public int id { get; set; }
        public string MaSinhVien { get; set; }

        public string DiaChi { get ; set; }
        public LopHoc LopHoc { get; set; }

    }
}
