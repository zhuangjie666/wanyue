using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer.customSave;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using Kingdee.K3.WANYUE.PlugIn.service.modify;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Custom : SchTask
    {
        public string SQLStatement;
        public string formID = "BD_Customer";
        public static string model = "customer";
        public bool CallResult = false;
        //需要操作的列表需要在父类处理 暂时没有处理
        string[] opearteList = { };

        public List<CustomInfoSaveObject> customSaveInfoObjList = null;

        public override void Run(Context ctx, Schedule schedule)
        {
            //1从中间表中获取当前需要调用的模块的SQL 
            // 1.2.连接中间数据库
            // 1.3.获取数据
            //2.调用Kingdee登录API
            //3.调用客户API
            //4.反写中间数据库
            //5.关闭数据库连接
            SQLStatement = GetStatement(CustomerSQLObject.Customer);
            opearteList = GetOpearteList(CustomerSQLObject.Customer);
            //修改的单独处理
            BussnessModify customerModify = new BussnessModify();
            customerModify.excuteSQL(model, connectionToRemoteDatabase(),ctx);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                customSaveInfoObjList = handleData<CustomInfoSaveObject>(excuteDataBaseResult.Ds);
                if (!closeConnetction(connectionToRemoteDatabase().Sqlconn))
                {
                    //打印关闭连接
                    BussnessLog.WriteBussnessLog("", "数据库操作错误", model + "中间表取数后,关闭数据库连接失败! 请联系系统管理员!");
                    return;
                }
            }
            else
            {
                if (!closeConnetction(connectionToRemoteDatabase().Sqlconn))
                {
                    BussnessLog.WriteBussnessLog("", "数据库操作错误", model + "中间表取数前打开数据库连接失败! 请联系系统管理员!");
                    return;
                }
            }
            Dictionary<string, Result> statusMap = new Dictionary<string, Result>();
            if (LoginAPI(model))
            {
                foreach (CustomInfoSaveObject customSaveInfoObj in customSaveInfoObjList)
                {
                    InvokeReturnHandle<InvokeResult> callResult = InvokeAPI(opearteList, customSaveInfoObj, InvokeCloudAPI.Login(model), ctx, formID, model);
                    statusMap.Add(customSaveInfoObj.Model.Fnumber, callResult.CustomOpearteObject.Result);
                }
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model,statusMap);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model, "t_kf_customer",Sqlconn);
                }
                if (!closeConnetction(Sqlconn))
                {
                    BussnessLog.WriteBussnessLog("", "数据库操作错误", model + "中间表数据更新后,关闭数据库连接失败! 请联系系统管理员!");
                    return;
                }
            }
        }

        public override List<T> handleData<T>(DataSet dataSet)
        {
            List<CustomInfoSaveObject> customSaveInfoObjList = new List<CustomInfoSaveObject>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                CustomInfoSaveObject customSaveInfoObj = new CustomInfoSaveObject();
                Model model = new Model();
                FCreateOrgId fcreateOrgID = new FCreateOrgId();
                FGroup fGroup = new FGroup();
                FSALGROUPID fsalGroupID = new FSALGROUPID();
                FTRADINGCURRID ftradingCurrID = new FTRADINGCURRID();
                FUseOrgId fUserOrgId = new FUseOrgId();
                fcreateOrgID.FNumber = "9999";
                //fGroup.fNumber = "KH001";
                fsalGroupID.fNumber = "0";
                ftradingCurrID.fNumber = "PRE001";
                fUserOrgId.FNumber = "9999";

                model.Fnumber = dataSet.Tables[0].Rows[i]["fnumber"].ToString();
                model.FName = dataSet.Tables[0].Rows[i]["fname"].ToString();
                if (model.Fnumber.Contains("-") || model.FName.Contains("未知"))
                {
                    BussnessLog.WriteBussnessLog("", "中间数据库取数", "中间数据库取数错误，编码=" + model.Fnumber + ",客户名称=" + model.FName);
                    continue;
                }
                model.FCreateOrgId = fcreateOrgID;
                //model.FGroup = fGroup;
                model.FsalGroupID = fsalGroupID;
                model.FtradingCurrID = ftradingCurrID;
                model.FUseOrgId = fUserOrgId;
                customSaveInfoObj.Model = model;
                customSaveInfoObjList.Add(customSaveInfoObj);
            }
            return (List<T>)(object)customSaveInfoObjList;
            throw new NotImplementedException();
        }
    }
}

