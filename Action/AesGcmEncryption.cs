using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Action
{
    public class AesGcmEncryption
    {
        public byte[] DecryptWithBouncyCastle(byte[] payload, byte[] aesKey)
        {
            byte[] realPayload = new byte[payload.Length - 12];
            byte[] nonce = new byte[12];

            Buffer.BlockCopy(payload, 0, nonce, 0, 12); // get the first 12 bytes as nonce
            Buffer.BlockCopy(payload, 12, realPayload, 0, payload.Length - 12); // get the rest as the payload

            var cipher = new GcmBlockCipher(new AesEngine());
            cipher.Init(false, new AeadParameters(new KeyParameter(aesKey), 128, nonce));

            var clearBytes = new byte[cipher.GetOutputSize(realPayload.Length)];
            int len = cipher.ProcessBytes(realPayload, 0, realPayload.Length, clearBytes, 0);
            cipher.DoFinal(clearBytes, len);

            return clearBytes;
        }
        public byte[] EncryptWithBouncyCastle(string plaintext, byte[] key)
        {
            const int nonceLength = 12; // in bytes
            const int tagLenth = 16; // in bytes

            var nonce = new byte[nonceLength];
            RandomNumberGenerator.Fill(nonce);

            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            var bcCiphertext = new byte[plaintextBytes.Length + tagLenth];

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), tagLenth * 8, nonce);
            cipher.Init(true, parameters);

            var offset = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, bcCiphertext, 0);
            cipher.DoFinal(bcCiphertext, offset);

            // Bouncy Castle includes the authentication tag in the ciphertext
            var ciphertext = new byte[plaintextBytes.Length];
            var tag = new byte[tagLenth];
            Buffer.BlockCopy(bcCiphertext, 0, ciphertext, 0, plaintextBytes.Length);
            Buffer.BlockCopy(bcCiphertext, plaintextBytes.Length, tag, 0, tagLenth);

            //byte[] rv = new byte[nonce.Length + ciphertext.Length];
            //Buffer.BlockCopy(nonce, 0, rv, 0, nonce.Length);
            //Buffer.BlockCopy(ciphertext, 0, rv, nonce.Length, ciphertext.Length);
            return ciphertext;
        }
        public (byte[] ciphertext, byte[] nonce, byte[] tag) EncryptWithBouncyCastle1(string plaintext, byte[] key)
        {
            const int nonceLength = 12; // in bytes
            const int tagLenth = 16; // in bytes

            var nonce = new byte[nonceLength];
            RandomNumberGenerator.Fill(nonce);

            var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
            var bcCiphertext = new byte[plaintextBytes.Length + tagLenth];

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), tagLenth * 8, nonce);
            cipher.Init(true, parameters);

            var offset = cipher.ProcessBytes(plaintextBytes, 0, plaintextBytes.Length, bcCiphertext, 0);
            cipher.DoFinal(bcCiphertext, offset);

            // Bouncy Castle includes the authentication tag in the ciphertext
            var ciphertext = new byte[plaintextBytes.Length];
            var tag = new byte[tagLenth];
            Buffer.BlockCopy(bcCiphertext, 0, ciphertext, 0, plaintextBytes.Length);
            Buffer.BlockCopy(bcCiphertext, plaintextBytes.Length, tag, 0, tagLenth);

            return (ciphertext, nonce, tag);
        }
        public string DecryptWithBouncyCastle1(byte[] ciphertext, byte[] nonce, byte[] tag, byte[] key)
        {
            var plaintextBytes = new byte[ciphertext.Length];

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(new KeyParameter(key), tag.Length * 8, nonce);
            cipher.Init(false, parameters);

            var bcCiphertext = ciphertext.Concat(tag).ToArray();

            var offset = cipher.ProcessBytes(bcCiphertext, 0, bcCiphertext.Length, plaintextBytes, 0);
            cipher.DoFinal(plaintextBytes, offset);

            return Encoding.UTF8.GetString(plaintextBytes);
        }
    }
}
