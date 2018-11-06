using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YH.Library
{
    /********************************************************************************
     ** 类名称： Launch
     ** 描述：线程执行启动器
     ** 作者： john liao
     ** 创建时间：2018-7-10
     ** 最后修改人：
     ** 最后修改时间：
     ** 版权所有 (C) :HY
    *********************************************************************************/

    /// <summary>
    /// 模块编号：       【模块编号，可以引用系统设计中的模块编号】
    /// 作用：线程执行启动器      【对此类的描述，可以引用系统设计中的描述】
    /// 主要功能：
    ///         1、启动器心跳设定、执行； 
    /// 作者：  john liao         【作者中文名】
    /// 编写日期：2018-7-10       【模块创建日期，格式：YYYY-MM-DD】
    /// 如果模块有修改，则每次修改必须添加以下注释：
    /// Log编号：        【LOG< SPAN>编号,从1开始一次增加】
    /// 修改描述：       【对此修改的描述】
    /// 作者：           【修改者中文名】
    /// 修改日期：       【模块修改日期，格式：YYYY-MM-DD】
    /// </summary>
    public class Launch
    {

        Thread WorkThread;
        AutoResetEvent WorkResetEvent = new AutoResetEvent(true);

        public delegate void OnElapsedEventDelegate();
        public event OnElapsedEventDelegate OnElapsed;

        #region *** Field ***

        private int _interval = 1000;      //时间间隔 默认1000ms

        private bool _isRun = false;
        private bool _isPause = false;

        #endregion

        #region *** Constructor *** 

        public Launch()
        {
            CreateWork();
        }

        public Launch(int Interval)
            : this()
        {
            _interval = Interval;
        }

        #endregion

        #region *** Destructor ***

        ~Launch()
        {
            DestroyWork();
        }

        #endregion

        #region *** Property ***

        /// <summary>
        /// 时间间隔 默认1000ms
        /// </summary>
        public int Interval
        {
            set { _interval = value; }
            get { return _interval; }
        }

        #endregion

        #region *** Method *** 

        #region *** Public Method ***

        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            this._isRun = true;
            this._isPause = false;
            this.WorkResetEvent.Set();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            this._isPause = true;
            this.WorkResetEvent.Reset();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            this._isRun = false;
            this._isPause = false;
            this.WorkResetEvent.Set();
        }

        #endregion

        #region *** Private Method ***

        /// <summary>
        /// 创建启动器线程
        /// </summary>
        private void CreateWork()
        {
            this.WorkThread = new Thread(new ThreadStart(this.WorkThreedFun));
            this.WorkThread.IsBackground = true;
            this.WorkThread.Start();
        }

        /// <summary>
        /// 销毁启动器线程
        /// </summary>
        private void DestroyWork()
        {
            _isRun = false;
            //Thread.Sleep(100);
            WorkThread.DisableComObjectEagerCleanup();
        }

        /// <summary>
        /// 执行线程函数
        /// </summary>
        private void WorkThreedFun()
        {
            while (true)
            {
                if (!_isRun || _isPause)
                {
                    this.WorkResetEvent.WaitOne();
                }
                this.DoWork();

                System.Threading.Thread.Sleep(_interval);
            }
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        private void DoWork()
        {
            if (OnElapsed != null)
            {
                OnElapsed();
            }
        }

        #endregion

        #endregion
    }
}
