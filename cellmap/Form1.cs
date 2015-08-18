namespace cellmap
{
    using GMap.CustomMarkers;
    using GMap.NET;
    using GMap.NET.MapProviders;
    using GMap.NET.WindowsForms;
    using GMap.NET.WindowsForms.Markers;
    using mCellmapManager;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.OleDb;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;
    using System.Threading;
 

    public class Form1 : Form
    {
        private ToolStripMenuItem aaaToolStripMenuItem;
        private BackgroundWorker backgroundWorker1;
        private BackgroundWorker backgroundWorker10;
        private BackgroundWorker backgroundWorker11;
        private BackgroundWorker backgroundWorker12;
        private BackgroundWorker backgroundWorker13;
        private BackgroundWorker backgroundWorker14;
        private BackgroundWorker backgroundWorker2;
        private BackgroundWorker backgroundWorker3;
        private BackgroundWorker backgroundWorker4;
        private BackgroundWorker backgroundWorker5;
        private BackgroundWorker backgroundWorker6;
        private BackgroundWorker backgroundWorker7;
        private BackgroundWorker backgroundWorker8;
        private BackgroundWorker backgroundWorker9;
        private Button button1;
        private Button button10;
        private Button button17;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button9;
        private string CdmaCitySidS;
        private string CdmaDataSourceFile = "CdmaCellDataSource.txt";
        private PointLatLng cellpoint;
        private string centerlat = "39.9087";
        private string centerlng = "116.397494";
        private IContainer components;
        public GMapMarker currentMarker;
        private AutoCompleteStringCollection data = new AutoCompleteStringCollection();
        private AutoCompleteStringCollection data_cell = new AutoCompleteStringCollection();
        private PointLatLng end = new PointLatLng(26.524565, 113.378624);
        private string error = "";
        private Form3 f3;
        private Form4 f4;
        private Form5 f5;
        private string GpsDataSourceFile = "offset.mdb";
        private ToolStripMenuItem gPS批量查询Excel文件ToolStripMenuItem;
        private ToolStripMenuItem gps批量查询ToolStripMenuItem;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox5;
        private string GsmDataSourceFile = "GsmCellDataSource.txt";
        private ToolStripMenuItem kkkToolStripMenuItem;
        private Label label1;
        private Label label11;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private int lac_num;
        public string login_button = "false";
        private string LoginNotice = "请登录后继续使用！";
        public string loginstatus = "true";
        public GMapControl MainMap;
        private MenuStrip menuStrip1;
        private string mnc = "0";
        private int nc_num;
        private int num_c;
        internal GMapOverlay objects;
        private GMapControl ololol2;
        private GMapPolygon polygon;
        public List<PointLatLng> polygonPoints = new List<PointLatLng>();
        internal GMapOverlay polygons;
        private int progress_num;
        private int progress_total;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        internal GMapOverlay routes;
        private int single_cell_search_nums;
        private string singlecell_cell;
        private string singlecell_city;
        private string singlecell_search_result;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer4;
        private PointLatLng start = new PointLatLng(39.9087, 116.397494);
        private StatusStrip statusStrip1;
        private TextBox textBox1;
        private TextBox textBox10;
        private TextBox textBox11;
        private TextBox textBox2;
        public TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;
        private double time1;
        private System.Windows.Forms.Timer timer1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        public GMapOverlay top;
        private ToolStripMenuItem TrackToolStripMenuItem;
        private string version = "1.0.0";
        public WebBrowser webBrowser1;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 打开10进制文件ToolStripMenuItem;
        private ToolStripMenuItem 登陆ToolStripMenuItem;
        private ToolStripMenuItem 地图放大ToolStripMenuItem;
        private ToolStripMenuItem 地图缩小ToolStripMenuItem;
        private ToolStripMenuItem 电子地图ToolStripMenuItem;
        private ToolStripMenuItem 谷歌电子地图ToolStripMenuItem;
        private ToolStripMenuItem 官方网站ToolStripMenuItem;
        private ToolStripMenuItem 检查更新ToolStripMenuItem;
        private ToolStripMenuItem 进制文件ToolStripMenuItem;
        private ToolStripMenuItem 批量查询ToolStripMenuItem;
        private ToolStripMenuItem 清除本地数据库ToolStripMenuItem;
        private ToolStripMenuItem 删除标记ToolStripMenuItem;
        private ToolStripMenuItem 删除所有标记ToolStripMenuItem;
        private ToolStripMenuItem 设置ToolStripMenuItem;
        private ToolStripMenuItem 图标样式ToolStripMenuItem;
        private ToolStripMenuItem 图形ToolStripMenuItem;
        private ToolStripMenuItem 卫星地图ToolStripMenuItem;
        private ToolStripMenuItem 文字ToolStripMenuItem;
        private ToolStripMenuItem 显示附近基站ToolStripMenuItem;
        private ToolStripMenuItem 显示联通基站ToolStripMenuItem;
        private ToolStripMenuItem 显示所有标记ToolStripMenuItem1;
        private ToolStripMenuItem 显示所有基站ToolStripMenuItem;
        private ToolStripMenuItem 显示移动基站ToolStripMenuItem;
        private ToolStripMenuItem 修改密码ToolStripMenuItem;
        private ToolStripMenuItem 用户登陆ToolStripMenuItem;
        private TextBox textBox5;
        private Label label2;
        private ToolStripMenuItem 账号查询ToolStripMenuItem;

        public Form1()
        {
            this.InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            base.IsMdiContainer = true;
            base.Show();
            this.Text = "基站查询工具 V" + this.version;
            this.init();
            if (CellmapManager.GetStringIni("1", "MarkerType") == "2")
            {
                this.文字ToolStripMenuItem.Checked = true;
            }
            else
            {
                this.图形ToolStripMenuItem.Checked = true;
                CellmapManager.WriteToIni("1", "MarkerType", "1");
            }
            //if (CellmapManager.GetStringIni("1", "AutoLogin") == "true")
            //{
            //    new Form2(this).ShowDialog();
            //}
        }

        private void aaaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.loginstatus == "true")
            //{
                this.textBox3.BringToFront();
                this.MainMap.BringToFront();
                this.f3 = new Form3(this);
                this.f3.Owner = this;
                this.f3.Show();
                this.f3.BringToFront();
            //}
            //else
            //{
            //    MessageBox.Show("请登录后继续使用！", "提示");
            //}
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\cellinput.txt", Encoding.Default);
                string str = "";
                this.polygonPoints = new List<PointLatLng>();
                int num = 0;
                while (str != null)
                {
                    string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                    string lac = strArray[0];
                    string cellid = strArray[1];
                    num++;
                    string gsmCellInfo = CellmapManager.GetGsmCellInfo(lac, cellid);
                    if (gsmCellInfo == "null")
                    {
                        gsmCellInfo = lac + "," + cellid + ",null,";
                    }
                    CellmapManager.write2file("celloutput.txt", true, gsmCellInfo);
                    int length = strArray.Length;
                    string str5 = "";
                    if (length >= 3)
                    {
                        str5 = strArray[2];
                    }
                    if (length >= 4)
                    {
                        str5 = str5 + "---" + strArray[3];
                    }
                    this.backgroundWorker1.ReportProgress((num * 100) / 10, gsmCellInfo + str5);
                }
                reader.Close();
            }
            catch
            {
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBox3.Text = this.textBox3.Text + e.UserState.ToString() + "\r\n";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                string text1 = strArray[6];
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "基站：" + strArray[0] + " - " + strArray[1] + "\r\n经纬度：" + strArray[2] + "," + strArray[3] + "\r\n火星坐标：" + strArray[4] + "," + strArray[5] + "\r\n地址：" + strArray[6] + "\r\n时间：" + strArray[7];
                this.currentMarker.ToolTipText = str2;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.MainMap.Position = this.cellpoint;
                this.polygonPoints.Add(this.cellpoint);
                this.ZoomToFitMarkers();
            }
            catch
            {
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GMapRoute item = new GMapRoute(this.polygonPoints, "") {
                Stroke = new Pen(Color.FromArgb(0x90, Color.Red))
            };
            item.Stroke.Width = 5f;
            this.routes.Routes.Add(item);
            this.textBox3.Text = this.textBox3.Text + "\r\n查询完成！";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
        }

        private void backgroundWorker10_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string centerlat = this.centerlat;
                string centerlng = this.centerlng;
                string str3 = "null";
                if (str3 == "null")
                {
                    str3 = CellmapManager.GetGps2AddressFromAMap(this.centerlat, this.centerlng, "autonavi");
                }
                e.Result = centerlat + "," + centerlng + "," + str3;
            }
            catch
            {
            }
        }

        private void backgroundWorker10_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Result.ToString();
            if (str != "null")
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.textBox3.Text = "火星坐标经纬度：" + strArray[0] + "，" + strArray[1] + "\r\n地址：" + strArray[2] + "\r\n";
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[0]), Convert.ToDouble(strArray[1]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "火星坐标经纬度：" + strArray[0] + "，" + strArray[1] + "\r\n地址：" + strArray[2] + "\r\n";
                this.currentMarker.ToolTipText = str2;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.MainMap.Position = this.cellpoint;
                this.polygonPoints.Add(this.cellpoint);
            }
            else
            {
                this.textBox3.Text = "查询失败！";
            }
            this.button17.Enabled = true;
        }

        private void backgroundWorker11_DoWork(object sender, DoWorkEventArgs e)
        {
            this.num_c = 0;
            string bid = null;
            if (this.radioButton4.Checked)
            {
                bid = this.textBox11.Text;
            }
            if (this.radioButton3.Checked)
            {
                bid = (Convert.ToInt32(this.textBox11.Text, 0x10)).ToString();
            }
            this.CdmaCitySidS = CellmapManager.GetCdmaCitySidNid(this.textBox10.Text);
            string[] strArray = this.CdmaCitySidS.Split(new char[] { ',' });
            int num = Convert.ToInt16(strArray[1]);
            for (int i = 2; i <= (num + 1); i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '-' });
                string sid = strArray2[0];
                string nid = strArray2[1];
                string userState = CellmapManager.GetCdmaCellInfo(sid, nid, bid);
                this.backgroundWorker11.ReportProgress(1, userState);
            }
        }

        private void backgroundWorker11_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            string str = e.UserState.ToString();
            if (str != "null")
            {
                try
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    string str2 = strArray[0];
                    string str3 = strArray[1];
                    string str4 = strArray[2];
                    string str5 = strArray[3];
                    string str6 = strArray[4];
                    string str7 = strArray[5];
                    string str8 = strArray[6];
                    string str9 = strArray[7];
                    this.textBox3.Text = this.textBox3.Text + "\r\nsid:" + str2 + ", nid:" + str3 + ", bid:" + str4 + "\r\n经纬度：" + str5 + "," + str6 + "\r\n地址：" + str9 + "\r\n";
                    this.cellpoint = new PointLatLng(Convert.ToDouble(str7), Convert.ToDouble(str8));
                    this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                    this.objects.Markers.Add(this.currentMarker);
                    string str10 = "sid:" + str2 + ",nid:" + str3 + ",bid:" + str4 + "\r\n经纬度：" + str5 + ", " + str6 + "\r\n" + str9 + "\r\n";
                    this.currentMarker.ToolTipText = str10;
                    this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    this.MainMap.Position = this.cellpoint;
                    this.num_c++;
                }
                catch
                {
                }
            }
        }

        private void backgroundWorker11_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.textBox3.Text = string.Concat(new object[] { this.textBox3.Text, "\r\n查询结束！查询到", this.num_c, "个基站。" });
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            this.Cursor = Cursors.Arrow;
            this.button3.Enabled = true;
        }

        private void backgroundWorker12_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = CellmapManager.GetUpdateInfo();
        }

        private void backgroundWorker12_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string strA = e.Result.ToString();
            if ((string.Compare(strA, this.version) > 0) && (strA != "null"))
            {
                MessageBox.Show("发现新版本" + strA + "，请到主页 www.cellmap.cn 下载！", "提示");
            }
            else
            {
                this.textBox3.Text = "当前为最新版本。";
            }
        }

        private void backgroundWorker13_DoWork(object sender, DoWorkEventArgs e)
        {
            this.nc_num = 0;
            this.error = "";
            string lac = "";
            if (this.radioButton1.Checked)
            {
                lac = this.textBox8.Text;
            }
            if (this.radioButton2.Checked)
            {
                lac = (Convert.ToInt32(this.textBox8.Text, 0x10)).ToString();
            }
            string[] strArray = CellmapManager.GetGsmLac(lac).Split(new char[] { ';' });
            int length = strArray.Length;
            for (int i = 1; i <= (length - 1); i++)
            {
                this.backgroundWorker13.ReportProgress(1, strArray[i]);
            }
        }

        private void backgroundWorker13_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "";
                if (Convert.ToInt32(strArray[1]) > 0xffff)
                {
                    str2 = " (" + (Convert.ToInt32(strArray[1]) % 0x10000) + ")";
                }
                string str3 = "LAC:" + strArray[0] + "\r\nCELL:" + strArray[1] + str2 + "\r\nfix_lat:" + strArray[4] + "\r\nfix_lng:" + strArray[5] + "\r\n地址：" + strArray[6];
                this.currentMarker.ToolTipText = str3;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.polygonPoints.Add(this.cellpoint);
                this.nc_num++;
                this.textBox3.SelectionStart = this.textBox3.Text.Length;
                this.textBox3.SelectionLength = 0;
                this.textBox3.ScrollToCaret();
            }
            catch
            {
            }
        }

        private void backgroundWorker13_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ZoomToFitMarkers();
            this.timer1.Stop();
            this.textBox3.Text = this.textBox3.Text + "查询结束！\r\n";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            this.Cursor = Cursors.Arrow;
        }

        private void backgroundWorker14_DoWork(object sender, DoWorkEventArgs e)
        {
            string lac = "";
            string cell = "";
            if (this.radioButton1.Checked)
            {
                lac = this.textBox8.Text;
                cell = this.textBox9.Text;
            }
            if (this.radioButton2.Checked)
            {
                lac = (Convert.ToInt32(this.textBox8.Text, 0x10)).ToString();
                cell = (Convert.ToInt32(this.textBox9.Text, 0x10)).ToString();
            }
            this.nc_num = 0;
            this.error = "";
            string gsmCellFuzzyResearch = CellmapManager.GetGsmCellFuzzyResearch(lac, cell);
            if (gsmCellFuzzyResearch != "error")
            {
                string[] strArray = gsmCellFuzzyResearch.Split(new char[] { ';' });
                int length = strArray.Length;
                for (int i = 1; i <= (length - 1); i++)
                {
                    this.backgroundWorker14.ReportProgress(1, strArray[i]);
                }
            }
            e.Result = gsmCellFuzzyResearch;
        }

        private void backgroundWorker14_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                string str2 = strArray[0];
                string str3 = strArray[1];
                if (Convert.ToInt32(str3) > 0xffff)
                {
                    str3 = (Convert.ToInt32(str3) % 0x10000).ToString();
                }
                Font font = new Font("宋体", 13f, FontStyle.Bold);
                Bitmap bitmap = CellmapManager.TextToBitmap(str2 + "-" + str3, font, Rectangle.Empty, Color.Blue, Color.Red);
                this.currentMarker = new GMarkerGoogle(this.cellpoint, bitmap);
                this.objects.Markers.Add(this.currentMarker);
                string str4 = "LAC：" + str2 + " , CELL：" + str3 + "\r\n纠偏后经纬度：" + strArray[4] + "," + strArray[5] + "\r\n地址：" + strArray[6];
                this.currentMarker.ToolTipText = str4;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.polygonPoints.Add(this.cellpoint);
                this.ZoomToFitMarkers();
                this.nc_num++;
                this.textBox3.SelectionStart = this.textBox3.Text.Length;
                this.textBox3.SelectionLength = 0;
                this.textBox3.ScrollToCaret();
            }
            catch
            {
            }
        }

        private void backgroundWorker14_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Result.ToString();
            this.timer1.Stop();
            this.textBox3.Text = string.Concat(new object[] { this.textBox3.Text, "查询结束！\r\n搜索到", this.nc_num, "个基站\r\n查询结果：", str, "\r\n" });
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            this.ZoomToFitMarkers();
            this.Cursor = Cursors.Arrow;
            this.显示附近基站ToolStripMenuItem.Enabled = true;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            this.singlecell_search_result = "";
            this.lac_num = 0;
            if (this.radioButton4.Checked)
            {
                this.singlecell_cell = this.textBox11.Text;
            }
            if (this.radioButton3.Checked)
            {
                this.singlecell_cell = (Convert.ToInt32(this.textBox11.Text, 0x10)).ToString();
            }
            this.singlecell_city = this.textBox10.Text;
            try
            {
                string[] strArray = CellmapManager.GetGsmCityLac(this.textBox10.Text).Split(new char[] { ',' });
                int num = Convert.ToInt16(strArray[1]);
                this.progress_total = num;
                for (int i = 2; i <= (num + 1); i++)
                {
                    string lac = strArray[i];
                    string gsmCellInfo = CellmapManager.GetGsmCellInfo(lac, this.singlecell_cell);
                    this.progress_num = i - 1;
                    this.backgroundWorker2.ReportProgress(1, gsmCellInfo);
                }
            }
            catch
            {
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBox3.Text = string.Concat(new object[] { this.textBox3.Text, this.progress_total, ",", this.progress_num, ",", e.UserState.ToString(), "\r\n" });
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            string str = e.UserState.ToString();
            if (str != "null")
            {
                try
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    string str2 = strArray[6];
                    if (str2.Contains(this.textBox10.Text))
                    {
                        this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                        this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                        this.objects.Markers.Add(this.currentMarker);
                        string str3 = "基站：" + strArray[0] + " - " + strArray[1] + "\r\n经纬度：" + strArray[2] + "," + strArray[3] + "\r\n纠偏坐标：" + strArray[4] + "," + strArray[5] + "\r\n地址：" + strArray[6] + "\r\n";
                        this.singlecell_search_result = this.singlecell_search_result + e.UserState.ToString() + "\r\n";
                        this.currentMarker.ToolTipText = str3;
                        this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        this.MainMap.Position = this.cellpoint;
                        this.polygonPoints.Add(this.cellpoint);
                        this.ZoomToFitMarkers();
                    }
                }
                catch
                {
                }
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.timer1.Stop();
            this.textBox3.Text = this.singlecell_search_result + "\r\n单基站查询完成。";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            this.button1.Enabled = true;
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\data\cellinput16.txt", Encoding.Default);
                string str = "";
                this.polygonPoints = new List<PointLatLng>();
                int num = 0;
                while (str != null)
                {
                    string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                    string lac = (Convert.ToInt32(strArray[0], 0x10).ToString());
                    string cellid = (Convert.ToInt32(strArray[1], 0x10)).ToString();
                    num++;
                    string gsmCellInfo = CellmapManager.GetGsmCellInfo(lac, cellid);
                    if (gsmCellInfo != "null")
                    {
                        string[] strArray2 = gsmCellInfo.Split(new char[] { ',' });
                        gsmCellInfo = strArray[0] + "," + strArray[1] + "," + strArray2[2] + "," + strArray2[3] + "," + strArray2[4] + "," + strArray2[5] + "," + strArray2[6] + ",";
                    }
                    else
                    {
                        gsmCellInfo = strArray[0] + "," + strArray[1] + ",null,";
                    }
                    CellmapManager.write2file("celloutput.txt", true, gsmCellInfo);
                    int length = strArray.Length;
                    string str5 = "";
                    if (length >= 3)
                    {
                        str5 = strArray[2];
                    }
                    if (length >= 4)
                    {
                        str5 = str5 + "---" + strArray[3];
                    }
                    this.backgroundWorker3.ReportProgress((num * 100) / 10, gsmCellInfo + str5);
                }
                reader.Close();
            }
            catch
            {
            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBox3.Text = this.textBox3.Text + e.UserState.ToString() + "\r\n";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                string text1 = strArray[6];
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "基站：" + strArray[0] + " - " + strArray[1] + "\r\n经纬度：" + strArray[2] + "," + strArray[3] + "\r\n火星坐标：" + strArray[4] + "," + strArray[5] + "\r\n地址：" + strArray[6] + "\r\n时间：" + strArray[7];
                this.currentMarker.ToolTipText = str2;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.MainMap.Position = this.cellpoint;
                this.polygonPoints.Add(this.cellpoint);
                this.ZoomToFitMarkers();
            }
            catch
            {
            }
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GMapRoute item = new GMapRoute(this.polygonPoints, "") {
                Stroke = new Pen(Color.FromArgb(0x90, Color.Red))
            };
            item.Stroke.Width = 5f;
            this.routes.Routes.Add(item);
            this.textBox3.Text = this.textBox3.Text + "\r\n查询结束！\r\n";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\data\gpsinput.txt", Encoding.Default);
                string str = "";
                this.polygonPoints = new List<PointLatLng>();
                int num = 0;
                while (str != null)
                {
                    string[] strArray = reader.ReadLine().Split(new char[] { ',' });
                    string str2 = strArray[0];
                    string str3 = strArray[1];
                    string[] strArray2 = CellmapManager.FixGpsApi(Convert.ToDouble(str2), Convert.ToDouble(str3)).Split(new char[] { ',' });
                    string str5 = str2 + "," + str3 + "," + strArray2[1] + "," + strArray2[0];
                    num++;
                    int length = strArray.Length;
                    string str6 = "";
                    if (length >= 3)
                    {
                        str6 = strArray[2];
                    }
                    if (length >= 4)
                    {
                        str6 = str6 + "---" + strArray[3];
                    }
                    this.backgroundWorker5.ReportProgress((num * 100) / 10, str5 + "," + str6);
                }
                reader.Close();
            }
            catch
            {
            }
        }

        private void backgroundWorker5_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBox3.Text = this.textBox3.Text + e.UserState.ToString() + "\r\n";
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[2]), Convert.ToDouble(strArray[3]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "原始经纬度：" + strArray[0] + " , " + strArray[1] + "\r\n纠偏后经纬度：" + strArray[2] + "," + strArray[3] + "\r\n时间：" + strArray[4];
                this.currentMarker.ToolTipText = str2;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.MainMap.Position = this.cellpoint;
                this.polygonPoints.Add(this.cellpoint);
                this.ZoomToFitMarkers();
            }
            catch
            {
            }
        }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GMapRoute item = new GMapRoute(this.polygonPoints, "") {
                Stroke = new Pen(Color.FromArgb(0x90, Color.Red))
            };
            item.Stroke.Width = 5f;
            this.routes.Routes.Add(item);
            MessageBox.Show("查询结束！", "gps批量查询");
        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            this.nc_num = 0;
            this.error = "";
            string str = CellmapManager.GetNeighCellInfo(this.centerlat, this.centerlng, this.mnc);
            if (str != "error")
            {
                string[] strArray = str.Split(new char[] { ';' });
                int length = strArray.Length;
                for (int i = 1; i <= (length - 1); i++)
                {
                    this.backgroundWorker6.ReportProgress(1, strArray[i]);
                }
            }
            e.Result = str;
        }

        private void backgroundWorker6_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "";
                if (Convert.ToInt32(strArray[1]) > 0xffff)
                {
                    str2 = " (" + (Convert.ToInt32(strArray[1]) % 0x10000) + ")";
                }
                string str3 = "LAC:" + strArray[0] + "\r\nCELL:" + strArray[1] + str2 + "\r\n纠偏后经纬度：" + strArray[4] + "," + strArray[5] + "\r\n地址：" + strArray[6];
                this.currentMarker.ToolTipText = str3;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.polygonPoints.Add(this.cellpoint);
                this.ZoomToFitMarkers();
                this.nc_num++;
                this.textBox3.SelectionStart = this.textBox3.Text.Length;
                this.textBox3.SelectionLength = 0;
                this.textBox3.ScrollToCaret();
            }
            catch
            {
            }
        }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Result.ToString();
            this.timer1.Stop();
            this.textBox3.Text = string.Concat(new object[] { this.textBox3.Text, "查询结束！\r\n搜索到", this.nc_num, "个基站\r\n查询结果：", str, "\r\n" });
            this.textBox3.SelectionStart = this.textBox3.Text.Length;
            this.textBox3.SelectionLength = 0;
            this.textBox3.ScrollToCaret();
            this.Cursor = Cursors.Arrow;
            this.显示附近基站ToolStripMenuItem.Enabled = true;
        }

        private void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            string text = this.textBox6.Text;
            string nid = this.textBox4.Text;
            string sid = this.textBox7.Text;
            string newcellinfo = CellmapManager.GetCdmaCellInfoFromLocalDataSource(sid, nid, text);
            if (newcellinfo == "null")
            {
                newcellinfo = CellmapManager.GetCdmaCellInfoFromWebSite(sid, nid, text);
                if (newcellinfo != "null")
                {
                    newcellinfo = sid + "," + nid + "," + text + "," + newcellinfo;
                    CellmapManager.write2file(this.CdmaDataSourceFile, true, newcellinfo);
                }
            }
            e.Result = newcellinfo;
        }

        private void backgroundWorker7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Result.ToString();
            if (str != "null")
            {
                try
                {
                    string[] strArray = str.Split(new char[] { ',' });
                    string str2 = strArray[0];
                    string str3 = strArray[1];
                    string str4 = strArray[2];
                    string str5 = strArray[3];
                    string str6 = strArray[4];
                    string str7 = strArray[5];
                    string str8 = strArray[6];
                    string str9 = strArray[7];
                    this.textBox3.Text = "sid:" + str2 + ", nid:" + str3 + ", bid:" + str4 + "\r\n经纬度：" + str5 + "," + str6 + "\r\n地址：" + str9;
                    this.cellpoint = new PointLatLng(Convert.ToDouble(str7), Convert.ToDouble(str8));
                    this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                    this.objects.Markers.Add(this.currentMarker);
                    string str10 = "sid:" + str2 + ",nid:" + str3 + ",bid:" + str4 + "\r\n经纬度：" + str5 + ", " + str6 + "\r\n" + str9 + "\r\n";
                    this.currentMarker.ToolTipText = str10;
                    this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    this.MainMap.Position = this.cellpoint;
                }
                catch
                {
                    this.textBox3.Text = "网络故障，请稍后再试！\r\n" + str;
                }
            }
            else
            {
                string text = this.textBox6.Text;
                string str12 = this.textBox4.Text;
                string str13 = this.textBox7.Text;
                this.textBox3.Text = "结果：查询不到该数据 " + str13 + "-" + str12 + "-" + text + "\r\n原因：数据未收录，或输入有误！";
            }
            this.button10.Enabled = true;
        }

        private void backgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            string lac = "";
            string cellid = "";
            if (this.radioButton1.Checked)
            {
                lac = this.textBox8.Text;
                cellid = this.textBox9.Text;
            }
            if (this.radioButton2.Checked)
            {
                lac = (Convert.ToInt32(this.textBox8.Text, 0x10)).ToString();
                cellid = (Convert.ToInt32(this.textBox9.Text, 0x10)).ToString();
            }
            string gsmCellInfoCloud = CellmapManager.GetGsmCellInfoCloud(lac, cellid);
            e.Result = gsmCellInfoCloud;
        }

        private void backgroundWorker8_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //GPSSPG: "9766,3682,22.7351,  113.802,  22.7322201319046,113.807093175813,广东省深圳市宝安区沙井街道新沙路38号,758,"
            //CELLMAP:"9766,3682,22.606658,113.851435,22.6036773226,  113.856412185454,中国广东省深圳市宝安区恒南一路 邮政编码: 518126,758"
            string str = e.Result.ToString();
            if (str != "null") 
            {
                try
                {
                    string text = this.textBox8.Text;
                    string text2 = this.textBox9.Text;
                    string[] strArray = str.Split(new char[] { ',' });
                    try
                    {
                        this.currentMarker.ToolTip.Fill = new SolidBrush(Color.WhiteSmoke);
                    }
                    catch
                    {
                    }
                    string str2 = "";
                    string str3 = "";
                    string str4 = "";
                    string str5 = "";
                    if (this.radioButton1.Checked)
                    {
                        str2 = this.textBox8.Text;
                        str3 = this.textBox9.Text;
                        str4 = Convert.ToInt32(str2).ToString("x");
                        str5 = Convert.ToInt32(str3).ToString("x");
                    }
                    if (this.radioButton2.Checked)
                    {
                        str2 = (Convert.ToInt32(this.textBox8.Text, 0x10)).ToString();
                        str3 = (Convert.ToInt32(this.textBox9.Text, 0x10)).ToString();
                        str4 = this.textBox8.Text;
                        str5 = this.textBox9.Text;
                    }
                    string str6 = "";
                    if (Convert.ToInt32(str3) > 0xffff)
                    {
                        str6 = "(" + (Convert.ToInt32(str3) % 0x10000) + ")";
                    }
                    this.textBox3.Text = "基站信息：\r\n10进制基站：" + str2 + " - " + str3 + str6 + "\r\n16进制基站：" + str4 + " - " + str5 + "\r\n经纬度：" + strArray[2] + "," + strArray[3] + "\r\n纠偏后经纬度：" + strArray[4] + "," + strArray[5] + "\r\n地址：" + strArray[6] + "\r\n范围：" + strArray[7];
                    this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[4]), Convert.ToDouble(strArray[5]));
                    if (CellmapManager.GetStringIni("1", "MarkerType") == "1")
                    {
                        this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                    }
                    else
                    {
                        Font font = new Font("宋体", 13f, FontStyle.Bold);
                        Bitmap bitmap = CellmapManager.TextToBitmap(this.textBox8.Text + "-" + this.textBox9.Text, font, Rectangle.Empty, Color.Blue, Color.Red);
                        this.currentMarker = new GMarkerGoogle(this.cellpoint, bitmap);
                    }
                    GMapMarker item = new GMapMarkerCircle(this.cellpoint, Convert.ToInt32(Convert.ToDecimal(strArray[7])) / 2);
                    this.objects.Markers.Add(this.currentMarker);
                    string str7 = "10进制：" + str2 + " - " + str3 + str6 + "\r\n16进制：" + str4 + " - " + str5 + "\r\nGPS：" + strArray[2] + "," + strArray[3] + "\r\n" + strArray[6];
                    this.currentMarker.ToolTipText = str7;
                    this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                    this.currentMarker.ToolTip.Fill = new SolidBrush(Color.Red);
                    this.MainMap.Position = this.cellpoint;
                    this.polygons.Markers.Add(item);
                    this.polygonPoints.Add(this.cellpoint);
                }
                catch (Exception)
                {
                    this.textBox3.Text = "查询失败，可能该数据未收录，或输入有误！\r\n";
                }
            }
            else
            {
                this.textBox3.Text = "结果：查询不到该数据 " + this.textBox8.Text + "-" + this.textBox9.Text + "\r\n原因：该数据未收录，或输入有误！\r\n";
            }
            this.button2.Enabled = true;
        }

        private void backgroundWorker9_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string text = this.textBox1.Text;
                string str2 = this.textBox2.Text;
                string[] strArray = CellmapManager.FixGpsApi(Convert.ToDouble(text), Convert.ToDouble(str2)).Split(new char[] { ',' });
                string str4 = "null";
                if (str4 == "null")
                {
                    str4 = CellmapManager.GetGps2AddressFromAMap(text, str2, "gps");
                }
                e.Result = strArray[0] + "," + strArray[1] + "," + str4;
            }
            catch
            {
                e.Result = "null";
            }
        }

        private void backgroundWorker9_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Result.ToString();
            if (str != "null")
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.textBox3.Text = "经纬度：" + this.textBox2.Text + "，" + this.textBox1.Text + "\r\n纠偏后经纬度：" + strArray[0] + "，" + strArray[1] + "\r\n地址：" + strArray[2] + "\r\n";
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[1]), Convert.ToDouble(strArray[0]));
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.objects.Markers.Add(this.currentMarker);
                string str2 = "经纬度：" + this.textBox2.Text + "，" + this.textBox1.Text + "\r\n纠偏后经纬度：" + strArray[0] + "，" + strArray[1] + "\r\n地址：" + strArray[2] + "\r\n";
                this.currentMarker.ToolTipText = str2;
                this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.MainMap.Position = this.cellpoint;
                this.polygonPoints.Add(this.cellpoint);
            }
            else
            {
                this.textBox3.Text = "查询失败！";
            }
            this.button9.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.objects.Markers.Remove(this.currentMarker);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.textBox3.BringToFront();
            this.MainMap.BringToFront();
            if (this.loginstatus == "true")
            {
                string text = "在没有LAC的情况下，将查询该城市包含该cell的所有基站。请耐心等候！";
                if (((MessageBox.Show(text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) && (this.textBox10.Text != "")) && (this.textBox11.Text != ""))
                {
                    this.backgroundWorker2.RunWorkerAsync();
                    this.timer1.Interval = 100;
                    this.time1 = 0.0;
                    this.button1.Enabled = false;
                    this.textBox3.Text = "Gsm 单基站查询开始，请稍后......\r\n城市：" + this.textBox10.Text + ", 基站：" + this.textBox11.Text + "\r\n";
                    this.CleanMap();
                }
            }
            else
            {
                MessageBox.Show(this.LoginNotice, "提示");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.textBox3.BringToFront();
            this.MainMap.BringToFront();
            this.button10.Enabled = false;
            this.textBox3.Text = "cdma基站查询中......";
            this.backgroundWorker7.RunWorkerAsync();
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            this.MainMap.BringToFront();
            this.textBox3.BringToFront();
            this.textBox3.Text = "定位中，请稍后......";
            this.button17.Enabled = false;
            this.backgroundWorker10.RunWorkerAsync();
        }


          void PointOnTheMap(LBS2GPS.CellServiceEntity result)
        {
            if (Convert.ToDouble(result.QQlat) == 0.00)
                return;

            this.cellpoint = new PointLatLng(Convert.ToDouble(result.QQlat), Convert.ToDouble(result.QQlng));
            //if (CellmapManager.GetStringIni("1", "MarkerType") != "1")
            //{
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
            //}
            //else
            //{
            //    Font font = new Font("宋体", 13f, FontStyle.Bold);
            //    Bitmap bitmap = CellmapManager.TextToBitmap(result.lac + "-" + result.cell, font, Rectangle.Empty, Color.Blue, Color.Red);
            //    this.currentMarker = new GMarkerGoogle(this.cellpoint, bitmap);
            //}
            this.objects.Markers.Add(this.currentMarker);

            this.currentMarker.ToolTipText = result.whichApi;
            this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            this.currentMarker.ToolTip.Fill = new SolidBrush(Color.Red);
            //设置地图中心为cellpoint
           
            this.MainMap.Position = this.cellpoint;
            //item为一个圆圈，用来表示基站范围
            //if (0 != result.distance )
            //{
            //    GMapMarker item = new GMapMarkerCircle(this.cellpoint, Convert.ToInt32(Convert.ToDecimal(result.distance)) / 2);
            //    this.polygons.Markers.Add(item);
            //}
            
            this.polygonPoints.Add(this.cellpoint);

        }
          private delegate void SetTextHandler(string text);
          private void SetText(string text)
          {

              if (textBox3.InvokeRequired == true)
              {
                  SetTextHandler set = new SetTextHandler(SetText);//委托的方法参数应和SetText一致
                  textBox3.Invoke(set, new object[] { text }); //此方法第二参数用于传入方法,代替形参text
              }
              else
              {
                  textBox3.Text += text; 
              }
          }
          void PringResult( LBS2GPS.CellServiceEntity result)
          {
              string sResult = string.Empty;
              if( result.address  == string.Empty)
              {
                    sResult = "----" + result.whichApi + "----" + "\r\n" + result.lat.ToString() + "," + result.lng.ToString() + "\r\n"  + "无地址信息\r\n";
                  SetText(sResult); 
              }
              else
              {
                  sResult = "----" + result.whichApi + "----" + "\r\n" + result.lat.ToString() + "," + result.lng.ToString() + "\r\n" + result.address + "\r\n";
                  SetText(sResult); 
              }
              
 
          }


          delegate LBS2GPS.CellServiceEntity httpget_lbsMGCaller(string lac, string cellid);//定义个代理

          delegate LBS2GPS.CellServiceEntity httpget_lbsCellIdCaller(string lac, string cellid);//定义个代理

          delegate LBS2GPS.CellServiceEntity httpget_lbsCellMapCaller(string lac, string cellid);//定义个代理

          delegate LBS2GPS.CellServiceEntity httpget_lbsMapbarCaller(string lac, string cellid);//定义个代理


          private void queryall()
          {
              //添加其它几个接口的查询
              string lac = "";
              string cellid = "";
              if (this.radioButton1.Checked)
              {
                  lac = this.textBox8.Text;
                  cellid = this.textBox9.Text;
              }
              if (this.radioButton2.Checked)
              {
                  lac = (Convert.ToInt32(this.textBox8.Text, 0x10)).ToString();
                  cellid = (Convert.ToInt32(this.textBox9.Text, 0x10)).ToString();
              }
              textBox3.Text = ("********************" + lac  + "," + cellid+ "********************\r\n");
              /********************************Mapbar查询**************************************/
              httpget_lbsMapbarCaller MapbarCaller = new httpget_lbsMapbarCaller(LBS2GPS.httpget_lbsMapbar);
              IAsyncResult resultMapbar = MapbarCaller.BeginInvoke(lac, cellid, null, null);
              LBS2GPS.CellServiceEntity sResultMapbar = MapbarCaller.EndInvoke(resultMapbar);//用于接收返回值 


              if (sResultMapbar != null)
              {
                  //textBox3.Text = string.Empty;
                  PringResult(sResultMapbar);
                  PointOnTheMap(sResultMapbar);
              }


              /********************************MG查询**************************************/
              httpget_lbsMGCaller MapgooCaller = new httpget_lbsMGCaller(LBS2GPS.httpget_lbsMG);

              IAsyncResult resultMG = MapgooCaller.BeginInvoke(lac, cellid, null, null);
              LBS2GPS.CellServiceEntity sResultMg = MapgooCaller.EndInvoke(resultMG);//用于接收返回值 


              if (sResultMg != null)
              {
                  PringResult(sResultMg);
                  PointOnTheMap(sResultMg);
              }

              /********************************CellMap查询**************************************/
              httpget_lbsCellMapCaller CellMapCaller = new httpget_lbsCellMapCaller(LBS2GPS.httpget_lbsCellMap);
              IAsyncResult resultCellMap = CellMapCaller.BeginInvoke(lac, cellid, null, null);
              LBS2GPS.CellServiceEntity sResultCellMap = CellMapCaller.EndInvoke(resultCellMap);//用于接收返回值 


              if (sResultCellMap != null)
              {
                  PringResult(sResultCellMap);
                  PointOnTheMap(sResultCellMap);
              }

              /********************************CellID查询**************************************/
              httpget_lbsCellIdCaller CellIdCaller = new httpget_lbsCellIdCaller(LBS2GPS.httpget_lbsCellId);
              IAsyncResult resultCellId = CellIdCaller.BeginInvoke(lac, cellid, null, null);
              LBS2GPS.CellServiceEntity sResultCellId = CellIdCaller.EndInvoke(resultCellId);//用于接收返回值 


              if (sResultCellId != null)
              {
                  PringResult(sResultCellId);
                  PointOnTheMap(sResultCellId);
              }
          
          }
        private void button2_Click_1(object sender, EventArgs e)
        {
            this.textBox3.BringToFront();
            this.MainMap.BringToFront();
            this.textBox3.Text = "基站查询中，请稍后......";
            //屏蔽掉原来的cellmap方法
            //this.backgroundWorker8.RunWorkerAsync();
            this.button2.Enabled = false;

            //this.textBox3.Text = string.Empty;

            System.Threading.Thread thread = new Thread(new ThreadStart(queryall));
            thread.Start();

            this.button2.Enabled = true;
 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.ZoomToFitMarkers();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.textBox3.BringToFront();
            this.MainMap.BringToFront();
            if (this.loginstatus == "true")
            {
                string text = "在没有Sid、Nid的情况下，将查询该城市所有包含该Bid的CDMA基站。确定开始？";
                if (((MessageBox.Show(text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK) && (this.textBox10.Text != "")) && (this.textBox11.Text != ""))
                {
                    this.backgroundWorker11.RunWorkerAsync();
                    this.button3.Enabled = false;
                    this.textBox3.Text = "根据城市模糊查询CDMA基站，请稍后......\r\n" + this.textBox10.Text + " Sid-Nid ：\r\n" + CellmapManager.GetCdmaCitySidNid(this.textBox10.Text) + "\r\n";
                    this.CleanMap();
                }
            }
            else
            {
                MessageBox.Show(this.LoginNotice, "提示");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.objects.Markers.Clear();
            this.top.Markers.Clear();
            this.routes.Routes.Clear();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.MainMap.BringToFront();
            if (this.loginstatus == "true")
            {
                if (MessageBox.Show("显示Lac：" + this.textBox8.Text + " 大区的覆盖范围！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    this.CleanMap();
                    this.textBox3.Text = "开始搜索，请耐心等候......\r\n";
                    this.backgroundWorker13.RunWorkerAsync();
                    this.timer1.Interval = 100;
                    this.time1 = 0.0;
                    this.timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("登陆后，可以显示指定Lac的所有基站！", "提示");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.MainMap.Zoom--;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.MainMap.BringToFront();
            if (this.loginstatus == "true")
            {
                if (MessageBox.Show("如查询不到指定基站的位置信息，本功能将按照基站编号顺序显示指定基站前后2个基站，从而帮助确定指定基站的大概位置。\r\n\r\n确定搜索基站 " + this.textBox8.Text + "- " + this.textBox9.Text + " 前后基站？", "说明", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    this.CleanMap();
                    this.textBox3.Text = "开始搜索，请耐心等候......\r\n";
                    this.backgroundWorker14.RunWorkerAsync();
                    this.timer1.Interval = 100;
                    this.time1 = 0.0;
                    this.timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("登陆后，可以显示指定Lac的所有基站！", "提示");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.MainMap.Zoom++;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            string sLacCell = textBox5.Text;
            long lLac = 0;
            long lCell = 0;
            //string sLachex = string.Empty;
            //string sCellhex = string.Empty;
            sLacCell = sLacCell.Trim();
            if ((sLacCell.Length != 8))
            {
                textBox3.Text = ("请输入正确的LBS信息\n");
                return;
            }
            else if (sLacCell.Length == 8)
            {
                //sLachex = sLacCell.Substring(0, 4);
                //sCellhex = sLacCell.Substring(4, 4);
                lLac = Convert.ToInt64(sLacCell.Substring(0, 4), 16);
                lCell = Convert.ToInt64(sLacCell.Substring(4, 4), 16);

                textBox8.Text = lLac.ToString();
                textBox9.Text = lCell.ToString();
            }
            button2_Click_1(  sender,   e);

            /*
            string lac = "";
            string cellid = "";
            lac = this.textBox8.Text;
            cellid = this.textBox9.Text;
            string gsmCellInfoFromCellmapApi = CellmapManager.GetGsmCellInfoFromCellmapApi(lac, cellid);
            //22.606658,113.851435,22.6036773226,113.856412185454,中国广东省深圳市宝安区恒南一路 邮政编码: 518126,758
            this.textBox3.Text = gsmCellInfoFromCellmapApi;

            this.cellpoint = new PointLatLng(Convert.ToDouble("22.6036773226"), Convert.ToDouble("113.856412185454"));
            if (CellmapManager.GetStringIni("1", "MarkerType") == "1")
            {
                this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
            }
            else
            {
                Font font = new Font("宋体", 13f, FontStyle.Bold);
                Bitmap bitmap = CellmapManager.TextToBitmap(this.textBox8.Text + "-" + this.textBox9.Text, font, Rectangle.Empty, Color.Blue, Color.Red);
                this.currentMarker = new GMarkerGoogle(this.cellpoint, bitmap);
            }
            //item为一个圆圈，用来表示基站范围
            GMapMarker item = new GMapMarkerCircle(this.cellpoint, Convert.ToInt32(Convert.ToDecimal("1000")) / 2);
            this.objects.Markers.Add(this.currentMarker);
            string str7 = "CellMap" ;
            this.currentMarker.ToolTipText = str7;
            this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            this.currentMarker.ToolTip.Fill = new SolidBrush(Color.Red);
            //设置地图中心为cellpoint
            this.MainMap.Position = this.cellpoint;
            this.polygons.Markers.Add(item);
            this.polygonPoints.Add(this.cellpoint);
             */

        }

        private void button7_Click(object sender, EventArgs e)
        {
            GMapRoute item = new GMapRoute(this.polygonPoints, "") {
                Stroke = new Pen(Color.FromArgb(0x90, Color.Red))
            };
            item.Stroke.Width = 5f;
            this.routes.Routes.Add(item);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.routes.Routes.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.textBox3.BringToFront();
            this.MainMap.BringToFront();
            this.textBox3.Text = "定位中，请稍后......";
            this.button9.Enabled = false;
            this.backgroundWorker9.RunWorkerAsync();
        }

        private void CleanMap()
        {
            this.objects.Markers.Clear();
            this.top.Markers.Clear();
            this.routes.Routes.Clear();
            this.polygons.Markers.Clear();
        }

        public static GMapPolygon CreateCircle(PointLatLng center, double radius, string name)
        {
            List<PointLatLng> points = new List<PointLatLng>();
            int num = 0x186a0;
            double num2 = 6.2831853071795862 / ((double) num);
            for (int i = 0; i < num; i++)
            {
                double a = i * num2;
                double lat = center.Lat + (Math.Sin(a) * radius);
                double lng = center.Lng + (Math.Cos(a) * radius);
                points.Add(new PointLatLng(lat, lng));
            }
            return new GMapPolygon(points, name) { Stroke = new Pen(Brushes.Red, 1f) };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.webBrowser1.Dock = DockStyle.Fill;
            //this.webBrowser1.BringToFront();
            //this.webBrowser1.ScriptErrorsSuppressed = true;
            //this.webBrowser1.Navigate(new Uri(CellmapManager.WebSite + "ad4cellmappc.html"));
            this.toolStripStatusLabel1.Text = "Lat: " + this.centerlat + ", Lng: " + this.centerlng;
            this.MainMap.OnMarkerEnter += new MarkerEnter(this.MainMap_OnMarkerEnter);
            this.MainMap.OnMarkerLeave += new MarkerLeave(this.MainMap_OnMarkerLeave);
            this.MainMap.OnMarkerClick += new MarkerClick(this.MainMap_OnMarkerClick);
            this.MainMap.Manager.Mode = AccessMode.ServerAndCache;
            this.MainMap.MapProvider = GMapProviders.AMap;
            this.MainMap.MaxZoom = 0x12;
            this.MainMap.MinZoom = 1;
            this.MainMap.Zoom = 15.0;
            this.MainMap.Position = this.start;
            this.MainMap.DragButton = MouseButtons.Left;
            this.routes = new GMapOverlay("routes");
            this.MainMap.Overlays.Add(this.routes);
            this.polygons = new GMapOverlay("polygons");
            this.MainMap.Overlays.Add(this.polygons);
            this.objects = new GMapOverlay("objects");
            this.MainMap.Overlays.Add(this.objects);
            this.top = new GMapOverlay("top");
            this.MainMap.Overlays.Add(this.top);
            this.polygon = new GMapPolygon(this.polygonPoints, "polygon test");
            this.polygons.Polygons.Add(this.polygon);
            this.MainMap.OnPositionChanged += new PositionChanged(this.MainMap_OnCurrentPositionChanged);
            //删掉版本检测
            //this.backgroundWorker12.RunWorkerAsync();
        }

        private void gPS批量查询Excel文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.loginstatus == "true")
            //{
                this.textBox3.BringToFront();
                this.MainMap.BringToFront();
                this.f4 = new Form4(this);
                this.f4.Owner = this;
                this.f4.Show();
                this.f4.BringToFront();
            //}
            //else
            //{
            //    MessageBox.Show("请登录后继续使用！", "提示");
            //}
        }

        private void gps批量查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.loginstatus == "true")
            {
                this.textBox3.BringToFront();
                this.MainMap.BringToFront();
                string text = "使用说明：\r\n1、把需要查询的GPS经纬度按照data文件夹下面的gpsinput.txt文件的格式生成，路径不变！";
                if (MessageBox.Show(text, "gps批量查询", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    this.backgroundWorker5.RunWorkerAsync();
                    this.textBox3.Text = "批量查询开始，请稍后......\r\n";
                }
            }
            else
            {
                MessageBox.Show(this.LoginNotice, "提示");
            }
        }

        private void init()
        {
            Directory.CreateDirectory(Application.StartupPath + @"\config");
            Directory.CreateDirectory(Application.StartupPath + @"\data");
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.登陆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户登陆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账号查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aaaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPS批量查询Excel文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开10进制文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进制文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gps批量查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示所有基站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.电子地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.卫星地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.谷歌电子地图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示所有标记ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除所有标记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除标记ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图缩小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.地图放大ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示附近基站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示移动基站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示联通基站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图标样式ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图形ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文字ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除本地数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.检查更新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.官方网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kkkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ololol2 = new GMap.NET.WindowsForms.GMapControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button17 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMap = new GMap.NET.WindowsForms.GMapControl();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker5 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker6 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker7 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker8 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker9 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker10 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker11 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker12 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker13 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker14 = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登陆ToolStripMenuItem,
            this.批量查询ToolStripMenuItem,
            this.显示所有基站ToolStripMenuItem,
            this.显示所有标记ToolStripMenuItem1,
            this.删除所有标记ToolStripMenuItem,
            this.删除标记ToolStripMenuItem,
            this.地图缩小ToolStripMenuItem,
            this.地图放大ToolStripMenuItem,
            this.显示附近基站ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.kkkToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(975, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 登陆ToolStripMenuItem
            // 
            this.登陆ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户登陆ToolStripMenuItem,
            this.修改密码ToolStripMenuItem,
            this.账号查询ToolStripMenuItem});
            this.登陆ToolStripMenuItem.Name = "登陆ToolStripMenuItem";
            this.登陆ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.登陆ToolStripMenuItem.Text = "注册用户专区";
            this.登陆ToolStripMenuItem.Visible = false;
            this.登陆ToolStripMenuItem.Click += new System.EventHandler(this.登陆ToolStripMenuItem_Click);
            // 
            // 用户登陆ToolStripMenuItem
            // 
            this.用户登陆ToolStripMenuItem.Name = "用户登陆ToolStripMenuItem";
            this.用户登陆ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.用户登陆ToolStripMenuItem.Text = "用户登陆";
            this.用户登陆ToolStripMenuItem.Click += new System.EventHandler(this.用户登陆ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改密码ToolStripMenuItem.Text = "密码修改";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 账号查询ToolStripMenuItem
            // 
            this.账号查询ToolStripMenuItem.Name = "账号查询ToolStripMenuItem";
            this.账号查询ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.账号查询ToolStripMenuItem.Text = "账号查询";
            this.账号查询ToolStripMenuItem.Click += new System.EventHandler(this.账号查询ToolStripMenuItem_Click);
            // 
            // 批量查询ToolStripMenuItem
            // 
            this.批量查询ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aaaToolStripMenuItem,
            this.TrackToolStripMenuItem,
            this.gPS批量查询Excel文件ToolStripMenuItem,
            this.打开10进制文件ToolStripMenuItem,
            this.进制文件ToolStripMenuItem,
            this.gps批量查询ToolStripMenuItem});
            this.批量查询ToolStripMenuItem.Name = "批量查询ToolStripMenuItem";
            this.批量查询ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.批量查询ToolStripMenuItem.Text = "批量查询";
            // 
            // aaaToolStripMenuItem
            // 
            this.aaaToolStripMenuItem.Name = "aaaToolStripMenuItem";
            this.aaaToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.aaaToolStripMenuItem.Text = "基站批量查询（移动联通）";
            this.aaaToolStripMenuItem.Click += new System.EventHandler(this.aaaToolStripMenuItem_Click);
            // 
            // TrackToolStripMenuItem
            // 
            this.TrackToolStripMenuItem.Name = "TrackToolStripMenuItem";
            this.TrackToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.TrackToolStripMenuItem.Text = "基站批量查询结果回放";
            this.TrackToolStripMenuItem.Click += new System.EventHandler(this.TrackToolStripMenuItem_Click);
            // 
            // gPS批量查询Excel文件ToolStripMenuItem
            // 
            this.gPS批量查询Excel文件ToolStripMenuItem.Name = "gPS批量查询Excel文件ToolStripMenuItem";
            this.gPS批量查询Excel文件ToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.gPS批量查询Excel文件ToolStripMenuItem.Text = "GPS批量查询";
            this.gPS批量查询Excel文件ToolStripMenuItem.Click += new System.EventHandler(this.gPS批量查询Excel文件ToolStripMenuItem_Click);
            // 
            // 打开10进制文件ToolStripMenuItem
            // 
            this.打开10进制文件ToolStripMenuItem.Name = "打开10进制文件ToolStripMenuItem";
            this.打开10进制文件ToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.打开10进制文件ToolStripMenuItem.Text = "GSM基站批量查询（10进制txt文件）";
            this.打开10进制文件ToolStripMenuItem.Visible = false;
            this.打开10进制文件ToolStripMenuItem.Click += new System.EventHandler(this.打开10进制文件ToolStripMenuItem_Click);
            // 
            // 进制文件ToolStripMenuItem
            // 
            this.进制文件ToolStripMenuItem.Name = "进制文件ToolStripMenuItem";
            this.进制文件ToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.进制文件ToolStripMenuItem.Text = "GSM基站批量查询（16进制txt文件）";
            this.进制文件ToolStripMenuItem.Visible = false;
            this.进制文件ToolStripMenuItem.Click += new System.EventHandler(this.进制文件ToolStripMenuItem_Click);
            // 
            // gps批量查询ToolStripMenuItem
            // 
            this.gps批量查询ToolStripMenuItem.Name = "gps批量查询ToolStripMenuItem";
            this.gps批量查询ToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.gps批量查询ToolStripMenuItem.Text = "GPS批量查询（txt文件）";
            this.gps批量查询ToolStripMenuItem.Visible = false;
            this.gps批量查询ToolStripMenuItem.Click += new System.EventHandler(this.gps批量查询ToolStripMenuItem_Click);
            // 
            // 显示所有基站ToolStripMenuItem
            // 
            this.显示所有基站ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.电子地图ToolStripMenuItem,
            this.卫星地图ToolStripMenuItem,
            this.谷歌电子地图ToolStripMenuItem});
            this.显示所有基站ToolStripMenuItem.Name = "显示所有基站ToolStripMenuItem";
            this.显示所有基站ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.显示所有基站ToolStripMenuItem.Text = "地图切换";
            this.显示所有基站ToolStripMenuItem.Click += new System.EventHandler(this.显示所有基站ToolStripMenuItem_Click);
            // 
            // 电子地图ToolStripMenuItem
            // 
            this.电子地图ToolStripMenuItem.Name = "电子地图ToolStripMenuItem";
            this.电子地图ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.电子地图ToolStripMenuItem.Text = "高德电子地图";
            this.电子地图ToolStripMenuItem.Click += new System.EventHandler(this.电子地图ToolStripMenuItem_Click);
            // 
            // 卫星地图ToolStripMenuItem
            // 
            this.卫星地图ToolStripMenuItem.Name = "卫星地图ToolStripMenuItem";
            this.卫星地图ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.卫星地图ToolStripMenuItem.Text = "高德卫星地图";
            this.卫星地图ToolStripMenuItem.Click += new System.EventHandler(this.卫星地图ToolStripMenuItem_Click);
            // 
            // 谷歌电子地图ToolStripMenuItem
            // 
            this.谷歌电子地图ToolStripMenuItem.Name = "谷歌电子地图ToolStripMenuItem";
            this.谷歌电子地图ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.谷歌电子地图ToolStripMenuItem.Text = "谷歌电子地图";
            this.谷歌电子地图ToolStripMenuItem.Visible = false;
            this.谷歌电子地图ToolStripMenuItem.Click += new System.EventHandler(this.谷歌电子地图ToolStripMenuItem_Click);
            // 
            // 显示所有标记ToolStripMenuItem1
            // 
            this.显示所有标记ToolStripMenuItem1.Name = "显示所有标记ToolStripMenuItem1";
            this.显示所有标记ToolStripMenuItem1.Size = new System.Drawing.Size(92, 21);
            this.显示所有标记ToolStripMenuItem1.Text = "显示所有标记";
            this.显示所有标记ToolStripMenuItem1.Click += new System.EventHandler(this.显示所有标记ToolStripMenuItem1_Click);
            // 
            // 删除所有标记ToolStripMenuItem
            // 
            this.删除所有标记ToolStripMenuItem.Name = "删除所有标记ToolStripMenuItem";
            this.删除所有标记ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.删除所有标记ToolStripMenuItem.Text = "删除所有标记";
            this.删除所有标记ToolStripMenuItem.Click += new System.EventHandler(this.删除所有标记ToolStripMenuItem_Click);
            // 
            // 删除标记ToolStripMenuItem
            // 
            this.删除标记ToolStripMenuItem.Name = "删除标记ToolStripMenuItem";
            this.删除标记ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.删除标记ToolStripMenuItem.Text = "删除指定标记";
            this.删除标记ToolStripMenuItem.Click += new System.EventHandler(this.删除标记ToolStripMenuItem_Click);
            // 
            // 地图缩小ToolStripMenuItem
            // 
            this.地图缩小ToolStripMenuItem.Name = "地图缩小ToolStripMenuItem";
            this.地图缩小ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.地图缩小ToolStripMenuItem.Text = "地图缩小";
            this.地图缩小ToolStripMenuItem.Click += new System.EventHandler(this.地图缩小ToolStripMenuItem_Click);
            // 
            // 地图放大ToolStripMenuItem
            // 
            this.地图放大ToolStripMenuItem.Name = "地图放大ToolStripMenuItem";
            this.地图放大ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.地图放大ToolStripMenuItem.Text = "地图放大";
            this.地图放大ToolStripMenuItem.Click += new System.EventHandler(this.地图放大ToolStripMenuItem_Click);
            // 
            // 显示附近基站ToolStripMenuItem
            // 
            this.显示附近基站ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示移动基站ToolStripMenuItem,
            this.显示联通基站ToolStripMenuItem});
            this.显示附近基站ToolStripMenuItem.Name = "显示附近基站ToolStripMenuItem";
            this.显示附近基站ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.显示附近基站ToolStripMenuItem.Text = "显示附近基站";
            // 
            // 显示移动基站ToolStripMenuItem
            // 
            this.显示移动基站ToolStripMenuItem.Name = "显示移动基站ToolStripMenuItem";
            this.显示移动基站ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示移动基站ToolStripMenuItem.Text = "显示移动基站";
            this.显示移动基站ToolStripMenuItem.Click += new System.EventHandler(this.显示移动基站ToolStripMenuItem_Click);
            // 
            // 显示联通基站ToolStripMenuItem
            // 
            this.显示联通基站ToolStripMenuItem.Name = "显示联通基站ToolStripMenuItem";
            this.显示联通基站ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示联通基站ToolStripMenuItem.Text = "显示联通基站";
            this.显示联通基站ToolStripMenuItem.Click += new System.EventHandler(this.显示联通基站ToolStripMenuItem_Click);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图标样式ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 图标样式ToolStripMenuItem
            // 
            this.图标样式ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.图形ToolStripMenuItem,
            this.文字ToolStripMenuItem});
            this.图标样式ToolStripMenuItem.Name = "图标样式ToolStripMenuItem";
            this.图标样式ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.图标样式ToolStripMenuItem.Text = "图标样式";
            // 
            // 图形ToolStripMenuItem
            // 
            this.图形ToolStripMenuItem.Name = "图形ToolStripMenuItem";
            this.图形ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.图形ToolStripMenuItem.Text = "图形";
            this.图形ToolStripMenuItem.Click += new System.EventHandler(this.图形ToolStripMenuItem_Click);
            // 
            // 文字ToolStripMenuItem
            // 
            this.文字ToolStripMenuItem.Name = "文字ToolStripMenuItem";
            this.文字ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.文字ToolStripMenuItem.Text = "文字";
            this.文字ToolStripMenuItem.Click += new System.EventHandler(this.文字ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除本地数据库ToolStripMenuItem,
            this.检查更新ToolStripMenuItem,
            this.官方网站ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 清除本地数据库ToolStripMenuItem
            // 
            this.清除本地数据库ToolStripMenuItem.Name = "清除本地数据库ToolStripMenuItem";
            this.清除本地数据库ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.清除本地数据库ToolStripMenuItem.Text = "清除本地数据库";
            this.清除本地数据库ToolStripMenuItem.Click += new System.EventHandler(this.清除本地数据库ToolStripMenuItem_Click);
            // 
            // 检查更新ToolStripMenuItem
            // 
            this.检查更新ToolStripMenuItem.Name = "检查更新ToolStripMenuItem";
            this.检查更新ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.检查更新ToolStripMenuItem.Text = "检查更新";
            this.检查更新ToolStripMenuItem.Click += new System.EventHandler(this.检查更新ToolStripMenuItem_Click_1);
            // 
            // 官方网站ToolStripMenuItem
            // 
            this.官方网站ToolStripMenuItem.Name = "官方网站ToolStripMenuItem";
            this.官方网站ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.官方网站ToolStripMenuItem.Text = "关于";
            this.官方网站ToolStripMenuItem.Click += new System.EventHandler(this.官方网站ToolStripMenuItem_Click);
            // 
            // kkkToolStripMenuItem
            // 
            this.kkkToolStripMenuItem.Name = "kkkToolStripMenuItem";
            this.kkkToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.kkkToolStripMenuItem.Text = "退出";
            this.kkkToolStripMenuItem.Click += new System.EventHandler(this.kkkToolStripMenuItem_Click);
            // 
            // ololol2
            // 
            this.ololol2.Bearing = 0F;
            this.ololol2.CanDragMap = true;
            this.ololol2.EmptyTileColor = System.Drawing.Color.Navy;
            this.ololol2.GrayScaleMode = false;
            this.ololol2.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.ololol2.LevelsKeepInMemmory = 5;
            this.ololol2.Location = new System.Drawing.Point(57, 78);
            this.ololol2.MarkersEnabled = true;
            this.ololol2.MaxZoom = 2;
            this.ololol2.MinZoom = 2;
            this.ololol2.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.ololol2.Name = "ololol2";
            this.ololol2.NegativeMode = false;
            this.ololol2.PolygonsEnabled = true;
            this.ololol2.RetryLoadTile = 0;
            this.ololol2.RoutesEnabled = true;
            this.ololol2.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.ololol2.ShowTileGridLines = false;
            this.ololol2.Size = new System.Drawing.Size(487, 303);
            this.ololol2.TabIndex = 0;
            this.ololol2.Zoom = 0D;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // backgroundWorker3
            // 
            this.backgroundWorker3.WorkerReportsProgress = true;
            this.backgroundWorker3.WorkerSupportsCancellation = true;
            this.backgroundWorker3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker3_DoWork);
            this.backgroundWorker3.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker3_ProgressChanged);
            this.backgroundWorker3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker3_RunWorkerCompleted);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.textBox3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.label2);
            this.splitContainer4.Panel2.Controls.Add(this.textBox5);
            this.splitContainer4.Panel2.Controls.Add(this.button6);
            this.splitContainer4.Panel2.Controls.Add(this.groupBox5);
            this.splitContainer4.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer4.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer4.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer4.Size = new System.Drawing.Size(334, 674);
            this.splitContainer4.SplitterDistance = 118;
            this.splitContainer4.TabIndex = 30;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox3.Location = new System.Drawing.Point(0, 0);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox3.Size = new System.Drawing.Size(334, 118);
            this.textBox3.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 30;
            this.label2.Text = "麦谷基站报文";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(94, 15);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(109, 21);
            this.textBox5.TabIndex = 30;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(217, 14);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(62, 23);
            this.button6.TabIndex = 30;
            this.button6.Text = "查询";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.textBox11);
            this.groupBox5.Controls.Add(this.textBox10);
            this.groupBox5.Controls.Add(this.radioButton3);
            this.groupBox5.Controls.Add(this.radioButton4);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Location = new System.Drawing.Point(16, 398);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(273, 144);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "根据城市查基站";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(189, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 23);
            this.button3.TabIndex = 28;
            this.button3.Text = "CDMA";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(74, 71);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(88, 21);
            this.textBox11.TabIndex = 27;
            this.textBox11.Text = "14242";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(74, 30);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(88, 21);
            this.textBox10.TabIndex = 26;
            this.textBox10.Text = "邯郸市";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(102, 109);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 16);
            this.radioButton3.TabIndex = 25;
            this.radioButton3.Text = "十六进制";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(17, 107);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(60, 20);
            this.radioButton4.TabIndex = 24;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "十进制";
            this.radioButton4.UseCompatibleTextRendering = true;
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "Cell/Bid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(15, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "城市";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "GSM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button17);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Location = new System.Drawing.Point(16, 289);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 92);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "经纬度查询";
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(189, 60);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(74, 23);
            this.button17.TabIndex = 28;
            this.button17.Text = "中点地址";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click_1);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(56, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(121, 21);
            this.textBox2.TabIndex = 15;
            this.textBox2.Text = "116.397494";
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(10, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "lng";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(10, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "lat";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(56, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 21);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "39.9087";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(188, 26);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 6;
            this.button9.Text = "查询";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button10);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.textBox7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBox6);
            this.groupBox3.Location = new System.Drawing.Point(16, 176);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 91);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "电信CDMA基站查询";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(188, 62);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 5;
            this.button10.Text = "查询";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "BID";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(35, 25);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(62, 21);
            this.textBox7.TabIndex = 3;
            this.textBox7.Text = "13824";
            this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox7_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "NID";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(152, 25);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(55, 21);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "1";
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox4_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "SID";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(35, 59);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(62, 21);
            this.textBox6.TabIndex = 5;
            this.textBox6.Text = "98";
            this.textBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox6_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.textBox9);
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(16, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 122);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "移动联通基站查询";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(177, 82);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 23);
            this.button5.TabIndex = 29;
            this.button5.Text = "前后基站查询";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(177, 50);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 23);
            this.button4.TabIndex = 28;
            this.button4.Text = "LAC 查询";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(58, 47);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(100, 21);
            this.textBox9.TabIndex = 27;
            this.textBox9.Text = "4043";
            this.textBox9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox9_KeyPress);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(58, 20);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 21);
            this.textBox8.TabIndex = 26;
            this.textBox8.Text = "9766";
            this.textBox8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox8_KeyPress);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(91, 82);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 25;
            this.radioButton2.Text = "十六进制";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(17, 80);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 20);
            this.radioButton1.TabIndex = 24;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "十进制";
            this.radioButton1.UseCompatibleTextRendering = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(15, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "CELL";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(15, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "LAC";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(177, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "云查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.statusStrip1);
            this.splitContainer1.Panel1.Controls.Add(this.MainMap);
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(975, 678);
            this.splitContainer1.SplitterDistance = 633;
            this.splitContainer1.TabIndex = 7;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 652);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(629, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // MainMap
            // 
            this.MainMap.Bearing = 0F;
            this.MainMap.CanDragMap = true;
            this.MainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.MainMap.GrayScaleMode = false;
            this.MainMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.MainMap.LevelsKeepInMemmory = 5;
            this.MainMap.Location = new System.Drawing.Point(0, 0);
            this.MainMap.MarkersEnabled = true;
            this.MainMap.MaxZoom = 17;
            this.MainMap.MinZoom = 2;
            this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MainMap.Name = "MainMap";
            this.MainMap.NegativeMode = false;
            this.MainMap.PolygonsEnabled = true;
            this.MainMap.RetryLoadTile = 0;
            this.MainMap.RoutesEnabled = true;
            this.MainMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.MainMap.ShowTileGridLines = false;
            this.MainMap.Size = new System.Drawing.Size(629, 674);
            this.MainMap.TabIndex = 3;
            this.MainMap.Zoom = 0D;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 1);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(170, 117);
            this.webBrowser1.TabIndex = 17;
            this.webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(this.WebBrowser_NewWindow);
            // 
            // backgroundWorker4
            // 
            this.backgroundWorker4.WorkerReportsProgress = true;
            this.backgroundWorker4.WorkerSupportsCancellation = true;
            // 
            // backgroundWorker5
            // 
            this.backgroundWorker5.WorkerReportsProgress = true;
            this.backgroundWorker5.WorkerSupportsCancellation = true;
            this.backgroundWorker5.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker5_DoWork);
            this.backgroundWorker5.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker5_ProgressChanged);
            this.backgroundWorker5.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker5_RunWorkerCompleted);
            // 
            // backgroundWorker6
            // 
            this.backgroundWorker6.WorkerReportsProgress = true;
            this.backgroundWorker6.WorkerSupportsCancellation = true;
            this.backgroundWorker6.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker6_DoWork);
            this.backgroundWorker6.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker6_ProgressChanged);
            this.backgroundWorker6.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker6_RunWorkerCompleted);
            // 
            // backgroundWorker7
            // 
            this.backgroundWorker7.WorkerReportsProgress = true;
            this.backgroundWorker7.WorkerSupportsCancellation = true;
            this.backgroundWorker7.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker7_DoWork);
            this.backgroundWorker7.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker7_RunWorkerCompleted);
            // 
            // backgroundWorker8
            // 
            this.backgroundWorker8.WorkerReportsProgress = true;
            this.backgroundWorker8.WorkerSupportsCancellation = true;
            this.backgroundWorker8.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker8_DoWork);
            this.backgroundWorker8.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker8_RunWorkerCompleted);
            // 
            // backgroundWorker9
            // 
            this.backgroundWorker9.WorkerReportsProgress = true;
            this.backgroundWorker9.WorkerSupportsCancellation = true;
            this.backgroundWorker9.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker9_DoWork);
            this.backgroundWorker9.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker9_RunWorkerCompleted);
            // 
            // backgroundWorker10
            // 
            this.backgroundWorker10.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker10_DoWork);
            this.backgroundWorker10.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker10_RunWorkerCompleted);
            // 
            // backgroundWorker11
            // 
            this.backgroundWorker11.WorkerReportsProgress = true;
            this.backgroundWorker11.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker11_DoWork);
            this.backgroundWorker11.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker11_ProgressChanged);
            this.backgroundWorker11.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker11_RunWorkerCompleted);
            // 
            // backgroundWorker12
            // 
            this.backgroundWorker12.WorkerReportsProgress = true;
            this.backgroundWorker12.WorkerSupportsCancellation = true;
            this.backgroundWorker12.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker12_DoWork);
            this.backgroundWorker12.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker12_RunWorkerCompleted);
            // 
            // backgroundWorker13
            // 
            this.backgroundWorker13.WorkerReportsProgress = true;
            this.backgroundWorker13.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker13_DoWork);
            this.backgroundWorker13.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker13_ProgressChanged);
            this.backgroundWorker13.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker13_RunWorkerCompleted);
            // 
            // backgroundWorker14
            // 
            this.backgroundWorker14.WorkerReportsProgress = true;
            this.backgroundWorker14.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker14_DoWork);
            this.backgroundWorker14.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker14_ProgressChanged);
            this.backgroundWorker14.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker14_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(975, 703);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void kkkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static DataSet LoadDataFromExcel(string filePath)
        {
            try
            {
                OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=False;IMEX=1'");
                selectConnection.Open();
                string selectCommandText = "SELECT * FROM  [Sheet1$]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(selectCommandText, selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Sheet1");
                selectConnection.Close();
                return dataSet;
            }
            catch (Exception exception)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + exception.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
        }

        private void MainMap_OnCurrentPositionChanged(PointLatLng point)
        {
            this.centerlat = (Math.Ceiling((double) (point.Lat * 1000000.0)) / 1000000.0).ToString();
            this.centerlng = (Math.Ceiling((double)(point.Lng * 1000000.0)) / 1000000.0).ToString();
            this.toolStripStatusLabel1.Text = "Lat: " + this.centerlat + ", Lng: " + this.centerlng;
        }

        private void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.currentMarker = item;
                if (this.currentMarker.ToolTipMode == MarkerTooltipMode.Always)
                {
                    this.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                }
                else
                {
                    this.currentMarker.ToolTipMode = MarkerTooltipMode.Always;
                }
            }
        }

        private void MainMap_OnMarkerEnter(GMapMarker item)
        {
        }

        private void MainMap_OnMarkerLeave(GMapMarker item)
        {
        }

        private void show_neigh_cell()
        {
            this.MainMap.BringToFront();
            if (this.loginstatus == "true")
            {
                string text = "本功能将显示地图中点坐标，方圆十公里范围内，距离最近的200个基站！\r\n\r\n请先把地图中点移动至指定位置，然后继续操作。";
                if (MessageBox.Show(text, "说明", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    this.cellpoint = new PointLatLng(Convert.ToDouble(this.centerlat), Convert.ToDouble(this.centerlng));
                    this.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.red_pushpin);
                    this.objects.Markers.Add(this.currentMarker);
                    this.显示附近基站ToolStripMenuItem.Enabled = false;
                    this.textBox3.Text = "开始搜索，请耐心等候......\r\n";
                    this.backgroundWorker6.RunWorkerAsync();
                    this.timer1.Interval = 100;
                    this.time1 = 0.0;
                    this.timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("登陆后，可以显示当前坐标附近一公里以内的基站！", "提示");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button9_Click(null, null);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button9_Click(null, null);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button10_Click(null, null);
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button10_Click(null, null);
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button10_Click(null, null);
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button2_Click_1(null, null);
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.button2_Click_1(null, null);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.time1 += 0.1;
            this.textBox3.Text = "开始搜索，请耐心等候......\r\n耗时（秒）：" + this.time1.ToString("0.0") + "\r\n";
        }

        private void TrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.loginstatus == "true")
            //{
                this.textBox3.BringToFront();
                this.MainMap.BringToFront();
                this.f5 = new Form5(this);
                this.f5.Owner = this;
                this.f5.Show();
                this.f5.BringToFront();
            //}
            //else
            //{
            //    MessageBox.Show("请登录后继续使用！", "提示");
            //}
        }

        private void WebBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            this.textBox3.BringToFront();
            this.打开10进制文件ToolStripMenuItem.Enabled = true;
            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button9.Enabled = true;
            this.button10.Enabled = true;
            this.button17.Enabled = true;
        }

        public void ZoomToFitMarkers()
        {
            this.MainMap.ZoomAndCenterMarkers("objects");
        }

        private void 打开10进制文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (this.loginstatus == "true")
            //{
                this.textBox3.BringToFront();
                this.MainMap.BringToFront();
                string text = "使用说明：\r\n1、把需要查询的基站按照data文件夹下面cellinput.txt文件的格式生成，路径不变；\r\n2、cellinput.txt不能有空行；\r\n3、celloutput.txt是结果输出文件，每次用完可以删除。\r\n";
                if (MessageBox.Show(text, "10进制基站批量查询", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    this.backgroundWorker1.RunWorkerAsync();
                    this.textBox3.Text = "批量查询开始，请稍后......\r\n";
                }
            //}
            //else
            //{
            //    MessageBox.Show("请登录后继续使用！", "提示");
            //}
        }

        private void 登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 地图放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.Zoom++;
        }

        private void 地图缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.Zoom--;
        }

        private void 电子地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.MapProvider = GMapProviders.AMap;
        }

        private void 谷歌电子地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.MapProvider = GMapProviders.GoogleChinaMap;
        }

        private void 官方网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "技术支持 Email：1207@mapgoo.net";
            MessageBox.Show(text, "麦谷专用基站查询工具");
        }

        private void 检查更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("IEXPLORE.EXE", CellmapManager.WebSite);
        }

        private void 检查更新ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //this.backgroundWorker12.RunWorkerAsync();

            MessageBox.Show("当前使用的为最新版", "麦谷专用基站查询工具");
        }

        private void 进制文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.loginstatus == "true")
            {
                this.textBox3.BringToFront();
                this.MainMap.BringToFront();
                string text = "使用说明：\r\n1、把需要查询的基站按照data文件夹下面cellinput16.txt文件的格式生成，路径不变；\r\n2、celloutput.txt是结果输出文件，每次用完可以删除！";
                if (MessageBox.Show(text, "16进制基站批量查询", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    this.backgroundWorker3.RunWorkerAsync();
                    this.textBox3.Text = "16进制基站批量查询开始，请稍后......\r\n";
                }
            }
            else
            {
                MessageBox.Show(this.LoginNotice, "提示");
            }
        }

        private void 清除本地数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "清除本地所有基站数据文件？";
            if (MessageBox.Show(text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(Application.StartupPath + @"\data\" + this.CdmaDataSourceFile, false, Encoding.GetEncoding("gb2312"));
                    writer.WriteLine("");
                    writer.Close();
                    StreamWriter writer2 = new StreamWriter(Application.StartupPath + @"\data\" + this.GsmDataSourceFile, false, Encoding.GetEncoding("gb2312"));
                    writer2.WriteLine("");
                    writer2.Close();
                    System.IO.File.Delete(this.GpsDataSourceFile);
                }
                catch
                {
                }
            }
        }

        private void 删除标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.objects.Markers.Remove(this.currentMarker);
        }

        private void 删除所有标记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CleanMap();
        }

        private void 使用帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.BringToFront();
            this.webBrowser1.Navigate(new Uri(CellmapManager.WebSite + "ad4cellmappc.html"));
        }

        private void 图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.图形ToolStripMenuItem.Checked = true;
            this.文字ToolStripMenuItem.Checked = false;
            CellmapManager.WriteToIni("1", "MarkerType", "1");
        }

        private void 卫星地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MainMap.MapProvider = GMapProviders.AMapSatelite;
        }

        private void 文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.图形ToolStripMenuItem.Checked = false;
            this.文字ToolStripMenuItem.Checked = true;
            CellmapManager.WriteToIni("1", "MarkerType", "2");
        }

        private void 显示联通基站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mnc = "1";
            this.show_neigh_cell();
        }

        private void 显示所有标记ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ZoomToFitMarkers();
        }

        private void 显示所有基站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 显示移动基站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mnc = "0";
            this.show_neigh_cell();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.webBrowser1.BringToFront();
            this.webBrowser1.Navigate(new Uri(CellmapManager.WebSite + "cellmappwrevise.aspx"));
        }

        private void 用户登陆ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.login_button = "true";
            new Form2(this).ShowDialog();
        }

        private void 账号查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.webBrowser1.BringToFront();
            this.webBrowser1.Navigate(new Uri(CellmapManager.WebSite + "cellmapuseradmin.aspx"));
        }

        private void 注册IDToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Process.Start("IEXPLORE.EXE", CellmapManager.WebSite);
        }
    }
}

