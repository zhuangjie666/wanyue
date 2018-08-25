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
using System.Data;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using System.Xml;
using System.IO;
using Kingdee.K3.WANYUE.PlugIn.service.initconfig;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier;
using Kingdee.K3.WANYUE.PlugIn.service.application.voucher;
using Kingdee.K3.WANYUE.PlugIn.service.application;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public abstract class SchTask : IScheduleService, SchInterface
    {
        public RemoteExcuteDataBase remoteExcuteDataBase;
        public static string folderFullName = "C:\\MiddleDataBase";
        public static string Save = "Save";
        public static string Submit = "Submit";
        public static string Audit = "Audit";
        
        public abstract void Run(Context ctx, Schedule schedule);

        public abstract List<T> handleData<T>(DataSet dataSet);

        public SchTask()
        {
            init();
        }

        private void init()
        {

            
            //1.读取配置文件
             readDataBaseConfig();
            //2.初始化 日志系统
            BussnessLog.createLogFileWithFunction(BaseInfo.ModelList);
        }

        private void readDataBaseConfig()
        {
            XmlDocument xmlDoc = new XmlDocument();
            //加载路径（方法1） 后面直接跟路径
            foreach (FileInfo configfile in new DirectoryInfo(folderFullName).GetFiles()) {
                xmlDoc.Load(folderFullName+"\\"+configfile.Name);
            }
            
            //方法二 将XML转化为 一个string 加载
            //xmlDoc.LoadXml (File.ReadAllText("AssetsMyInfo"));
            //获取第一个节点
            XmlNode root = xmlDoc.FirstChild;
            //获取当前节点下的所有子节点
            XmlNodeList list = root.ChildNodes;

            XmlNode listNode;

            for (int i = 0; i < list.Count; i++)
            {
                if ("IP".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    DataBaseInfo.IP = Convert.ToString(listNode.InnerText);
                }
                else if ("DATABASENAME".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    DataBaseInfo.database = listNode.InnerText;
                }
                else if ("USERID".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    DataBaseInfo.userid = listNode.InnerText;
                }
                else if ("PASSWORD".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    DataBaseInfo.password = listNode.InnerText;
                }
                else if ("MODELLIST".Equals(list[i].Name)) {
                    listNode = list.Item(i);
                    BaseInfo.ModelList = listNode.InnerText;
                } else if ("LOGINURL".Equals(list[i].Name)) {
                    listNode = list.Item(i);
                    InvokeLoginInfo.URL = listNode.InnerText;
                }
                else if ("LOGINDBID".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    InvokeLoginInfo.DBID = listNode.InnerText;
                }
                else if ("LOGINUSERNAME".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    InvokeLoginInfo.USERNAME = listNode.InnerText;
                }
                else if ("LOGINPASSWORD".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    InvokeLoginInfo.PASSWORD = listNode.InnerText;
                }
                else if ("LOGINLANGUAGE".Equals(list[i].Name))
                {
                    listNode = list.Item(i);
                    InvokeLoginInfo.LANGUAGE = Convert.ToInt32(listNode.InnerText);
                }
            }
        }

        public string GetStatement(string sourceObject) {
            return new SQLStatement(sourceObject).returnSQLStatement(sourceObject);
        }

        public string[] GetOpearteList(string model) {
            if (CustomerSQLObject.Customer.Equals(model) || SupplierSQLObject.Supplier.Equals(model))
            {
                return new string[] { Save, Submit, Audit };
            }
            else if(VouchSQLObject.Vouch.Equals(model) || PayableSQLObject.Payable.Equals(model))
            {
                return new string[] { Save };
            }

            return null;
        }

        public ConnectionResult connectionToRemoteDatabase()
        {
             remoteExcuteDataBase = new RemoteExcuteDataBase(DataBaseInfo.IP, DataBaseInfo.database, DataBaseInfo.userid, DataBaseInfo.password);
            if (!remoteExcuteDataBase.connectionToRemoteDatabase().Sucessd)
            {
                BussnessLog.WriteBussnessLog(remoteExcuteDataBase, "DATABASE","");
                remoteExcuteDataBase.Result = false;
            }
            remoteExcuteDataBase.Result = true;
            ConnectionResult result = remoteExcuteDataBase.connectionToRemoteDatabase();
            return result;
        }

        public bool closeConnetction()
        {
            if (!remoteExcuteDataBase.closeConnection(remoteExcuteDataBase.connectionToRemoteDatabase().Sqlconn))
            {
                BussnessLog.WriteBussnessLog("", "数据库操作错误","关闭数据库错误！");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool LoginAPI(string model)
        {
            Invoke.LoginResult loginResult = InvokeCloudAPI.Login(model);
            if (loginResult.LoginResultType != 1)
            {
                return false;
            }

            return true;
        }


        public InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx)
        {
            InvokeReturnHandle<T> invokeReturnHandle = new InvokeReturnHandle<T>();

            if (invokeResult == null)
            {
                BussnessLog.WriteBussnessLog(default(T), model, "调用模块=" + model + ",调用方法=" + opearte + ",返回为NULL");
            }
            if (model.Equals("voucher"))
            {
                if (!invokeResult.Result.ResponseStatus.IsSuccess)
                {
                    BussnessLog.WriteBussnessLog(invokeResult, model, "");
                    invokeReturnHandle.ReturnResult = false;
                }
                else {
                    invokeReturnHandle.ReturnResult = true;
                }
                invokeReturnHandle.CustomOpearteObject = (T)(object)invokeResult;
                return invokeReturnHandle;
            }

            if (!invokeResult.Result.ResponseStatus.IsSuccess) {
                invokeReturnHandle.ReturnResult = false;
                return invokeReturnHandle;
            }

            List<SuccessEntitysItem> entries = invokeResult.Result.ResponseStatus.SuccessEntitys;

            InfoexceptAllocate infoexceptAllocateObject = new InfoexceptAllocate();
            List<string> numbers = new List<string>();
            List<string> pkids = new List<string>();
            foreach (SuccessEntitysItem entry in entries)
            {
                numbers.Add(entry.Number.ToString());
                pkids.Add(entry.Id.ToString());
            }
            infoexceptAllocateObject.Numbers = numbers.ToArray();
            invokeReturnHandle.CustomOpearteObject = (T)(object)infoexceptAllocateObject;
            invokeReturnHandle.ReturnResult = true;
            return invokeReturnHandle;
            throw new NotImplementedException();
        }

        public InvokeReturnHandle<InvokeResult> InvokeAPI<T>(string[] opearteList, T t, Invoke.LoginResult loginResult, Context ctx,string formID,string model)
        {
            InvokeResult invokeResult = null;
            string input = null;
            string outopt = "";
            foreach (string opearte in opearteList)
            {
                BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte);
                if (Save.Equals(opearte))
                {
                    BussnessLog.WriteBussnessLog(default(T), model, "input json=" + JsonExtension.ToJSON(t));
                    invokeResult = InvokeCloudAPI.InvokeFunction(JsonExtension.ToJSON(t), loginResult.client, formID, Save, model);
                }
                else
                {
                    input = JsonExtension.ToJSON(handleReturnMessage<InfoexceptAllocate>(invokeResult, opearte, model, ctx).CustomOpearteObject);
                    BussnessLog.WriteBussnessLog(default(T), model, "input json=" + input);
                    invokeResult = InvokeCloudAPI.InvokeFunction(input, loginResult.client, formID, opearte, model);
                }
                if (model.Equals("voucher")||model.Equals("payable")) {
                    InvokeReturnHandle <InvokeResult> result = handleReturnMessage<InvokeResult>(invokeResult, opearte, model, ctx);
                    return result;
                }

                if (!handleReturnMessage<InfoexceptAllocate>(invokeResult, opearte, model, ctx).ReturnResult)
                {
                    outopt = opearte;
                    BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte + "失败!");
                    InvokeReturnHandle<InvokeResult> aresult = new InvokeReturnHandle<InvokeResult>();
                    aresult.ReturnResult = false;
                    aresult.CustomOpearteObject = invokeResult;
                    return aresult;
                }
                else {
                    BussnessLog.WriteBussnessLog(default(T), model, "操作=" + opearte + "成功!");
                }
            }
            InvokeReturnHandle<InvokeResult> a = new InvokeReturnHandle<InvokeResult>();
            a.ReturnResult = true;
            a.CustomOpearteObject = invokeResult;
            return a;

            throw new NotImplementedException();
        }

        public bool updateMiddleDataBase(string updateSQLStatement,string model,string tableName)
        {

            ExcuteDataBaseResult excuteDataBaseResult = remoteExcuteDataBase.excuteStatement(updateSQLStatement, connectionToRemoteDatabase().Sqlconn);
            if (!excuteDataBaseResult.Sucessd) {
                BussnessLog.WriteBussnessLog("","更新中间数据库","更新中间数据库,表="+ tableName+"错误, 操作模块="+model);
                return false;
            }
            closeConnetction();
            return true;
        }
        //public abstract InvokeReturnHandle<T> handleReturnMessage<T>(InvokeResult invokeResult, string opearte, string model, Context ctx);
        //   public abstract bool InvokeAPI<T>(string[] opearteList, T t, Invoke.LoginResult loginResult, Context ctx);

    }
}
