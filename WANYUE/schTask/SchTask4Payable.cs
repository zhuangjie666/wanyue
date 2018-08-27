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
        //public List<FEntity> fentryList = new List<FEntity>();
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
                List<string> getUpdateSQLStatements = new SQLStatement(model).getUpdateSQLStatement(model,statusMap);
                SqlConnection Sqlconn = connectionToRemoteDatabase().Sqlconn;
                foreach (string UpdateSQLStatement in getUpdateSQLStatements)
                {
                    updateMiddleDataBase(UpdateSQLStatement, model, "t_kf_payable",Sqlconn);
                }
            }
        }
        public override List<T> handleData<T>(DataSet dataSet)
        {
            List<PayableInfoSaveObject> payableSaveInfoObjList = new List<PayableInfoSaveObject>();

            List<PayableDataSet> payableDataSetList = new List<PayableDataSet>();
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                PayableDataSet payableDataSet = new PayableDataSet();

                payableDataSetList.Add(payableDataSet);
            }


            int j = 0;

            for (int i = 0; i < payableDataSetList.Count; i++)
            {

                //分录


                if (i + 1 < payableDataSetList.Count)
                {

                    //if (payableDataSetList[i].Fnumber != payableDataSetList[i + 1].Fnumber)
                    //{




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
                        //}
                        //else
                        //{
                        //    fentryList.Add(fEntry);
                        //}
                    //}
                    if (payableDataSetList.Count == i + 1)
                    {
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
                    }
                }

               
            }
            return (List<T>)(object)payableSaveInfoObjList;
        }
    }
}
