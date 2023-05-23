using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
//using System.Management;
using Microsoft.Win32;
using System.IO;
using GetHardId;
using GHIBMS.Common;
using System.Management;

namespace GHIBMS.Pub
{
    public class RSAHelper
    {
        string prikey = "<RSAKeyValue><Modulus>2L2w//jGZu2NMc8prCgapSF5/Hx4UHgW2rBUq8yWksaIJrn+gAeau1EX7/IttuhVRuFawGEgh4IJBgbUQ9QnYrjZn5WojqGsR1xoT2em3osKM8CQ7dcGrIUIndVh4CnjaOURGrwj5CcyBwmF13atWYuEGRaSxJY30eHFa+Ip1H0=</Modulus><Exponent>AQAB</Exponent><P>92HkLr5vaPne08ctHjyzKvSEEJavoXBKIeyRvrZd4X6TkcM0BZ0fmPVzX/Iuc2hLlnZelN/EOlzklTAyiUY3zw==</P><Q>4EqLv2csWf14MIe3vE4Sl6/RJYaosczmTZ/L1nqlb1DQDgq8NzvI9Z0K/1YflcLiD5naxYdrsjO/72utPfs18w==</Q><DP>iU39D3DFd3eQlOzs/uZj74iNsINicfFYRCIA9uBTlS/jCjlVK3R9MDGz2uIZpBaUNav3bRwR1u7uNFvvAMBHgw==</DP><DQ>WsgRdDEwVGQxZ4MnLQJ0qAyznHq6gOysMrMA8BjIKRwOegCCWeDK4A7mSp7zPcyZbzMYx2aegoxLnX55qmKpXQ==</DQ><InverseQ>xcn1WfViua/rCliAqM3pIO1+ysy3G2+TYL+4BYM46o8jxHxidOyJ4OAq36gntbdibGBylT08CCrqNLyAxfwONw==</InverseQ><D>O4g24sghLspW+vRs/NVzJCJC0GAb5/ZkSKMgTqP+Q8h2QzTZO69bB9JpYcXCWrgMHFDvzWGgPrgv5FDh4tOWEV0oPSukozMavd1k9chWjFQW7v5Wax3srMF2BGvdP/v8LMpufXDDVsIAJ9ii2GmU1JvMrQqRad8DhUlOjSYi6X0=</D></RSAKeyValue>";

        /// <summary>

        /// 生成公私钥
        /// </summary>

        /// <param name="PrivateKeyPath"></param>

        /// <param name="PublicKeyPath"></param>

        public void RSAKey(string PrivateKeyPath, string PublicKeyPath)
        {
            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                this.CreatePrivateKeyXML(PrivateKeyPath, provider.ToXmlString(true));

                this.CreatePublicKeyXML(PublicKeyPath, provider.ToXmlString(false));

            }

            catch (Exception exception)
            {

                Console.WriteLine(exception.ToString());

            }

        }


        /// <summary>

        /// 对原始数据进行MD5加密

        /// </summary>

        /// <param name="m_strSource">待加密数据</param>

        /// <returns>返回机密后的数据</returns>

        public string GetHash(string m_strSource)
        {

            HashAlgorithm algorithm = HashAlgorithm.Create("MD5");

            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(m_strSource);

            byte[] inArray = algorithm.ComputeHash(bytes);

            return Convert.ToBase64String(inArray);

        }

        /// <summary>

        /// RSA加密

        /// </summary>

        /// <param name="xmlPublicKey">公钥</param>

        /// <param name="m_strEncryptString">MD5加密后的数据</param>

        /// <returns>RSA公钥加密后的数据</returns>

        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {

            string str2 = "";

            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                provider.FromXmlString(xmlPublicKey);

                byte[] bytes = new UnicodeEncoding().GetBytes(m_strEncryptString);

                str2 = Convert.ToBase64String(provider.Encrypt(bytes, false));

            }

            catch
            {

                //Console.WriteLine( exception.ToString());

            }

