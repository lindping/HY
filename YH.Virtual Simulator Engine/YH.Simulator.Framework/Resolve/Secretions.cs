using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 分泌物数据字节处理
    /// </summary>
    static public class Secretions
    {
        /// <summary>
        /// 分泌  Secretions 0x34
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Secretions(byte[] dataBytes, ref Modle.Secretions Secretions)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x34)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //出汗
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];      //有无
                        byte wordData2 = dataBytes[4];      //量
                        Secretions.Sweat.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Secretions.Sweat.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x02:      //眼睛
                    if (wordKey3 == 0x01)
                    {
                        byte wordData3 = dataBytes[3];      //有无
                        byte wordData4 = dataBytes[4];      //量
                        Secretions.Eyes.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Secretions.Eyes.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                case 0x03:      //口
                    if (wordKey3 == 0x01)
                    {
                        byte wordData5 = dataBytes[3];      //有无
                        byte wordData6 = dataBytes[4];      //量
                        Secretions.Mouth.Status = DataConverter.ByteToControllerStatus(wordData5);
                        Secretions.Mouth.Value = DataConverter.ByteToInt32(wordData6);
                    }
                    break;
                case 0x04:      //耳分泌物
                    if (wordKey3 == 0x01)
                    {
                        byte wordData7 = dataBytes[3];      //有无
                        byte wordData8 = dataBytes[4];      //量
                        Secretions.Ears.Status = DataConverter.ByteToControllerStatus(wordData7);
                        Secretions.Ears.Value = DataConverter.ByteToInt32(wordData8);
                    }
                    break;
                case 0x05:      //鼻腔分泌物
                    if (wordKey3 == 0x01)
                    {
                        byte wordData9 = dataBytes[3];      //有无
                        byte wordData10 = dataBytes[4];     //量
                        Secretions.Nose.Status = DataConverter.ByteToControllerStatus(wordData9);
                        Secretions.Nose.Value = DataConverter.ByteToInt32(wordData10);
                    }
                    break;
                case 0x06:      //白沫 
                    if (wordKey3 == 0x01)
                    {
                        byte wordData11 = dataBytes[3];      //有无
                        byte wordData12 = dataBytes[4];      //量
                        Secretions.Froth.Status = DataConverter.ByteToControllerStatus(wordData11);
                        Secretions.Froth.Value = DataConverter.ByteToInt32(wordData12);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 分泌
        /// </summary>
        /// <param name="Secretions"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Secretions(Modle.Secretions Secretions)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Secretions_Sweat(Secretions.Sweat));
            dataBytes_List.Add(SetDataBytes_Secretions_Eyes(Secretions.Eyes));
            dataBytes_List.Add(SetDataBytes_Secretions_Mouth(Secretions.Mouth));
            dataBytes_List.Add(SetDataBytes_Secretions_Ears(Secretions.Ears));
            dataBytes_List.Add(SetDataBytes_Secretions_Nose(Secretions.Nose));
            dataBytes_List.Add(SetDataBytes_Secretions_Froth(Secretions.Froth));

            return dataBytes_List;
        }

        /// <summary>
        /// 出汗
        /// </summary>
        /// <param name="Sweat"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Secretions_Sweat(Controller Sweat)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x34;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Sweat.Status);
            dataBytes[4] = DataConverter.IntToBytes(Sweat.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 眼睛
        /// </summary>
        /// <param name="Eyes"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Secretions_Eyes(Controller Eyes)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x34;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Eyes.Status);
            dataBytes[4] = DataConverter.IntToBytes(Eyes.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 口
        /// </summary>
        /// <param name="Mouth"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Secretions_Mouth(Controller Mouth)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x34;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Mouth.Status);
            dataBytes[4] = DataConverter.IntToBytes(Mouth.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 耳分泌物
        /// </summary>
        /// <param name="Ears"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Secretions_Ears(Controller Ears)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x34;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Ears.Status);
            dataBytes[4] = DataConverter.IntToBytes(Ears.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 鼻腔分泌物
        /// </summary>
        /// <param name="Nose"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Secretions_Nose(Controller Nose)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x34;
            dataBytes[1] = 0x05;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Nose.Status);
            dataBytes[4] = DataConverter.IntToBytes(Nose.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 白沫
        /// </summary>
        /// <param name="Froth"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Secretions_Froth(Controller Froth)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x34;
            dataBytes[1] = 0x06;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Froth.Status);
            dataBytes[4] = DataConverter.IntToBytes(Froth.Value)[0];

            return dataBytes;
        }
        
    }
}
