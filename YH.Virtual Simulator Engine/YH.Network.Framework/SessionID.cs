using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Network.Framework
{
    /// <summary>
    /// 名称：SessionID
    /// 描述：Session唯一标识，辅助Session对象在Hash表中完成特定功能
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YMKJ
    /// 用例：
    public class SessionID
    {
        /// <summary>
        /// 与Session对象的Socket对象的Handle值相同,必须用这个值来初始化它
        /// </summary>
        private int _id;
        
        /// <summary>
        /// 返回ID值
        /// </summary>
        private int ID
        {
            get { return _id; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">Socket的Handle值</param>
        public SessionID(int id)
        {
            _id = id;
        }

        /// <summary>
        /// 重载.为了符合Hashtable键值特征
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                SessionID right = (SessionID)obj;
                return _id == right._id;
            }
            else if (this == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 重载.为了符合Hashtable键值特征
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _id;
        }
        /// <summary>
        /// 重载,为了方便显示输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
