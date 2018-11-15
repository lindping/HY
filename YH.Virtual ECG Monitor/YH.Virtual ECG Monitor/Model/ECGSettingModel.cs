using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_ECG_Monitor
{
    public class ECGSettingModel
    {
        /// <summary>
        /// 导联
        /// </summary>
        public string  Lead { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public string Speed { get; set; }

        /// <summary>
        /// QRS音量
        /// </summary>
        public int QRSVolumn  {get;set;}

        /// <summary>
        /// 增益
        /// </summary>
        public string Gain { get; set; }

        /// <summary>
        /// 心电图开关
        /// </summary>
        public bool ECGSwitch { get; set; }

        public int Max { get; set; }
        public int Min { get; set; }
        public bool Warning { get; set; }
        public string Level { get; set; }


    }

 
}
