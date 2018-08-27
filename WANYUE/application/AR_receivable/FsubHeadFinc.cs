using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AR_receivable
{
    public class FsubHeadFinc
    {
        private DateTime fACCNTTIMEJUDGETIME;
        private decimal fNoTaxAmountFor;
        private decimal fTaxAmountFor;

        public DateTime FACCNTTIMEJUDGETIME
        {
            get
            {
                return fACCNTTIMEJUDGETIME;
            }

            set
            {
                fACCNTTIMEJUDGETIME = value;
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
    }
}
