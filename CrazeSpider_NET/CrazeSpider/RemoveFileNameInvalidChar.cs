using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CrazeSpider
{
    public static class RemoveFileNameInvalidChar
    {
        public static string RemoveFileNameInvalidChars(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;

            string invalidChars = new string(Path.GetInvalidFileNameChars());
            string invalidReStr = string.Format("[{0}]", Regex.Escape(invalidChars));
            return Regex.Replace(fileName, invalidReStr, "");

        }

        public static string RemovePathInvalidChars(this string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return filePath;

            string invalidChars = new string(Path.GetInvalidPathChars());
            string invalidReStr = string.Format("[{0}]", Regex.Escape(invalidChars));
            return Regex.Replace(filePath, invalidReStr, "");
        }
    }
}
