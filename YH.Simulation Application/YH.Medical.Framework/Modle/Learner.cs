using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 学员
    /// </summary>
    public struct Learner
    {
        static private Learner _default;

        static public Learner Default
        {
            get { return _default; }
        }

        static Learner()
        {
            _default.Brief = "";
            _default.ShowOnPatientMonitor =0;
            _default.MakeAvailableOnPatientMonitor = 0;
        }

        /// <summary>
        /// 学员概况
        /// </summary>
        public string Brief;

        /// <summary>
        /// 启动时在病人监护仪上显示给学员简述  Show Learner brief on Patient monitor at startup
        /// </summary>
        public int ShowOnPatientMonitor;

        /// <summary>
        /// 在病人监护仪上让学员简述可得 Make learner brief available on Patient monitor
        /// </summary>
        public int MakeAvailableOnPatientMonitor;
    }
}
