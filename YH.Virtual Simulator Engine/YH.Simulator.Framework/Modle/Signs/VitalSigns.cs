using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{

    /// <summary>
    /// 基本生命体征 Basic Vital Signs
    /// </summary>
    public struct VitalSigns
    {
        static private VitalSigns _default;

        static public VitalSigns Default
        {
            get { return _default; }
        }

        static VitalSigns()
        {
            _default.Cyclic = Cyclic.Default;
            _default.Breath = Breath.Default;
            _default.Other = Other.Default;
        }

        /// <summary>
        /// 生命体征-循环 Cyclic
        /// </summary>
        public Cyclic Cyclic;

        /// <summary>
        ///  生命体征-呼吸 Breath
        /// </summary>
        public Breath Breath;

        /// <summary>
        /// 生命体征-其它 Other
        /// </summary>
        public Other Other;
    }

    #region 循环 Cyclic

    /// <summary>
    /// 生命体征-循环 Cyclic
    /// </summary>
    public struct Cyclic
    { 
        static private Cyclic _default;

        static public Cyclic Default
        {
            get { return _default; }
        }

        static Cyclic()
        {
            _default.Rhythm = Rhythm.Default;
            _default.HeartRate = DataValue.Default; 
            _default.SpO2 = DataValue.Default; 
            _default.IBP = IBP.Default;
            _default.PAP = PAP.Default;
            _default.CVP = DataValue.Default; 
            _default.PAWP = DataValue.Default; 
            _default.C_O_ = DataValue.Default; 

        }

        /// <summary>
        /// 心律
        /// </summary>
        public Rhythm Rhythm;

        /// <summary>
        /// 心率 
        /// </summary>
        public DataValue HeartRate;

        /// <summary>
        /// 血氧饱和度
        /// </summary>
        public DataValue SpO2;

        /// <summary>
        /// 有创血压 
        /// </summary>
        public IBP IBP;

        /// <summary>
        /// 肺动脉压
        /// </summary>
        public PAP PAP;

        /// <summary>
        /// 中心静脉压
        /// </summary>
        public DataValue CVP;

        /// <summary>
        /// 肺毛压
        /// </summary>
        public DataValue PAWP;

        /// <summary>
        /// 心输出量
        /// </summary>
        public DataValue C_O_;
    }

    public struct Rhythm
    {
        static private Rhythm _default;

        static public Rhythm Default
        {
            get { return _default; }
        }

        static Rhythm()
        {
            _default.Basic = 0;
            _default.Extrasystole = 0;
        }

        /// <summary>
        /// 基本心律 
        /// </summary>
        public int Basic;

        /// <summary>
        /// 期前收缩
        /// </summary>
        public int Extrasystole;
    }

    /// <summary>
    /// 有创血压
    /// </summary>
    public struct IBP
    {

        static private IBP _default;

        static public IBP Default
        {
            get { return _default; }
        }

        static IBP()
        {
            _default.Systolic = DataValue.Default; 
            _default.Diastolic = DataValue.Default; 
        }

        /// <summary>
        /// 收缩压
        /// </summary>
        public DataValue Systolic;

        /// <summary>
        /// 舒张压
        /// </summary>
        public DataValue Diastolic;
    }

    /// <summary>
    /// 肺动脉压
    /// </summary>
    public struct PAP
    {

        static private PAP _default;

        static public PAP Default
        {
            get { return _default; }
        }

        static PAP()
        {
            _default.Systolic = DataValue.Default; 
            _default.Diastolic = DataValue.Default; 
        }

        /// <summary>
        /// 收缩压
        /// </summary>
        public DataValue Systolic;

        /// <summary>
        /// 舒张压
        /// </summary>
        public DataValue Diastolic;
    }

    #endregion

    #region 呼吸 Breath

    /// <summary>
    /// 生命体征-呼吸 Breath
    /// </summary>
    public struct Breath
    {
        static private Breath _default;

        static public Breath Default
        {
            get { return _default; }
        }

        static Breath()
        {
            _default.RespType = 0;
            _default.RespRate = DataValue.Default;
            _default.InspiratoryCapacity = DataValue.Default; 
            _default.RespRatio = DataValue.Default; 
            _default.CO2 = DataValue.Default;
            _default.ETCO2 = DataValue.Default;
            _default.O2 = O2.Default;
            _default.N2O = N2O.Default; 
            _default.AGT = AGT.Default; 

        }
        /// <summary>
        /// 呼吸类型
        /// </summary>
        public int RespType;

        /// <summary>
        /// 呼吸率
        /// </summary>
        public DataValue RespRate;

        /// <summary>
        /// 吸气量 Inspiratory Capacity
        /// </summary>
        public DataValue InspiratoryCapacity;

        /// <summary>
        /// 呼吸比 Respiration ratio
        /// </summary>
        public DataValue RespRatio;

        /// <summary>
        /// 二氧化碳
        /// </summary>
        public DataValue CO2;

        /// <summary>
        /// 呼气末二氧化碳
        /// </summary>
        public DataValue ETCO2;

        /// <summary>
        /// 氧气
        /// </summary>
        public O2 O2;

        /// <summary>
        /// 笑气
        /// </summary>
        public N2O N2O;

        /// <summary>
        /// 麻醉剂
        /// </summary>
        public AGT AGT;
    }

    public struct O2
    {
        static private O2 _default;

        static public O2 Default
        {
            get { return _default; }
        }

        static O2()
        {
            _default.inO2 = DataValue.Default;
            _default.exO2 = DataValue.Default;
        }

        /// <summary>
        /// 吸入的氧气 Inhaled Oxygen
        /// </summary>
        public DataValue inO2;

        /// <summary>
        /// 呼出的氧气 Exhaled Oxygen
        /// </summary>
        public DataValue exO2;
    }

    public struct N2O
    {
        static private N2O _default;

        static public N2O Default
        {
            get { return _default; }
        }

        static N2O()
        {
            _default.inN2O = DataValue.Default;
            _default.exN2O = DataValue.Default;

        }

        /// <summary>
        /// 吸入的笑气 Inhaled Nitrous Oxide
        /// </summary>
        public DataValue inN2O;

        /// <summary>
        /// 呼出的笑气 Exhaled Nitrous Oxide
        /// </summary>
        public DataValue exN2O;
    }

    public struct AGT
    {
        static private AGT _default;

        static public AGT Default
        {
            get { return _default; }
        }

        static AGT()
        {
            _default.inAGT = DataValue.Default ;
            _default.exAGT = DataValue.Default;
        }

        /// <summary>
        /// 吸入的麻醉剂 Inhaled Anesthetic Agent
        /// </summary>
        public DataValue inAGT;

        /// <summary>
        /// 呼出的麻醉剂Exhaled Anesthetic Agent
        /// </summary>
        public DataValue exAGT;
    }

    #endregion

    #region 其它 Other

    /// <summary>
    /// 生命体征-其它 Other
    /// </summary>
    public struct Other
    {
        static private Other _default;

        static public Other Default
        {
            get { return _default; }
        }

        static Other()
        {
            _default.PeripheralTemperature = DataValue.Default;
            _default.BloodTemperature = DataValue.Default;
            _default.pH = DataValue.Default;
            _default.ICP = DataValue.Default;
            _default.TOF = TOF.Default;
            _default.PTC = DataValue.Default;

        }

        /// <summary>
        /// 外周温度
        /// </summary>
        public DataValue PeripheralTemperature;

        /// <summary>
        /// 血液温度
        /// </summary>
        public DataValue BloodTemperature;

        /// <summary>
        /// pH
        /// </summary>
        public DataValue pH;

        /// <summary>
        /// 颅内压
        /// </summary>
        public DataValue ICP;

        /// <summary>
        /// Train-of-four
        /// </summary>
        public TOF TOF;
        
        /// <summary>
        /// 强直后计数 Posttetanic count
        /// </summary>
        public DataValue PTC;

    }

    public struct TOF
    {
        static private TOF _default;

        static public TOF Default
        {
            get { return _default; }
        }

        static TOF()
        {
            _default.Numerical = DataValue.Default;
            _default.Ratio = DataValue.Default;

        }
        /// <summary>
        /// Train-of-four
        /// </summary>
        public DataValue Numerical;

        /// <summary>
        /// TOF比例 Train-of-four Ratio
        /// </summary>
        public DataValue Ratio;
    }

    #endregion

}
