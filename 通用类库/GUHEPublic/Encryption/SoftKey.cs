using System;
using System.Text;
using System.Runtime.InteropServices;

namespace GHIBMS.Pub
{
    // <summary>
    /// Class1 的摘要说明。
    /// </summary>

    public class SoftKey
    {
     
        [DllImport("my32.dll")]
        public static extern int YtReadString(StringBuilder outstring, short Address, short mylen, string HKey, string LKey, StringBuilder KeyPath);
        [DllImport("my32.dll")]
        public static extern int YtWriteString(string InString, short Address, string HKey, string LKey, StringBuilder KeyPath);
        [DllImport("my32.dll")]
        public static extern int YtWriteLong(int indata, short add, string HKey, string LKey, StringBuilder KeyPath);
        [DllImport("my32.dll")]
        public static extern int YtReadLong(ref int OutData, short Address, string HKey, string LKey, StringBuilder KeyPath);

        [DllImport("kernel32.dll")]
        public static extern int lstrcmp(byte[] pDest, byte[] pSource);
        [DllImport("kernel32.dll")]
        public static extern int lstrcmpi(byte[] pDest, byte[] pSource);
        [DllImport("kernel32.dll")]
        public static extern int lstrcat(StringBuilder pDest, byte[] pSource);
        [DllImport("kernel32.dll", EntryPoint = "lstrcat")]
        public static extern int lstrcat_2(StringBuilder pDest, string pSource);
        [DllImport("kernel32.dll")]
        public static extern int lstrcpy(StringBuilder pDest, String pSource);
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        public static extern void CopyLong(ref int pDest, ref double pSource, int ByteLen);

        [DllImport("My32.dll")]
        public static extern short FindPort(int start, StringBuilder sKeyPath);
        [DllImport("My32.dll")]
        public static extern short FindPort_2(int start, byte[] InByte, int InLen, StringBuilder sKeyPath);
        [DllImport("My32.dll")]
        public static extern short CalEx(byte[] InByte, int in_len, ref int D0, ref int D1, ref int D2, ref int D3, ref int D4, ref int D5, ref int D6, ref int D7,
            ref double F0, ref double F1, ref double F2, ref double F3, ref double F4, ref double F5, ref double F6, ref double F7,
            StringBuilder S0, StringBuilder S1, StringBuilder S2, StringBuilder S3, StringBuilder S4, StringBuilder S5, StringBuilder S6, StringBuilder S7, String sKeyPath, int over_count);
        [DllImport("My32.dll")]
        public static extern short CalPubByFile(string InFile, ref int D0, ref int D1, ref int D2, ref int D3, ref int D4, ref int D5, ref int D6, ref int D7,
            ref double F0, ref double F1, ref double F2, ref double F3, ref double F4, ref double F5, ref double F6, ref double F7,
            StringBuilder S0, StringBuilder S1, StringBuilder S2, StringBuilder S3, StringBuilder S4, StringBuilder S5, StringBuilder S6, StringBuilder S7, String sKeyPath, int over_count);
        [DllImport("My32.dll")]
        public static extern short CalPub(byte[] InByte, int in_len, ref int D0, ref int D1, ref int D2, ref int D3, ref int D4, ref int D5, ref int D6, ref int D7,
            ref double F0, ref double F1, ref double F2, ref double F3, ref double F4, ref double F5, ref double F6, ref double F7,
            StringBuilder S0, StringBuilder S1, StringBuilder S2, StringBuilder S3, StringBuilder S4, StringBuilder S5, StringBuilder S6, StringBuilder S7, String sKeyPath, int over_count);
        [DllImport("My32.dll")]
        public static extern short EdcByFile(string Key, string InbinFile, string OutbinFile, String KeyPath);
        [DllImport("My32.dll")]
        public static extern short ReadKeyFormEpm(StringBuilder OutAuthCode, short Addr, string HKey, string LKey, String KeyPath);
        [DllImport("My32.dll")]
        public static extern short GetIDVersion(ref  uint ID, ref short version, String KeyPath);
        [DllImport("My32.dll")]
        public static extern bool GetAuthFromFile(string InFile, StringBuilder HKey, StringBuilder LKey, StringBuilder OutSetTime, ref uint id);
        [DllImport("My32.dll")]
        public static extern short GetRunTimer(StringBuilder Year, StringBuilder Month, StringBuilder Day, StringBuilder Hour, StringBuilder Minuts, StringBuilder Second, String KeyPath);
        [DllImport("My32.dll")]
        public static extern bool WriteIniSetting(string FileName, string HKey, string LKey, string Time, uint id);
        [DllImport("My32.dll")]
        public static extern short AddConnectEx(byte InFunNum, short Addr, bool IfClose, String KeyPath);
        [DllImport("My32.dll")]
        public static extern short AddConnectNew(byte InFunNum, short Addr, bool IfClose, StringBuilder OutVerCode, String KeyPath);
        [DllImport("My32.dll")]
        public static extern short GetIpCountByFunNum(byte FunNum);
        [DllImport("My32.dll")]
        public static extern short GetConCountByFunNum(byte FunNum);
        [DllImport("My32.dll")]
        public static extern short GetSetCount(short Addr, ref int count, ref bool IsMachine, ref byte OutFum, String KeyPath);
        [DllImport("My32.dll")]
        public static extern short ConnectServer(string ServerName, int Port);
        [DllImport("My32.dll")]
        public static extern short DisconnectServer();
        [DllImport("My32.dll")]
        public static extern void AutoDisConnect(int timer, bool biao);
        [DllImport("my32.dll")]
        public static extern bool EdccBin(String Key, byte[] InByte, int in_len, ref sbyte OutErrCode, String KeyPath);
        public String KeyPath;
        public String KeyAuth;//储存授权号
        public bool b_ini;//是否已进初始化工作
        public int LastError;//返回最返错误码
        public SoftKey()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        //以下函数用于将字节数组转化为宽字符串
        public static string ByteConvertString(byte[] buffer)
        {
            char[] null_string = { '\0', '\0' };
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            return encoding.GetString(buffer).TrimEnd(null_string);
        }
        //以下函数用于将宽字符串转化为字节数组
        public static bool StringConvertByte(byte[] buffer, string InS)
        {
            byte[] temp; int n;
            temp = System.Text.Encoding.Default.GetBytes(InS);
            if (temp.Length > 50) return false;
            for (n = 1; n <= temp.Length; n++)
            {
                buffer[n - 1] = temp[n - 1];
            }
            return true;
        }

