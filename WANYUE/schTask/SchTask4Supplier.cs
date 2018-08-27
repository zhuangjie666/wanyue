using Kingdee.BOS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer.customSave;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke;
using System.Data;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using Kingdee.K3.WANYUE.PlugIn.service.application.customAllocate;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierAllocate;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierSave;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using Kingdee.K3.WANYUE.PlugIn.service.modify;
using System.Data.SqlClient;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Supplier : SchTask
    {
        public string formID = "BD_Supplier";
        public string SQLStatement;
        public bool CallResult = false;
        public List<SupplierInfoSaveObject> supplierSaveInfoObjList = null;
        string[] opearteList = { };
        public static string model = "supplier";

        public override void Run(Context ctx, Schedule schedule)
        {
            SQLStatement = base.GetStatement(SupplierSQLObject.Supplier);
            opearteList = GetOpearteList(SupplierSQLObject.Supplier);
            //修改的单独处理
            BussnessModify customerModify = new BussnessModify();
            customerModify.excuteSQL(model, connectionToRemoteDatabase(), ctx);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                supplierSaveInfoObjList = handleData<SupplierInfoSaveObject>(excuteDataBaseResult.Ds);
                if (!closeConnetction(connectionToRemoteDatabase().Sqlconn))
                {
                    //打印关闭连接
                    BussnessLog.WriteBussnessLog("", "数据库操作错误", model+"中间表取数后,关闭数据库连接失败! 请联系系统管理员!");
                    return;
                }
            }
            else
            {
                BussnessLog.WriteBussnessLog("", "数据库操作错误", model+"中间表取数前打开数据库连接失败! 请联系系统管理员!");
                return;
                
            }

            if (LoginAPI(model))
            {
                Dictionary<string, Result> statusMap = new Dictionary<string, Result>();
                foreach (SupplierInfoSaveObject supplierSaveInfoObj in supplierSaveInfoObjList)
                {
                    InvokeReturnHandle<InvokeResult> callResult = (InvokeReturnHandle<InvokeResult>)(object)InvokeAPI(opearteList, supplierSaveInfoObj, InvokeCloudAPI.Login(model), ctx,formID,model);
                    statusMap.Add(supplierSaveInfoObj.Model.Fnumber, callResult.CustomOpearteObject.Result);
                    
                }
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model,statusMap);

                //  ExcuteDataBaseResult excuteDataBaseResult = remoteExcuteDataBase.excuteStatement(updateSQLStatement, connectionToRemoteDatabase().Sqlconn);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model,  "t_kf_supplier", Sqlconn);
                }
                if (!closeConnetction(Sqlconn)) {
                    //打印关闭连接信息
                    BussnessLog.WriteBussnessLog("", "数据库操作错误", model + "中间表数据更新后,关闭数据库连接失败! 请联系系统管理员!");
                    return;
                }
            }

        }


        //public override bool InvokeAPI<T>(string[] opearteList, T t, LoginResult loginResult, Context ctx)
        //{
        //    InvokeResult invokeResult = null;
        //    string input = null;
        //    foreach (string opearte in opearteList)
        //    {
        //        BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte);
        //        if (Save.Equals(opearte))
        //        {
        //            BussnessLog.WriteBussnessLog(default(T),model,"input json=" + JsonExtension.ToJSON(t));
        //            invokeResult = InvokeCloudAPI.InvokeFunction(JsonExtension.ToJSON(t), loginResult.client, formID, Save, model);
        //        }
        //        else
        //        {
        //            input = JsonExtension.ToJSON(handleReturnMessage<SupplierInfoexceptAllocate>(invokeResult, opearte, model, ctx).CustomOpearteObject);
        //            BussnessLog.WriteBussnessLog(default(T),model,"input json=" + input);
        //            invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, Save);
        //        }
        //        if (!handleReturnMessage<SupplierInfoexceptAllocate>(invokeResult, opearte, model, ctx).ReturnResult)
        //        {
        //            break;
        //        }
        //        else {
        //            BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte + "成功!");
        //        }
        //    }
        //    return true;
        //    throw new NotImplementedException();
        //}

        public override List<T> handleData<T>(DataSet dataSet)
        {
            List<SupplierInfoSaveObject> supplierSaveInfoObjList = new List<SupplierInfoSaveObject>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                SupplierInfoSaveObject supplierSaveInfoObj = new SupplierInfoSaveObject();
                application.supplier.supplierSave.Model model = new application.supplier.supplierSave.Model();
                FFinanceInfo fFinanceInfo = new FFinanceInfo();
                FLocationInfo fLocationInfo = new FLocationInfo();
                List<FLocationInfo> fLocationInfoList = new List<FLocationInfo>();
                List<FBankInfo> fBankInfoList = new List<FBankInfo>();
                FBankInfo fBankInfo = new FBankInfo();
                FCreateOrgId fCreateOrgId = new FCreateOrgId();
                FUseOrgId fUseOrgId = new FUseOrgId();
                FPayCurrencyId fPayCurrencyId = new FPayCurrencyId();
                FLocNewContact fLocNewContact = new FLocNewContact();
                fCreateOrgId.FNumber = "9999";
                model.FCreateOrgId = fCreateOrgId;
                fUseOrgId.FNumber = "9999";
                model.FUseOrgId = fUseOrgId;
                fPayCurrencyId.Fnumber = "PRE001";
                fFinanceInfo.FPayCurrencyId = fPayCurrencyId;
                fBankInfo.FOpenBankName = dataSet.Tables[0].Rows[i]["FopenBankName"].ToString();
                fBankInfo.FBankCode = dataSet.Tables[0].Rows[i]["FbankCode"].ToString();
                fBankInfo.FBankHolder = dataSet.Tables[0].Rows[i]["FbankHolder"].ToString();
                fBankInfoList.Add(fBankInfo);
                model.Fnumber = dataSet.Tables[0].Rows[i]["fnumber"].ToString();
                model.FName = dataSet.Tables[0].Rows[i]["fname"].ToString();
                if (model.Fnumber.Contains("-") || model.FName.Contains("未知"))
                {
                    BussnessLog.WriteBussnessLog("", "中间数据库取数", "中间数据库取数错误，编码=" + model.Fnumber + ",客户名称=" + model.FName);
                    continue;
                }
                model.FBankInfo = fBankInfoList.ToArray();
                model.FFinanceInfo = fFinanceInfo;
                supplierSaveInfoObj.Model = model;
                supplierSaveInfoObjList.Add(supplierSaveInfoObj);

            }
            
            return (List<T>)(object)supplierSaveInfoObjList;
            throw new NotImplementedException();
        }

        //public override InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx)
        //{
        //    InvokeReturnHandle<T> invokeReturnHandle = new InvokeReturnHandle<T>();

        //    if (invokeResult == null)
        //    {
        //        BussnessLog.WriteBussnessLog(default(T),model,"调用模块=" + model + ",调用方法=" + opearte + ",返回为NULL");
        //    }
        //    if (!invokeResult.Result.ResponseStatus.IsSuccess)
        //    {
        //        BussnessLog.WriteBussnessLog(invokeResult, model, "");
        //        invokeReturnHandle.ReturnResult = false;
        //        return invokeReturnHandle;
        //    }

        //    List<SuccessEntitysItem> entries = invokeResult.Result.ResponseStatus.SuccessEntitys;

        //    SupplierInfoexceptAllocate supplierInfoexceptAllocateObject = new SupplierInfoexceptAllocate();
        //    List<string> numbers = new List<string>();
        //    List<string> pkids = new List<string>();
        //    foreach (SuccessEntitysItem entry in entries)
        //    {
        //        numbers.Add(entry.Number.ToString());
        //        pkids.Add(entry.Id.ToString());
        //    }
        //    supplierInfoexceptAllocateObject.Numbers = numbers.ToArray();
        //    invokeReturnHandle.CustomOpearteObject = (T)(object)supplierInfoexceptAllocateObject;
        //    invokeReturnHandle.ReturnResult = true;
        //    return invokeReturnHandle;
        //    throw new NotImplementedException();
        //}
    }
}
