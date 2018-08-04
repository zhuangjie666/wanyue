using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.supplier
{
   public class Model
    {
        private string fCreateOrgId;
        private string fUseOrgId;
        private string fName;
        private FFinanceInfo fFinanceInfo;
        private FLocationInfo fLocationInfo;

        public string FCreateOrgId
        {
            get
            {
                return fCreateOrgId;
            }

            set
            {
                fCreateOrgId = value;
            }
        }

        public string FUseOrgId
        {
            get
            {
                return fUseOrgId;
            }

            set
            {
                fUseOrgId = value;
            }
        }

        public string FName
        {
            get
            {
                return fName;
            }

            set
            {
                fName = value;
            }
        }

        public FFinanceInfo FFinanceInfo
        {
            get
            {
                return fFinanceInfo;
            }

            set
            {
                fFinanceInfo = value;
            }
        }

        public FLocationInfo FLocationInfo
        {
            get
            {
                return fLocationInfo;
            }

            set
            {
                fLocationInfo = value;
            }
        }
    }
}
