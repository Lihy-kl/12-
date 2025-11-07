using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHNSpec.Device.COM.Demo
{
   public enum EnumAngle
    {
        /// <summary>
        /// 2度
        /// </summary>
        [Description("2°")]
        TwoDegree = 2,

        /// <summary>
        /// 10度
        /// </summary>
        [Description("10°")]
        TenDegree = 10,
    }
}
