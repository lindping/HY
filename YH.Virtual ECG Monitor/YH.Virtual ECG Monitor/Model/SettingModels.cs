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
                if (_custom == null && Default!=null)
                {
                    _custom =(ECGSettingModel)Default.Clone();
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
                if (_custom == null && Default!=null)
                {
                    _custom =(PatientInfoModel)Default.Clone();
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
        public string  DefaultLayout { get; set; }
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

}
