using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.schTask
{
   public class  InvokeReturnHandle<T>
    {
        private bool returnResult;
        private T customOpearteObject;
       
        public bool ReturnResult
        {
            get
            {
                return returnResult;
            }

            set
            {
                returnResult = value;
            }
        }

        public T CustomOpearteObject
        {
            get
            {
                return customOpearteObject;
            }

            set
            {
                customOpearteObject = value;
            }
        }

       
    }
}
