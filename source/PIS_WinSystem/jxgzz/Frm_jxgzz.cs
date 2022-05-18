using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using AForge.Vision.Motion;
using DevComponents.DotNetBar;
using Greenshot;
using Greenshot.Drawing;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using Manina.Windows.Forms;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys.jxgzz
{
    public partial class Frm_jxgzz : DevComponents.DotNetBar.Office2007Form
    {

        private struct ColorComboBoxItem
        {
            public string Name;
            public PropertyInfo Field;

            public override string ToString()
            {
                return Name;
            }

            public ColorComboBoxItem(PropertyInfo field)
            {
                Name = field.Name;
                Field = field;
            }
        }




        Boolean thumsFlag = false;
        public Frm_jxgzz()
        {
            InitializeComponent();
            //热键操作
            HotkeyRepeatLimit = 1000;
            repeatLimitTimer = Stopwatch.StartNew();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            //
            this.HotkeyPress += new HotkeyEventHandler(Frm_Dtcj_HotkeyPress);
            //注册热键
            hotkey_bbxx = new HotkeyInfo(Keys.F4);
            if (hotkey_bbxx.Status == HotkeyStatus.Registered)
            {
                UnregisterHotkey(hotkey_bbxx);
            }
            if (hotkey_bbxx.Status != HotkeyStatus.Registered && hotkey_bbxx.IsValidHotkey)
            {
                RegisterHotkey(hotkey_bbxx);

                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {

                }
                else if (hotkey_bbxx.Status == HotkeyStatus.Failed)
                {

                }
            }
        }
        #region"快捷键操作"
        public bool IgnoreHotkeys = false;
        private void Frm_Dtcj_HotkeyPress(ushort id, Keys key, Modifiers modifier)
        {
            if (btn_PZ.Enabled == true)
            {
                if (!IgnoreHotkeys)
                {
                    if (this.Tag.Equals("show"))
                    {
                        switch (key)
                        {

                            case Keys.F4:
                                //添加标本信息快捷键
                                btn_PZ_Click(null, null);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                Frm_TJInfo("拍照提示", "请先打开摄像头开关！");
            }
        }
        //注册热键
        HotkeyInfo hotkey_bbxx;

        //移除热键
        private void Frm_Dtcj_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hotkey_bbxx != null)
            {
                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {
                    UnregisterHotkey(hotkey_bbxx);
                }
            }
        }


        #endregion

        #region "热键操作"
        public int HotkeyRepeatLimit { get; set; }

        public delegate void HotkeyEventHandler(ushort id, Keys key, Modifiers modifier);

        public event HotkeyEventHandler HotkeyPress;

        private Stopwatch repeatLimitTimer;

        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString("N");
        }
        public void RegisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null && hotkeyInfo.Status != HotkeyStatus.Registered)
            {
                if (!hotkeyInfo.IsValidHotkey)
                {
                    hotkeyInfo.Status = HotkeyStatus.NotConfigured;
                    return;
                }

                if (hotkeyInfo.ID == 0)
                {
                    string uniqueID = GetUniqueID();
                    hotkeyInfo.ID = NativeMethods.GlobalAddAtom(uniqueID);

                    if (hotkeyInfo.ID == 0)
                    {

                        hotkeyInfo.Status = HotkeyStatus.Failed;
                        return;
                    }
                }

                if (!NativeMethods.RegisterHotKey(Handle, hotkeyInfo.ID, (uint)hotkeyInfo.ModifiersEnum, (uint)hotkeyInfo.KeyCode))
                {
                    NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);

                    hotkeyInfo.ID = 0;
                    hotkeyInfo.Status = HotkeyStatus.Failed;
                    return;
                }

                hotkeyInfo.Status = HotkeyStatus.Registered;
            }
        }

        public bool UnregisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null)
            {
                if (hotkeyInfo.ID > 0)
                {
                    bool result = NativeMethods.UnregisterHotKey(Handle, hotkeyInfo.ID);

                    if (result)
                    {
                        NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);
                        hotkeyInfo.ID = 0;
                        hotkeyInfo.Status = HotkeyStatus.NotConfigured;
                        return true;
                    }
                }

                hotkeyInfo.Status = HotkeyStatus.Failed;
            }

            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowsMessages.HOTKEY && CheckRepeatLimitTime())
            {
                ushort id = (ushort)m.WParam;
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                Modifiers modifier = (Modifiers)((int)m.LParam & 0xFFFF);
                OnKeyPressed(id, key, modifier);
                return;
            }

            base.WndProc(ref m);
        }

        protected void OnKeyPressed(ushort id, Keys key, Modifiers modifier)
        {
            if (HotkeyPress != null)
            {
                HotkeyPress(id, key, modifier);
            }
        }

        private bool CheckRepeatLimitTime()
        {
            if (HotkeyRepeatLimit > 0)
            {
                if (repeatLimitTimer.ElapsedMilliseconds >= HotkeyRepeatLimit)
                {
                    repeatLimitTimer.Reset();
                    repeatLimitTimer.Start();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion


        public string Cap_BLH = "";
        public string Cap_XM = "";
        public string Cap_Sqdh = "";
        private void LoadVidoSource()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();
                foreach (FilterInfo device in videoDevices)
                {
                    devicesCombo.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                devicesCombo.Items.Add("不存在视频设备");
                devicesCombo.Enabled = false;
            }

            devicesCombo.SelectedIndex = 0;

        }
        // 定义调用方法的委托
        delegate void FunDelegateStart();
        void OnDownLoadComplete()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegateStart funDelegate = new FunDelegateStart(ftpDownload1_DownLoadComplete);
                this.BeginInvoke(funDelegate);
            }
        }
        private void ftpDownload1_DownLoadComplete()
        {
            ftpDownload1.Visible = false;
            RefreshMultiList();
        }
        delegate void FunFtpError(string errorinfo);
        void OnDownLoadError(string errorinfo)
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunFtpError funDelegate = new FunFtpError(ftpDownload1_DownLoadError);
                this.BeginInvoke(funDelegate, new object[] { errorinfo });
            }
        }
        private void ftpDownload1_DownLoadError(string errorinfo)
        {
            Program.FileLog.Error(errorinfo);
            ftpDownload1.Visible = false;

            ftpDownload1.SetOver();
        }



        //1视频路径
        string VideoFolder = "";
        //2图像路径
        string ImgFolder = "";

        private void Frm_Dtcj_Load(object sender, EventArgs e)
        {

            btn_closecj.Enabled = false;
            btn_PZ.Enabled = false;
            btn_LX.Enabled = false;
            btn_lxstop.Enabled = false;


            this.txt_pz_blh.Text = Cap_BLH;
            this.txt_pz_xm.Text = Cap_XM;
            //加载所有视频源
            LoadVidoSource();
            //缩略图不显示滚动条
            imageListView1.ScrollBars = false;

            //
            ftpDownload1.Visible = false;

            ftpDownload1.DownLoadComplete += new FtpDownloader.FtpDownload.DownLoadCompleteEventHandler(OnDownLoadComplete);
            ftpDownload1.DownLoadError += new FtpDownloader.FtpDownload.DownLoadErrorEventHandler(OnDownLoadError);

            foreach (FilterInfo device in videoDevices)
            {
                if (device.Name.Equals(PIS_Sys.Properties.Settings.Default.DTDevice))
                {
                    devicesCombo.Text = PIS_Sys.Properties.Settings.Default.DTDevice;
                    break;
                }
            }
            if (PIS_Sys.Properties.Settings.Default.Open_DT_SXT == true)
            {
                //打开视频设备
                buttonItem13_Click_1(null, null);
            }
            //加载图像列表
            Type colorType = typeof(ImageListViewColor);
            int i = 0;
            foreach (PropertyInfo field in colorType.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                colorToolStripComboBox.Items.Add(new ColorComboBoxItem(field));
                if (field.Name == "Default")
                    colorToolStripComboBox.SelectedIndex = i;
                i++;
            }
            //1视频路径
            VideoFolder = Program.APPdirPath + @"\Pis_Video\" + Cap_BLH;
            if (Directory.Exists(VideoFolder) == false)
            {
                Directory.CreateDirectory(VideoFolder);
            }
            //2图像路径
            ImgFolder = Program.APPdirPath + @"\Pis_Image\" + Cap_BLH;
            if (Directory.Exists(ImgFolder) == false)
            {
                Directory.CreateDirectory(ImgFolder);
            }

            if (PIS_Sys.Properties.Settings.Default.DTthumsWidth == 0 || PIS_Sys.Properties.Settings.Default.DTthumsHeight == 0)
            {
                thumsFlag = true;
            }
            else
            {
                thumsFlag = false;
                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
            }

            //刷新多媒体列表
            RefreshMultiList();
        }
        private void OpenVideoSource(IVideoSource source)
        {
            // 设置忙
            this.Cursor = Cursors.WaitCursor;

            // 关闭之前的视频源
            CloseVideoSource();

            // 开始新的视频源
            videoSourcePlayer1.VideoSource = new AsyncVideoSource(source);
            videoSourcePlayer1.Start();
            videoSource = source;
            this.Cursor = Cursors.Default;
        }
        // 关闭先前的视频源
        private void CloseVideoSource()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // 停止当前视频源
                videoSourcePlayer1.SignalToStop();
                // 等待视频源关闭
                for (int i = 0; (i < 50) && (videoSourcePlayer1.IsRunning); i++)
                {
                    Thread.Sleep(100);
                }
                if (videoSourcePlayer1.IsRunning)
                    videoSourcePlayer1.Stop();
                //videoSourcePlayer1.BorderColor = Color.Black;
                this.Cursor = Cursors.Default;
            }
            catch
            {

            }
        }
        //连接相机
        private void buttonItem13_Click_1(object sender, EventArgs e)
        {
            if (devicesCombo.Enabled == false)
            {
                return;
            }
            try
            {
                device = videoDevices[devicesCombo.SelectedIndex].MonikerString;
                //创建视频源
                VideoCaptureDevice videoSource = new VideoCaptureDevice(device);
                OpenVideoSource(videoSource);
                btn_closecj.Enabled = true;
                btn_PZ.Enabled = true;
                btn_LX.Enabled = true;
            }
            catch { }
        }
        //相机属性设置
        private void buttonItem14_Click_1(object sender, EventArgs e)
        {
            if ((videoSource != null) && (videoSource is VideoCaptureDevice))
            {
                try
                {
                    ((VideoCaptureDevice)videoSource).DisplayPropertyPage(this.Handle);
                }
                catch (NotSupportedException ex)
                {
                    Frm_TJInfo("错误", ex.Message);
                }
            }
        }
        //显示十字聚焦
        private void buttonItem15_Click_1(object sender, EventArgs e)
        {
            if ((videoSource != null) && (videoSource is VideoCaptureDevice) && (videoSource.IsRunning))
            {
                try
                {
                    ((VideoCaptureDevice)videoSource).DisplayCrossbarPropertyPage(this.Handle);
                }
                catch (NotSupportedException ex)
                {
                    Frm_TJInfo("错误", ex.Message);
                }
            }
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }

        private void Frm_Dtcj_FormClosing(object sender, FormClosingEventArgs e)
        {
            //结束下载
            ftpDownload1.SetOver();
            // 关闭之前的视频源
            CloseVideoSource();
            StopSaving();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //停止视频
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            StopSaving();
            // 关闭之前的视频源
            CloseVideoSource();
            btn_closecj.Enabled = false;
            btn_PZ.Enabled = false;
            btn_LX.Enabled = false;
        }

        // 真正需要执行的方法
        private void FunStart()
        {

            btn_lxstop.Enabled = true;
            btn_LX.Enabled = false;
        }
        void OnRecordingStart()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegateStart funDelegate = new FunDelegateStart(FunStart);
                this.BeginInvoke(funDelegate);
            }
        }


        // 定义调用方法的委托
        delegate void FunDelegateThumsSize(int width, int height);
        // 真正需要执行的方法
        private void FunThumsSize(int width, int height)
        {
            imageListView1.ThumbnailSize = new Size(width, height);

        }
        void OnThumsSizeChange(int width, int height)
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegateThumsSize funDelegate = new FunDelegateThumsSize(FunThumsSize);
                this.BeginInvoke(funDelegate, new object[] { width, height });
            }
        }




        //开始录像
        private void btn_ckhd_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer1.IsRunning)
                {
                    StartSaving();
                }
            }
            catch
            {

            }
        }

        // 定义调用方法的委托
        delegate void FunRecordStop(string VideoPath);
        // 真正需要执行的方法
        private void FunStop(string videopath)
        {
            StopwatchTimer.Stop();
            btn_lxstop.Enabled = false;
            btn_LX.Enabled = true;
            VideoTimeProgress.Text = "";
            //加载视频列表
            ListViewItem lvi = new ListViewItem();
            lvi.Text = "视频";
            int index = videopath.LastIndexOf(@"\");
            string filename = videopath.Substring(index + 1);
            lvi.SubItems.Add(filename);
            string filepath = videopath.Substring(0, index);
            lvi.SubItems.Add(filepath);
            this.listViewEx1.Items.Add(lvi);

        }
        void OnRecordStop(string videopath)
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunRecordStop funDelegate = new FunRecordStop(FunStop);
                this.BeginInvoke(funDelegate, new object[] { videopath });
            }
        }


        //结束录像
        private void btn_lxstop_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer1.IsRunning)
                {

                    StopSaving();

                }
            }
            catch
            {

            }
        }
        FilterInfoCollection videoDevices;
        private string device;
        // opened video source
        private IVideoSource videoSource = null;
        // motion detector
        MotionDetector detector = new MotionDetector(new TwoFramesDifferenceDetector(), null);
        public VideoCodec Codec = VideoCodec.MPEG4;
        public ConcurrentQueue<Helper.FrameAction> Buffer = new ConcurrentQueue<Helper.FrameAction>();
        private bool _firstFrame = true;
        private Thread _recordingThread;
        private readonly object _lockobject = new object();
        private DateTime _recordingStartTime;
        private readonly ManualResetEvent _stopWrite = new ManualResetEvent(false);
        public string VideoFileName = "";
        private VideoFileWriter _writer;
        private int _videoWidth, _videoHeight;
        //是否正在录像
        public bool Recording
        {
            get
            {
                try
                {
                    return _recordingThread != null && !_recordingThread.Join(TimeSpan.Zero);
                }
                catch
                {

                }
                return false;

            }
        }

        //开始录像
        public void StartSaving()
        {

            if (Recording)
                return;

            lock (_lockobject)
            {
                if (Recording)
                    return;

                if (!Recording)
                {
                    while (Buffer.Count > 0)
                    {
                        Helper.FrameAction fa;
                        if (Buffer.TryPeek(out fa))
                        {
                            if (Buffer.TryDequeue(out fa))
                                fa.Nullify();
                        }
                    }
                }
                _recordingStartTime = DateTime.UtcNow;
                _recordingThread = new Thread(Record)
                {
                    Name = "RecordingThread0",
                    IsBackground = false,
                    Priority = ThreadPriority.Normal
                };
                _recordingThread.Start();
            }
        }

        private unsafe void WriteFrame(Helper.FrameAction fa, DateTime recordingStart, ref long lastvideopts, ref double maxAlarm,
          ref Helper.FrameAction? peakFrame, ref long lastaudiopts)
        {
            switch (fa.FrameType)
            {
                case Enums.FrameType.Video:
                    using (var ms = new MemoryStream(fa.Content))
                    {
                        using (var bmp = (Bitmap)Image.FromStream(ms))
                        {
                            var pts = (long)(fa.TimeStamp - recordingStart).TotalMilliseconds;
                            if (pts >= lastvideopts)
                            {
                                _writer.WriteVideoFrame(ResizeBitmap(bmp));
                                lastvideopts = pts;
                            }
                        }
                        if (fa.Level > maxAlarm || peakFrame == null)
                        {
                            maxAlarm = fa.Level;
                            peakFrame = fa;
                        }
                        ms.Close();
                    }
                    break;
            }
            fa.Nullify();
        }

        private Bitmap ResizeBitmap(Bitmap frame)
        {

            if (frame.Width == _videoWidth && frame.Height == _videoHeight)
                return frame;

            var b = new Bitmap(_videoWidth, _videoHeight);
            var r = new Rectangle(0, 0, _videoWidth, _videoHeight);
            using (var g = Graphics.FromImage(b))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(frame, r);
            }

            frame.Dispose();
            frame = null;
            return b;
        }
        private static int CalcBitRate(int q)
        {
            return 8000 * (Convert.ToInt32(Math.Pow(2, (q - 1))));
        }


        [HandleProcessCorruptedStateExceptions]
        private void Record()
        {

            _stopWrite.Reset();

            VideoFileName = "Video_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string videopath = VideoFolder + @"\" + VideoFileName + ".avi";
            string thumbspath = VideoFolder + @"\" + VideoFileName + ".jpg";

            bool error = false;
            double maxAlarm = 0;
            long lastvideopts = -1, lastaudiopts = -1;
            DateTime recordingStart = Helper.Now;
            try
            {
                try
                {
                    Program.FFMPEGMutex.WaitOne();
                    _writer = new VideoFileWriter();
                    _writer.Open(videopath, _videoWidth, _videoHeight, 20, VideoCodec.MPEG4);
                    OnRecordingStart();
                }
                finally
                {
                    try
                    {
                        Program.FFMPEGMutex.ReleaseMutex();

                    }
                    catch (ObjectDisposedException)
                    {
                        //can happen on shutdown
                    }
                }


                Helper.FrameAction? peakFrame = null;
                bool first = true;

                while (!_stopWrite.WaitOne(5))
                {
                    Helper.FrameAction fa;
                    if (Buffer.TryDequeue(out fa))
                    {
                        if (first)
                        {
                            StopwatchTimer.Reset();
                            StopwatchTimer.Start();
                            recordingStart = fa.TimeStamp;
                            first = false;
                        }

                        WriteFrame(fa, recordingStart, ref lastvideopts, ref maxAlarm, ref peakFrame,
                            ref lastaudiopts);
                        //计算时间显示
                        OnRecordingOneFrame();
                    }

                }
                //录像结束！加载视频列表
                OnRecordStop(videopath);

                //if (peakFrame != null && peakFrame.Value.Content != null)
                //{
                //    try
                //    {
                //        using (var ms = new MemoryStream(peakFrame.Value.Content))
                //        {
                //            using (var bmp = (Bitmap)Image.FromStream(ms))
                //            {
                //                //Image.GetThumbnailImageAbort myCallback = ThumbnailCallback;
                //                //using (var myThumbnail = bmp.GetThumbnailImage(200, 200, myCallback, IntPtr.Zero))
                //                //{
                //                //    myThumbnail.Save(thumbspath,GetFEncoder,GetEncoderParameters());
                //                //}
                //                if (thumsFlag)
                //                {
                //                    thumsFlag = false;
                //                    PIS_Sys.Properties.Settings.Default.DTthumsWidth = bmp.Width/10;
                //                    PIS_Sys.Properties.Settings.Default.DTthumsHeight = bmp.Height/10;
                //                    PIS_Sys.Properties.Settings.Default.Save();
                //                    thumsWidth = PIS_Sys.Properties.Settings.Default.DTthumsWidth;
                //                    thumsHeight = PIS_Sys.Properties.Settings.Default.DTthumsHeight;
                //                    //改变缩略图大小
                //                    OnThumsSizeChange(thumsWidth, thumsHeight);
                //                }
                //                bmp.Save(thumbspath);
                //                if (bmp != null)
                //                {
                //                    bmp.Dispose();
                //                }

                //            }
                //            ms.Close();
                //        }

                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}
            }
            catch
            {
                error = true;

            }
            finally
            {
                if (_writer != null && _writer.IsOpen)
                {
                    try
                    {
                        Program.FFMPEGMutex.WaitOne();
                        _writer.Dispose();
                    }
                    catch
                    {

                    }
                    finally
                    {
                        try
                        {
                            Program.FFMPEGMutex.ReleaseMutex();
                        }
                        catch (ObjectDisposedException)
                        {
                            //can happen on shutdown
                        }
                    }

                    _writer = null;
                }

            }
            if (_firstFrame)
            {
                error = true;
            }
            if (error)
            {
                try
                {
                    if (File.Exists(videopath))
                        FileOperations.Delete(videopath);
                }
                catch
                {
                }

                ClearBuffer();


            }
        }
        // 定义调用方法的委托
        delegate void FunDelegate();
        // 真正需要执行的方法
        private void Fun()
        {
            VideoTimeProgress.Text = string.Format("已录像:{0}秒", (StopwatchTimer.ElapsedMilliseconds / 1000).ToString());
        }
        void OnRecordingOneFrame()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegate funDelegate = new FunDelegate(Fun);
                // this.BeginInvoke(被调用的方法的委托，要传递的参数[Object数组])
                this.BeginInvoke(funDelegate);
            }

        }


        private void ClearBuffer()
        {
            lock (_lockobject)
            {
                Helper.FrameAction fa;
                while (Buffer.TryDequeue(out fa))
                {
                    fa.Nullify();
                }
            }

        }
        private void StopSaving()
        {
            if (Recording)
            {
                _stopWrite.Set();
                try
                {
                    if (!_recordingThread.Join(TimeSpan.Zero))
                        _recordingThread.Join();
                }
                catch { }
                _firstFrame = true;
                btn_LX.Text = "开始录像";
            }
        }


        private static bool ThumbnailCallback()
        {
            return false;
        }
        public static ImageCodecInfo GetFEncoder
        {
            get
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == ImageFormat.Jpeg.Guid)
                    {
                        return codec;
                    }
                }
                return null;
            }
        }

        public static EncoderParameters GetEncoderParameters()
        {
            EncoderParameters EncoderParams;
            EncoderParams = new EncoderParameters(1);
            EncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L);
            return EncoderParams;
        }

        public static string FtpIp = Program.FtpIP;
        public static int FtpPort = Program.FtpPort;
        public static string FtpUser = Program.FtpUser;
        public static string FtpPwd = Program.FtpPwd;
        //拍照
        private void btn_PZ_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer1.IsRunning)
                {
                    //1.拍照
                    Bitmap bmps1 = videoSourcePlayer1.GetCurrentVideoFrame();
                    if (bmps1 != null)
                    {
                        if (thumsFlag)
                        {
                            thumsFlag = false;
                            PIS_Sys.Properties.Settings.Default.DTthumsWidth = bmps1.Width / 10;
                            PIS_Sys.Properties.Settings.Default.DTthumsHeight = bmps1.Height / 10;
                            PIS_Sys.Properties.Settings.Default.Save();
                            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
                        }
                        //只有当有病理号时，才可以进行上传及保存和展示
                        if (!txt_pz_blh.Text.Trim().Equals(""))
                        {
                            string filename = string.Format("DT_{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                            string strFileName = string.Format(@"{0}\{1}.jpg", ImgFolder, filename);
                            string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Cap_BLH, filename);
                            bmps1.Save(strFileName);
                            //2.上传 VBProcess.ftpUpload(strFileName, remoteFile,"127.0.0.1", "21", "peerct", "125353Ct")
                            if (FtpUpload(@remoteFile, strFileName))
                            {
                                DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                                if (insMulti.InsertData(txt_pz_blh.Text.Trim(), remoteFile, string.Format("{0}.jpg", filename), 1) == 1)
                                {
                                    //3.展示
                                    ImageListViewItem ins = new ImageListViewItem();
                                    ins.Tag = strFileName;
                                    ins.Text = string.Format("QC_{0}", imageListView1.Items.Count.ToString());
                                    if (toolStripLabel3.Text.Equals(""))
                                    {
                                        toolStripLabel3.Text = string.Format("({0}:{1})", bmps1.Width.ToString(), bmps1.Height.ToString());
                                    }
                                    ins.FileName = strFileName;
                                    imageListView1.Items.Insert(0, ins);
                                }
                            }
                        }
                        else
                        {
                            Frm_TJInfo("提示", "病理号不能为空\n请选择一个病人！");
                        }
                    }
                    if (bmps1 != null)
                    {
                        bmps1.Dispose();
                    }
                }

            }
            catch
            {
            }

        }

        //上传文件
        public static Boolean FtpUpload(string ftpPath, string localFile)
        {
            //检查目录是否存在，不存在创建
            FtpCheckDirectoryExist(ftpPath);
            FileInfo fi = new FileInfo(localFile);
            FileStream fs = fi.OpenRead();
            long length = fs.Length;
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(string.Format(@"ftp://{0}:{1}/", FtpIp, FtpPort) + ftpPath);
            req.Credentials = new NetworkCredential(FtpUser, FtpPwd);
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.ContentLength = length;
            req.Timeout = 10 * 1000;
            try
            {
                Stream stream = req.GetRequestStream();
                int BufferLength = 2048; //2K   
                byte[] b = new byte[BufferLength];
                int i;
                while ((i = fs.Read(b, 0, BufferLength)) > 0)
                {
                    stream.Write(b, 0, i);
                }
                stream.Close();
                stream.Dispose();
            }
            catch
            {
                return false;
            }
            finally
            {
                fs.Close();
                req.Abort();
            }
            req.Abort();
            return true;
        }

        //判断文件的目录是否存,不存则创建
        public static void FtpCheckDirectoryExist(string destFilePath)
        {
            string fullDir = FtpParseDirectory(destFilePath);
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];
                //如果是以/开始的路径,第一个为空  
                if (dir != null && dir.Length > 0)
                {
                    try
                    {
                        curDir += dir + "/";
                        FtpMakeDir(curDir);
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        public static string FtpParseDirectory(string destFilePath)
        {
            return destFilePath.Substring(0, destFilePath.LastIndexOf("/"));
        }

        //创建目录
        public static Boolean FtpMakeDir(string curDir)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(string.Format(@"ftp://{0}:{1}/", FtpIp, FtpPort) + curDir);
            req.Credentials = new NetworkCredential(FtpUser, FtpPwd);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }

        private void thumbnailsToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Thumbnails;
        }

        private void galleryToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Gallery;
        }

        private void paneToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Pane;
        }
        private void clearThumbsToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.ClearThumbnailCache();
        }

        private void x96ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
        }

        private void x120ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth * 2, PIS_Sys.Properties.Settings.Default.DTthumsHeight * 2);
        }

        private void x200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth * 3, PIS_Sys.Properties.Settings.Default.DTthumsHeight * 3);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            imageListView1.ScrollBars = !imageListView1.ScrollBars;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyInfo field = ((ColorComboBoxItem)colorToolStripComboBox.SelectedItem).Field;
            ImageListViewColor color = (ImageListViewColor)field.GetValue(null, null);
            imageListView1.Colors = color;
        }

        //private float motionAlarmLevel = 0.015f;
        Stopwatch StopwatchTimer = new Stopwatch();//录像计算时间
        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            if (Recording)
            {
                if (_firstFrame)
                {
                    _videoWidth = videoSourcePlayer1.Width;
                    _videoHeight = videoSourcePlayer1.Height;
                    if ((_videoWidth & 1) != 0)
                    {
                        _videoWidth = _videoWidth - 1;
                    }
                    if ((_videoHeight & 1) != 0)
                    {
                        _videoHeight = _videoHeight - 1;
                    }

                    _firstFrame = false;
                }

                lock (this)
                {

                    float motionLevel = 0;
                    //if (detector != null)
                    //{
                    //    motionLevel = detector.ProcessFrame(image);

                    //    if (motionLevel > motionAlarmLevel)
                    //    {

                    //    }
                    //}
                    Buffer.Enqueue(new Helper.FrameAction(image, motionLevel, Helper.Now));
                }
            }
        }

        private void 重置缩略图大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qcgl.FrmSetThums ins = new qcgl.FrmSetThums();
            ins.BringToFront();
            ins.Owner = this;
            if (ins.ShowDialog() == DialogResult.OK)
            {
                thumsFlag = false;
                PIS_Sys.Properties.Settings.Default.DTthumsWidth = ins.ThWidth;
                PIS_Sys.Properties.Settings.Default.DTthumsHeight = ins.ThHeigth;
                PIS_Sys.Properties.Settings.Default.Save();
                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
            }
        }

        //刷新本地视频音频和图像
        public void RefreshMultiList()
        {
            if (!this.txt_pz_blh.Text.Trim().Equals(""))
            {
                listViewEx1.Items.Clear();

                //视频
                string OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Video\" + txt_pz_blh.Text.Trim());
                foreach (var file in Directory.GetFiles(OutputFolder, "*.avi"))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "视频";
                    int index = file.LastIndexOf(@"\");
                    string filename = file.Substring(index + 1);
                    lvi.SubItems.Add(filename);
                    string filepath = file.Substring(0, index);
                    lvi.SubItems.Add(filepath);
                    this.listViewEx1.Items.Add(lvi);

                }
                //图像
                imageListView1.Items.Clear();
                int thumsWidth = PIS_Sys.Properties.Settings.Default.DTthumsWidth;
                int thumsHeight = PIS_Sys.Properties.Settings.Default.DTthumsHeight;
                imageListView1.ThumbnailSize = new Size(thumsWidth, thumsHeight);
                OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Image\" + txt_pz_blh.Text.Trim());
                imageListView1.SuspendLayout();
                foreach (var file in Directory.GetFiles(OutputFolder, "*.jpg"))
                {
                    ImageListViewItem ins = new ImageListViewItem();
                    ins.Tag = "image|" + file;
                    ins.Text = "QC_" + imageListView1.Items.Count.ToString();
                    ins.FileName = file;
                    if (toolStripLabel3.Text.Equals(""))
                    {
                        Image img = GetImage(file);
                        toolStripLabel3.Text = string.Format("({0}:{1})", img.Width.ToString(), img.Height.ToString());
                        if (thumsFlag == true)
                        {
                            thumsFlag = false;
                            PIS_Sys.Properties.Settings.Default.DTthumsWidth = img.Width / 10;
                            PIS_Sys.Properties.Settings.Default.DTthumsHeight = img.Height / 10;
                            PIS_Sys.Properties.Settings.Default.Save();
                            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
                        }
                    }
                    imageListView1.Items.Add(ins);
                }
                imageListView1.ResumeLayout();

            }

        }
        public Image GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
        }
        //播放视频
        private void btn_playVideo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewEx1.SelectedIndices.Count > 0)
                {
                    if (listViewEx1.SelectedIndices != null && listViewEx1.SelectedIndices.Count > 0)
                    {
                        ListView.SelectedIndexCollection c = listViewEx1.SelectedIndices;
                        string filename = listViewEx1.Items[c[0]].SubItems[1].Text;// 表示选中行的第二列
                        string filepath = listViewEx1.Items[c[0]].SubItems[2].Text;// 表示选中行的第二列
                        string filePathstr = Path.Combine(filepath, filename);
                        if (System.IO.File.Exists(filePathstr))
                        {
                            Process.Start(filePathstr);
                        }
                        else
                        {
                            Frm_TJInfo("提示", "文件不存在！");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Frm_TJInfo("播放错误", ex.ToString());
            }
        }
        //编辑图片
        private void imageListView1_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            if (System.IO.File.Exists(e.Item.FileName))
            {
                AnnotateImage(e.Item.FileName, e.Item.FilePath);
            }
        }
        public static Image LoadImage(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && IsImageFile(filePath) && File.Exists(filePath))
                {
                    return Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
                }
            }
            catch
            {

            }

            return null;
        }
        public static bool IsImageFile(string filePath)
        {
            return IsValidFile(filePath, typeof(ImageFileExtensions));
        }
        private static bool IsValidFile(string filePath, Type enumType)
        {
            string ext = GetFilenameExtension(filePath);

            if (!string.IsNullOrEmpty(ext))
            {
                return Enum.GetNames(enumType).Any(x => ext.Equals(x, StringComparison.InvariantCultureIgnoreCase));
            }

            return false;
        }
        public static string GetFilenameExtension(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                int pos = filePath.LastIndexOf('.');
                if (pos >= 0)
                {
                    return filePath.Substring(pos + 1).ToLowerInvariant();
                }
            }
            return null;
        }

        public static void AnnotateImage(string imgPath, string pathImage)
        {
            if (!IniConfig.isInitialized)
            {
                IniConfig.AllowSave = true;
                IniConfig.Init(@pathImage);
            }

            using (Image cloneImage = LoadImage(imgPath))
            using (ICapture capture = new Capture { Image = cloneImage })
            using (Surface surface = new Surface(capture))
            using (ImageEditorForm editor = new ImageEditorForm(surface, true))
            {
                editor.IsTaskWork = false;
                editor.SetImagePath(imgPath);
                DialogResult result = editor.ShowDialog();

                if (result == DialogResult.OK && editor.IsTaskWork)
                {

                }


            }
        }
        //双击采集图像
        private void videoSourcePlayer1_DoubleClick(object sender, EventArgs e)
        {
            //添加标本信息快捷键
            btn_PZ_Click(null, null);
        }



    }
    public enum ImageFileExtensions
    {
        [Description("Joint Photographic Experts Group")]
        jpg,
        jpeg,
        [Description("Portable Network Graphic")]
        png,
        [Description("CompuServe's Graphics Interchange Format")]
        gif,
        [Description("Microsoft Windows Bitmap formatted image")]
        bmp,
        [Description("File format used for icons in Microsoft Windows")]
        ico,
        [Description("Tagged Image File Format")]
        tif,
        tiff
    }
}
