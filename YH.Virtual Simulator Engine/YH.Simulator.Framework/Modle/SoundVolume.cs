using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    public enum SoundVolume
    {
        V0,
        V1,
        V2,
        V3,
        V4,
        V5,
        V6,
        V7,
        V8,
        V9,
        V10
    }

    public enum PlaybackMode
    {
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 播放一次
        /// </summary>
        Play,
        /// <summary>
        /// 重复播放
        /// </summary>
        Replay
    }
}
