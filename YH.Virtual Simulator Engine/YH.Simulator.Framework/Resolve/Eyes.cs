using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 眼睛数据字节处理 0x20
    /// </summary>
    static public class Eyes
    {

        /// <summary>
        /// 眼睛  Eyes
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Eyes(byte[] dataBytes, ref Modle.Eyes Eyes)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x20)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey2)
            {
                case 0x01:          //眼脸
                    if (wordKey3 == 0x01)
                    {
                        Modle.Eyelid Eyelid = Modle.Eyelid.Default;
                        //眼脸状态
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];

                        //右
                        Eyes.Eyelid.Right = DataConverter.ByteToEyelidStatus(wordData1);
                        //左
                        Eyes.Eyelid.Left = DataConverter.ByteToEyelidStatus(wordData2);

                    }
                    break;
                case 0x02:          //眨眼
                    if (wordKey3 == 0x01)
                    {
                        //眨眼速度
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        //右
                        Eyes.Blinking.Right = DataConverter.ByteToBlinkingSpeed(wordData1);
                        //左
                        Eyes.Blinking.Left = DataConverter.ByteToBlinkingSpeed(wordData2);
                    }
                    break;
                case 0x03:          //瞳孔
                    if (wordKey3 == 0x01)
                    {
                        //瞳孔大小
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];

                        //右
                        Eyes.Pupill.Right = DataConverter.ByteToPupillSize(wordData1);
                        //左
                        Eyes.Pupill.Left = DataConverter.ByteToPupillSize(wordData2);
                    }
                    break;
                case 0x04:          //对光
                    if (wordKey3 == 0x01)
                    {
                        //对光反射
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];

                        //右
                        Eyes.Light.Right = DataConverter.ByteToLightSensitivity(wordData1);
                        //左
                        Eyes.Light.Left = DataConverter.ByteToLightSensitivity(wordData2);
                    }
                    break;
                default:
                    break;
            }
        }

        static public void GetData_Eyes_PupillaryLight(byte[] dataBytes, ref Modle.PupillaryLight PupillaryLight)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x20)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            if (wordKey2 == 0x04)
            {

                if (wordKey3 == 0x02)
                {
                    //对光反射
                    byte wordData1 = dataBytes[3];
                    byte wordData2 = dataBytes[4];

                    //右
                    PupillaryLight.Right.Status = DataConverter.ByteToOperatorStatus(wordData1);
                    //左
                    PupillaryLight.Left.Status = DataConverter.ByteToOperatorStatus(wordData2);
                }

            }
        }


        /// <summary>
        /// 眼睛
        /// </summary>
        /// <param name="Eyes"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Eyes(Modle.Eyes Eyes)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Eyes_Eyelid(Eyes.Eyelid));
            dataBytes_List.Add(SetDataBytes_Eyes_Blinking(Eyes.Blinking));
            dataBytes_List.Add(SetDataBytes_Eyes_Pupill(Eyes.Pupill));
            dataBytes_List.Add(SetDataBytes_Eyes_Light(Eyes.Light));

            return dataBytes_List;
        }

        /// <summary>
        /// 眼脸
        /// </summary>
        /// <param name="Eyelid"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Eyes_Eyelid(Modle.Eyelid Eyelid)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x20;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.EyelidStatusToByte(Eyelid.Right);
            dataBytes[4] = DataConverter.EyelidStatusToByte(Eyelid.Left);

            return dataBytes;
        }

        /// <summary>
        /// 眨眼
        /// </summary>
        /// <param name="Blinking"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Eyes_Blinking(Modle.Blinking Blinking)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x20;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.BlinkingSpeedToByte(Blinking.Right);
            dataBytes[4] = DataConverter.BlinkingSpeedToByte(Blinking.Left);

            return dataBytes;
        }

        /// <summary>
        /// 瞳孔
        /// </summary>
        /// <param name="Pupill"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Eyes_Pupill(Modle.Pupill Pupill)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x20;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.PupillSizeToByte(Pupill.Right);
            dataBytes[4] = DataConverter.PupillSizeToByte(Pupill.Left);

            return dataBytes;
        }

        /// <summary>
        /// 对光
        /// </summary>
        /// <param name="Light"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Eyes_Light(Modle.Light Light)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x20;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.LightSensitivityToByte(Light.Right);
            dataBytes[4] = DataConverter.LightSensitivityToByte(Light.Left);

            return dataBytes;
        }

        /// <summary>
        /// 对光检查
        /// </summary>
        /// <param name="PupillaryLight"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Eyes_PupillaryLight(Modle.PupillaryLight PupillaryLight)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x20;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x02;
            dataBytes[3] = DataConverter.OperatorStatusToByte(PupillaryLight.Right.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(PupillaryLight.Left.Status);
            return dataBytes;
        }

    }
}


