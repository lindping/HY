using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 生命体征字节处理
    /// </summary>
    static public class VitalSigns
    {

        /// <summary>
        /// 下位机设置生命体征 VitalSigns 0x28
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_VitalSigns(byte[] dataBytes,ref Modle.VitalSigns VitalSigns)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x28)
                return;

            byte wordKey2 = dataBytes[1];

            switch (wordKey2)
            {
                case 0x01:      //循环 Cyclic
                    VitalSigns_Cyclic.SetData_VitalSigns_Cyclic(dataBytes, ref VitalSigns.Cyclic);
                    break;
                case 0x02:      //呼吸 Breath
                    VitalSigns_Breath.SetData_VitalSigns_Breath(dataBytes, ref VitalSigns.Breath);
                    break;
                case 0x03:      //其它 Other
                    VitalSigns_Other.SetData_VitalSigns_Other(dataBytes, ref VitalSigns.Other);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 上位机设置生命体征 VitalSigns 0x28
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="VitalSigns"></param>
        static public void GetData_VitalSigns(byte[] dataBytes, ref Modle.VitalSigns VitalSigns)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x28)
                return;

            byte wordKey2 = dataBytes[1];

            switch (wordKey2)
            {
                case 0x01:      //循环 Cyclic
                    VitalSigns_Cyclic.GetData_VitalSigns_Cyclic(dataBytes, ref VitalSigns.Cyclic);
                    break;
                case 0x02:      //呼吸 Breath
                    VitalSigns_Breath.GetData_VitalSigns_Breath(dataBytes, ref VitalSigns.Breath);
                    break;
                case 0x03:      //其它 Other
                    VitalSigns_Other.GetData_VitalSigns_Other(dataBytes, ref VitalSigns.Other);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 生命体征
        /// </summary>
        /// <param name="VitalSigns"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_VitalSigns(Modle.VitalSigns VitalSigns)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.AddRange(VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic(VitalSigns.Cyclic));
            dataBytes_List.AddRange(VitalSigns_Breath.SetDataBytes_VitalSigns_Breath(VitalSigns.Breath));
            dataBytes_List.AddRange(VitalSigns_Other.SetDataBytes_VitalSigns_Other(VitalSigns.Other));

            return dataBytes_List;
        }

        /// <summary>
        ///  生命体征 VitalSigns  循环 Cyclic
        /// </summary>
        static public class VitalSigns_Cyclic
        {
            /// <summary>
            ///下位机设置 循环 Cyclic
            /// </summary>
            /// <param name="dataBytes"></param>
            static public void SetData_VitalSigns_Cyclic(byte[] dataBytes, ref Modle.Cyclic Cyclic)
            {
                if (dataBytes.Length != 8)
                    return;

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x28)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x01)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {                    
                    case 0x01:      //心律
                        byte wordData1 = dataBytes[3];      //主心律
                        byte wordData2 = dataBytes[4];      //次心律
                        Cyclic.Rhythm.Basic = DataConverter.ByteToInt32(wordData1);
                        Cyclic.Rhythm.Extrasystole = DataConverter.ByteToInt32(wordData2);
                        break;
                    case 0x02:      //心率
                        byte[] wordData3 = new byte[2];     //心率
                        byte wordData4 = dataBytes[5];      //心率单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData3, 0, 2);
                        Cyclic.HeartRate.Value = DataConverter.BytesToInt32(wordData3);
                        Cyclic.HeartRate.Unit = DataConverter.ByteToInt32(wordData4);
                        break;
                    case 0x03:      //血氧饱和度
                        byte[] wordData5 = new byte[2];     //血氧饱和度
                        byte wordData6 = dataBytes[5];      //血氧饱和度单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData5, 0, 2);
                        Cyclic.SpO2.Value = DataConverter.BytesToInt32(wordData5);
                        Cyclic.SpO2.Unit = DataConverter.ByteToInt32(wordData6);
                        break;
                    case 0x04:      //有创血压
                        byte[] wordData7 = new byte[2];     //收缩压               
                        byte[] wordData8 = new byte[2];     //舒张压
                        byte wordData9 = dataBytes[7];      //血压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData7, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData8, 0, 2);
                        Cyclic.IBP.Systolic.Value = DataConverter.BytesToInt32(wordData7);
                        Cyclic.IBP.Diastolic.Value = DataConverter.BytesToInt32(wordData8);
                        Cyclic.IBP.Diastolic.Unit = Cyclic.IBP.Systolic.Unit = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x05:      //肺动脉压
                        byte[] wordData10 = new byte[2];    //收缩期压力                    
                        byte[] wordData11 = new byte[2];    //舒张斯压力
                        byte wordData12 = dataBytes[7];      //肺动脉压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData10, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData11, 0, 2);
                        Cyclic.PAP.Systolic.Value = DataConverter.BytesToInt32(wordData10);
                        Cyclic.PAP.Diastolic.Value = DataConverter.BytesToInt32(wordData11);
                        Cyclic.PAP.Diastolic.Unit = Cyclic.PAP.Systolic.Unit = DataConverter.ByteToInt32(wordData12);
                        break;
                    case 0x06:      //中心静脉压 
                        byte[] wordData13 = new byte[2];    //中心静脉压 
                        byte wordData14 = dataBytes[7];      //中心静脉压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData13, 0, 2);
                        Cyclic.CVP.Value = DataConverter.BytesToInt32(wordData13);
                        Cyclic.CVP.Unit = DataConverter.ByteToInt32(wordData14);
                        break;
                    case 0x07:      //肺毛压 
                        byte[] wordData15 = new byte[2];    //肺毛压  
                        byte wordData16 = dataBytes[5];      //肺毛压 单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData15, 0, 2);
                        Cyclic.PAWP.Value = DataConverter.BytesToInt32(wordData15);
                        Cyclic.PAWP.Unit = DataConverter.ByteToInt32(wordData16);
                        break;
                    case 0x08:      //心输出量 
                        byte[] wordData17 = new byte[2];    //颅内压 
                        byte wordData18 = dataBytes[5];      //颅内压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData17, 0, 2);
                        Cyclic.C_O_.Value = DataConverter.BytesToInt32(wordData17);
                        Cyclic.C_O_.Unit = DataConverter.ByteToInt32(wordData18);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 上位机设置 循环 Cyclic
            /// </summary>
            /// <param name="dataBytes"></param>
            /// <param name="Cyclic"></param>
            static public void GetData_VitalSigns_Cyclic(byte[] dataBytes, ref Modle.Cyclic Cyclic)
            {               

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x28)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x01)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x00:      //全部
                        {                                                                                    
                            //心律Array.ConstrainedCopy(dataBytes_Rhythm, 3, dataBytes, 3, 2);
                            byte wordData1_0 = dataBytes[3];      //主心律
                            byte wordData2_0 = dataBytes[4];      //次心律
                            Cyclic.Rhythm.Basic = DataConverter.ByteToInt32(wordData1_0);
                            Cyclic.Rhythm.Extrasystole = DataConverter.ByteToInt32(wordData2_0);
                            //心率Array.ConstrainedCopy(dataBytes_HeartRate, 3, dataBytes, 5, 3);
                            byte[] wordData3_0 = new byte[2];     //心率
                            byte wordData4_0 = dataBytes[7];      //心率单位
                            Array.ConstrainedCopy(dataBytes, 5, wordData3_0, 0, 2);
                            Cyclic.HeartRate.Value = DataConverter.BytesToInt32(wordData3_0);
                            Cyclic.HeartRate.Unit = DataConverter.ByteToInt32(wordData4_0);
                            //血氧饱和度Array.ConstrainedCopy(dataBytes_SpO2, 3, dataBytes, 8, 3);
                            byte[] wordData5_0 = new byte[2];     //血氧饱和度
                            byte wordData6_0 = dataBytes[10];      //血氧饱和度单位
                            Array.ConstrainedCopy(dataBytes, 8, wordData5_0, 0, 2);
                            Cyclic.SpO2.Value = DataConverter.BytesToInt32(wordData5_0);
                            Cyclic.SpO2.Unit = DataConverter.ByteToInt32(wordData6_0);
                            //有创血压Array.ConstrainedCopy(dataBytes_IBP, 3, dataBytes, 11, 5);
                            byte[] wordData7_0 = new byte[2];     //收缩压               
                            byte[] wordData8_0 = new byte[2];     //舒张压
                            byte wordData9_0 = dataBytes[15];      //血压单位
                            Array.ConstrainedCopy(dataBytes, 11, wordData7_0, 0, 2);
                            Array.ConstrainedCopy(dataBytes, 13, wordData8_0, 0, 2);
                            Cyclic.IBP.Systolic.Value = DataConverter.BytesToInt32(wordData7_0);
                            Cyclic.IBP.Diastolic.Value = DataConverter.BytesToInt32(wordData8_0);
                            Cyclic.IBP.Diastolic.Unit = Cyclic.IBP.Systolic.Unit = DataConverter.ByteToInt32(wordData9_0);
                            //肺动脉压Array.ConstrainedCopy(dataBytes_PAP, 3, dataBytes, 16, 5);
                            byte[] wordData10_0 = new byte[2];    //收缩期压力                    
                            byte[] wordData11_0 = new byte[2];    //舒张斯压力
                            byte wordData12_0 = dataBytes[20];      //肺动脉压单位
                            Array.ConstrainedCopy(dataBytes, 16, wordData10_0, 0, 2);
                            Array.ConstrainedCopy(dataBytes, 18, wordData11_0, 0, 2);
                            Cyclic.PAP.Systolic.Value = DataConverter.BytesToInt32(wordData10_0);
                            Cyclic.PAP.Diastolic.Value = DataConverter.BytesToInt32(wordData11_0);
                            Cyclic.PAP.Diastolic.Unit = Cyclic.PAP.Systolic.Unit = DataConverter.ByteToInt32(wordData12_0);
                            //中心静脉压 Array.ConstrainedCopy(dataBytes_CVP, 3, dataBytes, 21, 3);
                            byte[] wordData13_0 = new byte[2];    //中心静脉压 
                            byte wordData14_0 = dataBytes[23];      //中心静脉压单位
                            Array.ConstrainedCopy(dataBytes, 21, wordData13_0, 0, 2);
                            Cyclic.CVP.Value = DataConverter.BytesToInt32(wordData13_0);
                            Cyclic.CVP.Unit = DataConverter.ByteToInt32(wordData14_0);
                            //肺毛压  Array.ConstrainedCopy(dataBytes_PAWP, 3, dataBytes, 24, 3);
                            byte[] wordData15_0 = new byte[2];    //肺毛压  
                            byte wordData16_0 = dataBytes[26];      //肺毛压 单位
                            Array.ConstrainedCopy(dataBytes, 24, wordData15_0, 0, 2);
                            Cyclic.PAWP.Value = DataConverter.BytesToInt32(wordData15_0);
                            Cyclic.PAWP.Unit = DataConverter.ByteToInt32(wordData16_0);
                            //心输出量 Array.ConstrainedCopy(dataBytes_C_O_, 3, dataBytes, 30, 3);
                            byte[] wordData17_0 = new byte[2];    //心输出量 
                            byte wordData18_0 = dataBytes[29];      //心输出量单位
                            Array.ConstrainedCopy(dataBytes, 27, wordData17_0, 0, 2);
                            Cyclic.C_O_.Value = DataConverter.BytesToInt32(wordData17_0);
                            Cyclic.C_O_.Unit = DataConverter.ByteToInt32(wordData18_0);
                        }
                        break;
                    case 0x01:      //心律
                        byte wordData1 = dataBytes[3];      //主心律
                        byte wordData2 = dataBytes[4];      //次心律
                        Cyclic.Rhythm.Basic = DataConverter.ByteToInt32(wordData1);
                        Cyclic.Rhythm.Extrasystole = DataConverter.ByteToInt32(wordData2);
                        break;
                    case 0x02:      //心率
                        byte[] wordData3 = new byte[2];     //心率
                        byte wordData4 = dataBytes[5];      //心率单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData3, 0, 2);
                        Cyclic.HeartRate.Value = DataConverter.BytesToInt32(wordData3);
                        Cyclic.HeartRate.Unit = DataConverter.ByteToInt32(wordData4);
                        break;
                    case 0x03:      //血氧饱和度
                        byte[] wordData5 = new byte[2];     //血氧饱和度
                        byte wordData6 = dataBytes[5];      //血氧饱和度单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData5, 0, 2);
                        Cyclic.SpO2.Value = DataConverter.BytesToInt32(wordData5);
                        Cyclic.SpO2.Unit = DataConverter.ByteToInt32(wordData6);
                        break;
                    case 0x04:      //有创血压
                        byte[] wordData7 = new byte[2];     //收缩压               
                        byte[] wordData8 = new byte[2];     //舒张压
                        byte wordData9 = dataBytes[7];      //血压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData7, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData8, 0, 2);
                        Cyclic.IBP.Systolic.Value = DataConverter.BytesToInt32(wordData7);
                        Cyclic.IBP.Diastolic.Value = DataConverter.BytesToInt32(wordData8);
                        Cyclic.IBP.Diastolic.Unit = Cyclic.IBP.Systolic.Unit = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x05:      //肺动脉压
                        byte[] wordData10 = new byte[2];    //收缩期压力                    
                        byte[] wordData11 = new byte[2];    //舒张斯压力
                        byte wordData12 = dataBytes[7];      //肺动脉压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData10, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData11, 0, 2);
                        Cyclic.PAP.Systolic.Value = DataConverter.BytesToInt32(wordData10);
                        Cyclic.PAP.Diastolic.Value = DataConverter.BytesToInt32(wordData11);
                        Cyclic.PAP.Diastolic.Unit = Cyclic.PAP.Systolic.Unit = DataConverter.ByteToInt32(wordData12);
                        break;
                    case 0x06:      //中心静脉压 
                        byte[] wordData13 = new byte[2];    //中心静脉压 
                        byte wordData14 = dataBytes[7];      //中心静脉压单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData13, 0, 2);
                        Cyclic.CVP.Value = DataConverter.BytesToInt32(wordData13);
                        Cyclic.CVP.Unit = DataConverter.ByteToInt32(wordData14);
                        break;
                    case 0x07:      //肺毛压 
                        byte[] wordData15 = new byte[2];    //肺毛压  
                        byte wordData16 = dataBytes[5];      //肺毛压 单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData15, 0, 2);
                        Cyclic.PAWP.Value = DataConverter.BytesToInt32(wordData15);
                        Cyclic.PAWP.Unit = DataConverter.ByteToInt32(wordData16);
                        break;
                    case 0x08:      //心输出量 
                        byte[] wordData17 = new byte[2];    //心输出量 
                        byte wordData18 = dataBytes[5];      //心输出量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData17, 0, 2);
                        Cyclic.C_O_.Value = DataConverter.BytesToInt32(wordData17);
                        Cyclic.C_O_.Unit = DataConverter.ByteToInt32(wordData18);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 生命体征 - 循环
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public List<byte[]> SetDataBytes_VitalSigns_Cyclic(Modle.Cyclic Cyclic)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_Rhythm(Cyclic.Rhythm));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_HeartRate(Cyclic.HeartRate));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_SpO2(Cyclic.SpO2));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_IBP(Cyclic.IBP));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_PAP(Cyclic.PAP));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_CVP(Cyclic.CVP));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_PAWP(Cyclic.PAWP));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Cyclic_C_O_(Cyclic.C_O_));

                return dataBytes_List;
            }

            /// <summary>
            /// 心律 Rhythm
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_Rhythm(Modle.Rhythm Rhythm)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x01;
                dataBytes[3] = DataConverter.IntToBytes(Rhythm.Basic)[0];
                dataBytes[4] = DataConverter.IntToBytes(Rhythm.Extrasystole)[0];


                return dataBytes;
            }

            /// <summary>
            /// 心律 HeartRate
            /// </summary>
            /// <param name="HeartRate"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_HeartRate(Modle.DataValue HeartRate)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x02;

                byte[] Bytes = DataConverter.IntToBytes(HeartRate.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(HeartRate.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 血氧饱和度
            /// </summary>
            /// <param name="SpO2"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_SpO2(Modle.DataValue SpO2)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x03;

                byte[] Bytes = DataConverter.IntToBytes(SpO2.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(SpO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 有创血压
            /// </summary>
            /// <param name="IBP"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_IBP(Modle.IBP IBP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x04;
                byte[] Bytes = DataConverter.IntToBytes(IBP.Systolic.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(IBP.Diastolic.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(IBP.Systolic.Unit)[0];


                return dataBytes;
            }

            /// <summary>
            /// 肺动脉压
            /// </summary>
            /// <param name="PAP"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_PAP(Modle.PAP PAP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x05;
                byte[] Bytes = DataConverter.IntToBytes(PAP.Systolic.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(PAP.Diastolic.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(PAP.Systolic.Unit)[0];


                return dataBytes;
            }

            /// <summary>
            /// 中心静脉压
            /// </summary>
            /// <param name="CVP"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_CVP(Modle.DataValue CVP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x06;

                byte[] Bytes = DataConverter.IntToBytes(CVP.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                dataBytes[5] = DataConverter.IntToBytes(CVP.Unit)[0];


                return dataBytes;
            }

            /// <summary>
            /// 肺毛压
            /// </summary>
            /// <param name="PAWP"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_PAWP(Modle.DataValue PAWP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x07;

                byte[] Bytes = DataConverter.IntToBytes(PAWP.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                dataBytes[5] = DataConverter.IntToBytes(PAWP.Unit)[0];


                return dataBytes;
            }
            
            /// <summary>
            /// 心输出量
            /// </summary>
            /// <param name="C_O_"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Cyclic_C_O_(Modle.DataValue C_O_)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x08;

                byte[] Bytes = DataConverter.IntToBytes(C_O_.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                dataBytes[5] = DataConverter.IntToBytes(C_O_.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 循环全部
            /// </summary>
            /// <param name="VitalSigns"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic(Modle.Cyclic Cyclic)
            {
                byte[] dataBytes = new byte[33];
                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x00;

                byte[] dataBytes_Rhythm = GetDataBytes_VitalSigns_Cyclic_Rhythm(Cyclic.Rhythm);
                byte[] dataBytes_HeartRate = GetDataBytes_VitalSigns_Cyclic_HeartRate(Cyclic.HeartRate);
                byte[] dataBytes_SpO2 = GetDataBytes_VitalSigns_Cyclic_SpO2(Cyclic.SpO2);
                byte[] dataBytes_IBP = GetDataBytes_VitalSigns_Cyclic_IBP(Cyclic.IBP);
                byte[] dataBytes_PAP = GetDataBytes_VitalSigns_Cyclic_PAP(Cyclic.PAP);
                byte[] dataBytes_CVP = GetDataBytes_VitalSigns_Cyclic_CVP(Cyclic.CVP);
                byte[] dataBytes_PAWP = GetDataBytes_VitalSigns_Cyclic_PAWP(Cyclic.PAWP);                
                byte[] dataBytes_C_O_ = GetDataBytes_VitalSigns_Cyclic_C_O_(Cyclic.C_O_);

                Array.ConstrainedCopy(dataBytes_Rhythm, 3, dataBytes, 3, 2);
                Array.ConstrainedCopy(dataBytes_HeartRate, 3, dataBytes, 5, 3);
                Array.ConstrainedCopy(dataBytes_SpO2, 3, dataBytes, 8, 3);
                Array.ConstrainedCopy(dataBytes_IBP, 3, dataBytes, 11, 5);
                Array.ConstrainedCopy(dataBytes_PAP, 3, dataBytes, 16, 5);
                Array.ConstrainedCopy(dataBytes_CVP, 3, dataBytes, 21, 3);
                Array.ConstrainedCopy(dataBytes_PAWP, 3, dataBytes, 24, 3);
                Array.ConstrainedCopy(dataBytes_C_O_, 3, dataBytes, 27, 3);

                return dataBytes;
            }

            /// <summary>
            /// 心律
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_Rhythm(Modle.Rhythm Rhythm)
            {
                byte[] dataBytes = new byte[5];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x01;

                dataBytes[3] = DataConverter.IntToBytes(Rhythm.Basic)[0];
                dataBytes[4] = DataConverter.IntToBytes(Rhythm.Extrasystole)[0];

                return dataBytes;
            }


            /// <summary>
            /// 心率
            /// </summary>
            /// <param name="HeartRate"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_HeartRate(Modle.DataValue HeartRate)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x02;

                byte[] Bytes = DataConverter.IntToBytes(HeartRate.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(HeartRate.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 血氧饱和度 
            /// </summary>
            /// <param name="SpO2"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_SpO2(Modle.DataValue SpO2)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x03;

                byte[] Bytes = DataConverter.IntToBytes(SpO2.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(SpO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 有创血压
            /// </summary>
            /// <param name="IBP"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_IBP(Modle.IBP IBP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x04;

                byte[] Bytes = DataConverter.IntToBytes(IBP.Systolic.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(IBP.Diastolic.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(IBP.Systolic.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 肺动脉压
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_PAP(Modle.PAP PAP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x04;

                byte[] Bytes = DataConverter.IntToBytes(PAP.Systolic.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(PAP.Diastolic.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(PAP.Systolic.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 中心静脉压
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_CVP(Modle.DataValue CVP)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x06;

                byte[] Bytes = DataConverter.IntToBytes(CVP.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(CVP.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 肺毛压
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_PAWP(Modle.DataValue PAWP)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x07;

                byte[] Bytes = DataConverter.IntToBytes(PAWP.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(PAWP.Unit)[0];

                return dataBytes;
            }
            
            /// <summary>
            /// 心输出量
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Cyclic_C_O_(Modle.DataValue C_O_)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x08;

                byte[] Bytes = DataConverter.IntToBytes(C_O_.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(C_O_.Unit)[0];

                return dataBytes;
            }
        }

        /// <summary>
        /// 生命体征 VitalSigns 呼吸 Breath
        /// </summary>
        static public class VitalSigns_Breath
        {
            /// <summary>
            /// 下位机设置 呼吸 Breath
            /// </summary>
            /// <param name="dataBytes"></param>
            static public void SetData_VitalSigns_Breath(byte[] dataBytes, ref Modle.Breath Breath)
            {
                if (dataBytes.Length != 8)
                    return;

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x28)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x02)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x01:      //呼吸类型
                        byte wordData1 = dataBytes[3];
                        Breath.RespType = DataConverter.ByteToInt32(wordData1);
                        break;
                    case 0x02:      //呼吸率
                        byte[] wordData2 = new byte[2];         //呼吸率
                        byte wordData3 = dataBytes[5];          //呼吸率单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData2, 0, 2);
                        Breath.RespRate.Value = DataConverter.BytesToInt32(wordData2);
                        Breath.RespRate.Unit = DataConverter.ByteToInt32(wordData3);
                        break;
                    case 0x03:      //吸气量
                        byte[] wordData4 = new byte[2];         //吸气量
                        byte wordData5 = dataBytes[5];          //吸气量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData4, 0, 2);
                        Breath.InspiratoryCapacity.Value = DataConverter.BytesToInt32(wordData4);
                        Breath.InspiratoryCapacity.Unit = DataConverter.ByteToInt32(wordData5);
                        break;
                    case 0x04:      //呼吸比
                        byte[] wordData6 = new byte[2];         //呼吸比
                        byte wordData7 = dataBytes[5];          //呼吸比单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData6, 0, 2);
                        Breath.RespRatio.Value = DataConverter.BytesToInt32(wordData6);
                        Breath.RespRatio.Unit = DataConverter.ByteToInt32(wordData7);
                        break;
                    case 0x05:      //二氧化碳
                        byte[] wordData8 = new byte[2];         //二氧化碳
                        byte wordData9 = dataBytes[5];          //二氧化碳单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData8, 0, 2);
                        Breath.CO2.Value = DataConverter.BytesToInt32(wordData8);
                        Breath.CO2.Unit = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x06:      //呼气末二氧化碳
                        byte[] wordData10 = new byte[2];         //呼气末二氧化碳
                        byte wordData11 = dataBytes[5];          //呼气末二氧化碳单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData10, 0, 2);
                        Breath.ETCO2.Value = DataConverter.BytesToInt32(wordData10);
                        Breath.ETCO2.Unit = DataConverter.ByteToInt32(wordData11);
                        break;
                    case 0x07:      //氧气
                        byte[] wordData12 = new byte[2];        //吸收的氧气
                        byte[] wordData13 = new byte[2];        //呼出的氧气
                        byte wordData14 = dataBytes[7];
                        Array.ConstrainedCopy(dataBytes, 3, wordData12, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData13, 0, 2);
                        Breath.O2.inO2.Value = DataConverter.BytesToInt32(wordData12);
                        Breath.O2.exO2.Value = DataConverter.BytesToInt32(wordData13);
                        Breath.O2.exO2.Unit = Breath.O2.inO2.Unit = DataConverter.ByteToInt32(wordData14);
                        break;
                    case 0x08:      //笑气
                        byte[] wordData15 = new byte[2];        //吸收的笑气
                        byte[] wordData16 = new byte[2];        //呼出的笑气
                        byte wordData17 = dataBytes[7];
                        Array.ConstrainedCopy(dataBytes, 3, wordData15, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData16, 0, 2);
                        Breath.N2O.inN2O.Value = DataConverter.BytesToInt32(wordData15);
                        Breath.N2O.exN2O.Value = DataConverter.BytesToInt32(wordData16);
                        Breath.N2O.exN2O.Unit = Breath.N2O.inN2O.Unit = DataConverter.ByteToInt32(wordData17);
                        break;
                    case 0x09:      //麻醉剂
                        byte[] wordData18 = new byte[2];        //吸收的麻醉剂
                        byte[] wordData19 = new byte[2];        //呼出的麻醉剂
                        byte wordData20 = dataBytes[7];
                        Array.ConstrainedCopy(dataBytes, 3, wordData18, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData19, 0, 2);
                        Breath.AGT.inAGT.Value = DataConverter.BytesToInt32(wordData18);
                        Breath.AGT.exAGT.Value = DataConverter.BytesToInt32(wordData19);
                        Breath.AGT.exAGT.Unit = Breath.AGT.inAGT.Unit = DataConverter.ByteToInt32(wordData20);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 上位机设置 呼吸 Breath
            /// </summary>
            /// <param name="dataBytes"></param>
            /// <param name="Breath"></param>
            static public void GetData_VitalSigns_Breath(byte[] dataBytes, ref Modle.Breath Breath)
            {

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x28)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x02)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x00:      //全部
                        {
                            //呼吸类型Array.ConstrainedCopy(dataBytes_RespType, 3, dataBytes, 3, 1);
                            byte wordData1_0 = dataBytes[3];
                            Breath.RespType = DataConverter.ByteToInt32(wordData1_0);
                            //呼吸率Array.ConstrainedCopy(dataBytes_RespRate, 3, dataBytes, 4, 3);
                            byte[] wordData2_0 = new byte[2];         //呼吸率
                            byte wordData3_0 = dataBytes[6];          //呼吸率单位
                            Array.ConstrainedCopy(dataBytes, 4, wordData2_0, 0, 2);
                            Breath.RespRate.Value = DataConverter.BytesToInt32(wordData2_0);
                            Breath.RespRate.Unit = DataConverter.ByteToInt32(wordData3_0);
                            //吸气量Array.ConstrainedCopy(dataBytes_InspiratoryCapacity, 3, dataBytes, 7, 3);
                            byte[] wordData4_0 = new byte[2];         //吸气量
                            byte wordData5_0 = dataBytes[9];          //吸气量单位
                            Array.ConstrainedCopy(dataBytes, 7, wordData4_0, 0, 2);
                            Breath.InspiratoryCapacity.Value = DataConverter.BytesToInt32(wordData4_0);
                            Breath.InspiratoryCapacity.Unit = DataConverter.ByteToInt32(wordData5_0);
                            //呼吸比Array.ConstrainedCopy(dataBytes_RespRatio, 3, dataBytes, 10, 3);
                            byte[] wordData6_0 = new byte[2];         //呼吸比
                            byte wordData7_0 = dataBytes[12];          //呼吸比单位
                            Array.ConstrainedCopy(dataBytes, 10, wordData6_0, 0, 2);
                            Breath.RespRatio.Value = DataConverter.BytesToInt32(wordData6_0);
                            Breath.RespRatio.Unit = DataConverter.ByteToInt32(wordData7_0);
                            //二氧化碳Array.ConstrainedCopy(dataBytes_CO2, 3, dataBytes, 13, 3);
                            byte[] wordData8_0 = new byte[2];         //二氧化碳
                            byte wordData9_0 = dataBytes[15];          //二氧化碳单位
                            Array.ConstrainedCopy(dataBytes, 13, wordData8_0, 0, 2);
                            Breath.CO2.Value = DataConverter.BytesToInt32(wordData8_0);
                            Breath.CO2.Unit = DataConverter.ByteToInt32(wordData9_0);
                            //呼气末二氧化碳 Array.ConstrainedCopy(dataBytes_ETCO2, 3, dataBytes, 16, 3);
                            byte[] wordData10_0 = new byte[2];         //呼气末二氧化碳
                            byte wordData11_0 = dataBytes[18];          //呼气末二氧化碳单位
                            Array.ConstrainedCopy(dataBytes, 16, wordData10_0, 0, 2);
                            Breath.ETCO2.Value = DataConverter.BytesToInt32(wordData10_0);
                            Breath.ETCO2.Unit = DataConverter.ByteToInt32(wordData11_0);
                            //氧气Array.ConstrainedCopy(dataBytes_O2, 3, dataBytes, 19, 5);
                            byte[] wordData12_0 = new byte[2];        //吸收的氧气
                            byte[] wordData13_0 = new byte[2];        //呼出的氧气
                            byte wordData14_0 = dataBytes[23];
                            Array.ConstrainedCopy(dataBytes, 19, wordData12_0, 0, 2);
                            Array.ConstrainedCopy(dataBytes, 21, wordData13_0, 0, 2);
                            Breath.O2.inO2.Value = DataConverter.BytesToInt32(wordData12_0);
                            Breath.O2.exO2.Value = DataConverter.BytesToInt32(wordData13_0);
                            Breath.O2.exO2.Unit = Breath.O2.inO2.Unit = DataConverter.ByteToInt32(wordData14_0);
                            //笑气Array.ConstrainedCopy(dataBytes_N2O, 3, dataBytes, 24, 5);
                            byte[] wordData15_0 = new byte[2];        //吸收的笑气
                            byte[] wordData16_0 = new byte[2];        //呼出的笑气
                            byte wordData17_0 = dataBytes[28];
                            Array.ConstrainedCopy(dataBytes, 24, wordData15_0, 0, 2);
                            Array.ConstrainedCopy(dataBytes, 26, wordData16_0, 0, 2);
                            Breath.N2O.inN2O.Value = DataConverter.BytesToInt32(wordData15_0);
                            Breath.N2O.exN2O.Value = DataConverter.BytesToInt32(wordData16_0);
                            Breath.N2O.exN2O.Unit = Breath.N2O.inN2O.Unit = DataConverter.ByteToInt32(wordData17_0);
                            //麻醉剂Array.ConstrainedCopy(dataBytes_AGT, 3, dataBytes, 29, 5);
                            byte[] wordData18_0 = new byte[2];        //吸收的麻醉剂
                            byte[] wordData19_0 = new byte[2];        //呼出的麻醉剂
                            byte wordData20_0 = dataBytes[33];
                            Array.ConstrainedCopy(dataBytes, 29, wordData18_0, 0, 2);
                            Array.ConstrainedCopy(dataBytes, 31, wordData19_0, 0, 2);
                            Breath.AGT.inAGT.Value = DataConverter.BytesToInt32(wordData18_0);
                            Breath.AGT.exAGT.Value = DataConverter.BytesToInt32(wordData19_0);
                            Breath.AGT.exAGT.Unit = Breath.AGT.inAGT.Unit = DataConverter.ByteToInt32(wordData20_0);
                        }
                        break;
                    case 0x01:      //呼吸类型
                        byte wordData1 = dataBytes[3];
                        Breath.RespType = DataConverter.ByteToInt32(wordData1);
                        break;
                    case 0x02:      //呼吸率
                        byte[] wordData2 = new byte[2];         //呼吸率
                        byte wordData3 = dataBytes[5];          //呼吸率单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData2, 0, 2);
                        Breath.RespRate.Value = DataConverter.BytesToInt32(wordData2);
                        Breath.RespRate.Unit = DataConverter.ByteToInt32(wordData3);
                        break;
                    case 0x03:      //吸气量
                        byte[] wordData4 = new byte[2];         //吸气量
                        byte wordData5 = dataBytes[5];          //吸气量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData4, 0, 2);
                        Breath.InspiratoryCapacity.Value = DataConverter.BytesToInt32(wordData4);
                        Breath.InspiratoryCapacity.Unit = DataConverter.ByteToInt32(wordData5);
                        break;
                    case 0x04:      //呼吸比
                        byte[] wordData6 = new byte[2];         //呼吸比
                        byte wordData7 = dataBytes[5];          //呼吸比单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData6, 0, 2);
                        Breath.RespRatio.Value = DataConverter.BytesToInt32(wordData6);
                        Breath.RespRatio.Unit = DataConverter.ByteToInt32(wordData7);
                        break;
                    case 0x05:      //二氧化碳
                        byte[] wordData8 = new byte[2];         //二氧化碳
                        byte wordData9 = dataBytes[5];          //二氧化碳单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData8, 0, 2);
                        Breath.CO2.Value = DataConverter.BytesToInt32(wordData8);
                        Breath.CO2.Unit = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x06:      //呼气末二氧化碳
                        byte[] wordData10 = new byte[2];         //呼气末二氧化碳
                        byte wordData11 = dataBytes[5];          //呼气末二氧化碳单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData10, 0, 2);
                        Breath.ETCO2.Value = DataConverter.BytesToInt32(wordData10);
                        Breath.ETCO2.Unit = DataConverter.ByteToInt32(wordData11);
                        break;
                    case 0x07:      //氧气
                        byte[] wordData12 = new byte[2];        //吸收的氧气
                        byte[] wordData13 = new byte[2];        //呼出的氧气
                        byte wordData14 = dataBytes[7];
                        Array.ConstrainedCopy(dataBytes, 3, wordData12, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData13, 0, 2);
                        Breath.O2.inO2.Value = DataConverter.BytesToInt32(wordData12);
                        Breath.O2.exO2.Value = DataConverter.BytesToInt32(wordData13);
                        Breath.O2.exO2.Unit = Breath.O2.inO2.Unit = DataConverter.ByteToInt32(wordData14);
                        break;
                    case 0x08:      //笑气
                        byte[] wordData15 = new byte[2];        //吸收的笑气
                        byte[] wordData16 = new byte[2];        //呼出的笑气
                        byte wordData17 = dataBytes[7];
                        Array.ConstrainedCopy(dataBytes, 3, wordData15, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData16, 0, 2);
                        Breath.N2O.inN2O.Value = DataConverter.BytesToInt32(wordData15);
                        Breath.N2O.exN2O.Value = DataConverter.BytesToInt32(wordData16);
                        Breath.N2O.exN2O.Unit = Breath.N2O.inN2O.Unit = DataConverter.ByteToInt32(wordData17);
                        break;
                    case 0x09:      //麻醉剂
                        byte[] wordData18 = new byte[2];        //吸收的麻醉剂
                        byte[] wordData19 = new byte[2];        //呼出的麻醉剂
                        byte wordData20 = dataBytes[7];
                        Array.ConstrainedCopy(dataBytes, 3, wordData18, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData19, 0, 2);
                        Breath.AGT.inAGT.Value = DataConverter.BytesToInt32(wordData18);
                        Breath.AGT.exAGT.Value = DataConverter.BytesToInt32(wordData19);
                        Breath.AGT.exAGT.Unit = Breath.AGT.inAGT.Unit = DataConverter.ByteToInt32(wordData20);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 生命体征 - 呼吸
            /// </summary>
            /// <param name="Breath"></param>
            /// <returns></returns>
            static public List<byte[]> SetDataBytes_VitalSigns_Breath(Modle.Breath Breath)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_RespType(Breath.RespType));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_RespRate(Breath.RespRate));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_InspiratoryCapacity(Breath.InspiratoryCapacity));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_RespRatio(Breath.RespRatio));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_CO2(Breath.CO2));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_ETCO2(Breath.ETCO2));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_O2(Breath.O2));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_N2O(Breath.N2O));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Breath_AGT(Breath.AGT));

                return dataBytes_List;
            }
            /// <summary>
            /// 呼吸类型
            /// </summary>
            /// <param name="RespType"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_RespType(int RespType)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x01;                           

                dataBytes[3] = DataConverter.IntToBytes(RespType)[0];

                return dataBytes;
            }

            /// <summary>
            /// 呼吸率
            /// </summary>
            /// <param name="RespRate"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_RespRate(Modle.DataValue RespRate)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x02;

                byte[] Bytes = DataConverter.IntToBytes(RespRate.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(RespRate.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 吸气量 Inspiratory Capacity
            /// </summary>
            /// <param name="InspiratoryCapacity"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_InspiratoryCapacity(Modle.DataValue InspiratoryCapacity)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x03;

                byte[] Bytes = DataConverter.IntToBytes(InspiratoryCapacity.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(InspiratoryCapacity.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 呼吸比 Respiration ratio
            /// </summary>
            /// <param name="RespRatio"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_RespRatio(Modle.DataValue RespRatio)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x04;

                byte[] Bytes = DataConverter.IntToBytes(RespRatio.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(RespRatio.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 二氧化碳
            /// </summary>
            /// <param name="CO2"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_CO2(Modle.DataValue CO2)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x05;

                byte[] Bytes = DataConverter.IntToBytes(CO2.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(CO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 呼气末二氧化碳
            /// </summary>
            /// <param name="ETCO2"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_ETCO2(Modle.DataValue ETCO2)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x06;

                byte[] Bytes = DataConverter.IntToBytes(ETCO2.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(ETCO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 氧气 Inhaled Oxygen
            /// </summary>
            /// <param name="inO2"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_O2(Modle.O2 O2)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x07;

                byte[] Bytes = DataConverter.IntToBytes(O2.inO2.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(O2.exO2.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(O2.inO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 笑气
            /// </summary>
            /// <param name="exO2"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_N2O(Modle.N2O N2O)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x08;

                byte[] Bytes = DataConverter.IntToBytes(N2O.inN2O.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(N2O.exN2O.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(N2O.inN2O.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 麻醉剂
            /// </summary>
            /// <param name="inN2O"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Breath_AGT(Modle.AGT AGT)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x09;

                byte[] Bytes = DataConverter.IntToBytes(AGT.inAGT.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                byte[] Bytes2 = DataConverter.IntToBytes(AGT.exAGT.Value);

                dataBytes[5] = Bytes2[0];
                dataBytes[6] = Bytes2[1];

                dataBytes[7] = DataConverter.IntToBytes(AGT.inAGT.Unit)[0];

                return dataBytes;
            }
            /// <summary>
            /// 呼吸全部
            /// </summary>
            /// <param name="VitalSigns"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath(Modle.Breath Breath)
            {
                byte[] dataBytes = new byte[34];
                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x00;

                byte[] dataBytes_RespType = GetDataBytes_VitalSigns_Breath_RespType(Breath.RespType);
                byte[] dataBytes_RespRate = GetDataBytes_VitalSigns_Breath_RespRate(Breath.RespRate);
                byte[] dataBytes_InspiratoryCapacity = GetDataBytes_VitalSigns_Breath_InspiratoryCapacity(Breath.InspiratoryCapacity);
                byte[] dataBytes_RespRatio = GetDataBytes_VitalSigns_Breath_RespRatio(Breath.RespRatio);
                byte[] dataBytes_CO2 = GetDataBytes_VitalSigns_Breath_CO2(Breath.CO2);
                byte[] dataBytes_ETCO2 = GetDataBytes_VitalSigns_Breath_ETCO2(Breath.ETCO2);
                byte[] dataBytes_O2 = GetDataBytes_VitalSigns_Breath_O2(Breath.O2);
                byte[] dataBytes_N2O = GetDataBytes_VitalSigns_Breath_N2O(Breath.N2O);
                byte[] dataBytes_AGT = GetDataBytes_VitalSigns_Breath_AGT(Breath.AGT);

                Array.ConstrainedCopy(dataBytes_RespType, 3, dataBytes, 3, 1);
                Array.ConstrainedCopy(dataBytes_RespRate, 3, dataBytes, 4, 3);
                Array.ConstrainedCopy(dataBytes_InspiratoryCapacity, 3, dataBytes, 7, 3);
                Array.ConstrainedCopy(dataBytes_RespRatio, 3, dataBytes, 10, 3);
                Array.ConstrainedCopy(dataBytes_CO2, 3, dataBytes, 13, 3);
                Array.ConstrainedCopy(dataBytes_ETCO2, 3, dataBytes, 16, 3);
                Array.ConstrainedCopy(dataBytes_O2, 3, dataBytes, 19, 5);
                Array.ConstrainedCopy(dataBytes_N2O, 3, dataBytes, 24, 5);
                Array.ConstrainedCopy(dataBytes_AGT, 3, dataBytes, 29, 5);

                return dataBytes;
            }

            /// <summary>
            /// 呼吸类型 Respiration Type
            /// </summary>
            /// <param name="RespType"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_RespType(int RespType)
            {
                byte[] dataBytes = new byte[4];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x01;

                dataBytes[3] = DataConverter.IntToBytes(RespType)[0];

                return dataBytes;
            }

            /// <summary>
            /// 呼吸率 Respiration  Rate
            /// </summary>
            /// <param name="RespRate"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_RespRate(Modle.DataValue RespRate)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x02;

                dataBytes[3] = DataConverter.IntToBytes(RespRate.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(RespRate.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(RespRate.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 吸气量 Inspiratory Capacity
            /// </summary>
            /// <param name="Breath"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_InspiratoryCapacity(Modle.DataValue InspiratoryCapacity)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x03;

                dataBytes[3] = DataConverter.IntToBytes(InspiratoryCapacity.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(InspiratoryCapacity.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(InspiratoryCapacity.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 呼吸比 Respiration ratio
            /// </summary>
            /// <param name="RespRatio"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_RespRatio(Modle.DataValue RespRatio)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x04;

                dataBytes[3] = DataConverter.IntToBytes(RespRatio.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(RespRatio.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(RespRatio.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 二氧化碳 CO2
            /// </summary>
            /// <param name="CO2"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_CO2(Modle.DataValue CO2)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x05;

                dataBytes[3] = DataConverter.IntToBytes(CO2.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(CO2.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(CO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 呼气末二氧化碳 ETCO2
            /// </summary>
            /// <param name="Breath"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_ETCO2(Modle.DataValue ETCO2)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x06;

                dataBytes[3] = DataConverter.IntToBytes(ETCO2.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(ETCO2.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(ETCO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 氧气 O2 0x07
            /// </summary>
            /// <param name="Breath"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_O2(Modle.O2 O2)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x07;

                dataBytes[3] = DataConverter.IntToBytes(O2.inO2.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(O2.inO2.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(O2.exO2.Value)[0];
                dataBytes[6] = DataConverter.IntToBytes(O2.exO2.Value)[1];
                dataBytes[7] = DataConverter.IntToBytes(O2.inO2.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 笑气 N2O 0x08
            /// </summary>
            /// <param name="Breath"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_N2O(Modle.N2O N2O)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x08;

                dataBytes[3] = DataConverter.IntToBytes(N2O.inN2O.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(N2O.inN2O.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(N2O.exN2O.Value)[0];
                dataBytes[6] = DataConverter.IntToBytes(N2O.exN2O.Value)[1];
                dataBytes[7] = DataConverter.IntToBytes(N2O.inN2O.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 麻醉剂 AGT 0x09
            /// </summary>
            /// <param name="Breath"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Breath_AGT(Modle.AGT AGT)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x09;

                dataBytes[3] = DataConverter.IntToBytes(AGT.inAGT.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(AGT.inAGT.Value)[1];
                dataBytes[5] = DataConverter.IntToBytes(AGT.exAGT.Value)[0];
                dataBytes[6] = DataConverter.IntToBytes(AGT.exAGT.Value)[1];
                dataBytes[7] = DataConverter.IntToBytes(AGT.inAGT.Unit)[0];

                return dataBytes;
            }


        }

        /// <summary>
        /// 生命体征 VitalSigns  其它 Other
        /// </summary>
        static public class VitalSigns_Other
        {
            /// <summary>
            /// 下位机设置 其它 Other
            /// </summary>
            /// <param name="dataBytes"></param>
            static public void SetData_VitalSigns_Other(byte[] dataBytes, ref Modle.Other Other)
            {
                if (dataBytes.Length != 8)
                    return;

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x28)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x03)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x01:      //外周温度
                        byte[] wordData1 = new byte[2];
                        byte wordData2 = dataBytes[5];
                        Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);   //外周温度
                        Other.PeripheralTemperature.Value = DataConverter.BytesToInt32(wordData1);
                        Other.PeripheralTemperature.Unit = DataConverter.ByteToInt32(wordData2);
                        break;
                    case 0x02:      //血液温度
                        byte[] wordData3 = new byte[2];
                        byte wordData4 = dataBytes[5];
                        Array.ConstrainedCopy(dataBytes, 3, wordData3, 0, 2);   //血液温度
                        Other.BloodTemperature.Value = DataConverter.BytesToInt32(wordData3);
                        Other.BloodTemperature.Unit = DataConverter.ByteToInt32(wordData4);
                        break;
                    case 0x03:      //pH
                        byte[] wordData5 = new byte[2];
                        Array.ConstrainedCopy(dataBytes, 3, wordData5, 0, 2);   //pH
                        Other.pH.Value = DataConverter.BytesToInt32(wordData5);
                        break;
                    case 0x04:      //ICP
                        byte[] wordData6 = new byte[2];
                        byte wordData7 = dataBytes[5];
                        Array.ConstrainedCopy(dataBytes, 3, wordData6, 0, 2);   //ICP
                        Other.ICP.Value = DataConverter.BytesToInt32(wordData6);
                        Other.ICP.Unit = DataConverter.ByteToInt32(wordData7);
                        break;
                    case 0x05:      //TOF
                        byte wordData8 = dataBytes[3];
                        byte wordData9 = dataBytes[4];
                        Other.TOF.Numerical.Value = DataConverter.ByteToInt32(wordData8);
                        Other.TOF.Ratio.Value = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x06:      //PTC
                        byte wordData10 = dataBytes[3];
                        Other.PTC.Value = DataConverter.ByteToInt32(wordData10);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 上位机设置 其它 Other
            /// </summary>
            /// <param name="dataBytes"></param>
            /// <param name="Other"></param>
            static public void GetData_VitalSigns_Other(byte[] dataBytes, ref Modle.Other Other)
            {

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x28)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x03)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x00:      //全部
                        {   
                            //外周温度Array.ConstrainedCopy(dataBytes_PeripheralTemperature, 3, dataBytes, 3, 3);
                            byte[] wordData1_0 = new byte[2];
                            byte wordData2_0 = dataBytes[5];
                            Array.ConstrainedCopy(dataBytes, 3, wordData1_0, 0, 2);   //外周温度
                            Other.PeripheralTemperature.Value = DataConverter.BytesToInt32(wordData1_0);
                            Other.PeripheralTemperature.Unit = DataConverter.ByteToInt32(wordData2_0);
                            //血液温度Array.ConstrainedCopy(dataBytes_BloodTemperature, 3, dataBytes, 6, 3);
                            byte[] wordData3_0 = new byte[2];
                            byte wordData4_0 = dataBytes[8];
                            Array.ConstrainedCopy(dataBytes, 6, wordData3_0, 0, 2);   //血液温度
                            Other.BloodTemperature.Value = DataConverter.BytesToInt32(wordData3_0);
                            Other.BloodTemperature.Unit = DataConverter.ByteToInt32(wordData4_0);
                            //pH Array.ConstrainedCopy(dataBytes_pH, 3, dataBytes, 9, 1);
                            byte wordData5_0 = dataBytes[9];
                            Other.pH.Value = DataConverter.ByteToInt32(wordData5_0);
                            //ICP
                            byte[] wordData6_0 = new byte[2];
                            byte wordData7_0 = dataBytes[12];
                            Array.ConstrainedCopy(dataBytes, 10, wordData6_0, 0, 2);   //ICP
                            Other.ICP.Value = DataConverter.BytesToInt32(wordData6_0);
                            Other.ICP.Unit = DataConverter.ByteToInt32(wordData7_0);
                            //TOF Array.ConstrainedCopy(dataBytes_TOF, 3, dataBytes, 10, 2);
                            byte wordData8_0 = dataBytes[13];
                            byte wordData9_0 = dataBytes[14];
                            Other.TOF.Numerical.Value = DataConverter.ByteToInt32(wordData8_0);
                            Other.TOF.Ratio.Value = DataConverter.ByteToInt32(wordData9_0);
                            //PTC Array.ConstrainedCopy(dataBytes_PTC, 3, dataBytes, 12, 1);
                            byte wordData10_0 = dataBytes[15];
                            Other.PTC.Value = DataConverter.ByteToInt32(wordData10_0);
                        }
                        break;
                    case 0x01:      //外周温度
                        byte[] wordData1 = new byte[2];
                        byte wordData2 = dataBytes[5];
                        Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);   //外周温度
                        Other.PeripheralTemperature.Value = DataConverter.BytesToInt32(wordData1);
                        Other.PeripheralTemperature.Unit = DataConverter.ByteToInt32(wordData2);
                        break;
                    case 0x02:      //血液温度
                        byte[] wordData3 = new byte[2];
                        byte wordData4 = dataBytes[5];
                        Array.ConstrainedCopy(dataBytes, 3, wordData3, 0, 2);   //血液温度
                        Other.BloodTemperature.Value = DataConverter.BytesToInt32(wordData3);
                        Other.BloodTemperature.Unit = DataConverter.ByteToInt32(wordData4);
                        break;
                    case 0x03:      //pH
                        byte wordData5 = dataBytes[3];                       
                        Other.pH.Value = DataConverter.ByteToInt32(wordData5);
                        break;
                    case 0x04:      //ICP
                        byte[] wordData6 = new byte[2];
                        byte wordData7 = dataBytes[5];
                        Array.ConstrainedCopy(dataBytes, 3, wordData6, 0, 2);   //ICP
                        Other.ICP.Value = DataConverter.BytesToInt32(wordData6);
                        Other.ICP.Unit = DataConverter.ByteToInt32(wordData7);
                        break;
                    case 0x05:      //TOF
                        byte wordData8 = dataBytes[3];
                        byte wordData9 = dataBytes[4];
                        Other.TOF.Numerical.Value = DataConverter.ByteToInt32(wordData8);
                        Other.TOF.Ratio.Value = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x06:      //PTC
                        byte wordData10 = dataBytes[3];
                        Other.PTC.Value = DataConverter.ByteToInt32(wordData10);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 生命体征 - 其它
            /// </summary>
            /// <param name="Other"></param>
            /// <returns></returns>
            static public List<byte[]> SetDataBytes_VitalSigns_Other(Modle.Other Other)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_VitalSigns_Other_PeripheralTemperature(Other.PeripheralTemperature));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Other_BloodTemperature(Other.BloodTemperature));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Other_pH(Other.pH));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Other_ICP(Other.ICP));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Other_TOF(Other.TOF));
                dataBytes_List.Add(SetDataBytes_VitalSigns_Other_PTC(Other.PTC));
                
                return dataBytes_List;
            }

            /// <summary>
            /// 外周温度
            /// </summary>
            /// <param name="PeripheralTemperature"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Other_PeripheralTemperature(Modle.DataValue PeripheralTemperature)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x01;

                byte[] Bytes = DataConverter.IntToBytes(PeripheralTemperature.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(PeripheralTemperature.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 血液温度
            /// </summary>
            /// <param name="BloodTemperature"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Other_BloodTemperature(Modle.DataValue BloodTemperature)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x02;

                byte[] Bytes = DataConverter.IntToBytes(BloodTemperature.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(BloodTemperature.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// pH
            /// </summary>
            /// <param name="pH"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Other_pH(Modle.DataValue pH)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x03;

                dataBytes[3] = DataConverter.IntToBytes(pH.Value)[0];

                return dataBytes;
            }

            /// <summary>
            /// 颅内压
            /// </summary>
            /// <param name="ICP"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Other_ICP(Modle.DataValue ICP)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x04;

                byte[] Bytes = DataConverter.IntToBytes(ICP.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];

                dataBytes[5] = DataConverter.IntToBytes(ICP.Unit)[0];

                return dataBytes;

            }
            
            /// <summary>
            /// TOF
            /// </summary>
            /// <param name="TOF"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_VitalSigns_Other_TOF(Modle.TOF TOF)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x05;

                dataBytes[3] = DataConverter.IntToBytes(TOF.Numerical.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(TOF.Ratio.Value)[0];

                return dataBytes;
            }

            static public byte[] SetDataBytes_VitalSigns_Other_PTC(Modle.DataValue PTC)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x06;

                dataBytes[3] = DataConverter.IntToBytes(PTC.Value)[0];

                return dataBytes;
            }

            /// <summary>
            /// 其它全部
            /// </summary>
            /// <param name="Other"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other(Modle.Other Other)
            {
                byte[] dataBytes = new byte[13];
                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x00;

                byte[] dataBytes_PeripheralTemperature = GetDataBytes_VitalSigns_Other_PeripheralTemperature(Other.PeripheralTemperature);
                byte[] dataBytes_BloodTemperature = GetDataBytes_VitalSigns_Other_BloodTemperature(Other.BloodTemperature);
                byte[] dataBytes_pH = GetDataBytes_VitalSigns_Other_pH(Other.pH);
                byte[] dataBytes_ICP = GetDataBytes_VitalSigns_Other_ICP(Other.ICP);
                byte[] dataBytes_TOF = GetDataBytes_VitalSigns_Other_TOF(Other.TOF);
                byte[] dataBytes_PTC = GetDataBytes_VitalSigns_Other_PTC(Other.PTC);


                Array.ConstrainedCopy(dataBytes_PeripheralTemperature, 3, dataBytes, 3, 3);
                Array.ConstrainedCopy(dataBytes_BloodTemperature, 3, dataBytes, 6, 3);
                Array.ConstrainedCopy(dataBytes_pH, 3, dataBytes, 9, 1);
                Array.ConstrainedCopy(dataBytes_ICP, 3, dataBytes, 10, 3);
                Array.ConstrainedCopy(dataBytes_TOF, 3, dataBytes, 13, 2);
                Array.ConstrainedCopy(dataBytes_PTC, 3, dataBytes, 15, 1);

                return dataBytes;
            }

            /// <summary>
            /// 外周温度 PeripheralTemperature
            /// </summary>
            /// <param name="Other"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other_PeripheralTemperature(Modle.DataValue PeripheralTemperature)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x01;

                byte[] Bytes = DataConverter.IntToBytes(PeripheralTemperature.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(PeripheralTemperature.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// 血液温度 BloodTemperature
            /// </summary>
            /// <param name="Other"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other_BloodTemperature(Modle.DataValue BloodTemperature)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x02;

                byte[] Bytes = DataConverter.IntToBytes(BloodTemperature.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(BloodTemperature.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// pH
            /// </summary>
            /// <param name="pH"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other_pH(Modle.DataValue pH)
            {
                byte[] dataBytes = new byte[4];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x03;

                dataBytes[3] = DataConverter.IntToBytes(pH.Value)[0];

                return dataBytes;
            }

            /// <summary>
            /// 颅内压
            /// </summary>
            /// <param name="Cyclic"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other_ICP(Modle.DataValue ICP)
            {
                byte[] dataBytes = new byte[6];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x04;

                byte[] Bytes = DataConverter.IntToBytes(ICP.Value);

                dataBytes[3] = Bytes[0];
                dataBytes[4] = Bytes[1];
                dataBytes[5] = DataConverter.IntToBytes(ICP.Unit)[0];

                return dataBytes;
            }

            /// <summary>
            /// TOF
            /// </summary>
            /// <param name="TOF"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other_TOF(Modle.TOF TOF)
            {
                byte[] dataBytes = new byte[5];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x05;                             

                dataBytes[3] = DataConverter.IntToBytes(TOF.Numerical.Value)[0];
                dataBytes[4] = DataConverter.IntToBytes(TOF.Ratio.Value)[0];

                return dataBytes;
            }

            /// <summary>
            /// 强直后计数 PTC
            /// </summary>
            /// <param name="PTC"></param>
            /// <returns></returns>
            static public byte[] GetDataBytes_VitalSigns_Other_PTC(Modle.DataValue PTC)
            {
                byte[] dataBytes = new byte[4];

                dataBytes[0] = 0x28;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x06;
             
                dataBytes[3] = DataConverter.IntToBytes(PTC.Value)[0];

                return dataBytes;
            }
        }
    }
}
