using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 脉搏检查 Pulse Check
    /// </summary>
    public struct CheckPulse
    {
        static private CheckPulse _default;

        static public CheckPulse Default
        {
            get { return _default; }
        }

        static CheckPulse()
        {
            _default.CheckBodyPulse = CheckBodyPulse.Default;
            _default.CheckRightArmPulse = CheckRightArmPulse.Default;
            _default.CheckLeftArmPulse = CheckLeftArmPulse.Default;
            _default.CheckRightLegPulse = CheckRightLegPulse.Default;
            _default.CheckLeftLegPulse = CheckLeftLegPulse.Default;
        }

        /// <summary>
        /// 身体脉搏检查  Body
        /// </summary>
        public CheckBodyPulse CheckBodyPulse;

        /// <summary>
        /// 右臂脉搏检查 Right Arm
        /// </summary>
        public CheckRightArmPulse CheckRightArmPulse;

        /// <summary>
        /// 左臂脉搏检查 Left Arm
        /// </summary>
        public CheckLeftArmPulse CheckLeftArmPulse;

        /// <summary>
        /// 右腿脉搏检查 Right Leg
        /// </summary>
        public CheckRightLegPulse CheckRightLegPulse;

        /// <summary>
        /// 左腿脉搏检查 Left Leg
        /// </summary>
        public CheckLeftLegPulse CheckLeftLegPulse;
    }

    /// <summary>
    /// 身体脉搏  Body
    /// </summary>
    public struct CheckBodyPulse
    {
        static private CheckBodyPulse _default;

        static public CheckBodyPulse Default
        {
            get { return _default; }
        }

        static CheckBodyPulse()
        {
            _default.Carotid_Right = Operator.Default;
            _default.Carotid_Left = Operator.Default;
            _default.Femoral_Right = Operator.Default;
            _default.Femoral_Left = Operator.Default;
        }

        /// <summary>
        ///右颈动脉 Carotid Artery  
        /// </summary>
        public Operator Carotid_Right;

        /// <summary>
        ///左颈动脉 Carotid Artery  
        /// </summary>
        public Operator Carotid_Left;

        /// <summary>
        /// 右股动脉 Femoral Artery
        /// </summary>
        public Operator Femoral_Right;

        /// <summary>
        /// 左股动脉 Femoral Artery
        /// </summary>
        public Operator Femoral_Left;
    }

    /// <summary>
    /// 右臂 Right Arm
    /// </summary>
    public struct CheckRightArmPulse
    {
        static private CheckRightArmPulse _default;

        static public CheckRightArmPulse Default
        {
            get { return _default; }
        }

        static CheckRightArmPulse()
        {
            _default.Brachial = Operator.Default;
            _default.Radial = Operator.Default;
        }

        /// <summary>
        ///肱动脉 Brachial Artery
        /// </summary>
        public Operator Brachial;

        /// <summary>
        /// 桡动脉 Radial Artery
        /// </summary>
        public Operator Radial;
    }

    /// <summary>
    /// 左臂 Left Arm
    /// </summary>
    public struct CheckLeftArmPulse
    {
        static private CheckLeftArmPulse _default;

        static public CheckLeftArmPulse Default
        {
            get { return _default; }
        }

        static CheckLeftArmPulse()
        {
            _default.Brachial = Operator.Default;
            _default.Radial = Operator.Default;
        }

        /// <summary>
        ///肱动脉 Brachial Artery
        /// </summary>
        public Operator Brachial;

        /// <summary>
        /// 桡动脉 Radial Artery
        /// </summary>
        public Operator Radial;
    }

    /// <summary>
    /// 右腿 Right Leg
    /// </summary>
    public struct CheckRightLegPulse
    {
        static private CheckRightLegPulse _default;

        static public CheckRightLegPulse Default
        {
            get { return _default; }
        }

        static CheckRightLegPulse()
        {
            _default.Popliteal = Operator.Default;
            _default.DorsalisPedis = Operator.Default;
            _default.Heel = Operator.Default;
        }

        /// <summary>
        ///腘动脉 Popliteal Artery 
        /// </summary>
        public Operator Popliteal;

        /// <summary>
        /// 足背动脉 Dorsalis Pedis Artery
        /// </summary>
        public Operator DorsalisPedis;

        /// <summary>
        /// 足跟动脉 Heel Artery
        /// </summary>
        public Operator Heel;
    }

    /// <summary>
    /// 左腿 Left Leg
    /// </summary>
    public struct CheckLeftLegPulse
    {
        static private CheckLeftLegPulse _default;

        static public CheckLeftLegPulse Default
        {
            get { return _default; }
        }

        static CheckLeftLegPulse()
        {
            _default.Popliteal = Operator.Default;
            _default.DorsalisPedis = Operator.Default;
            _default.Heel = Operator.Default;
        }

        /// <summary>
        ///腘动脉 Popliteal Artery 
        /// </summary>
        public Operator Popliteal;

        /// <summary>
        /// 足背动脉 Dorsalis Pedis Artery
        /// </summary>
        public Operator DorsalisPedis;

        /// <summary>
        /// 足跟动脉 Heel Artery
        /// </summary>
        public Operator Heel;
    }
    
}
