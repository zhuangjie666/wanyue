using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult
{
   public class Result
    {
        private ResponseStatus responseStatus;
        private string id;
        private string number;
        private List<NeedReturnData> needReturnData;

        public ResponseStatus ResponseStatus
        {
            get
            {
                return responseStatus;
            }

            set
            {
                responseStatus = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        internal List<NeedReturnData> NeedReturnData
        {
            get
            {
                return needReturnData;
            }

            set
            {
                needReturnData = value;
            }
        }

        public string Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }
    }
}
