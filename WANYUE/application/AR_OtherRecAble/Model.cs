using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AR_OtherRecAble
{
    public class Model
    {
        private string fnumber;
        private FBillTypeID fBillTypeID;
        private DateTime fDate;
        private DateTime fENDDATE_H;//到期日
        private DateTime fACCNTTIMEJUDGETIME;
        private string fCONTACTUNITTYPE;
        private FCONTACTUNIT fCONTACTUNIT;
        private FCURRENCYID fCURRENCYID;
        private FSETTLEORGID fSETTLEORGID;
        private FPAYORGID fPAYORGID;
        private decimal fAMOUNTFOR;
        private List<FEntity> FEntity;

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

        public DateTime FDate
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

        public string FCONTACTUNITTYPE
        {
            get
            {
                return fCONTACTUNITTYPE;
            }

            set
            {
                fCONTACTUNITTYPE = value;
            }
        }

        public FCONTACTUNIT FCONTACTUNIT
        {
            get
            {
                return fCONTACTUNIT;
            }

            set
            {
                fCONTACTUNIT = value;
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

        public List<FEntity> FEntity1
        {
            get
            {
                return FEntity;
            }

            set
            {
                FEntity = value;
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
