using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using System.Data.SqlClient;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Payable : SchTask
    {
        public string SQLStatement;
        public string formID = "AP_Payable";
        //public bool CallResult = false;
        string[] opearteList = { };
        public static string model = "payable";
        public List<FEntityDetail> fentryList = new List<FEntityDetail>();
        public List<PayableInfoSaveObject> payableSaveInfoObjList = null;

        public override void Run(Context ctx, Schedule schedule)
        {
            SQLStatement = base.GetStatement(PayableSQLObject.Payable);
            opearteList = GetOpearteList(PayableSQLObject.Payable);
            if (connectionToRemoteDatabase().Sucessd)
            {
                ExcuteDataBaseResult excuteDataBaseResult = base.remoteExcuteDataBase.excuteStatement(SQLStatement, connectionToRemoteDatabase().Sqlconn);
                payableSaveInfoObjList = handleData<PayableInfoSaveObject>(excuteDataBaseResult.Ds);
            }
            else
            {
                //打印
            }

            if (LoginAPI(model))
            {
                Dictionary<string, Result> statusMap = new Dictionary<string, Result>();


                foreach (PayableInfoSaveObject payableSaveInfoObject in payableSaveInfoObjList)
                {
                    InvokeReturnHandle<InvokeResult> callResult = InvokeAPI(opearteList, payableSaveInfoObject, InvokeCloudAPI.Login(model), ctx, formID, model);
                    statusMap.Add(payableSaveInfoObject.Model.FBillNo, callResult.CustomOpearteObject.Result);
                }
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model, statusMap);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model, "t_kf_payable", Sqlconn);
                }
            }
        }
        public override List<T> handleData<T>(DataSet dataSet)
        {
            List<PayableInfoSaveObject> payableSaveInfoObjList = new List<PayableInfoSaveObject>();
            fentryList.Clear();
            List<PayableDataSet> payableDataSetList = new List<PayableDataSet>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                PayableDataSet payableDataSet = new PayableDataSet();
                payableDataSet.FNumber = dataSet.Tables[0].Rows[i]["FNumber"].ToString();
                payableDataSet.Fdate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["Fdate"]);
                payableDataSet.FendDate = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FendDate"]);
                payableDataSet.FaccntTimeJudgeTime = Convert.ToDateTime(dataSet.Tables[0].Rows[i]["FaccntTimeJudgeTime"]);
                payableDataSet.Fsupplier = dataSet.Tables[0].Rows[i]["Fsupplier"].ToString();
                payableDataSet.FsettleOrg = dataSet.Tables[0].Rows[i]["FsettleOrg"].ToString();
                payableDataSet.FpayOrg = dataSet.Tables[0].Rows[i]["FpayOrg"].ToString();
                payableDataSet.FpurchaseOrg = dataSet.Tables[0].Rows[i]["FpurchaseOrg"].ToString();
                payableDataSet.Fcurrency = dataSet.Tables[0].Rows[i]["Fcurrency"].ToString();
                payableDataSet.Fmaterial = dataSet.Tables[0].Rows[i]["Fmaterial"].ToString();
                payableDataSet.FpriceUnit = dataSet.Tables[0].Rows[i]["FpriceUnit"].ToString();
                payableDataSet.Fprice = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["Fprice"]);
                payableDataSet.FpriceQty = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FpriceQty"]);
                payableDataSet.FtaxPrice = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FtaxPrice"]);
                payableDataSet.FentryTaxRate = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FentryTaxRate"]);
                payableDataSet.FEntryDiscountRate = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FEntryDiscountRate"]);
                payableDataSet.FdiscountAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FdiscountAmountFor"]);
                payableDataSet.FNoTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FNoTaxAmountFor"]);
                payableDataSet.FTaxAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FTaxAmountFor"]);
                payableDataSet.FallAmountFor = Convert.ToDecimal(dataSet.Tables[0].Rows[i]["FallAmountFor"]);
                payableDataSet.Fremark = dataSet.Tables[0].Rows[i]["FREMARK"].ToString();
                payableDataSetList.Add(payableDataSet);
            }


            int j = 0;

            for (int i = 0; i < payableDataSetList.Count; i++)
            {

                //分录
                FEntityDetail fentryDetail = new FEntityDetail();
                //1.物料
                FMATERIALID fMATERIALID = new FMATERIALID();
                fMATERIALID.FNumber = payableDataSetList[i].Fmaterial;
                fentryDetail.FMATERIALID = fMATERIALID;
                //2.计价单位编码
                FPRICEUNITID fPRICEUNITID = new FPRICEUNITID();
                fPRICEUNITID.FNumber = payableDataSetList[i].FpriceUnit;
                fentryDetail.FPRICEUNITID = fPRICEUNITID;
                ///3.计价 数量
                fentryDetail.FPriceQty = payableDataSetList[i].FpriceQty;
                //3.单价
                fentryDetail.FPrice = payableDataSetList[i].Fprice;
                //4.含税单价
                fentryDetail.FTaxPrice = payableDataSetList[i].FtaxPrice;
                //5.税率(%)
                fentryDetail.FentryTaxRate = payableDataSetList[i].FentryTaxRate;
                //6.折扣率(%)
                fentryDetail.FEntryDiscountRate = payableDataSetList[i].FEntryDiscountRate;
                //7.折扣额
                fentryDetail.FdiscountAmountFor = payableDataSetList[i].FdiscountAmountFor;
                //8.价税合计
                fentryDetail.FALLAMOUNTFOR_D = payableDataSetList[i].FallAmountFor;

                ///10.不含税金额
                fentryDetail.FNoTaxAmountFor_D = payableDataSetList[i].FNoTaxAmountFor;
                ///11. 税额
                fentryDetail.FTAXAMOUNTFOR_D = payableDataSetList[i].FTaxAmountFor;

                if (i + 1 < payableDataSetList.Count)
                {
                    //fentryList.Add(fentryDetail);
                    if (payableDataSetList[i].FNumber != payableDataSetList[i + 1].FNumber)
                    {
                        fentryList.Add(fentryDetail);
                        PayableInfoSaveObject payableInfoSaveObject = new PayableInfoSaveObject();
                        Model model = new Model();
                        List<FEntityDetail> fEntityA = new List<FEntityDetail>();
                        fEntityA.AddRange(fentryList);
                        model.FEntityDetail = fEntityA;
                   
                        //1.单据类型
                        FBillTypeID fBillTypeID = new FBillTypeID();
                        fBillTypeID.FNumber = "YFD01_SYS";
                        model.FBillTypeID = fBillTypeID;
                        //2.单号
                        model.FBillNo = payableDataSetList[i].FNumber;
                        //3.供应商
                        FSUPPLIERID fSUPPLIERID = new FSUPPLIERID();
                        fSUPPLIERID.FNumber = payableDataSetList[i].Fsupplier;
                        model.FSUPPLIERID = fSUPPLIERID;
                        //4.作废状态
                        model.FCancelStatus = "A";
                        //5.业务类型 默认普通采购
                        model.FBUSINESSTYPE = "CG";
                        //6.单据状态
                        model.FDOCUMENTSTATUS = "A";
                        //7.业务日期
                        model.FDATE = payableDataSetList[i].Fdate;
                        //8.到期日
                        model.FENDDATE_H = payableDataSetList[i].FendDate;
                        //9.币别
                        FCURRENCYID fCURRENCYID = new FCURRENCYID();
                        fCURRENCYID.FNumber = payableDataSetList[i].Fcurrency;
                        model.FCURRENCYID = fCURRENCYID;
                        //10.结算组织
                        FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                        fSETTLEORGID.FNumber = payableDataSetList[i].FsettleOrg;
                        model.FSETTLEORGID = fSETTLEORGID;
                        //11.付款组织
                        FPAYORGID fPAYORGID = new FPAYORGID();
                        fPAYORGID.FNumber = payableDataSetList[i].FpayOrg;
                        model.FPAYORGID = fPAYORGID;
                        //12.采购组织
                        FPURCHASEORGID fPURCHASEORGID = new FPURCHASEORGID();
                        fPURCHASEORGID.FNumber = payableDataSetList[i].FpurchaseOrg;
                        model.FPURCHASEORGID = fPURCHASEORGID;

                        model.FAP_Remark = payableDataSetList[i].Fremark;

                        //13.头部财务信息
                        FsubHeadFinc fsubHeadFinc = new FsubHeadFinc();
                        //13.1 到期日计算日期
                        fsubHeadFinc.FaccntTimeJudgeTime = payableDataSetList[i].FaccntTimeJudgeTime;
                        //13.2 不含税金额
                        fsubHeadFinc.FNoTaxAmountFor = payableDataSetList[i].FNoTaxAmountFor;
                        //13.3 税额
                        fsubHeadFinc.FTaxAmountFor = payableDataSetList[i].FTaxAmountFor;
                        model.FsubHeadFinc = fsubHeadFinc;
                        List<FEntityDetail> fEntityB = new List<FEntityDetail>();
                        fEntityB.AddRange(fentryList);
                        model.FEntityDetail = fEntityB;
                        payableInfoSaveObject.Model = model;
                        payableSaveInfoObjList.Add(payableInfoSaveObject);
                        fentryList.Clear();
                    }
                    else
                    {
                        fentryList.Add(fentryDetail);
                    }
                }
                if (payableDataSetList.Count == i + 1)
                {
                    PayableInfoSaveObject payableInfoSaveObject = new PayableInfoSaveObject();
                    fentryList.Add(fentryDetail);
                    Model model = new Model();
                    //1.单据类型
                    FBillTypeID fBillTypeID = new FBillTypeID();
                    fBillTypeID.FNumber = "YFD01_SYS";
                    model.FBillTypeID = fBillTypeID;
                    //2.单号
                    model.FBillNo = payableDataSetList[i].FNumber;
                    //3.供应商
                    FSUPPLIERID fSUPPLIERID = new FSUPPLIERID();
                    fSUPPLIERID.FNumber = payableDataSetList[i].Fsupplier;
                    model.FSUPPLIERID = fSUPPLIERID;
                    //4.作废状态
                    model.FCancelStatus = "A";
                    //5.业务类型 默认普通采购
                    model.FBUSINESSTYPE = "CG";
                    //6.单据状态
                    model.FDOCUMENTSTATUS = "A";
                    //7.业务日期
                    model.FDATE = payableDataSetList[i].Fdate;
                    //8.到期日
                    model.FENDDATE_H = payableDataSetList[i].FendDate;
                    //9.币别
                    FCURRENCYID fCURRENCYID = new FCURRENCYID();
                    fCURRENCYID.FNumber = payableDataSetList[i].Fcurrency;
                    model.FCURRENCYID = fCURRENCYID;
                    //10.结算组织
                    FSETTLEORGID fSETTLEORGID = new FSETTLEORGID();
                    fSETTLEORGID.FNumber = payableDataSetList[i].FsettleOrg;
                    model.FSETTLEORGID = fSETTLEORGID;
                    //11.付款组织
                    FPAYORGID fPAYORGID = new FPAYORGID();
                    fPAYORGID.FNumber = payableDataSetList[i].FpayOrg;
                    model.FPAYORGID = fPAYORGID;
                    //12.采购组织
                    FPURCHASEORGID fPURCHASEORGID = new FPURCHASEORGID();
                    fPURCHASEORGID.FNumber = payableDataSetList[i].FpurchaseOrg;
                    model.FPURCHASEORGID = fPURCHASEORGID;
                    //备注
                    model.FAP_Remark = payableDataSetList[i].Fremark;
                    //13.头部财务信息
                    FsubHeadFinc fsubHeadFinc = new FsubHeadFinc();
                    //13.1 到期日计算日期
                    fsubHeadFinc.FaccntTimeJudgeTime = payableDataSetList[i].FaccntTimeJudgeTime;
                    //13.2 不含税金额
                    fsubHeadFinc.FNoTaxAmountFor = payableDataSetList[i].FNoTaxAmountFor;
                    //13.3 税额
                    fsubHeadFinc.FTaxAmountFor = payableDataSetList[i].FTaxAmountFor;
                    model.FsubHeadFinc = fsubHeadFinc;
                    List<FEntityDetail> fEntityB = new List<FEntityDetail>();
                    fEntityB.AddRange(fentryList);
                    model.FEntityDetail = fEntityB;
                    payableInfoSaveObject.Model = model;
                    payableSaveInfoObjList.Add(payableInfoSaveObject);

                }
            }

            return (List<T>)(object)payableSaveInfoObjList;
        }
    }
}

