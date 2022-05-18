using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace PIS_Sys
{
    public class Helper
    {


        /**/
        /// <summary>
        /// 序列化一个对象
        /// </summary>
        /// <param name="xmlobj">被序列化的对象</param>
        public static string SerObject(Object xmlobj)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            Type type = xmlobj.GetType();
            XmlSerializer sz = new XmlSerializer(type);
            sz.Serialize(tw, xmlobj);
            tw.Close();
            return sb.ToString();
        }

        /// <summary>
        /// 序列化 对象到字符串
        /// </summary>
        /// <param name="obj">泛型对象</param>
        /// <returns>序列化后的字符串</returns>
        public static string Serialize<T>(T obj)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                MemoryStream stream = new MemoryStream();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                return Convert.ToBase64String(buffer);
            }
            catch (Exception ex)
            {
                throw new Exception("序列化失败,原因:" + ex.Message);
            }
        }
        /// <summary>
        /// 反序列化 字符串到对象
        /// </summary>
        /// <param name="obj">泛型对象</param>
        /// <param name="str">要转换为对象的字符串</param>
        /// <returns>反序列化出来的对象</returns>
        public static T Desrialize<T>(T obj, string str)
        {
            try
            {
                obj = default(T);
                IFormatter formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(str);
                MemoryStream stream = new MemoryStream(buffer);
                obj = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("反序列化失败,原因:" + ex.Message);
            }
            return obj;
        }



        public static string GetUidStr()
        {
            return Guid.NewGuid().ToString("N");
        }


        public static DateTime Now
        {
            get { return DateTime.UtcNow; }
        }

        //DateTime date = DateTime.Now;

        //string filename = String.Format("{0}-{1}-{2}_{3}-{4}-{5}",
        //    date.Year, Helper.ZeroPad(date.Month), Helper.ZeroPad(date.Day),
        //    Helper.ZeroPad(date.Hour), Helper.ZeroPad(date.Minute),
        //    Helper.ZeroPad(date.Second));

        public static string ZeroPad(int i)
        {
            if (i < 10)
                return "0" + i;
            return i.ToString(CultureInfo.InvariantCulture);
        }


        static public void CopyFolder(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest, true);
            }
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                CopyFolder(folder, dest);
            }
        }









        // returns the number of milliseconds since Jan 1, 1970 (useful for converting C# dates to JS dates)
        public static double UnixTicks(DateTime dt)
        {
            var d1 = new DateTime(1970, 1, 1);
            var d2 = dt.ToUniversalTime();
            var ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }

        public static double UnixTicks(long ticks)
        {
            var d1 = new DateTime(1970, 1, 1);
            var d2 = new DateTime(ticks);
            var ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return ts.TotalMilliseconds;
        }

        public class AlertGroup
        {
            public DateTime LastReset;
            public readonly String Name;

            public AlertGroup(string name)
            {
                LastReset = Now;
                Name = name;
            }
        }



        public enum MachineType : ushort
        {
            IMAGE_FILE_MACHINE_UNKNOWN = 0x0,
            IMAGE_FILE_MACHINE_AM33 = 0x1d3,
            IMAGE_FILE_MACHINE_AMD64 = 0x8664,
            IMAGE_FILE_MACHINE_ARM = 0x1c0,
            IMAGE_FILE_MACHINE_EBC = 0xebc,
            IMAGE_FILE_MACHINE_I386 = 0x14c,
            IMAGE_FILE_MACHINE_IA64 = 0x200,
            IMAGE_FILE_MACHINE_M32R = 0x9041,
            IMAGE_FILE_MACHINE_MIPS16 = 0x266,
            IMAGE_FILE_MACHINE_MIPSFPU = 0x366,
            IMAGE_FILE_MACHINE_MIPSFPU16 = 0x466,
            IMAGE_FILE_MACHINE_POWERPC = 0x1f0,
            IMAGE_FILE_MACHINE_POWERPCFP = 0x1f1,
            IMAGE_FILE_MACHINE_R4000 = 0x166,
            IMAGE_FILE_MACHINE_SH3 = 0x1a2,
            IMAGE_FILE_MACHINE_SH3DSP = 0x1a3,
            IMAGE_FILE_MACHINE_SH4 = 0x1a6,
            IMAGE_FILE_MACHINE_SH5 = 0x1a8,
            IMAGE_FILE_MACHINE_THUMB = 0x1c2,
            IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x169,
        }




        #region Nested type: FrameAction

        public struct FrameAction
        {
            public byte[] Content;
            public int DataLength;
            public readonly double Level;
            public readonly DateTime TimeStamp;
            public readonly Enums.FrameType FrameType;

            public FrameAction(Bitmap frame, double level, DateTime timeStamp)
            {
                Level = level;
                TimeStamp = timeStamp;
                using (var ms = new MemoryStream())
                {
                    frame.Save(ms, qcgl.Frm_Dtcj.GetFEncoder, qcgl.Frm_Dtcj.GetEncoderParameters());
                    Content = ms.GetBuffer();
                }
                FrameType = Enums.FrameType.Video;
                DataLength = Content.Length;
            }

            public FrameAction(byte[] frame, int bytesRecorded, double level, DateTime timeStamp)
            {
                Content = frame;
                Level = level;
                TimeStamp = timeStamp;
                FrameType = Enums.FrameType.Audio;
                DataLength = bytesRecorded;
            }

            public void Nullify()
            {
                Content = null;
                DataLength = 0;
            }

        }
        #endregion

    }
    public class Enums
    {
        public enum FrameType { Video, Audio }

        public enum AudioStreamMode
        {
            //PCM,
            MP3
            //,M4A
        }
    }
}
