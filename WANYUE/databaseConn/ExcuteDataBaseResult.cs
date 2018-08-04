using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service
{
    public class ExcuteDataBaseResult
    {
        private bool sucessd;
        private DataSet ds;

        public bool Sucessd
        {
            get
            {
                return sucessd;
            }

            set
            {
                sucessd = value;
            }
        }

        public DataSet Ds
        {
            get
            {
                return ds;
            }

            set
            {
                ds = value;
            }
        }
    }
}
