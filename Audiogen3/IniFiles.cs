using System;
using System.Runtime.InteropServices;
using System.Text;
namespace Audiogen3 {
    public static class IniFiles {
        /// <summary>
        /// Reading of INI Files
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Default"></param>
        /// <param name="RetVal"></param>
        /// <param name="Size"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
        [DllImport("kernel32.dll")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        public static string ReadINI(string file, string section, string key, string _default = "") {
            try {
                var msg = new StringBuilder(500);
                if (GetPrivateProfileString(section, key, "", msg, msg.Capacity, file) == 0) {
                    return _default;
                } else {
                    return msg.ToString().Trim();
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Writing of INI Files
        /// </summary>
        /// <param name="file"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void WriteINI(string file, string section, string key, string value) {
            try {
                WritePrivateProfileString(section, key, value, file);
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}