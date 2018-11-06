using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 
    /// </summary>
    public struct Items
    {
        static private Items _default;

        static public Items Default
        {
            get { return _default; }
        }

        static Items()
        {
            _default.ID = 0;
            _default.NO = "";
            _default.Name = "";
            _default.CategoryID = 0;
        }

        /// <summary>
        /// ID
        /// </summary>
        public int ID;

        /// <summary>
        /// 编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string NO;

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
