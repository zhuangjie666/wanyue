using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierSave
{
    public class FLocationInfo
    {
        private string fLocName;
        private FLocNewContact fLocNewContact;
        private string fLocAddress;
        private string fLocMobile;
   

        public string FLocName
        {
            get
            {
                return fLocName;
            }

            set
            {
                fLocName = value;
            }
        }

        public string FLocAddress
        {
            get
            {
                return fLocAddress;
            }

            set
            {
                fLocAddress = value;
            }
        }

        public string FLocMobile
        {
            get
            {
                return fLocMobile;
            }

            set
            {
                fLocMobile = value;
            }
        }

        public FLocNewContact FLocNewContact
        {
            get
            {
                return fLocNewContact;
            }

            set
            {
                fLocNewContact = value;
            }
        }
    }
}
