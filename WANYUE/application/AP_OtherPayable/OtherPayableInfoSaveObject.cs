using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_OtherPayable
{
   public class OtherPayableInfoSaveObject : AllInfoObject
    {
        private Model model;
        private string fNumber;
        public Model Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public string FNumber
        {
            get
            {
                return fNumber;
            }

            set
            {
                fNumber = value;
            }
        }
    }
}
