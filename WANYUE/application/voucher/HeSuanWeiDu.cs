using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingdee.K3.WANYUE.PlugIn.service.application.voucher
{
    public static class HeSuanWeiDu
    {
        //  public static string[] hesuanweidu = { "HSWD01_SYS", "HSWD02_SYS", "HSWD03_SYS", "HSWD04_SYS", "HSWD05_SYS", "HSWD06_SYS", "HSWD07_SYS", "HSWD08_SYS", "HSWD09_SYS", "HSWD010_SYS", "ZDY0001", "ZDY0002", "ZDY0003", "ZDY0004" };
       public static Dictionary<string, object> hesuan = new Dictionary<string, object>();
        public static Dictionary<string, object> init() {
            hesuan.Add("HSWD01_SYS", new FDETAILID__FFLEX4().Fnumber);
            hesuan.Add("HSWD02_SYS", new FDETAILID__FFLEX5().Fnumber);
            hesuan.Add("HSWD03_SYS", new FDETAILID__FFLEX6().Fnumber);
            hesuan.Add("HSWD04_SYS", new FDETAILID__FFLEX7().Fnumber);
            hesuan.Add("HSWD05_SYS", new FDETAILID__FFLEX8().Fnumber);
            hesuan.Add("HSWD06_SYS", new FDETAILID__FFLEX9().Fnumber);
            hesuan.Add("HSWD07_SYS", new FDETAILID__FFLEX10().Fnumber);
            hesuan.Add("HSWD08_SYS", new FDETAILID__FFLEX11().Fnumber);
            hesuan.Add("HSWD09_SYS", new FDETAILID__FFLEX12().Fnumber);
            hesuan.Add("HSWD10_SYS", new FDETAILID__FFLEX13().Fnumber);
            hesuan.Add("ZDY0001", new FDETAILID__FFLEX4().Fnumber);
            hesuan.Add("ZDY0002", new FDETAILID__FFLEX4().Fnumber);
            hesuan.Add("ZDY0003", new FDETAILID__FFLEX4().Fnumber);
            hesuan.Add("ZDY0004", new FDETAILID__FFLEX4().Fnumber);

            return hesuan;
        }
    }
}
