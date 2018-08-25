using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable
{
    public class FsubHeadSuppiler
    {
        private string FEntryId;
        private FORDERID fORDERID;
        private FTRANSFERID fTRANSFERID;
        private FChargeId fChargeId;

        public string FEntryId1
        {
            get
            {
                return FEntryId;
            }

            set
            {
                FEntryId = value;
            }
        }

        public FORDERID FORDERID
        {
            get
            {
                return fORDERID;
            }

            set
            {
                fORDERID = value;
            }
        }

        public FTRANSFERID FTRANSFERID
        {
            get
            {
                return fTRANSFERID;
            }

            set
            {
                fTRANSFERID = value;
            }
        }

        public FChargeId FChargeId
        {
            get
            {
                return fChargeId;
            }

            set
            {
                fChargeId = value;
            }
        }
    }
}
