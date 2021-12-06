using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Text;

namespace Action
{
    public class RsaEncryptionAction
    {
        public RsaPrivateCrtKeyParameters CreatePrivateKeyFromPemString(string pem)
        {
            PemReader pr = new PemReader(new StringReader(pem));
            var key = (RsaKeyParameters)pr.ReadObject();
            RsaPrivateCrtKeyParameters privateKey = (RsaPrivateCrtKeyParameters)key;
            return privateKey;
        }
        public RsaKeyParameters CreatePublicKeyFromPemString(string pem) 
        {
            var pemReader = new PemReader(new StringReader(pem));
            var publicKey = (RsaKeyParameters)pemReader.ReadObject();
            return publicKey;
        }
        public string RsaOaep256Encrypt(RsaKeyParameters rsaPublicKey, string plainText)
        {
            var plain = Encoding.UTF8.GetBytes(plainText);
            var encrypter = new OaepEncoding(new RsaEngine(), new Sha256Digest(), new Sha256Digest(), null);
            encrypter.Init(true, rsaPublicKey);
            var cipher = encrypter.ProcessBlock(plain, 0, plain.Length);
            return Convert.ToBase64String(cipher);
        }
        public string RsaOaep256Decrypt(string base64EncryptedData, RsaPrivateCrtKeyParameters rsaPrivateKey)
        {
            var decrypter = new OaepEncoding(new RsaEngine(), new Sha256Digest(), new Sha256Digest(), null);
            decrypter.Init(false, rsaPrivateKey);

            byte[] encryptedData = Convert.FromBase64String(base64EncryptedData);
            byte[] decryptedData = decrypter.ProcessBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
