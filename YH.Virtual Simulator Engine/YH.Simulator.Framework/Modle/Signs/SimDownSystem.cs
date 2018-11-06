using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    public struct SimDownSystem
    {
        static private SimDownSystem _default;

        static public SimDownSystem Default
        {
            get { return _default; }
        }

        static SimDownSystem()
        {
            _default.SimConnector = SimConnector.Default;
            _default.SimAttribute = SimAttribute.Default;
            _default.Language = Language.Default;
            _default.PatientMonitor = Controller.Default;
            _default.DefibrillationCalibration = Controller.Default;
            _default.BloodPressureCalibration = Controller.Default;
            _default.Link = Controller.Default;
        }

        /// <summary>
        /// 模型连接器
        /// </summary>
        public SimConnector SimConnector;

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
        public Controller DefibrillationCalibration;

        /// <summary>
        /// 血压校准开关
        /// </summary>
        public Controller BloodPressureCalibration;

        /// <summary>
        /// 在线状态
        /// </summary>
        public Controller Link;
    }

    /// <summary>
    /// 模型连接器
    /// </summary>
    public struct SimConnector
    {
        static private SimConnector _default;

        static public SimConnector Default
        {
            get { return _default; }
        }

        static SimConnector()
        {
            _default.Connect.Status = ControllerStatus.No;
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        public Controller Connect;
    }

    /// <summary>
    /// 模型属性
    /// </summary>
    public struct SimAttribute
    {
        static private SimAttribute _default;

        static public SimAttribute Default
        {
            get { return _default; }
        }

        static SimAttribute()
        {
            _default.Tag = 0;
            _default.SimType = 0;
            _default.SimPN = 0;
            _default.SimSN = 0;
        }

        /// <summary>
        /// 模型标识
        /// </summary>
        public int Tag;

        /// <summary>
        /// 模型类型
        /// </summary>
        public int SimType;

        /// <summary>
        /// 模型编号
        /// </summary>
        public int SimPN;

        /// <summary>
        /// 模型序列号
        /// </summary>
        public int SimSN;
    }

    /// <summary>
    /// 语言
    /// </summary>
    public struct Language
    {
        static private Language _default;

        static public Language Default
        {
            get { return _default; }
        }

        static Language()
        {
            _default.LanguageCode = 0;
        }

        /// <summary>
        /// 语言代码
        /// </summary>
        public int LanguageCode;
    }
}
