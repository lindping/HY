using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.ECGWave.WaveData
{
    public class WaveData_ECG
    {
        private string _id;             //记录号
        private string _name;           //名称
        private string _remark;         //病例描述
        private int _rate;              //波形频率
        private float _baseAMP;         //波形放大倍数
        private float[,] _waveData;      //波形数据  

        public WaveData_ECG()
        {

        }

        /// <summary>
        /// 记录号
        /// </summary>
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        /// <summary>
        /// 波形频率
        /// </summary>
        public int Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }

        /// <summary>
        /// 波形放大倍数
        /// </summary>
        public float BaseAMP
        {
            get { return _baseAMP; }
            set { _baseAMP = value; }
        }

        /// <summary>
        /// 波形数据
        /// </summary>
        public float[,] WaveData
        {
            get { return _waveData; }
            set { _waveData = value; }
        }
    }
}
