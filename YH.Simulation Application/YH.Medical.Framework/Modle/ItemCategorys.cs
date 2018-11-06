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
    public struct ItemCategorys
    {
        static private ItemCategorys _default;

        static public ItemCategorys Default
        {
            get { return _default; }
        }

        static ItemCategorys()
        {
            _default.ID = 0;
            _default.NO = "";
            _default.Name = "";
            _default.Character = 0;
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
        /// 特性  标准项目1、治疗2
        /// </summary>
        public int Character;


    }
}
