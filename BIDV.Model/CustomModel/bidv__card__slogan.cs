using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDV.Model.CustomModel
{
    public class bidv__card__slogan
    {
        public int a { get; set; }
        public List<bidv__card__slogan_detail> detail { get; set; }
    }

    public class bidv__card__slogan_detail
    {
        public int i { get; set; }
        public string s { get; set; }
    }
}
