using System;
using System.Security.Cryptography;
using System.Text;

namespace lab7._1
{
    class Program
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                _privateKey = rsa.ExportParameters(true);
            }
        }
        public byte[] EncData(byte[] dataToEncrypt)
        {
            byte[] cipbyt;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_publicKey);
                cipbyt = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipbyt;
        }
        public byte[] DecData(byte[] dataToEncrypt)
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(_privateKey);
                plain = rsa.Decrypt(dataToEncrypt, true);
            }
            return plain;
        }
        static void Main(string[] args)
        {

            var rsaParameters = new Program();
            const string original = "The future belongs to those who believe in the beauty of their dreams";
            rsaParameters.AssignNewKey();
            var encrypted = rsaParameters.EncData(Encoding.UTF8.GetBytes(original));
            var decrypted = rsaParameters.DecData(encrypted);
            Console.WriteLine(" Original Text = " + original);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Encrypted Text = " + Convert.ToBase64String(encrypted));
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(" Decrypted Text = " + Encoding.Default.GetString(decrypted));
        }
    }
}
