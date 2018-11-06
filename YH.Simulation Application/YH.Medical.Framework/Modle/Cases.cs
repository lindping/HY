using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 病历
    /// </summary>
    public struct Cases
    {
        static private Cases _default;

        static public Cases Default
        {
            get { return _default; }
        }

        static Cases()
        {
            _default.Name = "";
            _default.Patients = Patients.Default;
            _default.ChiefComplaint = "";
            _default.Disease = Diseases.Default;
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
        /// 主诉
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ChiefComplaint;

        /// <summary>
        /// 病症
        /// </summary>
        public Diseases Disease;
    }
}
