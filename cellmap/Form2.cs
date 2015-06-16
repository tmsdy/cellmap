namespace cellmap
{
    using cellmap.Properties;
    using mCellmapManager;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class Form2 : Form
    {
        private BackgroundWorker backgroundWorker1;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private IContainer components;
        private Form1 f1;
        private GroupBox groupBox1;
        private string inifile = @"config\User.ini";
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private TextBox textBox2;

        public Form2(Form1 Form1)
        {
            this.f1 = Form1;
            this.InitializeComponent();
            if (this.GetStringIni("1", "SavePassword") == "true")
            {
                this.checkBox1.Checked = true;
                this.textBox1.Text = this.GetStringIni("1", "id");
                this.textBox2.Text = this.GetStringIni("1", "password");
            }
            if (this.GetStringIni("1", "AutoLogin") == "true")
            {
                this.checkBox1.Checked = true;
                this.checkBox2.Checked = true;
            }
            if (this.f1.login_button == "false")
            {
                this.groupBox1.Enabled = false;
                this.pictureBox1.Visible = true;
                this.label3.Visible = true;
                this.label3.Text = "正在登陆，请稍后......";
                this.backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string str = CellmapManager.login(this.textBox1.Text, this.textBox2.Text);
            e.Result = str;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string str = e.Result.ToString();
            if (str != "true")
            {
                this.f1.loginstatus = "true";
                this.f1.textBox3.Text = "登陆成功！";
                this.f1.textBox3.BringToFront();
                if (this.checkBox2.Checked)
                {
                    this.WriteToIni("1", "AutoLogin", "true");
                }
                else
                {
                    this.WriteToIni("1", "AutoLogin", "false");
                }
                if (this.checkBox1.Checked)
                {
                    this.WriteToIni("1", "SavePassword", "true");
                    this.WriteToIni("1", "id", this.textBox1.Text);
                    this.WriteToIni("1", "password", this.textBox2.Text);
                }
                else
                {
                    this.WriteToIni("1", "SavePassword", "false");
                    this.WriteToIni("1", "id", "");
                    this.WriteToIni("1", "password", "");
                }
                base.Close();
            }
            else
            {
                string str2 = "";
                if (str == "error")
                {
                    str2 = "登陆失败。原因：密码不正确，或账号过期！";
                }
                else
                {
                    str2 = "登陆失败。原因：密码不正确，或账号过期！";
                }
                this.WriteToIni("1", "password", "");
                this.f1.loginstatus = str;
                this.f1.textBox3.Text = str2;
                this.f1.textBox3.BringToFront();
                this.pictureBox1.Visible = false;
                this.label3.Text = str2;
            }
            this.button1.Enabled = true;
            this.groupBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            this.groupBox1.Enabled = false;
            this.pictureBox1.Visible = true;
            this.label3.Visible = true;
            this.label3.Text = "正在登陆，请稍后......";
            this.backgroundWorker1.RunWorkerAsync();
            this.f1.textBox3.BringToFront();
            this.f1.MainMap.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked)
            {
                this.checkBox1.Checked = true;
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

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnval, int size, string iniPath);
        private string GetStringIni(string section, string key)
        {
            StringBuilder returnval = new StringBuilder(0x400);
            string def = null;
            GetPrivateProfileString(section, key, def, returnval, 0x400, Application.StartupPath + @"\" + this.inifile);
            return returnval.ToString();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Form2));
            this.label1 = new Label();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.textBox2 = new TextBox();
            this.button1 = new Button();
            this.checkBox1 = new CheckBox();
            this.checkBox2 = new CheckBox();
            this.backgroundWorker1 = new BackgroundWorker();
            this.pictureBox1 = new PictureBox();
            this.groupBox1 = new GroupBox();
            this.button2 = new Button();
            this.label3 = new Label();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x5d, 0x25);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x25, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x5d, 0x59);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x25, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            this.textBox1.Location = new Point(0x99, 0x22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x7a, 0x18);
            this.textBox1.TabIndex = 2;
            this.textBox2.Location = new Point(0x99, 0x56);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Size(0x7a, 0x18);
            this.textBox2.TabIndex = 3;
            this.textBox2.UseSystemPasswordChar = true;
            this.button1.Location = new Point(0x60, 0xc4);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x56, 0x17);
            this.button1.TabIndex = 4;
            this.button1.Text = "登  陆";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new Point(0x60, 0x91);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new Size(0x56, 0x13);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "记住密码";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new Point(0xd7, 0x91);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new Size(0x56, 0x13);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "自动登陆";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
//            this.pictureBox1.Image = Resources.waiting_pic;
            this.pictureBox1.Location = new Point(0x4a, 300);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0x29, 40);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Font = new Font("宋体", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.groupBox1.Location = new Point(0x25, 0x21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(380, 250);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.button2.Location = new Point(0xd7, 0xc4);
            this.button2.Name = "button2";
            this.button2.Size = new Size(0x56, 0x17);
            this.button2.TabIndex = 7;
            this.button2.Text = "取  消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x8d, 0x148);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x29, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            this.label3.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1dc, 0x177);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.pictureBox1);
//            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "Form2";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "用户登陆";
            ((ISupportInitialize) this.pictureBox1).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        private void WriteToIni(string name, string key, string value)
        {
            WritePrivateProfileString(name, key, value, Application.StartupPath + @"\" + this.inifile);
        }
    }
}

