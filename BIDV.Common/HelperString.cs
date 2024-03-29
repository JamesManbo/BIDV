﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BIDV.Common
{
    public class HelperString
    {
        /// <summary>
        /// Url đẹp marketting, SEO
        /// </summary>
        /// <param name="strUnicode">Chuỗi tiếng việt có dấu</param>
        /// <returns>Trả lại một chuỗi không dấu</returns>
        public static string ToFriendlyUrl(string text)
        {
            Regex regex = new Regex("[^\\d\\w]+");
            text = regex.Replace(text.ToLower(), "-").Trim(new char[]
            {
                '-'
            });
            text = ToUnsign(text);

            return text;
        }
        /// <summary>
        /// Chuyển đổi chữ tiếng việt có dấu sang không dấu
        /// </summary>
        /// <param name="strUnicode">Chuỗi tiếng việt có dấu</param>
        /// <returns>Trả lại một chuỗi không dấu</returns>
        public static string UnsignCharacter(string text)
        {
            var pattern = new string[7];

            pattern[0] = "a|(á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ)";

            pattern[1] = "o|(ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ)";

            pattern[2] = "e|(é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ)";

            pattern[3] = "u|(ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ)";

            pattern[4] = "i|(í|ì|ỉ|ị|ĩ)";

            pattern[5] = "y|(ý|ỳ|ỷ|ỵ|ỹ)";

            pattern[6] = "d|đ";

            for (int i = 0; i < pattern.Length; i++)
            {

                // kí tự sẽ thay thế

                char replaceChar = pattern[i][0];

                MatchCollection matchs = Regex.Matches(text, pattern[i]);

                foreach (Match m in matchs)
                {

                    text = text.Replace(m.Value[0], replaceChar);

                }

            }

            return text;

        }

        /// <summary>
        /// Giới hạn số lượng từ trong một chuỗi
        /// </summary>
        /// <param name="fullText">Chuỗi truyền vào</param>
        /// <param name="maxLength">số lượng từ</param>
        /// <returns>Chuỗi đã giới hạn từ</returns>
        public static string TruncateByWord(string fullText, int maxLength)
        {
            if (fullText == null || fullText.Length < maxLength)
                return fullText;
            var iNextSpace = fullText.LastIndexOf(" ", maxLength, StringComparison.Ordinal);
            return string.Format("{0}...", fullText.Substring(0, (iNextSpace > 0) ? iNextSpace : maxLength).Trim());
        }

        /// <summary>
        /// Hàm lấy thông tin file ảnh và đường dẫn ảnh
        /// </summary>
        /// <param name="timespan"> Ngày tạo bài viết</param>
        /// <param name="imageFile">Tên file ảnh</param>
        /// <param name="imageSize">Kích thước file ảnh. Lấy thông tin từ hàng số trong Class </param>
        /// <param name="category"> Nhóm tin tức/khuyến mại</param>
        /// <returns> Đường dẫn file ảnh của tin bài</returns>
        public static string GetImageUrl( int timespan, string imageFile,string imageSize,string category)
        {
            const string startStr = "/Content/FrontEnd/_img_server/";
            string result = string.Empty;
            if (imageFile != null)
            {
                result = new StringBuilder(startStr + category + "/" + HelperDateTime.ConvertTimespan2DateTime(timespan).ToString("yyyy") + "/" +
                                  HelperDateTime.ConvertTimespan2DateTime(timespan).ToString("MM") + "/" +
                                  HelperDateTime.ConvertTimespan2DateTime(timespan).ToString("dd") + "/" + imageSize + "/" + imageFile).ToString();
            }
            return result;
        }
        public static string AsToLower(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return string.Empty;
            return item.ToString().Trim().ToLower();
        }
        public static string AsToUpper(this object item)
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return string.Empty;
            return item.ToString().Trim().ToUpper();
        }
        
        

    }
}