            return str2;

        }
        public string RSADecrypt(string m_strDecryptString)
        {
            return RSADecrypt(prikey, m_strDecryptString);
        }

        /// <summary>

        /// RSA解密

        /// </summary>

        /// <param name="xmlPrivateKey">私钥</param>

        /// <param name="m_strDecryptString">待解密的数据</param>

        /// <returns>解密后的结果</returns>

        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {

            string str2 = "";

            try
            {

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();

                provider.FromXmlString(xmlPrivateKey);

                byte[] rgb = Convert.FromBase64String(m_strDecryptString);

                byte[] buffer2 = provider.Decrypt(rgb, false);

                str2 = new UnicodeEncoding().GetString(buffer2);

            }

            catch (Exception exception)
            {

                Console.WriteLine("RSADecrypt解密出错，可能没有读取到待解密的数据"+exception.ToString());

            }

            return str2;

        }

        /// <summary>

        /// 对MD5加密后的密文进行签名

        /// </summary>

        /// <param name="p_strKeyPrivate">私钥</param>

        /// <param name="m_strHashbyteSignature">MD5加密后的密文</param>

        /// <returns></returns>

        public string SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature)
        {

            byte[] rgbHash = Convert.FromBase64String(m_strHashbyteSignature);

            RSACryptoServiceProvider key = new RSACryptoServiceProvider();

            key.FromXmlString(p_strKeyPrivate);

            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);

            formatter.SetHashAlgorithm("MD5");

            byte[] inArray = formatter.CreateSignature(rgbHash);

            return Convert.ToBase64String(inArray);

        }

        /// <summary>

        /// 签名验证

        /// </summary>

        /// <param name="p_strKeyPublic">公钥</param>

        /// <param name="p_strHashbyteDeformatter">待验证的用户名[机器码]</param>

        /// <param name="p_strDeformatterData">注册码</param>

        /// <returns></returns>

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            try
            {

                byte[] rgbHash = Convert.FromBase64String(p_strHashbyteDeformatter);

                RSACryptoServiceProvider key = new RSACryptoServiceProvider();

                key.FromXmlString(p_strKeyPublic);

                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);

                deformatter.SetHashAlgorithm("MD5");


                byte[] rgbSignature = Convert.FromBase64String(p_strDeformatterData);

                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {

                    return true;

                }

                return false;

            }

            catch
            {

                return false;

            }

        }

        ///// <summary>

        ///// 获取硬盘ID

        ///// </summary>

        ///// <returns>硬盘ID</returns>

        //public string GetHardID()
        //{

        //    string HDInfo = "";

        //    ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");

        //    ManagementObjectCollection moc1 = cimobject1.GetInstances();

        //    foreach (ManagementObject mo in moc1)
        //    {

        //        HDInfo = (string)mo.Properties["Model"].Value;

        //    }

        //    return HDInfo;

        //}


        /// <summary>

        /// 创建公钥文件

        /// </summary>

        /// <param name="path"></param>

        /// <param name="publickey"></param>

        public void CreatePublicKeyXML(string path, string publickey)
        {

            try
            {

                FileStream publickeyxml = new FileStream(path, FileMode.Create);

                StreamWriter sw = new StreamWriter(publickeyxml);

                sw.WriteLine(publickey);

                sw.Close();

                publickeyxml.Close();

            }

            catch
            {

                //throw;

            }

        }

        /// <summary>

        /// 创建私钥文件

        /// </summary>

        /// <param name="path"></param>

        /// <param name="privatekey"></param>

        public void CreatePrivateKeyXML(string path, string privatekey)
        {

            try
            {

                FileStream privatekeyxml = new FileStream(path, FileMode.Create);

                StreamWriter sw = new StreamWriter(privatekeyxml);

                sw.WriteLine(privatekey);

                sw.Close();

                privatekeyxml.Close();

            }

            catch
            {

                //throw;

            }

        }

        /// <summary>

        /// 读取公钥

        /// </summary>

        /// <param name="path"></param>

        /// <returns></returns>

        public string ReadPublicKey(string path)
        {

            StreamReader reader = new StreamReader(path);

            string publickey = reader.ReadToEnd();

            reader.Close();

            return publickey;

        }

        /// <summary>

        /// 读取私钥

        /// </summary>

        /// <param name="path"></param>

        /// <returns></returns>

        public string ReadPrivateKey(string path)
        {

            StreamReader reader = new StreamReader(path);

            string privatekey = reader.ReadToEnd();

            reader.Close();

            return privatekey;

        }

     
        public string GetComputerID(string softname)
        {

            return GetHash(softname +GetDiskInfo());
        }

        public void WriteRegFile(string filename, string id)
        {

            if (!File.Exists(filename))
            {

                Stream stream = File.Open(filename, FileMode.OpenOrCreate);

                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(id);

                writer.Close();

                stream.Close();
            }
        }
        public string ReadRegFile(string filename)
        {
            string s = "";
            if (File.Exists(filename))
            {

                Stream stream = File.Open(filename, FileMode.Open);

                StreamReader reader = new StreamReader(stream);

                s = reader.ReadToEnd();

                reader.Close();

                stream.Close();
            }
            return s;

        }
        //public string GetBIOSSerialNumber()
        //{

        //    try
        //    {

        //        ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");

        //        string str = "";

        //        foreach (ManagementObject obj2 in searcher.Get())
        //        {

        //            str = obj2["SerialNumber"].ToString().Trim();

        //        }

        //        return str;

        //    }

        //    catch
        //    {

        //        return "";

        //    }

        //}



        public string CreateGomputerbit(string softname)
        {
            try
            {
                //取得硬盘号
                string hardDiskSerialNumber = this.GetDiskInfo() + softname + "GUHESOFTWARE";

                MD5 md3 = new MD5CryptoServiceProvider();

                hardDiskSerialNumber = BitConverter.ToString(md3.ComputeHash(Encoding.Default.GetBytes(hardDiskSerialNumber))).Replace("-", "").ToUpper().Substring(8, 0x10);

                string strMachineCode = softname + "H" + hardDiskSerialNumber;
                string filename = System.AppDomain.CurrentDomain.BaseDirectory + "reg.dat";
                Stream stream = File.Open(filename, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(strMachineCode);
                writer.Close();
                stream.Close();
                return strMachineCode;
            }
            catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return "diskkeyerror";
            }


        }

        private string GetDiskInfo()
        {
            try
            {
          
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
                    string strHardDiskID = null;
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        strHardDiskID = mo["SerialNumber"].ToString().Trim();
                        break;
                    }
                    return strHardDiskID;
                
            }
            catch(Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return "unknow";
            }
        }

        //public string GetCpuID()
        //{

        //    try
        //    {

        //        ManagementObjectCollection instances = new ManagementClass("Win32_Processor").GetInstances();

        //        string str = null;

        //        foreach (ManagementObject obj2 in instances)
        //        {

        //            str = obj2.Properties["ProcessorId"].Value.ToString();

        //            break;

        //        }

        //        return str;

        //    }

        //    catch
        //    {

        //        return "";

        //    }

        //}

        //public string GetHardDiskSerialNumber()
        //{
        //    try
        //    {
        //        string hardDiskID = null;
        //        ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
        //        ManagementObjectCollection moc1 = cimobject1.GetInstances();
        //        foreach (ManagementObject mo in moc1)
        //        {
        //            if (hardDiskID == null)
        //            {
        //                hardDiskID = mo.Properties["Model"].Value.ToString();
        //            }
        //            else
        //            {
        //                hardDiskID += ";" + mo.Properties["Model"].Value.ToString();
        //            }
        //        }
        //        return (hardDiskID == null) ? "None" : hardDiskID;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
                
        //        return "None";

        //    }
        //}



        //public string GetNetCardMACAddress()
        //{

        //    try
        //    {

        //        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");

        //        string str = "";

        //        foreach (ManagementObject obj2 in searcher.Get())
        //        {

        //            str = obj2["MACAddress"].ToString().Trim();

        //        }

        //        return str;

        //    }

        //    catch
        //    {

        //        return "";

        //    }

        //}




        public bool CheckSoftRegCode(string softname)
        {
            try
            {
                string str = "";
                string computerbit = "";
                computerbit = CreateGomputerbit(softname);
                SHA1 sha = new SHA1CryptoServiceProvider();
                str = BitConverter.ToString(sha.ComputeHash(Encoding.Default.GetBytes(softname))).Replace("-", "").ToUpper();
                string str3 = computerbit;
                SHA1 sha2 = new SHA1CryptoServiceProvider();
                string str4 = BitConverter.ToString(sha2.ComputeHash(Encoding.Default.GetBytes(computerbit))).Replace("-", "").ToUpper();
                //Console.WriteLine("str4:" + str4);

                string str5 = "";

                for (int i = 0; i < str4.Length; i++)
                {

                    if ((i % 2) == 1)
                    {

                        str5 = str5 + str4[i];

                    }

                }

                string str6 = "";

                for (int j = 0; j < str.Length; j++)
                {

                    if ((j % 2) == 0)
                    {

                        str6 = str6 + str[j];

                    }

                }

                int[] numArray = new int[20];

                for (int k = 0; k < 20; k++)
                {

                    numArray[k] = str6[k] + str5[k];

                    numArray[k] = numArray[k] % 0x24;

                }

                string str7 = "";

                for (int m = 0; m < 20; m++)
                {

                    if ((m > 0) && ((m % 4) == 0))
                    {

                        str7 = str7 + "-";

                    }

                    str7 = str7 + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[numArray[m]];

                }

                string str2 = "";
                if (File.Exists("lic.dat"))
                {
                    Stream stream = File.Open("lic.dat", FileMode.Open);
                    StreamReader reader = new StreamReader(stream);
                    str2 = reader.ReadLine();
                    reader.Close();
                    stream.Close();
                    if (str2 == null || str2 == "")
                        return false;

                }
                else
                    return false;

                if (str2.Equals(str7))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
                return false;
            }

        }
        /// <summary>
        /// 注入软件的注册码
        /// </summary>
        /// <param name="softname"></param>
        /// <param name="strcode"></param>
        /// <returns></returns>
        public bool CheckSoftRegCodeByInput(string softname, string strcode)
        {

            string str = "";
            string computerbit = "";
            computerbit = CreateGomputerbit(softname);
            SHA1 sha = new SHA1CryptoServiceProvider();
            str = BitConverter.ToString(sha.ComputeHash(Encoding.Default.GetBytes(softname))).Replace("-", "").ToUpper();


            string str3 = computerbit;


            SHA1 sha2 = new SHA1CryptoServiceProvider();

            string str4 = BitConverter.ToString(sha2.ComputeHash(Encoding.Default.GetBytes(computerbit))).Replace("-", "").ToUpper();
            //Console.WriteLine("str4:" + str4);

            string str5 = "";

            for (int i = 0; i < str4.Length; i++)
            {

                if ((i % 2) == 1)
                {

                    str5 = str5 + str4[i];

                }

            }

            string str6 = "";

            for (int j = 0; j < str.Length; j++)
            {

                if ((j % 2) == 0)
                {

                    str6 = str6 + str[j];

                }

            }

            int[] numArray = new int[20];

            for (int k = 0; k < 20; k++)
            {

                numArray[k] = str6[k] + str5[k];

                numArray[k] = numArray[k] % 0x24;

            }

            string str7 = "";

            for (int m = 0; m < 20; m++)
            {

                if ((m > 0) && ((m % 4) == 0))
                {

                    str7 = str7 + "-";

                }

                str7 = str7 + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[numArray[m]];

            }

            string str2 = "";

            str2 = strcode;

            if (str2.Equals(str7))
            {

                //写授权文件
                Stream stream = File.Open("lic.dat", FileMode.Create);

                StreamWriter writer = new StreamWriter(stream);

                writer.WriteLine(str7);

                writer.Close();

                stream.Close();
                return true;
            }
            return false;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>返回值 True:试用期内，False:超出试用期</returns>
        public bool DateLimitCheck()
        {
            string dt = "";
            dt = ReadReg("datetime");
            if (dt == "")
            {
                //dt = RSAEncrypt(pubkey, DateTime.Now.ToString());
                //WriteReg("datetime", dt);

            }
            else
            {
                string dt1 = "";
                dt1 = RSADecrypt(prikey, dt);
                DateTime dt2;   //注册时间
                DateTime dt3;   //到期时间
                dt2 = Convert.ToDateTime(dt1);
                dt3 = dt2.AddDays(60);
                // MessageBox.Show(dt3.ToString());
                if (DateTime.Now > dt3)
                {
                    return false;
                }
            }
            return true;
        }
        private void WriteReg(string name, string tovalue)
        {
            try
            {
                RegistryKey hklm = Registry.LocalMachine;
                RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.CreateSubKey("GUHE");
                aimdir.SetValue(name, tovalue);
            }
            catch { }
        }
        private string ReadReg(string name)
        {
            try
            {
                string registData;
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("GUHE", true);
                registData = aimdir.GetValue(name).ToString();
                return registData;
            }
            catch
            {
                return "";
            }
        } 

    }
}
