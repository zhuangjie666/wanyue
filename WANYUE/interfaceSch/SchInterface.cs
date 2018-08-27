using Kingdee.BOS;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer.customSave;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.schTask;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.interfaceSch {
    [Kingdee.BOS.Util.HotUpdate]
    public interface SchInterface
    {

        bool updateMiddleDataBase(string updateSQLStatement,string model,string tableName, SqlConnection Sqlconn);

        ConnectionResult connectionToRemoteDatabase();
        bool closeConnetction(SqlConnection sqlConn);
        bool LoginAPI(string model);
        InvokeReturnHandle<InvokeResult> InvokeAPI<T>(string[] opearteList, T t, LoginResult loginResult, Context ctx,string formID,string model);

         List<T> handleData<T>(DataSet dataSet);

        InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx);
    }
}


