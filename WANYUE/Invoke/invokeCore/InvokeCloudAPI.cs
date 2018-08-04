using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS.WebApi.Client;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke;
using Kingdee.K3.WANYUE.PlugIn.service.log;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Kingdee.K3.WANYUE.PlugIn.service.tools;
using System.Reflection;
using Kingdee.K3.TestConRunJob.PlugIn.service;

namespace Kingdee.K3.WANYUE.PlugIn.service
{
    public class InvokeCloudAPI
    {
        public static LoginResult Login(string functionName) {
            K3CloudApiClient client = new K3CloudApiClient(InvokeLoginInfo.URL);
            string ret = client.ValidateLogin(InvokeLoginInfo.DBID,InvokeLoginInfo.USERNAME,InvokeLoginInfo.PASSWORD,InvokeLoginInfo.LANGUAGE);
            var result = JObject.Parse(ret)["LoginResultType"].Value<int>();
            LoginResult loginResult = null;
            if (result != 1)
            {
                 loginResult = JsonExtension.FromJSON<LoginResult>(ret);
                BussnessLog.WriteBussnessLog("当前登录模块=" + functionName + "，登录结果:" + BussnessResult.fail + "，错误代码=" + loginResult.MessageCode + "，错误原因=" + loginResult.Message);
            }
            else {
                BussnessLog.WriteBussnessLog("当前登录模块=" + functionName + "，登录结果:" + BussnessResult.sucessful);
                loginResult = new LoginResult();
                loginResult.client = client;
                loginResult.LoginResultType = 1;
            }
            return loginResult;
        }



        //其实我不想加注释的, 这段写的实在是估计以后我自己也看不懂,所以还是加上吧
        //通过传入的参数 function 反射获取类 K3CloudApiClient的方法,然后直接调用 
        //其中new string[] { JsonExtension.ToJSON(t) } 是调用方法的参数 参见 方法 public object Invoke(object obj, object[] parameters);
        // JsonExtension.ToJSON(t) 是把传入的对象转化为Json字符串 
        //然后 调用后 进行强制类型转化为string 
        //最后调用Json字符串转对象来实现调用
        public static InvokeResult InvokeFunction(string inputObjectString, K3CloudApiClient client, string formID, string function,string functionName)
        {
            InvokeResult invokeResult = new InvokeResult();
            try {
                Assembly asmb = Assembly.LoadFrom("C:\\Program Files (x86)\\Kingdee\\K3Cloud\\WebSite\\Bin\\Kingdee.BOS.WebApi.Client.dll");
                Type type = asmb.GetType("Kingdee.BOS.WebApi.Client.K3CloudApiClient");
                MethodInfo method = type.GetMethod(function, new Type[] { typeof(string), typeof(string) });
                object[] parameters = new object[] { formID, inputObjectString };
                return JsonExtension.FromJSON<InvokeResult>((string)method.Invoke(client, parameters));
            }
            catch (Exception e) {
                SystemLog.WriteSystemLog("☆☆☆☆☆☆☆☆☆ -- 系统级别错误! --☆☆☆☆☆☆☆☆☆");
                SystemLog.WriteSystemLog("☆☆☆☆☆☆☆☆☆ -- 调用API核心出错! -- ☆☆☆☆☆☆☆☆☆");
                SystemLog.WriteSystemLog("错误模块="+ functionName+"错误操作="+ function+"错误原因="+e);
                SystemLog.WriteSystemLog("☆☆☆☆☆☆☆☆☆ --打印核心错误结束 -- ☆☆☆☆☆☆☆☆☆");
                client.Logout();
                return null;
            }
            
            
        }

        //   object[] args =new object[1];
        //    char[] lpCh = InvokeLoginInfo.URL.ToCharArray();
        ///  args[0] = InvokeLoginInfo.URL;
        // object obj  = System.Activator.CreateInstance(type,args);
        //object obj = type.Assembly.CreateInstance(type);
        //string[] Jsons = new string[] { JsonExtension.ToJSON(t) };
        //=(string)method.Invoke(formID, new string[] { JsonExtension.ToJSON(t) });
        ////System.Reflection.PropertyInfo[] ps = GetPropertyInfo.getPropertyInfo(client);
        ////for (int i = 0; i < ps.Length; i++) {
        ////    if (ps[i].Name == function) {
        ////        client.
        ////    }
        ////}

        //public static InvokeResult InvokeAuditFunction<T>(T t, K3CloudApiClient client, string formID) {
        //    return JsonExtension.FromJSON<InvokeResult>(client.Audit(formID, JsonExtension.ToJSON(t))); 
        //}

        //public static InvokeResult InvokeBatchSaveFunction<T>(T t, K3CloudApiClient client, string formID)
        //{
        //    return JsonExtension.FromJSON<InvokeResult>(client.BatchSave(formID, JsonExtension.ToJSON(t)));
        //}

        //public static InvokeResult InvokeDeleteFunction<T>(T t, K3CloudApiClient client, string formID)
        //{
        //    return JsonExtension.FromJSON<InvokeResult>(client.Delete(formID, JsonExtension.ToJSON(t)));
        //}

        //public static InvokeResult InvokeStatusConvertFunction<T>(T t, K3CloudApiClient client, string formID)
        //{
        //    return JsonExtension.FromJSON<InvokeResult>(client.StatusConvert(formID, JsonExtension.ToJSON(t)));
        //}

        //public static InvokeResult InvokeUnAuditFunction<T>(T t, K3CloudApiClient client, string formID)
        //{
        //    return JsonExtension.FromJSON<InvokeResult>(client.UnAudit(formID, JsonExtension.ToJSON(t)));
        //}

        //public static InvokeResult InvokeAllocateFunction<T>(T t, K3CloudApiClient client, string formID)
        //{
        //    return JsonExtension.FromJSON<InvokeResult>(client.Allocate(formID, JsonExtension.ToJSON(t)));
        //}


    }
}
