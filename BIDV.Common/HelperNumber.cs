using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BIDV.Common
{
    public class HelperNumber
    {
        /// <summary>
        /// Kiểm tra số có phải số chẵn hay không ?
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsEvenNumber(int number)
        {
            return number % 2 == 0;
        }
        /// <summary>
        /// Kiểm tra có phải số lẻ hay không
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsOddNumber(int number)
        {
            return !IsEvenNumber(number);
        }
        /// <summary>
        /// Lấy ngẫu nhiên một số trong một khoảng
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomNumber(int min, int max)
        {
            var random = new Random();
            if (min >= max) { throw new Exception("Min value is greater than or equal to the max value"); }
            var r = random.Next(min, max);
            return r;
        }
        /// <summary>
        /// kiểm tra một chuỗi đưa ra có phải là số hay không?
        /// </summary>
        /// <param name="numberAsString"></param>
        /// <returns></returns>
        public static bool IsNumber(string numberAsString)
        {
            if (string.IsNullOrEmpty(numberAsString)) return false;
            numberAsString = numberAsString.Trim();
            double numberTest;
            var isNumber = double.TryParse(numberAsString, out numberTest);
            return isNumber;
        }
        /// <summary>
        /// Bitwise AND on 32-bit Integer
        /// Ví dụ: 1 & 5 == 1, 4 & 5 = 4, 2 & 5 = 0
        /// Giá trị để kiểm tra: 2^0, 2^1, 2^2, 2^3 ....
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool IsCheck(int source, int target)
        {
            return (source & target) == target;
        }
        /// <summary>
        /// Chuyển đổi bytes sang KB, MB, GB, TB...
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToSizeString(double bytes)
        {
            var culture = CultureInfo.CurrentUICulture;
            const string format = "#,0.0";

            if (bytes < 1024)
                return bytes.ToString("#,0", culture);
            bytes /= 1024;
            if (bytes < 1024)
                return bytes.ToString(format, culture) + " KB";
            bytes /= 1024;
            if (bytes < 1024)
                return bytes.ToString(format, culture) + " MB";
            bytes /= 1024;
            if (bytes < 1024)
                return bytes.ToString(format, culture) + " GB";
            bytes /= 1024;
            return bytes.ToString(format, culture) + " TB";
        }
    }
}