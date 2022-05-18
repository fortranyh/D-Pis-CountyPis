using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;

namespace PublicBaseLib
{
    public class PostWebService
    {
        //==========================================================================================
        //有参数的方法调用：
        ////请求参数
        //IDictionary<string, string> parameters = new Dictionary<string, string>();
        ////组织post请求参数（cType值为1是门诊 2是住院）
        //parameters.Add("cType", cType);
        //DataSet ds = PostCallWebService(DtWebServiceUrl, "AdviceCheckType", parameters);
        //==========================================================================================
        //static string DtWebServiceUrl = "http://111.111.111.30:10021/Stand_TjPacsinterface.asmx";
        //没参数的方法调用：
        // DataSet ds = PostCallWebService(DtWebServiceUrl, "DeptInfo", null);
        //==========================================================================================
        //方法1解析xml
        // XmlDocument xmlDoc = new XmlDocument();
        //xmlDoc.LoadXml(xmldata);
        //RetData = xmlDoc.DocumentElement.InnerText;
        //方法2解析xml
        //用dataset导入xml解析
        //ds = new DataSet();
        //ds.ReadXml(new StringReader(xmldata));
        //return ds;
        //==========================================================================================
        /// <summary>
        /// post调用webservice接口
        /// </summary>
        /// <param name="WebServiceUrl">webservice的URL地址</param>
        /// <param name="CallMethod">webservice的方法名</param>
        /// <param name="parameters">参数集合(参数名必须和webservice方法名定义时的参数名称一致)</param>
        /// <returns></returns>
        public static string PostCallWebServiceForXml(string WebServiceUrl, string CallMethod, IDictionary<string, string> parameters)
        {
            string xmldata = "";
            try
            {
                StringBuilder buffer = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                }
                string postData = buffer.ToString();
                buffer = null;
                byte[] dataArray = null;
                //设置编码规格
                if (postData.Length > 0)
                {
                    dataArray = Encoding.UTF8.GetBytes(postData);
                }
                //创建Web请求
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(WebServiceUrl + "/" + CallMethod);
                request2.Method = "Post";
                request2.ContentType = "application/x-www-form-urlencoded";
                if (postData.Length > 0)
                {
                    request2.ContentLength = dataArray.Length;
                    //写入post参数
                    Stream Writer = request2.GetRequestStream();//获取用于写入请求数据的Stream对象
                    Writer.Write(dataArray, 0, dataArray.Length);//把参数数据写入请求数据流
                    Writer.Close();
                }
                else
                {
                    request2.ContentLength = 0;
                }
                //传过来为XML用以下方法解析
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();//获得相应
                Stream stream2 = response2.GetResponseStream();//获取响应流
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                StreamReader sr = new StreamReader(stream2, encoding);
                xmldata = sr.ReadToEnd();
                sr.Close();
                response2.Close();
            }
            catch
            {
                xmldata = "";
            }
            return xmldata;
        }

