namespace cellmap
{
    using Aspose.Cells;
    using GMap.NET;
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
    using System.Windows.Forms;

    public class Form4 : Form
    {
        private BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button3;
        public string cellformat;
        public PointLatLng cellpoint;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox6;
        private IContainer components;
        private DataGridView dataGridView1;
        private Form1 f1;
        public static DataSet GpsDataSet;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private bool HasCell;
        private bool HasFixLat;
        private bool HasFixLng;
        private bool HasLac;
        private bool HasLat;
        private bool HasLng;
        private bool IsConverted;
        private bool IsLoaded;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        public static string latColumn;
        public static string lngColumn;
        private MenuStrip menuStrip1;
        public static string noteColumn1;
        public static string noteColumn2;
        public string outputFile;
        private ProgressBar progressBar1;
        private SaveFileDialog saveFileDialog1;
        private StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private ToolStripMenuItem 打开文件ToolStripMenuItem;
        private ToolStripMenuItem 开始批量查询ToolStripMenuItem;
        private ToolStripMenuItem 浏览输出文件ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;

        public Form4(Form1 Form1)
        {
            this.f1 = Form1;
            this.InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int num = 0;
            string userState = "null";
            foreach (DataRow row in GpsDataSet.Tables[0].Rows)
            {
                try
                {
                    string str2 = row[latColumn].ToString();
                    string str3 = row[lngColumn].ToString();
                    if (((str2 != "") && (str3 != "")) && ((str2 != "0") && (str3 != "0000")))
                    {
                        string str4;
                        string str5;
                        if (noteColumn1 == "无")
                        {
                            str4 = "";
                        }
                        else
                        {
                            str4 = row[noteColumn1].ToString();
                        }
                        if (noteColumn2 == "无")
                        {
                            str5 = "";
                        }
                        else
                        {
                            str5 = row[noteColumn2].ToString();
                        }
                        userState = CellmapManager.FixGpsApi(Convert.ToDouble(str2), Convert.ToDouble(str3));
                        if (userState == "null")
                        {
                            userState = str2 + "," + str3 + ",null,";
                        }
                        else
                        {
                            string str6 = "";
                            string[] strArray = userState.Split(new char[] { ',' });
                            userState = str2 + "," + str3 + "," + strArray[1] + "," + strArray[0] + "," + str4 + "," + str5 + "," + str6 + ",";
                            row["fixlat"] = strArray[1];
                            row["fixlng"] = strArray[0];
                            row["address"] = str6;
                        }
                    }
                }
                catch
                {
                }
                this.backgroundWorker1.ReportProgress(((num + 1) * 100) / GpsDataSet.Tables[0].Rows.Count, userState);
                num++;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            string str = e.UserState.ToString();
            try
            {
                string[] strArray = str.Split(new char[] { ',' });
                this.cellpoint = new PointLatLng(Convert.ToDouble(strArray[2]), Convert.ToDouble(strArray[3]));
                this.f1.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                this.f1.objects.Markers.Add(this.f1.currentMarker);
                string str2 = "经纬度：" + strArray[0] + ", " + strArray[1] + "\r\n火星坐标：" + strArray[2] + ", " + strArray[3] + "\r\n注释一：" + strArray[4] + "\r\n注释二：" + strArray[5] + "\r\n地址：" + strArray[6];
                this.f1.currentMarker.ToolTipText = str2;
                this.f1.currentMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                this.f1.polygonPoints.Add(this.cellpoint);
                this.f1.ZoomToFitMarkers();
            }
            catch
            {
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GpsDataSet.Tables[0].Columns[latColumn].ColumnName = "lat";
            GpsDataSet.Tables[0].Columns[lngColumn].ColumnName = "lng";
            this.ExportExcel(GpsDataSet, this.textBox2.Text);
            if (this.comboBox6.Text == "是")
            {
                GMapRoute item = new GMapRoute(this.f1.polygonPoints, "") {
                    Stroke = new Pen(Color.FromArgb(0x90, Color.Red))
                };
                item.Stroke.Width = 5f;
                this.f1.routes.Routes.Add(item);
            }
            MessageBox.Show("查询完毕！详细信息请浏览输出文件！", "提示信息");
            this.textBox3.Text = "查询完毕！详细信息请浏览输出文件！";
            this.IsLoaded = true;
            this.开始批量查询ToolStripMenuItem.Enabled = true;
            this.浏览输出文件ToolStripMenuItem.Enabled = true;
            string[] strArray = new string[GpsDataSet.Tables[0].Columns.Count];
            string[] strArray2 = new string[GpsDataSet.Tables[0].Columns.Count];
            string[] strArray3 = new string[GpsDataSet.Tables[0].Columns.Count + 1];
            string[] strArray4 = new string[GpsDataSet.Tables[0].Columns.Count + 1];
            for (int i = 0; i < GpsDataSet.Tables[0].Columns.Count; i++)
            {
                strArray[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                strArray2[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                strArray3[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                strArray4[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
            }
            this.comboBox1.DataSource = strArray;
            this.comboBox2.DataSource = strArray2;
            this.comboBox3.DataSource = strArray3;
            this.comboBox4.DataSource = strArray4;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                AddExtension = true,
                DefaultExt = "rtf",
                CheckPathExists = true,
                Filter = "文本文件 (*.txt)|*.txt",
                OverwritePrompt = true
            };
            if ((dialog.ShowDialog() == DialogResult.OK) && (dialog.FileName.Length > 0))
            {
                this.textBox2.Text = dialog.FileName;
                this.outputFile = this.textBox2.Text;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsLoaded)
            {
                try
                {
                    this.textBox3.Text = this.dataGridView1.Rows[e.RowIndex].Cells["lat"].Value.ToString() + ", " + this.dataGridView1.Rows[e.RowIndex].Cells["lng"].Value.ToString() + ", " + this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["address"].Value.ToString();
                    double lat = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["fixlat"].Value.ToString());
                    double lng = Convert.ToDouble(this.dataGridView1.Rows[e.RowIndex].Cells["fixlng"].Value.ToString());
                    this.cellpoint = new PointLatLng(lat, lng);
                    this.f1.MainMap.Position = this.cellpoint;
                }
                catch (Exception exception)
                {
                    this.textBox3.Text = exception.Message;
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                try
                {
                    this.textBox3.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["lat"].Value.ToString() + ", " + this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["lng"].Value.ToString() + ", " + this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["address"].Value.ToString();
                    double lat = Convert.ToDouble(this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["fixlat"].Value.ToString());
                    double lng = Convert.ToDouble(this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["fixlng"].Value.ToString());
                    this.cellpoint = new PointLatLng(lat, lng);
                    this.f1.MainMap.Position = this.cellpoint;
                }
                catch (Exception exception)
                {
                    this.textBox3.Text = exception.Message;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void ExportExcel(DataSet ds, string strExcelFileName)
        {
            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];
            Aspose.Cells.Cells cells = worksheet.Cells;
            DataTable table = ds.Tables[0];
            int count = table.Columns.Count;
            int num2 = table.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                cells[0, i].PutValue(table.Columns[i].ColumnName);
                cells.SetRowHeight(1, 25.0);
            }
            for (int j = 0; j < num2; j++)
            {
                for (int k = 0; k < count; k++)
                {
                    cells[1 + j, k].PutValue(table.Rows[j][k].ToString());
                }
                cells.SetRowHeight(2 + j, 24.0);
            }
            workbook.Save(strExcelFileName);
            workbook.Worksheets.DeleteName("Evaluation Warning");
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开始批量查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.浏览输出文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 271);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(725, 286);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(657, 92);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "选择输出文件";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 603);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 603);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(799, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox4);
            this.groupBox1.Controls.Add(this.comboBox3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 61);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段定义";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(367, 29);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(45, 20);
            this.comboBox4.TabIndex = 11;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(260, 29);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(45, 20);
            this.comboBox3.TabIndex = 10;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownWidth = 60;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(144, 29);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(45, 20);
            this.comboBox2.TabIndex = 9;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.DropDownWidth = 60;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(45, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(45, 20);
            this.comboBox1.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "注释二";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "注释一";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lat";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(584, 130);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 61);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(93, 25);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(38, 20);
            this.comboBox6.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "标注线路";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "GPS文件";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(85, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(542, 21);
            this.textBox1.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "目标文件";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(85, 92);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(542, 21);
            this.textBox2.TabIndex = 11;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开文件ToolStripMenuItem,
            this.开始批量查询ToolStripMenuItem,
            this.浏览输出文件ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 25);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开文件ToolStripMenuItem
            // 
            this.打开文件ToolStripMenuItem.Name = "打开文件ToolStripMenuItem";
            this.打开文件ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.打开文件ToolStripMenuItem.Text = "打开文件";
            this.打开文件ToolStripMenuItem.Click += new System.EventHandler(this.打开文件ToolStripMenuItem_Click);
            // 
            // 开始批量查询ToolStripMenuItem
            // 
            this.开始批量查询ToolStripMenuItem.Name = "开始批量查询ToolStripMenuItem";
            this.开始批量查询ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.开始批量查询ToolStripMenuItem.Text = "开始查询";
            this.开始批量查询ToolStripMenuItem.Click += new System.EventHandler(this.开始批量查询ToolStripMenuItem_Click);
            // 
            // 浏览输出文件ToolStripMenuItem
            // 
            this.浏览输出文件ToolStripMenuItem.Enabled = false;
            this.浏览输出文件ToolStripMenuItem.Name = "浏览输出文件ToolStripMenuItem";
            this.浏览输出文件ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.浏览输出文件ToolStripMenuItem.Text = "浏览输出文件";
            this.浏览输出文件ToolStripMenuItem.Click += new System.EventHandler(this.浏览输出文件ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(6, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(719, 21);
            this.textBox3.TabIndex = 13;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Location = new System.Drawing.Point(20, 197);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(725, 57);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询结果";
            // 
            // Form4
            // 
            this.ClientSize = new System.Drawing.Size(792, 625);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.Text = "GPS 批量查询";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public static DataSet LoadDataFromExcel(string filePath)
        {
            try
            {
                OleDbConnection selectConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'");
                selectConnection.Open();
                string srcTable = selectConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0][2].ToString().Trim();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM  [" + srcTable + "]", selectConnection);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, srcTable);
                selectConnection.Close();
                return dataSet;
            }
            catch (Exception exception)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + exception.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return null;
            }
        }

        private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Excel文件(*.xls)|*.xls|所有文件|*.*",
                InitialDirectory = Application.StartupPath
            };
            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                string fileName = dialog.FileName;
                this.textBox1.Text = fileName;
                this.textBox2.Text = Path.GetDirectoryName(fileName) + @"\" + Path.GetFileNameWithoutExtension(fileName) + "_output" + Path.GetExtension(fileName);
                GpsDataSet = null;
                this.dataGridView1.DataSource = null;
                GpsDataSet = LoadDataFromExcel(fileName);
                this.IsLoaded = false;
                string[] strArray = new string[GpsDataSet.Tables[0].Columns.Count];
                string[] strArray2 = new string[GpsDataSet.Tables[0].Columns.Count];
                string[] strArray3 = new string[GpsDataSet.Tables[0].Columns.Count + 1];
                string[] strArray4 = new string[GpsDataSet.Tables[0].Columns.Count + 1];
                for (int i = 0; i < GpsDataSet.Tables[0].Columns.Count; i++)
                {
                    if (GpsDataSet.Tables[0].Rows[0][i].ToString() == "lac")
                    {
                        this.HasLac = true;
                    }
                    if (GpsDataSet.Tables[0].Rows[0][i].ToString() == "cell")
                    {
                        this.HasCell = true;
                    }
                    if (GpsDataSet.Tables[0].Rows[0][i].ToString() == "lat")
                    {
                        this.HasLat = true;
                    }
                    if (GpsDataSet.Tables[0].Rows[0][i].ToString() == "lng")
                    {
                        this.HasLng = true;
                    }
                    if (GpsDataSet.Tables[0].Rows[0][i].ToString() == "fixlat")
                    {
                        this.HasFixLat = true;
                    }
                    if (GpsDataSet.Tables[0].Rows[0][i].ToString() == "fixlng")
                    {
                        this.HasFixLng = true;
                    }
                    bool flag1 = GpsDataSet.Tables[0].Rows[0][i].ToString() == "address";
                    strArray[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                    strArray2[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                    strArray3[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                    strArray4[i] = GpsDataSet.Tables[0].Columns[i].ColumnName;
                    this.f1.polygonPoints = new List<PointLatLng>();
                }
                this.dataGridView1.DataSource = GpsDataSet.Tables[0];
                this.IsLoaded = true;
                this.f1.objects.Markers.Clear();
                this.f1.top.Markers.Clear();
                this.f1.routes.Routes.Clear();
                strArray3[GpsDataSet.Tables[0].Columns.Count] = "无";
                strArray4[GpsDataSet.Tables[0].Columns.Count] = "无";
                this.comboBox1.DataSource = strArray;
                this.comboBox2.DataSource = strArray2;
                this.comboBox3.DataSource = strArray3;
                this.comboBox4.DataSource = strArray4;
                string[] strArray5 = new string[] { "是", "否" };
                this.comboBox6.DataSource = strArray5;
            }
        }

        private void 开始批量查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((this.textBox1.Text == "") || (this.textBox2.Text == ""))
            {
                MessageBox.Show("请导入数据文件！", "提示信息");
            }
            else
            {
                this.outputFile = this.textBox2.Text;
                if (this.comboBox1.Text == this.comboBox2.Text)
                {
                    MessageBox.Show("Lat 和 Lng同列，请重新选择！", "提示信息");
                }
                else if (MessageBox.Show("开始查询？", "GPS批量查询", MessageBoxButtons.OKCancel, MessageBoxIcon.None) != DialogResult.Cancel)
                {
                    if ((this.HasLac && this.HasCell) && this.HasLat)
                    {
                        bool hasLng = this.HasLng;
                    }
                    this.开始批量查询ToolStripMenuItem.Enabled = false;
                    File.Delete(this.textBox2.Text);
                    latColumn = this.comboBox1.Text;
                    lngColumn = this.comboBox2.Text;
                    noteColumn1 = this.comboBox3.Text;
                    noteColumn2 = this.comboBox4.Text;
                    try
                    {
                        GpsDataSet.Tables[0].Columns.Add("fixlat", typeof(string));
                        GpsDataSet.Tables[0].Columns.Add("fixlng", typeof(string));
                        GpsDataSet.Tables[0].Columns.Add("address", typeof(string));
                    }
                    catch
                    {
                    }
                    this.textBox3.Text = "开始查询，请稍后！";
                    this.IsLoaded = false;
                    this.backgroundWorker1.RunWorkerAsync();
                }
            }
        }

        private void 浏览输出文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(this.textBox2.Text);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

