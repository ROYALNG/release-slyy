using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security.Cryptography;
using System.IO;
using System.Collections.Specialized;

namespace GHMQS.Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string lic = "JifA3QZ0DLPltRj/s8sGL+rjawRlhGYx4PmRw+9hRmQfewUzP1A5bEHyA1JjF76A7eVO8NDYxT/Z5jQLW6n6IcCte0vkDwslqVOu5yBd+A8vJ2QEpy3Df2D8ANrF5rTCE0zK2w6yxYPvhquC19nI/+6zVVLtCEFdaIFvYWcQBxvJyzWMTSJqMCFiOmM+IW6e";
            //string error = ""; 
            //ApplyLicense(lic, out error);
            //return;
            string oauthurl = "http://192.168.88.160:9011";
            string rbacurl ="http://192.168.88.160:9013";
            string mnsurl = "http://192.168.88.160:9004";
            string rtdburl = "http://192.168.88.160:9005"; //Create Queue
            //MQSClient client = new MQSClient(url, "280C3D25EBD94DC198E2A7FCFB326793", "");
            GHNETBASE.OAUTH.OAuthHelper.SingletonInstance.Login(oauthurl, rbacurl, "GH-IOServer", "GH-IOServer", "admin", "123456");

            string var = GHNETBASE.RTDB.RTDBHelper.SingletonInstance.ReadVariableValue(rtdburl, "chl001", "ctrl001", "VAR001");

            bool r= GHNETBASE.MNS.MNSHelper.SingletonInstance.WriteQueueMessage(mnsurl, "testQueue003","测试");
            string rs = GHNETBASE.MNS.MNSHelper.SingletonInstance.PopQueueMessage(mnsurl, "testQueue003");
            rs = GHNETBASE.MNS.MNSHelper.SingletonInstance.ReadQueueMessage(mnsurl, "testQueue003");


            //var mqsqueue = client.getQueue("/api/testQueue003");

            //dynamic ret = mqsqueue.setAttribute();

            //ret = mqsqueue.getAttribute();

            //ret = mqsqueue.sendMessage("hello world", 0, 8);

            //var ret = mqsqueue.popMessage();

            //mqsqueue.popMessageAsync(new Action<MessageReceiveResponse>(res =>
            //{
            //    var r = res;
            //}));

            //var ret = mqsqueue.peekMessage();

            //ret = mqsqueue.changeVisibility("receipt", 0);

            //ret = mqsqueue.deleteMessage("receipt");
        }
        public static NameValueCollection DataToNVC(string data)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            if (!string.IsNullOrEmpty(data))
            {
                string[] array = data.Split(new char[]
		{
			'|'
		});
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string text = array2[i];
                    if (!string.IsNullOrEmpty(text))
                    {
                        string[] array3 = text.Split(new char[]
				{
					','
				});
                        if (array3.Length == 2)
                        {
                            nameValueCollection.Add(array3[0], array3[1]);
                        }
                    }
                }
            }
            return nameValueCollection;
        }


        public static bool ApplyLicense(string license, out string error)
        {
            error = "";
            try
            {
                string a = EncryptData("MaxServers,1000|Expires,637310305518367645|FingerPrint,E9B3-083D-B906-89A4-A13D-9493-53C6-BC42|LicenseKey,2b464061-3f26-4462-8d2d-73f10b6afda7",
                    Encoding.UTF8.GetString(Convert.FromBase64String("YmYwYjA5N2YtZTI3MC00M2NmLWE5M2ItMjNlYzE1MDlmNjYy")));
                string b = EncryptData("MaxServers,1|Expires,635712279415630000|FingerPrint,E9B3-083D-B906-89A4-A13D-9493-53C6-BC42|LicenseKey,2b464061-3f26-4462-8d2d-73f10b6afda7",
                    Encoding.UTF8.GetString(Convert.FromBase64String("YmYwYjA5N2YtZTI3MC00M2NmLWE5M2ItMjNlYzE1MDlmNjYy")));

                string data = DecryptData(license, Encoding.UTF8.GetString(Convert.FromBase64String("YmYwYjA5N2YtZTI3MC00M2NmLWE5M2ItMjNlYzE1MDlmNjYy")));
                


                NameValueCollection nameValueCollection = DataToNVC(data);
                string fp = nameValueCollection["FingerPrint"];
                //if (fp != Licensing.Value)
                //{
                //    Shared.c = "";
                //    Shared.e = 0;
                //    Shared.b = DateTime.MinValue;
                //    throw new Exception("Fingerprint mismatch");
                //}
                //Shared.c = nameValueCollection["LicenseKey"];
                //Shared.e = Convert.ToInt32(nameValueCollection["MaxServers"]);
                //Shared.b = new DateTime(Convert.ToInt64(nameValueCollection["Expires"]));
                //try
                //{
                //    Licensing licensing = new Licensing();
                //    licensing.CheckCompleted += new CheckCompletedEventHandler(Shared.a);
                //    licensing.CheckAsync(fp);
                //}
                //catch (Exception)
                //{
                //}
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            return true;
        }

        public static string EncryptData(string data, string password)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] inArray = EncryptData(Encoding.UTF8.GetBytes(data), password, PaddingMode.ISO10126);
            return Convert.ToBase64String(inArray);
        }

        // iSpyCentral.EncDec
        public static string DecryptData(string data, string password)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] data2 = Convert.FromBase64String(data);
            byte[] bytes = DecryptData(data2, password, PaddingMode.ISO10126);
            return Encoding.UTF8.GetString(bytes);
        }

        // iSpyCentral.EncDec
        public static byte[] EncryptData(byte[] data, string password, PaddingMode paddingMode)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("data");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, Encoding.UTF8.GetBytes("Salt"));
            ICryptoTransform transform = new RijndaelManaged
            {
                Padding = paddingMode
            }.CreateEncryptor(passwordDeriveBytes.GetBytes(16), passwordDeriveBytes.GetBytes(16));
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                    result = memoryStream.ToArray();
                }
            }
            return result;
        }


        // iSpyCentral.EncDec
        public static byte[] DecryptData(byte[] data, string password, PaddingMode paddingMode)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("data");
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, Encoding.UTF8.GetBytes("Salt"));
            ICryptoTransform transform = new RijndaelManaged
            {
                Padding = paddingMode
            }.CreateDecryptor(passwordDeriveBytes.GetBytes(16), passwordDeriveBytes.GetBytes(16));
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
                {
                    byte[] array = new byte[data.Length];
                    int num = cryptoStream.Read(array, 0, array.Length);
                    if (num < array.Length)
                    {
                        byte[] array2 = new byte[num];
                        Buffer.BlockCopy(array, 0, array2, 0, num);
                        result = array2;
                    }
                    else
                    {
                        result = array;
                    }
                }
            }
            return result;
        }
    }
}
