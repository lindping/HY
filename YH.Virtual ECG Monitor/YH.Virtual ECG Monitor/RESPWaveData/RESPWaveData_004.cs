using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework;
namespace YH.ECGMonitor.WaveData.RESPWaveData
{
    public class RESPWaveData_004 : RESPWaveData
    {
        public RESPWaveData_004()
            : base()
        {
            // TODO: Complete member initialization

            ID = "R04";
            Name = "有下急性心肌梗死的窦律，ST抬高";
            Remark = "";
            Rate = 8;
            InspCapacity = 3000;
            Ratio = 67;
            ETCO2 = 40;
            WaveData = resp_00;
        }

        public static float[] resp_00 = new float[44] {
 33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,34.60f
,35.64f
,36.67f
,35.64f
,34.60f
,34.60f
,34.03f
,35.64f
,43.89f
,59.20f
,42.28f
,29.91f
,33.57f
,34.60f
,34.60f
,34.03f
,33.57f
,33.57f
,34.03f
,37.13f
,40.22f
,40.79f
,38.16f
,36.09f
,34.03f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f
,33.57f

        };
    }

}
