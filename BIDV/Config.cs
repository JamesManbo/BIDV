using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BIDV
{
    public class Config
    {
        public static int PageSize = 6;
        public enum TypeCategory
        {
            LoaiThe = 0,
            TinTuc = 1,
            UuDai = 2,
            TinKhuyenMai = 3,
            Qa = 4,
        }
        public enum StatusUser
        {
            NoneActive = 0,
            Active = 1,
            Delete = 2
        }
        [Flags]
        public enum TypeUser
        {
            Normal = 0,
            SuperAdmin = 1,
            Admin = 2
        }
    }
}