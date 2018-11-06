using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    public struct DataValue
    {
        static private DataValue _default;

        static public DataValue Default
        {
            get { return _default; }
        }

        static DataValue()
        {
            _default.Value = 0;
            _default.Unit = 0;

        }

        /// <summary>
        /// 数据量
        /// </summary>
        public int Value;
        /// <summary>
        /// 数据量单位
        /// </summary>
        public int Unit;
    }
}
