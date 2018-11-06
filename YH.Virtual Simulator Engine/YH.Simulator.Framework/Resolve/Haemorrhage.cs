using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 出血数据字节处理 0x35
    /// </summary>
    static public class Haemorrhage
    {
        /// <summary>
        /// 出血  Haemorrhage
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Haemorrhage(byte[] dataBytes, ref Modle.Haemorrhage Haemorrhage)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x35)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //右上 
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];      //动脉有无
                        byte wordData2 = dataBytes[4];      //量
                        byte wordData3 = dataBytes[3];      //静脉有无
                        byte wordData4 = dataBytes[4];      //量
                        Haemorrhage.RightUpper.Arterial.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Haemorrhage.RightUpper.Arterial.Value = DataConverter.ByteToInt32(wordData2);
                        Haemorrhage.RightUpper.Venous.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Haemorrhage.RightUpper.Venous.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                case 0x02:      //左上
                    if (wordKey3 == 0x01)
                    {
                        byte wordData5 = dataBytes[3];      //动脉有无
                        byte wordData6 = dataBytes[4];      //量
                        byte wordData7 = dataBytes[3];      //静脉有无
                        byte wordData8 = dataBytes[4];      //量
                        Haemorrhage.LeftUpper.Arterial.Status = DataConverter.ByteToControllerStatus(wordData5);
                        Haemorrhage.LeftUpper.Arterial.Value = DataConverter.ByteToInt32(wordData6);
                        Haemorrhage.LeftUpper.Venous.Status = DataConverter.ByteToControllerStatus(wordData7);
                        Haemorrhage.LeftUpper.Venous.Value = DataConverter.ByteToInt32(wordData8);
                    }
                    break;
                case 0x03:      //右下
                    if (wordKey3 == 0x01)
                    {
                        byte wordData9 = dataBytes[3];      //动脉有无
                        byte wordData10 = dataBytes[4];      //量
                        byte wordData11 = dataBytes[3];      //静脉有无
                        byte wordData12 = dataBytes[4];      //量
                        Haemorrhage.RightLower.Arterial.Status = DataConverter.ByteToControllerStatus(wordData9);
                        Haemorrhage.RightLower.Arterial.Value = DataConverter.ByteToInt32(wordData10);
                        Haemorrhage.RightLower.Venous.Status = DataConverter.ByteToControllerStatus(wordData11);
                        Haemorrhage.RightLower.Venous.Value = DataConverter.ByteToInt32(wordData12);
                    }
                    break;
                case 0x04:      //左下
                    if (wordKey3 == 0x01)
                    {
                        byte wordData13 = dataBytes[3];      //动脉有无
                        byte wordData14 = dataBytes[4];      //量
                        byte wordData15 = dataBytes[3];      //静脉有无
                        byte wordData16 = dataBytes[4];      //量
                        Haemorrhage.LeftLower.Arterial.Status = DataConverter.ByteToControllerStatus(wordData13);
                        Haemorrhage.LeftLower.Arterial.Value = DataConverter.ByteToInt32(wordData14);
                        Haemorrhage.LeftLower.Venous.Status = DataConverter.ByteToControllerStatus(wordData15);
                        Haemorrhage.LeftLower.Venous.Value = DataConverter.ByteToInt32(wordData16);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 出血
        /// </summary>
        /// <param name="Haemorrhage"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Haemorrhage(Modle.Haemorrhage Haemorrhage)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Haemorrhage_RightUpper(Haemorrhage.RightUpper));
            dataBytes_List.Add(SetDataBytes_Haemorrhage_LeftUpper(Haemorrhage.LeftUpper));
            dataBytes_List.Add(SetDataBytes_Haemorrhage_RightLower(Haemorrhage.RightLower));
            dataBytes_List.Add(SetDataBytes_Haemorrhage_LeftLower(Haemorrhage.LeftLower));

            return dataBytes_List;
        }

        /// <summary>
        /// 右上
        /// </summary>
        /// <param name="RightUpper"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Haemorrhage_RightUpper(BloodVessel RightUpper)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x35;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(RightUpper.Arterial.Status);
            dataBytes[4] = DataConverter.IntToBytes(RightUpper.Arterial.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(RightUpper.Venous.Status);
            dataBytes[6] = DataConverter.IntToBytes(RightUpper.Venous.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 左上
        /// </summary>
        /// <param name="LeftUpper"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Haemorrhage_LeftUpper(BloodVessel LeftUpper)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x35;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(LeftUpper.Arterial.Status);
            dataBytes[4] = DataConverter.IntToBytes(LeftUpper.Arterial.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(LeftUpper.Venous.Status);
            dataBytes[6] = DataConverter.IntToBytes(LeftUpper.Venous.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 右下
        /// </summary>
        /// <param name="RightLower"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Haemorrhage_RightLower(BloodVessel RightLower)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x35;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(RightLower.Arterial.Status);
            dataBytes[4] = DataConverter.IntToBytes(RightLower.Arterial.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(RightLower.Venous.Status);
            dataBytes[6] = DataConverter.IntToBytes(RightLower.Venous.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 左下
        /// </summary>
        /// <param name="LeftLower"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Haemorrhage_LeftLower(BloodVessel LeftLower)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x35;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(LeftLower.Arterial.Status);
            dataBytes[4] = DataConverter.IntToBytes(LeftLower.Arterial.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(LeftLower.Venous.Status);
            dataBytes[6] = DataConverter.IntToBytes(LeftLower.Venous.Value)[0];

            return dataBytes;
        }
    }
}
