using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 病人
    /// </summary>
    public struct Patients
    {
        static private Patients _default;

        static public Patients Default
        {
            get { return _default; }
        }

        static Patients()
        {
            _default.Name = "";
            _default.Age = 0;
            _default.Gender = 0;
            _default.Height = 0;
            _default.Weight = 0;
            _default.Picture = "";
        }

        /// <summary>
        /// 姓名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name;

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age;

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender;

        /// <summary>
        /// 身高
        /// </summary>
        public int Height;

        /// <summary>
        /// 体重
        /// </summary>
        public int Weight;

        /// <summary>
        /// 照片
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 100)]
        public string Picture;

    }
}
