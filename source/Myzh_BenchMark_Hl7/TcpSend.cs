using System;
using System.Net.Sockets;
using System.Text;

namespace Myzh_BenchMark_Hl7
{
    public class TcpSend
    {
        //创建日志记录组件实例
        public static log4net.ILog FileLog = log4net.LogManager.GetLogger("FileLog.Logging");
        public TcpSend(string ip, int port)
        {
            IP = ip;
            PORT = port;
        }
        private string IP = "127.0.0.1";

        private int PORT = 10101;

        public string MLLPSender(string msg)
        {
            string ret = string.Empty;
            ret = TCPSender(msg);
            return ret;
        }

        public string TCPSender(string msg)
        {
            string ret = string.Empty;

            int BufferSize = 4096;
            TcpClient tcp = new TcpClient();
            tcp.Connect(IP, PORT);
            NetworkStream streamToServer = tcp.GetStream();

            byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(msg); //msg为发送的字符串   
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] == 0)
                {
                    buffer[i] = 32;
                }
            }

            try
            {
                lock (streamToServer)
                {
                    streamToServer.Write(buffer, 0, buffer.Length);     // 发往服务器
                }

                //接收字符串

                buffer = new byte[BufferSize];

                lock (streamToServer)
                {
                    int bytesRead = streamToServer.Read(buffer, 0, BufferSize);
                }
                ret = Encoding.GetEncoding("UTF-8").GetString(buffer);
                tcp.Close();
            }
            catch (Exception ex)
            {
                FileLog.Error(ex.Message);
                ret = string.Empty;
            }
            return ret;
        }
    }
}
