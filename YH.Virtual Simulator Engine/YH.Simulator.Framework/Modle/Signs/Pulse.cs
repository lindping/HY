using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 脉搏 Pulse
    /// </summary>
    public struct Pulse
    {
        static private Pulse _default;

        static public Pulse Default
        {
            get { return _default; }
        }

        static Pulse()
        {
            _default.BodyPulse = BodyPulse.Default;
            _default.RightArmPulse = RightArmPulse.Default;
            _default.LeftArmPulse = LeftArmPulse.Default;
            _default.RightLegPulse = RightLegPulse.Default;
            _default.LeftLegPulse = LeftLegPulse.Default;
        }

        /// <summary>
        /// 身体脉搏  Body
        /// </summary>
        public BodyPulse BodyPulse;

        /// <summary>
        /// 右臂 Right Arm
        /// </summary>
        public RightArmPulse RightArmPulse;

        /// <summary>
        /// 左臂 Left Arm
        /// </summary>
        public LeftArmPulse LeftArmPulse;

        /// <summary>
        /// 右腿 Right Leg
        /// </summary>
        public RightLegPulse RightLegPulse;

        /// <summary>
        /// 左腿 Left Leg
        /// </summary>
        public LeftLegPulse LeftLegPulse;

    }

    /// <summary>
    /// 身体脉搏  Body
    /// </summary>
    public struct BodyPulse
    {
        static private BodyPulse _default;

        static public BodyPulse Default
        {
            get { return _default; }
        }

        static BodyPulse()
        {
            _default.CarotidRight = Controller.Default;
            _default.CarotidLeft = Controller.Default;
            _default.FemoralRight = Controller.Default;
            _default.FemoralLeft = Controller.Default;
        }

        /// <summary>
        /// 右颈动脉 Carotid Artery  
        /// </summary>
        public Controller CarotidRight;

        /// <summary>
        ///左颈动脉 Carotid Artery  
        /// </summary>
        public Controller CarotidLeft;

        /// <summary>
        /// 右股动脉 Femoral Artery
        /// </summary>
        public Controller FemoralRight;

        /// <summary>
        /// 左股动脉 Femoral Artery
        /// </summary>
        public Controller FemoralLeft;
    }

    /// <summary>
    /// 右臂 Right Arm
    /// </summary>
    public struct RightArmPulse
    {
        static private RightArmPulse _default;

        static public RightArmPulse Default
        {
            get { return _default; }
        }

        static RightArmPulse()
        {
            _default.Brachial = Controller.Default;
            _default.Radial = Controller.Default;
        }

        /// <summary>
        ///肱动脉 Brachial Artery
        /// </summary>
        public Controller Brachial;

        /// <summary>
        /// 桡动脉 Radial Artery
        /// </summary>
        public Controller Radial;
    }

    /// <summary>
    /// 左臂 Left Arm
    /// </summary>
    public struct LeftArmPulse
    {
        static private LeftArmPulse _default;

        static public LeftArmPulse Default
        {
            get { return _default; }
        }

        static LeftArmPulse()
        {
            _default.Brachial = Controller.Default;
            _default.Radial = Controller.Default;
        }

        /// <summary>
        ///肱动脉 Brachial Artery
        /// </summary>
        public Controller Brachial;

        /// <summary>
        /// 桡动脉 Radial Artery
        /// </summary>
        public Controller Radial;
    }

    /// <summary>
    /// 右腿 Right Leg
    /// </summary>
    public struct RightLegPulse
    {
        static private RightLegPulse _default;

        static public RightLegPulse Default
        {
            get { return _default; }
        }

        static RightLegPulse()
        {
            _default.Popliteal = Controller.Default;
            _default.DorsalisPedis = Controller.Default;
            _default.Heel = Controller.Default;
        }

        /// <summary>
        ///腘动脉 Popliteal Artery 
        /// </summary>
        public Controller Popliteal;

        /// <summary>
        /// 足背动脉 Dorsalis Pedis Artery
        /// </summary>
        public Controller DorsalisPedis;

        /// <summary>
        /// 足跟动脉 Heel Artery
        /// </summary>
        public Controller Heel;
    }

    /// <summary>
    /// 左腿 Left Leg
    /// </summary>
    public struct LeftLegPulse
    {
        static private LeftLegPulse _default;

        static public LeftLegPulse Default
        {
            get { return _default; }
        }

        static LeftLegPulse()
        {
            _default.Popliteal = Controller.Default;
            _default.DorsalisPedis = Controller.Default;
            _default.Heel = Controller.Default;
        }

        /// <summary>
        ///腘动脉 Popliteal Artery 
        /// </summary>
        public Controller Popliteal;

        /// <summary>
        /// 足背动脉 Dorsalis Pedis Artery
        /// </summary>
        public Controller DorsalisPedis;

        /// <summary>
        /// 足跟动脉 Heel Artery
        /// </summary>
        public Controller Heel;
    }

    /// <summary>
    /// 脉搏强度
    /// </summary>
    public enum PulseStrength
    {
        /// <summary>
        /// 没有
        /// </summary>
        None,
        /// <summary>
        /// 弱
        /// </summary>
        Weak,
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 强
        /// </summary>
        Strong,
    }
}
