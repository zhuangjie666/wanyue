using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.AR_receivable;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using System.Data.SqlClient;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    public class SchTask4Receivable : SchTask
    {
        public string SQLStatement;
        public string formID = "AR_receivable";
        public bool CallResult = false;
        string[] opearteList = { };
        public static string model = "receivable";
        public List<FEntityDetail> fentryList = new List<FEntityDetail>();
        public Dictionary<string, string> VoucherNumbers = new Dictionary<string, string>();
        public List<ReceivableInfoSaveObject> receivableSaveInfoObjList = null;

        public override void Run(Context ctx, Schedule schedule)
        {
            SQLStatement = base.GetStatement(ReceivableSQLObject.Receivable);
            opearteList = GetOpearteList(ReceivableSQLObject.Receivable);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                receivableSaveInfoObjList = handleData<ReceivableInfoSaveObject>(excuteDataBaseResult.Ds);
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


                foreach (ReceivableInfoSaveObject receivableInfoSaveObject in receivableSaveInfoObjList)
                {
                    InvokeReturnHandle<InvokeResult> callResult = InvokeAPI(opearteList, receivableInfoSaveObject, InvokeCloudAPI.Login(model), ctx, formID, model);
                    statusMap.Add(receivableInfoSaveObject.Model.FBillNo, callResult.CustomOpearteObject.Result);
                }
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model, statusMap);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model, "t_kf_receivable", Sqlconn);
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
            List<ReceivableInfoSaveObject> receivableSaveInfoObjList = new List<ReceivableInfoSaveObject>();
            List<ReceivableDataSet> receivableDataSetList = new List<ReceivableDataSet>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                ReceivableDataSet receivableDataSet = new ReceivableDataSet();
                receivableDataSet.FNumber = dataSet.Tables[0].Rows[i]["FNumber"].ToString();
                receivableDataSet.Fdate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["Fdate"]);
                receivableDataSet.FendDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FendDate"]);
                receivableDataSet.FaccntTimeJudgeTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FaccntTimeJudgeTime"]);
                receivableDataSet.FCustomer = dataSet.Tables[0].Rows[i]["FCustomer"].ToString();
                receivableDataSet.FsettleOrg = dataSet.Tables[0].Rows[i]["FsettleOrg"].ToString();
                receivableDataSet.FpayOrg = dataSet.Tables[0].Rows[i]["FpayOrg"].ToString();
                receivableDataSet.FpurchaseOrg = dataSet.Tables[0].Rows[i]["FSaleOrg"].ToString();
                receivableDataSet.Fcurrency = dataSet.Tables[0].Rows[i]["Fcurrency"].ToString();
                receivableDataSet.Fmaterial = dataSet.Tables[0].Rows[i]["Fmaterial"].ToString();
                receivableDataSet.FpriceUnit = dataSet.Tables[0].Rows[i]["FpriceUnit"].ToString();
                receivableDataSet.Fprice = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["Fprice"]);
                receivableDataSet.FpriceQty = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FpriceQty"]);
                receivableDataSet.FtaxPrice = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FtaxPrice"]);
                receivableDataSet.FentryTaxRate = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FentryTaxRate"]);
                receivableDataSet.FEntryDiscountRate = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FEntryDiscountRate"]);
                receivableDataSet.FdiscountAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FdiscountAmountFor"]);
                receivableDataSet.FNoTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FNoTaxAmountFor"]);
                receivableDataSet.FTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FTaxAmountFor"]);
                receivableDataSet.FallAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FallAmountFor"]);
                receivableDataSet.Fremark = dataSet.Tables[0].Rows[i]["FREMARK"].ToString();
                receivableDataSetList.Add(receivableDataSet);
            }
            for (int i = 0; i < receivableDataSetList.Count; i++)
            {
                //分录
                FEntityDetail fentryDetail = new FEntityDetail();
                //1.物料
                FMATERIALID fMATERIALID = new FMATERIALID();
                fMATERIALID.FNumber = receivableDataSetList[i].Fmaterial;
                fentryDetail.FMATERIALID = fMATERIALID;
                //2.计价单位编码
                FPRICEUNITID fPRICEUNITID = new FPRICEUNITID();
                //if ("Pcs".Equals(receivableDataSetList[i].FpriceUnit)) {
                //    fPRICEUNITID.FNumber = "10101";
                //}
                fPRICEUNITID.FNumber = receivableDataSetList[i].FpriceUnit;
                fentryDetail.FPRICEUNITID = fPRICEUNITID;
                //3.单价
                fentryDetail.FPrice = receivableDataSetList[i].Fprice;
                //4.含税单价
                fentryDetail.FTaxPrice = receivableDataSetList[i].FtaxPrice;
                //5.税率(%)
                fentryDetail.FEntryTaxRate = receivableDataSetList[i].FentryTaxRate;
                //6.折扣率(%)
                fentryDetail.FEntryDiscountRate = receivableDataSetList[i].FEntryDiscountRate;
                //7.折扣额
                fentryDetail.FDISCOUNTAMOUNTFOR = receivableDataSetList[i].FdiscountAmountFor;
                //8.价税合计
                fentryDetail.FALLAMOUNTFOR_D = receivableDataSetList[i].FallAmountFor;
                //9.计价数量
                fentryDetail.FPriceQty = receivableDataSetList[i].FpriceQty;
                //10. 不含税金额 
                fentryDetail.FNoTaxAmountFor_D = receivableDataSetList[i].FNoTaxAmountFor;
                //11.税额
                fentryDetail.FTaxAmountFor_D = receivableDataSetList[i].FTaxAmountFor;
                //12. 
                FSalUnitId fSalUnitId = new FSalUnitId();
                fSalUnitId.FNumber = receivableDataSetList[i].FpriceUnit;
                fentryDetail.FSalUnitId = fSalUnitId;

                if (i + 1 < receivableDataSetList.Count)
                {
                   // fentryList.Add(fentryDetail);
                    if (receivableDataSetList[i].FNumber != receivableDataSetList[i + 1].FNumber)
                    {
                        fentryList.Add(fentryDetail);
                        ReceivableInfoSaveObject receivableInfoSaveObject = new ReceivableInfoSaveObject();
                        Model model = new Model();
                        List<FEntityDetail> fEntityA = new List<FEntityDetail>();
                        fEntityA.AddRange(fentryList);
                        model.FEntityDetail = fEntityA;

                        //1.单据类型
                        FBillTypeID fBillTypeID = new FBillTypeID();
                        fBillTypeID.FNumber = "YSD01_SYS";
                        model.FBillTypeID = fBillTypeID;
                        //2.单号
                        model.FBillNo = receivableDataSetList[i].FNumber;
                        //3.客户
                        FCUSTOMERID fCUSTOMERID = new FCUSTOMERID();
                        fCUSTOMERID.FNumber = receivableDataSetList[i].FCustomer;
                        model.FCUSTOMERID = fCUSTOMERID;
                        //4.作废状态
                        model.FCancelStatus = "A";
                        //5.业务类型 默认普通采购
                        model.FBUSINESSTYPE = "BZ";
                        //6.单据状态
                        model.FDOCUMENTSTATUS = "A";
                        //7.业务日期
                        model.FDATE = receivableDataSetList[i].Fdate;
                        //8.到期日
                        model.FENDDATE_H = receivableDataSetList[i].FendDate;
                        //9.币别
                        FCURRENCYID fCURRENCYID = new FCURRENCYID();
                        fCURRENCYID.FNumber = receivableDataSetList[i].Fcurrency;
                        model.FCURRENCYID = fCURRENCYID;
                        //10.结算组织
                        FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                        fSETTLEORGID.FNumber = receivableDataSetList[i].FsettleOrg;
                        model.FSETTLEORGID = fSETTLEORGID;
                        //11.付款组织
                        FPAYORGID fPAYORGID = new FPAYORGID();
                        fPAYORGID.FNumber = receivableDataSetList[i].FpayOrg;
                        model.FPAYORGID = fPAYORGID;

                        model.FAR_Remark = receivableDataSetList[i].Fremark;

                        ////12.采购组织
                        //FPURCHASEORGID fPURCHASEORGID = new FPURCHASEORGID();
                        //fPURCHASEORGID.FNumber = receivableDataSetList[i].FpurchaseOrg;
                        //model.FPURCHASEORGID = fPURCHASEORGID;
                        //13.头部财务信息
                        FsubHeadFinc fsubHeadFinc = new FsubHeadFinc();
                        //13.1 到期日计算日期
                        fsubHeadFinc.FACCNTTIMEJUDGETIME = receivableDataSetList[i].FaccntTimeJudgeTime;
                        //13.2 不含税金额
                        //fsubHeadFinc.FNoTaxAmountFor = receivableDataSetList[i].FNoTaxAmountFor;
                        ////13.3 税额
                        //fsubHeadFinc.FTaxAmountFor = receivableDataSetList[i].FTaxAmountFor;
                        model.FsubHeadFinc = fsubHeadFinc;
                        List<FEntityDetail> fEntityB = new List<FEntityDetail>();
                        fEntityB.AddRange(fentryList);
                        model.FEntityDetail = fEntityB;
                        receivableInfoSaveObject.Model = model;
                        receivableSaveInfoObjList.Add(receivableInfoSaveObject);
                        fentryList.Clear();
                    }
                    else
                    {
                        fentryList.Add(fentryDetail);
                    }
                }
                if (receivableDataSetList.Count == i + 1)
                {
                    fentryList.Add(fentryDetail);
                    ReceivableInfoSaveObject receivableInfoSaveObject = new ReceivableInfoSaveObject();
                    Model model = new Model();
                    //1.单据类型
                    FBillTypeID fBillTypeID = new FBillTypeID();
                    fBillTypeID.FNumber = "YSD01_SYS";
                    model.FBillTypeID = fBillTypeID;
                    //2.单号
                    model.FBillNo = receivableDataSetList[i].FNumber;
                    //3.客户
                    FCUSTOMERID fCUSTOMERID = new FCUSTOMERID();
                    fCUSTOMERID.FNumber = receivableDataSetList[i].FCustomer;
                    model.FCUSTOMERID = fCUSTOMERID;
                    //4.作废状态
                    model.FCancelStatus = "A";
                    //5.业务类型 默认普通采购
                    model.FBUSINESSTYPE = "BZ";
                    //6.单据状态
                    model.FDOCUMENTSTATUS = "A";
                    //7.业务日期
                    model.FDATE = receivableDataSetList[i].Fdate;
                    //8.到期日
                    model.FENDDATE_H = receivableDataSetList[i].FendDate;
                    //9.币别
                    FCURRENCYID fCURRENCYID = new FCURRENCYID();
                    fCURRENCYID.FNumber = receivableDataSetList[i].Fcurrency;
                    model.FCURRENCYID = fCURRENCYID;
                    //10.结算组织
                    FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                    fSETTLEORGID.FNumber = receivableDataSetList[i].FsettleOrg;
                    model.FSETTLEORGID = fSETTLEORGID;
                    //11.付款组织
                    FPAYORGID fPAYORGID = new FPAYORGID();
                    fPAYORGID.FNumber = receivableDataSetList[i].FpayOrg;
                    model.FPAYORGID = fPAYORGID;

                    model.FAR_Remark = receivableDataSetList[i].Fremark;
                    ////12.采购组织
                    //FPURCHASEORGID fPURCHASEORGID = new FPURCHASEORGID();
                    //fPURCHASEORGID.FNumber = receivableDataSetList[i].FpurchaseOrg;
                    //model.FPURCHASEORGID = fPURCHASEORGID;
                    //13.头部财务信息
                    FsubHeadFinc fsubHeadFinc = new FsubHeadFinc();
                    //13.1 到期日计算日期
                    fsubHeadFinc.FACCNTTIMEJUDGETIME = receivableDataSetList[i].FaccntTimeJudgeTime;
                    ////13.2 不含税金额
                    //fsubHeadFinc.FNoTaxAmountFor = receivableDataSetList[i].FNoTaxAmountFor;
                    ////13.3 税额
                    //fsubHeadFinc.FTaxAmountFor = receivableDataSetList[i].FTaxAmountFor;
                    model.FsubHeadFinc = fsubHeadFinc;
                    List<FEntityDetail> fEntityB = new List<FEntityDetail>();
                    fEntityB.AddRange(fentryList);
                    model.FEntityDetail = fEntityB;
                    receivableInfoSaveObject.Model = model;
                    receivableSaveInfoObjList.Add(receivableInfoSaveObject);
                    fentryList.Clear();
                }
            }
            
            return (List<T>)(object)receivableSaveInfoObjList;
        }
    }


}
