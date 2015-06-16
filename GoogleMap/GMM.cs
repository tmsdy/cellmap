namespace GoogleMap
{
    using System;
    using System.IO;
    using System.Net;

    internal class GMM
    {
        public static string GetLatLng(string[] args)
        {
            if (args.Length < 4)
            {
                return string.Empty;
            }
            string str = "";
            if (args.Length == 5)
            {
                str = args[4].ToLower();
            }
            try
            {
                string uriString = "http://mobilemaps.clients.google.com/glm/mmap";
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(new Uri(uriString));
                request.Method = "POST";
                int mCC = Convert.ToInt32(args[0]);
                int mNC = Convert.ToInt32(args[1]);
                int lAC = Convert.ToInt32(args[2]);
                int cID = Convert.ToInt32(args[3]);
                byte[] buffer = PostData(mCC, mNC, lAC, cID, str == "shortcid");
                request.ContentLength = buffer.Length;
                request.ContentType = "application/binary";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(buffer, 0, buffer.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();
                byte[] buffer2 = new byte[response.ContentLength];
                for (int i = 0; i < buffer2.Length; i += response.GetResponseStream().Read(buffer2, i, buffer2.Length - i))
                {
                }
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte num1 = buffer2[0];
                    byte num9 = buffer2[1];
                    byte num10 = buffer2[2];
                    if (((((buffer2[3] << 0x18) | (buffer2[4] << 0x10)) | (buffer2[5] << 8)) | buffer2[6]) == 0)
                    {
                        double num7 = ((double) ((((buffer2[7] << 0x18) | (buffer2[8] << 0x10)) | (buffer2[9] << 8)) | buffer2[10])) / 1000000.0;
                        double num8 = ((double) ((((buffer2[11] << 0x18) | (buffer2[12] << 0x10)) | (buffer2[13] << 8)) | buffer2[14])) / 1000000.0;
                        return (num7 + "," + num8);
                    }
                    return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static byte[] PostData(int MCC, int MNC, int LAC, int CID, bool shortCID)
        {
            byte[] buffer = new byte[] { 
                0, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0x1b, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0xff, 
                0xff, 0xff, 0xff, 0, 0, 0, 0
             };
            if (CID > 0xffffL)
            {
                Console.WriteLine("UMTS CID. {0}", shortCID ? "Using short CID to resolve." : "");
            }
            else
            {
                Console.WriteLine("GSM CID given.");
            }
            if (shortCID)
            {
                CID &= 0xffff;
            }
            if (CID > 0x10000L)
            {
                buffer[0x1c] = 5;
            }
            else
            {
                buffer[0x1c] = 3;
            }
            buffer[0x11] = (byte) ((MNC >> 0x18) & 0xff);
            buffer[0x12] = (byte) ((MNC >> 0x10) & 0xff);
            buffer[0x13] = (byte) ((MNC >> 8) & 0xff);
            buffer[20] = (byte) (MNC & 0xff);
            buffer[0x15] = (byte) ((MCC >> 0x18) & 0xff);
            buffer[0x16] = (byte) ((MCC >> 0x10) & 0xff);
            buffer[0x17] = (byte) ((MCC >> 8) & 0xff);
            buffer[0x18] = (byte) (MCC & 0xff);
            buffer[0x27] = (byte) ((MNC >> 0x18) & 0xff);
            buffer[40] = (byte) ((MNC >> 0x10) & 0xff);
            buffer[0x29] = (byte) ((MNC >> 8) & 0xff);
            buffer[0x2a] = (byte) (MNC & 0xff);
            buffer[0x2b] = (byte) ((MCC >> 0x18) & 0xff);
            buffer[0x2c] = (byte) ((MCC >> 0x10) & 0xff);
            buffer[0x2d] = (byte) ((MCC >> 8) & 0xff);
            buffer[0x2e] = (byte) (MCC & 0xff);
            buffer[0x1f] = (byte) ((CID >> 0x18) & 0xff);
            buffer[0x20] = (byte) ((CID >> 0x10) & 0xff);
            buffer[0x21] = (byte) ((CID >> 8) & 0xff);
            buffer[0x22] = (byte) (CID & 0xff);
            buffer[0x23] = (byte) ((LAC >> 0x18) & 0xff);
            buffer[0x24] = (byte) ((LAC >> 0x10) & 0xff);
            buffer[0x25] = (byte) ((LAC >> 8) & 0xff);
            buffer[0x26] = (byte) (LAC & 0xff);
            return buffer;
        }
    }
}

