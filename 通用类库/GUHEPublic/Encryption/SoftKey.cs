using System;
using System.Text;
using System.Runtime.InteropServices;

namespace GHIBMS.Pub
{
    // <summary>
    /// Class1 ��ժҪ˵����
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
        public String KeyAuth;//������Ȩ��
        public bool b_ini;//�Ƿ��ѽ���ʼ������
        public int LastError;//�����������
        public SoftKey()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        //���º������ڽ��ֽ�����ת��Ϊ���ַ���
        public static string ByteConvertString(byte[] buffer)
        {
            char[] null_string = { '\0', '\0' };
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            return encoding.GetString(buffer).TrimEnd(null_string);
        }
        //���º������ڽ����ַ���ת��Ϊ�ֽ�����
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

        //���º������ڽ����ַ���ת��Ϊ�ֽ�����
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



        //�ڳ���ʼ����ʱ�����ȵ���Ini���̣����Ҷ�Ӧ�ļ��������ڵ��豸·����
        //����ҵ���Ӧ�ļ��������Ὣ��·�������ڱ���KeyPath�У�
        //�Ա����������ĵ��ã�
        public short Ini()
        {

            short n, ret; sbyte err = 0; int i;
            int[] D = new int[8]; double[] F = new double[8];
            StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
            StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);
            //����������D0=123,D1=123,D2=123,D3=123���ʽ�����ģ����ڳ������Ҷ�Ӧ�ļ�������
            //��Ϊϵͳ�п��ܻ��ж�������ҵ���Ӧ�ļ������󣬱���·�����Ա�������ļ��ܺ������� 
            byte[] EncByte = { 6, 0, 81, 33, 57, 221, 180, 89, 149, 243 };//
            byte[] temp = new byte[10];//��ʱ����


