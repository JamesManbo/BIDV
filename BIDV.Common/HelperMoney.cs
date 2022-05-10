using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDV.Common
{
    public class HelperMoney
    {
        /// <summary>
        /// Chuyển đổi số sang kiểu tiền tệ
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertToMoney(object number)
        {
            return number == null ? null : Convert.ToInt32(number).ToString("#,##0");
        }
        /// <summary>
        /// Chuyển đổi kiểu tiền tệ sang số
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static decimal ConvertMoneyToDecimal(string money)
        {
            if (string.IsNullOrEmpty(money))
            {
                return 0;
            }
            return Convert.ToDecimal(money.Replace(",", string.Empty));
        }
    }
}
