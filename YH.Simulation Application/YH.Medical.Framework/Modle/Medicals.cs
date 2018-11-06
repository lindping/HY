using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 医案
    /// </summary>
    public struct Medicals
    {
        static private Medicals _default;

        static public Medicals Default
        {
            get { return _default; }
        }

        static Medicals()
        {
            _default.Name = "";
            _default.Patients = Patients.Default;
            _default.Outline = "";
            _default.Learner = Learner.Default;
            _default.Objectives = "";
            _default.Equipent = "";
        }

        /// <summary>
        /// 名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name;

        /// <summary>
        /// 病人信息
        /// </summary>
        public Patients Patients;

        /// <summary>
        /// 病例大纲
        /// </summary>
        public string Outline;

        /// <summary>
        /// 学员概况
        /// </summary>
        public Learner Learner;

        /// <summary>
        /// 学习目标
        /// </summary>
        public string Objectives;

        /// <summary>
        /// 设备清单
        /// </summary>
        public string Equipent;

    }
}
