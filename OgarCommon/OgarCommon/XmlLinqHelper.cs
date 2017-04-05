using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace OgarCommon
{
    /// <summary>
    /// 使用xmlLINQ来操作xml
    /// </summary>
    public static class XmlLinqHelper
    {
        /// <summary>
        /// 加载一个XML文件
        /// </summary>
        /// <param name="fileName"></param>
        /// 
        public static T LoadXML<T>(string path ,string fileName,T notFoundT)
        {
            T t = Activator.CreateInstance<T>();// 创建一个T类型的对象。

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var LoadPath = path + "\\" + fileName;

            if (!File.Exists(LoadPath))
            {
                SaveXML(notFoundT, path, fileName);
                throw new Exception("xml配置文件未设置参数！");
            }

            XDocument doc = XDocument.Load(LoadPath);
            var root = doc.Element("Settings");// 获取根节点

            if (root == null)
            {
                throw new Exception("错误的XML根节点");
            }

            Type type = t.GetType();
            string msg = string.Empty;
            foreach (var p in type.GetProperties())
            {
                try
                {
                    object value = root.Element(p.Name).Value;

                    int intValue;
                    if(int.TryParse(value.ToString(),out intValue))
                    {
                        p.SetValue(t, intValue, null);
                    }
                    else
                    {
                        p.SetValue(t, value, null);
                    }
                }
                catch (Exception ex)
                {
                    root.Add(new XElement(p.Name, "[" + p.Name.ToString() + "]"));
                    msg += ex.Message + "\n" + "详细信息：未命中XML:'"+ p.Name +"'属性节点\n";
                }
            }

            if (!string.IsNullOrEmpty(msg))
            {
                doc.Save(fileName);
                throw new Exception(msg);
            }

            return t;
        }

        /// <summary>
        /// 用linqXml保存一个XML文件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void SaveXML(object obj, string path , string fileName)
        {
            Type t = obj.GetType();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var savePath = path + "\\" + fileName;

            XElement doc = new XElement(new XElement("Settings"));// 创建XML根节点

            foreach (PropertyInfo p in t.GetProperties())
            {
                var objs = p.GetValue(obj, null);
                doc.Add(new XElement(p.Name, objs));// 在根节点中添加节点
            }

            doc.Save(savePath);
        }
    }
}
