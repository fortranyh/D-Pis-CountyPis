using System;
using System.Text;
namespace Myzh_BenchMark_Hl7
{
    public class Myzh_BenchMark
    {
        //创建日志记录组件实例
        public static log4net.ILog FileLog = log4net.LogManager.GetLogger("FileLog.Logging");
        public static string SendHl7Msg(string id, string BLH, string lk_no, string barcode, string djsj, string XM, string XB, string rs_code, string rs_name, string ip, int port)
        {
            DateTime d;
            d = DateTime.ParseExact(djsj, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
            djsj = d.ToString("yyyyMMddHHmmss");

            StringBuilder Hl7String = new StringBuilder();
            string Hl7Timestamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string Msgxh = Hl7Timestamp + id;
            StringBuilder ACKString = new StringBuilder();
            Hl7String.Append((char)0x0B);
            Hl7String.Append(@"MSH|^~\&|LIS|Lab|VIP|Benchmark|" + Hl7Timestamp + "||OML^O21|" + Msgxh + "|P|2.4|");
            Hl7String.Append((char)0x0D);
            Hl7String.Append("PID||" + lk_no + "|||" + XM + "||19000101|" + XB + "|");
            Hl7String.Append((char)0x0D);
            Hl7String.Append("PV1||||||||");
            Hl7String.Append((char)0x0D);
            Hl7String.Append("SAC|||||||" + djsj + "|");
            Hl7String.Append((char)0x0D);
            Hl7String.Append("ORC|NW|" + BLH + "|");
            Hl7String.Append((char)0x0D);
            Hl7String.Append("OBR|1|" + BLH + "||" + rs_code + "^" + rs_name + "^STAIN|||||||||||||||" + barcode + "^" + id + "|" + lk_no + "^" + id + "|" + BLH + "^" + id + "|");
            Hl7String.Append((char)0x0D);
            Hl7String.Append((char)0x1C);
            Hl7String.Append((char)0x0D);
            string ret = Hl7String.ToString();
            FileLog.Info(string.Format("要发送到免疫组化设备的HL7消息串：{0}", ret));
            TcpSend tcpins = new TcpSend(ip, port);
            ret = tcpins.MLLPSender(ret);
            FileLog.Info(string.Format("免疫组化设备返回HL7响应串：{0}", ret));
            if (ret.Contains("OK"))
            {
                FileLog.Info("免疫组化设备HL7通信成功！");
                return "OK";
            }
            else
            {
                FileLog.Error("免疫组化设备HL7通信失败！");
                return "UA";
            }
        }
    }
}
