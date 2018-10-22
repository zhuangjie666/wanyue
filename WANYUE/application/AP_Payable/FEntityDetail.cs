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
        private decimal fPriceQty;
        private decimal fPrice;
        private decimal fTaxPrice;
        private decimal fentryTaxRate;
        private decimal fEntryDiscountRate;
        private decimal fdiscountAmountFor;
        private decimal fNoTaxAmountFor_D;
        private decimal fTAXAMOUNTFOR_D;
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

        public decimal FPriceQty
        {
            get
            {
                return fPriceQty;
            }

            set
            {
                fPriceQty = value;
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

        public decimal FNoTaxAmountFor_D
        {
            get
            {
                return fNoTaxAmountFor_D;
            }

            set
            {
                fNoTaxAmountFor_D = value;
            }
        }

        public decimal FTAXAMOUNTFOR_D
        {
            get
            {
                return fTAXAMOUNTFOR_D;
            }

            set
            {
                fTAXAMOUNTFOR_D = value;
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
