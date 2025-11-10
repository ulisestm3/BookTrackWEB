using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class AlgoritmoRSA
    {
        public static string Encrypt(string data, string publicKey)
        {
            using RSA rsa = RSA.Create();
            // Importar la llave pública
            rsa.ImportParameters(GetKeyParameters(publicKey));

            // Convertir la cadena a bytes
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);

            // Encriptar los datos
            byte[] encryptedData = rsa.Encrypt(dataToEncrypt, RSAEncryptionPadding.OaepSHA256);

            // Convertir los datos encriptados a una cadena base64
            return Convert.ToBase64String(encryptedData);
        }

        public static string Decrypt(string encryptedData, string privateKey)
        {
            using RSA rsa = RSA.Create();
            // Importar la llave privada
            rsa.ImportParameters(GetKeyParameters(privateKey));

            // Convertir la cadena base64 a bytes
            byte[] dataToDecrypt = Convert.FromBase64String(encryptedData);

            // Desencriptar los datos
            byte[] decryptedData = rsa.Decrypt(dataToDecrypt, RSAEncryptionPadding.OaepSHA256);

            // Convertir los datos desencriptados a una cadena
            return Encoding.UTF8.GetString(decryptedData);
        }

        public static string GetKeyString(RSAParameters keyParameters)
        {
            using var rsa = RSA.Create();
            rsa.ImportParameters(keyParameters);
            return rsa.ToXmlString(true);
        }

        public static RSAParameters GetKeyParameters(string key)
        {
            using var rsa = RSA.Create();
            rsa.FromXmlString(key);
            return rsa.ExportParameters(true);
        }

    }
}

//using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
//{
//    // Obtener la clave pública y privada
//    RSAParameters publicKey = rsa.ExportParameters(false); // Clave pública
//    RSAParameters privateKey = rsa.ExportParameters(true); // Clave privada

//    // Convertir las claves a formato XML
//    string publicKeyXml = rsa.ToXmlString(false);
//    string privateKeyXml = rsa.ToXmlString(true);

//    // Imprimir las claves
//    Console.WriteLine("Clave pública:");
//    Console.WriteLine(publicKeyXml);

//    Console.WriteLine("\nClave privada:");
//    Console.WriteLine(privateKeyXml);
//}