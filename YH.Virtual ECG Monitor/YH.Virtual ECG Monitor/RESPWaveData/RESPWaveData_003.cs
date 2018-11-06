using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework;
namespace YH.ECGMonitor.WaveData.RESPWaveData
{
    public class RESPWaveData_003 : RESPWaveData
    {
        public RESPWaveData_003()
            : base()
        {
            // TODO: Complete member initialization

            ID = "R03";
            Name = "窦律，心肌缺血后";
            Remark = "";
            Rate = 8;
            InspCapacity = 3000;
            Ratio = 67;
            ETCO2 = 40;
            WaveData = resp_00;
        }

        public static float[] resp_00 = new float[45] {
34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,35.64f
,38.16f
,40.22f
,38.16f
,34.60f
,32.54f
,31.51f
,36.09f
,65.14f
,35.64f
,24.75f
,30.48f
,32.54f
,31.97f
,31.51f
,32.54f
,32.54f
,33.57f
,34.60f
,35.64f
,36.67f
,35.64f
,35.06f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f
,34.60f

        };
    }

}
