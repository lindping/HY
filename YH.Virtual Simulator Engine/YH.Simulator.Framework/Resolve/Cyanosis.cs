using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 紫绀数据字节处理 0x21
    /// </summary>
    static public class Cyanosis
    {
        /// <summary>
        /// 紫绀 Cyanosis
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Cyanosis(byte[] dataBytes, ref Modle.Cyanosis Cyanosis)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x21)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey2)
            {
                case 0x01:      //口唇
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Cyanosis.Lips.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Cyanosis.Lips.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x02:      //手指甲床
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Cyanosis.Fingernail.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Cyanosis.Fingernail.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x03:      //脚指甲床
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Cyanosis.FootNail.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Cyanosis.FootNail.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 紫绀 
        /// </summary>
        /// <param name="Cyanosis"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Cyanosis(Modle.Cyanosis Cyanosis)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Cyanosis_Lips(Cyanosis.Lips));
            dataBytes_List.Add(SetDataBytes_Cyanosis_Fingernail(Cyanosis.Fingernail));
            dataBytes_List.Add(SetDataBytes_Cyanosis_FootNail(Cyanosis.FootNail));

            return dataBytes_List;
        }
        /// <summary>
        /// 紫绀 - 口唇
        /// </summary>
        /// <param name="Cyanosis"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Cyanosis_Lips(Modle.Controller Lips)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x21;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Lips.Status);
            dataBytes[4] = DataConverter.IntToBytes(Lips.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 紫绀 - 手指甲床
        /// </summary>
        /// <param name="Cyanosis"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Cyanosis_Fingernail(Modle.Controller Fingernail)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x21;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Fingernail.Status);
            dataBytes[4] = DataConverter.IntToBytes(Fingernail.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 紫绀 - 脚指甲床
        /// </summary>
        /// <param name="Cyanosis"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Cyanosis_FootNail(Modle.Controller FootNail)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x21;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(FootNail.Status);
            dataBytes[4] = DataConverter.IntToBytes(FootNail.Value)[0];

            return dataBytes;
        }
    }
}
