using System;
using System.Collections.Generic;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.voucher;
using System.Data;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using System.Data.SqlClient;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Voucher : SchTask
    {
        public string SQLStatement;
        public string formID = "GL_VOUCHER";
        public bool CallResult = false;
        string[] opearteList = { };
        public static string model = "voucher";
        public List<FEntity> fentryList = new List<FEntity>();
        public  Dictionary<string, string> VoucherNumbers = new Dictionary<string, string> { { "记", "PRE001"}, { "现收", "PRE002" }, { "现付", "PRE003"}, { "转", "PRE004" }, { "银收", "PZZ1" }, {"银付", "PZZ2"}};

        public List<VoucherInfoSaveObject> voucherSaveInfoObjList = null;
        public override void Run(Context ctx, Schedule schedule)
        {
            //VoucherNumbers.Add("记", "PRE001");
            //VoucherNumbers.Add("现收", "PRE002");
            //VoucherNumbers.Add("现付", "PRE003");
            //VoucherNumbers.Add("转", "PRE004");
            //VoucherNumbers.Add("银收", "PZZ1");
            //VoucherNumbers.Add("银付", "PZZ2");

            SQLStatement = base.GetStatement(VouchSQLObject.Voucher);
            opearteList = GetOpearteList(VouchSQLObject.Voucher);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                voucherSaveInfoObjList = handleData<VoucherInfoSaveObject>(excuteDataBaseResult.Ds);
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
                    //打印关闭连接
                    BussnessLog.WriteBussnessLog("", "数据库操作错误", model + "中间表取数前打开数据库连接失败! 请联系系统管理员!");
                    return;
                }
            }

            if (LoginAPI(model))
            {
                Dictionary<string, Result> statusMap = new Dictionary<string, Result>();


                foreach (VoucherInfoSaveObject voucherSaveInfoObj in voucherSaveInfoObjList)
                {
                    InvokeReturnHandle <InvokeResult> callResult = InvokeAPI(opearteList, voucherSaveInfoObj, InvokeCloudAPI.Login(model), ctx, formID, model);
                    statusMap.Add(voucherSaveInfoObj.Model.FVOUCHERGROUPNO, callResult.CustomOpearteObject.Result);
                }
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model,statusMap);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model, "t_kf_voucher",Sqlconn);
                }
                if (!closeConnetction(Sqlconn))
                {
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
        //            BussnessLog.WriteBussnessLog(default(T), model, "input json=" + JsonExtension.ToJSON(t));
        //            invokeResult = InvokeCloudAPI.InvokeFunction(JsonExtension.ToJSON(t), loginResult.client, formID, Save, model);
        //        }
        //        else
        //        {
        //            input = JsonExtension.ToJSON(handleReturnMessage<VoucherInfoexceptAllocate>(invokeResult, opearte, model, ctx).CustomOpearteObject);
        //            BussnessLog.WriteBussnessLog(default(T), model, "input json=" + input);
        //            invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, model);
        //        }
        //        if (!handleReturnMessage<VoucherInfoexceptAllocate>(invokeResult, opearte, model, ctx).ReturnResult)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte + "成功!");
        //        }
        //    }
        //    return true;
        //    throw new NotImplementedException();
        //}

        public override List<T> handleData<T>(DataSet dataSet)
        {

            //   foreach (DataSet dt in dataSet.Tables) {
            fentryList.Clear();
            List<VoucherInfoSaveObject> voucherSaveInfoObjList = new List<VoucherInfoSaveObject>();
           
            List<VoucherDataSet> voucherDataSetList = new List<VoucherDataSet>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++) {
                VoucherDataSet voucherDataSet = new VoucherDataSet();
                voucherDataSet.Fid = Convert.ToInt32(dataSet.Tables[0].Rows[i]["FID"]);
                voucherDataSet.Fnumber = dataSet.Tables[0].Rows[i]["Fnumber"].ToString(); //头 凭证号
                voucherDataSet.FAccountBookNumber = dataSet.Tables[0].Rows[i]["FAccountBookNumber"].ToString(); //账簿
                voucherDataSet.FDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FDATE"]);
                voucherDataSet.FSystemNumber = "GL";
                voucherDataSet.FVoucherGroupNumber = dataSet.Tables[0].Rows[i]["FVoucherGroupNumber"].ToString(); //凭证字
                voucherDataSet.FAcctorgNumber = "";
                voucherDataSet.FAuditStatus = "B";
                voucherDataSet.FAccbookOrgID = dataSet.Tables[0].Rows[i]["FAcctOrgNumber"].ToString();
                //以上是头信息
                voucherDataSet.FEntryID = i;
                voucherDataSet.FEXPLANATION = dataSet.Tables[0].Rows[i]["FEXPLANATION"].ToString();
                voucherDataSet.FAccountID = dataSet.Tables[0].Rows[i]["FAccountNumber"].ToString();
                voucherDataSet.FDEBIT = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FDEBIT"]);
                voucherDataSet.FCREDIT = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FCREDIT"]);
                voucherDataSet.FDetailFflex = dataSet.Tables[0].Rows[i]["FDetailFflex"].ToString();
                voucherDataSet.FExchangeRateType = "HLTX01_SYS";
                voucherDataSet.FCurrencyID = "PRE001";
                voucherDataSetList.Add(voucherDataSet);
            }

           
            int j = 0;
            
            for (int i = 0; i < voucherDataSetList.Count; i++)
            {
                
                FEntity fEntry = new FEntity();
                //1.科目
                FAccountID fAccountID = new FAccountID();
                fAccountID.FNumber = voucherDataSetList[i].FAccountID;
                fEntry.FACCOUNTID = fAccountID;
                //2.汇率类型
                FExchangeRateType fExchangeRateType = new FExchangeRateType();
                fExchangeRateType.Fnumber = voucherDataSetList[i].FExchangeRateType;
                fEntry.FEXCHANGERATETYPE = fExchangeRateType;
                //3.币别
                FCurrencyID fCurrencyID = new FCurrencyID();
                fCurrencyID.Fnumber = voucherDataSetList[i].FCurrencyID;
                fEntry.FCURRENCYID = fCurrencyID;
                //4.借
                fEntry.FCREDIT = voucherDataSetList[i].FCREDIT;
                //5.贷
                fEntry.FDEBIT = voucherDataSetList[i].FDEBIT;

                //本位币金额
                if (fEntry.FCREDIT != 0)
                {
                    fEntry.FAmount = voucherDataSetList[i].FCREDIT;
                    fEntry.FAMOUNTFOR = voucherDataSetList[i].FCREDIT;
                }
                else {
                    fEntry.FAmount= voucherDataSetList[i].FDEBIT;
                    fEntry.FAMOUNTFOR = voucherDataSetList[i].FDEBIT;
                }
                //6.说明
                fEntry.FEXPLANATION = voucherDataSetList[i].FEXPLANATION;
                //7.id
               // fEntry.FEntryID = voucherDataSetList[i].FEntryID+1;
                //9.exchagerate
                fEntry.FEXCHANGERATE = 1;
                //8.detail 
                if (voucherDataSetList[i].FDetailFflex.ToString() != "")
                {
                    string[] details = voucherDataSetList[i].FDetailFflex.ToString().Split(';');
                    FDetailID fDetailID = new FDetailID();
                    foreach (string s in details) {
                       string[] detail =  s.Split('-');
                        //if (HeSuanWeiDu.init().ContainsKey(detail[0])){
                        //        HeSuanWeiDu.init()[detail[0]] = detail[1];
                        //}

                        if (detail[0].Equals("HSWD01_SYS"))
                        {
                            FDETAILID__FFLEX4 fDETAILID__FFLEX4 = new FDETAILID__FFLEX4();
                            fDETAILID__FFLEX4.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX4 = fDETAILID__FFLEX4;
                        }

                        if (detail[0].Equals("HSWD02_SYS"))
                        {
                            FDETAILID__FFLEX5 fDETAILID__FFLEX5 = new FDETAILID__FFLEX5();
                            fDETAILID__FFLEX5.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX5 = fDETAILID__FFLEX5;
                        }

                        if (detail[0].Equals("HSWD03_SYS"))
                        {
                            FDETAILID__FFLEX6 fDETAILID__FFLEX6 = new FDETAILID__FFLEX6();
                            fDETAILID__FFLEX6.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX6 = fDETAILID__FFLEX6;
                        }

                        if (detail[0].Equals("HSWD04_SYS"))
                        {
                            FDETAILID__FFLEX7 fDETAILID__FFLEX7 = new FDETAILID__FFLEX7();
                            fDETAILID__FFLEX7.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX7 = fDETAILID__FFLEX7;
                        }

                        if (detail[0].Equals("HSWD05_SYS"))
                        {
                            FDETAILID__FFLEX8 fDETAILID__FFLEX8 = new FDETAILID__FFLEX8();
                            fDETAILID__FFLEX8.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX8 = fDETAILID__FFLEX8;
                        }

                        if (detail[0].Equals("HSWD06_SYS"))
                        {
                            FDETAILID__FFLEX9 fDETAILID__FFLEX9 = new FDETAILID__FFLEX9();
                            fDETAILID__FFLEX9.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX9 = fDETAILID__FFLEX9;
                        }

                        if (detail[0].Equals("HSWD07_SYS"))
                        {
                            FDETAILID__FFLEX10 fDETAILID__FFLEX10 = new FDETAILID__FFLEX10();
                            fDETAILID__FFLEX10.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX10 = fDETAILID__FFLEX10;
                        }

                        if (detail[0].Equals("HSWD08_SYS"))
                        {
                            FDETAILID__FFLEX11 fDETAILID__FFLEX11 = new FDETAILID__FFLEX11();
                            fDETAILID__FFLEX11.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX11 = fDETAILID__FFLEX11;
                        }
                        if (detail[0].Equals("HSWD09_SYS"))
                        {
                            FDETAILID__FFLEX12 fDETAILID__FFLEX12 = new FDETAILID__FFLEX12();
                            fDETAILID__FFLEX12.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX12 = fDETAILID__FFLEX12;
                        }
                        if (detail[0].Equals("HSWD10_SYS"))
                        {
                            FDETAILID__FFLEX13 fDETAILID__FFLEX13 = new FDETAILID__FFLEX13();
                            fDETAILID__FFLEX13.Fnumber = detail[1];
                            fDetailID.FDETAILID__FFLEX13 = fDETAILID__FFLEX13;
                        }

                        if (detail[0].Equals("ZDY0001"))
                        {
                            FDETAILID__FF100002 fDETAILID__FF100002 = new FDETAILID__FF100002();
                            fDETAILID__FF100002.Fnumber = detail[1];
                            fDetailID.FDETAILID__FF100002 = fDETAILID__FF100002;
                        }
                        if (detail[0].Equals("ZDY0002"))
                        {
                            FDETAILID__FF100003 fDETAILID__FF100003 = new FDETAILID__FF100003();
                            fDETAILID__FF100003.Fnumber = detail[1];
                            fDetailID.FDETAILID__FF100003 = fDETAILID__FF100003;
                        }
                        if (detail[0].Equals("ZDY0003"))
                        {
                            FDETAILID__FF100004 fDETAILID__FF100004 = new FDETAILID__FF100004();
                            fDETAILID__FF100004.Fnumber = detail[1];
                            fDetailID.FDETAILID__FF100004 = fDETAILID__FF100004;
                        }
                        if (detail[0].Equals("ZDY0004"))
                        {
                            FDETAILID__FF100005 fDETAILID__FF100005 = new FDETAILID__FF100005();
                            fDETAILID__FF100005.Fnumber = detail[1];
                            fDetailID.FDETAILID__FF100005 = fDETAILID__FF100005;
                        }
                    }
                    fEntry.FDetailID = fDetailID;
                  
                }

                if (i + 1 < voucherDataSetList.Count)
                {

                    if (voucherDataSetList[i].Fnumber != voucherDataSetList[i + 1].Fnumber)
                    {
                        fentryList.Add(fEntry);
                        VoucherInfoSaveObject voucherSaveInfoObj = new VoucherInfoSaveObject();
                        Model model = new Model();
                        List<FEntity> fEntityA = new List<FEntity>();
                        fEntityA.AddRange(fentryList);
                        model.FEntity = fEntityA;

                        j = j + 1;
           
                        // model.FVoucherID = j;
                        //账簿
                        FAccountBookNumber FAccountBookNumber = new FAccountBookNumber();
                        FAccountBookNumber.Fnumber = voucherDataSetList[i].FAccountBookNumber;
                        model.FAccountBookID = FAccountBookNumber;
                        //日期
                        model.FDate = voucherDataSetList[i].FDate.ToLongDateString();
              
                        //GL
                        FSystemID fSystemID = new FSystemID();
                        fSystemID.Fnumber = "GL";
                        model.FSystemID = fSystemID;
                        //是否新增
                        model.FISADJUSTVOUCHER = "false";
                        //xxx
                        model.FVOUCHERID = "0";
                        //凭证字
                        FVoucherGroupID FVoucherGroupID = new FVoucherGroupID();
                        if (VoucherNumbers.ContainsKey(voucherDataSetList[i].FVoucherGroupNumber)) {
                            FVoucherGroupID.Fnumber = VoucherNumbers[voucherDataSetList[i].FVoucherGroupNumber];
                        }
                       // = voucherDataSetList[i].FVoucherGroupNumber;
                        model.FVoucherGroupID = FVoucherGroupID;
                        //审核状态
                        model.FDocumentStatus = "A";
                        //凭证号
                        model.FVOUCHERGROUPNO = voucherDataSetList[i].Fnumber;
                        //核算组织
                        FAccbookOrgID FAccbookOrgID = new FAccbookOrgID();
                        FAccbookOrgID.Fnumber = voucherDataSetList[i].FAccbookOrgID;
                        model.FAccbookOrgID = FAccbookOrgID;
                       // model.FEntity = fentryList1;
                        
                        voucherSaveInfoObj.Model = model;
                        voucherSaveInfoObj.Fid = voucherDataSetList[i].Fid.ToString();
                     //   voucherSaveInfoObj.needUpDateFields = new string[] { "FACCBOOKORGID", "FDate", "FAccountBookID", "FVOUCHERGROUPID", "FEntity", "FEXPLANATION", "FACCOUNTID", "FDEBIT", "FCREDIT", "FEXCHANGERATE", "fAmount", "fAmountFOR" };
                        voucherSaveInfoObjList.Add(voucherSaveInfoObj);
                        fentryList.Clear();
                    }
                    else {
                        fentryList.Add(fEntry);
                    }
                }
                if ( voucherDataSetList.Count == i + 1 ) {
                    fentryList.Add(fEntry);
                    Model model = new Model();
                    //账簿
                    FAccountBookNumber FAccountBookNumber = new FAccountBookNumber();
                    FAccountBookNumber.Fnumber = voucherDataSetList[i].FAccountBookNumber;
                    model.FAccountBookID = FAccountBookNumber;
                    //日期
                    model.FDate = voucherDataSetList[i].FDate.ToLongDateString();
                    //GL
                    FSystemID fSystemID = new FSystemID();
                    fSystemID.Fnumber = "GL";
                    model.FSystemID = fSystemID;
                    //凭证字
                    FVoucherGroupID FVoucherGroupID = new FVoucherGroupID();
                    if (VoucherNumbers.ContainsKey(voucherDataSetList[i].FVoucherGroupNumber))
                    {
                        FVoucherGroupID.Fnumber = VoucherNumbers[voucherDataSetList[i].FVoucherGroupNumber];
                    }
                   // FVoucherGroupID.Fnumber = voucherDataSetList[i].FVoucherGroupNumber;
                    model.FVoucherGroupID = FVoucherGroupID;
                    //审核状态
                    model.FDocumentStatus = "A";
                    //凭证号
                    model.FVOUCHERGROUPNO = voucherDataSetList[i].Fnumber;
                    //是否新增
                    model.FISADJUSTVOUCHER = "false";
                    model.FVOUCHERID = "0";
                    //核算组织
                    FAccbookOrgID FAccbookOrgID = new FAccbookOrgID();
                    FAccbookOrgID.Fnumber = voucherDataSetList[i].FAccbookOrgID;
                    model.FAccbookOrgID = FAccbookOrgID;
                    List<FEntity> fEntityB = new List<FEntity>();
                    fEntityB.AddRange(fentryList);
                    model.FEntity = fEntityB;
                    //model.FEntity = fentryList;
                    VoucherInfoSaveObject voucherSaveInfoObj = new VoucherInfoSaveObject();
                   // voucherSaveInfoObj.needUpDateFields= new string[] {};
                    voucherSaveInfoObj.Model = model;
                    voucherSaveInfoObj.Fid = voucherDataSetList[i].Fid.ToString();
                    voucherSaveInfoObjList.Add(voucherSaveInfoObj);
                    fentryList.Clear();
                }
            }
            
            return (List<T>)(object)voucherSaveInfoObjList;
            throw new NotImplementedException();
        }

        //    public override InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx)
        //    {

        //        InvokeReturnHandle<T> invokeReturnHandle = new InvokeReturnHandle<T>();

        //        if (invokeResult == null)
        //        {
        //            BussnessLog.WriteBussnessLog(default(T), model, "调用模块=" + model + ",调用方法=" + opearte + ",返回为NULL");
        //        }
        //        if (!invokeResult.Result.ResponseStatus.IsSuccess)
        //        {
        //            BussnessLog.WriteBussnessLog(invokeResult, model, "");
        //            invokeReturnHandle.ReturnResult = false;
        //            return invokeReturnHandle;
        //        }

        //        List<SuccessEntitysItem> entries = invokeResult.Result.ResponseStatus.SuccessEntitys;

        //        VoucherInfoexceptAllocate voucherInfoexceptAllocateObject = new VoucherInfoexceptAllocate();
        //        List<string> numbers = new List<string>();
        //        List<string> pkids = new List<string>();
        //        foreach (SuccessEntitysItem entry in entries)
        //        {
        //            numbers.Add(entry.Number.ToString());
        //            pkids.Add(entry.Id.ToString());
        //        }
        //        voucherInfoexceptAllocateObject.Numbers = numbers.ToArray();
        //        invokeReturnHandle.CustomOpearteObject = (T)(object)voucherInfoexceptAllocateObject;
        //        invokeReturnHandle.ReturnResult = true;
        //        return invokeReturnHandle;
        //        throw new NotImplementedException();
        //    }
        //}

        //for (int i = 0; i < dataSet.Tables.Count; i++)
        //{

        //FAccountBookNumber fAccountBookNumber = new FAccountBookNumber();
        //FAccountID fAccountID = new FAccountID();

        //FVoucherGroupID fVoucherGroupID = new FVoucherGroupID();
        //model.FDate = System.DateTime.Now;
        //fAccountBookNumber.Fnumber = dataSet.Tables[0].Rows[i]["FAccountBookNumber"].ToString();
        //model.FAccountBookID = fAccountBookNumber;
        //fVoucherGroupID.Fnumber = dataSet.Tables[0].Rows[i]["FVoucherGroupNumber"].ToString();
        //model.FVoucherGroupID = fVoucherGroupID;

        //   Dictionary<Model, List<FEntity>> fnumberFEntryMap = new Dictionary<Model, List<FEntity>>();
        //  foreach(VoucherDataSet voucherDataSet in voucherDataSetList) {
        //VoucherInfoSaveObject voucherSaveInfoObj = new VoucherInfoSaveObject();
        //Model model = new Model();

        //    voucherSaveInfoObjList.Add(voucherSaveInfoObj);
        //}
    }
}
