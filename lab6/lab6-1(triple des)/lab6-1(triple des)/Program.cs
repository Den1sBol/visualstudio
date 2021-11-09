using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab6_1_triple_des_
{
    class Program
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using (var ranNumGen = new RNGCryptoServiceProvider())
            {
                var ranNum = new byte[length];
                ranNumGen.GetBytes(ranNum);
                return ranNum;
            }
        }
        public byte[] Encrypt(byte[] dataToEncrypt,
                              byte[] key,
                              byte[] iv)
        {
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(
                    memoryStream,
                    des.CreateEncryptor(),
                    CryptoStreamMode.Write);
                    cryptoStream.Write(dataToEncrypt, 0,
                    dataToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    return memoryStream.ToArray();
                }
            }
        }
        public byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
        {
            using (var des = new TripleDESCryptoServiceProvider())
            {
                des.Mode = CipherMode.CBC;
                des.Padding = PaddingMode.PKCS7;
                des.Key = key;
                des.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream =
                        new CryptoStream(memoryStream,
                        des.CreateDecryptor(),
                        CryptoStreamMode.Write);
                    cryptoStream.Write(dataToDecrypt, 0,
                    dataToDecrypt.Length);
                    cryptoStream.FlushFinalBlock();
                    var decryptBytes = memoryStream.ToArray();
                    return decryptBytes;
                }
            }
        }
        static void Main(string[] args)
        {
            var tripledes = new Program();
            var key = tripledes.GenerateRandomNumber(24);
            var iv = tripledes.GenerateRandomNumber(8);
            const string original = "Osnovi inform bezpeki";
            var encrypted = tripledes.Encrypt(
            Encoding.UTF8.GetBytes(original), key, iv);
            var decrypted = tripledes.Decrypt(encrypted, key, iv);
            var decryptedMessage = Encoding.UTF8.GetString(decrypted);
            Console.WriteLine("TripleDES Encryption in .NET");
            Console.WriteLine();
            Console.WriteLine("Original Text = " + original);
            Console.WriteLine("Encrypted Text = " +
            Convert.ToBase64String(encrypted));
            Console.WriteLine("Decrypted Text = " + decryptedMessage);
            Console.ReadLine();

        }
    }
}
