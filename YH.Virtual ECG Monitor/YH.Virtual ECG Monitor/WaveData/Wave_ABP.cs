using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.ECGWave.WaveData;
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{
    public class Wave_ABP
    {
        private WaveData_ABP _wavedata_ABP = new WaveData_ABP();

        public WaveData_ABP WaveData_ABP
        {
            get { return _wavedata_ABP; }
        }

        public Wave_ABP()
        {
            _wavedata_ABP = DataToObject.To<WaveData_ABP>(1);
        }

        public Wave_ABP(int i)
        {
            _wavedata_ABP = DataToObject.To<WaveData_ABP>(i);
        }
    }
}
