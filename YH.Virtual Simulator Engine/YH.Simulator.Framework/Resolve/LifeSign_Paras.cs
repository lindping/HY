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

        public static List<byte[]> GetByte_ECG(int heartRate,int spo2,int abp,int respRate,int capacity,int respRatio)
        {
            List<byte[]> result = new List<byte[]>();

            byte[] bytes = new byte[5];
            bytes[0] = BitConverter.GetBytes(CategoryLevel0_LifeSign)[0];
            bytes[1] = BitConverter.GetBytes(CategoryLevel1_Circulation)[0];
            bytes[2] = BitConverter.GetBytes(CategoryLevel1_Circulation_HeartRate)[0];
            bytes[3] = BitConverter.GetBytes(heartRate)[0];
            bytes[4] = BitConverter.GetBytes(0)[0];
            result.Add(bytes);

            bytes = new byte[5];
            bytes[0] = BitConverter.GetBytes(CategoryLevel0_LifeSign)[0];
            bytes[1] = BitConverter.GetBytes(CategoryLevel1_Circulation)[0];
            bytes[2] = BitConverter.GetBytes(CategoryLevel1_Circulation_SPO2)[0];
            bytes[3] = BitConverter.GetBytes(spo2)[0];
            bytes[4] = BitConverter.GetBytes(0)[0];
            result.Add(bytes);

            bytes = new byte[5];
            bytes[0] = BitConverter.GetBytes(CategoryLevel0_LifeSign)[0];
            bytes[1] = BitConverter.GetBytes(CategoryLevel1_Circulation)[0];
            bytes[2] = BitConverter.GetBytes(CategoryLevel1_Circulation_ABP)[0];
            bytes[3] = BitConverter.GetBytes(abp)[0];
            bytes[4] = BitConverter.GetBytes(abp)[1];
            result.Add(bytes);

            bytes = new byte[5];
            bytes[0] = BitConverter.GetBytes(CategoryLevel0_LifeSign)[0];
            bytes[1] = BitConverter.GetBytes(CategoryLevel1_Respiratory)[0];
            bytes[2] = BitConverter.GetBytes(CategoryLevel1_Respiratory_RespRate)[0];
            bytes[3] = BitConverter.GetBytes(respRate)[0];
            bytes[4] = BitConverter.GetBytes(0)[1];
            result.Add(bytes);


            bytes = new byte[5];
            bytes[0] = BitConverter.GetBytes(CategoryLevel0_LifeSign)[0];
            bytes[1] = BitConverter.GetBytes(CategoryLevel1_Respiratory)[0];
            bytes[2] = BitConverter.GetBytes(CategoryLevel1_Respiratory_Capacity)[0];
            bytes[3] = BitConverter.GetBytes(capacity)[0];
            bytes[4] = BitConverter.GetBytes(capacity)[1];
            result.Add(bytes);

            bytes = new byte[5];
            bytes[0] = BitConverter.GetBytes(CategoryLevel0_LifeSign)[0];
            bytes[1] = BitConverter.GetBytes(CategoryLevel1_Respiratory)[0];
            bytes[2] = BitConverter.GetBytes(CategoryLevel1_Respiratory_RespRatio)[0];
            bytes[3] = BitConverter.GetBytes(respRatio)[0];
            bytes[4] = BitConverter.GetBytes(0)[1];
            result.Add(bytes);

            return result;
        }

    }
}
