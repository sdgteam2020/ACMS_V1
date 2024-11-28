using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IHSDC.WebApp.RepositryManager
{
    public class EncryptionManager
    {
        public static string Encryption(string input)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(input);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;

        }
        public static string Decryption(string input)
        {

            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(input);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;

        }


    }
}