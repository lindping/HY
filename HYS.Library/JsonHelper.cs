using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Library
{
    public class JsonHelper
    {
        public static string ReadFileJson(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
                {
                    json = sr.ReadToEnd();
                }
            }
            return json;
        }

        public static void SaveFileJson(string json, string filepath)
        {          

            FileStream fs = new FileStream(filepath, FileMode.Create);

            //把json数据写到文件  
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("utf-8")))
            {
                try
                {
                    sw.Write(json);
                    sw.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }
}
