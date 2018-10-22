using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable
{
   public class PayableDataSet
    {
        private string fNumber;
        private string fBillTypeID;
        private DateTime fdate;
        private DateTime fendDate;
        private DateTime faccntTimeJudgeTime;
        private string fsupplier;
        private string fsettleOrg;
        private string fpayOrg;
        private string fpurchaseOrg;
        private string fcurrency;
        private string fmaterial;
        private string fpriceUnit;
        private decimal fprice;
        private decimal fpriceQty;
        private decimal ftaxPrice;
        private decimal fentryTaxRate;
        private decimal fEntryDiscountRate;
        private decimal fdiscountAmountFor;
        private decimal fNoTaxAmountFor;
        private decimal fTaxAmountFor;
        private decimal fallAmountFor;
        private string fremark;
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

        public string FBillTypeID
        {
            get
            {
                return fBillTypeID;
            }

            set
            {
                fBillTypeID = value;
            }
        }

        public DateTime Fdate
        {
            get
            {
                return fdate;
            }

            set
            {
                fdate = value;
            }
        }

        public DateTime FendDate
        {
            get
            {
                return fendDate;
            }

            set
            {
                fendDate = value;
            }
        }

        public DateTime FaccntTimeJudgeTime
        {
            get
            {
                return faccntTimeJudgeTime;
            }

            set
            {
                faccntTimeJudgeTime = value;
            }
        }

        public string Fsupplier
        {
            get
            {
                return fsupplier;
            }

            set
            {
                fsupplier = value;
            }
        }

        public string FsettleOrg
        {
            get
            {
                return fsettleOrg;
            }

            set
            {
                fsettleOrg = value;
            }
        }

        public string FpayOrg
        {
            get
            {
                return fpayOrg;
            }

            set
            {
                fpayOrg = value;
            }
        }

        public string FpurchaseOrg
        {
            get
            {
                return fpurchaseOrg;
            }

            set
            {
                fpurchaseOrg = value;
            }
        }

        public string Fcurrency
        {
            get
            {
                return fcurrency;
            }

            set
            {
                fcurrency = value;
            }
        }

        public string Fmaterial
        {
            get
            {
                return fmaterial;
            }

            set
            {
                fmaterial = value;
            }
        }

        public string FpriceUnit
        {
            get
            {
                return fpriceUnit;
            }

            set
            {
                fpriceUnit = value;
            }
        }

        public decimal Fprice
        {
            get
            {
                return fprice;
            }

            set
            {
                fprice = value;
            }
        }

        public decimal FpriceQty
        {
            get
            {
                return fpriceQty;
            }

            set
            {
                fpriceQty = value;
            }
        }

        public decimal FtaxPrice
        {
            get
            {
                return ftaxPrice;
            }

            set
            {
                ftaxPrice = value;
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

        public decimal FNoTaxAmountFor
        {
            get
            {
                return fNoTaxAmountFor;
            }

            set
            {
                fNoTaxAmountFor = value;
            }
        }

        public decimal FTaxAmountFor
        {
            get
            {
                return fTaxAmountFor;
            }

            set
            {
                fTaxAmountFor = value;
            }
        }

        public decimal FallAmountFor
        {
            get
            {
                return fallAmountFor;
            }

            set
            {
                fallAmountFor = value;
            }
        }

        public string Fremark
        {
            get
            {
                return fremark;
            }

            set
            {
                fremark = value;
            }
        }
    }
}
