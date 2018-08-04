using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.customer
{
   public class Model
    {
        private string fcustID;
        private FCreateOrgId fCreateOrgId;
        private FGroup fGroup;
        private string fName;
        private FSALGROUPID fsalGroupID;
        private FTRADINGCURRID ftradingCurrID;
        private FUseOrgId fUseOrgId;

        public string FcustID
        {
            get
            {
                return fcustID;
            }

            set
            {
                fcustID = value;
            }
        }

        public FCreateOrgId FCreateOrgId
        {
            get
            {
                return fCreateOrgId;
            }

            set
            {
                fCreateOrgId = value;
            }
        }

        public FGroup FGroup
        {
            get
            {
                return fGroup;
            }

            set
            {
                fGroup = value;
            }
        }

        public string FName
        {
            get
            {
                return fName;
            }

            set
            {
                fName = value;
            }
        }

        public FSALGROUPID FsalGroupID
        {
            get
            {
                return fsalGroupID;
            }

            set
            {
                fsalGroupID = value;
            }
        }

        public FTRADINGCURRID FtradingCurrID
        {
            get
            {
                return ftradingCurrID;
            }

            set
            {
                ftradingCurrID = value;
            }
        }

        public FUseOrgId FUseOrgId
        {
            get
            {
                return fUseOrgId;
            }

            set
            {
                fUseOrgId = value;
            }
        }
    }
}
