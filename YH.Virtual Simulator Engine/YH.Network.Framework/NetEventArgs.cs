using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Network.Framework
{
    /// <summary>
    /// 名称：NetEventArgs
    /// 描述：服务器程序事件类
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YH
    /// 用例：
    /// </summary>
    public class NetEventArgs : EventArgs
    {
        /// <summary>
        /// 客户端与服务器之间的会话
        /// </summary>
        private Session _clientSession;

        /// <summary>
        /// 激发该事件的会话对象
        /// </summary>
        public Session ClientSession
        {
            get
            {
                return _clientSession;
            }

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="client"></param>
        public NetEventArgs(Session clientSession)
        {
            if (null == clientSession)
            {
                throw (new ArgumentNullException());
            }
            _clientSession = clientSession;
        }
    }

    /// <summary> 
    /// 网络通讯事件模型委托 
    /// </summary> 
    public delegate void NetEvent(object sender, NetEventArgs e);
}
