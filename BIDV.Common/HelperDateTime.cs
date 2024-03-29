﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BIDV.Common
{
    public class HelperDateTime
    {
        public static string getMonth(DateTime CreateDate)
        {
            var result = string.Empty;
            var th = string.Empty;
            var getday = DateTime.UtcNow - CreateDate;
            if (getday.Days < 30 && getday.Days >= 0)
            {
                if (getday.Days == 0)
                {
                    result = "1 ngày";
                }
                else
                {
                    result = getday.Days.ToString() + " ngày";
                }
            }
            else
            {
                result = (getday.Days / 30).ToString() + " tháng";
            }
            return result;
        }
        /// <summary>
        /// Hiển thị thứ ngày tháng tiếng việt
        /// </summary>
        /// <param name="dt">Thời gian hiện tại</param>
        /// <returns></returns>
        public static string DateTimeToVn(DateTime dt)
        {
            var thu = new[] { "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật" };
            var dayinWeek = (int)dt.DayOfWeek;
            var stringDatetime = string.Format("{0} ngày {1} tháng {2} năm {3}", thu[dayinWeek], dt.Day, dt.Month, dt.Year);
            return stringDatetime;
        }
        /// <summary>
        /// Đếm số ngày giữa 2 khoảng thời gian
        /// </summary>
        /// <param name="fromdate">Ngày bắt đầu</param>
        /// <param name="todate">Ngày kết thúc</param>
        /// <returns>Số ngày giữa 2 khoảng thời gian</returns>
        public static int CountDayBetween2Time(DateTime fromdate, DateTime todate)
        {
            // Difference in days, hours, and minutes.
            var ts = todate - fromdate;
            // Difference in days.
            var differenceInDays = ts.Days;
            return differenceInDays;
        }
        /// <summary>
        /// Convert DateTime định dạng dd/mm/yyyy sang mm/dd/yyyy
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ConvertDateTime(string dt)
        {
            var arrDate = dt.Split('/').Select(Int32.Parse).ToList();
            return new DateTime(arrDate[2], arrDate[1], arrDate[0]);
        }

        /// <summary>
        /// Convert mm/dd/yyyy thành DateTime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(string dt)
        {
            var arrDate = dt.Split('/').Select(Int32.Parse).ToList();
            return new DateTime(arrDate[2], arrDate[0], arrDate[1]);
        }
        /// <summary>
        /// Thời gian trước
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string TimeAgo(DateTime dt)
        {
            var span = DateTime.Now - Convert.ToDateTime(dt);
            if (span.Days > 365)
            {
                var years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format(" {0} {1} trước",
                years, "năm");
            }
            if (span.Days > 30)
            {
                var months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format(" {0} {1} trước",
                months, "tháng");
            }
            if (span.Days > 0)
                return String.Format(" {0} {1} trước",
                span.Days, "ngày");
            if (span.Hours > 0)
                return String.Format(" {0} {1} trước",
                span.Hours, "giờ");
            if (span.Minutes > 0)
                return String.Format(" {0} {1} trước",
                span.Minutes, "phút");
            if (span.Seconds > 5)
                return String.Format(" {0} giây trước", span.Seconds);
            return span.Seconds <= 5 ? "vừa xong" : string.Empty;
        }
        /// <summary>
        /// Chuyển đổi kiểu timespan 1462416264 sang DateTime  
        /// 06.05.2016 05:56:27
        /// </summary>
        /// <returns></returns>
        public static DateTime ConvertTimespan2DateTime(int timespan)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(((timespan) / 1000d) * 1000).ToLocalTime();
            return dt;
        }
        /// <summary>
        /// Chuyển đổi datetime sang string Date 2016/05/20
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns>YYYY/mm/dd</returns>
        public static string DateTimeToyyyymmdd(int timespan)
        {
            return ConvertTimespan2DateTime(timespan).ToString("yyyy/M/d");
        }

        /// <summary>
        /// Chuyển đổi datetime sang string Date 20/05/2016
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns>dd/MM/yyyy</returns>
        public static string DateTimeToddMMyyyy(int timespan)
        {
            return ConvertTimespan2DateTime(timespan).ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// Chuyển đổi datetime sang string Date 05/20/2016
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns>MM/dd/yyyy</returns>
        public static string DateTimeToMMddyyyy(int timespan)
        {
            return ConvertTimespan2DateTime(timespan).ToString("MM/dd/yyyy");
        }
        /// <summary>
        /// Chuyển đổi datetime sang string Date 20/05/2016 12:14:16
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns>dd/MM/yyyy HH:mm:ss</returns>
        public static string TimeSpan2DateTime(int timespan)
        {
            return ConvertTimespan2DateTime(timespan).ToString("dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Chuyển đổi DateTime sang TimeStamp
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>log</returns>
        public static Int64 Convert2TimeStamp(DateTime dt)
        {
            var tsp = (int)(dt.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds;
            return tsp;

        }
        //lay thoi gian update cua bai dang
        public string getFormatDate(DateTime Dateinput)
        {
            System.DateTime createDate = Dateinput;
            System.DateTime now = DateTime.Now;
            System.TimeSpan diff = now.Subtract(createDate);
            string date = "";

            double totalHours = diff.TotalHours;
            if (1 < totalHours && totalHours < 24)
            {
                date = Math.Floor(totalHours) + " giờ trước";
            }
            if (totalHours < 1)
            {
                if (diff.TotalMinutes < 1)
                {
                    date = "vài giây trước";
                }
                else
                {
                    date = Math.Floor(diff.TotalMinutes) + " phút trước";
                }
            }
            if (totalHours > 24 && (Dateinput.Day + 1) == now.Day)
            {
                date = "Hôm qua lúc " + Dateinput.ToString("HH:ss");
            }
            if (totalHours > 24 && (Dateinput.Day + 1) != now.Day)
            {
                date = Dateinput.Day + " Tháng " + Dateinput.Month + " lúc " + Dateinput.ToString("HH:ss");
            }
            return date;
        }
        //lay thoi gian cuoi cua ngay hien tai
        public static DateTime EndOfDay(DateTime time)
        {             
            return new DateTime(time.Year, time.Month, time.Day, 23,59,59);
        }
        //lay thoi gian bat dau cua ngay hien tai
        public static DateTime StartOfDay(DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, 0, 0, 0);
        }
         /// <summary>
        /// Chuyển đổi string sang Date 05/20/2016
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns>MM/dd/yyyy</returns>
        public static DateTime ParseExact(string s, string format="dd/MM/yyyy")
        {
            try
            {
                DateTime temp;
                var ok = DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out temp);
                return !ok ? DateTime.MinValue : temp;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
          /// <summary>
        /// Chuyển đổi string sang Datetime 
        /// </summary>
        /// <param name="timespan"></param>
        /// <returns>MM/dd/yyyy hhmmss</returns>
        public static DateTime ToDateTime(string s)
        {
            try
            {
                return Convert.ToDateTime(s); 
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }
    }
}
