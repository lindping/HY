using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 导尿 Catheterization
    /// </summary>
    public struct Catheterization
    {
        static private Catheterization _default;

        static public Catheterization Default
        {
            get { return _default; }
        }

        static Catheterization()
        {
            _default.InsertionBladder = Operator.Default;
        }

        /// <summary>
        /// 插入膀光
        /// </summary>
        public Operator InsertionBladder;
    }
    
}
