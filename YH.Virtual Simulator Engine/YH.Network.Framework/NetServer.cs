using System;
using System.Collections;
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
    /// 名称：NetServer
    /// 描述：服务器通讯基类,为服务器端程序提供网络通讯
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YH
    /// 用例：
    /// </summary>
    public class NetServer
    {
        /// <summary>
        /// 默认的服务器最大连接客户端端数据
        /// </summary>
        public const int DefaultMaxClient = 1024;

        /// <summary>
        /// 接收数据缓冲区大小64K
        /// </summary>
        public const int DefaultBufferSize = 64 * 1024;

        /// <summary>
        /// 最大数据报文大小
        /// </summary>
        public const int MaxDatagramSize = 640 * 1024;

        #region ******** 私有变量 ********

        /// <summary>
        /// 报文解析器
        /// </summary>
        private DatagramResolver _resolver;

        /// <summary>
        /// 通讯格式编码解码器
        /// </summary>
        private Coder _coder;

        /// <summary>
        /// 服务器使用的异步Socket
        /// </summary>
        private Socket _serverSocket;
                
        /// <summary>
        /// 服务器程序使用的端口 
        /// </summary>
        private ushort _port;

        /// <summary>
        /// 服务器程序允许的最大客户端连接数 
        /// </summary>
        private ushort _maxClient;
        

        /// <summary>
        /// 服务器的运行状态 
        /// </summary>
        private bool _isRun;

        /// <summary>
        /// 接收数据缓冲区 
        /// </summary>
        private byte[] _recvDataBuffer;

        /// <summary>
        /// 保存所有客户端会话的哈希表 
        /// </summary>
        private Hashtable _sessionTable;

        /// <summary>
        /// 当前的连接的客户端数 
        /// </summary>
        private ushort _clientCount;

        #endregion

        #region ******** 事件定义 ********

        /// <summary>
        /// 客户端建立连接事件
        /// </summary>
        public event NetEvent ClientConnect;

        /// <summary>
        /// 客户端关闭连接事件 
        /// </summary>
        public event NetEvent ClientClose;


        /// <summary>
        /// 服务器已经满事件 
        /// </summary>
        public event NetEvent ServerFull;

        /// <summary>
        /// 服务器接收到数据事件 
        /// </summary>
        public event NetEvent RecvData;

        #endregion

        #region ******** 构造函数 ********

        /// <summary>
        /// 保护的构造函数
        /// </summary>
        protected NetServer()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        /// <param name="encodingMothord">通讯的编码方式</param>
        public NetServer(ushort port, ushort maxClient, Coder coder)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = coder;
        }


        /// <summary>
        /// 构造函数(默认使用Default编码方式)
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        /// <param name="maxClient">服务器能容纳客户端的最大能力</param>
        public NetServer(ushort port, ushort maxClient)
        {
            _port = port;
            _maxClient = maxClient;
            _coder = new Coder(Coder.EncodingMothord.Default);
        }


        /// <summary>
        /// 构造函数(默认使用Default编码方式和DefaultMaxClient(100)个客户端的容量)
        /// </summary>
        /// <param name="port">服务器端监听的端口号</param>
        public NetServer(ushort port)
            : this(port, DefaultMaxClient)
        {
        }

        #endregion

        #region ******** 属性 ********

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
        /// 服务器使用的异步Socket
        /// </summary>
        public Socket ServerSocket
        {
            get
            {
                return _serverSocket;
            }
        }

        /// <summary>
        /// 客户端会话数组,保存所有的客户端,不允许对该数组的内容进行修改
        /// </summary>
        public Hashtable SessionTable
        {
            get
            {
                return _sessionTable;
            }
        }


        /// <summary>
        /// 服务器程序允许的最大客户端连接数
        /// </summary>
        public int MaxClient
        {
            get
            {
                return _maxClient;
            }
        }

        /// <summary>
        /// 当前的连接的客户端数 
        /// </summary>
        public int ClientCount
        {
            get
            {
                return _clientCount;
            }
        }

        /// <summary>
        /// 服务器的运行状态 
        /// </summary>
        public bool IsRun
        {
            get
            {
                return _isRun;
            }

        }
        
        #endregion

        #region ******** 公有的方法 ********

        /// <summary> 
        /// 启动服务器程序,开始监听客户端请求 
        /// </summary> 
        public virtual void Start()
        {
            if (_isRun)
            {
                throw (new ApplicationException("Server已经在运行."));
            }

            _sessionTable = new Hashtable(1024);
            _recvDataBuffer = new byte[DefaultBufferSize];
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //绑定端口 
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, _port);
            _serverSocket.Bind(iep);

            //开始监听 
            _serverSocket.Listen(1024);

            //设置异步方法接受客户端连接 
            _serverSocket.BeginAccept(new AsyncCallback(AcceptConn), _serverSocket);

            _isRun = true;
        }

        /// <summary> 
        /// 停止服务器程序,所有与客户端的连接将关闭 
        /// </summary> 
        public virtual void Stop()
        {
            if (!_isRun)
            {
                throw (new ApplicationException("Server已经停止"));
            }
            _isRun = false;

            //关闭数据连接,负责客户端会认为是强制关闭连接 
            if (_serverSocket.Connected)
            {
                _serverSocket.Shutdown(SocketShutdown.Both);
            }

            CloseAllClient();

            //清理资源 
            _serverSocket.Close();

            _sessionTable = null;

        }

        /// <summary> 
        /// 关闭所有的客户端会话,与所有的客户端连接会断开 
        /// </summary> 
        public virtual void CloseAllClient()
        {
            foreach (Session client in _sessionTable.Values)
            {
                client.Close();
            }
            _sessionTable.Clear();
            _clientCount = 0;
        }

        /// <summary> 
        /// 关闭一个与客户端之间的会话 
        /// </summary> 
        /// <param name="closeClient">需要关闭的客户端会话对象</param> 
        public virtual void CloseSession(Session closeClient)
        {
            Debug.Assert(closeClient != null);

            if (closeClient != null)
            {
                closeClient.Datagram = null;
                _sessionTable.Remove(closeClient.ID);
                _clientCount--;

                //客户端强制关闭链接 
                if (ClientClose != null)
                {
                    ClientClose(this, new NetEventArgs(closeClient));
                }

                closeClient.Close();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="recvDataClient">接收数据的客户端会话</param>
        /// <param name="datagram">数据报文</param>
        public virtual void Send(Session recvDataClient, string datagram)
        {
            //获得数据编码
            byte[] data = _coder.GetEncodingBytes(datagram);

            recvDataClient.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
        }

        public virtual void SendBytesToAll(byte[] dataBytes)
        {
            ArrayList avalues = new ArrayList(this._sessionTable.Keys);//获取客户连接Socket列表
            for (int i = 0; i < avalues.Count; i++)
            {
                string sessionID = avalues[i].ToString();
                Session client = (Session)_sessionTable[new SessionID(int.Parse(sessionID))];
                {
                    SendBytes(client, dataBytes);
                }
            }
        }

        public virtual void SendBytes(Session recvDataClient, byte[] dataBytes)
        {
            //获得报文的编码字节
            byte[] data = _coder.GetEncodingBytes(dataBytes, _resolver);

            recvDataClient.ClientSocket.BeginSend(data, 0, data.Length, SocketFlags.None,
             new AsyncCallback(SendDataEnd), recvDataClient.ClientSocket);
        }

        #endregion

            #region ******** 受保护的方法 ********

            /// <summary> 
            /// 客户端连接处理函数 
            /// </summary> 
            /// <param name="iar">欲建立服务器连接的Socket对象</param> 
        protected virtual void AcceptConn(IAsyncResult iar)
        {
            //如果服务器停止了服务,就不能再接收新的客户端 
            if (!_isRun)
            {
                return;
            }

            //接受一个客户端的连接请求
            Socket oldserver = (Socket)iar.AsyncState;
            Socket clientSocket = oldserver.EndAccept(iar);

            //检查是否达到最大的允许的客户端数目 
            if (_clientCount == _maxClient)
            {
                //服务器已满,发出通知 
                if (ServerFull != null)
                {
                    ServerFull(this, new NetEventArgs(new Session(clientSocket)));
                }
            }
            else
            {
                Session newSession = new Session(clientSocket);
                _sessionTable.Add(newSession.ID, newSession);

                //客户端引用计数+1 
                _clientCount++;

                newSession.RecvDataBuffer = new byte[16 * 1024];
                //开始接受来自该客户端的数据
                clientSocket.BeginReceive(newSession.RecvDataBuffer, 0, newSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), newSession);

                //新的客户段连接,发出通知
                if (ClientConnect != null)
                {
                    ClientConnect(this, new NetEventArgs(newSession));
                }
            }

            //继续接受客户端
            _serverSocket.BeginAccept(new AsyncCallback(AcceptConn), _serverSocket);
        }

        /// <summary>
        /// 接受数据完成处理函数，异步的特性就体现在这个函数中，
        /// 收到数据后，会自动解析为字符串报文
        /// </summary>
        /// <param name="iar">目标客户端Socket</param>
        protected virtual void ReceiveData(IAsyncResult iar)
        {
            Session sendDataSession = (Session)iar.AsyncState;
            Socket client = sendDataSession.ClientSocket;

            try
            {
                //如果两次开始了异步的接收,所以当客户端退出的时候
                //会两次执行EndReceive
                int recv = client.EndReceive(iar);

                if (recv == 0)
                {
                    CloseClient(client, Session.ExitType.NormalExit);
                    return;
                }

                byte [] receiveDataBytes = _coder.GetDecodingBytes(sendDataSession.RecvDataBuffer, recv);

                //string receivedData = _coder.GetEncodingString(sendDataSession.RecvDataBuffer, recv);
                //string receivedData = _coder.GetEncodingString(_recvDataBuffer, recv);

                //发布收到数据的事件
                if (RecvData != null)
                {
                    ICloneable copySession = (ICloneable)sendDataSession;
                    Session clientSession = (Session)copySession.Clone();
                    //clientSession.ClassName = this.GetClassFullName(ref receivedData);
                    //clientSession.Datagram = receivedData;
                    clientSession.RecvDataBuffer = new byte[receiveDataBytes.Length];
                    Array.ConstrainedCopy(receiveDataBytes, 0, clientSession.RecvDataBuffer, 0, receiveDataBytes.Length);

                    RecvData(this, new NetEventArgs(clientSession));

                    //Session sendDataSession = FindSession(client);

                    //Debug.Assert(sendDataSession != null);

                    //if (_resolver != null)
                    //{
                    //    if (sendDataSession.Datagram != null && sendDataSession.Datagram.Length != 0)
                    //    {
                    //        receivedData = sendDataSession.Datagram + receivedData;
                    //    }

                    //    string[] recvDatagrams = _resolver.Resolve(ref receivedData);

                    //    foreach (string newDatagram in recvDatagrams)
                    //    {
                    //        ICloneable copySession = (ICloneable)sendDataSession;
                    //        Session clientSession = (Session)copySession.Clone();

                    //        string strDatagram = newDatagram;
                    //        //clientSession.ClassName = this.GetClassFullName(ref strDatagram);
                    //        clientSession.Datagram = strDatagram;

                    //        //发布一个报文消息
                    //        RecvData(this, new NetEventArgs(clientSession));
                    //    }

                    //    //剩余的代码片断,下次接收的时候使用
                    //    sendDataSession.Datagram = receivedData;

                    //    if (sendDataSession.Datagram.Length > MaxDatagramSize)
                    //    {
                    //        sendDataSession.Datagram = null;
                    //    }
                    //}
                    //else
                    //{
                    //    ICloneable copySession = (ICloneable)sendDataSession;
                    //    Session clientSession = (Session)copySession.Clone();
                    //    //clientSession.ClassName = this.GetClassFullName(ref receivedData);
                    //    clientSession.Datagram = receivedData;

                    //    RecvData(this, new NetEventArgs(clientSession));
                    //}
                }

                //继续接收来自来客户端的数据
                client.BeginReceive(sendDataSession.RecvDataBuffer, 0, sendDataSession.RecvDataBuffer.Length, SocketFlags.None,
                 new AsyncCallback(ReceiveData), sendDataSession);
            }
            catch (SocketException ex)
            {
                if (10054 == ex.ErrorCode)
                {
                    //客户端强制关闭
                    CloseClient(client, Session.ExitType.ExceptionExit);
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

        /// <summary>
        /// 关闭一个客户端Socket,首先需要关闭Session
        /// </summary>
        /// <param name="client">目标Socket对象</param>
        /// <param name="exitType">客户端退出的类型</param>
        protected virtual void CloseClient(Socket client, Session.ExitType exitType)
        {
            Debug.Assert(client != null);

            //查找该客户端是否存在,如果不存在,抛出异常
            Session closeClient = FindSession(client);

            closeClient.TypeOfExit = exitType;

            if (closeClient != null)
            {
                CloseSession(closeClient);
            }
            else
            {
                throw (new ApplicationException("需要关闭的Socket对象不存在"));
            }
        }


        /// <summary> 
        /// 通过Socket对象查找Session对象 
        /// </summary> 
        /// <param name="client"></param> 
        /// <returns>找到的Session对象,如果为null,说明并不存在该回话</returns> 
        private Session FindSession(Socket client)
        {
            SessionID id = new SessionID((int)client.Handle);

            return (Session)_sessionTable[id];
        }

        /// <summary> 
        /// 发送数据完成处理函数 
        /// </summary> 
        /// <param name="iar">目标客户端Socket</param> 
        protected virtual void SendDataEnd(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            int sent = client.EndSend(iar);           
        }

        /// <summary>
        /// 协议解析方法
        /// </summary>
        /// <param name="Term"></param>
        /// <returns></returns>
        private string GetClassFullName(ref string Term)
        {
            //{[object name][channel][request id][|param1|param2|param3|...|]}

            string ClassFullName = Term.Substring(2, Term.IndexOf(']') - 2);
            Term = "{" + Term.Substring(Term.IndexOf(']') + 1);

            return ClassFullName;
        }

        #endregion
    }
}
