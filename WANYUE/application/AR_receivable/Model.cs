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
        private DateTime fDATE;//业务日期
        private DateTime fENDDATE_H;//到期日
        private FCUSTOMERID fCUSTOMERID;
        private FMAINBOOKSTDCURRID fMAINBOOKSTDCURRID;
        private FEXCHANGETYPE fEXCHANGETYPE;
        private FCURRENCYID fCURRENCYID;
        private FSETTLEORGID fSETTLEORGID;
        private FPAYORGID fPAYORGID;
        private FSALEORGID fSALEORGID;
        private FsubHeadFinc fsubHeadFinc;
        private List<FEntityDetail> FEntityDetail;

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

        public List<FEntityDetail> FEntityDetail1
        {
            get
            {
                return FEntityDetail;
            }

            set
            {
                FEntityDetail = value;
            }
        }

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
    }
}
