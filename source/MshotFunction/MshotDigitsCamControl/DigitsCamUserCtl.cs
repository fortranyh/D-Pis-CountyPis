using DVPCameraType;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MshotDigitsCamControl
{
    public partial class DigitsCamUserCtl : UserControl
    {
        public uint m_handle = 0;
        public int m_n_dev_count = 0;
        string m_strFriendlyName;
        public static IntPtr m_ptr_wnd = new IntPtr();
        public static IntPtr m_ptr = new IntPtr();
        public static bool m_b_start = false;
        public DigitsCamUserCtl()
        {
            InitializeComponent();
            // update control style
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer | ControlStyles.UserPaint, true);
            this.HandleCreated += DigitsCamUserCtl_HandleCreated;
        }
        // Check if the control is accessed from a none UI thread
        private void CheckForCrossThreadAccess()
        {
            // force handle creation, so InvokeRequired() will use it instead of searching through parent's chain
            if (!IsHandleCreated)
            {
                CreateControl();

                // if the control is not Visible, then CreateControl() will not be enough
                if (!IsHandleCreated)
                {
                    CreateHandle();
                }
            }

            if (InvokeRequired)
            {
                throw new InvalidOperationException("Cross thread access to the control is not allowed.");
            }
        }
        private void DigitsCamUserCtl_HandleCreated(object sender, EventArgs e)
        {
            CheckForCrossThreadAccess();
            m_ptr_wnd = pictureBoxCam.Handle;
            InitDevList();
            this.Refresh();
        }
        public void ShowPanelControl()
        {
            panelControl.Visible = true;
        }
        public void HidePanelControl()
        {
            panelControl.Visible = false;
        }
        //析构函数释放摄像头资源
        public void DigitsCamClosed()
        {
            if (IsValidHandle(m_handle))
            {
                DVPCamera.dvpStop(m_handle);
                m_b_start = false;
                DVPCamera.dvpStreamCallback pf = new DVPCamera.dvpStreamCallback(_dvpStreamCallback);
                dvpStatus s = DVPCamera.dvpUnregisterStreamCallback(m_handle, pf, dvpStreamEvent.STREAM_EVENT_PROCESSED, m_ptr);
                DVPCamera.dvpClose(m_handle);
                m_handle = 0;
                pictureBoxCam.Invalidate();
                Application.DoEvents();
            }
            UpdateControls();
        }
        public bool IsValidHandle(uint handle)
        {
            bool bValidHandle = false;
            dvpStatus status = DVPCamera.dvpIsValid(handle, ref bValidHandle);
            return bValidHandle;
        }

        private void BUTTON_SCAN_Click(object sender, EventArgs e)
        {
            InitDevList();
        }

        //初始化列表
        public void InitDevList()
        {
            dvpStatus status;
            uint i, n = 0;
            dvpCameraInfo dev_info = new dvpCameraInfo();

            // 此时，n为成功枚举到的相机数量，将添加到下拉列表中，下拉列表中的内容为每个相机的FriendlyName
            COMBO_DEVICES.Items.Clear();


            // 获得当前能连接的相机数量
            status = DVPCamera.dvpRefresh(ref n);
            m_n_dev_count = (int)n;
            if (status == dvpStatus.DVP_STATUS_OK)
            {
                for (i = 0; i < n; i++)
                {
                    // 逐个枚举出每个相机的信息
                    status = DVPCamera.dvpEnum(i, ref dev_info);

                    if (status == dvpStatus.DVP_STATUS_OK)
                    {
                        // 界面使用的是UNICODE，枚举的设备信息为ANSI字符串，需要将ANSI转UNICODE
                        int item = COMBO_DEVICES.Items.Add(dev_info.FriendlyName);
                        if (item == 0)
                        {
                            COMBO_DEVICES.SelectedIndex = item;
                        }
                    }
                }
            }
            if (n == 0)
            {
                BUTTON_OPEN.Enabled = false;
            }
            else
            {
                BUTTON_OPEN.Enabled = true;
            }

            if (!IsValidHandle(m_handle))
            {
                BUTTON_START.Enabled = false;
                BUTTON_PROPERTY.Enabled = false;
                BUTTON_SAVE.Enabled = false;
                BUTTON_CAPImg.Enabled = false;
            }
        }

        private DVPCamera.dvpStreamCallback _proc;

        // 数据接收回调函数
        public static int _dvpStreamCallback(uint handle, dvpStreamEvent _event, IntPtr pContext, ref dvpFrame refFrame, IntPtr pBuffer)
        {
            RECT rt = new RECT();
            rt.Bottom = 100;
            rt.Left = 0;
            rt.Top = 0;
            rt.Right = 100;
            // 刷新显示
            dvpStatus s = DVPCamera.dvpDrawPicture(ref refFrame, pBuffer, m_ptr_wnd, m_ptr, m_ptr);
            //Debug.Assert(s == dvpStatus.DVP_STATUS_OK, "Draw pictures fail!");
            return 1;
        }

        private void BUTTON_OPEN_Click(object sender, EventArgs e)
        {
            OpenCamera();
        }
        public void OpenCamera()
        {
            if (!IsValidHandle(m_handle))
            {
                uint i = (uint)COMBO_DEVICES.SelectedIndex;
                dvpStatus status = DVPCamera.dvpOpen(i, dvpOpenMode.OPEN_NORMAL, ref m_handle);
                if (status != dvpStatus.DVP_STATUS_OK)
                {
                    m_handle = 0;
                    MessageBox.Show("打开失败");
                }
                else
                {
                    m_strFriendlyName = COMBO_DEVICES.Text;
                    _proc = _dvpStreamCallback;
                    using (Process curProcess = Process.GetCurrentProcess())
                    using (ProcessModule curModule = curProcess.MainModule)
                    {
                        dvpStatus s = DVPCamera.dvpRegisterStreamCallback(m_handle, _proc, dvpStreamEvent.STREAM_EVENT_PROCESSED, m_ptr);
                    }
                }
            }
            else
            {
                DVPCamera.dvpStop(m_handle);
                m_b_start = false;
                DVPCamera.dvpStreamCallback pf = new DVPCamera.dvpStreamCallback(_dvpStreamCallback);
                dvpStatus s = DVPCamera.dvpUnregisterStreamCallback(m_handle, pf, dvpStreamEvent.STREAM_EVENT_PROCESSED, m_ptr);
                DVPCamera.dvpClose(m_handle);
                m_handle = 0;
                pictureBoxCam.Invalidate();
            }
            UpdateControls();
        }

        private void BUTTON_START_Click(object sender, EventArgs e)
        {
            StartCamera();
        }
        public void StartCamera()
        {
            dvpStreamState state = dvpStreamState.STATE_STOPED;
            dvpStatus status = dvpStatus.DVP_STATUS_UNKNOW;

            if (IsValidHandle(m_handle))
            {
                // 根据当前视频状态，执行相反的操作，实现一个按钮既能启动又能停止
                status = DVPCamera.dvpGetStreamState(m_handle, ref state);
                if (state == dvpStreamState.STATE_STARTED)
                {
                    status = DVPCamera.dvpStop(m_handle);
                    m_b_start = status == dvpStatus.DVP_STATUS_OK ? false : true;
                }
                else
                {
                    status = DVPCamera.dvpStart(m_handle);
                    m_b_start = status == dvpStatus.DVP_STATUS_OK ? true : false;
                }

            }

            UpdateControls();
        }
        public void UpdateControls()
        {
            dvpStatus status = dvpStatus.DVP_STATUS_UNKNOW;
            if (IsValidHandle(m_handle))
            {
                // 此时已经打开了一个设备
                // 更新基本功能控件
                dvpStreamState state = new dvpStreamState();
                status = DVPCamera.dvpGetStreamState(m_handle, ref state);
                BUTTON_START.Text = (state == dvpStreamState.STATE_STARTED ? ("3停止") : ("3开始"));
                BUTTON_OPEN.Text = "2关闭";
                BUTTON_START.Enabled = true;
                BUTTON_PROPERTY.Enabled = true;
                BUTTON_SAVE.Enabled = true;
                BUTTON_CAPImg.Enabled = true;
            }
            else
            {
                // 此时设备还没有被打开;更新基本功能控件
                BUTTON_OPEN.Text = "2打开";
                BUTTON_START.Enabled = false;
                BUTTON_PROPERTY.Enabled = false;
                BUTTON_SAVE.Enabled = false;
                BUTTON_CAPImg.Enabled = false;
                BUTTON_OPEN.Enabled = m_n_dev_count == 0 ? false : true;
            }
        }

        private void BUTTON_PROPERTY_Click(object sender, EventArgs e)
        {
            CameraProperty();
        }
        public void CameraProperty()
        {
            if (IsValidHandle(m_handle))
            {
                dvpStatus status = DVPCamera.dvpShowPropertyModalDialog(m_handle, this.Handle);

                // 此时一些配置可能发生改变，将同步到界面
                UpdateControls();
            }
        }
        private void BUTTON_SAVE_Click(object sender, EventArgs e)
        {
            saveAs();
        }
        public void saveAs()
        {
            if (!m_b_start)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            string file_path;

            sfd.Filter = "bmp文件(*.bmp)|*.bmp|jpeg文件(*.jpeg)|*.jpg|png文件(*.png)|*.png";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                file_path = sfd.FileName;
                IntPtr buffer = new IntPtr();
                dvpFrame frame = new dvpFrame();
                dvpStatus status;

                // 从视频流中抓取一帧图像，要求5000ms以内；从视频流中抓取一帧图像，超时时间通常不应小于曝光时间
                status = DVPCamera.dvpGetFrame(m_handle, ref frame, ref buffer, 5000);
                if (status == dvpStatus.DVP_STATUS_OK)
                {
                    // 将图像保存为图片文件
                    status = DVPCamera.dvpSavePicture(ref frame, buffer, file_path, 100);
                }
                else if (status == dvpStatus.DVP_STATUS_TIME_OUT)
                {
                    MessageBox.Show(("获取图像数据超时!"));
                }
                else
                {
                    MessageBox.Show(("获取图像数据时发生错误!"));
                }
            }
        }
        //双击控件触发采集图像
        private void pictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (BUTTON_CAPImg.Enabled)
            {
                if (MshotCaptureImageEvent != null)
                {
                    MshotCaptureImageEvent(this, new CapImageEventArgs("", ""));
                }
            }
        }

        //应用程序执行路径
        public static string APPdirPath = System.Environment.CurrentDirectory;
        public string ExeCaptureSingleImg(string study_no)
        {
            bool opflag = false;
            if (!m_b_start)
                return "";
            string file_path = string.Format(@"{0}\{1}.bmp", APPdirPath, study_no);
            try
            {
                IntPtr buffer = new IntPtr();
                dvpFrame frame = new dvpFrame();
                dvpStatus status;
                // 从视频流中抓取一帧图像，要求5000ms以内；从视频流中抓取一帧图像，超时时间通常不应小于曝光时间
                status = DVPCamera.dvpGetFrame(m_handle, ref frame, ref buffer, 5000);
                if (status == dvpStatus.DVP_STATUS_OK)
                {
                    // 将图像保存为图片文件
                    status = DVPCamera.dvpSavePicture(ref frame, buffer, file_path, 100);
                    opflag = true;
                }
                else if (status == dvpStatus.DVP_STATUS_TIME_OUT)
                {
                    MessageBox.Show(("获取图像数据超时!"));
                }
                else
                {
                    MessageBox.Show(("获取图像数据时发生错误!"));
                }
            }
            catch
            {
                file_path = "";
            }
            if (opflag)
            {
                return string.Format("{0}|{1}", study_no, file_path);
            }
            else
            {
                return "";
            }
        }
        public delegate void CaptureSingleImgHandle(object sender, CapImageEventArgs e);
        //定义事件
        public event CaptureSingleImgHandle MshotCaptureImageEvent;
        private void BUTTON_CAPImg_Click(object sender, EventArgs e)
        {
            if (MshotCaptureImageEvent != null)
            {
                MshotCaptureImageEvent(this, new CapImageEventArgs("", ""));
            }
        }
        //打开摄像头属性
        public void OpenPropertyForm()
        {
            if (BUTTON_PROPERTY.Enabled)
            {
                CameraProperty();
            }
        }
        //连接指定摄像头 
        public void ConnCamera(string FriendlyName)
        {
            //先执行关闭
            DigitsCamClosed();

            //1.初始化设备
            InitDevList();
            Application.DoEvents();
            //选中指定设备
            if (COMBO_DEVICES.Items.Count > 0)
            {
                if (COMBO_DEVICES.Items.Contains(FriendlyName))
                {
                    COMBO_DEVICES.Text = FriendlyName;
                }
            }
            Application.DoEvents();
            //2.打开设备
            if (BUTTON_OPEN.Enabled)
            {
                OpenCamera();
                Application.DoEvents();
            }
            //3.显示视频流
            if (BUTTON_START.Enabled)
            {
                StartCamera();
                Application.DoEvents();
            }
        }
        //关闭摄像头
        public void CloseCamera()
        {
            DigitsCamClosed();
            Application.DoEvents();
        }

    }
}
