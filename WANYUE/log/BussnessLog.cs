using Kingdee.K3.WANYUE.PlugIn.service.Invoke;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service
{
    [Kingdee.BOS.Util.HotUpdate]
    public class BussnessLog
    {
        //public static FileStream defaultFileStream = new FileStream("C:\\cloud\\SystemLog\\log.txt", FileMode.Append);
        public static string defaultFileStream = "C:\\cloud\\SystemLog\\log.txt";
        public static Dictionary<string, string> logFiles = new Dictionary<string, string>();
        //public static Dictionary<string, FileStream> logFiles = new Dictionary<string, FileStream>();
        public static string filename=null;
        static object ob = new object();
        public static StreamWriter sw = null;
        public static string Path = "";
        public static Thread thread;
        static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
          //public static Dictionary<string, FileStream> createLogFileWithFunction(string modelList) {
        public static Dictionary<string, string> createLogFileWithFunction(string modelList)
        {
            string[] models= modelList.Split(',');
            foreach (string model in models) {
                filename = "C:\\cloud\\BussnessLog\\" + model + "log.txt";
                if (!logFiles.ContainsKey(model)) {
                    logFiles.Add(model, filename);
                }
            }
            return logFiles;
        }
        public static void WriteBussnessLog<T>(T t,string model,string errorInfo)
        {
            
            //TODO: 日志文件按照模块分开处理
            //.1.检查当前目录文件是否存在

            StringBuilder sb = new StringBuilder();
            StringBuilder errorSB = new StringBuilder();
            if (t is InvokeResult)
            {
                InvokeResult invokeResult = (InvokeResult)(object)t;
                errorSB.Append("调用模块=" + model);
                errorSB.Append("错误代码=" + invokeResult.Result.ResponseStatus.ErrorCode);
                errorSB.Append(",错误原因: 字段=" + invokeResult.Result.ResponseStatus.Errors[0].FieldName);
                errorSB.Append(",错误原因: 错误消息=" + invokeResult.Result.ResponseStatus.Errors[0].Message);

            }
            else if (t is RemoteExcuteDataBase)
            {
                RemoteExcuteDataBase RemoteExcuteDataBase = (RemoteExcuteDataBase)(object)t;
                errorSB.Append("连接中间层数据库.....");
                errorSB.Append("中间数据库IP地址=" + DataBaseInfo.IP).Append(",数据库名称=" + DataBaseInfo.database);
                errorSB.Append(",用户ID=" + DataBaseInfo.userid).Append(",密码=" + ToBase64String.ToBase64(DataBaseInfo.password));
            }
            else if (t is LoginResult) {
                LoginResult loginResult = (LoginResult)(object)t;
                errorSB.Append(model).Append(errorInfo).Append("错误");
                errorSB.Append("错误代码=").Append(loginResult.MessageCode);
                errorSB.Append(",错误原因=").Append(loginResult.Message);
            }

            else if (t is bool)
            {
                errorSB.Append(model).Append(",").Append(errorInfo);
            }
            else if (t is string)
            {
                errorSB.Append(model).Append(",").Append(errorInfo);
            }else
            {
                errorSB.Append(model).Append(",").Append(errorInfo);
            }
            sb.Append("--").Append(DateTime.Now.ToString()).Append("    ").Append(errorSB);

            if (!logFiles.ContainsKey(model))
            {
                createLogFileWithFunction(model);
            }
            foreach (KeyValuePair<string, string> kvp in logFiles)
            {
                if (kvp.Key.Equals(model))
                {
                    Path = kvp.Value;
                    //thread = new Thread(writeLog);
                    //thread.Start();
                    AsynFileHelp asynFileHelp = new AsynFileHelp(Path);
                    asynFileHelp.WriteLine(sb.ToString());
                }
            }
        }

    }
}
