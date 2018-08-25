using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierSave
{
   public class Model
    {
        private FCreateOrgId fCreateOrgId;
        private FUseOrgId fUseOrgId;
        private string fName;
        private FFinanceInfo fFinanceInfo;
        private FLocationInfo[] fLocationInfo;
        private FBankInfo[] fBankInfo;
        private string fnumber;


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

    

        public FCreateOrgId FCreateOrgId
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

        public FUseOrgId FUseOrgId
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

        public FLocationInfo[] FLocationInfo
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

        public string Fnumber
        {
            get
            {
                return fnumber;
            }

            set
            {
                fnumber = value;
            }
        }

        public FBankInfo[] FBankInfo
        {
            get
            {
                return fBankInfo;
            }

            set
            {
                fBankInfo = value;
            }
        }
    }
}
