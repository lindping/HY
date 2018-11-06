using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 起搏 Pacing
    /// </summary>
    public struct Pacing
    {
        static private Pacing _default;

        static public Pacing Default
        {
            get { return _default; }
        }

        static Pacing()
        {
            _default.Current = 0;

        }

        public int Current;
    }

    /// <summary>
    /// 起搏电极 Pacer Electrode
    /// </summary>
    public struct PacerElectrode
    {

        static private PacerElectrode _default;

        static public PacerElectrode Default
        {
            get { return _default; }
        }

        static PacerElectrode()
        {
            _default.Sternum = Operator.Default;
            _default.Back = Operator.Default;

        }

        /// <summary>
        /// 胸前（Sternum）
        /// </summary>
        public Operator Sternum;

        /// <summary>
        ///后背（Back）
        /// </summary>
        public Operator Back;
    }


}
