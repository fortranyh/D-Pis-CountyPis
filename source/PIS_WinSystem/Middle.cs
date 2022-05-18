namespace PIS_Sys
{
    public class Middle
    {
        // 创建一个委托，返回类型为void，两个参数
        public delegate void SendMessage(string sender, string e);
        // 将创建的委托和特定事件关联,在这里特定的事件为DoSendMessage
        public static event SendMessage DoSendMessage;
        public static void Run(string sender, string e)
        {
            DoSendMessage(sender, e);
        }
    }
}
