using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.ECGWave.WaveData;
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{
    public class Wave_ECG
    {
        private WaveData_ECG _wavedata_ECG = new WaveData_ECG();

        public WaveData_ECG WaveData_ECG
        {
            get { return _wavedata_ECG; }
        }

        public Wave_ECG()
        {
            _wavedata_ECG = DataToObject.To<WaveData_ECG>(1);
        }

        public Wave_ECG(Rhythm eRhythm)
        {
            _wavedata_ECG = DataToObject.To<WaveData_ECG>((int)eRhythm);
        }
    }
}
