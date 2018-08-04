using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service
{
    public class ConnectionResult
    {
        private bool sucessd;
        private SqlConnection sqlconn;

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

        public SqlConnection Sqlconn
        {
            get
            {
                return sqlconn;
            }

            set
            {
                sqlconn = value;
            }
        }
    }
}
