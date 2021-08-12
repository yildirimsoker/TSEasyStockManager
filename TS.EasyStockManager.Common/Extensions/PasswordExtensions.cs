using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TS.EasyStockManager.Common.Extensions
{
    public static class PasswordExtensions
    {
        public static string MD5Hash(this string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                strBuilder.Append(result[i].ToString("x2"));
            return strBuilder.ToString();
        }
    }
}
