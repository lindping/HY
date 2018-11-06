using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 模拟语音 Analog Voice
    /// </summary>
    public struct AnalogVocal
    {
        static private AnalogVocal _default;

        static public AnalogVocal Default
        {
            get { return _default; }
        }

        static AnalogVocal()
        {
            _default.Vocal = Vocal.Default;
            _default.Voice = Voice.Default;
        }

        /// <summary>
        /// 发声
        /// </summary>
        public Vocal Vocal;

        /// <summary>
        /// 语音
        /// </summary>
        public Voice Voice;
    }

    /// <summary>
    /// 发声
    /// </summary>
    public struct Vocal
    {
        static private Vocal _default;

        static public Vocal Default
        {
            get { return _default; }
        }

        static Vocal()
        {
            _default.SoundID = 0;
            _default.PlaybackMode = PlaybackMode.Stop;
            _default.SoundVolume = SoundVolume.V0;
        }

        /// <summary>
        /// 发声声音
        /// </summary>
        public int SoundID;

        /// <summary>
        /// 播放方式 Playback Mode
        /// </summary>
        public PlaybackMode PlaybackMode;

        /// <summary>
        /// 声音音量
        /// </summary>
        public SoundVolume SoundVolume;
    }

    /// <summary>
    /// 语音
    /// </summary>
    public struct Voice
    {
        static private Voice _default;

        static public Voice Default
        {
            get { return _default; }
        }

        static Voice()
        {
            _default.SoundID = 0;
            _default.PlaybackMode = PlaybackMode.Stop;
            _default.SoundVolume = SoundVolume.V0;
        }

        /// <summary>
        /// 发声声音
        /// </summary>
        public int SoundID;

        /// <summary>
        /// 播放方式 Playback Mode
        /// </summary>
        public PlaybackMode PlaybackMode;

        /// <summary>
        /// 声音音量
        /// </summary>
        public SoundVolume SoundVolume;
    }
}
