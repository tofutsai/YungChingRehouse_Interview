using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YungChingRehouse_Interview.Services.AccountService;

namespace YungChingRehouse_Interview.Services
{
    public interface IEncryptService
    {
        /// <summary>
        /// 轉Base64編碼
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string Base64Encode(string plainText);
        /// <summary>
        /// 產生隨機salt
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] GenerateSalt(int length);
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length);
        /// <summary>
        /// 產生token
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        UserToken GenerateUserToken(string password);

        /// <summary>
        /// 驗證密碼是否正確
        /// </summary>
        /// <param name="inputHash"></param>
        /// <param name="inputSalt"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool IsValidPassword(string inputHash, string inputSalt, string password);
    }
}
