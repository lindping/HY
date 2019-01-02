using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.ECGWave.WaveData;
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{
    public class Wave_PLETH
    {
        private WaveData_PLETH _wavedata_PLETH = new WaveData_PLETH();

        public WaveData_PLETH WaveData_PLETH
        {
            get { return _wavedata_PLETH; }
        }

        public Wave_PLETH()
        {
            _wavedata_PLETH = DataToObject.To<WaveData_PLETH>(1);
        }

        public Wave_PLETH(int i)
        {
            _wavedata_PLETH = DataToObject.To<WaveData_PLETH>(i);
        }
    }
}
