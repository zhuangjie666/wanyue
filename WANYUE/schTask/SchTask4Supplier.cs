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

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Supplier : SchTask
    {
        public string supplierSaveModel = "供应商保存模块";
        public static string Save = "Save";
        public string supplierSubmitModel = "供应商提交模块";
        public static string Submit = "Submit";
        public string supplierAuditModel = "供应商审核模块";
        public static string Audit = "Audit";
        public string supplierAllocateModel = "供应商分配模块";
        public static string Allocate = "Allocate";
        public string formID = "BD_Supplier";
        public string SQLStatement;
        public bool CallResult = false;
        public List<SupplierInfoSaveObject> supplierSaveInfoObjList = null;
        string[] opearteList = new string[] { Save, Submit, Audit, Allocate };

        public override void Run(Context ctx, Schedule schedule)
        {
            SQLStatement = base.GetStatement(SupplierSQLObject.Supplier);

            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                supplierSaveInfoObjList = handleData<SupplierInfoSaveObject>(excuteDataBaseResult.Ds);
            }
            else
            {
                //打印
            }

            if (LoginAPI(supplierSaveModel))
            {
                
                foreach (SupplierInfoSaveObject supplierSaveInfoObj in supplierSaveInfoObjList)
                {
                    CallResult = InvokeAPI(opearteList, supplierSaveInfoObj, InvokeCloudAPI.Login(supplierSaveModel), ctx);
                }
            }

            if (!CallResult)
            {
                if (closeConnetction())
                {
                    return;
                }
                else
                {
                    //打印
                }
            }
            else {
                closeConnetction();
                return;
            }
            throw new NotImplementedException();
        }


        public override bool InvokeAPI<T>(string[] opearteList, T t, LoginResult loginResult, Context ctx)
        {
            InvokeResult invokeResult = null;
            int i = 0;
            string input = null;
            foreach (string opearte in opearteList)
            {
                i = i + 1;
                if (Save.Equals(opearte))
                {
                    BussnessLog.WriteBussnessLog("操作=" + opearte);
                    BussnessLog.WriteBussnessLog("input json=" + JsonExtension.ToJSON(t));
                    invokeResult = InvokeCloudAPI.InvokeFunction(JsonExtension.ToJSON(t), loginResult.client, formID, Save, supplierSaveModel);
                }
                else
                {
                    BussnessLog.WriteBussnessLog("操作=" + opearte);
                    if (Allocate.Equals(opearte))
                    {
                        input = JsonExtension.ToJSON(handleReturnMessage<SupplierInfoAllocateObject>(invokeResult, opearte, Save, ctx).CustomOpearteObject);
                        BussnessLog.WriteBussnessLog("input json=" + input);
                        invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, supplierAuditModel);
                        if (handleReturnMessage<SupplierInfoAllocateObject>(invokeResult, Allocate, SupplierSQLObject.Supplier, ctx).ReturnResult)
                        {
                            BussnessLog.WriteBussnessLog("分配成功!");
                            return true;
                        }
                        else
                        {
                            BussnessLog.WriteBussnessLog("分配失败");
                            return false;
                        }
                    }
                    input = JsonExtension.ToJSON(handleReturnMessage<SupplierInfoexceptAllocate>(invokeResult, opearteList[i - 1], Save, ctx).CustomOpearteObject);
                    BussnessLog.WriteBussnessLog("input json=" + input);
                    invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, Save);
                }
                if (!handleReturnMessage<SupplierInfoexceptAllocate>(invokeResult, opearte, supplierSaveModel, ctx).ReturnResult)
                {
                    break;
                }
            }
            return false;



            throw new NotImplementedException();
        }

        public override List<T> handleData<T>(DataSet dataSet)
        {
            List<SupplierInfoSaveObject> supplierSaveInfoObjList = new List<SupplierInfoSaveObject>();
            SupplierInfoSaveObject supplierSaveInfoObj = new SupplierInfoSaveObject();
            application.supplier.supplierSave.Model model = new application.supplier.supplierSave.Model();
            FFinanceInfo fFinanceInfo = new FFinanceInfo();
            FLocationInfo fLocationInfo = new FLocationInfo();
            List<FLocationInfo> fLocationInfoList = new List<FLocationInfo>();
            FCreateOrgId fCreateOrgId = new FCreateOrgId();
            FUseOrgId fUseOrgId = new FUseOrgId();
            FPayCurrencyId fPayCurrencyId = new FPayCurrencyId();
            FLocNewContact fLocNewContact = new FLocNewContact();
            fCreateOrgId.FNumber = "101.1";
            model.FCreateOrgId = fCreateOrgId;
            fUseOrgId.FNumber = "101.1";
            model.FUseOrgId = fUseOrgId;
            model.FName="供应商1";
            fPayCurrencyId.Fnumber = "PRE001";
            fFinanceInfo.FPayCurrencyId = fPayCurrencyId;
            //fLocationInfo.FLocAddress = "测试地址1";
            //fLocationInfo.FLocMobile = "15601799842";
            //fLocationInfo.FLocName = "测试名字1";
            //fLocNewContact.FNumber = "CONT000001";
            //fLocationInfo.FLocNewContact = fLocNewContact;
            //fLocationInfoList.Add(fLocationInfo);
            model.FFinanceInfo = fFinanceInfo;
          //  model.FLocationInfo= fLocationInfoList.ToArray();
            supplierSaveInfoObj.Model = model;
            supplierSaveInfoObjList.Add(supplierSaveInfoObj);

            //SupplierInfoSaveObject supplierSaveInfoObj1 = new SupplierInfoSaveObject();
            //application.supplier.supplierSave.Model model1 = new application.supplier.supplierSave.Model();
            //FFinanceInfo fFinanceInfo1 = new FFinanceInfo();
            //FLocationInfo fLocationInfo1 = new FLocationInfo();
            //FCreateOrgId fCreateOrgId1 = new FCreateOrgId();
            //FUseOrgId fUseOrgId1 = new FUseOrgId();
            //FPayCurrencyId fPayCurrencyId1 = new FPayCurrencyId();
            //List<FLocationInfo> fLocationInfoList1 = new List<FLocationInfo>();
            //FLocNewContact fLocNewContact1 = new FLocNewContact();
            //fCreateOrgId1.FNumber = "101.1";
            //model1.FCreateOrgId = fCreateOrgId1;
            //fUseOrgId1.FNumber = "101.1";
            //model1.FUseOrgId = fUseOrgId1;

            //model1.FName = "供应商2";
            //fPayCurrencyId1.Fnumber = "PRE001";
            //fFinanceInfo1.FPayCurrencyId = fPayCurrencyId1;
            //fLocationInfo1.FLocAddress = "测试地址2";
            //fLocationInfo1.FLocMobile = "15061813635";
            //fLocationInfo1.FLocName = "测试名字2";
            //fLocNewContact1.FNumber = "CUST0001";
            //fLocationInfo1.FLocNewContact = fLocNewContact1;
            //fLocationInfoList1.Add(fLocationInfo1);
            //model1.FFinanceInfo = fFinanceInfo1;
            //model1.FLocationInfo = fLocationInfoList1.ToArray();
            //supplierSaveInfoObj1.Model = model1;
            //supplierSaveInfoObjList.Add(supplierSaveInfoObj1);

            return (List<T>)(object)supplierSaveInfoObjList;


            throw new NotImplementedException();
        }

        public override InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx)
        {
            InvokeReturnHandle<T> invokeReturnHandle = new InvokeReturnHandle<T>();

            if (invokeResult == null)
            {
                BussnessLog.WriteBussnessLog("调用模块=" + model + ",调用方法=" + opearte + ",返回为NULL");
            }
            if (!invokeResult.Result.ResponseStatus.IsSuccess)
            {
                StringBuilder errorSB = new StringBuilder();
                errorSB.Append("调用模块=" + model);
                errorSB.Append(",调用方法=" + opearte + "失败,").Append("错误代码=" + invokeResult.Result.ResponseStatus.ErrorCode);
                errorSB.Append(",错误原因: 字段=" + invokeResult.Result.ResponseStatus.Errors[0].FieldName);
                errorSB.Append(",错误消息=" + invokeResult.Result.ResponseStatus.Errors[0].Message);
                BussnessLog.WriteBussnessLog(errorSB.ToString());
                invokeReturnHandle.ReturnResult = false;
                return invokeReturnHandle;
            }

            List<SuccessEntitysItem> entries = invokeResult.Result.ResponseStatus.SuccessEntitys;

            SupplierInfoexceptAllocate supplierInfoexceptAllocateObject = new SupplierInfoexceptAllocate();
            List<string> numbers = new List<string>();
            List<string> pkids = new List<string>();
            foreach (SuccessEntitysItem entry in entries)
            {
                numbers.Add(entry.Number.ToString());
                pkids.Add(entry.Id.ToString());
            }
            if (Allocate.Equals(opearte))
            {
                SupplierInfoAllocateObject supplierInfoAllocateObject = new SupplierInfoAllocateObject();
                Dictionary<string, string> outresult = GetToOrgID.getToOrgID(ctx, numbers, SupplierSQLObject.Supplier);
                foreach (KeyValuePair<string, string> kvp in outresult)
                {
                    supplierInfoAllocateObject.PkIds = kvp.Key;
                    supplierInfoAllocateObject.TOrgIds = kvp.Value;
                    break;
                }
                supplierInfoAllocateObject.IsAutoSubmitAndAudit = "true";
                invokeReturnHandle.CustomOpearteObject = (T)(object)supplierInfoAllocateObject;
                invokeReturnHandle.ReturnResult = true;
                return invokeReturnHandle;
            }
            supplierInfoexceptAllocateObject.Numbers = numbers.ToArray();
            invokeReturnHandle.CustomOpearteObject = (T)(object)supplierInfoexceptAllocateObject;
            invokeReturnHandle.ReturnResult = true;
            return invokeReturnHandle;




            throw new NotImplementedException();
        }
    }
}
