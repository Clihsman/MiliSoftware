/***************************************
 * Author : Clihsman Iscala            *
 * Company : All Software Company      *
 * Creation date : 24/10/2022          *
 * *************************************/

using MiliEngine.SqlLite;
using MiliSoftware.Model.Database;
using MiliSoftware.Model.Json;
using MiliSoftware.Model.Security;
using MiliSoftware.Model.WebServices;
using System;
using System.IO;
using System.Windows;

namespace MiliSoftware.Login.Model
{
    public class LoginModel
    {
        private string name;
        private string password;

        public LoginModel()
        {
            name = "";
            password = "";
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }

        public string GetName() {
            return name;
        }

        public string GetPassword() {
            return password;
        }

        public bool Login() {

            //     User[] users;
            //   users = JsonUtils.FromJson<User[]>(MiliDatabase.GetValue(""));
            //    WebService.InitServices("abc", "123");
            //  WebService.AddQuery("autoritation", Encrypt.GetMD5(name + password));

            SqlLiteDatabase sqlLiteDatabase = new SqlLiteDatabase();
            sqlLiteDatabase.Open();
            var elements = sqlLiteDatabase.ExecuteQuery(@"SELECT * FROM users");
            foreach (object[] rows in elements)
            {
                Console.WriteLine(rows[0]);
                Console.WriteLine(rows[1]);
                Console.WriteLine(rows[2]);
            }
            sqlLiteDatabase.Close();

            RSAEncryptServices encryptServices = new RSAEncryptServices(File.ReadAllText("publicKey.txt"), null);
            RSADecryptServices decryptServices = new RSADecryptServices(File.ReadAllText("privateKey.txt"), null);

            string en = encryptServices.EncryptString("Hola, Mundo!");

          //  rsa = RCACryptoServices.DecodeRSAPrivateKey(File.ReadAllText("privateKey.txt"));
            MessageBox.Show(decryptServices.DecryptString(en));

           // RCACryptoServices.Init();
         //   MessageBox.Show(RCACryptoServices.GetPrivatePEM());

            //  Console.WriteLine(WebService.GetString("apiRest/"));
            //string md5 = Encrypt.GetMD5("1234");

            //   Console.WriteLine(md5);
            /*
        foreach (User user in users)
        {
            System.Console.WriteLine("{0} {1}",user.name,user.password);
            if (user.name == name && user.password == password)
            {

                return true;
            }
        }
        */
            return true;
        }
    }
}