using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 课题
    /// </summary>
    public struct Projects
    {
        static private Projects _default;

        static public Projects Default
        {
            get { return _default; }
        }

        static Projects()
        {
            _default.Name = "";
            _default.Type = 0;
            _default.PatientType = 0;
            _default.Feature = 0;
            _default.Simulators = "";
            _default.Equipment = "";
            _default.LanguageCode = 0;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name;

        /// <summary>
        /// 类型 1基于模拟人的训练、2标准病人、3任务培训器
        /// </summary>
        public int Type;

        /// <summary>
        /// 病人类型
        /// </summary>
        public int PatientType;

        /// <summary>
        /// 特性 
        /// </summary>
        public int Feature;

        /// <summary>
        /// 支持的模拟人
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string Simulators;

        /// <summary>
        /// 设备
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string Equipment;

        /// <summary>
        /// 语言代码 Language Code
        /// </summary>
        public int LanguageCode;

    }
}
