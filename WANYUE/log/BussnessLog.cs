using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service
{
    public class BussnessLog
    {
        public static void WriteBussnessLog(string logs)
        {
            FileStream aFile = new FileStream("C:\\cloud\\BussnessLog\\bussnesslog.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(aFile);
            StringBuilder sb = new StringBuilder();
            sb.Append("--").Append(DateTime.Now.ToString()).Append("    ").Append(logs);
            sw.WriteLine(sb);
            sw.Close();
        }
    }
}
