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
      //  public List<FEntity> fentryList = new List<FEntity>();
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
                    statusMap.Add(otherPayableInfoSaveObject.FNumber, callResult.CustomOpearteObject.Result);
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
            throw new NotImplementedException();
        }
    }


    
}
