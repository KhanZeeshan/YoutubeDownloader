using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Web;
using System.Text;
using System.Collections.Specialized;

namespace YoutubeDownloader
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private Thread thrDownload;
        private Stream strResponse;
        private Stream strLocal;
        private Process process;
        private static List<DownloadLinkInfo> AvailableDownlinks = new List<DownloadLinkInfo>();
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        public static int PercentProgress = 0;
        private delegate void UpdateProgessCallback(Int64 BytesRead, Int64 TotalBytes);
        private string DownloadFile = string.Empty;
        bool DownloadCancel = false;
        string title;
        string FormatCode = string.Empty;
        string Extension = string.Empty;
        int filelen;
        bool CloseForm = false;
        static string TempFolderPath;
        bool DownloadComplete = false;
        static string[] MessageToCheck = { "This video contains content from WMG, who has blocked it in your country on copyright grounds.",
                                  "This video contains content from WMG, who has blocked it on copyright grounds.",
                                  "This video contains content from EMI. It is not available in your country.",
                                  "This video contains content from Vevo. It is not available in your country.",
                                  "This video contains content from Sony Music Entertainment and No Good TV, one or more of whom have blocked it in your country on copyright grounds."};

        List<string> SupportedFormats = new List<string>();
        static string SelectedFormat = string.Empty;

        private void Btn_Download_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txt_Url.Text.Length > 0)
                {
                    if (process != null && TempFolderPath != null && TempFolderPath.Trim().Length > 0)
                    {
                        process.Close();
                        process.Dispose();
                        if (Directory.Exists(TempFolderPath))
                            Directory.Delete(TempFolderPath, true);
                    }
                    DownloadFolderPath.Description = "Select Folder to Save File";
                    DialogResult SelctPath = DialogResult.Cancel;
                    if (DownloadFolderPath.SelectedPath == null || DownloadFolderPath.SelectedPath.Trim().Length == 0)
                        SelctPath = DownloadFolderPath.ShowDialog();
                    else
                        SelctPath = DialogResult.OK;

                    if (SelctPath == DialogResult.OK)
                    {
                        if (DownloadFolderPath.SelectedPath != null || DownloadFolderPath.SelectedPath.Trim().Length != 0)
                        {
                            using (this.Cursor = Cursors.WaitCursor)
                            {
                                if (Txt_Url.Text.Length > 0)
                                {
                                    DataGrd_VdoLst.DataSource = null;
                                    DataGrd_VdoLst.Enabled = true;
                                    lblProgress.Text = "";
                                    PrgrsBar_Download.Value = 0;
                                    bool DownloadNow = false;

                                    if (RadBtn_DownloadConvert.Checked)
                                    {
                                        Cmb_Formats.SelectedIndex = 1;
                                        DownloadNow = true;
                                        /*if (Cmb_Formats.SelectedIndex > 0)
                                        {
                                            DownloadNow = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please Select Format To Convert Into", "Information");
                                            this.Cursor = Cursors.Default;
                                            return;
                                        }*/
                                    }
                                    else if (RadBtn_Download.Checked)
                                    {
                                        DownloadNow = true;
                                    }
                                    if (DownloadNow)
                                    {
                                        BindGrid();
                                        if (title != null && title.Trim().Length > 0)
                                        {
                                            if (title.Length > 70)
                                                LblTitle.Text = title.PadRight(80, ' ').Substring(0, 70) + "....";
                                            else
                                                LblTitle.Text = title;

                                            label2.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Youtube Video URL", "Information");
                                }
                            }
                            this.Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Folder To Save The File", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Video's Url in Textbox", "Information");
                    Txt_Url.Focus();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The remote server returned an error: (404) Not Found."))
                    MessageBox.Show(ex.Message);
                else
                    Common.LogError(ex);
            }
        }

        private void BindGrid()
        {
            try
            {
                YTDownloader DownloadVideo = new YTDownloader();

                DataTable VideoLists = new DataTable();

                VideoLists.Columns.Add("Video Format", typeof(string));
                VideoLists.Columns.Add("Download Link");
                VideoLists.Columns.Add("Video URL", typeof(string));

                string UrlToDownlaod = Txt_Url.Text;
                UrlToDownlaod = (!UrlToDownlaod.StartsWith("http://") && !UrlToDownlaod.StartsWith("https://")) ? ("http://" + UrlToDownlaod) : UrlToDownlaod;

                if (Txt_Url.Text.Contains("youtube.com/"))
                {
                    this.GetDownloadLink(UrlToDownlaod, out title);
                    if (AvailableDownlinks.Count > 0)
                    {
                        for (int i = 0; i < AvailableDownlinks.Count; i++)
                        {
                            DataRow row = VideoLists.NewRow();
                            FormatCode = AvailableDownlinks[i].Tag;
                            string VdoType = string.Empty;

                            switch (FormatCode)
                            {
                                case "37":
                                case "18":
                                case "22":
                                    Extension = ".mp4";
                                    VdoType = "MP4";
                                    break;

                                case "46":
                                case "45":
                                case "44":
                                case "43":
                                    Extension = ".webm";
                                    VdoType = "WEBM";
                                    break;


                                case "?":
                                case "6":
                                case "5":
                                case "34":
                                case "35":
                                    Extension = ".flv";
                                    VdoType = "FLV";
                                    break;

                                case "36":
                                case "13":
                                case "17":
                                    Extension = ".3gp";
                                    VdoType = "3GP";
                                    break;
                            }
                            string NewUrl = AvailableDownlinks[i].DownloadLink;
                            row["Video Format"] = VdoType;
                            row["Download Link"] = "Download";


                            if (FormatCode == "22" || FormatCode == "37" ||
                                FormatCode == "45" || FormatCode == "46")
                                row["Download Link"] += " - HD";

                            row["Video URL"] = NewUrl;
                            VideoLists.Rows.Add(row);

                            DataGrd_VdoLst.DataSource = VideoLists;
                            DataGrd_VdoLst.Columns[0].Width = 150;
                            DataGrd_VdoLst.Columns[1].Width = 200;
                            DataGrd_VdoLst.Columns[2].Visible = false;
                            DataGrd_VdoLst.Columns[2].Width = 0;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wrong Url!", "Information");
                    }
                }
                else
                {
                    MessageBox.Show("Entered Url Is Not of Youtube!", "Information");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The remote server returned an error: (404) Not Found."))
                    MessageBox.Show(ex.Message);
                else
                    Common.LogError(ex);
            }
        }

        private void Download()
        {
            this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { -3, -3 });
            try
            {
                string NewUrl = DataGrd_VdoLst.Rows[SelectedRow].Cells[2].Value.ToString().Trim();
                Extension = DataGrd_VdoLst.Rows[SelectedRow].Cells[0].Value.ToString().Trim().ToLower();
                webRequest = (HttpWebRequest)WebRequest.Create(NewUrl);
                webRequest.Credentials = CredentialCache.DefaultCredentials;

                if (RadBtn_DownloadConvert.Checked)
                {
                    //DownloadFile = GetDownloadLocation() + @"\" + "___111" + "." + Extension;
                    DownloadFile = GetDownloadLocation() + @"\" + DateTime.Now.ToString("MMddyyyyhhmmss") + "." + Extension;
                }
                else if (RadBtn_Download.Checked)
                {
                    DownloadFile = GetDownloadLocation() + Common.RemoveCharaters(title).Replace(":", "_").Replace(";", "_") + "." + Extension;
                }
                filelen = 0;

                if (File.Exists(DownloadFile))
                {
                    FileInfo FileGetSize = new FileInfo(DownloadFile);
                    filelen = Convert.ToInt32(FileGetSize.Length);
                    webRequest.AddRange(filelen);
                }

                webResponse = (HttpWebResponse)webRequest.GetResponse();

                Int64 fileSize = webResponse.ContentLength;

                if (filelen > (Convert.ToInt32(fileSize) + filelen))
                {
                    MessageBox.Show("Local File is Greater Than The File Being Downloaded \r\nPlease Delete It!", "Information");
                    this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { -5, -6 });
                    return;
                }
                else
                {
                    if (fileSize > 0)
                    {
                        strResponse = webResponse.GetResponseStream();

                        if (File.Exists(DownloadFile))
                        {
                            strLocal = new FileStream(DownloadFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                        }
                        else
                        {
                            strLocal = new FileStream(DownloadFile, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);
                        }

                        int bytesSize = 0;
                        byte[] downBuffer = new byte[2048];

                        if (strLocal != null && strResponse != null && downBuffer != null)
                        {
                            while ((bytesSize = strResponse.Read(downBuffer, 0, downBuffer.Length)) > 0)
                            {
                                DownloadComplete = false;
                                if (strLocal != null && strResponse != null && downBuffer != null)
                                {
                                    strLocal.Write(downBuffer, 0, bytesSize);
                                    //Updating Progress Bar                  
                                    if (!CloseForm)
                                    {
                                        if (this.InvokeRequired)
                                        {
                                            if (strLocal.Length <= (Convert.ToInt32(fileSize) + filelen))
                                                this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { strLocal.Length, fileSize + filelen });
                                        }
                                        else
                                        {
                                            if (strLocal.Length <= (Convert.ToInt32(fileSize) + filelen))
                                                this.UpdateProgress(strLocal.Length, fileSize + filelen);
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The remote server returned an error: (403) Forbidden."))
                {
                    MessageBox.Show("Access To Server Forbidden", "Information");
                    this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { -6, -6 });
                }
                else if (ex.Message.Contains("The remote server returned an error: (416) Requested range not satisfiable."))
                {
                    this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { filelen, filelen });
                }
                else if (!ex.Message.Contains("Unable to read data from the transport connection: A blocking operation was interrupted by a call to WSACancelBlockingCall.")
                    && PrgrsBar_Download.Value != 100 && !ex.Message.Contains("Thread was being aborted."))
                {
                    MessageBox.Show(ex.Message);
                    this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { -6, -6 });
                    Common.LogError(ex);
                }
            }
            finally
            {
                if (RadBtn_DownloadConvert.Checked)
                {
                    StartConversion();
                }
            }
        }

        private void StartConversion()
        {
            try
            {
                if (PrgrsBar_Download.Value == 100 && DownloadComplete)
                {
                    VideoConversion(DownloadFile, SelectedFormat.Trim());
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        private void UpdateProgress(Int64 BytesRead, Int64 TotalBytes)
        {
            try
            {
                PercentProgress = 0;
                DownloadComplete = false;
                if (!DownloadCancel)
                {
                    if (TotalBytes >= BytesRead)
                    {
                        if (BytesRead > 0 && TotalBytes > 0)
                        {
                            PrgrsBar_Download.Style = ProgressBarStyle.Continuous;
                            PercentProgress = Convert.ToInt32((BytesRead * 100) / TotalBytes);
                            PrgrsBar_Download.Value = PercentProgress;
                            lblProgress.Text = "Downloaded : " + Common.ConvertBytesToMegabytes(BytesRead).ToString("0.00") +
                                " Mb(s)" + " out of " + Common.ConvertBytesToMegabytes(TotalBytes).ToString("0.00")
                                + " Mb(s)" + " (" + PercentProgress + "% Completed)";
                        }

                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            this.Text = PercentProgress + "% Completed";
                        }
                        else
                        {
                            this.Text = "Youtube Downloader";
                        }


                        if (BytesRead == -2 && TotalBytes == -2)
                        {
                            PrgrsBar_Download.Value = 0;
                            PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                            lblProgress.Text = "";
                            strLocal.Close();
                            strResponse.Close();
                            strLocal.Dispose();
                            strResponse.Dispose();
                            strLocal = null;
                            strResponse = null;
                            Btn_Download.Enabled = true;
                            DataGrd_VdoLst.Enabled = true;
                            label2.Visible = false;
                            LblResume.Visible = true;
                        }
                        else if (BytesRead == -6 && TotalBytes == -6)
                        {
                            PrgrsBar_Download.Value = 0;
                            PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                            lblProgress.Text = "";
                            strLocal.Close();
                            strResponse.Close();
                            strLocal.Dispose();
                            strResponse.Dispose();
                            strLocal = null;
                            strResponse = null;
                            Btn_Download.Enabled = true;
                            DataGrd_VdoLst.Enabled = true;
                            label2.Visible = false;
                            LblResume.Visible = false;
                        }
                        else if (BytesRead == -7 && TotalBytes == -7)
                        {
                            DownloadComplete = true;
                            MessageBox.Show("Download Complete", "Information");
                            BytesRead = -1;
                            TotalBytes = -1;
                            strLocal.Close();
                            strResponse.Close();
                            strLocal.Dispose();
                            strResponse.Dispose();
                            strResponse = null;
                            strLocal = null;
                            DataGrd_VdoLst.Enabled = true;
                            label2.Visible = true;
                            Btn_Download.Enabled = true;
                            LblResume.Visible = false;
                            PrgrsBar_Download.Value = 100;
                            PrgrsBar_Download.Style = ProgressBarStyle.Continuous;
                            //thrDownload.Abort();
                            Thread.CurrentThread.Join(100);
                        }
                        else if (BytesRead == -1 && TotalBytes == -1)
                        {
                            lblProgress.Text = "";
                            Btn_Download.Enabled = true;
                            PrgrsBar_Download.Value = 0;
                            PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                            DataGrd_VdoLst.Enabled = true;
                            label2.Visible = false;
                            LblResume.Visible = true;
                        }
                        else if (BytesRead == -4 && TotalBytes == -4)
                        {
                            lblProgress.Text = "";
                            Btn_Download.Enabled = true;
                            PrgrsBar_Download.Value = 0;
                            PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                            DataGrd_VdoLst.Enabled = true;
                            label2.Visible = false;
                            LblResume.Visible = false;
                        }
                        else if (BytesRead == -3 && TotalBytes == -3)
                        {
                            lblProgress.Text = "";
                            PrgrsBar_Download.Value = 0;
                            PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                            DataGrd_VdoLst.Enabled = false;
                            label2.Visible = false;
                            Btn_Download.Enabled = false;
                            LblResume.Visible = false;
                        }

                        else if (PercentProgress == 100)
                        {
                            DownloadComplete = true;
                            MessageBox.Show("Download Complete", "Information");
                            BytesRead = -1;
                            TotalBytes = -1;
                            strLocal.Close();
                            strResponse.Close();
                            strLocal.Dispose();
                            strResponse.Dispose();
                            strResponse = null;
                            strLocal = null;
                            DataGrd_VdoLst.Enabled = true;
                            label2.Visible = true;
                            Btn_Download.Enabled = true;
                            LblResume.Visible = false;
                            //thrDownload.Abort();                            
                            Thread.CurrentThread.Join(100);

                        }
                        else if (BytesRead == -8 && TotalBytes == -8)
                        {
                            lblProgress.Text = "Please Wait While Your Video is Being Converted";
                        }
                    }
                    else if (BytesRead == -5 && TotalBytes == -6)
                    {
                        strLocal.Close();
                        strResponse.Close();
                        strLocal.Dispose();
                        strResponse.Dispose();
                        strResponse = null;
                        strLocal = null;
                        Btn_Download.Enabled = true;
                        DataGrd_VdoLst.Enabled = true;
                        label2.Visible = false;
                        LblResume.Visible = false;
                    }
                }
                else
                {
                    PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                    if (BytesRead == -1 && TotalBytes == -1)
                    {
                        lblProgress.Text = "Downloading Paused";
                        strLocal.Close();
                        strResponse.Close();
                        strLocal.Dispose();
                        strResponse.Dispose();
                        strResponse = null;
                        strLocal = null;
                        Btn_Download.Enabled = true;
                        DataGrd_VdoLst.Enabled = true;
                        label2.Visible = true;
                        LblResume.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Stop.Enabled = false;
                DownloadCancel = true;
                webResponse.Close();
                webResponse = null;
                strResponse.Close();
                strResponse = null;
                strLocal.Close();
                strLocal = null;
                // Abort the thread that's downloading
                thrDownload.Abort();
                this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { -1, -1 });
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CloseForm = true;
                Directory.Delete(TempFolderPath, true);
                if (this.InvokeRequired)
                {
                    Application.Exit();
                }
                else
                {
                    strLocal = null;
                    Application.ExitThread();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                CloseForm = true;
                if (this.InvokeRequired)
                {
                    Application.Exit();
                }
                else
                {
                    strLocal = null;
                    Application.ExitThread();
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LblTitle.Text = "";
            lblProgress.Text = "";
            Txt_Url.Focus();

            SupportedFormats.Clear();
            SupportedFormats.Add("Select");
            //SupportedFormats.Add("Avi");
            SupportedFormats.Add("Mp3");
            //SupportedFormats.Add("Mp4");
            //SupportedFormats.Add("Mpeg");
            //SupportedFormats.Add("Wmv");

            Cmb_Formats.DataSource = SupportedFormats;
        }

        int SelectedRow = -1;

        private void DataGrd_VdoLst_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                SelectedRow = e.RowIndex;
                DownloadCancel = false;
                lblProgress.Text = "Download Starting.........";
                PrgrsBar_Download.Style = ProgressBarStyle.Marquee;
                DataGrd_VdoLst.Enabled = false;
                Btn_Stop.Enabled = true;
                thrDownload = new Thread(Download);
                thrDownload.Start();
            }
        }

        public void GetDownloadLink(string youttubehtml, out string Title)
        {
            Title = "";
            try
            {
                Uri videoUri = new Uri(youttubehtml);
                string videoID = HttpUtility.ParseQueryString(videoUri.Query).Get("v");
                string videoInfoUrl = videoUri.Scheme + "://" + videoUri.Host + "/get_video_info?video_id=" + videoID;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));

                string videoInfo = HttpUtility.HtmlDecode(reader.ReadToEnd());

                NameValueCollection videoParams = HttpUtility.ParseQueryString(videoInfo);

                if (videoParams["reason"] != null)
                {
                    MessageBox.Show(videoParams["reason"]);
                    return;
                }

                string[] videoURLs = videoParams["url_encoded_fmt_stream_map"].Split(',');
                Title = videoParams["title"];

                AvailableDownlinks.Clear();
                for (int j = 0; j < videoURLs.Length; j++)
                {
                    string TagValue = "";
                    string DownloadLinkLocation = "";
                    string[] w = videoURLs[j].Split('&');
                    for (int i = 0; i < w.Length; i++)
                    {
                        if (w[i].StartsWith("itag"))
                            TagValue = w[i].Split('=')[1];
                        else if (w[i].StartsWith("url"))
                            DownloadLinkLocation = w[i].Split('=')[1];

                        if (TagValue.Trim().Length > 0 && DownloadLinkLocation.Trim().Length > 0 && (DownloadLinkLocation.ToLower().StartsWith("http") || DownloadLinkLocation.ToLower().StartsWith("https")))
                        {
                            AvailableDownlinks.Add(new DownloadLinkInfo
                            {
                                Tag = TagValue,
                                DownloadLink = HttpUtility.UrlDecode(DownloadLinkLocation)
                            });
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            if (Txt_Url.Text.Length > 0)
            {
                if (process != null && TempFolderPath != null && TempFolderPath.Trim().Length > 0)
                {
                    process.Close();
                    process.Dispose();
                    if (Directory.Exists(TempFolderPath))
                        Directory.Delete(TempFolderPath, true);
                }
                DownloadFolderPath.Description = "Select Folder to Save File";
                if (DownloadFolderPath.ShowDialog() == DialogResult.OK)
                {
                    Btn_Download.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please Enter Video's Url in Textbox", "Information");
                Txt_Url.Focus();
            }
        }

        private void RadBtn_DownloadConvert_Click(object sender, EventArgs e)
        {
            RadBtn_Download.Checked = false;
            Cmb_Formats.Enabled = true;
        }

        private void RadBtn_Download_Click(object sender, EventArgs e)
        {
            RadBtn_DownloadConvert.Checked = false;
            Cmb_Formats.Enabled = false;
        }

        private void VideoConversion(string FileToConvert, string FileFormat)
        {
            try
            {
                //if (Extension.Trim().Length > 0 && !Extension.Trim().ToUpper().Contains("FLV"))
                {
                    TempFolderPath = GetDownloadLocation();
                    if (this.InvokeRequired)
                    {
                        this.Invoke(new UpdateProgessCallback(this.UpdateProgress), new object[] { -8, -8 });
                    }
                    else
                    {
                        lblProgress.Text = "Please Wait While Your Video is Being Converted";
                    }

                    string OutputFile = TempFolderPath + DateTime.Now.ToString("MMddyyyyhhmmss") + "." + FileFormat.ToLower();

                    string ffmpegpath = Application.StartupPath + @"\ffmpeg.exe";
                    if (File.Exists(ffmpegpath))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = ffmpegpath;
                        if (FileFormat.ToLower() != "mp3")
                            startInfo.Arguments = string.Format("-i {0} -sameq -r 32 -f " + FileFormat.ToLower() + "  {1}   ", FileToConvert, OutputFile);
                        else
                            startInfo.Arguments = string.Format("-i {0} -ab 256k  {1}   ", FileToConvert, OutputFile);

                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = true;
                        startInfo.RedirectStandardOutput = true;
                        startInfo.RedirectStandardInput = true;
                        startInfo.RedirectStandardError = true;
                        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process = new Process();

                        process.StartInfo = startInfo;
                        //Thread.CurrentThread.Start(process.Start());
                        process.Start();
                        process.PriorityClass = ProcessPriorityClass.High;

                        StreamReader OutputData = process.StandardOutput;
                        StreamWriter InputData = process.StandardInput;
                        StreamReader ErrorData = process.StandardError;

                        process.WaitForExit();

                        string DownloadLocation = DownloadFolderPath.SelectedPath + @"\" +
                            Common.RemoveCharaters(title) + "." + FileFormat;
                        if (File.Exists(OutputFile))
                        {
                            process.Close();
                            process.Dispose();
                            if (!File.Exists(DownloadLocation))
                            {
                                File.Move(OutputFile, DownloadLocation);
                                MessageBox.Show(string.Format("File Converted To : {0} Format", FileFormat), "Information");
                                File.Delete(FileToConvert);
                                Directory.Delete(TempFolderPath, true);
                            }
                            else
                            {
                                MessageBox.Show("File Already Exists", "Information");
                            }
                        }

                    }
                    else
                        MessageBox.Show("Conversion Application Not Found", "Information");
                }
                /*else
                {
                    MessageBox.Show("FLV File Format is not Suitable For Video Conversion.", "Information");
                }*/
            }
            catch (Exception ex)
            {
                Common.LogError(ex);
            }

        }

        private void Cmb_Formats_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedFormat = string.Empty;
            if (RadBtn_DownloadConvert.Checked)
            {
                if (Cmb_Formats.SelectedIndex > 0)
                {
                    SelectedFormat = Cmb_Formats.SelectedValue.ToString();
                }
            }
            else
                SelectedFormat = string.Empty;
        }

        private string GetDownloadLocation()
        {
            string DownloadLocation = string.Empty;

            if (RadBtn_DownloadConvert.Checked)
            {
                string SFolderpath = Environment.GetLogicalDrives()[0] + @"\temp\";
                //string SFolderpath = Path.GetTempPath() + @"\temp\";
                if (!Directory.Exists(SFolderpath))
                    Directory.CreateDirectory(SFolderpath);

                DownloadLocation = Path.GetFullPath(SFolderpath);
            }
            else if (RadBtn_Download.Checked)
            {
                DownloadLocation = DownloadFolderPath.SelectedPath + @"\";
            }
            return DownloadLocation;
        }
    }

    public class DownloadLinkInfo
    {
        public string Tag { get; set; }
        public string DownloadLink { get; set; }

        public DownloadLinkInfo()
        {
            Tag = "";
            DownloadLink = "";
        }
    }
}