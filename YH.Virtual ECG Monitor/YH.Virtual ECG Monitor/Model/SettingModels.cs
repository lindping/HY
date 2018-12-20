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

    public class ECGSettingData
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

    public class PatientInfoData
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

    public class OtherSettingData
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

    public class LayoutSettingData
    {
        public List<LayoutSettingModel> Layouts { get; set; }
    }


    /// <summary>
    /// 模型身体特征
    /// </summary>
    public class VirtualManAttributeModel
    {
        public int HeartRate { get; set; }
        public int PulseRate { get; set; }
        public int SPO2 { get; set; }
        public int Temp1 { get; set; }
        public int Temp2 { get; set; }
        public int IBP { get; set; }
        public int CO { get; set; }
        public int ETCO2 { get; set; }
        public int RESP { get; set; }
        public int N2O { get; set; }
        public int O2 { get; set; }
        public int PAP { get; set; }

        public int this[int i]
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
                    case 5: return IBP;
                    case 6: return CO;
                    case 7: return ETCO2;
                    case 8: return RESP;
                    case 9: return N2O;
                    case 10: return O2;
                    case 11: return PAP;
                    default: return HeartRate;
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
                    case 5: IBP = value; break;
                    case 6: CO = value; break;
                    case 7: ETCO2 = value; break;
                    case 8: RESP = value; break;
                    case 9: N2O = value; break;
                    case 10: O2 = value; break;
                    case 11: PAP = value; break;
                    default: HeartRate = value; break; ;
                }
            }
        }

        public VirtualManAttributeModel Clone()
        {
            return this.MemberwiseClone() as VirtualManAttributeModel;
        }
    }
    public class VirtualManAttributeData
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

    }
}


