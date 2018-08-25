using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable
{
   public class Model
    {
        /// <summary>
        /// 必填字段
        /// </summary>
        private FBillTypeID fBillTypeID;  //单据类型
        private string fBillNo; //单号
        private FSUPPLIERID fSUPPLIERID;//供应商
        private string fCancelStatus; //作废状态
        private string fBUSINESSTYPE;//业务类型
        private string fDOCUMENTSTATUS; //单据状态
        private DateTime fDATE;//业务日期
        private DateTime fENDDATE_H;//到期日
        private FCURRENCYID fCURRENCYID;//币别
        private FSETTLEORGID fSETTLEORGID;///结算组织
        private FPAYORGID fPAYORGID; //付款组织
        private FPURCHASEORGID fPURCHASEORGID; //采购组织
        /// <summary>
        /// 非必填字段
        /// </summary>
        private FsubHeadSuppiler fsubHeadSuppiler; //头部供应商信息
        private FsubHeadFinc fsubHeadFinc;
        private FEntityDetail fEntityDetail;

        public FBillTypeID FBillTypeID
        {
            get
            {
                return fBillTypeID;
            }

            set
            {
                fBillTypeID = value;
            }
        }

        public FSUPPLIERID FSUPPLIERID
        {
            get
            {
                return fSUPPLIERID;
            }

            set
            {
                fSUPPLIERID = value;
            }
        }

        public string FCancelStatus
        {
            get
            {
                return fCancelStatus;
            }

            set
            {
                fCancelStatus = value;
            }
        }

        public string FBUSINESSTYPE
        {
            get
            {
                return fBUSINESSTYPE;
            }

            set
            {
                fBUSINESSTYPE = value;
            }
        }

        public string FDOCUMENTSTATUS
        {
            get
            {
                return fDOCUMENTSTATUS;
            }

            set
            {
                fDOCUMENTSTATUS = value;
            }
        }

        public DateTime FDATE
        {
            get
            {
                return fDATE;
            }

            set
            {
                fDATE = value;
            }
        }

        public DateTime FENDDATE_H
        {
            get
            {
                return fENDDATE_H;
            }

            set
            {
                fENDDATE_H = value;
            }
        }

        public FCURRENCYID FCURRENCYID
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

        public FSETTLEORGID FSETTLEORGID
        {
            get
            {
                return fSETTLEORGID;
            }

            set
            {
                fSETTLEORGID = value;
            }
        }

        public FPAYORGID FPAYORGID
        {
            get
            {
                return fPAYORGID;
            }

            set
            {
                fPAYORGID = value;
            }
        }

        public FPURCHASEORGID FPURCHASEORGID
        {
            get
            {
                return fPURCHASEORGID;
            }

            set
            {
                fPURCHASEORGID = value;
            }
        }

        public FsubHeadSuppiler FsubHeadSuppiler
        {
            get
            {
                return fsubHeadSuppiler;
            }

            set
            {
                fsubHeadSuppiler = value;
            }
        }

        public FsubHeadFinc FsubHeadFinc
        {
            get
            {
                return fsubHeadFinc;
            }

            set
            {
                fsubHeadFinc = value;
            }
        }

        public FEntityDetail FEntityDetail
        {
            get
            {
                return fEntityDetail;
            }

            set
            {
                fEntityDetail = value;
            }
        }

        public string FBillNo
        {
            get
            {
                return fBillNo;
            }

            set
            {
                fBillNo = value;
            }
        }
    }
}
