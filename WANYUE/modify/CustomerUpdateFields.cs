﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.modify
{
    public class CustomerUpdateFields:UpdateFields
    {
        private string fName;

        public string FName
        {
            get
            {
                return fName;
            }

            set
            {
                fName = value;
            }
        }
    }
}
