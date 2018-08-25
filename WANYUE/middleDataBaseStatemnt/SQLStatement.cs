using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier;
using Kingdee.K3.WANYUE.PlugIn.service.application.voucher;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
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
        //TODO：客户名字更新的暂未考虑
        public static string getCustomerInfoStatement = "select fnumber,fname from t_kf_customer where fstatus =0";
        public static string updateCustomerSucessStatement = "update t_kf_customer set fstatus = 1 ,FLastUpdateTime = '{0}' where fnumber in ('{1}')";
        public static string updateCustomerFailStatement = "update t_kf_customer set fstatus = 2, FLastUpdateTime = '{0}' , FResultMessage = '{1}' where fnumber in ('{2}')";

        public static string updateSupplierSucessStatement = "update t_kf_supplier set fstatus = 1 ,FLastUpdateTime = '{0}' where fnumber in ('{1}')";
        public static string updateSupplierFailStatement = "update t_kf_supplier set fstatus = 2, FLastUpdateTime = '{0}' , FResultMessage = '{1}' where fnumber in ('{2}')";
        public static string getSupplierInfoStatement = "select fnumber,fname,FopenBankName,FbankCode,FbankHolder from t_kf_supplier where fstatus =0";

        public static string getVoucherInfoStatement = "SELECT * FROM t_kf_voucher WHERE fstatus =0";
        public static string updateVoucherSucessStatement = "update t_kf_voucher set fstatus = 1 , FLastUpdateTime = '{0}' , FVOUCHERGROUPNO = '{1}' where fnumber in  ('{2}')";
        public static string updateVoucherFailStatement = "update t_kf_voucher set fstatus = 2 , FLastUpdateTime = '{0}' , FResultMessage = '{1}' where fnumber in  ('{2}')";
        public SQLStatement(string sourceObject)
        {
            this.sourceObject = sourceObject;
        }

        public List<string> getUpdateSQLStatement(string model,Dictionary<string, Result> statusMap)
        {
            List<string> UpdateSQL = new List<string>();
            //string SucessStatement = "";
            //string FailStatement = "";
           

            Dictionary<string, string> sucessList = new Dictionary<string, string>();
            Dictionary<string, string> failList = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Result> kvp in statusMap)
            {
                if (kvp.Value.ResponseStatus.IsSuccess)
                {
                    sucessList.Add(kvp.Key, kvp.Value.Number);
                }
                else
                {
                    string errorInfo = "";
                    StringBuilder sb = new StringBuilder();
                    sb.Append("错误代码=").Append(kvp.Value.ResponseStatus.ErrorCode).Append(",错误字段:");
                    sb.Append(kvp.Value.ResponseStatus.Errors[0].FieldName).Append(",错误原因=").Append(kvp.Value.ResponseStatus.Errors[0].Message);
                    if (sb.ToString().Length > 1024)
                    {
                        errorInfo = sb.ToString().Substring(0, 1024);
                    }
                    else
                    {
                        errorInfo = sb.ToString();
                    }
                    failList.Add(kvp.Key, errorInfo);
                }
            }
            string SQLstring = "";

            //if (CustomerSQLObject.Customer.Equals(model))
            //{
            //    SucessStatement = updateCustomerSucessStatement;
            //    FailStatement = updateCustomerFailStatement;
            //}
            //if (SupplierSQLObject.Supplier.Equals(sourceObject))
            //{
            //    SucessStatement = updateSupplierSucessStatement;
            //    FailStatement = updateSupplierFailStatement;
            //}
            //if (VouchSQLObject.Vouch.Equals(sourceObject))
            //{
            //    SucessStatement = updateVoucherSucessStatement;
            //    FailStatement = updateVoucherFailStatement;
            //}

            if (sucessList.Count != 0)
            {
                foreach (KeyValuePair<string, string> kvp in sucessList)
                {
                    if (string.Equals(CustomerSQLObject.Customer, model, StringComparison.CurrentCultureIgnoreCase)) {
                        SQLstring = string.Format(updateCustomerSucessStatement, System.DateTime.Now, kvp.Key);
                    }
                    if (string.Equals(SupplierSQLObject.Supplier, model, StringComparison.CurrentCultureIgnoreCase)) {
                        SQLstring = string.Format(updateSupplierSucessStatement, System.DateTime.Now, kvp.Key);
                    }
                    if (string.Equals(VouchSQLObject.Vouch, model, StringComparison.CurrentCultureIgnoreCase)) {
                        SQLstring = string.Format(updateVoucherSucessStatement, System.DateTime.Now, kvp.Value, string.Join("','", kvp.Key));
                    }
                    UpdateSQL.Add(SQLstring);
                }
            }

            if (failList.Count != 0)
            {
                foreach (KeyValuePair<string, string> kvp in failList)
                {

                    if (string.Equals(CustomerSQLObject.Customer, model, StringComparison.CurrentCultureIgnoreCase))
                    {
                        SQLstring = string.Format(updateCustomerFailStatement, System.DateTime.Now, kvp.Value, string.Join("','", kvp.Key));
                    }
                    if (string.Equals(SupplierSQLObject.Supplier, model, StringComparison.CurrentCultureIgnoreCase))
                    {
                        SQLstring = string.Format(updateSupplierFailStatement, System.DateTime.Now, kvp.Value, string.Join("','", kvp.Key));
                    }
                    if (string.Equals(VouchSQLObject.Vouch, model, StringComparison.CurrentCultureIgnoreCase))
                    {
                        SQLstring = string.Format(updateVoucherFailStatement, System.DateTime.Now, kvp.Value, string.Join("','", kvp.Key));
                    }
                    UpdateSQL.Add(SQLstring);
                }
                //string SQLstring = string.Format(updateCustomerSucessStatement,string.Join("','", sucessList));
                //UpdateSQL.Add(SQLstring);
                //SQLstring = string.Format(updateCustomerFailStatement, string.Join("','", failList));
                //UpdateSQL.Add(SQLstring);
            }
            return UpdateSQL;
        }

        //public List<string> getUpdateSQLStatement4Supplier(Dictionary<string, bool> statusMap)
        //{
        //    List<string> UpdateSQL = new List<string>();
        //    List<string> sucessList = new List<string>();
        //    List<string> failList = new List<string>();
        //    foreach (KeyValuePair<string, bool> kvp in statusMap)
        //    {
        //        if (kvp.Value)
        //        {
        //            sucessList.Add(kvp.Key);
        //        }
        //        else
        //        {
        //            failList.Add(kvp.Key);
        //        }
        //    }

        //    string SQLstring = string.Format(updateSupplierSucessStatement, string.Join("','", sucessList));
        //    UpdateSQL.Add(SQLstring);
        //    SQLstring = string.Format(updateSupplierFailStatement, string.Join("','", failList));
        //    UpdateSQL.Add(SQLstring);

        //    return UpdateSQL;
        //}

        //public List<string> getUpdateSQLStatement4Voucher(Dictionary<string, Result> statusMap)
        //{
        //    List<string> UpdateSQL = new List<string>();
        //    Dictionary<string,string> sucessList = new Dictionary<string,string>();
        //    Dictionary<string,string> failList = new Dictionary<string,string>();
        //    HashSet<string> sucessValue = new HashSet<string>();
        //    HashSet<string> failValue = new HashSet<string>();
        //    foreach (KeyValuePair<string, Result> kvp in statusMap)
        //    {
        //        if (kvp.Value.ResponseStatus.IsSuccess)
        //        {
        //            sucessList.Add(kvp.Key, kvp.Value.Number);
        //        }
        //        else
        //        {
        //            failList.Add(kvp.Key, "错误代码=" + kvp.Value.ResponseStatus.ErrorCode + "错误字段:"+kvp.Value.ResponseStatus.Errors[0].FieldName +"错误原因="+ kvp.Value.ResponseStatus.Errors[0].Message);
        //        }
        //    }
        //    string SQLstring = "";
        //    if (sucessList.Count != 0) {
        //        foreach (KeyValuePair<string, string> kvp in sucessList)
        //        {
        //            SQLstring = string.Format(updateVoucherSucessStatement, System.DateTime.Now, kvp.Value, string.Join("','", kvp.Key));
        //            UpdateSQL.Add(SQLstring);
        //        }
        //    }

        //    if (failList.Count != 0) {
        //        foreach (KeyValuePair<string, string> kvp in failList) {
        //            SQLstring = string.Format(updateVoucherFailStatement, System.DateTime.Now, kvp.Value, string.Join("','", kvp.Key));
        //            UpdateSQL.Add(SQLstring);
        //        }
                    
        //    }
            
            

        //    return UpdateSQL;
        //}


        public string returnSQLStatement(string sourceObject) {
            if (CustomerSQLObject.Customer.Equals(sourceObject)) {
                return getCustomerInfoStatement;
            }
            if (SupplierSQLObject.Supplier.Equals(sourceObject)) {
                return getSupplierInfoStatement;
            }
            if (VouchSQLObject.Vouch.Equals(sourceObject)) {
                return getVoucherInfoStatement;
            }
            return "";
        }

    }
}
