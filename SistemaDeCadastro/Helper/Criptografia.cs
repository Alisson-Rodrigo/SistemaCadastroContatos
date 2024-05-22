using System.Security.Cryptography;
using System.Text;

namespace SistemaDeCadastro.Helper
{
    public static class Criptografia
    {
        public static string GerarHash(this string pass) {
            var hash = SHA256.Create();
            var encoder = Encoding.UTF8;
            var array = encoder.GetBytes(pass);
            array = hash.ComputeHash(array);
            var strHexa = new StringBuilder();

            foreach (byte item in array)
            {
                strHexa.Append(item.ToString("X2"));
            }

            return strHexa.ToString();
        }

    }
}