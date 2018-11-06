using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    public struct ECG
    {
        static private ECG _default;

        static public ECG Default
        {
            get { return _default; }
        }

        static ECG()
        {
            _default.LeadLine = LeadLine.Default;

        }
        /// <summary>
        /// 拍打 Pat
        /// </summary>
        public LeadLine LeadLine;
               
    }

    /// <summary>
    /// 导联线 LeadLine
    /// </summary>
    public struct LeadLine
    {
        static private LeadLine _default;

        static public LeadLine Default
        {
            get { return _default; }
        }

        static LeadLine()
        {
            _default.LeadLine_ECG = LeadLine_ECG.Default;
            _default.LeadLine_SPO2 = LeadLine_SPO2.Default;
            _default.LeadLine_NIBP = LeadLine_NIBP.Default;
            _default.LeadLine_Temperature = LeadLine_Temperature.Default;
        }

        /// <summary>
        /// ECG导联线
        /// </summary>
        public LeadLine_ECG LeadLine_ECG;

        /// <summary>
        /// 血氧饱和度导联线 SpO2 
        /// </summary>
        public LeadLine_SPO2 LeadLine_SPO2;

        /// <summary>
        /// 无创血压导联线 NIBP
        /// </summary>
        public LeadLine_NIBP LeadLine_NIBP;

        /// <summary>
        /// 体温导联线 Temperature
        /// </summary>
        public LeadLine_Temperature LeadLine_Temperature;


    }

    /// <summary>
    /// ECG导联线
    /// </summary>
    public struct LeadLine_ECG
    {
        static private LeadLine_ECG _default;

        static public LeadLine_ECG Default
        {
            get { return _default; }
        }

        static LeadLine_ECG()
        {
            _default.LeadLine_ECG5 = LeadLine_ECG5.Default;
            _default.LeadLine_ECG5 = LeadLine_ECG5.Default;
        }

        /// <summary>
        /// ECG5导联
        /// </summary>
        public LeadLine_ECG5 LeadLine_ECG5;

        /// <summary>
        /// ECG12导联 V1-V6
        /// </summary>
        public LeadLine_ECG12 LeadLine_ECG12;

    }

    public struct LeadLine_ECG5
    {
        static private LeadLine_ECG5 _default;

        static public LeadLine_ECG5 Default
        {
            get { return _default; }
        }

        static LeadLine_ECG5()
        {
            _default.RA = Operator.Default;
            _default.RL = Operator.Default;
            _default.LA = Operator.Default;
            _default.LL = Operator.Default;
            _default.V0 = Operator.Default;
        }

        /// <summary>
        ///  Right Arm
        /// </summary>
        public Operator RA;

        /// <summary>
        ///  Left Arm
        /// </summary>
        public Operator LA;

        /// <summary>
        ///  Right Leg
        /// </summary>
        public Operator RL;

        /// <summary>
        ///  Left Leg
        /// </summary>
        public Operator LL;

        /// <summary>
        /// V0   -   5导联-V
        /// </summary>
        public Operator V0;
    }

    public struct LeadLine_ECG12
    {
        static private LeadLine_ECG12 _default;

        static public LeadLine_ECG12 Default
        {
            get { return _default; }
        }

        static LeadLine_ECG12()
        {
            _default.V1 = Operator.Default;
            _default.V2 = Operator.Default;
            _default.V3 = Operator.Default;
            _default.V4 = Operator.Default;
            _default.V5 = Operator.Default;
            _default.V6 = Operator.Default;
        }

        /// <summary>
        /// V1   -   12导联-V1
        /// </summary>
        public Operator V1;

        /// <summary>
        /// V2   -   12导联-V2
        /// </summary>
        public Operator V2;

        /// <summary>
        /// V3   -   12导联-V3
        /// </summary>
        public Operator V3;

        /// <summary>
        /// V4   -   12导联-V4
        /// </summary>
        public Operator V4;

        /// <summary>
        /// V5   -   12导联-V5
        /// </summary>
        public Operator V5;

        /// <summary>
        /// V6   -   12导联-V6
        /// </summary>
        public Operator V6;
    }

    /// <summary>
    /// 
    /// </summary>
    public struct LeadLine_SPO2
    {
        static private LeadLine_SPO2 _default;

        static public LeadLine_SPO2 Default
        {
            get { return _default; }
        }

        static LeadLine_SPO2()
        {
            _default.SPO2 = Operator.Default;
        }

        /// <summary>
        ///  血氧饱和度
        /// </summary>
        public Operator SPO2;
    }

    public struct LeadLine_NIBP
    {
        static private LeadLine_NIBP _default;

        static public LeadLine_NIBP Default
        {
            get { return _default; }
        }

        static LeadLine_NIBP()
        {
            _default.NIBP = Operator.Default;
        }

        /// <summary>
        ///  Non-Invasive Blood Pressure
        /// </summary>
        public Operator NIBP;
    }

    public struct LeadLine_Temperature
    {
        static private LeadLine_Temperature _default;

        static public LeadLine_Temperature Default
        {
            get { return _default; }
        }

        static LeadLine_Temperature()
        {
            _default.PeripheralTemperature = Operator.Default;
            _default.BloodTemperature = Operator.Default;
        }

        /// <summary>
        ///  体温1 PeripheralTemperature
        /// </summary>
        public Operator PeripheralTemperature;

        /// <summary>
        /// 体温2 BloodTemperature
        /// </summary>
        public Operator BloodTemperature;
    }
}
