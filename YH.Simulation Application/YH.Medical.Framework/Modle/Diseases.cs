using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 病症
    /// </summary>
    public struct Diseases
    {
        static private Diseases _default;

        static public Diseases Default
        {
            get { return _default; }
        }

        static Diseases()
        {
            _default.Name = "";
            _default.CategoryID = 0;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name;

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryID;
    }
}
