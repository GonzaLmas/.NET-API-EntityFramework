using System.Security.Cryptography;
using System.Text;

namespace BalearesChallenge.Services
{
    public class Utilidades
    {
        public static string EncriptarClaveRegistro(string clave, out string salt)
        {
            // Genera el salt
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            salt = Convert.ToBase64String(saltBytes);

            // Concatena la clave con el salt
            string claveConSalt = clave + salt;

            // Crea el hash
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(claveConSalt));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string EncriptarClaveLogin(string clave, string salt)
        {
            // Concatena la clave con el salt
            string claveConSalt = clave + salt;

            // Crea el hash
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(claveConSalt));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
