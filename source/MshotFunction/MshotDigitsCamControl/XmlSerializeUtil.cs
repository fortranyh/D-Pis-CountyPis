using System;
using System.IO;
using System.Xml.Serialization;

namespace MshotDigitsCamControl
{

    /*
     *==============================使用示例====================================
    
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@path+"../../test.xml");
            Console.WriteLine(xmlDoc.OuterXml);
            CardInfo cardInfoIns = XmlSerializeUtil.Deserialize(typeof(CardInfo), xmlDoc.OuterXml) as CardInfo;
            Console.WriteLine(cardInfoIns.Head.DatagramId);
            Console.WriteLine(cardInfoIns.Head.Fi);
            string xml = XmlSerializeUtil.Serializer(typeof(CardInfo), cardInfoIns);
            Console.WriteLine(xml);
    *============================================================================
    */
    /// <summary>
    /// <remarks>Xml序列化与反序列化</remarks>
    /// <creator>zhangdapeng</creator>
    /// </summary>
    public class XmlSerializeUtil
    {
        #region 反序列化
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }
        #endregion

        #region 序列化
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            //序列化对象
            xml.Serialize(Stream, obj);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            Stream.Dispose();
            return str;
        }

        #endregion
    }
}
