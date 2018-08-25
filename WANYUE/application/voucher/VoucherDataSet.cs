using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.voucher
{
   public class VoucherDataSet
    {
        private int fid;
        private string fnumber;
        private string fAccountBookNumber;
        private DateTime fDate;
        private string fSystemNumber;
        private string fVoucherGroupNumber;
        private string fAcctorgNumber;
        private int fEntryID;
        private string fEXPLANATION;
        private string fAccountID;
        private decimal fDEBIT;
        private decimal fCREDIT;
        private string fDetailFflex;
        private int fstatus;
        private string fExchangeRateType;
        private string fCurrencyID;
        private string fAuditStatus;
        private string fAccbookOrgID;

        public int Fid
        {
            get
            {
                return fid;
            }

            set
            {
                fid = value;
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

        public string FAccountBookNumber
        {
            get
            {
                return fAccountBookNumber;
            }

            set
            {
                fAccountBookNumber = value;
            }
        }

        public DateTime FDate
        {
            get
            {
                return fDate;
            }

            set
            {
                fDate = value;
            }
        }

        public string FSystemNumber
        {
            get
            {
                return fSystemNumber;
            }

            set
            {
                fSystemNumber = value;
            }
        }

        public string FVoucherGroupNumber
        {
            get
            {
                return fVoucherGroupNumber;
            }

            set
            {
                fVoucherGroupNumber = value;
            }
        }

        public string FAcctorgNumber
        {
            get
            {
                return fAcctorgNumber;
            }

            set
            {
                fAcctorgNumber = value;
            }
        }

        public int FEntryID
        {
            get
            {
                return fEntryID;
            }

            set
            {
                fEntryID = value;
            }
        }

        public string FEXPLANATION
        {
            get
            {
                return fEXPLANATION;
            }

            set
            {
                fEXPLANATION = value;
            }
        }



        public decimal FDEBIT
        {
            get
            {
                return fDEBIT;
            }

            set
            {
                fDEBIT = value;
            }
        }

        public decimal FCREDIT
        {
            get
            {
                return fCREDIT;
            }

            set
            {
                fCREDIT = value;
            }
        }

        public string FDetailFflex
        {
            get
            {
                return fDetailFflex;
            }

            set
            {
                fDetailFflex = value;
            }
        }

        public int Fstatus
        {
            get
            {
                return fstatus;
            }

            set
            {
                fstatus = value;
            }
        }

        public string FExchangeRateType
        {
            get
            {
                return fExchangeRateType;
            }

            set
            {
                fExchangeRateType = value;
            }
        }

        public string FCurrencyID
        {
            get
            {
                return fCurrencyID;
            }

            set
            {
                fCurrencyID = value;
            }
        }

        public string FAuditStatus
        {
            get
            {
                return fAuditStatus;
            }

            set
            {
                fAuditStatus = value;
            }
        }

        public string FAccountID
        {
            get
            {
                return fAccountID;
            }

            set
            {
                fAccountID = value;
            }
        }

        public string FAccbookOrgID
        {
            get
            {
                return fAccbookOrgID;
            }

            set
            {
                fAccbookOrgID = value;
            }
        }
    }
}
