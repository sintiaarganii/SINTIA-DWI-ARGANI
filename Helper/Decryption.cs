using System.Security.Cryptography;
using System.Text;

namespace SINTIA_DWI_ARGANI.Helper
{
    public class Decryption
    {
        public string Decrypt(string encrypt, string key)
        {
            using (TripleDESCryptoServiceProvider tripleDESCryptoService = new TripleDESCryptoServiceProvider())
            {
                using (MD5CryptoServiceProvider hashMD5Provider = new MD5CryptoServiceProvider())
                {
                    byte[] byteHash = { };// hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(key));
                    tripleDESCryptoService.Key = byteHash;
                    tripleDESCryptoService.Mode = CipherMode.ECB;

                    byte[] data = Convert.FromBase64String(encrypt);
                    byte[] decryptedData = tripleDESCryptoService.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);

                    return Encoding.UTF8.GetString(decryptedData);
                }
            }
        }
    }
}
