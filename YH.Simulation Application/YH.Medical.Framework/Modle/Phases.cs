using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Simulators = YH.Simulator.Framework.Modle;

namespace YH.Medical.Framework.Modle
{
    /// <summary>
    /// 阶段
    /// </summary>
    public struct Phases
    {
        static private Phases _default;

        static public Phases Default
        {
            get { return _default; }
        }

        static Phases()
        {
            _default.Name = "";
            _default.Description = "";
            _default.Symptoms = Symptoms.Default;
            _default.VitalSigns = Simulators.VitalSigns.Default;
            _default.Trend = 0;
            _default.NonMedicalResponses = "";
            _default.StopRunningTransitionsTrends = 0;
            _default.ClearDelayedResponses = 0;
            _default.AllOtherValuesTODefault = 0;
            _default.AutomaticShockConversion = 0;
            _default.TransitionTime = 0;
            _default.TransitionCurve = 0;
        }

        /// <summary>
        /// 名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Name;

        /// <summary>
        /// 描述 description
        /// </summary>
        public string Description;

        /// <summary>
        /// 症状 Symptoms
        /// </summary>
        public Symptoms Symptoms;

        /// <summary>
        /// 生命体征 Vital signs
        /// </summary>
        public Simulators.VitalSigns VitalSigns;

        /// <summary>
        /// 趋势
        /// </summary>
        public int Trend;

        /// <summary>
        /// 非医疗反应
        /// </summary>
        public string NonMedicalResponses;

        /// <summary>
        /// 进入此状态时停止所有运行中的过渡/趋势 Stop all running transitions/trends when entering this state
        /// </summary>
        public int StopRunningTransitionsTrends;

        /// <summary>
        /// 清除进入此状态时其他状态的延迟反应 Clear delayed responses from other states when entering this state
        /// </summary>
        public int ClearDelayedResponses;

        /// <summary>
        /// 其他值都设置为正常/缺省 All other values are set to normal/default
        /// </summary>
        public int AllOtherValuesTODefault;

        /// <summary>
        /// 如果设置为等待状态则自动除颤转律 Automatic shock conversion if set as waiting state
        /// </summary>
        public int AutomaticShockConversion;

        /// <summary>
        /// 转换时间到此状态 Transition time to this state
        /// </summary>
        public int TransitionTime;

        /// <summary>
        /// 转换曲线 Transition curve
        /// </summary>
        public int TransitionCurve;

    }
}
