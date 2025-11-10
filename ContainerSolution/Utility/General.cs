using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace Utility
{
    public interface IGeneral
    {
        public byte ValidateByte(object posibleNumero);
        public short ValidateShort(object posibleNumero);
        public int ValidateInt(object posibleNumero);
        public Int64 ValidateInt64(object posibleNumero);
        public decimal ValidateDecimal(object? posibleNumero);
        public float ValidateFloat(object posibleNumero);
        public bool ValidateBool(object posibleBool);
        public bool ValidateEmail(string? email);
        public bool EnviarCorreo(string from, string to, string subject, string body);



    }
    public class General : IGeneral
    {
        public byte ValidateByte(object posibleNumero)
        {
            byte numero = 0;
            if (posibleNumero != null)
            {
                string? auxiliar = Convert.ToString(posibleNumero);
                if (byte.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public short ValidateShort(object posibleNumero)
        {
            short numero = 0;
            if (posibleNumero != null)
            {
                string? auxiliar = Convert.ToString(posibleNumero);
                if (short.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public int ValidateInt(object posibleNumero)
        {
            int numero = 0;
            if (posibleNumero != null)
            {
                string? auxiliar = Convert.ToString(posibleNumero);
                if (int.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public Int64 ValidateInt64(object posibleNumero)
        {
            Int64 numero = 0;
            if (posibleNumero != null)
            {
                string? auxiliar = Convert.ToString(posibleNumero);
                if (Int64.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public decimal ValidateDecimal(object? posibleNumero)
        {
            decimal numero = 0;
            if (posibleNumero != null)
            {
                string? auxiliar = Convert.ToString(posibleNumero);
                if (decimal.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public float ValidateFloat(object posibleNumero)
        {
            float numero = 0;
            if (posibleNumero != null)
            {
                string? auxiliar = Convert.ToString(posibleNumero);
                if (float.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public bool ValidateBool(object posibleBool)
        {
            bool numero = false;
            if (posibleBool != null)
            {
                string? auxiliar = Convert.ToString(posibleBool);
                if (bool.TryParse(auxiliar, out numero))
                {
                    return numero;
                }
            }
            return numero;
        }
        public bool ValidateEmail(string? email)
        {
            if (!(string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email)))
            {
                if (email.Length > 0)
                {
                    string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                    return Regex.IsMatch(email, pattern);
                }
            }
            return false;
        }
        public static string ObtenerSiguienteVersion(string versionActual)
        {
            // Si no hay versión actual, iniciar en "A"
            if (string.IsNullOrWhiteSpace(versionActual))
            {
                return "A";
            }

            // Validar que solo sea una letra y esté en el rango A-Z
            if (versionActual.Length != 1 || !char.IsLetter(versionActual[0]) || !char.IsUpper(versionActual[0]))
            {
                throw new ArgumentException("La versión actual debe ser una letra mayúscula de A a Z.");
            }

            // Convertir a carácter y obtener la siguiente letra
            char letraActual = versionActual[0];

            // Si la versión es "Z", no hay siguiente (o podríamos reiniciar a "A", según el requerimiento del cliente)
            if (letraActual == 'Z')
            {
                throw new InvalidOperationException("No se pueden generar versiones más allá de 'Z'.");
            }

            // Calcular la siguiente versión
            char nuevaVersion = (char)(letraActual + 1);

            return nuevaVersion.ToString();
        }

        public bool EnviarCorreo(string from, string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(from) || string.IsNullOrWhiteSpace(from) || string.IsNullOrEmpty(to) || string.IsNullOrWhiteSpace(to))
            {
                return false;
            }

            int smtpPort = 587;
            const string smtpServer = "smtp-relay.brevo.com";
            const string smtpUsername = "srg.dex@gmail.com";
            const string smtpPassword = "xsmtpsib-92f6fff586ee9bf8dcc4801ca3ddad7405ab59c7f2eafc91c1ea8d4548e3c39f-a8tbfSGhAnyOgwjJ";

            try
            {
                MailMessage mail = new(from, to, subject, body);
                SmtpClient smtpClient = new(smtpServer, smtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };
                smtpClient.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}

