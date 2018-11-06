using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 系统 SimSystem 0x10
    /// </summary>
    static public class SimSystem
    {
        /// <summary>
        /// 设置系统
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="SimDownSystem"></param>
        static public void SetData_SimDownSystem(byte[] dataBytes, ref Modle.SimDownSystem SimDownSystem)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x10)
                return;

            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //模型连接器
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        SimDownSystem.SimConnector.Connect.Status = DataConverter.ByteToControllerStatus(wordData1);
                    }
                    break;
                case 0x02:      //模型属性
                    if (wordKey3 == 0x01)
                    {
                        byte wordData2 = dataBytes[3];      //模型类型
                        byte[] wordData3 = new byte[2];     //模型编号
                        byte[] wordData4 = new byte[2];     //模型序列号

                        Array.ConstrainedCopy(dataBytes, 4, wordData4, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 6, wordData4, 0, 2);

                        SimDownSystem.SimAttribute.SimType = DataConverter.ByteToInt32(wordData2);
                        SimDownSystem.SimAttribute.SimPN = DataConverter.BytesToInt32(wordData3);
                        SimDownSystem.SimAttribute.SimSN = DataConverter.BytesToInt32(wordData4);
                    }
                    break;
                case 0x03:      //语言
                    if (wordKey3 == 0x01)
                    {
                        byte wordData5 = dataBytes[3];
                        SimDownSystem.Language.LanguageCode = DataConverter.ByteToInt32(wordData5);
                    }
                    break;
                case 0x04:      //病人监护仪开关
                    if (wordKey3 == 0x01)
                    {
                        byte wordData6 = dataBytes[3];
                        SimDownSystem.PatientMonitor.Status = DataConverter.ByteToControllerStatus(wordData6);
                    }
                    break;
                case 0x05:      //除颤校准开关
                    if (wordKey3 == 0x01)
                    {
                        byte wordData7 = dataBytes[3];
                        SimDownSystem.DefibrillationCalibration.Status = DataConverter.ByteToControllerStatus(wordData7);
                    }
                    break;
                case 0x06:      //血压校准开关
                    if (wordKey3 == 0x01)
                    {
                        byte wordData8 = dataBytes[3];
                        SimDownSystem.BloodPressureCalibration.Status = DataConverter.ByteToControllerStatus(wordData8);
                    }
                    break;
                case 0x07:      //在线检测
                    if (wordKey3 == 0x01)
                    {
                        byte wordData9 = dataBytes[3];
                        SimDownSystem.Link.Status = DataConverter.ByteToControllerStatus(wordData9);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 模型连接
        /// </summary>
        /// <param name="SimAttribute"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_SimConnector(Modle.SimAttribute SimAttribute)
        {
            byte[] dataBytes = new byte[9];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.IntToBytes(SimAttribute.Tag)[0];          //模型标识
            dataBytes[4] = DataConverter.IntToBytes(SimAttribute.SimType)[0];      //模型类型

            byte[] Bytes = DataConverter.IntToBytes(SimAttribute.SimPN);            //模型编号

            dataBytes[5] = Bytes[0];
            dataBytes[6] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(SimAttribute.SimSN);           //模型序列号

            dataBytes[7] = Bytes2[0];
            dataBytes[8] = Bytes2[1];


            return dataBytes;
        }

        /// <summary>
        /// 模型设置
        /// </summary>
        /// <param name="SimAttribute"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_SimAttribute(Modle.SimAttribute SimAttribute)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.IntToBytes(SimAttribute.SimType)[0];      //模型类型

            byte[] Bytes = DataConverter.IntToBytes(SimAttribute.SimPN);            //模型编号

            dataBytes[4] = Bytes[0];
            dataBytes[5] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(SimAttribute.SimSN);           //模型序列号

            dataBytes[6] = Bytes2[0];
            dataBytes[7] = Bytes2[1];

            return dataBytes;
        }

        /// <summary>
        /// 语言
        /// </summary>
        /// <param name="Language"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Language(Modle.Language Language)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.IntToBytes(Language.LanguageCode)[0];          //语言代号

            return dataBytes;
        }

        /// <summary>
        /// 病人监护仪
        /// </summary>
        /// <param name="Language"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_PatientMonitor(Modle.Operator PatientMonitor)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(PatientMonitor.Status);          //病人监护仪开关

            return dataBytes;
        }

        /// <summary>
        /// 除颤校准
        /// </summary>
        /// <param name="DefibrillationCalibration"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_DefibrillationCalibration(Modle.Operator DefibrillationCalibration)
        {
            byte[] dataBytes = new byte[6];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(DefibrillationCalibration.Status);          //除颤校准开关

            byte[] Bytes = DataConverter.IntToBytes(DefibrillationCalibration.Volume);              //除颤能量

            dataBytes[4] = Bytes[0];
            dataBytes[5] = Bytes[1];

            return dataBytes;
        }

        /// <summary>
        /// 血压校准
        /// </summary>
        /// <param name="BloodPressureCalibration"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_BloodPressureCalibration(Modle.Operator BloodPressureCalibration)
        {
            byte[] dataBytes = new byte[6];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x06;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(BloodPressureCalibration.Status);          //除颤校准开关

            byte[] Bytes = DataConverter.IntToBytes(BloodPressureCalibration.Volume);                   //袖带压力

            dataBytes[4] = Bytes[0];
            dataBytes[5] = Bytes[1];

            return dataBytes;
        }

        /// <summary>
        /// 在线检测
        /// </summary>
        /// <param name="Link"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Link(Modle.Operator Link)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x10;
            dataBytes[1] = 0x07;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(Link.Status);          //病人监护仪开关

            return dataBytes;
        }
    }
}
