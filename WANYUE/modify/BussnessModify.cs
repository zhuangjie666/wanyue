using Kingdee.BOS;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.ServiceHelper;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.modify
{
    public class BussnessModify
    {
        public string SQLStatement = "";
        public static List<string> CustomerFnunbers= new List<string>();
        public static List<string> SupplierFnumbers = new List<string>();
        public string UpdateSQLStatement = "";
        public string UpdateSQLStatement1 = "";
        public string UpdateSQLStatement2 = "";
        public string UpdateSQLStatement3 = "";
        public string UpdateSQLStatement4 = "";
        public  List<string> UpdateSQLStatementList = new List<string>();
        public void excuteSQL(string model, ConnectionResult result, Context ctx)
        {
            BussnessLog.WriteBussnessLog("", model + "更新", "开始更新");
            if (string.Equals(CustomerSQLObject.Customer, model, StringComparison.CurrentCultureIgnoreCase))
            {
                SQLStatement = "SELECT FNUMBER,FNAME FROM T_KF_CUSTOMER WHERE FSTATUS = 3";
                UpdateSQLStatement = "UPDATE T_BD_CUSTOMER_L SET FNAME = '{0}' WHERE FCUSTID = (SELECT FCUSTID FROM T_BD_CUSTOMER WHERE FNUMBER = '{1}')";
                UpdateSQLStatementList.Add(UpdateSQLStatement);
            }
            if (string.Equals(SupplierSQLObject.Supplier, model, StringComparison.CurrentCultureIgnoreCase))
            {
                SQLStatement = "SELECT FNUMBER,FNAME,FOPENBANKNAME,FBANKCODE FROM T_KF_SUPPLIER WHERE FSTATUS = 3";
                UpdateSQLStatement1 = " UPDATE T_BD_SUPPLIER_L SET FNAME = '{0}' WHERE FSUPPLIERID IN (SELECT FSUPPLIERID FROM T_BD_SUPPLIER WHERE FNUMBER='{1}')";
                UpdateSQLStatement2 = " UPDATE T_BD_SUPPLIERBANK SET FBANKCODE = '{0}' WHERE FSUPPLIERID IN (SELECT FSUPPLIERID FROM T_BD_SUPPLIER WHERE FNUMBER='{1}')";
                UpdateSQLStatement3 = "UPDATE T_BD_SUPPLIERBANK_L SET FOPENBANKNAME = '{0}' WHERE FBANKID IN (SELECT  FBANKID FROM T_BD_SUPPLIER T1 LEFT JOIN T_BD_SUPPLIERBANK T2 ON T1.FSUPPLIERID = T2.FSUPPLIERID WHERE T1.FNUMBER ='{1}')";
                UpdateSQLStatementList.Add(UpdateSQLStatement1);
                UpdateSQLStatementList.Add(UpdateSQLStatement2);
                UpdateSQLStatementList.Add(UpdateSQLStatement3);
            }
            RemoteExcuteDataBase dbOjbect = new RemoteExcuteDataBase(DataBaseInfo.IP, DataBaseInfo.database, DataBaseInfo.userid, DataBaseInfo.password);
            ExcuteDataBaseResult dataResult = dbOjbect.excuteStatement(SQLStatement, result.Sqlconn);

            if (dataResult.Ds.Tables[0].Rows.Count <= 0) {
                BussnessLog.WriteBussnessLog("", model+"更新", "无字段需要更新");
                return;
            }
            //取数
            Dictionary<string, UpdateFields> updateMap = getUpdateFields(model, dataResult);
            //更新金蝶数据库
            if (updateKingDeeDataBase(model, UpdateSQLStatementList, updateMap, ctx))
            {
                //更新中间表数据库
                updateMiddleDataBase(model, dataResult, result, dbOjbect);
                BussnessLog.WriteBussnessLog("", model + "更新", "更新金蝶数据库成功！");
            }
           

            //关闭
            //if (dbOjbect.closeConnection(result.Sqlconn))
            //{
            //    BussnessLog.WriteBussnessLog("", model + "更新", "更新结束,关闭数据库连接成功!");
            //}
            //else {
            //    BussnessLog.WriteBussnessLog("", model + "更新", "更新结束,关闭数据库连接失败!");
            //}
            
        }

        public void updateMiddleDataBase(string model, ExcuteDataBaseResult dataResult, ConnectionResult result, RemoteExcuteDataBase dbOjbect)
        {
            if (string.Equals(CustomerSQLObject.Customer, model, StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (string s in CustomerFnunbers) {
                    SQLStatement = "UPDATE T_KF_CUSTOMER SET FSTATUS = 1, FResultMessage=' ', FLASTUPDATETIME='{0}' WHERE FNUMBER = '{1}'";
                    string mSQL = string.Format(SQLStatement, System.DateTime.Now,s);
                    ExcuteDataBaseResult excuteResult = dbOjbect.excuteStatement(mSQL, result.Sqlconn);
                    if (!excuteResult.Sucessd)
                    {
                        BussnessLog.WriteBussnessLog("", model + "更新", "更新中间表失败! fnumber="+s);
                    }
                    else
                    {
                        BussnessLog.WriteBussnessLog("", model + "更新", "更新中间表成功!");
                    }
                }
            }
            if (string.Equals(SupplierSQLObject.Supplier, model, StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (string s in SupplierFnumbers) {
                    SQLStatement = "UPDATE T_KF_SUPPLIER SET FSTATUS = 1, FResultMessage=' ', FLastUpdateTime = '{0}'  WHERE FNUMBER = '{1}'";
                    string mSQL = string.Format(SQLStatement, System.DateTime.Now, s);
                    ExcuteDataBaseResult excuteResult = dbOjbect.excuteStatement(mSQL, result.Sqlconn);
                    if (!excuteResult.Sucessd)
                    {
                        BussnessLog.WriteBussnessLog("", model + "更新", "更新中间表失败! fnumber = "+s);
                    }
                    else
                    {
                        BussnessLog.WriteBussnessLog("", model + "更新", "更新中间表成功!");
                    }
                }
            }
        }

        public bool updateKingDeeDataBase(string model, List<string> SQLStatement, Dictionary<string, UpdateFields> updateMap, Context ctx)
        {
            List<string> statementList = new List<string>();
            foreach (KeyValuePair<string, UpdateFields> kvp in updateMap)
            {
                if (string.Equals(CustomerSQLObject.Customer, model, StringComparison.CurrentCultureIgnoreCase))
                {
                    CustomerUpdateFields customerUpdateFields = (CustomerUpdateFields)kvp.Value;
                    string statement = string.Format(SQLStatement[0], customerUpdateFields.FName, kvp.Key);
                    statementList.Add(statement);
                    CustomerFnunbers.Add(kvp.Key);
                }
                if (string.Equals(SupplierSQLObject.Supplier, model, StringComparison.CurrentCultureIgnoreCase))
                {
                    SupplierUpdateFields supplierUpdateFields = (SupplierUpdateFields)kvp.Value;
                    string statement1 = string.Format(SQLStatement[0], supplierUpdateFields.FName, kvp.Key);
                    string statement2 = string.Format(SQLStatement[1], supplierUpdateFields.FbankCode, kvp.Key);
                    string statement3 = string.Format(SQLStatement[2], supplierUpdateFields.FopenBankName, kvp.Key);
                    SupplierFnumbers.Add(kvp.Key);

                    using (KDTransactionScope trans = new KDTransactionScope(System.Transactions.TransactionScopeOption.Required))
                    {
                        if (DBServiceHelper.Execute(ctx, statement1) > 0)
                        {
                            if (DBServiceHelper.Execute(ctx, statement2) > 0)
                            {
                                if (DBServiceHelper.Execute(ctx, statement3) > 0)
                                {
                                    BussnessLog.WriteBussnessLog("", model + "更新", "更新金蝶数据库成功! fnumber=" + kvp.Key);
                                }
                                else
                                {
                                    BussnessLog.WriteBussnessLog("", model + "更新", "更新金蝶数据库失败! fnumber=" + kvp.Key);
                                }
                            }
                        }
                        trans.Complete();
                    }
                }
            }

            if (DBServiceHelper.ExecuteBatch(ctx, statementList) > 0)
            {
                foreach (string s in statementList)
                {
                    BussnessLog.WriteBussnessLog("", model + "更新", "更新金蝶数据库成功! 更新语句=" + s);
                }
            }
            else {
                foreach (string s in statementList) {
                    BussnessLog.WriteBussnessLog("", model + "更新", "更新金蝶数据库失败! 更新语句=" + s);
                }
            }
            
            return true;
        }

        private Dictionary<string, UpdateFields> getUpdateFields(string model, ExcuteDataBaseResult dataResult)
        {
            Dictionary<string, UpdateFields> updateFieldMap = new Dictionary<string, UpdateFields>();
            if (string.Equals(CustomerSQLObject.Customer, model, StringComparison.CurrentCultureIgnoreCase))
            {
                for (int i = 0; i < dataResult.Ds.Tables[0].Rows.Count; i++)
                {
                    CustomerUpdateFields customerUpdateFields = new CustomerUpdateFields();
                    customerUpdateFields.FName = dataResult.Ds.Tables[0].Rows[i]["FName"].ToString();
                    string key = dataResult.Ds.Tables[0].Rows[i]["FNumber"].ToString();
                    updateFieldMap.Add(key,customerUpdateFields);
                }
            }
            if (string.Equals(SupplierSQLObject.Supplier, model, StringComparison.CurrentCultureIgnoreCase))
            {
                for (int i = 0; i < dataResult.Ds.Tables[0].Rows.Count; i++)
                {
                    SupplierUpdateFields supplierUpdateFields = new SupplierUpdateFields();
                    supplierUpdateFields.FName = dataResult.Ds.Tables[0].Rows[i]["FName"].ToString();
                    supplierUpdateFields.FopenBankName = dataResult.Ds.Tables[0].Rows[i]["FopenBankName"].ToString();
                    supplierUpdateFields.FbankCode = dataResult.Ds.Tables[0].Rows[i]["FbankCode"].ToString();
                    string key = dataResult.Ds.Tables[0].Rows[i]["FNumber"].ToString();
                    updateFieldMap.Add(key, supplierUpdateFields);
                }
            }
            return updateFieldMap;
        }
    }
}
