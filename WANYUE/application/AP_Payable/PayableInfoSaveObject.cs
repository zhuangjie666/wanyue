﻿using Kingdee.K3.WANYUE.PlugIn.service.application.customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.AP_Payable
{
    public class PayableInfoSaveObject : AllInfoObject
    {
        private Model model;

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
