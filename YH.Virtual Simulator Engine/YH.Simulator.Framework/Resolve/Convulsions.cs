using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 惊厥数据字节处理 0x32
    /// </summary>
    static public class Convulsions
    {
        /// <summary>
        /// 惊厥  Convulsions 0x32
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Convulsions(byte[] dataBytes, ref Modle.Convulsions Convulsions)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x32)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //阵挛
                    if (wordKey3 == 0x01)
                    {
                        //手臂阵挛 0x01
                        byte wordData1 = dataBytes[3];      //右手臂
                        byte wordData2 = dataBytes[4];      //右手臂持续时间
                        byte wordData3 = dataBytes[5];      //左手臂
                        byte wordData4 = dataBytes[6];      //左手臂持续时间
                        Convulsions.Clonic.RightArm.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Convulsions.Clonic.RightArm.Value = DataConverter.ByteToInt32(wordData2);
                        Convulsions.Clonic.LeftArm.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Convulsions.Clonic.LeftArm.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                case 0x02:      //强直 
                    if (wordKey3 == 0x01)
                    {
                        // 颈部强直 0x01
                        byte wordData5 = dataBytes[3];      //颈部强直
                        byte wordData6 = dataBytes[4];      //颈部强直持续时间                   
                        Convulsions.Tonic.NeckAnkylosis.Status = DataConverter.ByteToControllerStatus(wordData5);
                        Convulsions.Tonic.NeckAnkylosis.Value = DataConverter.ByteToInt32(wordData6);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 惊厥
        /// </summary>
        /// <param name="Convulsions"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Convulsions(Modle.Convulsions Convulsions)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Convulsions_Clonic(Convulsions.Clonic));
            dataBytes_List.Add(SetDataBytes_Convulsions_Tonic(Convulsions.Tonic));

            return dataBytes_List;
        }

        /// <summary>
        /// 阵挛
        /// </summary>
        /// <param name="Clonic"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Convulsions_Clonic(Modle.Clonic Clonic)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x32;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Clonic.RightArm.Status);
            dataBytes[4] = DataConverter.IntToBytes(Clonic.RightArm.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(Clonic.LeftArm.Status);
            dataBytes[6] = DataConverter.IntToBytes(Clonic.LeftArm.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 强直
        /// </summary>
        /// <param name="Tonic"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Convulsions_Tonic(Modle.Tonic Tonic)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x32;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Tonic.NeckAnkylosis.Status);
            dataBytes[4] = DataConverter.IntToBytes(Tonic.NeckAnkylosis.Value)[0];

            return dataBytes;
        }
    }
}
