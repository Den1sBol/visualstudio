using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace lab11
{
    class User
    {
        public string Login { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }
        public string[] Roles { get; set; }
    }

    public class GenerateHashSalt
    {
        public static byte[] GenerateSalt()
        {
            using (var ranNumGen = new RNGCryptoServiceProvider())
            {
                var randNumber = new byte[32];
                ranNumGen.GetBytes(randNumber);
                return randNumber;
            }
        }
        public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds, HashAlgorithmName hashAlgorithm, int NumberOfBytes)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds, hashAlgorithm))
            {
                return rfc2898.GetBytes(NumberOfBytes);
            }
        }
    }
    class Protector
    {
        private static Dictionary<string, User> users = new Dictionary<string, User>();
        public static void Regist(string userName, string password, string[] roles = null)
        {
            if (users.ContainsKey(userName))
            {
                Console.WriteLine("Таке ім'я вже існує");
            }
            else
            {
                var user = new User();
                user.Login = userName; user.Salt = GenerateHashSalt.GenerateSalt(); user.PasswordHash = GenerateHashSalt.HashPassword(Encoding.Default.GetBytes(password), user.Salt, 1000, HashAlgorithmName.SHA512, 32); user.Roles = roles;
                users.Add(user.Login, user);
                Console.WriteLine("Користувач " + user.Login + "є у базі даних");
            }
        }
    }
}
