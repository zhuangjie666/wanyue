using Kingdee.BOS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingdee.BOS;
using Kingdee.BOS.Core;
using Kingdee.K3.WANYUE.PlugIn.service.application.supplier;
using Kingdee.K3.WANYUE.PlugIn.service.application.customer.customSave;
using Kingdee.K3.WANYUE.PlugIn.service.Invoke;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
    [Kingdee.BOS.Util.HotUpdate]
    public class SchTask4Supplier : SchTask
    {
        public string supplierSaveModel = "供应商保存模块";
        public string Save = "Save";
        public string supplierSubmitModel = "供应商提交模块";
        public string Submit = "Submit";
        public string supplierAuditModel = "供应商审核模块";
        public string Audit = "Audit";
        public string supplierAllocateModel = "供应商分配模块";
        public string Allocate = "Allocate";
        public string formID = "";

 

        public override void Run(Context ctx, Schedule schedule)
        {
            throw new NotImplementedException();
        }


        public override bool InvokeAPI<T>(string[] opearteList, T t, LoginResult loginResult, Context ctx)
        {
            throw new NotImplementedException();
        }
    }
}