//fentryList.Add(fEntry);
//Model model = new Model();
////账簿
//FAccountBookNumber FAccountBookNumber = new FAccountBookNumber();
//FAccountBookNumber.Fnumber = voucherDataSetList[i].FAccountBookNumber;
//model.FAccountBookID = FAccountBookNumber;
////日期
//model.FDate = voucherDataSetList[i].FDate.ToLongDateString();
////GL
//FSystemID fSystemID = new FSystemID();
//fSystemID.Fnumber = "GL";
//model.FSystemID = fSystemID;
////凭证字
//FVoucherGroupID FVoucherGroupID = new FVoucherGroupID();
//if (VoucherNumbers.ContainsKey(voucherDataSetList[i].FVoucherGroupNumber))
//{
//    FVoucherGroupID.Fnumber = VoucherNumbers[voucherDataSetList[i].FVoucherGroupNumber];
//}
//// FVoucherGroupID.Fnumber = voucherDataSetList[i].FVoucherGroupNumber;
//model.FVoucherGroupID = FVoucherGroupID;
////审核状态
//model.FDocumentStatus = "A";
////凭证号
//model.FVOUCHERGROUPNO = voucherDataSetList[i].Fnumber;
////是否新增
//model.FISADJUSTVOUCHER = "false";
//model.FVOUCHERID = "0";
////核算组织
//FAccbookOrgID FAccbookOrgID = new FAccbookOrgID();
//FAccbookOrgID.Fnumber = voucherDataSetList[i].FAccbookOrgID;
//model.FAccbookOrgID = FAccbookOrgID;
//List<FEntity> fEntityB = new List<FEntity>();
//fEntityB.AddRange(fentryList);
//model.FEntity = fEntityB;
////model.FEntity = fentryList;
//VoucherInfoSaveObject voucherSaveInfoObj = new VoucherInfoSaveObject();
//// voucherSaveInfoObj.needUpDateFields= new string[] {};
//voucherSaveInfoObj.Model = model;
//voucherSaveInfoObj.Fid = voucherDataSetList[i].Fid.ToString();
//voucherSaveInfoObjList.Add(voucherSaveInfoObj);
//  fentryList.Clear();
//    fentryList.Add(fEntry);
//    VoucherInfoSaveObject voucherSaveInfoObj = new VoucherInfoSaveObject();
//    Model model = new Model();
//    List<FEntity> fEntityA = new List<FEntity>();
//    fEntityA.AddRange(fentryList);
//    model.FEntity = fEntityA;

