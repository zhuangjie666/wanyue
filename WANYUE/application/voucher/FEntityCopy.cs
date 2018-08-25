using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.voucher
{
    public class FEntityCopy
    {
        private string fExplanation;
        //private int fEntryID;
        private FAccountID fAccountID; //科目编码
        private FDetailID fDetailID;
        private FExchangeRateType fExchangeRateType;
        private int fExchangeRate;
        private FCurrencyID fCurrencyID;
        private decimal fAmount;
        private decimal fAMOUNTFOR;
        private decimal fDebit;
        private decimal fCredit;

        public string FExplanation
        {
            get
            {
                return fExplanation;
            }

            set
            {
                fExplanation = value;
            }
        }

        public FAccountID FAccountID
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

        public FDetailID FDetailID
        {
            get
            {
                return fDetailID;
            }

            set
            {
                fDetailID = value;
            }
        }

        public FExchangeRateType FExchangeRateType
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

        public int FExchangeRate
        {
            get
            {
                return fExchangeRate;
            }

            set
            {
                fExchangeRate = value;
            }
        }

        public FCurrencyID FCurrencyID
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

        public decimal FAmount
        {
            get
            {
                return fAmount;
            }

            set
            {
                fAmount = value;
            }
        }

        public decimal FAMOUNTFOR
        {
            get
            {
                return fAMOUNTFOR;
            }

            set
            {
                fAMOUNTFOR = value;
            }
        }

        public decimal FDebit
        {
            get
            {
                return fDebit;
            }

            set
            {
                fDebit = value;
            }
        }

        public decimal FCredit
        {
            get
            {
                return fCredit;
            }

            set
            {
                fCredit = value;
            }
        }
    }
}
