using System;
using System.Security.Cryptography;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random(5613);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(random.Next(0, 50));
            }
            Console.WriteLine("****************************");
            Random random1 = new Random(5613);
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(random1.Next(0, 50));
            }
            Console.WriteLine("****************************");
            static byte[] GetCrypto(int length = 10)
            {
                var Gen = new RNGCryptoServiceProvider();
                var Num = new byte[length];
                Gen.GetBytes(Num);
                return Num;
            }
            for (int i = 0; i <5;i++)
            {
                string crypto = Convert.ToBase64String(GetCrypto());
                Console.WriteLine(crypto);
            }
        }
    }
}
