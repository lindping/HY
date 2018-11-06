using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 开关控制器
    /// </summary>
    public struct Controller
    {

        static private Controller _default;

        static public Controller Default
        {
            get { return _default; }
        }

        static Controller()
        {
            _default.Status = ControllerStatus.No;
            _default.Value = 0;

        }

        /// <summary>
        /// 开关状态
        /// </summary>
        public ControllerStatus Status;
        /// <summary>
        /// 数据量
        /// </summary>
        public int Value;
    }

    /// <summary>
    /// 控制器开关状态
    /// </summary>
    public enum ControllerStatus
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
