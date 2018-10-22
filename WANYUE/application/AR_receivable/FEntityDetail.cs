using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AR_receivable
{
    public class FEntityDetail
    {
        private FMATERIALID fMATERIALID;
        private FPRICEUNITID fPRICEUNITID;
        private decimal fPriceQty;
        private decimal fPrice;
        private decimal fTaxPrice;
        private decimal fEntryTaxRate;
        private decimal fEntryDiscountRate;
        private decimal fDISCOUNTAMOUNTFOR;
        private decimal fALLAMOUNTFOR_D;
        private decimal fNoTaxAmountFor_D;
        private decimal fTaxAmountFor_D;
        private FSalUnitId fSalUnitId;
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

        public decimal FEntryTaxRate
        {
            get
            {
                return fEntryTaxRate;
            }

            set
            {
                fEntryTaxRate = value;
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

        public decimal FDISCOUNTAMOUNTFOR
        {
            get
            {
                return fDISCOUNTAMOUNTFOR;
            }

            set
            {
                fDISCOUNTAMOUNTFOR = value;
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

        public decimal FTaxAmountFor_D
        {
            get
            {
                return fTaxAmountFor_D;
            }

            set
            {
                fTaxAmountFor_D = value;
            }
        }

        public FSalUnitId FSalUnitId
        {
            get
            {
                return fSalUnitId;
            }

            set
            {
                fSalUnitId = value;
            }
        }
    }
}
