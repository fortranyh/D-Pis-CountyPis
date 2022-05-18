using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml;

namespace PublicBaseLib
{
    public class JsonHelper
    {
        public static string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T JsonTo<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static Dictionary<string, string> GetDictFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                dict.Add(node.Name, node.InnerText.Trim());
            }
            return dict;
        }
    }
}
