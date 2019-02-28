using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Resolve
{
    public class LifeSign_Paras
    {

        public const int CategoryLevel0_LifeSign = 40;
        public const int CategoryLevel1_Circulation = 1;
        public const int CategoryLevel1_Respiratory = 2;

        public const int CategoryLevel1_Circulation_HeartRate = 2;
        public const int CategoryLevel1_Circulation_SPO2 = 3;
        public const int CategoryLevel1_Circulation_ABP = 4;


        public const int CategoryLevel1_Respiratory_RespRate = 2;
        public const int CategoryLevel1_Respiratory_Capacity = 3;
        public const int CategoryLevel1_Respiratory_RespRatio = 4;


        public int CategoryLevel1 { get; set; }
        public int CategoryLevel2 { get; set; }

        public int ParamValue { get; set; }


        public static int[] GetData_ECG(byte[] data)
        {
            int[] result = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = Convert.ToInt32(data[i]);
            }      
            return result;
        }

     
    }
}
