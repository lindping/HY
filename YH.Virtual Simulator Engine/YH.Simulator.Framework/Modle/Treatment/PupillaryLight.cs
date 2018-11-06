using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 瞳孔对光检查
    /// </summary>
    public struct  PupillaryLight
    {
        static private PupillaryLight _default;

        static public PupillaryLight Default
        {
            get { return _default; }
        }

        static PupillaryLight()
        {
            _default.Right = Operator.Default;
            _default.Left = Operator.Default;
        }

        /// <summary>
        /// 右侧
        /// </summary>
        public Operator Right ;

        /// <summary>
        /// 左侧
        /// </summary>
        public Operator Left;
    }


}
