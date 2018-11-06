using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 操作器
    /// </summary>
    public struct Operator
    {
        static private Operator _default;

        static public Operator Default
        {
            get { return _default; }
        }

        static Operator()
        {
            _default.Status = OperatorStatus.No;
            _default.Volume = 0;

        }

        /// <summary>
        /// 操作状态
        /// </summary>
        public OperatorStatus Status;
        /// <summary>
        /// 数据量
        /// </summary>
        public int Volume;
    }

    public enum OperatorStatus
    {
        /// <summary>
        /// 否
        /// </summary>
        No = 0,
        /// <summary>
        /// 是
        /// </summary>
        Yes = 1,

    }
}
