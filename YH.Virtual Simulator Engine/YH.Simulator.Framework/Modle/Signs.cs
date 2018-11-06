using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 体征
    /// </summary>
    public struct Signs
    {
        static private Signs _default;

        static public Signs Default
        {
            get { return _default; }
        }

        static Signs()
        {
            _default.Eyes = Eyes.Default;
            _default.Cyanosis = Cyanosis.Default;
            _default.AnalogVocal = AnalogVocal.Default;
            _default.Airway = Airway.Default;
            _default.VitalSigns = VitalSigns.Default;
            _default.Palpation = Palpation.Default;
            _default.Stethoscopy = Stethoscopy.Default;
            _default.BloodPressure = BloodPressure.Default;
            _default.Convulsions = Convulsions.Default;
            _default.Pulse = Pulse.Default;
            _default.Secretions = Secretions.Default;
            _default.Haemorrhage = Haemorrhage.Default;
            _default.Urine = Urine.Default;
            _default.DrugDelivery = DrugDelivery.Default;
            _default.ECG = ECG.Default;
        }

        /// <summary>
        /// 眼睛 Eyes
        /// </summary>
        public Eyes Eyes;

        /// <summary>
        /// 紫绀 Cyanosis
        /// </summary>
        public Cyanosis Cyanosis;

        /// <summary>
        /// 语音 Vocal
        /// </summary>
        public AnalogVocal AnalogVocal;

        /// <summary>
        /// 气道 Airway
        /// </summary>
        public Airway Airway;

        /// <summary>
        /// 生命体征 Vital Signs
        /// </summary>
        public VitalSigns VitalSigns;

        /// <summary>
        /// 触诊 Palpation
        /// </summary>
        public Palpation Palpation;

        /// <summary>
        /// 听诊 Stethoscopy
        /// </summary>
        public Stethoscopy Stethoscopy;

        /// <summary>
        /// 血压测量 Blood Pressure
        /// </summary>
        public BloodPressure BloodPressure;

        /// <summary>
        /// 惊厥 Convulsions
        /// </summary>
        public Convulsions Convulsions;

        /// <summary>
        /// 脉搏  Pulse
        /// </summary>
        public Pulse Pulse;

        /// <summary>
        /// 分泌  Secretions
        /// </summary>
        public Secretions Secretions;

        /// <summary>
        /// 出血 Haemorrhage
        /// </summary>
        public Haemorrhage Haemorrhage;

        /// <summary>
        /// 尿液 Urine
        /// </summary>
        public Urine Urine;

        /// <summary>
        /// 给药 DrugDelivery
        /// </summary>
        public DrugDelivery DrugDelivery;
        
        /// <summary>
        /// 导联线 LeadLine
        /// </summary>
        public ECG ECG;
    }
}
