using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable
{
    public class FEntityDetail
    {
        private FMATERIALID fMATERIALID; //物料
        private FPRICEUNITID fPRICEUNITID;
        private decimal fPrice;
        private decimal fTaxPrice;
        private decimal fentryTaxRate;
        private decimal fEntryDiscountRate;
        private decimal fdiscountAmountFor;
        private decimal fALLAMOUNTFOR_D;

        public FMATERIALID FMATERIALID
        {
            get
            {
                return fMATERIALID;
            }

            set
            {
                fMATERIALID = value;
            }
        }

        public FPRICEUNITID FPRICEUNITID
        {
            get
            {
                return fPRICEUNITID;
            }

            set
            {
                fPRICEUNITID = value;
            }
        }

        public decimal FPrice
        {
            get
            {
                return fPrice;
            }

            set
            {
                fPrice = value;
            }
        }

        public decimal FTaxPrice
        {
            get
            {
                return fTaxPrice;
            }

            set
            {
                fTaxPrice = value;
            }
        }

        public decimal FentryTaxRate
        {
            get
            {
                return fentryTaxRate;
            }

            set
            {
                fentryTaxRate = value;
            }
        }

        public decimal FEntryDiscountRate
        {
            get
            {
                return fEntryDiscountRate;
            }

            set
            {
                fEntryDiscountRate = value;
            }
        }

        public decimal FdiscountAmountFor
        {
            get
            {
                return fdiscountAmountFor;
            }

            set
            {
                fdiscountAmountFor = value;
            }
        }

        public decimal FALLAMOUNTFOR_D
        {
            get
            {
                return fALLAMOUNTFOR_D;
            }

            set
            {
                fALLAMOUNTFOR_D = value;
            }
        }
    }
}
