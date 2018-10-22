using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.AR_OtherRecAble;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using System.Data.SqlClient;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{

    public class SchTask4OtherRecAble : SchTask
    {
        public string SQLStatement;
        public string formID = "AR_OtherRecAble";
        public bool CallResult = false;
        string[] opearteList = { };
        public static string model = "otherrecable";
        public List<FEntity> fentryList = new List<FEntity>();
        public Dictionary<string, string> VoucherNumbers = new Dictionary<string, string>();
        public List<OtherRecAbleInfoSaveObject> otherRecAbleSaveInfoObjList = null;
        public override void Run(Context ctx, Schedule schedule)
        {
            SQLStatement = base.GetStatement(OtherRecAbleSQLObject.OtherRecAble);
            opearteList = GetOpearteList(OtherRecAbleSQLObject.OtherRecAble);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                otherRecAbleSaveInfoObjList = handleData<OtherRecAbleInfoSaveObject>(excuteDataBaseResult.Ds);
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


                foreach (OtherRecAbleInfoSaveObject otherRecAbleInfoSaveObject in otherRecAbleSaveInfoObjList)
                {
                    InvokeReturnHandle<InvokeResult> callResult = InvokeAPI(opearteList, otherRecAbleInfoSaveObject, InvokeCloudAPI.Login(model), ctx, formID, model);
                    statusMap.Add(otherRecAbleInfoSaveObject.Model.FBillNo, callResult.CustomOpearteObject.Result);
                }
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model, statusMap);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model, "t_kf_otherpayable", Sqlconn);
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
            fentryList.Clear();
            List<OtherRecAbleInfoSaveObject> otherRecAbleSaveInfoObjList = new List<OtherRecAbleInfoSaveObject>();
            List<OtherRecAbleDataSet> otherRecAbleDataSetList = new List<OtherRecAbleDataSet>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                OtherRecAbleDataSet otherRecAbleDataSet = new OtherRecAbleDataSet();
                otherRecAbleDataSet.FNumber = dataSet.Tables[0].Rows[i]["FNumber"].ToString();
                otherRecAbleDataSet.Fdate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["Fdate"]);
                otherRecAbleDataSet.FendDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FendDate"]);
                otherRecAbleDataSet.FaccntTimeJudgeTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FaccntTimeJudgeTime"]);
                otherRecAbleDataSet.FContactUnitType= dataSet.Tables[0].Rows[i]["FContactUnitType"].ToString();
                otherRecAbleDataSet.FContactUnit= dataSet.Tables[0].Rows[i]["FContactUnit"].ToString();
                otherRecAbleDataSet.FCurrency = dataSet.Tables[0].Rows[i]["FCurrency"].ToString();
                otherRecAbleDataSet.FsettleOrg = dataSet.Tables[0].Rows[i]["FsettleOrg"].ToString();
                otherRecAbleDataSet.FpayOrg = dataSet.Tables[0].Rows[i]["FpayOrg"].ToString();
                otherRecAbleDataSet.FCost = dataSet.Tables[0].Rows[i]["FCost"].ToString();
                otherRecAbleDataSet.FCostDepartment = dataSet.Tables[0].Rows[i]["FCostDepartment"].ToString();
                otherRecAbleDataSet.FInvoiceType = dataSet.Tables[0].Rows[i]["FInvoiceType"].ToString();
                otherRecAbleDataSet.FEntryTaxRate = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FEntryTaxRate"]);
                otherRecAbleDataSet.FNoTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FNoTaxAmountFor"]);
                otherRecAbleDataSet.FTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FTaxAmountFor"]);
                otherRecAbleDataSet.FAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FAmountFor"]);
                otherRecAbleDataSet.Fremark = dataSet.Tables[0].Rows[i]["FREMARK"].ToString();
                otherRecAbleDataSetList.Add(otherRecAbleDataSet);
            }

            for (int i = 0; i < otherRecAbleDataSetList.Count; i++)
            {
                //分录
                FEntity fentry = new FEntity();
                //1.费用项目编码
                FCOSTID fCOSTID = new FCOSTID();
                fCOSTID.FNumber = otherRecAbleDataSetList[i].FCost;
                fentry.FCOSTID = fCOSTID;
                //2.费用承担部门编码
                FCOSTDEPARTMENTID fCOSTDEPARTMENTID = new FCOSTDEPARTMENTID();
                fCOSTDEPARTMENTID.FNumber = otherRecAbleDataSetList[i].FCostDepartment;
                fentry.FCOSTDEPARTMENTID = fCOSTDEPARTMENTID;
                //3.发票类型： 0普通发票；1 增值税发票 
                fentry.FINVOICETYPE = otherRecAbleDataSetList[i].FInvoiceType;
                //4.税率(%)
                fentry.FEntryTaxRate = otherRecAbleDataSetList[i].FEntryTaxRate;
                //5. 不含税金额
                fentry.FNOTAXAMOUNTFOR = otherRecAbleDataSetList[i].FNoTaxAmountFor;
                //6.税额
                fentry.FTAXAMOUNTFOR = otherRecAbleDataSetList[i].FTaxAmountFor;
                //7.总金额
                fentry.FAMOUNTFOR_D = otherRecAbleDataSetList[i].FAmountFor;
                if (i + 1 < otherRecAbleDataSetList.Count)
                {
                   
                    if (otherRecAbleDataSetList[i].FNumber != otherRecAbleDataSetList[i + 1].FNumber)
                    {
                        fentryList.Add(fentry);
                        OtherRecAbleInfoSaveObject otherRecAbleInfoSaveObject = new OtherRecAbleInfoSaveObject();
                        Model model = new Model();
                        List<FEntity> fEntityA = new List<FEntity>();
                        fEntityA.AddRange(fentryList);
                        model.FEntity = fEntityA;
                        //1.单据类型
                        FBillTypeID fBillTypeID = new FBillTypeID();
                        fBillTypeID.FNumber = "QTYSD01_SYS";
                        model.FBillTypeID = fBillTypeID;
                        //2.单号
                        model.FBillNo = otherRecAbleDataSetList[i].FNumber;
                        //3.业务日期
                        model.FDate = otherRecAbleDataSetList[i].Fdate;
                        //4.到期日
                        model.FENDDATE_H = otherRecAbleDataSetList[i].FendDate;
                        //5. 到期日计算日期
                        model.FACCNTTIMEJUDGETIME = otherRecAbleDataSetList[i].FaccntTimeJudgeTime;
                        //6.往来单位类型：供应商、客户、员工、部门、其他往来单位
                        if (otherRecAbleDataSetList[i].FContactUnitType.Equals("供应商")) {
                            model.FCONTACTUNITTYPE = "BD_Supplier";
                        }
                        if (otherRecAbleDataSetList[i].FContactUnitType.Equals("客户")) {
                            model.FCONTACTUNITTYPE = "BD_Customer";
                        }
                        if (otherRecAbleDataSetList[i].FContactUnitType.Equals("员工"))
                        {
                            model.FCONTACTUNITTYPE = "BD_Empinfo";
                        }
                        if (otherRecAbleDataSetList[i].FContactUnitType.Equals("部门"))
                        {
                            model.FCONTACTUNITTYPE = "BD_Department";
                        }
                        if (otherRecAbleDataSetList[i].FContactUnitType.Equals("其他往来单位"))
                        {
                            model.FCONTACTUNITTYPE = "FIN_OTHERS";
                        }

                        //7.往来单位编码
                        FCONTACTUNIT fCONTACTUNIT = new FCONTACTUNIT();
                        fCONTACTUNIT.FNumber = otherRecAbleDataSetList[i].FContactUnit;
                        model.FCONTACTUNIT = fCONTACTUNIT;
                        //8.币别
                        FCURRENCYID fCURRENCYID = new FCURRENCYID();
                        fCURRENCYID.FNumber = otherRecAbleDataSetList[i].FCurrency;
                        model.FCURRENCYID = fCURRENCYID;
                        //9.结算组织
                        FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                        fSETTLEORGID.FNumber = otherRecAbleDataSetList[i].FsettleOrg;
                        model.FSETTLEORGID = fSETTLEORGID;
                        //10.收款组织
                        FPAYORGID fPAYORGID = new FPAYORGID();
                        fPAYORGID.FNumber = otherRecAbleDataSetList[i].FpayOrg;
                        model.FPAYORGID = fPAYORGID;
                        ////11.总金额
                        //model.FAMOUNTFOR = otherRecAbleDataSetList[i].FAmountFor;
                        //12.本位币
                        //model.FMAINBOOKSTDCURRID= 
                        model.FAR_OtherRemarks = otherRecAbleDataSetList[i].Fremark;
                        List<FEntity> fEntityB = new List<FEntity>();
                        fEntityB.AddRange(fentryList);
                        model.FEntity = fEntityB;
                        otherRecAbleInfoSaveObject.Model = model;
                        otherRecAbleSaveInfoObjList.Add(otherRecAbleInfoSaveObject);
                        fentryList.Clear();
                    }
                    else
                    {
                        fentryList.Add(fentry);
                    }
                }
                if (otherRecAbleDataSetList.Count == i + 1)
                {
                    OtherRecAbleInfoSaveObject otherRecAbleInfoSaveObject = new OtherRecAbleInfoSaveObject();
                    fentryList.Add(fentry);
                    Model model = new Model();

                    //1.单据类型
                    FBillTypeID fBillTypeID = new FBillTypeID();
                    fBillTypeID.FNumber = "QTYSD01_SYS";
                    model.FBillTypeID = fBillTypeID;
                    //2.单号
                    model.FBillNo = otherRecAbleDataSetList[i].FNumber;
                    //3.业务日期
                    model.FDate = otherRecAbleDataSetList[i].Fdate;
                    //4.到期日
                    model.FENDDATE_H = otherRecAbleDataSetList[i].FendDate;
                    //5. 到期日计算日期
                    model.FACCNTTIMEJUDGETIME = otherRecAbleDataSetList[i].FaccntTimeJudgeTime;
                    //6.往来单位类型：供应商、客户、员工、部门、其他往来单位
                    if (otherRecAbleDataSetList[i].FContactUnitType.Equals("供应商"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Supplier";
                    }
                    if (otherRecAbleDataSetList[i].FContactUnitType.Equals("客户"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Customer";
                    }
                    if (otherRecAbleDataSetList[i].FContactUnitType.Equals("员工"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Empinfo";
                    }
                    if (otherRecAbleDataSetList[i].FContactUnitType.Equals("部门"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Department";
                    }
                    if (otherRecAbleDataSetList[i].FContactUnitType.Equals("其他往来单位"))
                    {
                        model.FCONTACTUNITTYPE = "FIN_OTHERS";
                    }
                    //7.往来单位编码
                    FCONTACTUNIT fCONTACTUNIT = new FCONTACTUNIT();
                    fCONTACTUNIT.FNumber = otherRecAbleDataSetList[i].FContactUnit;
                    model.FCONTACTUNIT = fCONTACTUNIT;
                    //8.币别
                    FCURRENCYID fCURRENCYID = new FCURRENCYID();
                    fCURRENCYID.FNumber = otherRecAbleDataSetList[i].FCurrency;
                    model.FCURRENCYID = fCURRENCYID;
                    //9.结算组织
                    FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                    fSETTLEORGID.FNumber = otherRecAbleDataSetList[i].FsettleOrg;
                    model.FSETTLEORGID = fSETTLEORGID;
                    //10.收款组织
                    FPAYORGID fPAYORGID = new FPAYORGID();
                    fPAYORGID.FNumber = otherRecAbleDataSetList[i].FpayOrg;
                    model.FPAYORGID = fPAYORGID;
                    //11.总金额
                    // model.FAMOUNTFOR = otherRecAbleDataSetList[i].FAmountFor;
                    model.FAR_OtherRemarks = otherRecAbleDataSetList[i].Fremark;
                    List<FEntity> fEntityB = new List<FEntity>();
                    fEntityB.AddRange(fentryList);
                    model.FEntity = fEntityB;
                    otherRecAbleInfoSaveObject.Model = model;
                    otherRecAbleSaveInfoObjList.Add(otherRecAbleInfoSaveObject);
                }

            }
            return (List<T>)(object)otherRecAbleSaveInfoObjList;
        }
    }


  
    }
