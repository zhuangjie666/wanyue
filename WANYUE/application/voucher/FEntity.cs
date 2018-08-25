using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.voucher
{
    public class FEntity
    {
        private int fEntryID;
        private string fEXPLANATION;
        private FAccountID fACCOUNTID; //科目编码
        private FDetailID fDetailID;
        private FExchangeRateType fEXCHANGERATETYPE;
        private int fEXCHANGERATE;
        private FCurrencyID fCURRENCYID;
        private decimal fAmount;
        private decimal fAMOUNTFOR;
        private decimal fDEBIT;
        private decimal fCREDIT;

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

        public FAccountID FACCOUNTID
        {
            get
            {
                return fACCOUNTID;
            }

            set
            {
                fACCOUNTID = value;
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

        public FExchangeRateType FEXCHANGERATETYPE
        {
            get
            {
                return fEXCHANGERATETYPE;
            }

            set
            {
                fEXCHANGERATETYPE = value;
            }
        }

        public int FEXCHANGERATE
        {
            get
            {
                return fEXCHANGERATE;
            }

            set
            {
                fEXCHANGERATE = value;
            }
        }

        public FCurrencyID FCURRENCYID
        {
            get
            {
                return fCURRENCYID;
            }

            set
            {
                fCURRENCYID = value;
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
    }
}