        //以下函数用于将宽字符串转化为字节数组
        public static bool StringConvertByteEx(byte[] buffer, string InS)
        {
            byte[] temp; int n;
            temp = System.Text.Encoding.Default.GetBytes(InS);
            for (n = 1; n <= temp.Length; n++)
            {
                buffer[n - 1] = temp[n - 1];
            }
            return true;
        }

        public static void StringToDword(ref int outd0, ref int outd1, ref int outd2, byte[] buffer, int start_pos)
        {
            int n; uint d0 = 0, d1 = 0, d2 = 0; int temp_len;
            int len = buffer.Length - start_pos;
            if (len > 4) temp_len = 4; else temp_len = len;
            for (n = temp_len; n > 0; n--)
            {
                d0 = d0 | (uint)((buffer[n - 1 + 4 * 0 + start_pos] & 255) << ((temp_len - n) * 8));
            }
            len = len - 4; if (len < 0) goto my_exit;
            if (len > 4) temp_len = 4; else temp_len = len;
            for (n = temp_len; n > 0; n--)
            {
                d1 = d1 | (uint)((buffer[n - 1 + 4 * 1 + start_pos] & 255) << ((temp_len - n) * 8));
            }
            len = len - 4; if (len < 0) goto my_exit;
            if (len > 4) temp_len = 4; else temp_len = len;
            for (n = temp_len; n > 0; n--)
            {
                d2 = d2 | (uint)((buffer[n - 1 + 4 * 2 + start_pos] & 255) << ((temp_len - n) * 8));
            }
        my_exit:
            outd0 = (int)d0; outd1 = (int)d1; outd2 = (int)d2;
            return;
        }



