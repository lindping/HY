using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YH.Library
{
    /// <summary>
    /// 消息侦听器
    /// </summary>
    public class MessageListener : DependencyObject
    {
        /// <summary>
        /// 消息侦听器实例
        /// </summary>
        private static MessageListener mInstance;

        /// <summary>
        /// 
        /// </summary>
        private MessageListener()
        {

        }

        /// <summary>
        /// 获取消息侦听器实例
        /// </summary>
        public static MessageListener Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new MessageListener();
                return mInstance;
            }
        }

        #region 标题

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        public void SetTitle(string title)
        {
            Title = title;
            Debug.WriteLine(Title);
            DispatcherHelper.DoEvents();
        }

        /// <summary>
        /// 标题属性
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// 标题源属性
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));

        #endregion

        #region 消息

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SetMessage(string message)
        {
            Message = message;
            Debug.WriteLine(Message);
            DispatcherHelper.DoEvents();
        }

        /// <summary>
        /// Get or set received message
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// 消息源属性
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));

        #endregion

        #region 版权信息

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SetBottomMessage(string systemName,string copyrigytAll)
        {
            SystemName = systemName;
            CopyrigytAll = copyrigytAll;
            Debug.WriteLine(Message);
            DispatcherHelper.DoEvents();
        }

        /// <summary>
        /// Get or set received message
        /// </summary>
        public string SystemName
        {
            get { return (string)GetValue(SystemNameProperty); }
            set { SetValue(SystemNameProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty SystemNameProperty =
            DependencyProperty.Register("SystemName", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));

        public string CopyrigytAll
        {
            get { return (string)GetValue(CopyrigytAllProperty); }
            set { SetValue(CopyrigytAllProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CopyrigytAllProperty =
            DependencyProperty.Register("CopyrigytAll", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));

        #endregion

    }
}
