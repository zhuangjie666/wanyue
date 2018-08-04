using Kingdee.BOS;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.customAllocate;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer.customSave;
using Kingdee.K3.WANYUE.PlugIn.service.interfaceSch;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using Kingdee.K3.WANYUE.PlugIn.service.schTask;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Kingdee.BOS.Authentication;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Custom : SchTask
    {
        public string SQLStatement;
        public string customSaveModel = "客户保存模块";
        public string Save = "Save";
        public string customSubmitModel = "客户提交模块";
        public string Submit = "Submit";
        public string customAuditModel = "客户审核模块";
        public string Audit = "Audit";
        public string customAllocateModel = "客户分配模块";
        public string Allocate = "Allocate";
        public string formID = "BD_Customer";
        public bool CallResult = false;

        public List<CustomInfoSaveObject> customSaveInfoObjList = null;

        public override void Run(Context ctx, Schedule schedule)
        {
            //1从中间表中获取当前需要调用的模块的SQL 
            // 1.2.连接远程数据库
            // 1.3.获取数据
            //2.调用Kingdee登录API
            //3.调用客户API
            //4.关闭数据库连接
            SQLStatement = base.GetStatement(CustomerSQLObject.Customer);

            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                customSaveInfoObjList = handleData<CustomInfoSaveObject>(excuteDataBaseResult.Ds);
            }
            else {
                //打印
            }

            if (LoginAPI(customSaveModel)) {
                string[] opearteList = new string[] { Save, Submit, Audit, Allocate };
                foreach(CustomInfoSaveObject customSaveInfoObj in customSaveInfoObjList) {
                    CallResult = InvokeAPI(opearteList, customSaveInfoObj, InvokeCloudAPI.Login(customSaveModel), ctx);
                }
              
            }

            if (!CallResult)
            {
                if (closeConnetction()) {
                    return;
                }
                else{
                    //打印
                }
            }
        }


        public override List<T> handleData<T>(DataSet dataSet)
        {
            List<CustomInfoSaveObject> customSaveInfoObjList = new List<CustomInfoSaveObject>();
            CustomInfoSaveObject customSaveInfoObj = new CustomInfoSaveObject();
            Model model = new Model();
            FCreateOrgId fcreateOrgID = new FCreateOrgId();
            FGroup fGroup = new FGroup();
            FSALGROUPID fsalGroupID = new FSALGROUPID();
            FTRADINGCURRID ftradingCurrID = new FTRADINGCURRID();
            FUseOrgId fUserOrgId = new FUseOrgId();
            fcreateOrgID.FNumber = "103";
            fGroup.fNumber = "KH002";
            fsalGroupID.fNumber = "0";
            model.FName = "测试1";
            ftradingCurrID.fNumber = "PRE001";
            fUserOrgId.FNumber = "103";
            model.FCreateOrgId = fcreateOrgID;
            model.FGroup = fGroup;
            model.FsalGroupID = fsalGroupID;
            model.FtradingCurrID = ftradingCurrID;
            model.FUseOrgId = fUserOrgId;
            customSaveInfoObj.Model = model;
            customSaveInfoObjList.Add(customSaveInfoObj);
            fcreateOrgID.FNumber = "103";
            fGroup.fNumber = "KH002";
            fsalGroupID.fNumber = "0";
            model.FName = "测试2";
            ftradingCurrID.fNumber = "PRE001";
            fUserOrgId.FNumber = "103";
            model.FCreateOrgId = fcreateOrgID;
            model.FGroup = fGroup;
            model.FsalGroupID = fsalGroupID;
            model.FtradingCurrID = ftradingCurrID;
            model.FUseOrgId = fUserOrgId;
            customSaveInfoObj.Model = model;
            customSaveInfoObjList.Add(customSaveInfoObj);

            return (List<T>)(object)customSaveInfoObjList;
            throw new NotImplementedException();
        }


        public override bool InvokeAPI<T>(string[] opearteList, T t, Invoke.LoginResult loginResult, Context ctx)
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
                    invokeResult = InvokeCloudAPI.InvokeFunction(JsonExtension.ToJSON(t), loginResult.client, formID, Save, customSaveModel);
                }
                else
                {
                    BussnessLog.WriteBussnessLog("操作=" + opearte);
                    if (Allocate.Equals(opearte))
                    {
                        input = JsonExtension.ToJSON(handleReturnMessage<CustomInfoAllocateObject>(invokeResult, opearte, customSaveModel, ctx).CustomOpearteObject);
                        BussnessLog.WriteBussnessLog("input json=" + input);
                        invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, customAuditModel);
                        if (handleReturnMessage<CustomInfoAllocateObject>(invokeResult, Allocate, customAllocateModel, ctx).ReturnResult)
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
                    input = JsonExtension.ToJSON(handleReturnMessage<CustomInfoexceptAllocate>(invokeResult, opearteList[i - 1], customSaveModel, ctx).CustomOpearteObject);
                    BussnessLog.WriteBussnessLog("input json=" + input);
                    invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, customAuditModel);
                }
                if (!handleReturnMessage<CustomInfoexceptAllocate>(invokeResult, opearte, customSaveModel, ctx).ReturnResult)
                {
                    break;
                }
            }
            return false;
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

            CustomInfoexceptAllocate customInfoexceptAllocateObject = new CustomInfoexceptAllocate();
            List<string> numbers = new List<string>();
            List<string> pkids = new List<string>();
            foreach (SuccessEntitysItem entry in entries) {
                numbers.Add(entry.Number.ToString());
                pkids.Add(entry.Id.ToString());
            }
            if (Allocate.Equals(opearte)) {
                CustomInfoAllocateObject customInfoAllocateObject = new CustomInfoAllocateObject();
                Dictionary<string, string> outresult = GetToOrgID.getToOrgID(ctx, numbers,model);
                foreach (KeyValuePair<string, string> kvp in outresult)
                {
                    customInfoAllocateObject.PkIds = kvp.Key;
                    customInfoAllocateObject.TOrgIds = kvp.Value;
                    break;
                }
                customInfoAllocateObject.IsAutoSubmitAndAudit = "true";
                invokeReturnHandle.CustomOpearteObject = (T)(object)customInfoAllocateObject;
                invokeReturnHandle.ReturnResult = true;
                return invokeReturnHandle;
            }
            customInfoexceptAllocateObject.Numbers = numbers.ToArray();
            invokeReturnHandle.CustomOpearteObject = (T)(object)customInfoexceptAllocateObject;
            invokeReturnHandle.ReturnResult = true;
            return invokeReturnHandle;
            throw new NotImplementedException();
        }
    }
}
