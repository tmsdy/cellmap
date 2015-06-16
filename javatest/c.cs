namespace javatest
{
    using System;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.Text;

    public class c
    {
        private static char[] _a = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
        private static char[] _if1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/.".ToCharArray();

        private static char[] a(byte[] abyte0)
        {
            char[] chArray = new char[0x20];
            try
            {
                byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(abyte0);
                int num = 0;
                for (int i = 0; i < 0x10; i++)
                {
                    byte x = buffer[i];
                    chArray[num++] = _a[foo(x, 4) & 15];
                    chArray[num++] = _a[x & 15];
                }
            }
            catch
            {
            }
            return chArray;
        }

        public static string a(string s)
        {
            try
            {
                char[] sourceArray = a(Encoding.UTF8.GetBytes(new StringBuilder(Convert.ToString(s)).Append("webgis").ToString()));
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                byte[] buffer2 = new byte[bytes.Length + 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    buffer2[i] = bytes[i];
                }
                char[] destinationArray = new char[2];
                Array.Copy(sourceArray, 10, destinationArray, 0, 2);
                StringBuilder builder = new StringBuilder();
                builder.Append(destinationArray);
                buffer2[bytes.Length] = (byte) (0xff & int.Parse(builder.ToString(), NumberStyles.HexNumber));
                char[] chArray3 = new char[2];
                Array.Copy(sourceArray, 20, chArray3, 0, 2);
                StringBuilder builder2 = new StringBuilder();
                builder2.Append(chArray3);
                buffer2[bytes.Length + 1] = (byte) (0xff & int.Parse(builder2.ToString(), NumberStyles.HexNumber));
                char[] chArray4 = new char[2];
                Array.Copy(sourceArray, 6, chArray4, 0, 2);
                StringBuilder builder3 = new StringBuilder();
                builder3.Append(chArray4);
                string str2 = "";
                str2 = new StringBuilder(Convert.ToString(str2)).Append((char) (0xff & int.Parse(builder3.ToString(), NumberStyles.HexNumber))).ToString();
                char[] chArray5 = new char[2];
                Array.Copy(sourceArray, 0x10, chArray5, 0, 2);
                StringBuilder builder4 = new StringBuilder();
                builder4.Append(chArray5);
                str2 = new StringBuilder(Convert.ToString(str2)).Append((char) (0xff & int.Parse(builder4.ToString(), NumberStyles.HexNumber))).ToString();
                char[] chArray6 = new char[2];
                Array.Copy(sourceArray, 0x1a, chArray6, 0, 2);
                StringBuilder builder5 = new StringBuilder();
                builder5.Append(chArray6);
                str2 = new StringBuilder(Convert.ToString(str2)).Append((char) (0xff & int.Parse(builder5.ToString(), NumberStyles.HexNumber))).ToString();
                StringBuilder builder6 = new StringBuilder(Convert.ToString(str2));
                builder6.Append("webgis");
                char[] chArray7 = a(Encoding.GetEncoding("iso-8859-1").GetBytes(builder6.ToString()));
                int length = buffer2.Length;
                int num3 = str2.Length;
                byte[] buffer3 = new byte[length + num3];
                for (int j = 0; j < ((length + 0x1f) / 0x20); j++)
                {
                    int num5 = j * 0x20;
                    for (int m = 0; (m < 0x20) && ((num5 + m) < length); m++)
                    {
                        buffer3[num5 + m] = (byte) ((chArray7[m] & '\x00ff') ^ (buffer2[num5 + m] & 0xff));
                    }
                }
                for (int k = 0; k < num3; k++)
                {
                    buffer3[length + k] = (byte) str2.ToCharArray()[k];
                }
                return new string(if1(buffer3));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            return "UnsupportedEncodingException";
        }

        public static int foo(int x, int y)
        {
            int num = 0x7fffffff;
            for (int i = 0; i < y; i++)
            {
                x = x >> 1;
                x &= num;
            }
            return x;
        }

        private static char[] if1(byte[] abyte0)
        {
            char[] chArray = new char[((abyte0.Length + 2) / 3) * 4];
            int index = 0;
            for (int i = 0; index < abyte0.Length; i += 4)
            {
                bool flag = false;
                bool flag2 = false;
                int num3 = 0xff & abyte0[index];
                num3 = num3 << 8;
                if ((index + 1) < abyte0.Length)
                {
                    num3 |= 0xff & abyte0[index + 1];
                    flag2 = true;
                }
                num3 = num3 << 8;
                if ((index + 2) < abyte0.Length)
                {
                    num3 |= 0xff & abyte0[index + 2];
                    flag = true;
                }
                chArray[i + 3] = _if1[flag ? (0x3f - (num3 & 0x3f)) : 0x40];
                num3 = num3 >> 6;
                chArray[i + 2] = _if1[flag2 ? (0x3f - (num3 & 0x3f)) : 0x40];
                num3 = num3 >> 6;
                chArray[i + 1] = _if1[0x3f - (num3 & 0x3f)];
                num3 = num3 >> 6;
                chArray[i] = _if1[0x3f - (num3 & 0x3f)];
                index += 3;
            }
            return chArray;
        }
    }
}

