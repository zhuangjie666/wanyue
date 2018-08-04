using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.TestConRunJob.PlugIn.service
{
    public class SystemLog
    {
        public static void WriteSystemLog(string logs) {
            FileStream aFile = new FileStream("C:\\cloud\\SystemLog\\systemlog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(aFile);
            StringBuilder sb = new StringBuilder();
            sb.Append("--").Append(DateTime.Now.ToString()).Append("    ").Append(logs);
            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
