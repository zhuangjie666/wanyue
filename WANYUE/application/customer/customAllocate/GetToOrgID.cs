using Kingdee.BOS;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.ServiceHelper;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.customAllocate
{
    public static class GetToOrgID
    {
        public static string SQL4currOrg = "";
        public static Dictionary<string, string> getToOrgID(Context ctx,List<string> numbers,string model)
        {
            if (SupplierSQLObject.Supplier.Equals(model))
            {
                SQL4currOrg = "select FSUPPLIERID,FUSEORGID from t_BD_Supplier where fnumber = '{0}'";
            }
            else {
                SQL4currOrg = "select FCUSTID,FUSEORGID from T_BD_CUSTOMER where fnumber = '{0}'";
            }
             
            string SQL4AllOrg = "select FORGID from T_ORG_Organizations";
            string currOrgID = "";
          //  string[] fmaterID = new string[] { };
            string fmaterID = "";
            List<string> orgs = new List<string>();
            foreach (string number in numbers)
            {
                string executeSQL = string.Format(SQL4currOrg, number);
                DynamicObjectCollection ds = DBServiceHelper.ExecuteDynamicObject(ctx, executeSQL);
                foreach (DynamicObject doname in ds)
                {
                    currOrgID = doname["FUSEORGID"].ToString();
                    if (SupplierSQLObject.Supplier.Equals(model))
                    {
                        fmaterID = doname["FSUPPLIERID"].ToString();
                    }
                    else {
                        fmaterID = doname["FCUSTID"].ToString();
                    }
                }

                DynamicObjectCollection dynamicObjectCollection = DBUtils.ExecuteDynamicObject(ctx, SQL4AllOrg);
                foreach (DynamicObject allorg in dynamicObjectCollection)
                {
                    orgs.Add(allorg["FORGID"].ToString());
                }
                orgs.Remove(currOrgID);
                Dictionary<string, string> outResult = new Dictionary<string, string>();
                outResult.Add(fmaterID, string.Join(",",orgs.ToArray()));
                return outResult;
            }
            return null;
        }
    }
}
