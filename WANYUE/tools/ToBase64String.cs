using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.tools
{
   public static class ToBase64String
    {
        public static string ToBase64(string input)
        {
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(input));
        }
    }
}
