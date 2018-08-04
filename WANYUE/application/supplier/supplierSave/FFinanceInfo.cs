using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierSave
{
   public class FFinanceInfo
    {
        private FPayCurrencyId fPayCurrencyId;

        public FPayCurrencyId FPayCurrencyId
        {
            get
            {
                return fPayCurrencyId;
            }

            set
            {
                fPayCurrencyId = value;
            }
        }
    }
}
