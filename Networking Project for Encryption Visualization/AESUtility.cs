using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AESUT
{
    public class AESUtility
    {
        private Aes aes;

        // Constructor to initialize AES
        public AESUtility()
        {
            aes = Aes.Create();
            aes.KeySize = 128;  // Set the key size (128, 192, or 256 bits)
            aes.BlockSize = 128;  // Block size in bits (AES works with 128-bit blocks)
        }

        // Method to generate a random key and IV (Initialization Vector)
        public void GenerateKeys()
        {
            aes.GenerateKey();
            aes.GenerateIV();
        }

        // Method to encrypt a message
        public string Encrypt(string plainText)
        {
            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                using (var writer = new StreamWriter(cryptoStream))
                {
                    writer.Write(plainText);
                }

                byte[] cipherText = memoryStream.ToArray();
                // Return the encrypted text as a Base64 string
                return Convert.ToBase64String(aes.IV) + ":" + Convert.ToBase64String(cipherText);
            }
        }

        // Method to decrypt a message
        public string Decrypt(string cipherText)
        {
            string[] parts = cipherText.Split(':');
            byte[] iv = Convert.FromBase64String(parts[0]);
            byte[] cipherBytes = Convert.FromBase64String(parts[1]);

            using (var decryptor = aes.CreateDecryptor(aes.Key, iv))
            using (var memoryStream = new MemoryStream(cipherBytes))
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            using (var reader = new StreamReader(cryptoStream))
            {
                return reader.ReadToEnd();
            }
        }

        // Get the key and IV (for displaying or saving)
        public string GetKey()
        {
            return Convert.ToBase64String(aes.Key);
        }

        public string GetIV()
        {
            return Convert.ToBase64String(aes.IV);
        }
    }
}
