using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 气管插管 Trachea Cannula
    /// </summary>
    public struct TracheaCannula
    {
        static private TracheaCannula _default;

        static public TracheaCannula Default
        {
            get { return _default; }
        }

        static TracheaCannula()
        {
            _default.InTrachea = Operator.Default;
            _default.InEsophagus = Operator.Default;
            _default.InRightBronchus = Operator.Default;
            _default.InLeftBronchus = Operator.Default;
        }
        /// <summary>
        /// 插入气管 
        /// </summary>
        public Operator InTrachea;

        /// <summary>
        /// 插入食管 
        /// </summary>
        public Operator InEsophagus;

        /// <summary>
        /// 插入右支气管 
        /// </summary>
        public Operator InRightBronchus;

        /// <summary>
        /// 插入左支气管
        /// </summary>
        public Operator InLeftBronchus;
    }    
}
