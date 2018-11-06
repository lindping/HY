using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 惊厥 Convulsions
    /// </summary>
    public struct Convulsions
    {
        static private Convulsions _default;

        static public Convulsions Default
        {
            get { return _default; }
        }

        static Convulsions()
        {
            _default.Clonic = Clonic.Default;
            _default.Tonic = Tonic.Default;

        }

        /// <summary>
        /// 阵挛
        /// </summary>
        public Clonic Clonic;

        /// <summary>
        /// 强直
        /// </summary>
        public Tonic Tonic;
    }

    /// <summary>
    /// 阵挛
    /// </summary>
    public struct Clonic
    {
        static private Clonic _default;

        static public Clonic Default
        {
            get { return _default; }
        }

        static Clonic()
        {
            _default.RightArm = Controller.Default;
            _default.LeftArm = Controller.Default;

        }

        /// <summary>
        /// 右手臂 Right Arm
        /// </summary>
        public Controller RightArm;

        /// <summary>
        /// 左手臂 Left Arm
        /// </summary>
        public Controller LeftArm;

    }
    
    /// <summary>
    /// 强直
    /// </summary>
    public struct Tonic
    {
        static private Tonic _default;

        static public Tonic Default
        {
            get { return _default; }
        }

        static Tonic()
        {
            _default.NeckAnkylosis = Controller.Default;

        }

        public Controller NeckAnkylosis;
    }
    
}
