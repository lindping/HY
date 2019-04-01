using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 听诊
    /// </summary>
    public struct Stethoscopy
    {
        static private Stethoscopy _default;

        static public Stethoscopy Default
        {
            get { return _default; }
        }

        static Stethoscopy()
        {
            _default.HeartSounds = HeartSounds.Default;
            _default.LungSounds = LungSounds.Default;
            _default.AbdomenSounds = AbdomenSounds.Default;
        }

        /// <summary>
        /// 心音
        /// </summary>
        public HeartSounds HeartSounds;

        /// <summary>
        /// 肺音
        /// </summary>
        public LungSounds LungSounds;

        /// <summary>
        /// 腹部声音
        /// </summary>
        public AbdomenSounds AbdomenSounds;
    }

    /// <summary>
    /// 心音
    /// </summary>
    public struct HeartSounds
    {
        static private HeartSounds _default;

        static public HeartSounds Default
        {
            get { return _default; }
        }

        static HeartSounds()
        {
            _default.M = BodySound.Default;
            _default.T = BodySound.Default;
            _default.A = BodySound.Default;
            _default.P = BodySound.Default;
            _default.E = BodySound.Default;
        }

        /// <summary>
        /// 二尖瓣区 M Mitral area
        /// </summary>
        public BodySound M;

        /// <summary>
        /// 三尖瓣区 T Tricuspid area
        /// </summary>
        public BodySound T;

        /// <summary>
        /// 主动脉瓣区 A Aortic area
        /// </summary>
        public BodySound A;

        /// <summary>
        /// 肺动脉瓣区 P Pulmonary area
        /// </summary>
        public BodySound P;

        /// <summary>
        /// 主动脉瓣第二听诊区 E Erb
        /// </summary>
        public BodySound E;


    }
        
    /// <summary>
    /// 肺音
    /// </summary>
    public struct LungSounds
    {
        static private LungSounds _default;

        static public LungSounds Default
        {
            get { return _default; }
        }

        static LungSounds()
        {
            _default.ARUL = BodySound.Default;
            _default.ARML = BodySound.Default;
            _default.ARLL = BodySound.Default;
            _default.ALUL = BodySound.Default;
            _default.ALLL = BodySound.Default;

            _default.PLUL = BodySound.Default;
            _default.PLLL = BodySound.Default;
            _default.PRUL = BodySound.Default;
            _default.PRLL = BodySound.Default;
        }

        /// <summary>
        /// 前右上叶 ARUL anterior Right upper lobe
        /// </summary>
        public BodySound ARUL;

        /// <summary>
        /// 前右中叶 ARML anterior Right middle lobe
        /// </summary>
        public BodySound ARML;

        /// <summary>
        /// 前右下叶 ARLL anterior Right lower lobe
        /// </summary>
        public BodySound ARLL;

        /// <summary>
        /// 前左上叶 ALUL anterior Left upper lobe
        /// </summary>
        public BodySound ALUL;

        /// <summary>
        /// 前左下叶 ALLL anterior Left lower lobe
        /// </summary>
        public BodySound ALLL;

        /// <summary>
        /// 背左上叶 PLUL posterior Left upper lobe
        /// </summary>
        public BodySound PLUL;

        /// <summary>
        /// 背左下叶 PLLL posterior Left lower lobe
        /// </summary>
        public BodySound PLLL;

        /// <summary>
        /// 背右上叶 PRUL posterior Right upper lobe
        /// </summary>
        public BodySound PRUL;

        /// <summary>
        /// 背右下叶 PRLL posterior Right lower lobe
        /// </summary>
        public BodySound PRLL;

    }

    /// <summary>
    /// 腹部声音
    /// </summary>
    public struct AbdomenSounds
    {
        static private AbdomenSounds _default;

        static public AbdomenSounds Default
        {
            get { return _default; }
        }

        static AbdomenSounds()
        {
            _default.Bowel = BodySound.Default;
            _default.Vascular = BodySound.Default;                  
        }

        /// <summary>
        /// 肠鸣音 Bowel Sounds
        /// </summary>
        public BodySound Bowel;

        /// <summary>
        /// 血管音 Vascular Sounds
        /// </summary>
        public BodySound Vascular;
    }

    /// <summary>
    /// 身体声音 BodySounds
    /// </summary>
    public struct BodySound
    {
        static private BodySound _default;

        static public BodySound Default
        {
            get { return _default; }
        }

        static BodySound()
        {
            _default.SoundID = 0;
            _default.SoundVolume = SoundVolume.V0;
        }

        public int SoundID;
        public SoundVolume SoundVolume;
    }
}
