using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECGMonitor
{
    public class RESPWaveData
    {
        private string _resp_id;           //记录号
        private string _case;      //病例名称
        private string _remark;        //病例描述       
        private int _rate;                      //波形频率   
        private int _inspcapacity;              //吸气量     //3000ml
        private int _ratio;                  //吸气比(时间)     呼吸比1:2
        private int _etco2;                       //呼气末二氧化碳
        private float[] _waveData;          //波形数据


        /// <summary>
        /// 记录号
        /// </summary>
        public string RESP_ID
        {
            get { return _resp_id; }
            set { _resp_id = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Case
        {
            get { return _case; }
            set { _case = value; }
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
        /// 吸气量     3000ml
        /// </summary>
        public int InspCapacity              
        {
            get { return _inspcapacity; }
            set { _inspcapacity = value; }
        }

        /// <summary>
        /// 吸气比(时间)     呼吸比1:2
        /// </summary>
        public int Ratio                 
        {
            get { return _ratio; }
            set { _ratio = value; }
        }

        /// <summary>
        /// 呼气末二氧化碳
        /// </summary>
        public int ETCO2                       
        {
            get { return _etco2; }
            set { _etco2 = value; }
        }

        /// <summary>
        /// 波形数据
        /// </summary>
        public float[] WaveData
        {
            get { return _waveData; }
            set { _waveData = value; }
        }

        public RESPWaveData()
        {

        }
    }
}
