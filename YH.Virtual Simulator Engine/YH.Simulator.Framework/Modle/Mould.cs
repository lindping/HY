using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 模型
    /// </summary>
    public struct Mould
    {
        static private Mould _default;

        static public Mould Default
        {
            get { return _default; }
        }

        static Mould()
        {
            _default.Identifier = 0;
            _default.Attribute = Attribute.Default;
            _default.LanguageCode = 0;
            _default.PatientMonitorON = 0;
            _default.DefibrillationCalibration = 0;
            _default.BloodPressureCalibration = 0;
            _default.Link = 0;
        }

        /// <summary>
        /// 虚拟0、真实1
        /// </summary>
        public int Identifier;

        /// <summary>
        /// 模型属性
        /// </summary>
        public Attribute Attribute;

        /// <summary>
        /// 语言代码 Language Code
        /// </summary>
        public int LanguageCode;

        /// <summary>
        /// 病人监护仪开关
        /// </summary>
        public int PatientMonitorON;

        /// <summary>
        /// 除颤校准 Defibrillation calibration
        /// </summary>
        public int DefibrillationCalibration;

        /// <summary>
        /// 血压校准 Blood pressure calibration
        /// </summary>
        public int BloodPressureCalibration;

        /// <summary>
        /// 在线检测 Link 
        /// </summary>
        public int Link;
    }

    /// <summary>
    /// 模型属性
    /// </summary>
    public struct Attribute
    {
        static private Attribute _default;

        static public Attribute Default
        {
            get { return _default; }
        }

        static Attribute()
        {

        }


        /// <summary>
        /// 模型类型
        /// </summary>
        public int PatientType;

        /// <summary>
        /// 模型编号 Product number 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string PN;

        /// <summary>
        /// 模型序列号 Serial number
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string SN;
    }
}
