using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_ECG_Monitor
{
    public class ECGSettingModel
    {
        /// <summary>
        /// 导联
        /// </summary>
        public int Lead { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public float Speed { get; set; }

        /// <summary>
        /// QRS音量
        /// </summary>
        public int QRSVolumn { get; set; }

        /// <summary>
        /// 增益
        /// </summary>
        public int Gain { get; set; }

        /// <summary>
        /// 心电图开关
        /// </summary>
        public bool ECGSwitch { get; set; }

        public double Max { get; set; }
        public double Min { get; set; }
        public bool Warning { get; set; }
        public string Level { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public interface ISettingData
    {
        void SetDefault();

    }

    public class ECGSettingData: ISettingData
    {
        private ECGSettingModel _custom;
        public ECGSettingModel Custom
        {
            get
            {
                if (_custom == null && Default != null)
                {
                    _custom = (ECGSettingModel)Default.Clone();
                }
                return _custom;
            }
            set
            {
                _custom = value;
            }
        }
        public ECGSettingModel Default { get; set; }

        public void SetDefault()
        {

        }

    }

    public class PatientInfoModel
    {
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
        public string MedRecNo { get; set; }
        public string BedNo { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class PatientInfoData:ISettingData
    {
        private PatientInfoModel _custom;
        public PatientInfoModel Custom
        {
            get
            {
                if (_custom == null && Default != null)
                {
                    _custom = (PatientInfoModel)Default.Clone();
                }
                return _custom;
            }
            set
            {
                _custom = value;
            }
        }
        public PatientInfoModel Default { get; set; }

        public void SetDefault()
        {

        }

    }

    public class OtherSettingModel
    {
        public string DefaultLayout { get; set; }
        public int AlartVolumn { get; set; }
        public bool NIBP { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class OtherSettingData:ISettingData
    {
        private OtherSettingModel _custom;
        public OtherSettingModel Custom
        {
            get
            {
                if (_custom == null && Default != null)
                {
                    _custom = (OtherSettingModel)Default.Clone();
                }
                return _custom;
            }
            set
            {
                _custom = value;
            }
        }
        public OtherSettingModel Default { get; set; }
        public void SetDefault()
        {

        }
    }

    public class LayoutWave
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class LayoutSettingModel
    {
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public List<LayoutWave> MainWaveCategories { get; set; }
        public List<LayoutWave> OtherWaveCategories { get; set; }
        public LayoutWave NIBPWaveCategory { get; set; }

        /// <summary>
        /// 版面布局,只有0-3的4种
        /// </summary>
        public int GridModel { get; set; }

        public LayoutSettingModel Clone()
        {
            return this.MemberwiseClone() as LayoutSettingModel;
        }

    }

    public class LayoutSettingData:ISettingData
    {
        public List<LayoutSettingModel> Layouts { get; set; }
        public void SetDefault()
        {

        }
    }

    public class VirtualManAttributeItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string unit { get; set; }

    
    }

    /// <summary>
    /// 虚拟人体征
    /// </summary>
    public class VirtualManAttributeModel
    {
        public VirtualManAttributeItem HeartRate { get; set; }
        public VirtualManAttributeItem PulseRate { get; set; }
        public VirtualManAttributeItem SPO2 { get; set; }
        public VirtualManAttributeItem Temp1 { get; set; }
        public VirtualManAttributeItem Temp2 { get; set; }
        public VirtualManAttributeItem[] IBP { get; set; }
        public VirtualManAttributeItem[] NIBP { get; set; }
        public VirtualManAttributeItem CO { get; set; }
        public VirtualManAttributeItem ETCO2 { get; set; }
        public VirtualManAttributeItem RESP { get; set; }
        public VirtualManAttributeItem N2O { get; set; }
        public VirtualManAttributeItem O2 { get; set; }
        public VirtualManAttributeItem PAP { get; set; }


        public VirtualManAttributeModel()
        {
            IBP = new VirtualManAttributeItem[2];
            NIBP = new VirtualManAttributeItem[2];
        }

        public VirtualManAttributeItem this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return HeartRate;
                    case 1: return PulseRate;
                    case 2: return SPO2;
                    case 3: return Temp1;
                    case 4: return Temp2;
                    case 7: return CO;
                    case 8: return ETCO2;
                    case 9: return RESP;
                    case 10: return N2O;
                    case 11: return O2;
                    case 12: return PAP;
                    default: return null;
                }
            }
            set
            {
                switch (i)
                {
                    case 0: HeartRate = value; break;
                    case 1: PulseRate = value; break;
                    case 2: SPO2 = value; break;
                    case 3: Temp1 = value; break;
                    case 4: Temp2 = value; break;
                    case 7: CO = value; break;
                    case 8: ETCO2 = value; break;
                    case 9: RESP = value; break;
                    case 10: N2O = value; break;
                    case 11: O2 = value; break;
                    case 12: PAP = value; break;
                    default: break;
                }
            }
        }

        public VirtualManAttributeModel Clone()
        {
            return this.MemberwiseClone() as VirtualManAttributeModel;
        }
    }
    public class VirtualManAttributeData:ISettingData
    {
        private VirtualManAttributeModel _custom;
        public VirtualManAttributeModel Custom
        {
            get
            {
                if (_custom == null && Default != null)
                {
                    _custom = (VirtualManAttributeModel)Default.Clone();
                }
                return _custom;
            }
            set
            {
                _custom = value;
            }
        }
        public VirtualManAttributeModel Default { get; set; }

        public void SetDefault()
        {

        }

    }

    public  class WaveSettingData:ISettingData
    {
        private WaveSettingModel _custom;
        public WaveSettingModel Custom
        {
            get
            {
                if (_custom == null && Default != null)
                {
                    _custom = (WaveSettingModel)Default.Clone();
                }
                return _custom;
            }
            set
            {
                _custom = value;
            }
        }
        public WaveSettingModel Default { get; set; }
        public void SetDefault()
        {
            Default = new WaveSettingModel()
            {
                ABP = new WaveSettingItem()
                {
                    Gain = 20,
                    Speed = 25,
                    WarnSwitch = true,
                    WarnLevel = 2,
                    WarnRanges = new List<WarnRange>() {
                            new WarnRange() {  Min=90,Max=140},
                            new WarnRange() {  Min=40,Max=90 },
                            new WarnRange() {  Min=80,Max=100}
                        }
                },
                RESP = new WaveSettingItem()
                {
                    Gain = 20,
                    Speed = 25,
                    WarnSwitch = true,
                    WarnLevel = 2,
                    WarnRanges = new List<WarnRange>() {
                                 new WarnRange() {  Min=10,Max=20}
                        }
                },
                PAP = new WaveSettingItem() { Gain = 20, Speed = 25 },
                PLETH = new WaveSettingItem()
                {
                    Gain = 20,
                    Speed = 25,
                    WarnSwitch = true,
                    WarnLevel = 2,
                    WarnRanges = new List<WarnRange>() {
                                new WarnRange() {  Min=96,Max=99}
                        }
                }
            };
        }
    }

    public class WaveSettingModel
    {
         public WaveSettingItem PLETH { get; set; }
        public WaveSettingItem ABP { get; set; }
        public WaveSettingItem PAP { get; set; }
        public WaveSettingItem RESP { get; set; }

        public WaveSettingModel Clone()
        {
            return this.MemberwiseClone() as WaveSettingModel;
        }
    }

    public class WaveSettingItem
    {
        public int Gain { get; set; }
        public int Speed { get; set; }


        public bool WarnSwitch { get;set; }
        public int  WarnLevel { get; set; }
         public List<WarnRange> WarnRanges { get; set; }

        public WaveSettingItem Clone()
        {
            return this.MemberwiseClone() as WaveSettingItem;
        }
    }

    public class WarnRange
    {
        public int Min { get; set; }
        public int Max { get; set; }

    }

   
}


