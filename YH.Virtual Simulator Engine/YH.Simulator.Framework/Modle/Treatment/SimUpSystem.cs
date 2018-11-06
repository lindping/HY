using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    public struct SimUpSystem
    {
        static private SimUpSystem _default;

        static public SimUpSystem Default
        {
            get { return _default; }
        }

        static SimUpSystem()
        {
            _default.SimAttribute = SimAttribute.Default;
            _default.Language = Language.Default;
            _default.PatientMonitor = Controller.Default;
            _default.DefibrillationCalibration = Operator.Default;
            _default.BloodPressureCalibration = Operator.Default;
            _default.Link = Operator.Default;
        }

        /// <summary>
        /// 模型属性
        /// </summary>
        public SimAttribute SimAttribute;

        /// <summary>
        /// 语言
        /// </summary>
        public Language Language;

        /// <summary>
        /// 病人监护仪开关
        /// </summary>
        public Controller PatientMonitor;

        /// <summary>
        /// 除颤校准开关
        /// </summary>
        public Operator DefibrillationCalibration;

        /// <summary>
        /// 血压校准开关
        /// </summary>
        public Operator BloodPressureCalibration;

        /// <summary>
        /// 在线状态
        /// </summary>
        public Operator Link;
    }
}
