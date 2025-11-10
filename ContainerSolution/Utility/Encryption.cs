using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public interface IEncryption
    {
        public byte[] EncryptText(byte[] key, string plainText);
        public string DecryptText(byte[] key, byte[] encryptedBytes);
        public string EncryptToHex(string plainText, string key);
        public string DecryptFromHex(string hexString, string key);
    }
    public class Encryption:IEncryption
    {
        public byte[] EncryptText(byte[] key, string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.GenerateIV();

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new())
                {
                    using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    byte[] encryptedBytes = new byte[aesAlg.IV.Length + msEncrypt.ToArray().Length];
                    Buffer.BlockCopy(aesAlg.IV, 0, encryptedBytes, 0, aesAlg.IV.Length);
                    Buffer.BlockCopy(msEncrypt.ToArray(), 0, encryptedBytes, aesAlg.IV.Length, msEncrypt.ToArray().Length);

                    return encryptedBytes;
                }
            }
        }

        public string DecryptText(byte[] key, byte[] encryptedBytes)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;

                byte[] iv = new byte[aesAlg.IV.Length];
                Buffer.BlockCopy(encryptedBytes, 0, iv, 0, aesAlg.IV.Length);
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new(encryptedBytes, aesAlg.IV.Length, encryptedBytes.Length - aesAlg.IV.Length))
                {
                    using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public string EncryptToHex(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.Mode = CipherMode.ECB; // Modo de cifrado (se puede cambiar según tus necesidades)
                aesAlg.Padding = PaddingMode.PKCS7; // Relleno del bloque (se puede cambiar según tus necesidades)

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encrypted;

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }

                // Convertir el arreglo de bytes cifrado a una cadena hexadecimal
                return BitConverter.ToString(encrypted).Replace("-", "");
            }
        }


        public string DecryptFromHex(string hexString, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.Mode = CipherMode.ECB; // Modo de cifrado (debe ser el mismo que se usó para cifrar)
                aesAlg.Padding = PaddingMode.PKCS7; // Relleno del bloque (debe ser el mismo que se usó para cifrar)

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Convertir la cadena hexadecimal a un arreglo de bytes
                byte[] cipherBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < cipherBytes.Length; i++)
                {
                    cipherBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }

                string plaintext = null;

                using (var msDecrypt = new System.IO.MemoryStream(cipherBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return plaintext;
            }
        }


    }
}
