using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.initconfig
{
   public class BaseInfo
    {
        private static string modelList;

        public static string ModelList
        {
            get
            {
                return modelList;
            }

            set
            {
                modelList = value;
            }
        }
    }
}