            //����ϵͳ�����е�����
            for (n = 0; n < 256; n++)
            {
                //������ʱ����
                for (i = 0; i < 10; i++) temp[i] = EncByte[i];
                {
                    StringBuilder sKeyPath = new StringBuilder("", 260);
                    ret = FindPort(n, sKeyPath);
                    KeyPath = sKeyPath.ToString();
                    if (ret != 0 && n == 0) return -1053;//��ʾϵͳ��û���κ�������
                    if (ret != 0) return ret;
                    //���������ж�ȡ���������е���Ȩ�ţ�����Ҫ�Ļ��������޸�����Ķ�д����
                    StringBuilder Key = new StringBuilder("", 300);//���ڱ��洢�����������е���Ȩ�ţ�����ռ�һ��Ҫ����257
                    ret = ReadKeyFormEpm(Key, 30000, "ffffffff", "ffffffff", KeyPath);
                    KeyAuth = Key.ToString();
                    if (ret != 0) return ret;
                    //ʹ����Ȩ�ŶԼ����ļ�����ע�ᣬֻ�ж�Ӧ��������Ӧ����Ȩ�ţ����ɵ�ע�����飬�ſ��Ա�����Ĵ�����ȷ���м�������ȷ������
                    if (!EdccBin(KeyAuth, temp, 10, ref err, KeyPath)) { return (short)err; };
                    //ʹ�ø��豸·������������,������Ľ���Ƿ�ΪD0=123,D1=123,D2=123,D3=123,ֻ�ж�Ӧ��������Ӧ����Ȩ�Ųſ��Է�����������ֵ
                    ret = CalPub(temp, 10, ref D[0], ref D[1], ref D[2], ref D[3], ref D[4], ref D[5], ref D[6], ref D[7],
                        ref F[0], ref F[1], ref F[2], ref F[3], ref F[4], ref F[5], ref F[6], ref F[7],
                        s0, s1, s2, s3, s4, s5, s6, s7, KeyPath, 20000);
                }
                if (ret == -63) return ret;
                //�����ȷ���򷵻ظ��豸·�����Ժ�ʹ��
                if ((ret == 0) && (D[0] == 123)) { b_ini = true; return 0; }
            }
            return -53;
        }

        //�������YCheckKey
        //Ŀ�ģ������Լ���Ƿ���ڶ�Ӧ�ļ�����
        //���ؽ����Ϊ���ʾ���ڶ�Ӧ�ļ�����������Ϊ��
        //���������LastError=0��������ȷ�����LastErrorΪ��������μ������ֲ�

        byte[] YCheckKey_1_Array = new byte[522];
        static bool YCheckKey_1_alread;
        public bool YCheckKey_1()
        {
            int d0 = 0, d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0;
            int pc_d0, pc_d1, pc_d2, pc_d3;
            double[] F = new double[8];
            StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
            StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);


            //ԭ�������������Ȼ���ڳ���˶���������м������㣬Ȼ���ü����������������н������㣬�����ܺ�Ľ���Ƿ������ǰ�������������������Ϊ���ڶ�Ӧ�ļ�����
            //����ǿ�ȵͣ����������������ڲ����ã������ж��Ƿ���ڶ�Ӧ������
            LastError = 0;
            if (!b_ini) { LastError = Ini(); if (LastError != 0)return false; }//���û�н��г�ʼ�������Ƚ��г�ʼ������
            //�������ɿ�������������ɵļ��ܺ�ļ��ܱ��ʽ���Զ����Ƶ������ʾ
            //ע�⣬������ܱ��ʽ�����ģ��������Զ���Ŀ�����Կ������������Կ�������ɣ�
            //�����ڳ��������Ӷ�YCheckKey_1Array������м����У֤���Ӷ���������Ӱ�ȫ�ԣ�����͵�ԭ����ǽ������ȫ���򲿷���ӣ�������Ƿ���ȶ��Ľ�����������������˳��������ת������ط�
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


            //ʹ����Ȩ�ŶԼ����ļ�����ע�ᣬ
            //���º����������Ƶļ��ܺ�ļ��ܱ��ʽ�͵��������н��н��ܲ����¼��ܣ�ʹ����Ӧ����Ȩ�ţ�����ע���ת��
            //Ȼ�󷵻ؼ��ܺ�����ݣ��ü��ܺ��������ü��������Ӧ��
            //ֻ�б���Ӧ����Ȩ�Ž���ת�������ݲ��ܱ���Ӧ�ļ��������ܲ�����
            //����KeyΪ������Ӧ����Ȩ��

            if (!YCheckKey_1_alread)//����ѽ�����ע��Ĺ��̣����ٽ���ע�ᣬ���ڼӿ���������ٶȣ���Ϊע�����ڼ������н��У�
            {
                sbyte errcode = 0; int i;
                //��������
                for (i = 0; i < 522; i++) YCheckKey_1_Array[i] = EncByte[i];

                {
                    if (!EdccBin(KeyAuth, YCheckKey_1_Array, 522, ref errcode, KeyPath)) { LastError = errcode; return false; }; YCheckKey_1_alread = true;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////
            //���������
            System.Random rnd = new System.Random();
            pc_d0 = d0 = rnd.Next(0, 2147483646); pc_d1 = d1 = rnd.Next(0, 2147483646); pc_d2 = d2 = rnd.Next(0, 2147483646); pc_d3 = d3 = rnd.Next(0, 2147483646);
            //�����ڳ���˶���������м�������
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

            //���½�������͵�������������������

            {
                LastError = CalPub(YCheckKey_1_Array, 522, ref d0, ref d1, ref d2, ref d3, ref d4, ref d5, ref d6, ref d7,
                                ref F[0], ref F[1], ref F[2], ref F[3], ref F[4], ref F[5], ref F[6], ref F[7],
                                s0, s1, s2, s3, s4, s5, s6, s7, KeyPath, 20000000);
            }
            if (LastError != 0 && LastError != -43) { return false; }
            //�����ͬ������ڶ�Ӧ�����������򣬲����ڶ�Ӧ�ļ�����
            if ((d0 == pc_d0) && (d1 == pc_d1) && (d2 == pc_d2) && (d3 == pc_d3))
            {
                return true;
            }
            return false;
        }


        //�������YCompareStringNoCase
        //Ŀ�ģ������������ַ������бȽ�(���Դ�Сд)
        //����ins1��Ҫ�Ƚϵ������ַ���֮һ
        //����ins2��Ҫ�Ƚϵ������ַ���֮һ
        //���ؽ�������Ϊ�棬���������ַ�����ȣ�����Ϊ��
        //���������LastError=0��������ȷ�����LastErrorΪ��������μ������ֲ�

        byte[] YCompareStringNoCase_2_Array = new byte[610];
        static bool YCompareStringNoCase_2_alread;
        public bool YCompareStringNoCase_2(string ins1, string ins2)
        {
            ins1 = ins1.ToLower(); ins2 = ins2.ToLower();
            int d0 = 0, d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0, d7 = 0; int n;
            double[] F = new double[8];
            StringBuilder s0 = new StringBuilder("", 50), s1 = new StringBuilder("", 50), s2 = new StringBuilder("", 50), s3 = new StringBuilder("", 50);
            StringBuilder s4 = new StringBuilder("", 50), s5 = new StringBuilder("", 50), s6 = new StringBuilder("", 50), s7 = new StringBuilder("", 50);

            //ԭ���ȶ�Ҫ��������Ĳ����ڳ���˽��м������㣬Ȼ�������жԸò������н������㲢���бȽϣ�Ȼ�󷵻ؽ���������
            LastError = 0;
            if (!b_ini) { LastError = Ini(); if (LastError != 0)return false; }//���û�н��г�ʼ�������Ƚ��г�ʼ������
            //�������ɿ�������������ɵļ��ܺ�ļ��ܱ��ʽ���Զ����Ƶ������ʾ
            //ע�⣬������ܱ��ʽ�����ģ��������Զ���Ŀ�����Կ������������Կ�������ɣ�
            //�����ڳ��������Ӷ�YCompareStringNoCase_2Array������м����У֤���Ӷ���������Ӱ�ȫ�ԣ�����͵�ԭ����ǽ������ȫ���򲿷���ӣ�������Ƿ���ȶ��Ľ�����������������˳��������ת������ط�
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


            //ʹ����Ȩ�ŶԼ����ļ�����ע�ᣬ
            //���º����������Ƶļ��ܺ�ļ��ܱ��ʽ�͵��������н��н��ܲ����¼��ܣ�ʹ����Ӧ����Ȩ�ţ�����ע���ת��
            //Ȼ�󷵻ؼ��ܺ�����ݣ��ü��ܺ��������ü��������Ӧ��
            //ֻ�б���Ӧ����Ȩ�Ž���ת�������ݲ��ܱ���Ӧ�ļ��������ܲ�����
            //����KeyΪ������Ӧ����Ȩ��

            if (!YCompareStringNoCase_2_alread)//����ѽ�����ע��Ĺ��̣����ٽ���ע�ᣬ���ڼӿ���������ٶȣ���Ϊע�����ڼ������н��У�
            {
                sbyte errcode = 0; int i;
                //��������
                for (i = 0; i < 610; i++) YCompareStringNoCase_2_Array[i] = EncByte[i];

                {
                    if (!EdccBin(KeyAuth, YCompareStringNoCase_2_Array, 610, ref errcode, KeyPath)) { LastError = errcode; return false; }; YCompareStringNoCase_2_alread = true;
                }
            }
            //////////////////////////////////////////////////////////////////////////////////////
            //����һ���ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d0-d2,���ڼ����Ժ�ļ�������
            byte[] temp_string_1 = System.Text.Encoding.Default.GetBytes(ins1); int nLen_1 = temp_string_1.Length;
            byte[] leave_string_1 = new byte[nLen_1];
            if (nLen_1 > 0) { StringToDword(ref d0, ref d1, ref d2, temp_string_1, 0); leave_string_1[0] = 0; }
            //������µ��ַ���
            int temp_len = nLen_1 - 12;
            for (n = 0; n < temp_len; n++) leave_string_1[n] = temp_string_1[12 + n];
            //���ڶ����ַ�����ǰ12�ֽڷֽ�Ϊ3�������Σ���ֵ��d3-d5,���ڼ����Ժ�ļ�������
            byte[] temp_string_2 = System.Text.Encoding.Default.GetBytes(ins2); int nLen_2 = temp_string_2.Length;
            byte[] leave_string_2 = new byte[nLen_2];
            if (nLen_2 > 0) { StringToDword(ref d3, ref d4, ref d5, temp_string_2, 0); leave_string_2[0] = 0; }
            //������µ��ַ���
            temp_len = nLen_2 - 12;
            for (n = 0; n < temp_len; n++) leave_string_2[n] = temp_string_2[12 + n];
            //�����ڳ���˶�����������м�������
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

            //���½����ܺ�Ĳ����͵������������������㣬�������ܺ�Ľ���ϲ�Ϊ�ַ������ٽ��бȽ�

            {
                LastError = CalPub(YCompareStringNoCase_2_Array, 610, ref d0, ref d1, ref d2, ref d3, ref d4, ref d5, ref d6, ref d7,
                                ref F[0], ref F[1], ref F[2], ref F[3], ref F[4], ref F[5], ref F[6], ref F[7],
                                s0, s1, s2, s3, s4, s5, s6, s7, KeyPath, 20000000);
            }
            if (LastError != 0 && LastError != -43) { return false; }
            //���û�з��ش����򽫽��ܲ�����Ľ������;
            if (d7 == 0) return false;//���d7��Ϊ0���������ڽ��бȽϵĽ���������ֱ�ӷ��ؼ�
            //������ڱȽ�������ٱȽ����µĲ���
            return (lstrcmpi(temp_string_1, temp_string_2) == 0); ;
        }



        public bool GetAuthFromFileEx(string IniFile, ref string OutHKey, ref string OutLKey, ref string OutSetTime, ref uint OutID)
        {
            StringBuilder HKey;//��Ȩ��ĸ�λ
            StringBuilder LKey;//��Ȩ��ĵ�λ
            StringBuilder SetTime;//���õ���Ȩʱ��
            HKey = new StringBuilder("", 50); LKey = new StringBuilder("", 50); SetTime = new StringBuilder("", 100);
            //��INI�ļ��л����Ȩ��ĸߵ�λ�����õ���Ȩʱ�估��ȨID�ȡ�Զ�̸���ʱ��ʱ��ֱ�Ӹ���INI�ļ��Ϳ�����
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
                    return "δ�ҵ����ؽ������";

                case -2:
                    return "δ�ҵ� = ����";

                case -3:
                    return "����û���ҵ���Ӧ����";

                case -5:
                    return "�����Ҳ����ַ����ĵ�һ��˫����";

                case -6:
                    return "�����Ҳ����ַ����ĵڶ���˫����";

                case -7:
                    return "IF���û���ҵ�goto�ַ�";

                case -8:
                    return "IF���û���ҵ���һ���Ƚ��ַ�";

                case -9:
                    return "IF���û���ҵ��ȽϷ���";

                case -10:
                    return "���߱������Ͳ����";

                case -11:
                    return "û���ҵ�NOT����";

                case -12:
                    return "�������α���";

                case -13:
                    return "����û���ҵ���Ӧ���γ���";

                case -14:
                    return "����û���ҵ���Ӧ�ַ�������";

                case -15:
                    return "����û���ҵ���Ӧ���㳣��";

                case -16:
                    return "����֧���������";

                case -17:
                    return "����û���������";

                case -18:
                    return "����û�б���";

                case -19:
                    return "����û������";

                case -20:
                    return "����û���ұ�����";

                case -21:
                    return "����������ָ�ⶨ�ķ�Χ";

                case -22:
                    return "���������ĵ�ַ����ָ���ķ�Χ���������ܳ���EEPROM_LEN-4�����㲻�ܳ���30720-8";

                case -23:
                    return "���������ĵ�ַ����ָ���ķ�Χ���ַ������ܳ���EEPROM_LEN-LEN������LENΪ�ַ����ĳ���";

                case -24:
                    return "�����У�����������Ϊ0";

                case -25:
                    return "δ֪����";

                case -26:
                    return "�ڶ�����������ָ����λ��";

                case -27:
                    return "�ַ�����������ָ���ĳ���";

                case -28:
                    return "�����ַ�������";

                case -29:
                    return "û�е���������";

                case -30:
                    return "GOTO�ı�ʶ��䲻��ȫΪ����";

                case -31:
                    return "���ܴ�ENC�ļ�";

                case -32:
                    return "���ܶ�ENC�ļ�";

                case -33:
                    return "GOTO CALL�����ҵ�ָ������תλ��";

                case -34:
                    return "���ܿ������У�δ֪��������";

                case -35:
                    return "���ܿ������У�δ֪��������";

                case -36:
                    return "�ַ������ȳ���50";

                case -37:
                    return "RIGHT����ʱ����������";

                case -38:
                    return "��ʶ�ظ�";

                case -39:
                    return "�����ջ���";

                case -40:
                    return "��ջ���";

                case -41:
                    return "���ܽ��������ļ�����鿴�ļ��Ƿ���ֻ�����ԣ��������ļ���";

                case -42:
                    return "����д�ļ�����鿴�ļ��Ƿ���ֻ�����ԣ��������ļ���";

                case -43:
                    return "������;ʹ��END������";

                case -44:
                    return "������ת���ⲿ�Ŀռ�";

                case -45:
                    return "��������ʧ��";

                case -46:
                    return "���򳬳������������������ѭ��";

                case -47:
                    return "д���벻��ȷ";

                case -48:
                    return "�����벻��ȷ";

                case -49:
                    return "��дEEPROMʱ����ַ���";

                case -50:
                    return "USB����ʧ�ܣ�������û���ҵ���ص�ָ��";

                case -51:
                    return "��USB�ļ����ʧ��";

                case -52:
                    return "ʹ�ü����������Զ�����ʽ�����ɼ��ܴ���ʱ��������";

                case -53:
                    return "�޷���usb�豸��������������û�а�װ��û�в����������";
                case -63:
                    return "���ܴ�ָ�����ļ���";
                case -64:
                    return "���ܽ���ָ�����ļ���";
                case -65:
                    return "��֤����󣬿��������������Կ���󣬻�ע����Ȩ�����";
                case -66:
                    return "ִ��TIMEOUT������UPDATE����ʱ�������ID����ID�����";
                case -67:
                    return "ִ��TIMEOUT����ʱ�����ܿ����к����ѵ���";
                case -68:
                    return "������������ʱ������Ĳ������ᵼ�·���ֵ��һ������ֵ";
                case -69:
                    return "����û�㹻�ı�������";
                case -70:
                    return "���ر����뺯�������";
                case -71:
                    return "������ת���ַ���ʱ��Ҫת��������̫��";
                case -72:
                    return "��ʼ������������";
                case -73:
                    return "�Ի���������MD5����ʱ����";
                case -74:
                    return "MD5��֤IPVAR����";
                case -75:
                    return "MD5��֤IPCount����";
                case -76:
                    return "û���ҵ���Ӧ��SOCKET����";
                case -77:
                    return "û���ҵ�Ҫɾ���Ķ�Ӧ��SOCKET����";
                case -78:
                    return "û���ҵ�Ҫɾ���Ķ�Ӧ�Ĺ���ģ�������";
                case -79:
                    return "û���ҵ�Ҫ���ӵĶ�Ӧ�Ĺ���ģ�������";
                case -80:
                    return "�û����ѳ������Ƶ���Ȩ����";
                case -81:
                    return "�Ҳ�����Ӧ��INI�ļ���Ŀ";
                case -82:
                    return "û�н��г�ʼ����������";
                case -252:
                    return "���벻��ȷ";
                case -1088:
                    return "�������ݴ���";
                case -1089:
                    return "��ȡ���ݴ���";
                case -1092:
                    return "�Ҳ�����Ӧ�ķ���˲�����";
                case -1093:
                    return "��ʾ���ӷ���ʱ����";
                case -1095:
                    return "��ȡ��������ʧ��";
                case -1097:
                    return "�������ֽӴ���";
                case -1098:
                    return "�����ֽڶ˿ڴ���";
                case -1099:
                    return "��ʾ��Ч���ӣ����ܽ�����صĲ�����";
                case -2002:
                    return "��ʾ����ʱ��������";
                case -2003:
                    return "��ʾ���͵����ݳ�������յ����ݳ��Ȳ����";
                case -2005:
                    return "��ʾ��ǰ���񲻴����κ�����";
                case -2006:
                    return "��ʾ��ǰ��ѯ�ڵ㳬�����Ϸ���Χ";
                case -2009:
                    return "��ʾ�ر����Ӵ���";
                case -1052:
                    return "�������������Ȩ�Ų���ȷ��";
                case -1053:
                    return "ϵͳ��û���κ���������";

                default:
                    return "δ֪�������";
            }
        }
    }
}
