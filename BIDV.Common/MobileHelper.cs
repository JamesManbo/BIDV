using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Canavi.Common
{
    public class Common
    {
        public static string GenerateNonce()
        {
            var random = new Random();
            return random.Next(1, 99999999).ToString().PadLeft(8);
        }

        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString("N").ToLower();
        }

        public static string GenerateCodeFromId(long id, int padLeft = 1)
        {
            string c = "123456789qwertyuiopasdfghjklzxcvbnm".ToUpper();
            string code = string.Empty;
            int length = c.Length;
            long index = id;
            while (true)
            {
                int lastIndex = (int)(index % length) - 1;
                if (lastIndex < 0)
                {
                    lastIndex = c.Length - 1;
                }
                code = c[lastIndex] + code;
                if (index > length)
                {
                    index = index / length;
                    if (index < length)
                    {
                        code = c[(int)index - 1] + code;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            code = code.PadLeft(padLeft, '0');
            return code;
        }

        public static bool IsMobileTabletBrowser(string userAgent)
        {
            return IsMobileBrowser(userAgent) || IsTabletBrowser(userAgent);
        }

        /// <summary>
        /// Nhận diện điện thoại di động theo User-Agent
        /// </summary>
        /// <param name="userAgent">User-Agent</param>
        /// <returns>true nếu client là điện thoại di động, false trong trường hợp ngược lại</returns>
        public static bool IsMobileBrowser(string userAgent)
        {
            Regex b =
                new Regex(
                    @"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v =
                new Regex(
                    @"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-",
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return !String.IsNullOrWhiteSpace(userAgent) &&
                   (b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4)));
        }

        /// <summary>
        /// Nhận diện máy tính bảng theo User-Agent
        /// </summary>
        /// <param name="userAgent">User-Agent</param>
        /// <returns>True nếu là máy tính bảng, false trong trường hợp ngược lại</returns>
        public static bool IsTabletBrowser(string userAgent)
        {
            return !String.IsNullOrWhiteSpace(userAgent) &&
                   Regex.IsMatch(userAgent,
                       @"iPad|GT-P|SCH-I800|Nexus\s(10|7)|Tablet|PlayBook|Xoom|Kindle|Silk|KFAPWI|Android(?!.*Mobile)",
                       RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }

        public static bool IsEmail(ref string emailaddress)
        {
            try
            {
                emailaddress = ReplaceSpace(emailaddress);
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsMobile(ref string mobile)
        {
            try
            {
                mobile = ReplaceSpace(mobile);
                return IsViettel(ref mobile) || IsMobifone(ref mobile) || IsVinaphone(ref mobile) || IsVietnamobile(ref mobile) || IsSphone(ref mobile) || IsGmobile(ref mobile);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static readonly string[] ViettelMobile = { "086", "096", "097", "098", "0162", "0163", "0164", "0165", "0166", "0167", "0168", "0169" };
        public static bool IsViettel(ref string mobile)
        {
            foreach (var s in ViettelMobile)
            {
                if (mobile.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly string[] MobifoneMobile = { "090", "093", "0120", "0121", "0122", "0126", "0128" };
        public static bool IsMobifone(ref string mobile)
        {
            foreach (var s in MobifoneMobile)
            {
                if (mobile.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly string[] VinaphoneMobile = { "091", "094", "0123", "0124", "0125", "0127", "0129" };
        public static bool IsVinaphone(ref string mobile)
        {
            foreach (var s in VinaphoneMobile)
            {
                if (mobile.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly string[] VietnamobileMobile = { "0186", "0188", "092" };
        public static bool IsVietnamobile(ref string mobile)
        {
            foreach (var s in VietnamobileMobile)
            {
                if (mobile.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly string[] SphoneMobile = { "095" };
        public static bool IsSphone(ref string mobile)
        {
            foreach (var s in SphoneMobile)
            {
                if (mobile.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
        private static readonly string[] GmobileMobile = { "0199", "0993", "0994", "0995", "0996", "0997" };
        public static bool IsGmobile(ref string mobile)
        {
            foreach (var s in GmobileMobile)
            {
                if (mobile.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
        public static string ReplaceSpace(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return string.Empty;
            }
            return Regex.Replace(txt, @"\s+", string.Empty);
        }

        public static string SetDomainCdn(string domain, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            if (path.StartsWith("http"))
            {
                return path;
            }
            return domain + path;
        }

        public static string SetDomainCdnAndSite(string domain, string path, int with, int height)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            string url = SetDomainCdn(domain, path);
            int index = url.LastIndexOf("/", StringComparison.Ordinal) + 1;
            url = url.Insert(index, $"{with}_{height}_resize_");
            return url;
        }

        public static string FormatAmount(decimal? value)
        {
            if (value == null) return "";
            if (value.Value < 1000)
                return value.ToString();
            else
                return string.Format("{0:0,0}", value).Replace(",", ".");
        }
    }
}
