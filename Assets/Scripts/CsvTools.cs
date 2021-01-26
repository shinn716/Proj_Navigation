// Author : Shinn
// Date : 20190918
// Reference : https://sushanta1991.blogspot.com/2015/02/how-to-write-data-to-csv-file-in-unity.html
// 

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Shinn.Common
{
    public static class CsvTools
    {
        public static event ExportCallback eventExportCallback;

        public delegate void ExportCallback();

        public static string GetFullname = string.Empty;

        public static string ReadCsv(string path)
        {
            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    // Read the stream to a string, and write the string to the console.
                    text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (System.NullReferenceException e)
            {
                Debug.Log("資料格式錯誤 " + e);
            }
            catch (System.Exception e)
            {
                Debug.Log("The file could not be read: " + e);
            }
            return text;
        }

        public static void WriteToCsvStr(string csvContent, string saveName)
        {
            string filePath = Path.Combine(AndroidStreamingAssets.Path, saveName + ".csv");

            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                GetFullname = filePath;
                eventExportCallback?.Invoke();
                sw.WriteLine(csvContent);
                sw.Close();
            }
        }

        public static bool ExportStatus()
        {
            return true;
        }
    }
}