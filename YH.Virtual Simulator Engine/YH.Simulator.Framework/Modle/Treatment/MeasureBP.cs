using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 测量血压
    /// </summary>
    public struct MeasureBP
    {
        static private MeasureBP _default;

        static public MeasureBP Default
        {
            get { return _default; }
        }

        static MeasureBP()
        {
            _default.CuffPressure = 0;
        }

        /// <summary>
        /// 袖带压力
        /// </summary>
        public int CuffPressure;
    }
}
