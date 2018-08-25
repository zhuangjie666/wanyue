using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierSave
{
    public class FBankInfo
    {
        private string fOpenBankName;
        private string fBankCode;
        private string fBankHolder;

        public string FOpenBankName
        {
            get
            {
                return fOpenBankName;
            }

            set
            {
                fOpenBankName = value;
            }
        }

        public string FBankCode
        {
            get
            {
                return fBankCode;
            }

            set
            {
                fBankCode = value;
            }
        }

        public string FBankHolder
        {
            get
            {
                return fBankHolder;
            }

            set
            {
                fBankHolder = value;
            }
        }
    }
}
