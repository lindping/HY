using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Loading
{
    public static class Loader
    {
        private static LoadWindow mLoad;

        public static LoadWindow Load
        {
            get
            {
                return mLoad;
            }
            set
            {
                mLoad = value;
            }
        }

        /// <summary>
        /// 显示Splash
        /// </summary>
        public static void ShowLoad()
        {
            if (mLoad != null)
            {
                mLoad.Show();
            }
        }

        /// <summary>
        /// 关闭Splash
        /// </summary>
        public static void CloseLoad()
        {
            if (mLoad != null)
            {
                mLoad.Close();

                if (mLoad is IDisposable)
                    (mLoad as IDisposable).Dispose();
            }
        }
    }
}
