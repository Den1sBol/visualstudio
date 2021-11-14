using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab7._2
{
    class Program
    {
        public void AssignNewKey(string publicKeyPath = "publicKey.xml", string privateKeyPath = "privateKey.xml")
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
                File.WriteAllText(privateKeyPath, rsa.ToXmlString(true));
            }
        }

        public byte[] EncData(byte[] dataToEncrypt, string publicKeyPath = "publicKey.xml")
        {
            byte[] cipbyt;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cipbyt = rsa.Encrypt(dataToEncrypt, false);
            }
            return cipbyt;
        }
        public byte[] DecData(byte[] dataToEncrypt, string privateKeyPath = "privateKey.xml")
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(privateKeyPath));
                plain = rsa.Decrypt(dataToEncrypt, false);
            }
            return plain;
        }
        static void Main(string[] args)
        {
            var rsaParams = new Program();
            const string original = "The future belongs to those who believe in the beauty of their dreams";
            rsaParams.AssignNewKey();
            var encrypted = rsaParams.EncData(Encoding.UTF8.GetBytes(original));
            var decrypted = rsaParams.DecData(encrypted);
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
