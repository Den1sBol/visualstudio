using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] Data = File.ReadAllBytes("lab2.txt").ToArray();
            foreach(byte i in Data)
            {
                Console.Write(i);
                Console.Write(" ");
            }
            Console.WriteLine();

            var password = new RNGCryptoServiceProvider();
            byte[] pass = new byte[Data.Length];
            password.GetBytes(pass);
            foreach (byte i in pass)
            {
                Console.Write(i);
                Console.Write(" ");
            }
            Console.WriteLine();

            byte[] enc = new byte[Data.Length];
            for (int i = 0; i < Data.Length; i++)
            {
                enc[i] = (byte)(Data[i] ^ pass[i]);
            }

            File.WriteAllBytes("encrypt.dat", enc);
            byte[] text = File.ReadAllBytes("encrypt.dat").ToArray();
            byte[] dec = new byte[text.Length];
            for (int i=0; i<text.Length;i++)
            {
                dec[i] = (byte)(text[i] ^ pass[i]);
            }

            Console.Write(Encoding.UTF8.GetString(dec));
            Console.WriteLine();
            Console.WriteLine();

        }
    }
}
