using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 腹部触诊 Abdominal Touch
    /// </summary>
    public struct AbdominalTouch
    {
        static private AbdominalTouch _default;

        static public AbdominalTouch Default
        {
            get { return _default; }
        }

        static AbdominalTouch()
        {
            _default.RightUpperTouch = Operator.Default;
            _default.LeftUpperTouch = Operator.Default;
            _default.MiddleTouch = Operator.Default;
            _default.RightLowerTouch = Operator.Default;
            _default.LeftLowerTouch = Operator.Default;

        }

        /// <summary>
        /// 右上腹肋弓下压痛点 Right Upper
        /// </summary>
        public Operator RightUpperTouch;

        /// <summary>
        /// 左上腹压痛点 Left Upper
        /// </summary>
        public Operator LeftUpperTouch;

        /// <summary>
        /// 肚脐周围压痛点 Middle
        /// </summary>
        public Operator MiddleTouch;

        /// <summary>
        /// 右下腹压痛点 Right Lower
        /// </summary>
        public Operator RightLowerTouch;

        /// <summary>
        /// 左下腹压痛点 Left Lower
        /// </summary>
        public Operator LeftLowerTouch;
    }

}