// BussnessLog.logFiles.Remove(model);

//FTRADINGCURRID ftradingCurrID = new FTRADINGCURRID();
//FUseOrgId fUserOrgId = new FUseOrgId();
//fcreateOrgID.FNumber = "103";
//fGroup.fNumber = "KH002";
//fsalGroupID.fNumber = "0";
//model.FName = "测试1";

//fUserOrgId.FNumber = "103";

//fcreateOrgID.FNumber = "103";
//fGroup.fNumber = "KH002";
//fsalGroupID.fNumber = "0";
//model.FName = "测试2";
//ftradingCurrID.fNumber = "PRE001";
//fUserOrgId.FNumber = "103";
//model.FCreateOrgId = fcreateOrgID;
//model.FGroup = fGroup;
//model.FsalGroupID = fsalGroupID;
//model.FtradingCurrID = ftradingCurrID;
//model.FUseOrgId = fUserOrgId;
//customSaveInfoObj.Model = model;
//customSaveInfoObjList.Add(customSaveInfoObj);




//public override bool InvokeAPI<T>(string[] opearteList, T t, Invoke.LoginResult loginResult, Context ctx)
//{
//    InvokeResult invokeResult = null;
//    string input = null;
//    foreach (string opearte in opearteList)
//    {
//        BussnessLog.WriteBussnessLog(default(T),model,"操作=" + opearte);
//        if (Save.Equals(opearte))
//        {
//            BussnessLog.WriteBussnessLog(default(T),model,"input json=" + JsonExtension.ToJSON(t));
//            invokeResult = InvokeCloudAPI.InvokeFunction(JsonExtension.ToJSON(t), loginResult.client, formID, Save, model);
//        }
//        else
//        {
//            input = JsonExtension.ToJSON(handleReturnMessage<CustomInfoexceptAllocate>(invokeResult, opearte, model, ctx).CustomOpearteObject);
//            BussnessLog.WriteBussnessLog(default(T),model,"input json=" + input);
//            invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, model);
//        }
//        if (!handleReturnMessage<CustomInfoexceptAllocate>(invokeResult, opearte, model, ctx).ReturnResult)
//        {
//            break;
//        }
//        else {
//            BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte+"成功!");
//        }
//    }
//    return true;
//    throw new NotImplementedException();
//}


//public override InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx)
//{

//    InvokeReturnHandle<T> invokeReturnHandle = new InvokeReturnHandle<T>();

//    if (invokeResult == null)
//    {
//        BussnessLog.WriteBussnessLog(default(T),model,"调用模块=" + model + ",调用方法=" + opearte + ",返回为NULL");
//    }
//    if (!invokeResult.Result.ResponseStatus.IsSuccess)
//    {
//        BussnessLog.WriteBussnessLog(invokeResult,model,"");
//        invokeReturnHandle.ReturnResult = false;
//        return invokeReturnHandle;
//    }

//    List<SuccessEntitysItem> entries = invokeResult.Result.ResponseStatus.SuccessEntitys;

//    CustomInfoexceptAllocate customInfoexceptAllocateObject = new CustomInfoexceptAllocate();
//    List<string> numbers = new List<string>();
//    List<string> pkids = new List<string>();
//    foreach (SuccessEntitysItem entry in entries) {
//        numbers.Add(entry.Number.ToString());
//        pkids.Add(entry.Id.ToString());
//    }
//    customInfoexceptAllocateObject.Numbers = numbers.ToArray();
//    invokeReturnHandle.CustomOpearteObject = (T)(object)customInfoexceptAllocateObject;
//    invokeReturnHandle.ReturnResult = true;
//    return invokeReturnHandle;
//    throw new NotImplementedException();
//}

