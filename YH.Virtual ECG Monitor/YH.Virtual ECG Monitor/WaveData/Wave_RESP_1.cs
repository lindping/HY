using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.ECGWave.WaveData;
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{
    public class Wave_RESP_1
    {
        private WaveData_RESP _wavedata_RESP = new WaveData_RESP();

        public WaveData_RESP WaveData_RESP
        {
            get { return _wavedata_RESP; }
        }

        public Wave_RESP()
        {
            _wavedata_RESP = DataToObject.To<WaveData_RESP>(1);
        }

        public Wave_RESP(RespType eRespType)
        {
            _wavedata_RESP = DataToObject.To<WaveData_RESP>((int)eRespType);
        }
    }
}
