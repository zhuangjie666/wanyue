using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.AP_OtherPayable;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using System.Data.SqlClient;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    public class SchTask4OtherPayable : SchTask
    {
        public string SQLStatement;
        public string formID = "AP_OtherPayable";
        public bool CallResult = false;
        string[] opearteList = { };
        public static string model = "otherpayable";
        public List<FEntity> fentryList = new List<FEntity>();
        public Dictionary<string, string> VoucherNumbers = new Dictionary<string, string>();
        public List<OtherPayableInfoSaveObject> otherPayableSaveInfoObjList = null;

        public override void Run(Context ctx, Schedule schedule)
        {
            SQLStatement = base.GetStatement(OtherPayableSQLObject.OtherPayable);
            opearteList = GetOpearteList(OtherPayableSQLObject.OtherPayable);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                otherPayableSaveInfoObjList = handleData<OtherPayableInfoSaveObject>(excuteDataBaseResult.Ds);
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


                foreach (OtherPayableInfoSaveObject otherPayableInfoSaveObject in otherPayableSaveInfoObjList)
                {
                    InvokeReturnHandle<InvokeResult> callResult = InvokeAPI(opearteList, otherPayableInfoSaveObject, InvokeCloudAPI.Login(model), ctx, formID, model);
                    statusMap.Add(otherPayableInfoSaveObject.Model.FBillNo, callResult.CustomOpearteObject.Result);
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
            List<OtherPayableInfoSaveObject> otherPayableInfoSaveObjList = new List<OtherPayableInfoSaveObject>();
            List<OtherPayableDataSet> otherPayableDataSetList = new List<OtherPayableDataSet>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                OtherPayableDataSet otherPayableDataSet = new OtherPayableDataSet();
                otherPayableDataSet.FNumber = dataSet.Tables[0].Rows[i]["FNumber"].ToString();
                otherPayableDataSet.Fdate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["Fdate"]);
                otherPayableDataSet.FendDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FendDate"]);
                otherPayableDataSet.FaccntTimeJudgeTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FaccntTimeJudgeTime"]);
                otherPayableDataSet.FContactUnitType = dataSet.Tables[0].Rows[i]["FContactUnitType"].ToString();
                otherPayableDataSet.FContactUnit = dataSet.Tables[0].Rows[i]["FContactUnit"].ToString();
                otherPayableDataSet.FCurrency = dataSet.Tables[0].Rows[i]["FCurrency"].ToString();
                otherPayableDataSet.FsettleOrg = dataSet.Tables[0].Rows[i]["FsettleOrg"].ToString();
                otherPayableDataSet.FpayOrg = dataSet.Tables[0].Rows[i]["FpayOrg"].ToString();
                otherPayableDataSet.FCost = dataSet.Tables[0].Rows[i]["FCost"].ToString();
                otherPayableDataSet.FCostDepartment = dataSet.Tables[0].Rows[i]["FCostDepartment"].ToString();
                otherPayableDataSet.FInvoiceType = dataSet.Tables[0].Rows[i]["FInvoiceType"].ToString();
                otherPayableDataSet.FEntryTaxRate = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FEntryTaxRate"]);
                otherPayableDataSet.FNoTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FNoTaxAmountFor"]);
                otherPayableDataSet.FTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FTaxAmountFor"]);
                otherPayableDataSet.FAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FAmountFor"]);
                otherPayableDataSet.Fremark = dataSet.Tables[0].Rows[i]["FREMARK"].ToString();
                otherPayableDataSetList.Add(otherPayableDataSet);
            }


            for (int i = 0; i < otherPayableDataSetList.Count; i++)
            {
                //分录
                FEntity fentry = new FEntity();
                //1.费用项目编码
                FCOSTID fCOSTID = new FCOSTID();
                fCOSTID.FNumber = otherPayableDataSetList[i].FCost;
                fentry.FCOSTID = fCOSTID;
                //2.费用承担部门编码
                FCOSTDEPARTMENTID fCOSTDEPARTMENTID = new FCOSTDEPARTMENTID();
                fCOSTDEPARTMENTID.FNumber = otherPayableDataSetList[i].FCostDepartment;
                fentry.FCOSTDEPARTMENTID = fCOSTDEPARTMENTID;
                //3.发票类型： 0普通发票；1 增值税发票 
                fentry.FINVOICETYPE = otherPayableDataSetList[i].FInvoiceType;
                //4.税率(%)
                fentry.FEntryTaxRate = otherPayableDataSetList[i].FEntryTaxRate;
                //5. 不含税金额
                fentry.FNOTAXAMOUNTFOR = otherPayableDataSetList[i].FNoTaxAmountFor;
                //6.税额
                fentry.FTAXAMOUNTFOR = otherPayableDataSetList[i].FTaxAmountFor;
                //7.总金额
                fentry.FTOTALAMOUNTFOR = otherPayableDataSetList[i].FAmountFor;
                if (i + 1 < otherPayableDataSetList.Count)
                {
                    
                    if (otherPayableDataSetList[i].FNumber != otherPayableDataSetList[i + 1].FNumber)
                    {
                        fentryList.Add(fentry);
                        OtherPayableInfoSaveObject otherPayableInfoSaveObject = new OtherPayableInfoSaveObject();
                        Model model = new Model();
                        List<FEntity> fEntityA = new List<FEntity>();
                        fEntityA.AddRange(fentryList);
                        model.FEntity = fEntityA;
                        //1.单据类型
                        FBillTypeID fBillTypeID = new FBillTypeID();
                        fBillTypeID.FNumber = "QTYFD01_SYS";
                        model.FBillTypeID = fBillTypeID;
                        //2.单号
                        model.FBillNo = otherPayableDataSetList[i].FNumber;
                        //3.业务日期
                        model.FDATE = otherPayableDataSetList[i].Fdate;
                        //4.到期日
                        model.FENDDATE_H = otherPayableDataSetList[i].FendDate;
                        //5. 到期日计算日期
                        model.FACCNTTIMEJUDGETIME = otherPayableDataSetList[i].FaccntTimeJudgeTime;
                        //6.往来单位类型：供应商、客户、员工、部门、其他往来单位
                        if (otherPayableDataSetList[i].FContactUnitType.Equals("供应商"))
                        {
                            model.FCONTACTUNITTYPE = "BD_Supplier";
                        }
                        if (otherPayableDataSetList[i].FContactUnitType.Equals("客户"))
                        {
                            model.FCONTACTUNITTYPE = "BD_Customer";
                        }
                        if (otherPayableDataSetList[i].FContactUnitType.Equals("员工"))
                        {
                            model.FCONTACTUNITTYPE = "BD_Empinfo";
                        }
                        if (otherPayableDataSetList[i].FContactUnitType.Equals("部门"))
                        {
                            model.FCONTACTUNITTYPE = "BD_Department";
                        }
                        if (otherPayableDataSetList[i].FContactUnitType.Equals("其他往来单位"))
                        {
                            model.FCONTACTUNITTYPE = "FIN_OTHERS";
                        }

                        //7.往来单位编码
                        FCONTACTUNIT fCONTACTUNIT = new FCONTACTUNIT();
                        fCONTACTUNIT.FNumber = otherPayableDataSetList[i].FContactUnit;
                        model.FCONTACTUNIT = fCONTACTUNIT;
                        //8.币别
                        FCURRENCYID fCURRENCYID = new FCURRENCYID();
                        fCURRENCYID.FNumber = otherPayableDataSetList[i].FCurrency;
                        model.FCURRENCYID = fCURRENCYID;
                        //9.结算组织
                        FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                        fSETTLEORGID.FNumber = otherPayableDataSetList[i].FsettleOrg;
                        model.FSETTLEORGID = fSETTLEORGID;
                        //10.收款组织
                        FPAYORGID fPAYORGID = new FPAYORGID();
                        fPAYORGID.FNumber = otherPayableDataSetList[i].FpayOrg;
                        model.FPAYORGID = fPAYORGID;
                        ////11.总金额
                        //model.FAMOUNTFOR = otherRecAbleDataSetList[i].FAmountFor;
                        //12.本位币
                        //model.FMAINBOOKSTDCURRID= 
                        model.FRemarks = otherPayableDataSetList[i].Fremark;

                        List<FEntity> fEntityB = new List<FEntity>();
                        fEntityB.AddRange(fentryList);
                        model.FEntity = fEntityB;
                        otherPayableInfoSaveObject.Model = model;
                        otherPayableInfoSaveObjList.Add(otherPayableInfoSaveObject);
                        fentryList.Clear();
                    }
                    else
                    {
                        fentryList.Add(fentry);
                    }
                }
                if (otherPayableDataSetList.Count == i + 1)
                {
                    OtherPayableInfoSaveObject otherPayableInfoSaveObject = new OtherPayableInfoSaveObject();
                    fentryList.Add(fentry);
                    Model model = new Model();

                    //1.单据类型
                    FBillTypeID fBillTypeID = new FBillTypeID();
                    fBillTypeID.FNumber = "QTYFD01_SYS";
                    model.FBillTypeID = fBillTypeID;
                    //2.单号
                    model.FBillNo = otherPayableDataSetList[i].FNumber;
                    //3.业务日期
                    model.FDATE = otherPayableDataSetList[i].Fdate;
                    //4.到期日
                    model.FENDDATE_H = otherPayableDataSetList[i].FendDate;
                    //5. 到期日计算日期
                    model.FACCNTTIMEJUDGETIME = otherPayableDataSetList[i].FaccntTimeJudgeTime;
                    //6.往来单位类型：供应商、客户、员工、部门、其他往来单位
                    if (otherPayableDataSetList[i].FContactUnitType.Equals("供应商"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Supplier";
                    }
                    if (otherPayableDataSetList[i].FContactUnitType.Equals("客户"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Customer";
                    }
                    if (otherPayableDataSetList[i].FContactUnitType.Equals("员工"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Empinfo";
                    }
                    if (otherPayableDataSetList[i].FContactUnitType.Equals("部门"))
                    {
                        model.FCONTACTUNITTYPE = "BD_Department";
                    }
                    if (otherPayableDataSetList[i].FContactUnitType.Equals("其他往来单位"))
                    {
                        model.FCONTACTUNITTYPE = "FIN_OTHERS";
                    }
                    //7.往来单位编码
                    FCONTACTUNIT fCONTACTUNIT = new FCONTACTUNIT();
                    fCONTACTUNIT.FNumber = otherPayableDataSetList[i].FContactUnit;
                    model.FCONTACTUNIT = fCONTACTUNIT;
                    //8.币别
                    FCURRENCYID fCURRENCYID = new FCURRENCYID();
                    fCURRENCYID.FNumber = otherPayableDataSetList[i].FCurrency;
                    model.FCURRENCYID = fCURRENCYID;
                    //9.结算组织
                    FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                    fSETTLEORGID.FNumber = otherPayableDataSetList[i].FsettleOrg;
                    model.FSETTLEORGID = fSETTLEORGID;
                    //10.收款组织
                    FPAYORGID fPAYORGID = new FPAYORGID();
                    fPAYORGID.FNumber = otherPayableDataSetList[i].FpayOrg;
                    model.FPAYORGID = fPAYORGID;
                    //11.总金额
                    // model.FAMOUNTFOR = otherRecAbleDataSetList[i].FAmountFor;

                    model.FRemarks = otherPayableDataSetList[i].Fremark;

                    List<FEntity> fEntityB = new List<FEntity>();
                    fEntityB.AddRange(fentryList);
                    model.FEntity = fEntityB;
                    otherPayableInfoSaveObject.Model = model;
                    otherPayableInfoSaveObjList.Add(otherPayableInfoSaveObject);
                }

            }
            return (List<T>)(object)otherPayableInfoSaveObjList;
        }
    }
}
