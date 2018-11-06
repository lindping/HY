using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 导尿数据字节处理 0x36
    /// </summary>
    static public class Catheterization
    {
        /// <summary>
        /// 尿液 Urine
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Urine(byte[] dataBytes, ref Modle.Urine Urine)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x36)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x01)
            {
                if (wordKey3 == 0x01)
                {
                    byte wordData1 = dataBytes[3];      //尿液有无
                    byte wordData2 = dataBytes[4];      //尿液量
                    Urine.Urinate.Status = DataConverter.ByteToControllerStatus(wordData1);
                    Urine.Urinate.Value = DataConverter.ByteToInt32(wordData2);
                }
            }
        }

        static public List<byte[]> SetDataBytes_Urine(Modle.Urine Urine)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Urine_Urinate(Urine));
       
            return dataBytes_List;
        }

        /// <summary>
        /// 尿液
        /// </summary>
        /// <param name="Urine"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Urine_Urinate(Modle.Urine Urinate)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x36;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01; 
            dataBytes[3] = DataConverter.ControllerStatusToByte(Urinate.Urinate.Status);
            dataBytes[4] = DataConverter.IntToBytes(Urinate.Urinate.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 导尿检查
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="Catheterization"></param>
        static public void GetData_Catheterization(byte[] dataBytes, ref Modle.Catheterization Catheterization)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x36)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x02)
            {
                if (wordKey3 == 0x01)
                {
                    byte wordData1 = dataBytes[3];      //插入膀光        
                    Catheterization.InsertionBladder.Status = DataConverter.ByteToOperatorStatus(wordData1);
                }
            }
        }

        /// <summary>
        /// 导尿检查
        /// </summary>
        /// <param name="Catheterization"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Catheterization(Modle.Catheterization Catheterization)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x36;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;

            dataBytes[3] = DataConverter.OperatorStatusToByte(Catheterization.InsertionBladder.Status);

            return dataBytes;
        }
    }
}
