using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{


    /// <summary>
    /// 尿液 Urine
    /// </summary>
    public struct Urine
    {
        static private Urine _default;

        static public Urine Default
        {
            get { return _default; }
        }

        static Urine()
        {
            _default.Urinate = Controller.Default;
        }

        /// <summary>
        /// 排尿
        /// </summary>
        public Controller Urinate;
        
    }
       
}