        //在程序开始运行时，请先调用Ini过程，查找对应的加密锁所在的设备路径；
        //如果找到对应的加密锁，会将该路径保存在变量KeyPath中，
        //以备其它函数的调用；
        public short Ini()
        {

            short n, ret; sbyte err = 0; int i;
            int[] D = new int[8]; double[] F = new double[8];
            StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
            StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
            //以下数组是D0=123,D1=123,D2=123,D3=123表达式的密文，用于初步查找对应的加密锁，
            //因为系统中可能会有多把锁，找到对应的加密锁后，保存路径，以备其它真的加密函数调用 
            byte[] EncByte = { 6, 0, 81, 33, 57, 221, 180, 89, 149, 243 };//
            byte[] temp = new byte[10];//临时数组


            //查找系统上所有的锁，
            for (n = 0; n < 256; n++)
            {
                //拷贝临时数组
                for (i = 0; i < 10; i++) temp[i] = EncByte[i];
                {
                    StringBuilder sKeyPath = new StringBuilder("", 260);
                    ret = FindPort(n, sKeyPath);
                    KeyPath = sKeyPath.ToString();
                    if (ret != 0 && n == 0) return -1053;//表示系统上没有任何智能锁
                    if (ret != 0) return ret;
                    //从智能锁中读取储存在锁中的授权号，如需要的话，可以修改这里的读写密码
                    StringBuilder Key = new StringBuilder("", 300);//用于保存储存在智能锁中的授权号，分配空间一定要大于257
                    ret = ReadKeyFormEpm(Key, 30000, "ffffffff", "ffffffff", KeyPath);
                    KeyAuth = Key.ToString();
                    if (ret != 0) return ret;
                    //使用授权号对加密文件进行注册，只有对应的锁及对应的授权号，生成的注册数组，才可以被下面的代码正确运行及返回正确的数据
                    if (!EdccBin(KeyAuth, temp, 10, ref err, KeyPath)) { return (short)err; };
                    //使用该设备路径锁进行运算,看运算的结果是否为D0=123,D1=123,D2=123,D3=123,只有对应的锁及对应的授权号才可以返回正常的数值
                    ret = CalPub(temp, 10, ref D[0], ref D[1], ref D[2], ref D[3], ref D[4], ref D[5], ref D[6], ref D[7],
                        ref F[0], ref F[1], ref F[2], ref F[3], ref F[4], ref F[5], ref F[6], ref F[7],
                        s0, s1, s2, s3, s4, s5, s6, s7, KeyPath, 20000);
                }
                if (ret == -63) return ret;
                //如果正确，则返回该设备路径供以后使用
                if ((ret == 0) && (D[0] == 123)) { b_ini = true; return 0; }
            }
            return -53;
        }

        //输出函数YCheckKey
        //目的：用来对检查是否存在对应的加密锁
        //返回结果：为真表示存在对应的加密锁，否则为假
        //如果错误码LastError=0，操作正确，如果LastError为负数，请参见操作手册

        byte[] YCheckKey_1_Array = new byte[522];
        static bool YCheckKey_1_alread;
        public bool YCheckKey_1()
        {
            int d0 = 0, d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0;
            int pc_d0, pc_d1, pc_d2, pc_d3;
            double[] F = new double[8];
            StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
            StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);


