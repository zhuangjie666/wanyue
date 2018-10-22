using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_OtherPayable
{
   public class OtherPayableDataSet
    {
        private string fNumber;
        private string fBillType;
        private DateTime fdate;
        private DateTime fendDate;
        private DateTime faccntTimeJudgeTime;
        private string fContactUnitType;
        private string fContactUnit;
        private string fsettleOrg;
        private string fpayOrg;
        private string fCurrency;
        private string fCost;
        private string fCostDepartment;
        private string fInvoiceType;
        private decimal fEntryTaxRate;
        private decimal fNoTaxAmountFor;
        private decimal fTaxAmountFor;
        private decimal fAmountFor;
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

        public string FBillType
        {
            get
            {
                return fBillType;
            }

            set
            {
                fBillType = value;
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

        public string FContactUnitType
        {
            get
            {
                return fContactUnitType;
            }

            set
            {
                fContactUnitType = value;
            }
        }

        public string FContactUnit
        {
            get
            {
                return fContactUnit;
            }

            set
            {
                fContactUnit = value;
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

        public string FCurrency
        {
            get
            {
                return fCurrency;
            }

            set
            {
                fCurrency = value;
            }
        }

        public string FCost
        {
            get
            {
                return fCost;
            }

            set
            {
                fCost = value;
            }
        }

        public string FCostDepartment
        {
            get
            {
                return fCostDepartment;
            }

            set
            {
                fCostDepartment = value;
            }
        }

        public string FInvoiceType
        {
            get
            {
                return fInvoiceType;
            }

            set
            {
                fInvoiceType = value;
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

        public decimal FAmountFor
        {
            get
            {
                return fAmountFor;
            }

            set
            {
                fAmountFor = value;
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
