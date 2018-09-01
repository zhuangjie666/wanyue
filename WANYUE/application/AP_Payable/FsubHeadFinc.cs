using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable
{
   public class FsubHeadFinc
    {
        private decimal fNoTaxAmountFor;
        private decimal fTaxAmountFor;
        private DateTime faccntTimeJudgeTime;

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
    }
}