            //原理：生产随机数，然后在程序端对随机数进行加密运算，然后让加密锁对随数机进行解密运算，看解密后的结果是否与加密前的数据相符，如果相符则为存在对应的加密锁
            //加密强度低，但这个函数必须存在并调用，用于判断是否存在对应加密锁
            LastError = 0;
            if (!b_ini) { LastError = Ini(); if (LastError != 0)return false; }//如果没有进行初始化，进先进行初始化操作
            //以下是由开发工具随机生成的加密后的加密表达式，以二进制的数组表示
            //注意，这个加密表达式是密文，是由你自定义的开发密钥及主锁内置密钥加密生成，
            //可以在程序中增加对YCheckKey_1Array数组进行检验和校证，从而更大地增加安全性，检验和的原理就是将数组的全部或部分相加，看结果是否与既定的结果的相符，不相符则退出程序或跳转到错误地方
            byte[] EncByte ={7,2,123,85,177,66,90,153,223,143,75,114,245,83,115,246,242,186,160,54,30,189,246,185,17,65,255,156,55,61,40,23,45,250,104,237,129,63,36,194,182,52,186,144,152,135,240,7,176,231,
45,76,228,91,143,80,102,210,43,130,118,173,102,91,32,248,249,139,50,22,254,154,69,1,72,51,142,144,19,58,19,114,65,24,231,223,112,61,50,225,83,134,74,176,31,67,186,25,78,187,
228,249,233,123,14,135,217,132,37,131,20,105,158,243,21,74,237,138,224,248,31,97,59,187,149,71,97,93,143,71,83,134,74,176,31,67,186,25,13,227,53,26,247,38,178,134,127,32,30,20,
230,84,74,243,182,127,215,21,49,157,165,112,77,86,115,50,101,240,230,169,42,30,57,197,84,224,101,214,93,124,82,112,166,195,140,214,226,96,202,119,111,22,79,194,83,134,74,176,31,67,
186,25,162,172,173,33,134,43,147,49,78,97,125,218,210,144,252,237,106,248,148,246,169,2,61,167,21,74,237,138,224,248,31,97,86,140,91,22,217,227,117,203,182,127,215,21,49,157,165,112,
251,82,49,202,83,168,42,235,82,236,21,127,242,119,223,119,93,124,82,112,166,195,140,214,215,72,22,217,134,221,198,38,182,127,215,21,49,157,165,112,77,86,115,50,101,240,230,169,194,38,
88,93,196,187,162,248,72,51,142,144,19,58,19,114,13,40,143,236,150,87,220,132,153,174,1,113,154,15,25,12,78,187,228,249,233,123,14,135,217,132,37,131,20,105,158,243,21,74,237,138,
224,248,31,97,86,140,91,22,217,227,117,203,182,127,215,21,49,157,165,112,77,86,115,50,101,240,230,169,17,27,82,82,200,201,111,75,146,242,237,32,237,76,202,23,116,252,85,214,169,11,
139,185,106,248,148,246,169,2,61,167,21,74,237,138,224,248,31,97,59,187,149,71,97,93,143,71,83,134,74,176,31,67,186,25,92,71,8,54,92,214,239,239,194,38,88,93,196,187,162,248,
72,51,142,144,19,58,19,114,90,214,72,111,130,117,162,231,189,76,200,87,137,128,112,197,106,248,148,246,169,2,61,167,21,74,237,138,224,248,31,97,137,74,28,189,1,78,12,239,217,132,
37,131,20,105,158,243,21,74,237,138,224,248,31,97,178,20,142,132,149,109,13,126};


            //使用授权号对加密文件进行注册，
            //以下函数将二进制的加密后的加密表达式送到加密锁中进行解密并重新加密（使用相应的授权号），即注册或转换
            //然后返回加密后的数据（该加密后的数据与该加密锁相对应）
            //只有被对应的授权号进行转换的数据才能被对应的加密锁解密并运行
            //其中Key为与锁对应的授权号

