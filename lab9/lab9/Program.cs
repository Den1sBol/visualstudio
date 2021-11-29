using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace lab9
{
    class DigitSign
    {
        public static byte[] ComputeHashSha512(byte[] toBeHashed)
        {
            using (var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(toBeHashed);
            }
        }

        private readonly static string CspContainerName = "Container";
        public void AssignNewKey()
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
            File.WriteAllText("BolshakovDenis.xml", rsa.ToXmlString(false));
        }

        public byte[] SignData(byte[] hashOfDataToSign)
        {

            var cspParameters = new CspParameters
            {
                KeyContainerName = CspContainerName,

            };

            using (var rsa = new RSACryptoServiceProvider(cspParameters))
            {
                rsa.PersistKeyInCsp = true;
                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                rsaFormatter.SetHashAlgorithm("SHA512");
                return rsaFormatter.CreateSignature(hashOfDataToSign);
            }
        }
        public bool VerifySignature(byte[] hashOfDataToSign, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText("BolshakovDenis.xml"));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA512");
                return rsaDeformatter.VerifySignature(hashOfDataToSign, signature);
            }
        }

        static void Main(string[] args)
        {
            var document = Encoding.UTF8.GetBytes("Bolshakov Denis");
            byte[] hashedDocument = ComputeHashSha512(document);
            var digitalSignature = new DigitSign();
            digitalSignature.AssignNewKey();
            var signature = digitalSignature.SignData(hashedDocument);
            var verified = digitalSignature.VerifySignature(hashedDocument, signature);


            Console.WriteLine("Digital Signature Demonstration in .NET");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" Original Text = " +
            Encoding.Default.GetString(document));
            Console.WriteLine();
            Console.WriteLine(" Digital Signature = " +
            Convert.ToBase64String(signature));
            Console.WriteLine(verified
            ? "The digital signature has been correctly verified."
            : "The digital signature has NOT been correctly verified.");


        }

    }
}