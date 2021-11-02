using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace lab5_4
{
    public class PBKDF2
    {
        public static byte[] GenerateSalt()
        {
            using (var ranNumGen = new RNGCryptoServiceProvider())
            {
                var ranNum = new byte[32];
                ranNumGen.GetBytes(ranNum);
                return ranNum;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, System.Security.Cryptography.HashAlgorithmName hashAlgorithm)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(20);
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            const string passwordToHash = "KCML2015_2020";
            HashPassword(passwordToHash, 20000);
            HashPassword(passwordToHash, 70000);
            HashPassword(passwordToHash, 120000);
            HashPassword(passwordToHash, 170000);
            HashPassword(passwordToHash, 220000);
            HashPassword(passwordToHash, 270000);
            HashPassword(passwordToHash, 320000);
            HashPassword(passwordToHash, 370000);
            HashPassword(passwordToHash, 420000);
            HashPassword(passwordToHash, 470000);
            Console.ReadLine();
        }
        private static void HashPassword(string passwordToHash, int numberOfRounds)
        {
            var sw = new Stopwatch();
            sw.Start();
            var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.GenerateSalt(), numberOfRounds, HashAlgorithmName.SHA512);
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Password to hash : " + passwordToHash);
            Console.WriteLine("Hashed Password : " + Convert.ToBase64String(hashedPassword));
            Console.WriteLine("Iterations <" + numberOfRounds + "> Elapsed Time: " + sw.ElapsedMilliseconds + "ms");


        }
    }
}