//    j = j + 1;

//    // model.FVoucherID = j;
//    //账簿
//    FAccountBookNumber FAccountBookNumber = new FAccountBookNumber();
//    FAccountBookNumber.Fnumber = voucherDataSetList[i].FAccountBookNumber;
//    model.FAccountBookID = FAccountBookNumber;
//    //日期
//    model.FDate = voucherDataSetList[i].FDate.ToLongDateString();

//    //GL
//    FSystemID fSystemID = new FSystemID();
//    fSystemID.Fnumber = "GL";
//    model.FSystemID = fSystemID;
//    //是否新增
//    model.FISADJUSTVOUCHER = "false";
//    //xxx
//    model.FVOUCHERID = "0";
//    //凭证字
//    FVoucherGroupID FVoucherGroupID = new FVoucherGroupID();
//    if (VoucherNumbers.ContainsKey(voucherDataSetList[i].FVoucherGroupNumber))
//    {
//        FVoucherGroupID.Fnumber = VoucherNumbers[voucherDataSetList[i].FVoucherGroupNumber];
//    }
//    // = voucherDataSetList[i].FVoucherGroupNumber;
//    model.FVoucherGroupID = FVoucherGroupID;
//    //审核状态
//    model.FDocumentStatus = "A";
//    //凭证号
//    model.FVOUCHERGROUPNO = voucherDataSetList[i].Fnumber;
//    //核算组织
//    FAccbookOrgID FAccbookOrgID = new FAccbookOrgID();
//    FAccbookOrgID.Fnumber = voucherDataSetList[i].FAccbookOrgID;
//    model.FAccbookOrgID = FAccbookOrgID;
//    // model.FEntity = fentryList1;

//    voucherSaveInfoObj.Model = model;
//    voucherSaveInfoObj.Fid = voucherDataSetList[i].Fid.ToString();
//    //   voucherSaveInfoObj.needUpDateFields = new string[] { "FACCBOOKORGID", "FDate", "FAccountBookID", "FVOUCHERGROUPID", "FEntity", "FEXPLANATION", "FACCOUNTID", "FDEBIT", "FCREDIT", "FEXCHANGERATE", "fAmount", "fAmountFOR" };
//    voucherSaveInfoObjList.Add(voucherSaveInfoObj);
//    fentryList.Clear();