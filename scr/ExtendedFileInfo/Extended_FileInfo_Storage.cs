using System.Collections.Generic;
using System.IO;

namespace vshed.IO
{
    public static class Extended_FileInfo_Storage
    {
        private static Dictionary<object, Dictionary<string, object>> ExtendedStorage = new Dictionary<object, Dictionary<string, object>>();
        private static bool PropertyExists(this FileInfo fileInfo, string name) { return (ExtendedStorage.ContainsKey(fileInfo) && ExtendedStorage[fileInfo].ContainsKey(name)); }
        public static object GetValue(this FileInfo fileInfo, string name)
        {
            if (fileInfo.PropertyExists(name))
                return ExtendedStorage[fileInfo][name];
            return null;
        }
        public static void SetValue(this FileInfo fileInfo, string name, object value)
        {
            if (!ExtendedStorage.ContainsKey(fileInfo))
                ExtendedStorage[fileInfo] = new Dictionary<string, object>();
            ExtendedStorage[fileInfo][name] = value;
        }
        public static void DeleteValue(this FileInfo fileInfo, string name)
        {
            if (fileInfo.PropertyExists(name)) ExtendedStorage[fileInfo].Remove(name);
            if (ExtendedStorage[fileInfo].Count == 0) ExtendedStorage.Remove(fileInfo);
        }
        public static void DeleteAllValues(this FileInfo fileInfo)
        { if (ExtendedStorage.ContainsKey(fileInfo)) { ExtendedStorage.Remove(fileInfo); } }
    }
}