            if (!YCheckKey_1_alread)//如果已进行了注册的过程，则不再进行注册，用于加快程序运行速度，因为注册是在加密锁中进行，
            {
                sbyte errcode = 0; int i;
                //拷贝数组
                for (i = 0; i < 522; i++) YCheckKey_1_Array[i] = EncByte[i];

                {
                    if (!EdccBin(KeyAuth, YCheckKey_1_Array, 522, ref errcode, KeyPath)) { LastError = errcode; return false; }; YCheckKey_1_alread = true;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////
            //产生随机数
            System.Random rnd = new System.Random();
            pc_d0 = d0 = rnd.Next(0, 2147483646); pc_d1 = d1 = rnd.Next(0, 2147483646); pc_d2 = d2 = rnd.Next(0, 2147483646); pc_d3 = d3 = rnd.Next(0, 2147483646);
            //以下在程序端对随机数进行加密运算
            d2 = d2 ^ d1;
            d3 = (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
            d3 = d3 ^ d0;
            d3 = (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
            d3 = d3 ^ d0;
            d1 = d1 ^ d2;
            d0 = d0 ^ d2;
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d3 = (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
            d3 = d3 ^ d1;
            d0 = (int)((((uint)d0) << 1) | (((uint)d0) >> 31));
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d3 = (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
            d1 = (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d3 = (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
            d1 = d1 ^ 1356656511;
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d2 = (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d2 = d2 ^ d1;
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d3 = (int)((((uint)d3) >> 1) | (((uint)d3) << 31));
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d1 = (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
            d0 = (int)((((uint)d0) << 1) | (((uint)d0) >> 31));

            //以下将随机数送到加密锁内作解密运算

            {
                LastError = CalPub(YCheckKey_1_Array, 522, ref d0, ref d1, ref d2, ref d3, ref d4, ref d5, ref d6, ref d7,
                                ref F[0], ref F[1], ref F[2], ref F[3], ref F[4], ref F[5], ref F[6], ref F[7],
                                s0, s1, s2, s3, s4, s5, s6, s7, KeyPath, 20000000);
            }
            if (LastError != 0 && LastError != -43) { return false; }
            //如果相同，则存在对应加密锁，否则，不存在对应的加密锁
            if ((d0 == pc_d0) && (d1 == pc_d1) && (d2 == pc_d2) && (d3 == pc_d3))
            {
                return true;
            }
            return false;
        }


        //输出函数YCompareStringNoCase
        //目的：用来对两个字符串进行比较(忽略大小写)
        //参数ins1：要比较的两个字符串之一
        //参数ins2：要比较的两个字符串之一
        //返回结果：如果为真，表明两个字符串相等，否则为假
        //如果错误码LastError=0，操作正确，如果LastError为负数，请参见操作手册

        byte[] YCompareStringNoCase_2_Array = new byte[610];
        static bool YCompareStringNoCase_2_alread;
        public bool YCompareStringNoCase_2(string ins1, string ins2)
        {
            ins1 = ins1.ToLower(); ins2 = ins2.ToLower();
            int d0 = 0, d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0; int n;
            double[] F = new double[8];
            StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
            StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);

            //原理：先对要参与运算的参数在程序端进行加密运算，然后在锁中对该参数进行解密运算并进行比较，然后返回结果给程序端
            LastError = 0;
            if (!b_ini) { LastError = Ini(); if (LastError != 0)return false; }//如果没有进行初始化，进先进行初始化操作
            //以下是由开发工具随机生成的加密后的加密表达式，以二进制的数组表示
            //注意，这个加密表达式是密文，是由你自定义的开发密钥及主锁内置密钥加密生成，
            //可以在程序中增加对YCompareStringNoCase_2Array数组进行检验和校证，从而更大地增加安全性，检验和的原理就是将数组的全部或部分相加，看结果是否与既定的结果的相符，不相符则退出程序或跳转到错误地方
            byte[] EncByte ={95,2,123,85,177,66,90,153,223,143,75,114,245,83,115,246,242,186,160,54,30,189,246,185,17,65,255,156,55,61,40,23,45,250,104,237,129,63,36,194,182,52,104,124,47,29,154,232,117,211,
182,127,215,21,49,157,165,112,77,86,115,50,101,240,230,169,42,30,57,197,84,224,101,214,93,124,82,112,166,195,140,214,204,87,35,14,77,108,71,1,211,229,17,30,47,105,128,146,42,60,
19,76,243,30,149,33,230,109,209,156,17,195,231,170,88,247,127,101,222,241,106,104,108,182,125,109,102,225,54,123,76,190,121,89,10,56,92,248,182,127,215,21,49,157,165,112,100,12,46,94,
119,74,99,134,164,98,149,33,129,131,182,162,88,247,127,101,222,241,106,104,213,12,194,100,9,218,231,124,17,27,82,82,200,201,111,75,146,242,237,32,237,76,202,23,173,64,44,109,232,81,
251,18,194,38,88,93,196,187,162,248,72,51,142,144,19,58,19,114,91,132,214,52,74,81,3,90,182,127,215,21,49,157,165,112,38,225,143,0,242,111,38,22,2,249,70,38,226,208,123,69,
46,10,18,246,220,28,58,170,43,130,118,173,102,91,32,248,130,73,109,253,174,141,4,198,15,158,240,86,145,212,50,165,115,7,51,4,212,36,42,30,83,134,74,176,31,67,186,25,78,187,
228,249,233,123,14,135,54,166,43,194,43,22,88,212,42,60,19,76,243,30,149,33,87,238,58,5,242,175,161,184,46,10,18,246,220,28,58,170,158,113,114,255,7,55,169,29,42,30,57,197,
84,224,101,214,93,124,82,112,166,195,140,214,161,53,190,253,110,28,77,124,226,203,85,77,131,168,178,224,30,1,216,151,108,153,73,78,213,246,132,86,23,225,11,207,52,38,223,64,173,226,
135,122,251,82,49,202,83,168,42,235,249,139,50,22,254,154,69,1,72,51,142,144,19,58,19,114,54,214,51,28,155,99,17,170,108,107,185,144,93,3,180,122,226,205,77,205,0,133,223,228,
156,117,93,30,107,248,110,103,146,242,237,32,237,76,202,23,150,167,95,80,73,6,12,201,46,10,18,246,220,28,58,170,158,113,114,255,7,55,169,29,49,58,220,191,202,115,159,224,15,158,
240,86,145,212,50,165,66,145,184,81,209,72,32,31,42,30,57,197,84,224,101,214,93,124,82,112,166,195,140,214,142,82,139,218,179,32,217,221,85,57,208,16,46,81,94,145,129,45,138,118,
79,178,252,89,109,165,4,149,169,122,171,229,112,30,125,149,103,201,238,147,232,225,18,165,140,231,137,166,222,140,27,217,133,100,40,193,173,142,83,76,14,234,160,95,119,215,23,121,71,160,
211,104,165,132,143,225,224,209,194,127};


            //使用授权号对加密文件进行注册，
            //以下函数将二进制的加密后的加密表达式送到加密锁中进行解密并重新加密（使用相应的授权号），即注册或转换
            //然后返回加密后的数据（该加密后的数据与该加密锁相对应）
            //只有被对应的授权号进行转换的数据才能被对应的加密锁解密并运行
            //其中Key为与锁对应的授权号

            if (!YCompareStringNoCase_2_alread)//如果已进行了注册的过程，则不再进行注册，用于加快程序运行速度，因为注册是在加密锁中进行，
            {
                sbyte errcode = 0; int i;
                //拷贝数组
                for (i = 0; i < 610; i++) YCompareStringNoCase_2_Array[i] = EncByte[i];

                {
                    if (!EdccBin(KeyAuth, YCompareStringNoCase_2_Array, 610, ref errcode, KeyPath)) { LastError = errcode; return false; }; YCompareStringNoCase_2_alread = true;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////
            //将第一个字符串的前12字节分解为3个长整形，赋值给d0-d2,用于加密以后的加密运算
            byte[] temp_string_1 = System.Text.Encoding.Default.GetBytes(ins1); int nLen_1 = temp_string_1.Length;
            byte[] leave_string_1 = new byte[nLen_1];
            if (nLen_1 > 0) { StringToDword(ref d0, ref d1, ref d2, temp_string_1, 0); leave_string_1[0] = 0; }
            //获得余下的字符串
            int temp_len = nLen_1 - 12;
            for (n = 0; n < temp_len; n++) leave_string_1[n] = temp_string_1[12 + n];
            //将第二个字符串的前12字节分解为3个长整形，赋值给d3-d5,用于加密以后的加密运算
            byte[] temp_string_2 = System.Text.Encoding.Default.GetBytes(ins2); int nLen_2 = temp_string_2.Length;
            byte[] leave_string_2 = new byte[nLen_2];
            if (nLen_2 > 0) { StringToDword(ref d3, ref d4, ref d5, temp_string_2, 0); leave_string_2[0] = 0; }
            //获得余下的字符串
            temp_len = nLen_2 - 12;
            for (n = 0; n < temp_len; n++) leave_string_2[n] = temp_string_2[12 + n];
            //以下在程序端对输入参数进行加密运算
            d2 = (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
            d2 = d2 ^ d5;
            d4 = (int)((((uint)d4) << 1) | (((uint)d4) >> 31));
            d0 = (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
            d0 = (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
            d3 = (int)((((uint)d3) << 1) | (((uint)d3) >> 31));
            d1 = (int)((((uint)d1) >> 1) | (((uint)d1) << 31));
            d2 = (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
            d5 = d5 ^ d3;
            d4 = (int)((((uint)d4) >> 1) | (((uint)d4) << 31));
            d2 = (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
            d0 = (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
            d5 = (int)((((uint)d5) >> 1) | (((uint)d5) << 31));
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d4 = (int)((((uint)d4) >> 1) | (((uint)d4) << 31));
            d0 = (int)((((uint)d0) >> 1) | (((uint)d0) << 31));
            d1 = d1 ^ d3;
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d1 = (int)((((uint)d1) << 1) | (((uint)d1) >> 31));
            d5 = d5 ^ d2;
            d0 = (int)((((uint)d0) << 1) | (((uint)d0) >> 31));
            d5 = (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
            d3 = d3 ^ d2;
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));
            d0 = d0 ^ d2;
            d5 = (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
            d5 = (int)((((uint)d5) << 1) | (((uint)d5) >> 31));
            d5 = d5 ^ d2;
            d2 = (int)((((uint)d2) << 1) | (((uint)d2) >> 31));
            d2 = (int)((((uint)d2) >> 1) | (((uint)d2) << 31));

            //以下将加密后的参数送到加密锁内作解密运算，并将解密后的结果合并为字符串，再进行比较

            {
                LastError = CalPub(YCompareStringNoCase_2_Array, 610, ref d0, ref d1, ref d2, ref d3, ref d4, ref d5, ref d6, ref d7,
                                ref F[0], ref F[1], ref F[2], ref F[3], ref F[4], ref F[5], ref F[6], ref F[7],
                                s0, s1, s2, s3, s4, s5, s6, s7, KeyPath, 20000000);
            }
            if (LastError != 0 && LastError != -43) { return false; }
            //如果没有返回错误，则将解密并运算的结果返回;
            if (d7 == 0) return false;//如果d7不为0表明在锁内进行比较的结果不相符，直接返回假
            //如果锁内比较相符，再比较余下的部分
            return (lstrcmpi(temp_string_1, temp_string_2) == 0); ;
        }



        public bool GetAuthFromFileEx(string IniFile, ref string OutHKey, ref string OutLKey, ref string OutSetTime, ref uint OutID)
        {
            StringBuilder HKey;//授权码的高位
            StringBuilder LKey;//授权码的低位
            StringBuilder SetTime;//设置的授权时间
            HKey = new StringBuilder("", 50); LKey = new StringBuilder("", 50); SetTime = new StringBuilder("", 100);
            //从INI文件中获得授权码的高低位，设置的授权时间及授权ID等。远程更新时间时，直接更新INI文件就可以了
            {
                if (!GetAuthFromFile(IniFile, HKey, LKey, SetTime, ref OutID)) return false;
            }
            OutHKey = HKey.ToString();
            OutLKey = LKey.ToString();
            OutSetTime = SetTime.ToString();
            return true;
        }

        public short GetRunTimerEx(ref string OutYear, ref string OutMonth, ref string OutDay,
           ref string OutHour, ref string OutMinuts, ref string OutSecond)
        {
            short ret;
            StringBuilder year = new StringBuilder("", 10), month = new StringBuilder("", 10), day = new StringBuilder("", 10), hour = new StringBuilder("", 10);
            StringBuilder minuts = new StringBuilder("", 10), second = new StringBuilder("", 10);
            {
                ret = GetRunTimer(year, month, day, hour, minuts, second, KeyPath);
                if (ret != 0) return ret;
            }
            OutYear = year.ToString(); OutMonth = month.ToString();
            OutDay = day.ToString(); OutHour = hour.ToString();
            OutMinuts = minuts.ToString(); OutSecond = second.ToString();
            return ret;
        }

        public short GetIDVersionEx(ref uint id, ref short ver)
        {
            {
                return GetIDVersion(ref id, ref ver, KeyPath);
            }
        }

        public short FindPortEx(int start)
        {
            StringBuilder sKeyPath = new StringBuilder("", 260);
            {
                short ret = FindPort(start, sKeyPath);
                KeyPath = sKeyPath.ToString();
                return ret;
            }
        }

        public string GetErrInfo(short err)
        {
            switch (err)
            {
                case -1:
                    return "未找到返回结果变量";

                case -2:
                    return "未找到 = 符号";

                case -3:
                    return "代表没有找到相应常数";

                case -5:
                    return "代表找不到字符串的第一个双引号";

                case -6:
                    return "代表找不到字符串的第二个双引号";

                case -7:
                    return "IF语句没有找到goto字符";

                case -8:
                    return "IF语句没有找到第一个比较字符";

                case -9:
                    return "IF语句没有找到比较符号";

                case -10:
                    return "两边变量类型不相符";

                case -11:
                    return "没有找到NOT符号";

                case -12:
                    return "不是整形变量";

                case -13:
                    return "代表没有找到相应整形常数";

                case -14:
                    return "代表没有找到相应字符串常数";

                case -15:
                    return "代表没有找到相应浮点常数";

                case -16:
                    return "代表不支持这个运算";

                case -17:
                    return "代表没有左边括号";

                case -18:
                    return "代表没有变量";

                case -19:
                    return "代表没“，”";

                case -20:
                    return "代表没有右边括号";

                case -21:
                    return "代表常数超过指这定的范围";

                case -22:
                    return "代表储存器的地址超过指定的范围，整数不能超过EEPROM_LEN-4，浮点不能超过30720-8";

                case -23:
                    return "代表储存器的地址超过指定的范围，字符串不能超过EEPROM_LEN-LEN，其中LEN为字符串的长度";

                case -24:
                    return "除法中，被除数不能为0";

                case -25:
                    return "未知错误";

                case -26:
                    return "第二个变量不在指定的位置";

                case -27:
                    return "字符串常量超过指定的长度";

                case -28:
                    return "不是字符串变量";

                case -29:
                    return "没有第三个变量";

                case -30:
                    return "GOTO的标识语句不能全为数字";

                case -31:
                    return "不能打开ENC文件";

                case -32:
                    return "不能读ENC文件";

                case -33:
                    return "GOTO CALL不能找到指定的跳转位置";

                case -34:
                    return "智能卡运算中，未知数据类型";

                case -35:
                    return "智能卡运算中，未知代码类型";

                case -36:
                    return "字符串长度超出50";

                case -37:
                    return "RIGHT操作时超长，负长";

                case -38:
                    return "标识重复";

                case -39:
                    return "程序堆栈溢出";

                case -40:
                    return "堆栈溢出";

                case -41:
                    return "不能建立编译文件，请查看文件是否有只读属性，或被其它文件打开";

                case -42:
                    return "不能写文件，请查看文件是否有只读属性，或被其它文件打开";

                case -43:
                    return "程序被中途使用END语句结束";

                case -44:
                    return "程序跳转到外部的空间";

                case -45:
                    return "传送数据失败";

                case -46:
                    return "程序超出运算次数，可能是死循环";

                case -47:
                    return "写密码不正确";

                case -48:
                    return "读密码不正确";

                case -49:
                    return "读写EEPROM时，地址溢出";

                case -50:
                    return "USB操作失败，可能是没有找到相关的指令";

                case -51:
                    return "打开USB文件句柄失败";

                case -52:
                    return "使用加密锁加密自定义表达式，生成加密代码时生产错误";

                case -53:
                    return "无法打开usb设备，可能驱动程序没有安装或没有插入加密锁。";
                case -63:
                    return "不能打开指定的文件。";
                case -64:
                    return "不能建立指定的文件。";
                case -65:
                    return "验证码错误，可能是输入解密密钥错误，或注册授权码错误";
                case -66:
                    return "执行TIMEOUT函数或UPDATE函数时，输入的ID与锁ID不相符";
                case -67:
                    return "执行TIMEOUT函数时，智能卡运行函数已到期";
                case -68:
                    return "操作浮点运算时，输入的参数将会导致返回值是一个无穷值";
                case -69:
                    return "代表没足够的变量参数";
                case -70:
                    return "返回变量与函数不相符";
                case -71:
                    return "浮点数转换字符串时，要转换的数据太大。";
                case -72:
                    return "初始化服务器错误";
                case -73:
                    return "对缓冲区进行MD5运算时错误";
                case -74:
                    return "MD5验证IPVAR错误";
                case -75:
                    return "MD5验证IPCount错误";
                case -76:
                    return "没有找到对应的SOCKET连接";
                case -77:
                    return "没有找到要删除的对应的SOCKET连接";
                case -78:
                    return "没有找到要删除的对应的功能模块号连接";
                case -79:
                    return "没有找到要增加的对应的功能模块号连接";
                case -80:
                    return "用户数已超过限制的授权数量";
                case -81:
                    return "找不到对应的INI文件条目";
                case -82:
                    return "没有进行初始化服务工作。";
                case -252:
                    return "密码不正确";
                case -1088:
                    return "发送数据错误";
                case -1089:
                    return "获取数据错误";
                case -1092:
                    return "找不到对应的服务端操作码";
                case -1093:
                    return "表示连接服务时错误";
                case -1095:
                    return "获取主机名称失败";
                case -1097:
                    return "建立套字接错误";
                case -1098:
                    return "绑定套字节端口错误";
                case -1099:
                    return "表示无效连接，不能进行相关的操作。";
                case -2002:
                    return "表示监听时产生错误";
                case -2003:
                    return "表示发送的数据长度与接收的数据长度不相符";
                case -2005:
                    return "表示当前服务不存在任何连接";
                case -2006:
                    return "表示当前查询节点超出集合范范围";
                case -2009:
                    return "表示关闭连接错误";
                case -1052:
                    return "可能是输入的授权号不正确。";
                case -1053:
                    return "系统上没有任何智能锁。";

                default:
                    return "未知错误代码";
            }
        }
    }
}
