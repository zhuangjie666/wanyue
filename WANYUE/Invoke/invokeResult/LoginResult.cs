using Kingdee.BOS.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.Invoke
{
    
   public   class LoginResult
    {
        public K3CloudApiClient client { get; set; }
        /// <summary>
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MessageCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LoginResultType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Context { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KDSVCSessionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FormId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RedirectFormParam { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FormInputObject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrorStackTrace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Lcid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KdAccessResult { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IsSuccessByAPI { get; set; }
    }
}
