using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 治疗、处理
    /// </summary>
    public struct Treatment
    {
        static private Treatment _default;

        static public Treatment Default
        {
            get { return _default; }
        }

        static Treatment()
        {
            _default.PupillaryLight = PupillaryLight.Default;
            _default.TracheaCannula = TracheaCannula.Default;
            _default.VitalSigns = VitalSigns.Default;
            _default.CPR_Q = CPR_Q.Default;
            _default.CPR_P = CPR_P.Default;
            _default.Defibrillation = Defibrillation.Default;
            _default.DefibrillatorElectrode = DefibrillatorElectrode.Default;
            _default.Pacing = Pacing.Default;
            _default.PacerElectrode = PacerElectrode.Default;
            _default.AbdominalTouch = AbdominalTouch.Default;
            _default.MeasureBP = MeasureBP.Default;
            _default.CheckPulse = CheckPulse.Default;
            _default.Catheterization = Catheterization.Default;
            _default.Medication = Medication.Default;
            _default.ECG = ECG.Default;

        }

        /// <summary>
        /// 瞳孔对光 Pupillary Light
        /// </summary>
        public PupillaryLight PupillaryLight;

        /// <summary>
        /// 气管插管 Trachea Cannula
        /// </summary>
        public TracheaCannula TracheaCannula;

        /// <summary>
        /// 生命体征 Vital Signs
        /// </summary>
        public VitalSigns VitalSigns;

        /// <summary>
        /// CPR-Q
        /// </summary>
        public CPR_Q CPR_Q;

        /// <summary>
        /// CPR操作
        /// </summary>
        public CPR_P CPR_P;

        /// <summary>
        /// 除颤
        /// </summary>
        public Defibrillation Defibrillation;

        /// <summary>
        /// 除颤电极
        /// </summary>
        public DefibrillatorElectrode DefibrillatorElectrode;

        /// <summary>
        /// 起搏 Pacing
        /// </summary>
        public Pacing Pacing;

        /// <summary>
        /// 起搏电极 Pacer Electrode
        /// </summary>
        public PacerElectrode PacerElectrode;

        /// <summary>
        /// 测量血压 Measure BP
        /// </summary>
        public MeasureBP MeasureBP;

        /// <summary>
        /// 腹部触诊 Abdominal Touch
        /// </summary>
        public AbdominalTouch AbdominalTouch;

        /// <summary>
        /// 脉搏检查 Pulse Check
        /// </summary>
        public CheckPulse CheckPulse;

        /// <summary>
        /// 导尿 Catheterization
        /// </summary>
        public Catheterization Catheterization;

        /// <summary>
        /// 药物治疗  Medication
        /// </summary>
        public Medication Medication;        

        /// <summary>
        /// 导联线 LeadLine
        /// </summary>
        public ECG ECG;
    }
}
