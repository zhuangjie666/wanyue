using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.voucher
{
   public class VoucherInfoSaveObject : AllInfoObject
    {
        private Model model;
        private string fid;

      

        public string Fid
        {
            get
            {
                return fid;
            }

            set
            {
                fid = value;
            }
        }

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
    }
}
