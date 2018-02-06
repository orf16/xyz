using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Usuarios
{
    internal class LNGeneraPassword
    {
        private const int SaltByteSize = 24;
        private const int HashByteSize = 24;
        private const int HasingIterationsCount = 10101;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saltByteSize"></param>
        /// <returns></returns>
        internal static byte[] GenerarSalt(int saltByteSize = SaltByteSize)
        {
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[saltByteSize];
                saltGenerator.GetBytes(salt);
                return salt;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="hashByteSize"></param>
        /// <returns></returns>
        internal static byte[] CalcularHash(string password, byte[] salt, int iterations = HasingIterationsCount, int hashByteSize = HashByteSize)
        {
            using (Rfc2898DeriveBytes hashGenerator = new Rfc2898DeriveBytes(password, salt))
            {
                hashGenerator.IterationCount = iterations;
                return hashGenerator.GetBytes(hashByteSize);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordSalt"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        internal static bool VerificarPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            byte[] computedHash = CalcularHash(password, passwordSalt);
            return AreHashesEqual(computedHash, passwordHash);
        }

        //Length constant verification - prevents timing attack
        private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
        {
            int minHashLenght = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
            var xor = firstHash.Length ^ secondHash.Length;
            for (int i = 0; i < minHashLenght; i++)
                xor |= firstHash[i] ^ secondHash[i];
            return 0 == xor;
        }

        /// <summary>
        /// Genera una clave dinámica para asignarla temporalmente a los usuarios
        /// que han sido aprobados.
        /// </summary>
        /// <param name="caracteresPasswordTemporal"></param>
        /// <param name="longitudPasswordTemporal"></param>
        /// <returns></returns>
        public static string GenerarPasswordTemporalAleatorio(string caracteresPasswordTemporal, string longitudPasswordTemporal)
        {
            Random random = new Random();
            int maxValue = 0;
            int num = 0;
            var arrayCaracteresEspeciales = new string[] { ",", "*", "+", "-", ";", "(", ")", "[", "]", "@", "#", "$" };
            string text = string.Empty;
            if (!string.IsNullOrEmpty(caracteresPasswordTemporal))
            {
                maxValue = caracteresPasswordTemporal.Length;
            }
            if (!string.IsNullOrEmpty(longitudPasswordTemporal))
            {
                num = Convert.ToInt32(longitudPasswordTemporal);
            }
            for (int i = 0; i < 6; i++)
            {
                text += caracteresPasswordTemporal[random.Next(maxValue)].ToString();
            }
            for (int i = 0; i < 3; i++)
            {
                text += random.Next(0, 10).ToString();
            }
            text += arrayCaracteresEspeciales[random.Next(arrayCaracteresEspeciales.Length)].ToString();
            return text;
        }
    }
}
