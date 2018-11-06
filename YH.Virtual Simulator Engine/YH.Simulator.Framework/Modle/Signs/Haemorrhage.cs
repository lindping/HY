using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 出血 Haemorrhage
    /// </summary>
    public struct Haemorrhage
    {
        static private Haemorrhage _default;

        static public Haemorrhage Default
        {
            get { return _default; }
        }

        static Haemorrhage()
        {
            _default.RightUpper = BloodVessel.Default;
            _default.LeftUpper = BloodVessel.Default;
            _default.RightLower = BloodVessel.Default;
            _default.LeftLower = BloodVessel.Default;
        }

        /// <summary>
        /// 右上 Right Upper
        /// </summary>
        public BloodVessel RightUpper;

        /// <summary>
        /// 左上 Left Upper
        /// </summary>
        public BloodVessel LeftUpper;

        /// <summary>
        /// 右下 Right Lower
        /// </summary>
        public BloodVessel RightLower;

        /// <summary>
        /// 左下 Left Lower
        /// </summary>
        public BloodVessel LeftLower;
    }

    /// <summary>
    /// 血管
    /// </summary>
    public struct BloodVessel
    {
        static private BloodVessel _default;

        static public BloodVessel Default
        {
            get { return _default; }
        }

        static BloodVessel()
        {
            _default.Arterial = Controller.Default;
            _default.Venous = Controller.Default;

        }

        /// <summary>
        /// 动脉
        /// </summary>
        public Controller Arterial;

        /// <summary>
        /// 静脉
        /// </summary>
        public Controller Venous;
    }        

}
