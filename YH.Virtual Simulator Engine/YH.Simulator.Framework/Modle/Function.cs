using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    public enum Function
    {
        /// <summary>
        /// 眼睛
        /// </summary>
        Eyes = 1,

        /// <summary>
        /// 紫绀
        /// </summary>
        Cyanosis = 2,

        /// <summary>
        /// 模拟声音
        /// </summary>
        AnalogVocal = 3,

        /// <summary>
        /// 气道
        /// </summary>
        Airway = 4,

        /// <summary>
        /// 气管插管
        /// </summary>
        TracheaCannula = 5,

        /// <summary>
        /// 心肺复苏
        /// </summary>
        CPR = 6,

        /// <summary>
        /// 除颤
        /// </summary>
        Defibrillation = 7,

        /// <summary>
        /// 起搏
        /// </summary>
        Pacing = 8,

        /// <summary>
        /// 生命体征
        /// </summary>
        VitalSigns = 9,

        /// <summary>
        /// 触诊
        /// </summary>
        Palpation = 10,

        /// <summary>
        /// 听诊
        /// </summary>
        Stethoscopy = 11,

        /// <summary>
        /// 血压测量
        /// </summary>
        BloodPressure = 12,

        /// <summary>
        /// 惊厥
        /// </summary>
        Convulsions = 13,

        /// <summary>
        /// 脉搏
        /// </summary>
        Pulse = 14,

        /// <summary>
        /// 分泌物
        /// </summary>
        Secretions = 15,

        /// <summary>
        /// 出血
        /// </summary>
        Haemorrhage = 16,

        /// <summary>
        /// 导尿
        /// </summary>
        Catheterization = 17,

        /// <summary>
        /// 药物治疗
        /// </summary>
        Medication = 18,

        /// <summary>
        /// 心电监护
        /// </summary>
        ECG = 19,
    }
}
