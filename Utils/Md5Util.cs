using System.Security.Cryptography;
using System.Text;

namespace Utils
{
    public class Md5Util
    {
        public static string Encrypt(string input)
        {
            try
            {
                using MD5 md5 = MD5.Create();
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
