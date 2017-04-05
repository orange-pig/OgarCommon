using System;
using System.Xml;

namespace OgarCommon
{
    /// <summary>
    /// XmlHelper
    /// </summary>
    public static class XmlHelper
    {
        #region

        /// <summary>
        /// Gets the attribute to string.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static string GetAttributeToString(XmlNode node, string attributeName)
        {
            return GetValue(node, attributeName);
        }

        /// <summary>
        /// Gets the attribute to string.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetAttributeToString(XmlNode node, string attributeName, string defaultValue)
        {
            string val = GetAttributeToString(node, attributeName);

            return val ?? defaultValue;
        }


        /// <summary>
        /// Gets the attribute to boolean.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static bool GetAttributeToBoolean(XmlNode node, string attributeName)
        {
            string val = GetValue(node, attributeName);
            return Boolean.Parse(val);
        }


        /// <summary>
        /// Gets the attribute to boolean.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public static bool GetAttributeToBoolean(XmlNode node, string attributeName, bool defaultValue)
        {
            string val = GetValue(node, attributeName);
            if (val == null)
            {
                return defaultValue;
            }
            try
            {
                return Boolean.Parse(val);
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Gets the attribute to int32.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static int GetAttributeToInt32(XmlNode node, string attributeName)
        {
            string val = GetValue(node, attributeName);
            int result = Convert.ToInt32(val);
            return result;
        }


        /// <summary>
        /// Gets the attribute to int32.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static int GetAttributeToInt32(XmlNode node, string attributeName, int defaultValue)
        {
            string val = GetValue(node, attributeName);
            if (val == null)
            {
                return defaultValue;
            }

            try
            {
                int result = Convert.ToInt32(val);
                return result;
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Gets the attribute to bytes.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static byte[] GetAttributeToBytes(XmlNode node, string attributeName)
        {
            string val = GetValue(node, attributeName);
            byte[] result = Convert.FromBase64String(val);
            return result;
        }


        /// <summary>
        /// Gets the attribute to bytes.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static byte[] GetAttributeToBytes(XmlNode node, string attributeName, byte[] defaultValue)
        {
            string val = GetValue(node, attributeName);
            if (val == null)
            {
                return defaultValue;
            }

            try
            {
                byte[] result = Convert.FromBase64String(val);
                return result;
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="val">The val.</param>
        public static void SetAttribute(XmlNode node, string name, string val)
        {
            XmlNode paramNode = node.Attributes.GetNamedItem(name);
            if (paramNode != null)
            {
                node.Attributes[name].Value = val;
            }
            else
            {
                XmlAttribute attr = node.OwnerDocument.CreateAttribute(name);
                attr.Value = val;
                node.Attributes.Append(attr);
            }
        }


        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="val">if set to <c>true</c> [val].</param>
        public static void SetAttribute(XmlNode node, string name, bool val)
        {
            SetAttribute(node, name, val.ToString());
        }


        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="val">The val.</param>
        public static void SetAttribute(XmlNode node, string name, int val)
        {
            SetAttribute(node, name, val.ToString());
        }


        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        /// <param name="val">The val.</param>
        public static void SetAttribute(XmlNode node, string name, byte[] val)
        {
            SetAttribute(node, name, Convert.ToBase64String(val));
        }

        #endregion


        /// <summary>
        /// Sets the child node.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="childNode">The child node.</param>
        public static void SetChildNode(XmlNode currentNode, XmlNode childNode)
        {
            if (childNode != null)
            {
                //currentNode.InnerXml = childNode.InnerXml;
                currentNode.RemoveAll();
                currentNode.AppendChild(childNode);
            }
        }


        /// <summary>
        /// Sets the child node.
        /// </summary>
        /// <param name="currentNode">The current node.</param>
        /// <param name="childXml">The child XML.</param>
        public static void SetChildNode(XmlNode currentNode, string childXml)
        {
            if (childXml != null)
            {
                currentNode.InnerXml = childXml;
            }
        }


        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        private static string GetValue(XmlNode node, string attributeName)
        {
            string val = null;

            XmlNode paramNode = node.Attributes.GetNamedItem(attributeName);
            if (paramNode != null)
            {
                XmlAttribute attr = (XmlAttribute)paramNode;
                val = attr.Value;
            }

            return val;
        }
    }
}
