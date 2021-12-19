using Action;
using Entity;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ProjectPercobaan
{
    class Program
    {
        const string rsaPrivateKeyLocation = @"C:\Users\nfd96\PycharmProjects\pythonProject2\myprivkey.pem";
        const string rsaPublicKeyLocation = @"C:\Users\nfd96\PycharmProjects\pythonProject2\mypubkey.pem";
        static void Main(string[] args)
        {
            RgAction.RG0();
        }
        
        
        public static (RsaPrivateCrtKeyParameters privKey, RsaKeyParameters pubKey) GenerateRsaKey()
        {
            var random = new SecureRandom();
            int keySize = 2048;
            var keyGenerationParameters = new KeyGenerationParameters(random, keySize);
            RsaKeyPairGenerator generator = new RsaKeyPairGenerator();
            generator.Init(keyGenerationParameters);

            var keyPair = generator.GenerateKeyPair();
            RsaPrivateCrtKeyParameters privKey = (RsaPrivateCrtKeyParameters)keyPair.Private;
            RsaKeyParameters pubKey = (RsaKeyParameters)keyPair.Public;
            return (privKey, pubKey);
        }
        public static void Rsa_Encryption_With_Generate_Key()
        {
            var encryptAction = new RsaEncryptionAction();

            var key = GenerateRsaKey();
            string plainText = "Bang Jago";
            // Encryption
            string encryptedData = encryptAction.RsaOaep256Encrypt(key.pubKey, plainText);
            //Decryption
            string decryptedData = encryptAction.RsaOaep256Decrypt(encryptedData, key.privKey);

            Console.WriteLine("plain text \n" + plainText + "\n");
            Console.WriteLine("encrypted data \n" + encryptedData + "\n");
            Console.WriteLine("decrypted data \n" + decryptedData + "\n");
        }
        public static void Rsa_Encryption_Without_Generate_Key()
        {
            var encryptAction = new RsaEncryptionAction();
            string plainText = "Bang Jago";
            // Encryption
            //string publicKeyFromFile = ReadFileAction.ReadFromPath(@"\lead_api\RSAPublicKey.pem");
            string publicKeyFromFile = ReadFileAction.ReadFile(rsaPublicKeyLocation);

            RsaKeyParameters rsaPublicKey = encryptAction.CreatePublicKeyFromPemString(publicKeyFromFile);
            string encryptedData = encryptAction.RsaOaep256Encrypt(rsaPublicKey, plainText);

            //Decryption
            //string stringPrivateKey = ReadFileAction.ReadFromPath(@"\lead_api\RSAPrivateKey.pem");
            string stringPrivateKey = ReadFileAction.ReadFile(rsaPrivateKeyLocation);

            RsaPrivateCrtKeyParameters privateKey = encryptAction.CreatePrivateKeyFromPemString(stringPrivateKey);
            string decryptedData = encryptAction.RsaOaep256Decrypt(encryptedData, privateKey);

            Console.WriteLine("plain text \n" + plainText + "\n");
            Console.WriteLine("encrypted data \n" + encryptedData + "\n");
            Console.WriteLine("decrypted data \n" + decryptedData + "\n");
        }
        public static void Aes_Gcm_Encryption()
        {
            AesGcmEncryption aesGcmEncryption = new AesGcmEncryption();
            var key = new byte[32];
            RandomNumberGenerator.Fill(key);

            //var dataEncrypt = aesGcmEncryption.EncryptWithBouncyCastle1("Vosadsadsadsadsadsadbab", key);
            var dataEncrypt = aesGcmEncryption.EncryptWithBouncyCastle("Vosadsadsadsadsadsadbab", key);
            //var dataDecrypt = aesGcmEncryption.DecryptWithBouncyCastle1(dataEncrypt.ciphertext,dataEncrypt.nonce, dataEncrypt.tag, key);
            var dataDecrypt = aesGcmEncryption.DecryptWithBouncyCastle(dataEncrypt, key);
            Console.WriteLine(dataDecrypt);
        }
    }
    public class Data
    {
        public string Departement { get; set; }
        public List<int> EmployeeId { get; set; }
    }
}
