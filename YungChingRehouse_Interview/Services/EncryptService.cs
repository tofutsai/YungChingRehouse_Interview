using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using static YungChingRehouse_Interview.Services.AccountService;

namespace YungChingRehouse_Interview.Services
{
    public class EncryptService : IEncryptService
    {
        public int SaltLength { get; private set; }
        public int HashLength { get; private set; }
        public int Iteration { get; private set; }

        public EncryptService() : this(256, 256, 5)
        {

        }
        public EncryptService(int saltLength, int hashLength, int iteration)
        {
            SaltLength = saltLength;
            HashLength = hashLength;
            Iteration = iteration;
        }
        /// <summary>
        /// 轉Base64編碼
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        /// <summary>
        /// 產生隨機salt
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                return deriveBytes.GetBytes(length);
            }
        }

        /// <summary>
        /// 產生token
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserToken GenerateUserToken(string password)
        {
            var encodePass = Base64Encode(password);
            var salt = GenerateSalt(SaltLength);
            var hash = GenerateHash(Convert.FromBase64String(encodePass), salt, Iteration, HashLength);

            UserToken token = new UserToken
            {
                PasswordSalt = Convert.ToBase64String(salt),
                PasswordHash = Convert.ToBase64String(hash)
            };

            return token;
        }
        /// <summary>
        /// 驗證密碼是否正確
        /// </summary>
        /// <param name="inputHash">DB內hash的密碼</param>
        /// <param name="inputSalt">DB內的salt</param>
        /// <param name="password">用戶輸入的密碼</param>
        /// <returns></returns>
        public bool IsValidPassword(string inputHash, string inputSalt, string password)
        {
            var encodePass = Base64Encode(password);
            var hash = GenerateHash(Convert.FromBase64String(encodePass), Convert.FromBase64String(inputSalt), Iteration, HashLength);
            var hasString = Convert.ToBase64String(hash);
            return inputHash.Equals(hasString);
        }
    }
}