using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AR_receivable
{
    public class Model
    {
        private string fnumber;
        private FBillTypeID fBillTypeID;
        private string fBillNo;
        private string fCancelStatus; //作废状态
        private DateTime fDATE;//业务日期
        private DateTime fENDDATE_H;//到期日
        private FCUSTOMERID fCUSTOMERID;
        private string fBUSINESSTYPE;//业务类型
        private string fDOCUMENTSTATUS; //单据状态
        private FMAINBOOKSTDCURRID fMAINBOOKSTDCURRID;
        private FEXCHANGETYPE fEXCHANGETYPE;
        private FCURRENCYID fCURRENCYID;
        private FSETTLEORGID fSETTLEORGID;
        private FPAYORGID fPAYORGID;
        private FSALEORGID fSALEORGID;
        private FsubHeadFinc fsubHeadFinc;
        private List<FEntityDetail> fEntityDetail;

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

        public FCUSTOMERID FCUSTOMERID
        {
            get
            {
                return fCUSTOMERID;
            }

            set
            {
                fCUSTOMERID = value;
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

        public FMAINBOOKSTDCURRID FMAINBOOKSTDCURRID
        {
            get
            {
                return fMAINBOOKSTDCURRID;
            }

            set
            {
                fMAINBOOKSTDCURRID = value;
            }
        }

        public FEXCHANGETYPE FEXCHANGETYPE
        {
            get
            {
                return fEXCHANGETYPE;
            }

            set
            {
                fEXCHANGETYPE = value;
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

        public FSALEORGID FSALEORGID
        {
            get
            {
                return fSALEORGID;
            }

            set
            {
                fSALEORGID = value;
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

        public List<FEntityDetail> FEntityDetail
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
    }
}
