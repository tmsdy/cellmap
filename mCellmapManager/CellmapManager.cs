namespace mCellmapManager
{
    using cellmap;
    using cellmap.Properties;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Drawing;
    using System.Drawing.Text;
    using System.IO;
    using System.Net;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Web;
    using System.Windows.Forms;

    internal class CellmapManager
    {
        private static double a = 6378245.0;
        private static string CdmaDataSourceFile = "CdmaCellDataSource.txt";
        private static double ee = 0.0066934216229659433;
        private static string GsmDataSourceFile = "GsmCellDataSource.txt";
        public static string GsmNullFile = "GsmCellInfoAboutNull.txt";
        public static string inifile = @"config\User.ini";
        private static double pi = 3.1415926535897931;
        public static string WebSite = "http://www.cellmap.cn/cellmapapi/";

        public static string addressResponseJson(string responseJson)
        {
            try
            {
                JObject obj2 = JObject.Parse(responseJson);
                string str = obj2["location"]["address"]["country"].ToString();
                str = str.Substring(1, str.Length - 2);
                string str2 = obj2["location"]["address"]["region"].ToString();
                str2 = str2.Substring(1, str2.Length - 2);
                string str3 = obj2["location"]["address"]["city"].ToString();
                str3 = str3.Substring(1, str3.Length - 2);
                string str4 = obj2["location"]["address"]["street"].ToString();
                str4 = str4.Substring(1, str4.Length - 2);
                string str5 = obj2["location"]["address"]["street_number"].ToString();
                str5 = str5.Substring(1, str5.Length - 2);
                return (str + str2 + str3 + str4 + str5);
            }
            catch
            {
                return "";
            }
        }

        public static string CopyResourcesFile(string filename)
        {
            try
            {
                byte[] array = new byte[Resources.offset.Length];
                Resources.offset.CopyTo(array, 0);
                FileStream stream = new FileStream("offset.mdb", FileMode.Create, FileAccess.Write);
                stream.Write(array, 0, array.Length);
                stream.Close();
            }
            catch
            {
            }
            return "finish";
        }

        public static string FixGpsApi(double wgLat, double wgLon)
        {
            double num;
            double num2;
            if (outOfChina(wgLat, wgLon))
            {
                num = wgLat;
                num2 = wgLon;
                return "0,0";
            }
            double num3 = transformLat(wgLon - 105.0, wgLat - 35.0);
            double num4 = transformLon(wgLon - 105.0, wgLat - 35.0);
            double a = (wgLat / 180.0) * pi;
            double d = Math.Sin(a);
            d = 1.0 - ((ee * d) * d);
            double num7 = Math.Sqrt(d);
            num3 = (num3 * 180.0) / (((CellmapManager.a * (1.0 - ee)) / (d * num7)) * pi);
            num4 = (num4 * 180.0) / (((CellmapManager.a / num7) * Math.Cos(a)) * pi);
            num = wgLat + num3;
            num2 = wgLon + num4;
            return (num2 + "," + num);
        }

        public GsmCellInfo Get()
        {
            GsmCellInfo info;
            info.lat = 0.0;
            info.lng = 0.0;
            return info;
        }

        public static string GetCdmaCellInfo(string sid, string nid, string bid)
        {
            string newcellinfo = GetCdmaCellInfoFromLocalDataSource(sid, nid, bid);
            if (newcellinfo == "null")
            {
                newcellinfo = GetCdmaCellInfoFromWebSite(sid, nid, bid);
                if (newcellinfo != "null")
                {
                    newcellinfo = sid + "," + nid + "," + bid + "," + newcellinfo;
                    write2file(CdmaDataSourceFile, true, newcellinfo);
                }
            }
            return newcellinfo;
        }

        public static string GetCdmaCellInfoFromLocalDataSource(string sid, string nid, string bid)
        {
            string str = "null";
            string str2 = "";
            try
            {
                StreamReader reader = new StreamReader(CdmaDataSourceFile, Encoding.Default);
                while (str2 != null)
                {
                    str2 = reader.ReadLine();
                    try
                    {
                        string[] strArray = str2.Split(new char[] { ',' });
                        if (((sid == strArray[0]) && (nid == strArray[1])) && (bid == strArray[2]))
                        {
                            str = str2;
                            reader.Close();
                            return str;
                        }
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
                reader.Close();
            }
            catch
            {
            }
            return str;
        }

        public static string GetCdmaCellInfoFromWebSite(string sid, string nid, string bid)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "cellmap_cdma2gps_api.aspx?sid=" + sid + "&nid=" + nid + "&bid=" + bid;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetCdmaCitySidNid(string city)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                Encoding e = Encoding.GetEncoding("GB2312");
                string requestUriString = WebSite + "cellmap_city_sid_api.aspx?city=" + HttpUtility.UrlEncode(city, e);
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGps2AddressFromAMap(string lat, string lng, string coordsys)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = "http://restapi.amap.com/v3/geocode/regeo?key=975821407c6999bc25289121c08ebbe6&location=" + lng + "," + lat + "&coordsys=" + coordsys;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                string json = reader.ReadToEnd();
                reader.Close();
                response.Close();
                string str4 = JObject.Parse(json)["regeocode"]["formatted_address"].ToString();
                str = str4.Substring(1, str4.Length - 2);
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGps2AddressFromBaidu(string lat, string lng)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "gps2baiduaddress.aspx?lat=" + lat + "&lng=" + lng;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGsmCellFuzzyResearch(string lac, string cell)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "cellmap_gsm2gps_fuzzy_api.aspx?lac=" + lac + "&cell=" + cell;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "error";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGsmCellInfo(string lac, string cellid)
        {
            string gsmCellInfoFromLocalFile = GetGsmCellInfoFromLocalFile(lac, cellid);
            if (gsmCellInfoFromLocalFile == "null")
            {
                gsmCellInfoFromLocalFile = GetGsmCellInfoFromLbsbase(lac, cellid);
                if (gsmCellInfoFromLocalFile != "null")
                {
                    write2file(GsmDataSourceFile, true, gsmCellInfoFromLocalFile);
                }
            }
            return gsmCellInfoFromLocalFile;
        }

        public static string GetGsmCellInfoCloud(string lac, string cellid)
        {
            string gsmCellInfoFromLocalFile = GetGsmCellInfoFromLocalFile(lac, cellid);
            if (gsmCellInfoFromLocalFile == "null")
            {
                gsmCellInfoFromLocalFile = GetGsmCellInfoFromLbsbase(lac, cellid);
                if (gsmCellInfoFromLocalFile == "null")
                {
                    gsmCellInfoFromLocalFile = GpsspgApi.GetGsmCellInfo(lac, cellid);
                }
                if (gsmCellInfoFromLocalFile != "null")
                {
                    write2file(GsmDataSourceFile, true, gsmCellInfoFromLocalFile);
                }
            }
            return gsmCellInfoFromLocalFile;
        }

        public static string GetGsmCellInfoFromCellmapApi(string lac, string cellid)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "cellmap_gsm2gps_api.aspx?lac=" + lac + "&cell=" + cellid;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGsmCellInfoFromLbsbase(string lac, string cellid)
        {
            string gsmCellInfoFromCellmapApi = GetGsmCellInfoFromCellmapApi(lac, cellid);
            if (gsmCellInfoFromCellmapApi != "null")
            {
                return (lac + "," + cellid + "," + gsmCellInfoFromCellmapApi);
            }
            return "null";
        }

        public static string GetGsmCellInfoFromLbsbaseMySql(string lac, string cellid)
        {
            try
            {
                string gsmCellInfoFromMysql = GetGsmCellInfoFromMysql(lac, cellid);
                if (gsmCellInfoFromMysql == "null")
                {
                    return "null";
                }
                return (lac + "," + cellid + "," + gsmCellInfoFromMysql);
            }
            catch
            {
                return "null";
            }
        }

        public static string GetGsmCellInfoFromLocalFile(string lac, string cell)
        {
            string str = "null";
            string str2 = "";
            try
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\data\" + GsmDataSourceFile, Encoding.Default);
                while (str2 != null)
                {
                    str2 = reader.ReadLine();
                    try
                    {
                        string[] strArray = str2.Split(new char[] { ',' });
                        if ((lac == strArray[0]) && (cell == strArray[1]))
                        {
                            str = str2;
                            reader.Close();
                            return str;
                        }
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
                reader.Close();
            }
            catch
            {
            }
            return str;
        }

        public static string GetGsmCellInfoFromLocalFileAboutNull(string lac, string cell)
        {
            string str = "null";
            string str2 = "";
            try
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\data\" + GsmNullFile, Encoding.Default);
                while (str2 != null)
                {
                    str2 = reader.ReadLine();
                    try
                    {
                        string[] strArray = str2.Split(new char[] { ',' });
                        if ((lac == strArray[0]) && (cell == strArray[1]))
                        {
                            str = "exist";
                            reader.Close();
                            return str;
                        }
                        continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
                reader.Close();
            }
            catch
            {
            }
            return str;
        }

        public static string GetGsmCellInfoFromMysql(string lac, string cellid)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "cellmap_gsm2gps_mysql_api.aspx?lac=" + lac + "&cell=" + cellid;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGsmCellInfoMysql(string lac, string cellid)
        {
            string gsmCellInfoFromLocalFile = GetGsmCellInfoFromLocalFile(lac, cellid);
            if (gsmCellInfoFromLocalFile == "null")
            {
                gsmCellInfoFromLocalFile = GetGsmCellInfoFromLbsbaseMySql(lac, cellid);
                if (gsmCellInfoFromLocalFile != "null")
                {
                    write2file(GsmDataSourceFile, true, gsmCellInfoFromLocalFile);
                }
            }
            return gsmCellInfoFromLocalFile;
        }

        public static string GetGsmCityLac(string city)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                Encoding e = Encoding.GetEncoding("GB2312");
                string requestUriString = WebSite + "cellmap_city_lac_api.aspx?city=" + HttpUtility.UrlEncode(city, e);
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetGsmLac(string lac)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                Encoding e = Encoding.GetEncoding("GB2312");
                string requestUriString = WebSite + "cellmap_lac_api.aspx?lac=" + HttpUtility.UrlEncode(lac, e);
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string GetNeighCellInfo(string lat, string lng, string mnc)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "cellmap_neigh_cells_detail_api.aspx?mnc=" + mnc + "&lat=" + lat + "&lng=" + lng;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "error";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnval, int size, string iniPath);
        public static string GetStringIni(string section, string key)
        {
            StringBuilder returnval = new StringBuilder(0x400);
            string def = null;
            GetPrivateProfileString(section, key, def, returnval, 0x400, Application.StartupPath + @"\" + inifile);
            return returnval.ToString();
        }

        public static string GetUpdateInfo()
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                Encoding.GetEncoding("GB2312");
                string requestUriString = WebSite + "CellmapPcUpdateInfo.txt";
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                responseStream.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "null";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string InsertMysqlCellDate(string mnc, string lac, string cell, string lat, string lng, string olat, string olng, string address, string radius)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                Encoding e = Encoding.GetEncoding("gbk");
                string requestUriString = WebSite + "cellmap_mysql_insert_data.aspx?mnc=" + mnc + "&lac=" + lac + "&cell=" + cell + "&lat=" + lat + "&lng=" + lng + "&olat=" + olat + "&olng=" + olng + "&address=" + HttpUtility.UrlEncode(address, e) + "&radius=" + radius;
                requestUriString = WebSite + "cellmap_mysql_insert_data.aspx?mnc=" + mnc + "&lac=" + lac + "&cell=" + cell + "&lat=" + lat + "&lng=" + lng + "&olat=" + olat + "&olng=" + olng + "&address=" + address + "&radius=" + radius;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                str = reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
                str = "error";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        public static string login(string id, string password)
        {
            string str = "null";
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream responseStream = null;
            StreamReader reader = null;
            try
            {
                string requestUriString = WebSite + "cellmap_user_login_2013.aspx?id=" + id + "&pw=" + password;
                GC.Collect();
                request = (HttpWebRequest) WebRequest.Create(requestUriString);
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                response = request.GetResponse();
                responseStream = response.GetResponseStream();
                reader = new StreamReader(responseStream);
                string str3 = reader.ReadToEnd();
                reader.Close();
                response.Close();
                str = str3.Split(new char[] { ',' })[0];
            }
            catch (Exception)
            {
                str = "error";
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return str;
        }

        private static bool outOfChina(double lat, double lon)
        {
            if (((lon >= 72.004) && (lon <= 137.8347)) && ((lat >= 0.8293) && (lat <= 55.8271)))
            {
                return false;
            }
            return true;
        }

        public static string ParseResponseJson(string responseJson)
        {
            try
            {
                StringBuilder builder = new StringBuilder();
                JObject obj2 = JObject.Parse(responseJson);
                string str = obj2["location"]["latitude"].ToString();
                string str2 = obj2["location"]["longitude"].ToString();
                builder.Append(str);
                builder.Append(",");
                builder.Append(str2);
                return builder.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Bitmap bitmap;
            int width = 0;
            int height = 0;
            StringFormat stringFormat = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bitmap = new Bitmap(1, 1);
                SizeF ef = Graphics.FromImage(bitmap).MeasureString(text, font, PointF.Empty, stringFormat);
                width = (int) (ef.Width + 1f);
                height = (int) (ef.Height + 1f);
                rect = new Rectangle(0, 0, width, height);
                bitmap.Dispose();
                bitmap = new Bitmap(width, height);
            }
            else
            {
                bitmap = new Bitmap(rect.Width, rect.Height);
            }
            Graphics graphics = Graphics.FromImage(bitmap);
            SolidBrush brush = new SolidBrush(fontcolor);
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            graphics.FillRectangle(new SolidBrush(backColor), rect);
            graphics.DrawString(text, font, brush, rect, stringFormat);
            Point point = new Point(0, 0);
            Point point2 = new Point(0, height);
            Point point3 = new Point(width, height);
            Point point4 = new Point(width, 0);
            Point[] points = new Point[] { point, point2, point3, point4 };
            Pen pen = new Pen(Color.Red, 3f);
            graphics.DrawPolygon(pen, points);
            return bitmap;
        }

        private static double transformLat(double x, double y)
        {
            double num = ((((-100.0 + (2.0 * x)) + (3.0 * y)) + ((0.2 * y) * y)) + ((0.1 * x) * y)) + (0.2 * Math.Sqrt(Math.Abs(x)));
            num += (((20.0 * Math.Sin((6.0 * x) * pi)) + (20.0 * Math.Sin((2.0 * x) * pi))) * 2.0) / 3.0;
            num += (((20.0 * Math.Sin(y * pi)) + (40.0 * Math.Sin((y / 3.0) * pi))) * 2.0) / 3.0;
            return (num + ((((160.0 * Math.Sin((y / 12.0) * pi)) + (320.0 * Math.Sin((y * pi) / 30.0))) * 2.0) / 3.0));
        }

        private static double transformLon(double x, double y)
        {
            double num = ((((300.0 + x) + (2.0 * y)) + ((0.1 * x) * x)) + ((0.1 * x) * y)) + (0.1 * Math.Sqrt(Math.Abs(x)));
            num += (((20.0 * Math.Sin((6.0 * x) * pi)) + (20.0 * Math.Sin((2.0 * x) * pi))) * 2.0) / 3.0;
            num += (((20.0 * Math.Sin(x * pi)) + (40.0 * Math.Sin((x / 3.0) * pi))) * 2.0) / 3.0;
            return (num + ((((150.0 * Math.Sin((x / 12.0) * pi)) + (300.0 * Math.Sin((x / 30.0) * pi))) * 2.0) / 3.0));
        }

        public static void UpdateGsmCellInfoToLbsbase(string lac, string cellid)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest) WebRequest.Create(WebSite + "CellmapGsmUpdate.aspx?lac=" + lac + "&cell=" + cellid);
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727; InfoPath.1) Web-Sniffer/1.0.24";
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                reader.ReadToEnd();
                reader.Close();
                response.Close();
            }
            catch (Exception)
            {
            }
        }

        public static void write2file(string filename, bool IsAppend, string newcellinfo)
        {
            StreamWriter writer = new StreamWriter(Application.StartupPath + @"\data\" + filename, IsAppend, Encoding.GetEncoding("gb2312"));
            writer.WriteLine(newcellinfo);
            writer.Close();
        }

        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        public static void WriteToIni(string name, string key, string value)
        {
            WritePrivateProfileString(name, key, value, Application.StartupPath + @"\" + inifile);
        }
    }
}

