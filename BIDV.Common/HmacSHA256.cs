using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public class HmacSHA256
    {
		public static string HashString(string StringToHash, string HachKey)
		{
			System.Text.UTF8Encoding myEncoder = new System.Text.UTF8Encoding();
			byte[] Key = myEncoder.GetBytes(HachKey);
			byte[] Text = myEncoder.GetBytes(StringToHash);
			System.Security.Cryptography.HMACSHA256 myHMACSHA256 = new System.Security.Cryptography.HMACSHA256(Key);
			byte[] HashCode = myHMACSHA256.ComputeHash(Text);
			string hash = BitConverter.ToString(HashCode).Replace("-", "");
			return hash.ToLower();
		}
	}
}
