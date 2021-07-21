using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace G1_Website_Tour.Models
{
    public class Encode
    {
        public static string EncodeMD5(string key)
        {
            MD5 md5 = MD5.Create();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(key));
            StringBuilder sHash = new StringBuilder();
            foreach (byte b in bHash)
            {
                sHash.Append(string.Format("{0:x2}", b));
            }
            return sHash.ToString();
        }
    }
}