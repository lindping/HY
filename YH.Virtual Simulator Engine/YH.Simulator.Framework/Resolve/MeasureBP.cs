using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 血压测量数据字节处理 0x31
    /// </summary>
    static public class MeasureBP
    {
        /// <summary>
        /// 血压  Blood Pressure
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_BloodPressure(byte[] dataBytes, ref Modle.BloodPressure BloodPressure)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x31)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //血压
                    {
                        switch (wordKey3)
                        {
                            case 0x01:      //无创血压
                                byte[] wordData1 = new byte[2];     //收缩压               
                                byte[] wordData2 = new byte[2];     //舒张压
                                byte wordData3 = dataBytes[7];      //血压单位
                                Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);
                                Array.ConstrainedCopy(dataBytes, 5, wordData2, 0, 2);
                                BloodPressure.NIBP.Systolic.Value = DataConverter.BytesToInt32(wordData1);
                                BloodPressure.NIBP.Diastolic.Value = DataConverter.BytesToInt32(wordData2);
                                BloodPressure.NIBP.Diastolic.Unit = BloodPressure.NIBP.Systolic.Unit = DataConverter.ByteToInt32(wordData3);
                                break;
                            case 0x02:      //科罗特科夫音
                                byte wordData4 = dataBytes[3];
                                byte wordData5 = dataBytes[4];
                                byte wordData6 = dataBytes[5];
                                BloodPressure.Korotkoff.SoundVolume = DataConverter.ByteToSoundVolume(wordData4);
                                BloodPressure.Korotkoff.Priority.Status = DataConverter.ByteToControllerStatus(wordData5);
                                BloodPressure.Korotkoff.Priority.Value = DataConverter.ByteToInt32(wordData6);
                                break;
                            default:

                                break;
                        }
                    }
                    break;
                default:

                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="MeasureBP"></param>
        static public void GetData_MeasureBP(byte[] dataBytes, ref Modle.MeasureBP MeasureBP)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x31)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:

                    break;
                case 0x02:      //测量血压
                    {
                        if (wordKey3 == 0x01)
                        {
                            //袖带压力
                            byte[] wordData1 = new byte[2];     //收缩压
                            Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);
                            MeasureBP.CuffPressure = DataConverter.BytesToInt32(wordData1);
                        }
                    }
                    break;
                default:

                    break;
            }
        }

        static public List<byte[]> SetDataBytes_BloodPressure(Modle.BloodPressure BloodPressure)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_NIBP(BloodPressure.NIBP));
            dataBytes_List.Add(SetDataBytes_Korotkoff(BloodPressure.Korotkoff));

            return dataBytes_List;
        }

        /// <summary>
        /// 无创血压
        /// </summary>
        /// <param name="NIBP"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_NIBP(Modle.NIBP NIBP)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x31;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            byte[] Bytes = DataConverter.IntToBytes(NIBP.Systolic.Value);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(NIBP.Diastolic.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(NIBP.Systolic.Unit)[0];


            return dataBytes;
        }

        /// <summary>
        /// 科罗特科夫音
        /// </summary>
        /// <param name="Korotkoff"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Korotkoff(Modle.Korotkoff Korotkoff)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x31;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.SoundVolumeToByte(Korotkoff.SoundVolume);
            dataBytes[4] = DataConverter.ControllerStatusToByte(Korotkoff.Priority.Status);

            return dataBytes;
        }
        /// <summary>
        /// 袖带压力
        /// </summary>
        /// <param name="MeasureBP"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_MeasureBP(Modle.MeasureBP MeasureBP)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x31;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;

            byte[] Bytes = DataConverter.IntToBytes(MeasureBP.CuffPressure);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            return dataBytes;
        }
    }
}
