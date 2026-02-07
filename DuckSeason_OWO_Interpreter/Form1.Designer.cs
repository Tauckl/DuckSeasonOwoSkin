namespace DuckSeason_OWO_Form
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dsExeFilePath = new System.Windows.Forms.TextBox();
            this.browseExePath = new System.Windows.Forms.Button();
            this.browseExeOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.owoIpText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.logOutTextBox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sendTestSense = new System.Windows.Forms.Button();
            this.owoConStat = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Install/Update Mod";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(436, 52);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // dsExeFilePath
            // 
            this.dsExeFilePath.Location = new System.Drawing.Point(15, 82);
            this.dsExeFilePath.Name = "dsExeFilePath";
            this.dsExeFilePath.Size = new System.Drawing.Size(480, 20);
            this.dsExeFilePath.TabIndex = 2;
            this.dsExeFilePath.Text = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\DuckSeason\\DuckSeason\\DuckSeason.ex" +
    "e";
            this.dsExeFilePath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // browseExePath
            // 
            this.browseExePath.Location = new System.Drawing.Point(501, 80);
            this.browseExePath.Name = "browseExePath";
            this.browseExePath.Size = new System.Drawing.Size(75, 23);
            this.browseExePath.TabIndex = 3;
            this.browseExePath.Text = "Browse";
            this.browseExePath.UseVisualStyleBackColor = true;
            this.browseExePath.Click += new System.EventHandler(this.browseExePath_Click);
            // 
            // owoIpText
            // 
            this.owoIpText.Location = new System.Drawing.Point(15, 200);
            this.owoIpText.Name = "owoIpText";
            this.owoIpText.Size = new System.Drawing.Size(480, 20);
            this.owoIpText.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(524, 39);
            this.label2.TabIndex = 5;
            this.label2.Text = "Press the \"Connect\"-Button to connect to the owoskin. Afterwards authorize the ga" +
    "me in the \"My OWO\"-App.\r\n\r\nOwoskin-IP (leave empty, for it to automagically conn" +
    "ect)";
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(501, 197);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 6;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // logOutTextBox
            // 
            this.logOutTextBox.Location = new System.Drawing.Point(12, 290);
            this.logOutTextBox.Name = "logOutTextBox";
            this.logOutTextBox.ReadOnly = true;
            this.logOutTextBox.Size = new System.Drawing.Size(776, 148);
            this.logOutTextBox.TabIndex = 7;
            this.logOutTextBox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 274);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Log Output";
            // 
            // sendTestSense
            // 
            this.sendTestSense.Location = new System.Drawing.Point(15, 226);
            this.sendTestSense.Name = "sendTestSense";
            this.sendTestSense.Size = new System.Drawing.Size(116, 23);
            this.sendTestSense.TabIndex = 9;
            this.sendTestSense.Text = "Send test sensation";
            this.sendTestSense.UseVisualStyleBackColor = true;
            this.sendTestSense.Click += new System.EventHandler(this.sendTestSense_Click);
            // 
            // owoConStat
            // 
            this.owoConStat.AutoSize = true;
            this.owoConStat.Location = new System.Drawing.Point(592, 24);
            this.owoConStat.Name = "owoConStat";
            this.owoConStat.Size = new System.Drawing.Size(196, 13);
            this.owoConStat.TabIndex = 10;
            this.owoConStat.Text = "OWO Connection Status: Disconnected";
            this.owoConStat.Click += new System.EventHandler(this.label4_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(669, 261);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Clear Log Output";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(669, 80);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Launch Game";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.owoConStat);
            this.Controls.Add(this.sendTestSense);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.logOutTextBox);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.owoIpText);
            this.Controls.Add(this.browseExePath);
            this.Controls.Add(this.dsExeFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Duck Season OWO";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dsExeFilePath;
        private System.Windows.Forms.Button browseExePath;
        private System.Windows.Forms.OpenFileDialog browseExeOpenFileDialog;
        private System.Windows.Forms.TextBox owoIpText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.RichTextBox logOutTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button sendTestSense;
        private System.Windows.Forms.Label owoConStat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

