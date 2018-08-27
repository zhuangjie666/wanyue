using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.modify
{
    public class SupplierUpdateFields:UpdateFields
    {
        private string fNumber;
        private string fName;
        private string fopenBankName;
        private string fbankCode;

        public string FNumber
        {
            get
            {
                return fNumber;
            }

            set
            {
                fNumber = value;
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

        public string FopenBankName
        {
            get
            {
                return fopenBankName;
            }

            set
            {
                fopenBankName = value;
            }
        }

        public string FbankCode
        {
            get
            {
                return fbankCode;
            }

            set
            {
                fbankCode = value;
            }
        }
    }
}
