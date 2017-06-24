using OgarCommon.Win32Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgarCommon
{
    public class IniFile
    {
        public string path;


        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <PARAM name="INIPath"></PARAM>
        public IniFile(string INIPath)
        {
            path = INIPath;
        }


        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void Write(string section, string key, string value)
        {
            kernel32.WritePrivateProfileString(section, key, value, this.path);
        }

        public void Write<T>(string section, string key, T value)
        {
            kernel32.WritePrivateProfileString(section, key, string.Format("{0}", value), this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string Read(string section, string key, string defaultVal = "")
        {
            StringBuilder temp = new StringBuilder(1024);
            int i = kernel32.GetPrivateProfileString(section, key, defaultVal, temp,
                                            1024, this.path);
            return temp.ToString();
        }

        public int ReadInt(string section, string key, int defaultVal = 0)
        {
            var textVal = Read(section, key, defaultVal.ToString());
            int val = defaultVal;
            if (int.TryParse(textVal, out val))
                return val;
            return defaultVal;
        }

        public float ReadSingle(string section, string key, float defaultVal = 0)
        {
            var textVal = Read(section, key, defaultVal.ToString());
            float val = defaultVal;
            if (float.TryParse(textVal, out val))
                return val;
            return defaultVal;
        }

        public double ReadDouble(string section, string key, double defaultVal = 0)
        {
            var textVal = Read(section, key, defaultVal.ToString());
            double val = defaultVal;
            if (double.TryParse(textVal, out val))
                return val;
            return defaultVal;
        }
    }
}
