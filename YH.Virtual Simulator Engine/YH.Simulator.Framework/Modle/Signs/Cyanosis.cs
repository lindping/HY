using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 紫绀
    /// </summary>
    public struct Cyanosis
    {
        static private Cyanosis _default;

        static public Cyanosis Default
        {
            get { return _default; }
        }

        static Cyanosis()
        {
            _default.Lips = Controller.Default;
            _default.Fingernail = Controller.Default;
            _default.FootNail = Controller.Default;
        }

        /// <summary>
        /// 口唇紫绀
        /// </summary>
        public Controller Lips;

        /// <summary>
        /// 手指甲床紫绀
        /// </summary>
        public Controller Fingernail;

        /// <summary>
        /// 脚指甲床紫绀
        /// </summary>
        public Controller FootNail;
    }    

}
