using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public interface ISHA256
    {
        public byte[] Sha256(string input);
        public string GenerateRandomToken();

    }
    public class SHA256:ISHA256
    {
        public byte[] Sha256(string input)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(input));
            }
        }
        public string GenerateRandomToken()
        {
            byte[] token = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(token);
            }

            return BitConverter.ToString(token).Replace("-", "");
        }
    }
}
