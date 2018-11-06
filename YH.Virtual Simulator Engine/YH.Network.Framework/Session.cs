using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace YH.Network.Framework
{
    /// <summary>
    /// 名称：Session
    /// 描述：与服务器之间的会话接口类
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YH
    /// 用例：
    /// </summary>
    public class Session : ICloneable
    {
        #region ******** 字段 ********

        /// <summary>
        /// 会话ID
        /// </summary>
        private SessionID _id;

        /// <summary>
        /// 会话通道号
        /// </summary>
        private string _channel = "";

        /// <summary>
        /// Socket实例
        /// </summary>
        private Socket _clientSocket;

        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] _recvDataBuffer;

        /// <summary>
        /// 客户端发送到服务器的报文
        /// 注意:在有些情况下报文可能只是报文的片断而不完整
        /// </summary>
        private string _datagram;

        /// <summary>
        /// 退出类型
        /// </summary>
        private ExitType _exitType;

        /// <summary>
        /// 退出类型枚举
        /// </summary>
        public enum ExitType
        {
            /// <summary>
            /// 正常退出
            /// </summary>
            NormalExit,
            /// <summary>
            /// 异常退出
            /// </summary>
            ExceptionExit
        };

        #endregion

        #region ******** 属性 ********

        /// <summary>
        /// 会话ID
        /// </summary>
        public SessionID ID
        {
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// 会话执行的通道号
        /// </summary>
        public string Channel
        {
            get
            {
                return _channel;
            }

            set
            {
                _channel = value;
            }
        }

        /// <summary>
        /// 获得与客户端会话关联的Socket对象
        /// </summary>
        public Socket ClientSocket
        {
            get
            {
                return _clientSocket;

            }
        }

        /// <summary>
        /// 接收数据缓冲区 
        /// </summary>
        public byte[] RecvDataBuffer
        {
            get
            {
                return _recvDataBuffer;
            }
            set
            {
                _recvDataBuffer = value;
            }
        }

        /// <summary>
        /// 存取会话的报文
        /// </summary>
        public string Datagram
        {
            get
            {
                return _datagram;
            }
            set
            {
                _datagram = value;
            }
        }

        /// <summary>
        /// 退出方式
        /// </summary>
        public ExitType TypeOfExit
        {
            get
            {
                return _exitType;
            }
            set
            {
                _exitType = value;
            }
        }

        #endregion

        #region ******** 方法 ********

        public Session(Socket clientSocket)
        {
            Debug.Assert(clientSocket != null);
            _clientSocket = clientSocket;
            _id = new SessionID((int)clientSocket.Handle);
        }

        /// <summary>
        /// 重载GetHashCode()方法,HashCode中获取Socket实例的Handle值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (int)_clientSocket.Handle;
        }

        /// <summary>
        /// 重载Equals()方法,比较两个Session是否同一个
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Session rightObj = (Session)obj;

            return (int)_clientSocket.Handle == (int)rightObj.ClientSocket.Handle;
        }

        /// <summary>
        /// 重载ToString()方法,返回Session对象的特征
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string result = string.Format("Session:{0},IP:{1}",
            _id, _clientSocket.RemoteEndPoint.ToString());
            return result;
        }

        /// <summary>
        /// 关闭会话
        /// </summary>
        public void Close()
        {
            Debug.Assert(_clientSocket != null);
            //关闭数据的接受和发送
            _clientSocket.Shutdown(SocketShutdown.Both);
            //清理资源
            _clientSocket.Close();
        }

        #endregion

        #region ******** ICloneable 成员 ********
        object System.ICloneable.Clone()
        {
            Session newSession = new Session(_clientSocket);
            newSession.Datagram  = Datagram;
            newSession.TypeOfExit = _exitType;
            return newSession;
        }
        #endregion
    }        
}
