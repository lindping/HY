using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{

    /// <summary>
    /// 血压
    /// </summary>
    public struct BloodPressure
    {
        static private BloodPressure _default;

        static public BloodPressure Default
        {
            get { return _default; }
        }

        static BloodPressure()
        {
            _default.NIBP = NIBP.Default;
            _default.Korotkoff = Korotkoff.Default;
        }

        /// <summary>
        /// 无创血压
        /// </summary>
        public NIBP NIBP;

        /// <summary>
        /// 科罗特科夫音
        /// </summary>
        public Korotkoff Korotkoff;
    }

    /// <summary>
    /// 血压 Blood Pressure
    /// </summary>
    public struct NIBP
    {
        static private NIBP _default;

        static public NIBP Default
        {
            get { return _default; }
        }

        static NIBP()
        {
            _default.Systolic = DataValue.Default;
            _default.Diastolic = DataValue.Default;
        }

        /// <summary>
        /// 收缩压systolic pressure
        /// </summary>
        public DataValue Systolic;

        /// <summary>
        /// 舒张压diastolic pressure
        /// </summary>
        public DataValue Diastolic;        

    }

    /// <summary>
    /// 科罗特科夫音
    /// </summary>
    public struct Korotkoff
    {
        static private Korotkoff _default;

        static public Korotkoff Default
        {
            get { return _default; }
        }

        static Korotkoff()
        {
            _default.SoundVolume = SoundVolume.V0;
            _default.Priority = Controller.Default;
        }
        /// <summary>
        /// 科罗特科夫音量 korotkoff volume
        /// </summary>
        public SoundVolume SoundVolume;

        /// <summary>
        ///  科罗特科夫音量优先权
        /// </summary>
        public Controller Priority;
    }
}
