using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 触诊 Palpation
    /// </summary>
    public struct Palpation
    {
        static private Palpation _default;

        static public Palpation Default
        {
            get { return _default; }
        }

        static Palpation()
        {
            _default.Abdominal = Abdominal.Default;

        }

        public Abdominal Abdominal;
    }

    /// <summary>
    /// 腹部触诊 Abdominal Touch
    /// </summary>
    public struct Abdominal
    {
        static private Abdominal _default;

        static public Abdominal Default
        {
            get { return _default; }
        }

        static Abdominal()
        {
            _default.RightUpper = Controller.Default;
            _default.LeftUpper = Controller.Default;
            _default.Middle = Controller.Default;
            _default.RightLower = Controller.Default;
            _default.LeftLower = Controller.Default;
        }

        /// <summary>
        /// 右上腹肋弓下 Right Upper
        /// </summary>
        public Controller RightUpper;

        /// <summary>
        /// 左上腹压痛点 Left Upper
        /// </summary>
        public Controller LeftUpper;

        /// <summary>
        /// 肚脐周围压痛点 Middle
        /// </summary>
        public Controller Middle;

        /// <summary>
        /// 右下腹压痛点 Right Lower
        /// </summary>
        public Controller RightLower;

        /// <summary>
        /// 左下腹压痛点 Left Lower
        /// </summary>
        public Controller LeftLower;
    }

}
