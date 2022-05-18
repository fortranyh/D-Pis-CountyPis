using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys
{
    public class HotkeyInfo
    {
        public Keys Hotkey { get; set; }
        [JsonIgnore]
        public ushort ID { get; set; }

        [JsonIgnore]
        public HotkeyStatus Status { get; set; }

        public Keys KeyCode
        {
            get
            {
                return Hotkey & Keys.KeyCode;
            }
        }

        public Keys ModifiersKeys
        {
            get
            {
                return Hotkey & Keys.Modifiers;
            }
        }

        public bool Control
        {
            get
            {
                return Hotkey.HasFlag(Keys.Control);
            }
        }

        public bool Shift
        {
            get
            {
                return Hotkey.HasFlag(Keys.Shift);
            }
        }

        public bool Alt
        {
            get
            {
                return Hotkey.HasFlag(Keys.Alt);
            }
        }

        public bool Win { get; set; }

        public Modifiers ModifiersEnum
        {
            get
            {
                Modifiers modifiers = Modifiers.None;

                if (Alt) modifiers |= Modifiers.Alt;
                if (Control) modifiers |= Modifiers.Control;
                if (Shift) modifiers |= Modifiers.Shift;
                if (Win) modifiers |= Modifiers.Win;

                return modifiers;
            }
        }

        public bool IsOnlyModifiers
        {
            get
            {
                return KeyCode == Keys.ControlKey || KeyCode == Keys.ShiftKey || KeyCode == Keys.Menu;
            }
        }

        public bool IsValidHotkey
        {
            get
            {
                return KeyCode != Keys.None && !IsOnlyModifiers;
            }
        }

        public HotkeyInfo()
        {
            Status = HotkeyStatus.NotConfigured;
        }

        public HotkeyInfo(Keys hotkey)
            : this()
        {
            Hotkey = hotkey;
        }

        public HotkeyInfo(Keys hotkey, ushort id)
            : this(hotkey)
        {
            ID = id;
        }

        public override string ToString()
        {
            string text = string.Empty;

            if (KeyCode != Keys.None)
            {
                if (Control)
                {
                    text += "Ctrl + ";
                }

                if (Shift)
                {
                    text += "Shift + ";
                }

                if (Alt)
                {
                    text += "Alt + ";
                }

                if (Win)
                {
                    text += "Win + ";
                }
            }

            if (IsOnlyModifiers)
            {
                text += "...";
            }
            else if (KeyCode == Keys.Back)
            {
                text += "Backspace";
            }
            else if (KeyCode == Keys.Return)
            {
                text += "Enter";
            }
            else if (KeyCode == Keys.Capital)
            {
                text += "Caps Lock";
            }
            else if (KeyCode == Keys.Next)
            {
                text += "Page Down";
            }
            else if (KeyCode == Keys.Scroll)
            {
                text += "Scroll Lock";
            }
            else if (KeyCode >= Keys.D0 && KeyCode <= Keys.D9)
            {
                text += (KeyCode - Keys.D0).ToString();
            }
            else if (KeyCode >= Keys.NumPad0 && KeyCode <= Keys.NumPad9)
            {
                text += "Numpad " + (KeyCode - Keys.NumPad0).ToString();
            }
            else
            {
                text += ToStringWithSpaces(KeyCode);
            }

            return text;
        }

        private string ToStringWithSpaces(Keys key)
        {
            string name = key.ToString();

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i]))
                {
                    result.Append(" " + name[i]);
                }
                else
                {
                    result.Append(name[i]);
                }
            }

            return result.ToString();
        }

    }

    public enum HotkeyStatus
    {
        Registered,
        Failed,
        NotConfigured
    }
    public static partial class NativeMethods
    {
        public const int GCL_HICONSM = -34;
        public const int GCL_HICON = -14;
        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;
        public const int SC_MINIMIZE = 0xF020;
        public const int HT_CAPTION = 2;
        public const int CURSOR_SHOWING = 1;
        public const int GWL_STYLE = -16;
        public const int DWM_TNP_RECTDESTINATION = 0x1;
        public const int DWM_TNP_RECTSOURCE = 0x2;
        public const int DWM_TNP_OPACITY = 0x4;
        public const int DWM_TNP_VISIBLE = 0x8;
        public const int DWM_TNP_SOURCECLIENTAREAONLY = 0x10;
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;
        public const int ULW_ALPHA = 0x02;
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
    }
    public static partial class NativeMethods
    {
        #region Delegates

        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion Delegates

        #region user32.dll

        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);

        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpfn, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        public static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr parentHwnd, IntPtr childAfterHwnd, IntPtr className, string windowText);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CursorInfo pci);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out IconInfo piconinfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// The GetNextWindow function retrieves a handle to the next or previous window in the Z-Order.
        /// The next window is below the specified window; the previous window is above.
        /// If the specified window is a topmost window, the function retrieves a handle to the next (or previous) topmost window.
        /// If the specified window is a top-level window, the function retrieves a handle to the next (or previous) top-level window.
        /// If the specified window is a child window, the function searches for a handle to the next (or previous) child window.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowConstants wCmd);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern ulong GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>Determines the visibility state of the specified window.</summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>Determines whether the specified window is minimized (iconic).</summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        /// <summary>Determines whether a window is maximized.</summary>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsZoomed(IntPtr hWnd);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, int Msg, int wParam, int lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out IntPtr lpdwResult);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pptSrc, uint crKey, [In] ref BLENDFUNCTION pblend, uint dwFlags);

        /// <summary> The RegisterHotKey function defines a system-wide hot key </summary>
        /// <param name="hWnd">Handle to the window that will receive WM_HOTKEY messages generated by the hot key.</param>
        /// <param name="id">Specifies the identifier of the hot key.</param>
        /// <param name="fsModifiers">Specifies keys that must be pressed in combination with the key
        /// specified by the 'vk' parameter in order to generate the WM_HOTKEY message.</param>
        /// <param name="vk">Specifies the virtual-key code of the hot key</param>
        /// <returns><c>true</c> if the function succeeds, otherwise <c>false</c></returns>
        /// <seealso cref="http://msdn.microsoft.com/en-us/library/ms646309(VS.85).aspx"/>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        #endregion user32.dll

        #region kernel32.dll

        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, IntPtr min, IntPtr max);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern ushort GlobalAddAtom(string lpString);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern ushort GlobalDeleteAtom(ushort nAtom);

        [DllImport("user32.dll")]
        public static extern uint GetClassLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);

        #endregion kernel32.dll

        #region gdi32.dll

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nReghtRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nReghtRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, [In] ref BITMAPINFOHEADER pbmi, uint pila, out IntPtr ppvBits, IntPtr hSection, uint dwOffset);

        #endregion gdi32.dll

        #region gdiplus.dll

        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GdipGetImageType(HandleRef image, out int type);

        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GdipImageForceValidation(HandleRef image);

        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GdipLoadImageFromFile(string filename, out IntPtr image);

        [DllImport("gdiplus.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int GdipDisposeImage(HandleRef image);

        [DllImport("gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);

        #endregion gdiplus.dll

        #region shell32.dll

        [DllImport("shell32.dll")]
        public static extern IntPtr SHAppBarMessage(uint dwMessage, [In] ref APPBARDATA pData);

        #endregion shell32.dll

        #region dwmapi.dll

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out RECT pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttribute, out bool pvAttribute, int cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern void DwmEnableBlurBehindWindow(IntPtr hwnd, ref DWM_BLURBEHIND blurBehind);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(CompositionAction uCompositionAction);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS margins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out SIZE size);

        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetDxFrameDuration(IntPtr hwnd, uint cRefreshes);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr thumb);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

        #endregion dwmapi.dll

        #region Other dll

        /// <summary>
        /// Copy a block of memory.
        /// </summary>
        ///
        /// <param name="dst">Destination pointer.</param>
        /// <param name="src">Source pointer.</param>
        /// <param name="count">Memory block's length to copy.</param>
        ///
        /// <returns>Return's the value of <b>dst</b> - pointer to destination.</returns>
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int memcpy(int dst, int src, int count);

        /// <summary>
        /// Initialize the AVIFile library.
        /// </summary>
        [DllImport("avifil32.dll")]
        public static extern void AVIFileInit();

        /// <summary>
        /// Exit the AVIFile library.
        /// </summary>
        [DllImport("avifil32.dll")]
        public static extern void AVIFileExit();

        /// <summary>
        /// Open an AVI file.
        /// </summary>
        ///
        /// <param name="aviHandler">Opened AVI file interface.</param>
        /// <param name="fileName">AVI file name.</param>
        /// <param name="mode">Opening mode (see <see cref="OpenFileMode"/>).</param>
        /// <param name="handler">Handler to use (<b>null</b> to use default).</param>
        ///
        /// <returns>Returns zero on success or error code otherwise.</returns>
        [DllImport("avifil32.dll", CharSet = CharSet.Unicode)]
        public static extern int AVIFileOpen(out IntPtr aviHandler, String fileName, OpenFileMode mode, IntPtr handler);

        /// <summary>
        /// Release an open AVI stream.
        /// </summary>
        ///
        /// <param name="aviHandler">Open AVI file interface.</param>
        ///
        /// <returns>Returns the reference count of the file.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIFileRelease(IntPtr aviHandler);

        /// <summary>
        /// Get stream interface that is associated with a specified AVI file
        /// </summary>
        ///
        /// <param name="aviHandler">Handler to an open AVI file.</param>
        /// <param name="streamHandler">Stream interface.</param>
        /// <param name="streamType">Stream type to open.</param>
        /// <param name="streamNumner">Count of the stream type. Identifies which occurrence of the specified stream type to access. </param>
        ///
        /// <returns></returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIFileGetStream(IntPtr aviHandler, out IntPtr streamHandler, int streamType, int streamNumner);

        /// <summary>
        /// Create a new stream in an existing file and creates an interface to the new stream.
        /// </summary>
        ///
        /// <param name="aviHandler">Handler to an open AVI file.</param>
        /// <param name="streamHandler">Stream interface.</param>
        /// <param name="streamInfo">Pointer to a structure containing information about the new stream.</param>
        ///
        /// <returns>Returns zero if successful or an error otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIFileCreateStream(IntPtr aviHandler, out IntPtr streamHandler, ref AVISTREAMINFO streamInfo);

        /// <summary>
        /// Release an open AVI stream.
        /// </summary>
        ///
        /// <param name="streamHandler">Handle to an open stream.</param>
        ///
        /// <returns>Returns the current reference count of the stream.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamRelease(IntPtr streamHandler);

        /// <summary>
        /// Set the format of a stream at the specified position.
        /// </summary>
        ///
        /// <param name="streamHandler">Handle to an open stream.</param>
        /// <param name="position">Position in the stream to receive the format.</param>
        /// <param name="format">Pointer to a structure containing the new format.</param>
        /// <param name="formatSize">Size, in bytes, of the block of memory referenced by <b>format</b>.</param>
        ///
        /// <returns>Returns zero if successful or an error otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamSetFormat(IntPtr streamHandler, int position, ref BITMAPINFOHEADER format, int formatSize);

        /// <summary>
        /// Get the starting sample number for the stream.
        /// </summary>
        ///
        /// <param name="streamHandler">Handle to an open stream.</param>
        ///
        /// <returns>Returns the number if successful or  1 otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamStart(IntPtr streamHandler);

        /// <summary>
        /// Get the length of the stream.
        /// </summary>
        ///
        /// <param name="streamHandler">Handle to an open stream.</param>
        ///
        /// <returns>Returns the stream's length, in samples, if successful or -1 otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamLength(IntPtr streamHandler);

        /// <summary>
        /// Obtain stream header information.
        /// </summary>
        ///
        /// <param name="streamHandler">Handle to an open stream.</param>
        /// <param name="streamInfo">Pointer to a structure to contain the stream information.</param>
        /// <param name="infoSize">Size, in bytes, of the structure used for <b>streamInfo</b>.</param>
        ///
        /// <returns>Returns zero if successful or an error otherwise.</returns>
        [DllImport("avifil32.dll", CharSet = CharSet.Unicode)]
        public static extern int AVIStreamInfo(IntPtr streamHandler, ref AVISTREAMINFO streamInfo, int infoSize);

        /// <summary>
        /// Prepare to decompress video frames from the specified video stream
        /// </summary>
        ///
        /// <param name="streamHandler">Pointer to the video stream used as the video source.</param>
        /// <param name="wantedFormat">Pointer to a structure that defines the desired video format. Specify NULL to use a default format.</param>
        ///
        /// <returns>Returns an object that can be used with the <see cref="AVIStreamGetFrame"/> function.</returns>
        [DllImport("avifil32.dll")]
        public static extern IntPtr AVIStreamGetFrameOpen(IntPtr streamHandler, ref BITMAPINFOHEADER wantedFormat);

        /// <summary>
        /// Prepare to decompress video frames from the specified video stream.
        /// </summary>
        ///
        /// <param name="streamHandler">Pointer to the video stream used as the video source.</param>
        /// <param name="wantedFormat">Pointer to a structure that defines the desired video format. Specify NULL to use a default format.</param>
        ///
        /// <returns>Returns a <b>GetFrame</b> object that can be used with the <see cref="AVIStreamGetFrame"/> function.</returns>
        [DllImport("avifil32.dll")]
        public static extern IntPtr AVIStreamGetFrameOpen(IntPtr streamHandler, int wantedFormat);

        /// <summary>
        /// Releases resources used to decompress video frames.
        /// </summary>
        ///
        /// <param name="getFrameObject">Handle returned from the <see cref="AVIStreamGetFrameOpen(IntPtr,int)"/> function.</param>
        ///
        /// <returns>Returns zero if successful or an error otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamGetFrameClose(IntPtr getFrameObject);

        /// <summary>
        /// Return the address of a decompressed video frame.
        /// </summary>
        ///
        /// <param name="getFrameObject">Pointer to a GetFrame object.</param>
        /// <param name="position">Position, in samples, within the stream of the desired frame.</param>
        ///
        /// <returns>Returns a pointer to the frame data if successful or NULL otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern IntPtr AVIStreamGetFrame(IntPtr getFrameObject, int position);

        /// <summary>
        /// Write data to a stream.
        /// </summary>
        ///
        /// <param name="streamHandler">Handle to an open stream.</param>
        /// <param name="start">First sample to write.</param>
        /// <param name="samples">Number of samples to write.</param>
        /// <param name="buffer">Pointer to a buffer containing the data to write. </param>
        /// <param name="bufferSize">Size of the buffer referenced by <b>buffer</b>.</param>
        /// <param name="flags">Flag associated with this data.</param>
        /// <param name="samplesWritten">Pointer to a buffer that receives the number of samples written. This can be set to NULL.</param>
        /// <param name="bytesWritten">Pointer to a buffer that receives the number of bytes written. This can be set to NULL.</param>
        ///
        /// <returns>Returns zero if successful or an error otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIStreamWrite(IntPtr streamHandler, int start, int samples, IntPtr buffer, int bufferSize, int flags, IntPtr samplesWritten,
            IntPtr bytesWritten);

        /// <summary>
        /// Retrieve the save options for a file and returns them in a buffer.
        /// </summary>
        ///
        /// <param name="window">Handle to the parent window for the Compression Options dialog box.</param>
        /// <param name="flags">Flags for displaying the Compression Options dialog box.</param>
        /// <param name="streams">Number of streams that have their options set by the dialog box.</param>
        /// <param name="streamInterfaces">Pointer to an array of stream interface pointers.</param>
        /// <param name="options">Pointer to an array of pointers to AVICOMPRESSOPTIONS structures.</param>
        ///
        /// <returns>Returns TRUE if the user pressed OK, FALSE for CANCEL, or an error otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVISaveOptions(IntPtr window, int flags, int streams, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] streamInterfaces,
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] options);

        /// <summary>
        /// Free the resources allocated by the AVISaveOptions function.
        /// </summary>
        ///
        /// <param name="streams">Count of the AVICOMPRESSOPTIONS structures referenced in <b>options</b>.</param>
        /// <param name="options">Pointer to an array of pointers to AVICOMPRESSOPTIONS structures.</param>
        ///
        /// <returns>Returns 0.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVISaveOptionsFree(int streams, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] IntPtr[] options);

        /// <summary>
        /// Create a compressed stream from an uncompressed stream and a
        /// compression filter, and returns the address of a pointer to
        /// the compressed stream.
        /// </summary>
        ///
        /// <param name="compressedStream">Pointer to a buffer that receives the compressed stream pointer.</param>
        /// <param name="sourceStream">Pointer to the stream to be compressed.</param>
        /// <param name="options">Pointer to a structure that identifies the type of compression to use and the options to apply.</param>
        /// <param name="clsidHandler">Pointer to a class identifier used to create the stream.</param>
        ///
        /// <returns>Returns 0 if successful or an error otherwise.</returns>
        [DllImport("avifil32.dll")]
        public static extern int AVIMakeCompressedStream(out IntPtr compressedStream, IntPtr sourceStream, ref AVICOMPRESSOPTIONS options, IntPtr clsidHandler);

        [DllImport("dnsapi.dll")]
        public static extern uint DnsFlushResolverCache();

        #endregion Other dll
    }
    public static partial class NativeMethods
    {
        public static string GetForegroundWindowText()
        {
            IntPtr handle = GetForegroundWindow();
            return GetWindowText(handle);
        }

        public static string GetWindowText(IntPtr handle)
        {
            if (handle.ToInt32() > 0)
            {
                int length = GetWindowTextLength(handle);

                if (length > 0)
                {
                    StringBuilder sb = new StringBuilder(length + 1);

                    if (GetWindowText(handle, sb, sb.Capacity) > 0)
                    {
                        return sb.ToString();
                    }
                }
            }

            return null;
        }

        public static Process GetForegroundWindowProcess()
        {
            IntPtr handle = GetForegroundWindow();
            return GetProcessByWindowHandle(handle);
        }

        public static Process GetProcessByWindowHandle(IntPtr hwnd)
        {
            if (hwnd.ToInt32() > 0)
            {
                try
                {
                    uint processID;
                    GetWindowThreadProcessId(hwnd, out processID);
                    return Process.GetProcessById((int)processID);
                }
                catch
                {

                }
            }

            return null;
        }

        public static string GetClassName(IntPtr handle)
        {
            if (handle.ToInt32() > 0)
            {
                StringBuilder sb = new StringBuilder(256);

                if (GetClassName(handle, sb, sb.Capacity) > 0)
                {
                    return sb.ToString();
                }
            }

            return null;
        }

        public static IntPtr GetClassLongPtrSafe(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
            {
                return GetClassLongPtr(hWnd, nIndex);
            }

            return new IntPtr(GetClassLong(hWnd, nIndex));
        }

        private static Icon GetSmallApplicationIcon(IntPtr handle)
        {
            IntPtr iconHandle;

            SendMessageTimeout(handle, (int)WindowsMessages.GETICON, ICON_SMALL2, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

            if (iconHandle == IntPtr.Zero)
            {
                SendMessageTimeout(handle, (int)WindowsMessages.GETICON, ICON_SMALL, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

                if (iconHandle == IntPtr.Zero)
                {
                    iconHandle = GetClassLongPtrSafe(handle, GCL_HICONSM);

                    if (iconHandle == IntPtr.Zero)
                    {
                        SendMessageTimeout(handle, (int)WindowsMessages.QUERYDRAGICON, 0, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);
                    }
                }
            }

            if (iconHandle != IntPtr.Zero)
            {
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }

        private static Icon GetBigApplicationIcon(IntPtr handle)
        {
            IntPtr iconHandle;

            SendMessageTimeout(handle, (int)WindowsMessages.GETICON, ICON_BIG, 0, SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 1000, out iconHandle);

            if (iconHandle == IntPtr.Zero)
            {
                iconHandle = GetClassLongPtrSafe(handle, GCL_HICON);
            }

            if (iconHandle != IntPtr.Zero)
            {
                return Icon.FromHandle(iconHandle);
            }

            return null;
        }

        public static Icon GetApplicationIcon(IntPtr handle)
        {
            return GetSmallApplicationIcon(handle) ?? GetBigApplicationIcon(handle);
        }

        public static bool GetBorderSize(IntPtr handle, out Size size)
        {
            WINDOWINFO wi = new WINDOWINFO();

            bool result = GetWindowInfo(handle, ref wi);

            if (result)
            {
                size = new Size((int)wi.cxWindowBorders, (int)wi.cyWindowBorders);
            }
            else
            {
                size = Size.Empty;
            }

            return result;
        }

        public static bool GetWindowRegion(IntPtr hWnd, out Region region)
        {
            IntPtr hRgn = CreateRectRgn(0, 0, 0, 0);
            RegionType regionType = (RegionType)GetWindowRgn(hWnd, hRgn);
            region = Region.FromHrgn(hRgn);
            return regionType != RegionType.ERROR && regionType != RegionType.NULLREGION;
        }

        public static bool IsDWMEnabled()
        {
            return IsWindowsVistaOrGreater() && DwmIsCompositionEnabled();
        }

        public static bool GetExtendedFrameBounds(IntPtr handle, out Rectangle rectangle)
        {
            RECT rect;
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.ExtendedFrameBounds, out rect, Marshal.SizeOf(typeof(RECT)));
            rectangle = rect;
            return result == 0;
        }

        public static bool GetNCRenderingEnabled(IntPtr handle)
        {
            bool enabled;
            int result = DwmGetWindowAttribute(handle, (int)DwmWindowAttribute.NCRenderingEnabled, out enabled, sizeof(bool));
            return result == 0 && enabled;
        }

        public static void SetNCRenderingPolicy(IntPtr handle, DwmNCRenderingPolicy renderingPolicy)
        {
            int renderPolicy = (int)renderingPolicy;
            DwmSetWindowAttribute(handle, (int)DwmWindowAttribute.NCRenderingPolicy, ref renderPolicy, sizeof(int));
        }

        public static Rectangle GetWindowRect(IntPtr handle)
        {
            RECT rect;
            GetWindowRect(handle, out rect);
            return rect;
        }

        public static Rectangle GetClientRect(IntPtr handle)
        {
            RECT rect;
            GetClientRect(handle, out rect);
            Point position = rect.Location;
            ClientToScreen(handle, ref position);
            return new Rectangle(position, rect.Size);
        }

        public static Rectangle MaximizedWindowFix(IntPtr handle, Rectangle windowRect)
        {
            Size size;

            if (GetBorderSize(handle, out size))
            {
                windowRect = new Rectangle(windowRect.X + size.Width, windowRect.Y + size.Height, windowRect.Width - (size.Width * 2), windowRect.Height - (size.Height * 2));
            }

            return windowRect;
        }

        public static void ActivateWindow(IntPtr handle)
        {
            SetForegroundWindow(handle);
            SetActiveWindow(handle);
        }

        public static void ActivateWindowRepeat(IntPtr handle, int count)
        {
            for (int i = 0; GetForegroundWindow() != handle && i < count; i++)
            {
                BringWindowToTop(handle);
                Thread.Sleep(1);
                Application.DoEvents();
            }
        }

        public static Rectangle GetTaskbarRectangle()
        {
            APPBARDATA abd = APPBARDATA.NewAPPBARDATA();
            SHAppBarMessage((uint)ABMsg.ABM_GETTASKBARPOS, ref abd);
            return abd.rc;
        }
        public static readonly Version OSVersion = Environment.OSVersion.Version;
        public static bool IsWindows7()
        {
            return OSVersion.Major == 6 && OSVersion.Minor == 1;
        }
        public static bool IsWindowsVista()
        {
            return OSVersion.Major == 6;
        }

        public static bool IsWindowsVistaOrGreater()
        {
            return OSVersion.Major >= 6;
        }

        public static bool SetTaskbarVisibilityIfIntersect(bool visible, Rectangle rect)
        {
            bool result = false;

            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);

            if (taskbarHandle != IntPtr.Zero)
            {
                Rectangle taskbarRect = GetWindowRect(taskbarHandle);

                if (rect.IntersectsWith(taskbarRect))
                {
                    ShowWindow(taskbarHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);
                    result = true;
                }

                if (IsWindowsVista() || IsWindows7())
                {
                    IntPtr startHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);

                    if (startHandle != IntPtr.Zero)
                    {
                        Rectangle startRect = GetWindowRect(startHandle);

                        if (rect.IntersectsWith(startRect))
                        {
                            ShowWindow(startHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);
                            result = true;
                        }
                    }
                }
            }

            return result;
        }

        public static bool SetTaskbarVisibility(bool visible)
        {
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);

            if (taskbarHandle != IntPtr.Zero)
            {
                ShowWindow(taskbarHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);

                if (IsWindowsVista() || IsWindows7())
                {
                    IntPtr startHandle = FindWindowEx(IntPtr.Zero, IntPtr.Zero, (IntPtr)0xC017, null);

                    if (startHandle != IntPtr.Zero)
                    {
                        ShowWindow(startHandle, visible ? (int)WindowShowStyle.Show : (int)WindowShowStyle.Hide);
                    }
                }

                return true;
            }

            return false;
        }

        public static void TrimMemoryUse()
        {
            GC.Collect();
            GC.WaitForFullGCComplete();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (IntPtr)(-1), (IntPtr)(-1));
        }

        public static bool IsWindowMaximized(IntPtr handle)
        {
            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
            wp.length = Marshal.SizeOf(wp);
            GetWindowPlacement(handle, ref wp);
            return wp.showCmd == WindowShowStyle.Maximize;
        }

        public static IntPtr SetHook(int hookType, HookProc hookProc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            using (ProcessModule currentModule = currentProcess.MainModule)
            {
                return SetWindowsHookEx(hookType, hookProc, GetModuleHandle(currentModule.ModuleName), 0);
            }
        }

        public static void RestoreWindow(IntPtr handle)
        {
            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
            wp.length = Marshal.SizeOf(wp);
            GetWindowPlacement(handle, ref wp);

            if (wp.flags == (int)WindowPlacementFlags.WPF_RESTORETOMAXIMIZED)
            {
                wp.showCmd = WindowShowStyle.ShowMaximized;
            }
            else
            {
                wp.showCmd = WindowShowStyle.Restore;
            }

            SetWindowPlacement(handle, ref wp);
        }

        /// <summary>
        /// Version of <see cref="AVISaveOptions(IntPtr, int, int, IntPtr[], IntPtr[])"/> for one stream only.
        /// </summary>
        ///
        /// <param name="stream">Stream to configure.</param>
        /// <param name="options">Stream options.</param>
        ///
        /// <returns>Returns TRUE if the user pressed OK, FALSE for CANCEL, or an error otherwise.</returns>
        public static int AVISaveOptions(IntPtr stream, ref AVICOMPRESSOPTIONS options, IntPtr parentWindow)
        {
            IntPtr[] streams = new IntPtr[1];
            IntPtr[] infPtrs = new IntPtr[1];

            // alloc unmanaged memory
            IntPtr mem = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(AVICOMPRESSOPTIONS)));

            // copy from managed structure to unmanaged memory
            Marshal.StructureToPtr(options, mem, false);

            streams[0] = stream;
            infPtrs[0] = mem;

            // show dialog with a list of available compresors and configuration
            int ret = AVISaveOptions(parentWindow, 0, 1, streams, infPtrs);

            // copy from unmanaged memory to managed structure
            options = (AVICOMPRESSOPTIONS)Marshal.PtrToStructure(mem, typeof(AVICOMPRESSOPTIONS));

            // free AVI compression options
            AVISaveOptionsFree(1, infPtrs);

            // clear it, because the information already freed by AVISaveOptionsFree
            options.format = 0;
            options.parameters = 0;

            // free unmanaged memory
            Marshal.FreeHGlobal(mem);

            return ret;
        }

        /// <summary>
        /// .NET replacement of mmioFOURCC macros. Converts four characters to code.
        /// </summary>
        ///
        /// <param name="str">Four characters string.</param>
        ///
        /// <returns>Returns the code created from provided characters.</returns>
        public static int mmioFOURCC(string str)
        {
            return (
                (byte)(str[0]) |
                ((byte)(str[1]) << 8) |
                ((byte)(str[2]) << 16) |
                ((byte)(str[3]) << 24));
        }

        /// <summary>
        /// Inverse to <see cref="mmioFOURCC"/>. Converts code to fout characters string.
        /// </summary>
        ///
        /// <param name="code">Code to convert.</param>
        ///
        /// <returns>Returns four characters string.</returns>
        public static string decode_mmioFOURCC(int code)
        {
            char[] chs = new char[4];

            for (int i = 0; i < 4; i++)
            {
                chs[i] = (char)(byte)((code >> (i << 3)) & 0xFF);
                if (!char.IsLetterOrDigit(chs[i]))
                    chs[i] = ' ';
            }
            return new string(chs);
        }

        public static bool Is64Bit()
        {
            if (IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor()))
            {
                return true;
            }

            return false;
        }

        private static bool Is32BitProcessOn64BitProcessor()
        {
            bool retVal;

            IsWow64Process(Process.GetCurrentProcess().Handle, out retVal);

            return retVal;
        }
    }
}
