using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.customer
{
    public class AllInfoObject
    {
        public string creator { get; set; }
        public List<string> needUpDateFields { get; set; }
        public List<string> needReturnFields { get; set; }
        public string isDeleteEntry { get; set; }
        public string subSystemId { get; set; }
        public string isVerifyBaseDataField { get; set; }
    }
}
