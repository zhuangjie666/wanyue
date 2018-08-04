using Kingdee.K3.WANYUE.PlugIn.service.interfaceSch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.middleDataBaseStatemnt;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Authentication;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer.customSave;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    public abstract class SchTask : IScheduleService, SchInterface
    {
        public RemoteExcuteDataBase remoteExcuteDataBase;
        public abstract void Run(Context ctx, Schedule schedule);

        public string GetStatement(string sourceObject) {
            return new SQLStatement(sourceObject).returnSQLStatement(sourceObject);
        }

        public abstract bool InvokeAPI<T>(string[] opearteList, T t, Invoke.LoginResult loginResult, Context ctx);

        public ConnectionResult connectionToRemoteDatabase()
        {
             remoteExcuteDataBase = new RemoteExcuteDataBase(DataBaseInfo.IP, DataBaseInfo.database, DataBaseInfo.userid, DataBaseInfo.password);
            if (!remoteExcuteDataBase.connectionToRemoteDatabase().Sucessd)
            {
                BussnessLog.WriteBussnessLog("连接远程数据库IP=" + DataBaseInfo.IP + "数据库名称=" + DataBaseInfo.database + "userid=" + DataBaseInfo.userid + "密码=" + "******" + " 错误");
                remoteExcuteDataBase.Result = false;
            }
            remoteExcuteDataBase.Result = true;
            ConnectionResult result = remoteExcuteDataBase.connectionToRemoteDatabase();
            return result;

            throw new NotImplementedException();
        }

        public bool closeConnetction()
        {
            if (!remoteExcuteDataBase.closeConnection(remoteExcuteDataBase.connectionToRemoteDatabase().Sqlconn))
            {
                BussnessLog.WriteBussnessLog("关闭数据库错误！");
                return false;
            }
            else
            {
                return true;
            }
            throw new NotImplementedException();
        }

        public bool LoginAPI(string model)
        {
            Invoke.LoginResult loginResult = InvokeCloudAPI.Login(model);
            if (loginResult.LoginResultType != 1)
            {
                return false;
            }

            return true;
            throw new NotImplementedException();
        }
    }
}
