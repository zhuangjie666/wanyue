using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_OtherPayable
{
   public class FEntity
    {
        private FCOSTID fCOSTID;
        private FCOSTDEPARTMENTID fCOSTDEPARTMENTID;
        private string fINVOICETYPE;
        private decimal fEntryTaxRate;
        private decimal fNOTAXAMOUNTFOR;
        private decimal fTAXAMOUNTFOR;
        private decimal fTOTALAMOUNTFOR;
        private string fCOMMENT;

        public FCOSTID FCOSTID
        {
            get
            {
                return fCOSTID;
            }

            set
            {
                fCOSTID = value;
            }
        }

        public FCOSTDEPARTMENTID FCOSTDEPARTMENTID
        {
            get
            {
                return fCOSTDEPARTMENTID;
            }

            set
            {
                fCOSTDEPARTMENTID = value;
            }
        }

        public string FINVOICETYPE
        {
            get
            {
                return fINVOICETYPE;
            }

            set
            {
                fINVOICETYPE = value;
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

        public decimal FNOTAXAMOUNTFOR
        {
            get
            {
                return fNOTAXAMOUNTFOR;
            }

            set
            {
                fNOTAXAMOUNTFOR = value;
            }
        }

        public decimal FTAXAMOUNTFOR
        {
            get
            {
                return fTAXAMOUNTFOR;
            }

            set
            {
                fTAXAMOUNTFOR = value;
            }
        }

        public decimal FTOTALAMOUNTFOR
        {
            get
            {
                return fTOTALAMOUNTFOR;
            }

            set
            {
                fTOTALAMOUNTFOR = value;
            }
        }

        public string FCOMMENT
        {
            get
            {
                return fCOMMENT;
            }

            set
            {
                fCOMMENT = value;
            }
        }
    }
}
