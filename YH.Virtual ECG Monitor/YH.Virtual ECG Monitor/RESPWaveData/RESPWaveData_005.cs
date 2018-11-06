using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework;
namespace YH.ECGMonitor.WaveData.RESPWaveData
{
    public class RESPWaveData_005 : RESPWaveData
    {
        public RESPWaveData_005()
            : base()
        {
            // TODO: Complete member initialization

            ID = "R05";
            Name = "有前壁急性心肌梗死的窦律，ST抬高";
            Remark = "";
            Rate = 8;
            InspCapacity = 3000;
            Ratio = 67;
            ETCO2 = 40;
            WaveData = resp_00;
        }

        public static float[] resp_00 = new float[44] {
34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,35.09f
,36.10f
,36.55f
,35.54f
,35.09f
,35.09f
,35.54f
,41.63f
,49.30f
,42.65f
,34.52f
,32.04f
,33.06f
,34.07f
,34.07f
,34.07f
,34.07f
,35.09f
,38.13f
,40.16f
,39.60f
,38.13f
,36.10f
,34.52f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f
,34.07f

        };
    }

}
