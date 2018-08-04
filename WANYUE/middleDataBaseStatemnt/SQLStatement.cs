using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt
{
   public class SQLStatement
    {
        private string sourceObject;
        public static string getCustomerInfoStatement = "select * from T_BD_MATERIAL";
        public static string getSupplierInfoStatement = "SELECT * FROM T_BD_MATERIAL";
        public SQLStatement(string sourceObject)
        {
            this.sourceObject = sourceObject;
        }

        public string returnSQLStatement(string sourceObject) {
            if (CustomerSQLObject.Customer.Equals(sourceObject)) {
                return getCustomerInfoStatement;
            }
            return "";
        }

    }
}
