namespace cellmap
{
    using GMap.NET;
    using GMap.NET.WindowsForms;
    using GMap.NET.WindowsForms.Markers;
    using mCellmapManager;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.OleDb;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class Form5 : Form
    {
        public string address;
        private BackgroundWorker backgroundWorker1;
        private Button button2;
        public string cell;
        public static string cellColumn;
        public static DataSet cellDataSet;
        public string cellformat;
        public PointLatLng cellpoint;
        private CheckBox checkBox1;
        private IContainer components;
        private DataGridView dataGridView1;
        public string date;
        public static string dateColumn;
        private Form1 f1;
        public string fixlat;
        public string fixlng;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private bool HasCell;
        private bool HasFixLat;
        private bool HasFixLng;
        private bool HasLac;
        private bool HasLat;
        private bool HasLng;
        private bool IsConverted;
        private bool IsLoaded;
        public string lac;
        public static string lacColumn;
        public string lat;
        public string lng;
        private MenuStrip menuStrip1;
        public string outputFile;
        private ProgressBar progressBar1;
        private TextBox textBox3;
        public string time;
        public static string timeColumn;
        private ToolStripMenuItem 帮助ToolStripMenuItem;
        private ToolStripMenuItem 打开轨迹文件ToolStripMenuItem;
        private ToolStripMenuItem 退出ToolStripMenuItem;

        public Form5(Form1 Form1)
        {
            this.f1 = Form1;
            this.InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int num = 0;
            foreach (DataRow row in cellDataSet.Tables[0].Rows)
            {
                this.lac = this.GetColumnName(row, "lac");
                this.cell = this.GetColumnName(row, "cell");
                this.date = this.GetColumnName(row, "date");
                this.time = this.GetColumnName(row, "time");
                this.lat = this.GetColumnName(row, "lat");
                this.lng = this.GetColumnName(row, "lng");
                this.fixlat = this.GetColumnName(row, "fixlat");
                this.fixlng = this.GetColumnName(row, "fixlng");
                this.address = this.GetColumnName(row, "address");
                this.backgroundWorker1.ReportProgress(((num + 1) * 100) / cellDataSet.Tables[0].Rows.Count, "");
                num++;
                Thread.Sleep(50);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
            try
            {
                this.cellpoint = new PointLatLng(Convert.ToDouble(this.fixlat), Convert.ToDouble(this.fixlng));
                if (CellmapManager.GetStringIni("1", "MarkerType") == "1")
                {
                    this.f1.currentMarker = new GMarkerGoogle(this.cellpoint, GMarkerGoogleType.green);
                }
                else
                {
                    Font font = new Font("宋体", 13f, FontStyle.Bold);
                    Bitmap bitmap = CellmapManager.TextToBitmap(this.lac + "-" + this.cell, font, Rectangle.Empty, Color.Blue, Color.Red);
                    this.f1.currentMarker = new GMarkerGoogle(this.cellpoint, bitmap);
                }
                this.f1.objects.Markers.Add(this.f1.currentMarker);
                string str = "基站：" + this.lac + " - " + this.cell + "\r\n经纬度：" + this.lat + "," + this.lng + "\r\n火星坐标：" + this.fixlat + "," + this.fixlng + "\r\n地址：" + this.address + "\r\n时间：" + this.date + "-" + this.time;
                this.f1.currentMarker.ToolTipText = str;
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
            if (this.checkBox1.Checked)
            {
                this.drawRoute();
            }
            this.textBox3.Text = "执行完毕！";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.f1.objects.Markers.Clear();
            this.f1.top.Markers.Clear();
            this.f1.routes.Routes.Clear();
            this.f1.polygons.Markers.Clear();
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.drawRoute();
            }
            else
            {
                this.f1.routes.Routes.Clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.IsLoaded)
            {
                try
                {
                    this.textBox3.Text = this.dataGridView1.Rows[e.RowIndex].Cells["lac"].Value.ToString() + "-" + this.dataGridView1.Rows[e.RowIndex].Cells["cell"].Value.ToString() + ", " + this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["address"].Value.ToString();
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
                    this.textBox3.Text = this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["lac"].Value.ToString() + "-" + this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["cell"].Value.ToString() + ", " + this.dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells["address"].Value.ToString();
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

        public void drawRoute()
        {
            GMapRoute item = new GMapRoute(this.f1.polygonPoints, "") {
                Stroke = new Pen(Color.FromArgb(0x90, Color.Red))
            };
            item.Stroke.Width = 3f;
            this.f1.routes.Routes.Add(item);
        }

        public string GetColumnName(DataRow mDr, string co)
        {
            try
            {
                return mDr[co].ToString();
            }
            catch
            {
                return "";
            }
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开轨迹文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开轨迹文件ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(508, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开轨迹文件ToolStripMenuItem
            // 
            this.打开轨迹文件ToolStripMenuItem.Name = "打开轨迹文件ToolStripMenuItem";
            this.打开轨迹文件ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.打开轨迹文件ToolStripMenuItem.Text = "打开轨迹文件";
            this.打开轨迹文件ToolStripMenuItem.Click += new System.EventHandler(this.打开轨迹文件ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(33, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 61);
            this.button2.TabIndex = 24;
            this.button2.Text = "显示轨迹";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Location = new System.Drawing.Point(33, 130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(430, 57);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询结果";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(6, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(409, 21);
            this.textBox3.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Location = new System.Drawing.Point(165, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 61);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(45, 23);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "线路标注";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(33, 222);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(430, 346);
            this.dataGridView1.TabIndex = 16;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
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
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 592);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(508, 23);
            this.progressBar1.TabIndex = 25;
            // 
            // Form5
            // 
            this.ClientSize = new System.Drawing.Size(508, 615);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(800, 50);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "批量查询轨迹回放";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "1、本功能用于显示批量查询生成的excel文件\r\n2、文件中必须包含如下列：lac、cell、date、time、lat、lng、fixlat、fixlng、address\r\n3、文件格式可参考data目录下的sample_output.xls";
            MessageBox.Show(text, "帮助");
        }

        private void 打开轨迹文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Excel文件(*.xls)|*.xls|所有文件|*.*",
                InitialDirectory = Application.StartupPath + @"\data"
            };
            if (dialog.ShowDialog() != DialogResult.Cancel)
            {
                string fileName = dialog.FileName;
                cellDataSet = null;
                this.dataGridView1.DataSource = null;
                cellDataSet = LoadDataFromExcel(fileName);
                this.IsLoaded = false;
                for (int i = 0; i < cellDataSet.Tables[0].Columns.Count; i++)
                {
                    cellDataSet.Tables[0].Columns[i].ColumnName = cellDataSet.Tables[0].Rows[0][i].ToString();
                    this.f1.polygonPoints = new List<PointLatLng>();
                }
                this.dataGridView1.DataSource = cellDataSet.Tables[0];
                this.IsLoaded = true;
                this.button2.Enabled = true;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

