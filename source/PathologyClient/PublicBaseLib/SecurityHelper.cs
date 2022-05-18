using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PublicBaseLib
{
    public class SecurityHelper
    {


        #region 非对称加密算法

        /// <summary>
        /// 生成 RSA 公钥和私钥
        /// </summary>
        /// <param name="publicKey"> 公钥 </param>
        /// <param name="privateKey"> 私钥 </param>
        private static void GenerateRsaKeys(out string publicKey, out string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="publickey"> 公钥 </param>
        /// <param name="content"> 待加密的内容 </param>
        /// <returns> 经过加密的字符串 </returns>
        public static string RsaEncrypt(string publickey, string content)
        {
            Encoding encoding = Encoding.UTF8;

            var rsa = new RSACryptoServiceProvider();

            rsa.FromXmlString(publickey);

            var cipherbytes = rsa.Encrypt(encoding.GetBytes(content), false);

            var base64Str = Convert.ToBase64String(cipherbytes);

            var Retstr = HttpUtility.UrlEncode(HttpUtility.UrlEncode(base64Str, System.Text.Encoding.GetEncoding(65001)), System.Text.Encoding.GetEncoding(65001));

            return Retstr;
        }

        /// <summary>
        /// RSA 解密
        /// </summary>
        /// <param name="privatekey"> 私钥 </param>
        /// <param name="content"> 待解密的内容 </param>
        /// <param name="encoding"></param>
        /// <returns> 解密后的字符串 </returns>
        public static string RsaDecrypt(string privatekey, string content)
        {
            Encoding encoding = Encoding.UTF8;
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privatekey);
            var base64Str = HttpUtility.UrlDecode(HttpUtility.UrlDecode(content, System.Text.Encoding.GetEncoding(65001)), System.Text.Encoding.GetEncoding(65001));
            var cipherbytes = rsa.Decrypt(Convert.FromBase64String(base64Str), false);
            return encoding.GetString(cipherbytes);
        }

        #endregion 非对称加密算法
    }
}
