using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace YH.Network.Framework
{
    /// <summary>
    /// 名称：NetClient
    /// 描述：客户端通讯基类,为客户端程序提供网络通讯
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YH
    /// 用例：
    /// </summary>
    public class NetClient
    {
        /// <summary>
        /// 接收数据缓冲区大小64K
        /// </summary>
        public const int DefaultBufferSize = 1024;

        #region 字段

        /// <summary>
        /// 客户端与服务器之间的会话类
        /// </summary>
        private Session _session;

        /// <summary>
        /// 客户端是否已经连接服务器
        /// </summary>
        private bool _isConnected = false;

        /// <summary>
        /// 报文解析器
        /// </summary>
        private DatagramResolver _resolver;

        /// <summary>
        /// 通讯格式编码解码器
        /// </summary>
        private Coder _coder;

        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        private byte[] _recvDataBuffer = new byte[DefaultBufferSize];

        #endregion


        #region 事件定义

        /// <summary>
        /// 已经连接服务器事件
        /// </summary>
        public event NetEvent ConnectedServer;

        /// <summary>
        /// 接收到数据报文事件
        /// </summary>
        public event NetEvent ReceivedDatagram;

        /// <summary>
        /// 连接断开事件
        /// </summary>
        public event NetEvent DisConnectedServer;

        #endregion

        #region 属性

        /// <summary>
        /// 返回客户端与服务器之间的会话对象
        /// </summary>
        public Session ClientSession
        {
            get
            {
                return _session;
            }
        }

        /// <summary>
        /// 返回客户端与服务器之间的连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
        }

        /// <summary>
        /// 数据报文分析器
        /// </summary>
        public DatagramResolver Resovlver
        {
            get
            {
                return _resolver;
            }
            set
            {
                _resolver = value;
            }
        }

        /// <summary>
        /// 编码解码器
        /// </summary>
        public Coder ServerCoder
        {
            get
            {
                return _coder;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 默认构造函数,使用默认的编码格式
        /// </summary>
        public NetClient()
        {
            _coder = new Coder(Coder.EncodingMothord.Default);
        }

        /// <summary>
        /// 构造函数,使用一个特定的编码器来初始化
        /// </summary>
        /// <param name="_coder">报文编码器</param>
        public NetClient(Coder coder)
        {
            _coder = coder;
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="ip">服务器IP地址</param>
        /// <param name="port">服务器端口</param>
        public virtual void Connect(string ip, int port)
        {
            /*
            if (IsConnected)
            {
                Debug.Assert(_session != null);
                Close();
            }
            */

            Socket newClientSocket = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
            newClientSocket.BeginConnect(iep, new AsyncCallback(Connected), newClientSocket);

        }

        /// <summary>
        /// 发送数据报文
        /// </summary>
        /// <param name="datagram"></param>
        public virtual void Send(string datagram)
        {
            if (datagram.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("没有连接服务器，不能发送数据"));
            }

            //获得报文的编码字节
            byte[] data = _coder.GetEncodingBytes(datagram);

            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), _session.ClientSocket);
        }

        /// <summary>
        /// 发送数据报文
        /// </summary>
        /// <param name="dataBytes"></param>
        public virtual void SendBytes(byte[] dataBytes)
        {
            if (dataBytes.Length == 0)
            {
                return;
            }

            if (!_isConnected)
            {
                throw (new ApplicationException("没有连接服务器，不能发送数据"));
            }

            //获得报文的编码字节
            byte[] data = _coder.GetEncodingBytes(dataBytes, _resolver);

            _session.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), _session.ClientSocket);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void Close()
        {
            if (!_isConnected)
            {
                return;
            }

            _session.Close();
            _session = null;
            _isConnected = false;
        }

        #endregion

        #region 受保护方法

        /// <summary>
        /// 数据发送完成处理函数
        /// </summary>
        /// <param name="iar"></param>
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket remoteSocket = (Socket)iar.AsyncState;
            int sent = remoteSocket.EndSend(iar);
            Debug.Assert(sent != 0);
        }

        /// <summary>
        /// 建立Tcp连接后处理过程
        /// </summary>
        /// <param name="iar">异步Socket</param>
        protected virtual void Connected(IAsyncResult iar)
        {
            Socket cliectSocket = (Socket)iar.AsyncState;
            cliectSocket.EndConnect(iar);

            //创建新的会话
            _session = new Session(cliectSocket);
            _isConnected = true;

            //触发连接建立事件
            if (ConnectedServer != null)
            {
                ConnectedServer(this, new NetEventArgs(_session));
            }

            _session.ClientSocket.BeginReceive(_recvDataBuffer, 0,
             DefaultBufferSize, SocketFlags.None,
             new AsyncCallback(ReceiveData), cliectSocket);
        }

        /// <summary>
        /// 数据接收处理函数
        /// </summary>
        /// <param name="iar">异步Socket</param>
        protected virtual void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;

            try
            {
                int recv = remote.EndReceive(iar);

                if (recv == 0)
                {
                    _session.TypeOfExit = Session.ExitType.NormalExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }

                    return;
                }

                byte[] receiveDataBytes = _coder.GetDecodingBytes(_recvDataBuffer, recv);

                //string receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);

                //通过事件发布收到的报文
                if (ReceivedDatagram != null)
                {
                    ICloneable copySession = (ICloneable)_session;
                    Session clientSession = (Session)copySession.Clone();
                    clientSession.RecvDataBuffer = new byte[receiveDataBytes.Length];

                    Array.ConstrainedCopy(receiveDataBytes, 0, clientSession.RecvDataBuffer, 0, receiveDataBytes.Length);
                    //clientSession.Datagram = newDatagram;

                    //发布一个报文消息
                    ReceivedDatagram(this, new NetEventArgs(clientSession));

                    if (_resolver != null)
                    {                        
                        //if (_session.Datagram != null && _session.Datagram.Length != 0)
                        //{
                        //    receivedData = _session.Datagram + receivedData;
                        //}

                        //string[] recvDatagrams = _resolver.Resolve(ref receivedData);
                        //foreach (string newDatagram in recvDatagrams)
                        //{
                        //    ICloneable copySession = (ICloneable)_session;
                        //    Session clientSession = (Session)copySession.Clone();
                        //    clientSession.Datagram = newDatagram;

                        //    //发布一个报文消息
                        //    ReceivedDatagram(this, new NetEventArgs(clientSession));
                        //}

                        ////剩余的代码片断,下次接收的时候使用
                        //_session.Datagram = receivedData;
                    }
                    else
                    {
                        //ICloneable copySession = (ICloneable)_session;
                        //Session clientSession = (Session)copySession.Clone();
                        //clientSession.Datagram = receivedData;

                        //ReceivedDatagram(this, new NetEventArgs(clientSession));
                    }

                }//end of if(ReceivedDatagram != null)

                //继续接收数据
                _session.ClientSocket.BeginReceive(_recvDataBuffer, 0, DefaultBufferSize, SocketFlags.None,
                 new AsyncCallback(ReceiveData), _session.ClientSocket);
            }
            catch (SocketException ex)
            {
                //客户端退出
                if (10054 == ex.ErrorCode)
                {
                    _session.TypeOfExit = Session.ExitType.ExceptionExit;

                    if (DisConnectedServer != null)
                    {
                        DisConnectedServer(this, new NetEventArgs(_session));
                    }
                }
                else
                {
                    throw (ex);
                }
            }
            catch (ObjectDisposedException ex)
            {
                if (ex != null)
                {
                    ex = null;
                    //DoNothing;
                }
            }
        }
        #endregion
    }
}
