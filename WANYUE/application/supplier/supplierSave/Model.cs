using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.voucher
{
   public class Model
    {
        private string fVOUCHERID;
        private string fVOUCHERGROUPNO; //凭证号
        private string fISADJUSTVOUCHER;
        private FAccountBookNumber fAccountBookID; //账簿
        private string fDate; //日期
        private FSystemID fSystemID;//系统字段GL
        private FVoucherGroupID fVoucherGroupID; //凭证字
        private string fDocumentStatus; //审核状态 
        private FAccbookOrgID fAccbookOrgID; // 核算组织
        private List<FEntity> fEntity;

        public string FVOUCHERID
        {
            get
            {
                return fVOUCHERID;
            }

            set
            {
                fVOUCHERID = value;
            }
        }


        public string FVOUCHERGROUPNO
        {
            get
            {
                return fVOUCHERGROUPNO;
            }

            set
            {
                fVOUCHERGROUPNO = value;
            }
        }

        public string FISADJUSTVOUCHER
        {
            get
            {
                return fISADJUSTVOUCHER;
            }

            set
            {
                fISADJUSTVOUCHER = value;
            }
        }

        public FAccountBookNumber FAccountBookID
        {
            get
            {
                return fAccountBookID;
            }

            set
            {
                fAccountBookID = value;
            }
        }

        public string FDate
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


        public FVoucherGroupID FVoucherGroupID
        {
            get
            {
                return fVoucherGroupID;
            }

            set
            {
                fVoucherGroupID = value;
            }
        }

        public string FDocumentStatus
        {
            get
            {
                return fDocumentStatus;
            }

            set
            {
                fDocumentStatus = value;
            }
        }

        public FAccbookOrgID FAccbookOrgID
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

        public List<FEntity> FEntity
        {
            get
            {
                return fEntity;
            }

            set
            {
                fEntity = value;
            }
        }

        public FSystemID FSystemID
        {
            get
            {
                return fSystemID;
            }

            set
            {
                fSystemID = value;
            }
        }
    }
}
