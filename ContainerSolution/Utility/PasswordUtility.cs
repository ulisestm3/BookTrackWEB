namespace Utility
{
    public interface IPasswordUtility
    {
        public bool ValidatePassword(string password, sbyte longitud = 8);
        public string GetPassword(byte longitud);
    }
    public class PasswordUtility:IPasswordUtility
    {
        public bool ValidatePassword(string password,sbyte longitud = 8)
        {
            // Verificar la longitud mínima
            if (password.Length < longitud)
            {
                return false;
            }

            // Verificar la presencia de caracteres alfanuméricos
            if (!password.Any(char.IsDigit) || !password.Any(char.IsLetter))
            {
                return false;
            }

            // Verificar la presencia de letras mayúsculas y minúsculas
            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower))
            {
                return false;
            }

            // Verificar la presencia de caracteres especiales
            if (!password.Any(c => !char.IsLetterOrDigit(c)))
            {
                return false;
            }

            // La contraseña cumple con los criterios de complejidad
            return true;
        }
        public string GetPassword(byte longitud)
        {
            const string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_";
            Random rng = new Random();

            char[] caracteres = new char[longitud];
            for (int i = 0; i < longitud; i++)
            {
                caracteres[i] = caracteresPermitidos[rng.Next(caracteresPermitidos.Length)];
            }

            return new string(caracteres);
        }
    }
}