        public static Stream PostCallWebServiceForStream(string WebServiceUrl, string CallMethod, IDictionary<string, string> parameters)
        {
            Stream StreamIns = null;
            try
            {
                StringBuilder buffer = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                }
                string postData = buffer.ToString();
                buffer = null;
                byte[] dataArray = null;
                //设置编码规格
                if (postData.Length > 0)
                {
                    dataArray = Encoding.UTF8.GetBytes(postData);
                }
                //创建Web请求
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(WebServiceUrl + "/" + CallMethod);
                request2.Method = "Post";
                request2.ContentType = "application/x-www-form-urlencoded";
                if (postData.Length > 0)
                {
                    request2.ContentLength = dataArray.Length;
                    //写入post参数
                    Stream Writer = request2.GetRequestStream();//获取用于写入请求数据的Stream对象
                    Writer.Write(dataArray, 0, dataArray.Length);//把参数数据写入请求数据流
                    Writer.Close();
                }
                else
                {
                    request2.ContentLength = 0;
                }
                //传过来为XML用以下方法解析
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();//获得相应
                StreamIns = response2.GetResponseStream();//获取响应流
            }
            catch
            {
                StreamIns = null;
            }
            return StreamIns;
        }
        public static string PostCallWebServiceForImgFile(string WebServiceUrl, string CallMethod, IDictionary<string, string> parameters, string filePathStr)
        {
            try
            {
                StringBuilder buffer = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                }
                string postData = buffer.ToString();
                buffer = null;
                byte[] dataArray = null;
                //设置编码规格
                if (postData.Length > 0)
                {
                    dataArray = Encoding.UTF8.GetBytes(postData);
                }
                //创建Web请求
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(WebServiceUrl + "/" + CallMethod);
                request2.Method = "Post";
                request2.ContentType = "application/x-www-form-urlencoded";
                if (postData.Length > 0)
                {
                    request2.ContentLength = dataArray.Length;
                    //写入post参数
                    Stream Writer = request2.GetRequestStream();//获取用于写入请求数据的Stream对象
                    Writer.Write(dataArray, 0, dataArray.Length);//把参数数据写入请求数据流
                    Writer.Close();
                }
                else
                {
                    request2.ContentLength = 0;
                }
                //传过来为XML用以下方法解析
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();//获得相应
                Stream StreamIns = response2.GetResponseStream();//获取响应流
                                                                 //创建本地文件写入流
                Stream fs = new FileStream(filePathStr, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = StreamIns.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    fs.Write(bArr, 0, size);
                    size = StreamIns.Read(bArr, 0, (int)bArr.Length);
                }
                fs.Close();
                StreamIns.Close();
                response2.Close();
            }
            catch
            {
                filePathStr = "";
            }
            return filePathStr;
        }
        public static string PostCallWebServiceForTxt(string WebServiceUrl, string CallMethod, IDictionary<string, string> parameters)
        {
            string RetStr = "";
            try
            {
                StringBuilder buffer = new StringBuilder();
                if (parameters != null && parameters.Count > 0)
                {
                    int i = 0;
                    foreach (string key in parameters.Keys)
                    {
                        if (i > 0)
                        {
                            buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                        }
                        else
                        {
                            buffer.AppendFormat("{0}={1}", key, parameters[key]);
                        }
                        i++;
                    }
                }
                string postData = buffer.ToString();
                buffer = null;
                byte[] dataArray = null;
                //设置编码规格
                if (postData.Length > 0)
                {
                    dataArray = Encoding.UTF8.GetBytes(postData);
                }
                //创建Web请求
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(WebServiceUrl + "/" + CallMethod);
                request2.Method = "Post";
                request2.ContentType = "application/x-www-form-urlencoded";
                if (postData.Length > 0)
                {
                    request2.ContentLength = dataArray.Length;
                    //写入post参数
                    Stream Writer = request2.GetRequestStream();//获取用于写入请求数据的Stream对象
                    Writer.Write(dataArray, 0, dataArray.Length);//把参数数据写入请求数据流
                    Writer.Close();
                }
                else
                {
                    request2.ContentLength = 0;
                }
                //返回值用以下方法解析
                HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();//获得相应
                Stream stream2 = response2.GetResponseStream();//获取响应流
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                StreamReader sr = new StreamReader(stream2, encoding);
                RetStr = sr.ReadToEnd();
                sr.Close();
                response2.Close();
            }
            catch
            {
                RetStr = "";
            }
            return RetStr;
        }

        //根据datatable组织xml串
        public static string ConvertDataTableToXML_CDATA(DataTable dt, string rootNode, string childNode)
        {
            StringBuilder strXml = new StringBuilder();
            strXml.Append("<?xml version='1.0' encoding='UTF-8'?>");
            //添加根节点名称
            strXml.AppendFormat("<{0}>", rootNode);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (childNode != "")//子节点为空时不再添加
                        strXml.AppendFormat("<{0}>", childNode);
                    for (int j = 0; j < dt.Columns.Count; j++)//将列名添加为节点名，并填充数据
                    {
                        strXml.AppendFormat("<{0}><![CDATA[{1}]]></{0}>", dt.Columns[j].ColumnName, dt.Rows[i][j].ToString().Trim());
                    }
                    if (childNode != "")
                        strXml.AppendFormat("</{0}>", childNode);
                }
            }
            //添加根节点名称
            strXml.AppendFormat("</{0}>", rootNode);
            string RetStr = strXml.ToString();
            strXml = null;
            return RetStr;//返回Xml字符串
        }

    }
}
