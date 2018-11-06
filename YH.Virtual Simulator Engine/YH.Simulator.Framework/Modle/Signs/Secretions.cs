using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 分泌 Secretions
    /// </summary>
    public struct Secretions
    {
        static private Secretions _default;

        static public Secretions Default
        {
            get { return _default; }
        }

        static Secretions()
        {
            _default.Sweat = Controller.Default;
            _default.Eyes = Controller.Default;
            _default.Mouth = Controller.Default;
            _default.Ears = Controller.Default;
            _default.Nose = Controller.Default;
            _default.Froth = Controller.Default;
        }

        /// <summary>
        /// 出汗 Sweat
        /// </summary>
        public Controller Sweat;

        /// <summary>
        /// 眼睛 Eyes
        /// </summary>
        public Controller Eyes;

        /// <summary>
        /// 口Mouth
        /// </summary>
        public Controller Mouth;

        /// <summary>
        /// 耳 Ears
        /// </summary>
        public Controller Ears;

        /// <summary>
        /// 鼻 Nose
        /// </summary>
        public Controller Nose;

        /// <summary>
        /// 白沫 Froth
        /// </summary>
        public Controller Froth;
    }


}
