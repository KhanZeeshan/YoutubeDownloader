namespace YoutubeDownloader
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.Btn_Download = new System.Windows.Forms.Button();
            this.Txt_Url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.PrgrsBar_Download = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.DownloadFolderPath = new System.Windows.Forms.FolderBrowserDialog();
            this.DataGrd_VdoLst = new System.Windows.Forms.DataGridView();
            this.LblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblResume = new System.Windows.Forms.Label();
            this.RadBtn_Download = new System.Windows.Forms.RadioButton();
            this.RadBtn_DownloadConvert = new System.Windows.Forms.RadioButton();
            this.Cmb_Formats = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrd_VdoLst)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Download
            // 
            this.Btn_Download.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Download.Location = new System.Drawing.Point(415, 122);
            this.Btn_Download.Name = "Btn_Download";
            this.Btn_Download.Size = new System.Drawing.Size(119, 26);
            this.Btn_Download.TabIndex = 2;
            this.Btn_Download.Text = "Fetch Details";
            this.Btn_Download.UseVisualStyleBackColor = true;
            this.Btn_Download.Click += new System.EventHandler(this.Btn_Download_Click);
            // 
            // Txt_Url
            // 
            this.Txt_Url.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Url.Location = new System.Drawing.Point(10, 40);
            this.Txt_Url.Name = "Txt_Url";
            this.Txt_Url.Size = new System.Drawing.Size(523, 25);
            this.Txt_Url.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Youtube URL";
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Enabled = false;
            this.Btn_Stop.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Stop.Location = new System.Drawing.Point(393, 394);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(67, 26);
            this.Btn_Stop.TabIndex = 4;
            this.Btn_Stop.Text = "Pause";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // PrgrsBar_Download
            // 
            this.PrgrsBar_Download.Location = new System.Drawing.Point(10, 334);
            this.PrgrsBar_Download.Name = "PrgrsBar_Download";
            this.PrgrsBar_Download.Size = new System.Drawing.Size(523, 23);
            this.PrgrsBar_Download.TabIndex = 11;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(7, 303);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(524, 28);
            this.lblProgress.TabIndex = 9;
            this.lblProgress.Text = ".";
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Exit.Location = new System.Drawing.Point(466, 394);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(67, 26);
            this.Btn_Exit.TabIndex = 5;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // DownloadFolderPath
            // 
            this.DownloadFolderPath.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
            // 
            // DataGrd_VdoLst
            // 
            this.DataGrd_VdoLst.AllowUserToAddRows = false;
            this.DataGrd_VdoLst.AllowUserToDeleteRows = false;
            this.DataGrd_VdoLst.AllowUserToResizeColumns = false;
            this.DataGrd_VdoLst.AllowUserToResizeRows = false;
            this.DataGrd_VdoLst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGrd_VdoLst.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DataGrd_VdoLst.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.DataGrd_VdoLst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrd_VdoLst.Location = new System.Drawing.Point(10, 158);
            this.DataGrd_VdoLst.MultiSelect = false;
            this.DataGrd_VdoLst.Name = "DataGrd_VdoLst";
            this.DataGrd_VdoLst.ReadOnly = true;
            this.DataGrd_VdoLst.RowHeadersVisible = false;
            this.DataGrd_VdoLst.RowHeadersWidth = 25;
            this.DataGrd_VdoLst.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGrd_VdoLst.Size = new System.Drawing.Size(523, 117);
            this.DataGrd_VdoLst.TabIndex = 3;
            this.DataGrd_VdoLst.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGrd_VdoLst_CellClick);
            // 
            // LblTitle
            // 
            this.LblTitle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTitle.Location = new System.Drawing.Point(12, 70);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(519, 40);
            this.LblTitle.TabIndex = 7;
            this.LblTitle.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(157, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(373, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Click In Download Column Inside Grid to Download Video";
            this.label2.Visible = false;
            // 
            // LblResume
            // 
            this.LblResume.AutoSize = true;
            this.LblResume.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblResume.Location = new System.Drawing.Point(7, 363);
            this.LblResume.Name = "LblResume";
            this.LblResume.Size = new System.Drawing.Size(350, 16);
            this.LblResume.TabIndex = 10;
            this.LblResume.Text = "To Resume Download Just Click The Respective Row in Grid";
            this.LblResume.Visible = false;
            // 
            // RadBtn_Download
            // 
            this.RadBtn_Download.AutoSize = true;
            this.RadBtn_Download.Checked = true;
            this.RadBtn_Download.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.RadBtn_Download.Location = new System.Drawing.Point(267, 9);
            this.RadBtn_Download.Name = "RadBtn_Download";
            this.RadBtn_Download.Size = new System.Drawing.Size(82, 20);
            this.RadBtn_Download.TabIndex = 12;
            this.RadBtn_Download.TabStop = true;
            this.RadBtn_Download.Text = "Download";
            this.RadBtn_Download.UseVisualStyleBackColor = true;
            this.RadBtn_Download.Click += new System.EventHandler(this.RadBtn_Download_Click);
            // 
            // RadBtn_DownloadConvert
            // 
            this.RadBtn_DownloadConvert.AutoSize = true;
            this.RadBtn_DownloadConvert.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.RadBtn_DownloadConvert.Location = new System.Drawing.Point(359, 9);
            this.RadBtn_DownloadConvert.Name = "RadBtn_DownloadConvert";
            this.RadBtn_DownloadConvert.Size = new System.Drawing.Size(179, 20);
            this.RadBtn_DownloadConvert.TabIndex = 13;
            this.RadBtn_DownloadConvert.Text = "Download And Extract Mp3";
            this.RadBtn_DownloadConvert.UseVisualStyleBackColor = true;
            this.RadBtn_DownloadConvert.Click += new System.EventHandler(this.RadBtn_DownloadConvert_Click);
            // 
            // Cmb_Formats
            // 
            this.Cmb_Formats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Formats.Enabled = false;
            this.Cmb_Formats.Font = new System.Drawing.Font("Tahoma", 11F);
            this.Cmb_Formats.FormattingEnabled = true;
            this.Cmb_Formats.Location = new System.Drawing.Point(290, 122);
            this.Cmb_Formats.Name = "Cmb_Formats";
            this.Cmb_Formats.Size = new System.Drawing.Size(119, 26);
            this.Cmb_Formats.TabIndex = 14;
            this.Cmb_Formats.Visible = false;
            this.Cmb_Formats.SelectedValueChanged += new System.EventHandler(this.Cmb_Formats_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(174, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 18);
            this.label3.TabIndex = 15;
            this.label3.Text = "Output Format:";
            this.label3.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 428);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Cmb_Formats);
            this.Controls.Add(this.RadBtn_DownloadConvert);
            this.Controls.Add(this.RadBtn_Download);
            this.Controls.Add(this.LblResume);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblTitle);
            this.Controls.Add(this.DataGrd_VdoLst);
            this.Controls.Add(this.Btn_Exit);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.PrgrsBar_Download);
            this.Controls.Add(this.Btn_Stop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_Url);
            this.Controls.Add(this.Btn_Download);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Youtube Downloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrd_VdoLst)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.Button Btn_Download;
        private System.Windows.Forms.TextBox Txt_Url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.ProgressBar PrgrsBar_Download;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.FolderBrowserDialog DownloadFolderPath;
        private System.Windows.Forms.DataGridView DataGrd_VdoLst;
        private System.Windows.Forms.Label LblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblResume;
        private System.Windows.Forms.RadioButton RadBtn_Download;
        private System.Windows.Forms.RadioButton RadBtn_DownloadConvert;
        private System.Windows.Forms.ComboBox Cmb_Formats;
        private System.Windows.Forms.Label label3;
    }
}