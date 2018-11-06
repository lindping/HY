using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 眼睛
    /// </summary>
    public struct Eyes
    {
        static private  Eyes _default;

        static public  Eyes Default
        {
            get { return _default; }
        }

        static Eyes()
        {
            _default.Eyelid = Eyelid.Default;
            _default.Blinking = Blinking.Default;
            _default.Pupill = Pupill.Default;
            _default.Light = Light.Default;
                        
        }

        /// <summary>
        /// 眼脸状态
        /// </summary>
        public Eyelid Eyelid;

        /// <summary>
        /// 眨眼速度
        /// </summary>
        public Blinking Blinking;

        /// <summary>
        /// 瞳孔大小
        /// </summary>
        public Pupill Pupill;

        /// <summary>
        /// 对光反射
        /// </summary>
        public Light Light;


    }

    /// <summary>
    /// 眼脸
    /// </summary>
    public struct Eyelid
    {
        static private Eyelid _default;

        static public Eyelid Default
        {
            get { return _default; }
        }

        static Eyelid()
        {
            _default.Right =  EyelidStatus.WideOpen;
            _default.Left = EyelidStatus.WideOpen;

        }

        /// <summary>
        /// 
        /// </summary>
        public EyelidStatus Right;

        /// <summary>
        /// 
        /// </summary>
        public EyelidStatus Left;
    }

    /// <summary>
    /// 眼脸状态
    /// </summary>
    public enum EyelidStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 全开
        /// </summary>
        WideOpen = 1,
        /// <summary>
        /// 半开
        /// </summary>
        HalfOpen = 2,
        /// <summary>
        /// 关闭
        /// </summary>
        Closed = 3,

    }

    /// <summary>
    /// 眨眼
    /// </summary>
    public struct Blinking
    {
        static private Blinking _default;

        static public Blinking Default
        {
            get { return _default; }
        }

        static Blinking()
        {
            _default.Right = BlinkingSpeed.Normal;
            _default.Left = BlinkingSpeed.Normal;

        }
        public BlinkingSpeed Right;
        public BlinkingSpeed Left;
    }

    /// <summary>
    /// 眨眼速度
    /// </summary>
    public enum BlinkingSpeed
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 频密
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 正常
        /// </summary>
        Infrequently = 2,
        /// <summary>
        /// 不频密
        /// </summary>
        Frequently = 3,
    }

    /// <summary>
    /// 瞳孔
    /// </summary>
    public struct Pupill
    {
        static private Pupill _default;

        static public Pupill Default
        {
            get { return _default; }
        }

        static Pupill()
        {
            _default.Right = PupillSize.Normal;
            _default.Left = PupillSize.Normal;

        }
        public PupillSize Right;
        public PupillSize Left;
    }

    /// <summary>
    /// 瞳孔大小
    /// </summary>
    public enum PupillSize
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 缩小
        /// </summary>
        Narrow = 2,
        /// <summary>
        /// 散大
        /// </summary>
        Loose = 3,
    }

    /// <summary>
    /// 对光反射
    /// </summary>
    public struct Light
    {
        static private Light _default;

        static public Light Default
        {
            get { return _default; }
        }

        static Light()
        {
            _default.Right = LightSensitivity.Normal;
            _default.Left = LightSensitivity.Normal;

        }
        public LightSensitivity Right;
        public LightSensitivity Left;
    }

    /// <summary>
    /// 对光灵敏度
    /// </summary>
    public enum LightSensitivity
    {

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 缓慢
        /// </summary>
        Slow = 2,
        /// <summary>
        /// 没有
        /// </summary>
        None = 3,
    }


}
