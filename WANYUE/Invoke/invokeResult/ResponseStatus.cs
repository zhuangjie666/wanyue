using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.Invoke.invokeResult
{
    public class ResponseStatus
    {
        /// <summary>
        /// 
        /// </summary>
        private string errorCode;
        /// <summary>
        /// 
        /// </summary>
        private bool isSuccess;
        /// <summary>
        /// 
        /// </summary>
        private List<ErrorsItem> errors;
        /// <summary>
        /// 
        /// </summary>
        private List<SuccessEntitysItem> successEntitys;
        /// <summary>
        /// 
        /// </summary>
        private List<SuccessMessagesItem> successMessages;

        public string ErrorCode
        {
            get
            {
                return errorCode;
            }

            set
            {
                errorCode = value;
            }
        }

        public bool IsSuccess
        {
            get
            {
                return isSuccess;
            }

            set
            {
                isSuccess = value;
            }
        }

        public List<ErrorsItem> Errors
        {
            get
            {
                return errors;
            }

            set
            {
                errors = value;
            }
        }

        public List<SuccessEntitysItem> SuccessEntitys
        {
            get
            {
                return successEntitys;
            }

            set
            {
                successEntitys = value;
            }
        }

        public List<SuccessMessagesItem> SuccessMessages
        {
            get
            {
                return successMessages;
            }

            set
            {
                successMessages = value;
            }
        }
    }
}
