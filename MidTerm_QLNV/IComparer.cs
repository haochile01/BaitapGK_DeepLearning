using MidTerm_QLNV.NhanVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTerm_QLNV
{
    public class EmployeeComparer : IComparer<ThongtinNhanVien>
    {
        public int Compare(ThongtinNhanVien x, ThongtinNhanVien y)
        {
            // So sánh thâm niên giảm dần
            int compareYears = y.NgayVaoLam.Year.CompareTo(x.NgayVaoLam.Year);

            // Nếu thâm niên giống nhau, so sánh theo họ tên tăng dần
            if (compareYears == 0)
            {
                return x.TenNV.CompareTo(y.TenNV);
            }

            return compareYears;
        }
    }
}
