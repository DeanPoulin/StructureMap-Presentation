using System.Security.Cryptography;
using System.Text;

namespace DeanIS.Net.Utilities
{
    public static class HashingExtensions
    {
        private static readonly MD5 Md5Hasher = MD5.Create();

        public static string ToHexString(this byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string ToMD5Hash(this string input)
        {
            return Md5Hasher.ComputeHash(Encoding.ASCII.GetBytes(input)).ToHexString();
        }
    }
}
