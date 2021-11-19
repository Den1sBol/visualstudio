using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace lab8
{
    class Program
    {

        private readonly static string CspContainerName = "container";
        public static void AssignNewKey(string publicKeyPath)
        {
            CspParameters cspParameters = new CspParameters(1)
            {
                KeyContainerName = CspContainerName,

                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            var rsa = new RSACryptoServiceProvider(cspParameters)
            {
                PersistKeyInCsp = true
            };
            File.WriteAllText(publicKeyPath, rsa.ToXmlString(false));
        }
        public static byte[] DecryptData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,

            };
            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, false);
            }
            return plainBytes;
        }
        public static byte[] EncryptData(string publicKeyPath, byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            return cipherbytes;
        }
        static void Main(string[] args)
        {
            //byte[] data = File.ReadAllBytes("encryptMessage.dat").ToArray();
            //string dat = BitConverter.ToString(data);
            string secret = "Good morning!";
            AssignNewKey("BolshakovDenis.xml");
            //var encrypted = EncryptData("BolshakovArtem.xml", Encoding.Unicode.GetBytes(secret));
            //var decrypted = DecryptData(data);
            //File.WriteAllBytes("secretMessage.dat", encrypted);
            //Console.WriteLine("Message = " + dat);
            //Console.WriteLine();
            //Console.WriteLine("Encrypted message = " + Convert.ToBase64String(encrypted));
            //Console.WriteLine();
            //Console.WriteLine("Decrypted message = " + Encoding.Unicode.GetString(decrypted));
        }
    }
}
