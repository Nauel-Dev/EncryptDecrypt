using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace encryptAndDecrypt
{
    internal class Program
    {
        static void Main(string[] args)

        {

            Console.WriteLine("Enter username: ");
          string username=  Console.ReadLine();

            Console.WriteLine("Enter Password: ");
          string password=  Console.ReadLine();




            byte[] key=new byte[16];
            byte[] iv=new byte[16];

            using(RandomNumberGenerator rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
             
            }

            //encrypt
            byte[] encryptedPassword= Encrypt(password,key,iv);  
            string encrptedPasswordString=Convert.ToBase64String(encryptedPassword);
            Console.WriteLine("Encrypted Password: {0}", encrptedPasswordString);

            //decrypt
            string decryptedPassword=Decrypt(encryptedPassword,key,iv);
            Console.WriteLine("Decrypted password: "+ decryptedPassword);
            Console.ReadLine();

            Console.ReadLine();
        }

        //static byte[] Encrypt(string simpletext,byte[]key,byte[]iv)
        //{

        //    byte[] cipheredtext;
        //    using (Aes aes = Aes.Create())
        //    {
        //        ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, iv);

        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter sw = new StreamWriter(cs))
        //                {
        //                    sw.Write(simpletext);

        //                }
        //                cipheredtext = ms.ToArray();
        //            }
                 
        //        }
               

        //    }
        //    return cipheredtext;
        //}





        static byte[] Encrypt(string simpletext,byte[] key, byte[] iv)
        {
            byte[] cipheredText;

            using(Aes aes=Aes.Create())
            {
                ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, iv);
                using (MemoryStream ms=new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write)) { 
                    
                    
                    using (StreamWriter sw=new StreamWriter(cs))
                        {
                            sw.Write(simpletext);
                        }
                    
                    
                    
                    };
                    cipheredText = ms.ToArray();
                }
            }


            return cipheredText;
        }




        static string Decrypt(byte[] cipheredtext, byte[] key, byte[] iv)
        {
            string simpletext=string.Empty;
            using(Aes aes = Aes.Create())
            {
                ICryptoTransform decrypt= aes.CreateDecryptor(key, iv);

                using(MemoryStream ms = new MemoryStream(cipheredtext))
                {
                    using(CryptoStream cs=new CryptoStream(ms,decrypt,CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {



                            simpletext = sr.ReadToEnd();
                        }
                    }
                }

            }
            return simpletext;
        }
        


    }
}
