using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace PathologyClient
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public class SetComputerDateTime
    {

        [DllImport("kernel32.dll", EntryPoint = "GetSystemDefaultLCID")]
        public static extern int GetSystemDefaultLCID();

        [DllImport("kernel32.dll", EntryPoint = "SetLocaleInfoA")]
        public static extern int SetLocaleInfo(int Locale, int LCType, string lpLCData);

        public const int LOCALE_SLONGDATE = 0x20;
        public const int LOCALE_SSHORTDATE = 0x1F;
        public const int LOCALE_STIME = 0x1E;

        [DllImportAttribute("Kernel32.dll")]
        public static extern void SetLocalTime(SystemTime st);


        public void SetDateTimeFormat()
        {
            try
            {
                int x = GetSystemDefaultLCID();
                SetLocaleInfo(x, LOCALE_SSHORTDATE, "yyyy-MM-dd"); //短日期格式
                SetLocaleInfo(x, LOCALE_STIME, "HH:mm"); //时间格式
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class SystemTime
    {
        public ushort wYear;
        public ushort wMonth;
        public ushort wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
}
