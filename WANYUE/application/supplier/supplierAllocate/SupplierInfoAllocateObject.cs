﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.supplier.supplierAllocate
{
    public class SupplierInfoAllocateObject
    {
        private string pkIds;
        private string tOrgIds;
        private string isAutoSubmitAndAudit;

        public string PkIds
        {
            get
            {
                return pkIds;
            }

            set
            {
                pkIds = value;
            }
        }

        public string TOrgIds
        {
            get
            {
                return tOrgIds;
            }

            set
            {
                tOrgIds = value;
            }
        }

        public string IsAutoSubmitAndAudit
        {
            get
            {
                return isAutoSubmitAndAudit;
            }

            set
            {
                isAutoSubmitAndAudit = value;
            }
        }
    }
}
