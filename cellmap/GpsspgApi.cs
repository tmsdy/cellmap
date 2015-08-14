namespace cellmap
{
    using mCellmapManager;
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    internal class GpsspgApi
    {
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public long ConvertDateTimeInt(DateTime time)
        {
            DateTime time2 = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1, 0, 0, 0, 0));
            return ((time.Ticks - time2.Ticks) / 0x2710L);
        }

        public static string GetGpsspgJson(string mnc, string lac, string cell)
        {
            string str = "http://api.gpsspg.com/bs/?oid=159&mcc=460&mnc=" + mnc + "&a=" + lac + "&b=" + cell + "&hex=10&type=&to=1&output=json";
            string str2 = SendDataByGET("http://api.gpsspg.com/bs/?oid=159&mcc=460&mnc=" + mnc + "&a=" + lac + "&b=" + cell + "&hex=10&type=&to=1&output=jsonp&callback=jQuery110207112049707211554_1415929124658&_=1415929124659", "", new CookieContainer(), "http://www.gpsspg.com/bs.htm", Encoding.UTF8);
            return str2.Substring(str2.IndexOf("(") + 1, str2.LastIndexOf(")") - str2.IndexOf("("));
        }

        public static string GetGsmCellInfo(string lac, string cell)
        {
            try
            {
                string str;
                string str2;
                string str3;
                string str4;
                string str5;
                string str6;
                //先解析移动
                JObject obj2 = JObject.Parse(GetGpsspgJson("00", lac, cell));
                string str8 = obj2["status"].ToString();
                if (str8.Substring(1, str8.Length - 2) == "200")
                {
                    str = obj2["result"]["lats"].ToString();
                    str2 = obj2["result"]["lngs"].ToString();
                    string[] strArray = CellmapManager.FixGpsApi(Convert.ToDouble(str), Convert.ToDouble(str2)).Split(new char[] { ',' });
                    str3 = strArray[1];
                    str4 = strArray[0];
                    str5 = obj2["result"]["address"].ToString();
                    str5 = str5.Substring(1, str5.Length - 2);
                    str6 = obj2["result"]["radius"].ToString();
                    CellmapManager.InsertMysqlCellDate("0", lac, cell, str, str2, str3, str4, str5, str6);
                    return (lac + "," + cell + "," + str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + ",");
                }
                //再解析联通
                obj2 = JObject.Parse(GetGpsspgJson("01", lac, cell));
                str8 = obj2["status"].ToString();
                if (str8.Substring(1, str8.Length - 2) == "200")
                {
                    str = obj2["result"]["lats"].ToString();
                    str2 = obj2["result"]["lngs"].ToString();
                    string[] strArray2 = CellmapManager.FixGpsApi(Convert.ToDouble(str), Convert.ToDouble(str2)).Split(new char[] { ',' });
                    str3 = strArray2[1];
                    str4 = strArray2[0];
                    str5 = obj2["result"]["address"].ToString();
                    str5 = str5.Substring(1, str5.Length - 2);
                    str6 = obj2["result"]["radius"].ToString();
                    CellmapManager.InsertMysqlCellDate("1", lac, cell, str, str2, str3, str4, str5, str6);
                    return (lac + "," + cell + "," + str + "," + str2 + "," + str3 + "," + str4 + "," + str5 + "," + str6 + ",");
                }
                return "null";
            }
            catch (Exception)
            {
                return "null";
            }
        }

        public static string SendDataByGET(string Url, string postDataStr, CookieContainer cookie, string reff, Encoding e2)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                if (Url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(GpsspgApi.CheckValidationResult);
                    request = WebRequest.Create(Url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version11;
                }
                else
                {
                    request = (HttpWebRequest) WebRequest.Create(Url + ((postDataStr == "") ? "" : "?") + postDataStr);
                }
                request.CookieContainer = cookie;
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 5.1; rv:21.0) Gecko/20100101 Firefox/21.0";
                request.Headers["Accept-Language"] = "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3";
                request.KeepAlive = true;
                request.ServicePoint.Expect100Continue = false;
                //Referer必须为http://www.gpsspg.com/bs.htm
                request.Referer = reff;
                response = (HttpWebResponse) request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream, e2);
                string str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
                request.Abort();
                return str;
            }
            catch
            {
                if (request != null)
                {
                    request.Abort();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                return "";
            }
        }
    }
}

