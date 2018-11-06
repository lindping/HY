using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulators = YH.Simulator.Framework.Modle;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 症状
    /// </summary>
    public struct Symptoms
    {
        static private Symptoms _default;

        static public Symptoms Default
        {
            get { return _default; }
        }

        static Symptoms()
        {
            _default.Eyes = Simulators.Eyes.Default;
            _default.Cyanosis = Simulators.Cyanosis.Default;
            _default.AnalogVocal = Simulators.AnalogVocal.Default;
            _default.Airway = Simulators.Airway.Default;
            _default.Palpation = Simulators.Palpation.Default;
            _default.Stethoscopy = Simulators.Stethoscopy.Default;
            _default.BloodPressure = Simulators.BloodPressure.Default;
            _default.Convulsions = Simulators.Convulsions.Default;
            _default.Pulse = Simulators.Pulse.Default;
            _default.Secretions = Simulators.Secretions.Default;
            _default.Haemorrhage = Simulators.Haemorrhage.Default;
            _default.Urine = Simulators.Urine.Default;
        }

        /// <summary>
        /// 眼睛 
        /// </summary>
        public Simulators.Eyes Eyes;

        /// <summary>
        /// 紫绀
        /// </summary>
        public Simulators.Cyanosis Cyanosis;

        /// <summary>
        /// 语音 Vocal
        /// </summary>
        public Simulators.AnalogVocal AnalogVocal;

        /// <summary>
        /// 气道
        /// </summary>
        public Simulators.Airway Airway;

        /// <summary>
        /// 触诊 Palpation
        /// </summary>
        public Simulators.Palpation Palpation;

        /// <summary>
        /// 听诊 Stethoscopy
        /// </summary>
        public Simulators.Stethoscopy Stethoscopy;

        /// <summary>
        /// 血压测量 Blood Pressure
        /// </summary>
        public Simulators.BloodPressure BloodPressure;

        /// <summary>
        /// 惊厥 Convulsions
        /// </summary>
        public Simulators.Convulsions Convulsions;

        /// <summary>
        /// 脉搏  Pulse
        /// </summary>
        public Simulators.Pulse Pulse;

        /// <summary>
        /// 分泌  Secretions
        /// </summary>
        public Simulators.Secretions Secretions;

        /// <summary>
        /// 出血 Haemorrhage
        /// </summary>
        public Simulators.Haemorrhage Haemorrhage;

        /// <summary>
        /// 尿液 Urine
        /// </summary>
        public Simulators.Urine Urine;


    }
}
