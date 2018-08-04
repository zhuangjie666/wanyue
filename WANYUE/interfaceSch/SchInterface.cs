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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.interfaceSch {

    public interface SchInterface
    {

        ConnectionResult connectionToRemoteDatabase();
        bool closeConnetction();
        bool LoginAPI(string model);
        bool InvokeAPI<T>(string[] opearteList, T t, LoginResult loginResult, Context ctx);

         List<T> handleData<T>(DataSet dataSet);

        InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx);
    }
}


