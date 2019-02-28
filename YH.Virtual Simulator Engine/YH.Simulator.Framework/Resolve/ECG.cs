using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    static public class ECG
    {

        /// <summary>
        /// ECG导联线数据字节处理
        /// </summary>
        static public class LeadLine
        {
            static public void SetData_LeadLine(byte[] dataBytes, ref Modle.LeadLine LeadLine)
            {
                if (dataBytes.Length != 8)
                    return;

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x38)
                    return;
                byte wordKey2 = dataBytes[1];
                byte wordKey3 = dataBytes[2];
                if (wordKey2 == 0x01)
                {
                    switch (wordKey3)
                    {
                        case 0x01:
                            byte wordData1 = dataBytes[3];      //RA
                            byte wordData2 = dataBytes[4];      //LA
                            byte wordData3 = dataBytes[5];      //RL
                            byte wordData4 = dataBytes[6];      //LL
                            byte wordData5 = dataBytes[7];      //V                          
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = DataConverter.ByteToOperatorStatus(wordData1);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status = DataConverter.ByteToOperatorStatus(wordData2);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status = DataConverter.ByteToOperatorStatus(wordData3);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status = DataConverter.ByteToOperatorStatus(wordData4);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status = DataConverter.ByteToOperatorStatus(wordData5);
                            break;
                        case 0x02:
                            byte wordData6 = dataBytes[3];      //V1
                            byte wordData7 = dataBytes[4];      //V2
                            byte wordData8 = dataBytes[5];      //V3
                            byte wordData9 = dataBytes[6];      //V4
                            byte wordData10 = dataBytes[7];     //V5                          
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V1.Status = DataConverter.ByteToOperatorStatus(wordData6);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V2.Status = DataConverter.ByteToOperatorStatus(wordData7);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V3.Status = DataConverter.ByteToOperatorStatus(wordData8);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V4.Status = DataConverter.ByteToOperatorStatus(wordData9);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V5.Status = DataConverter.ByteToOperatorStatus(wordData10);
                            break;
                        case 0x03:
                            byte wordData12 = dataBytes[3];      //SPO2
                            LeadLine.LeadLine_SPO2.SPO2.Status = DataConverter.ByteToOperatorStatus(wordData12);
                            break;
                        case 0x04:
                            byte wordData13 = dataBytes[3];      //NIBP
                            LeadLine.LeadLine_NIBP.NIBP.Status = DataConverter.ByteToOperatorStatus(wordData13);
                            break;
                        case 0x05:
                            byte wordData14 = dataBytes[3];      //PeripheralTemperature
                            byte wordData15 = dataBytes[4];      //BloodTemperature
                            LeadLine.LeadLine_Temperature.PeripheralTemperature.Status = DataConverter.ByteToOperatorStatus(wordData14);
                            LeadLine.LeadLine_Temperature.BloodTemperature.Status = DataConverter.ByteToOperatorStatus(wordData15);
                            break;
                        default:
                            break;
                    }
                }
            }

            /// <summary>
            /// 上位机导联线状态
            /// </summary>
            /// <param name="dataBytes"></param>
            /// <param name="LeadLine"></param>
            static public void GetData_LeadLine(byte[] dataBytes, ref Modle.LeadLine LeadLine)
            {

                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x38)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x01)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x00:      //全部
                        {
                            //心电 ECG 5导联
                            byte wordData1_0 = dataBytes[3];      //RA
                            byte wordData2_0 = dataBytes[4];      //RL
                            byte wordData3_0 = dataBytes[5];      //LA
                            byte wordData4_0 = dataBytes[6];      //LL
                            byte wordData5_0 = dataBytes[7];      //V0
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = DataConverter.ByteToOperatorStatus(wordData1_0);                            
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status = DataConverter.ByteToOperatorStatus(wordData2_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status = DataConverter.ByteToOperatorStatus(wordData3_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status = DataConverter.ByteToOperatorStatus(wordData4_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status = DataConverter.ByteToOperatorStatus(wordData5_0);

                            //心电 ECG 12导联 V1-V6
                            byte wordData6_0 = dataBytes[8];      //V1
                            byte wordData7_0 = dataBytes[9];      //V2
                            byte wordData8_0 = dataBytes[10];      //V3
                            byte wordData9_0 = dataBytes[11];      //V4
                            byte wordData10_0 = dataBytes[12];      //V5
                            byte wordData11_0 = dataBytes[13];      //V6
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V1.Status = DataConverter.ByteToOperatorStatus(wordData6_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V2.Status = DataConverter.ByteToOperatorStatus(wordData7_0); 
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V3.Status = DataConverter.ByteToOperatorStatus(wordData8_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V4.Status = DataConverter.ByteToOperatorStatus(wordData9_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V5.Status = DataConverter.ByteToOperatorStatus(wordData10_0);
                            LeadLine.LeadLine_ECG.LeadLine_ECG12.V6.Status = DataConverter.ByteToOperatorStatus(wordData11_0);
                            //血氧饱和度                   
                            byte wordData12_0 = dataBytes[3];      //血氧饱和度                     
                            LeadLine.LeadLine_SPO2.SPO2.Status = DataConverter.ByteToOperatorStatus(wordData12_0);
                            //无创血压                   
                            byte wordData13_0 = dataBytes[3];      //无创血压                   
                            LeadLine.LeadLine_NIBP.NIBP.Status = DataConverter.ByteToOperatorStatus(wordData13_0);
                            //体温                 
                            byte wordData14_0 = dataBytes[3];      //外周温度  Temp1               
                            byte wordData15_0 = dataBytes[4];      //血液温度  Temp2               
                            LeadLine.LeadLine_Temperature.PeripheralTemperature.Status = DataConverter.ByteToOperatorStatus(wordData14_0);
                            LeadLine.LeadLine_Temperature.BloodTemperature.Status = DataConverter.ByteToOperatorStatus(wordData14_0);
                        }
                        break;
                    case 0x01:      //心电 ECG
                        byte wordData1 = dataBytes[3];      //RA
                        byte wordData2 = dataBytes[4];      //RL
                        byte wordData3 = dataBytes[5];      //LA
                        byte wordData4 = dataBytes[6];      //LL
                        byte wordData5 = dataBytes[7];      //V0
                        LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = DataConverter.ByteToOperatorStatus(wordData1);                        
                        LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status = DataConverter.ByteToOperatorStatus(wordData2);
                        LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status = DataConverter.ByteToOperatorStatus(wordData3);
                        LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status = DataConverter.ByteToOperatorStatus(wordData4);
                        LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status = DataConverter.ByteToOperatorStatus(wordData5);
                        break;
                    case 0x02:
                        byte wordData6 = dataBytes[8];      //V1
                        byte wordData7 = dataBytes[9];      //V2
                        byte wordData8 = dataBytes[10];      //V3
                        byte wordData9 = dataBytes[11];      //V4
                        byte wordData10 = dataBytes[12];      //V5
                        byte wordData11 = dataBytes[13];      //V6                     
                        LeadLine.LeadLine_ECG.LeadLine_ECG12.V1.Status = DataConverter.ByteToOperatorStatus(wordData6);
                        LeadLine.LeadLine_ECG.LeadLine_ECG12.V2.Status = DataConverter.ByteToOperatorStatus(wordData7);
                        LeadLine.LeadLine_ECG.LeadLine_ECG12.V3.Status = DataConverter.ByteToOperatorStatus(wordData8);
                        LeadLine.LeadLine_ECG.LeadLine_ECG12.V4.Status = DataConverter.ByteToOperatorStatus(wordData9);
                        LeadLine.LeadLine_ECG.LeadLine_ECG12.V5.Status = DataConverter.ByteToOperatorStatus(wordData10);
                        LeadLine.LeadLine_ECG.LeadLine_ECG12.V6.Status = DataConverter.ByteToOperatorStatus(wordData11);                        
                        break;
                    case 0x03:      //血氧饱和度                   
                        byte wordData12 = dataBytes[3];      //血氧饱和度                     
                        LeadLine.LeadLine_SPO2.SPO2.Status = DataConverter.ByteToOperatorStatus(wordData12);
                        break;
                    case 0x04:      //无创血压                   
                        byte wordData13 = dataBytes[3];      //无创血压                   
                        LeadLine.LeadLine_NIBP.NIBP.Status = DataConverter.ByteToOperatorStatus(wordData13);
                        break;
                    case 0x05:      //体温                 
                        byte wordData14 = dataBytes[3];      //外周温度  Temp1               
                        byte wordData15 = dataBytes[4];      //血液温度  Temp2               
                        LeadLine.LeadLine_Temperature.PeripheralTemperature.Status = DataConverter.ByteToOperatorStatus(wordData14);
                        LeadLine.LeadLine_Temperature.BloodTemperature.Status = DataConverter.ByteToOperatorStatus(wordData14);
                        break;
                    default:
                        break;
                }
            }

            static public List<byte[]> SetDataBytes_LeadLine(Modle.LeadLine LeadLine)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.AddRange(SetDataBytes_LeadLineECG(LeadLine.LeadLine_ECG));
                dataBytes_List.Add(SetDataBytes_LeadLine_SPO2(LeadLine.LeadLine_SPO2));
                dataBytes_List.Add(SetDataBytes_LeadLine_NIBP(LeadLine.LeadLine_NIBP));
                dataBytes_List.Add(SetDataBytes_LeadLine_Temperature(LeadLine.LeadLine_Temperature));

                return dataBytes_List;
            }



            static public List<byte[]> SetDataBytes_LeadLineECG(Modle.LeadLine_ECG LeadLine_ECG)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_LeadLineECG_LeadLineECG5(LeadLine_ECG.LeadLine_ECG5));
                dataBytes_List.Add(SetDataBytes_LeadLineECG_LeadLineECG12(LeadLine_ECG.LeadLine_ECG12));

                return dataBytes_List;
            }            

            static public byte[] SetDataBytes_LeadLineECG_LeadLineECG5(Modle.LeadLine_ECG5 leadLine_ECG5)
            {
                //throw new NotImplementedException();

                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x01;
                dataBytes[3] = DataConverter.OperatorStatusToByte(leadLine_ECG5.RA.Status);
                dataBytes[4] = DataConverter.OperatorStatusToByte(leadLine_ECG5.LA.Status);
                dataBytes[5] = DataConverter.OperatorStatusToByte(leadLine_ECG5.RL.Status);
                dataBytes[6] = DataConverter.OperatorStatusToByte(leadLine_ECG5.LL.Status);
                dataBytes[7] = DataConverter.OperatorStatusToByte(leadLine_ECG5.V0.Status);

                return dataBytes;
            }

            static public byte[] SetDataBytes_LeadLineECG_LeadLineECG12(Modle.LeadLine_ECG12 leadLine_ECG12)
            {
                //throw new NotImplementedException();

                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x02;
                dataBytes[3] = DataConverter.OperatorStatusToByte(leadLine_ECG12.V1.Status);
                dataBytes[4] = DataConverter.OperatorStatusToByte(leadLine_ECG12.V2.Status);
                dataBytes[5] = DataConverter.OperatorStatusToByte(leadLine_ECG12.V3.Status);
                dataBytes[6] = DataConverter.OperatorStatusToByte(leadLine_ECG12.V4.Status);
                dataBytes[7] = DataConverter.OperatorStatusToByte(leadLine_ECG12.V5.Status);

                return dataBytes;
            }

            static public byte[] SetDataBytes_LeadLine_SPO2(Modle.LeadLine_SPO2 LeadLine_SPO2)
            {
                //throw new NotImplementedException();

                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x03;
                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_SPO2.SPO2.Status);

                return dataBytes;
            }

            static public byte[] SetDataBytes_LeadLine_NIBP(Modle.LeadLine_NIBP LeadLine_NIBP)
            {
                //throw new NotImplementedException();

                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x04;
                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_NIBP.NIBP.Status);

                return dataBytes;
            }

            static public byte[] SetDataBytes_LeadLine_Temperature(Modle.LeadLine_Temperature LeadLine_Temperature)
            {
                //throw new NotImplementedException();

                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x05;
                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_Temperature.PeripheralTemperature.Status);
                dataBytes[4] = DataConverter.OperatorStatusToByte(LeadLine_Temperature.BloodTemperature.Status);

                return dataBytes;
            }

            static public byte[] GetDataBytes_LeadLine(Modle.LeadLine LeadLine)
            {
                byte[] dataBytes = new byte[18];
                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x00;

                byte[] dataBytes_LeadLine_ECG5 = GetDataBytes_LeadLine_ECG5(LeadLine.LeadLine_ECG.LeadLine_ECG5);
                byte[] dataBytes_LeadLine_ECG12 = GetDataBytes_LeadLine_ECG12(LeadLine.LeadLine_ECG.LeadLine_ECG12);
                byte[] dataBytes_LeadLine_SPO2 = GetDataBytes_LeadLine_SPO2(LeadLine.LeadLine_SPO2);
                byte[] dataBytes_LeadLine_NIBP = GetDataBytes_LeadLine_NIBP(LeadLine.LeadLine_NIBP);
                byte[] dataBytes_LeadLine_Temperature = GetDataBytes_LeadLine_Temperature(LeadLine.LeadLine_Temperature);

                Array.ConstrainedCopy(dataBytes_LeadLine_ECG5, 3, dataBytes, 3, 5);
                Array.ConstrainedCopy(dataBytes_LeadLine_ECG12, 3, dataBytes, 8, 6);
                Array.ConstrainedCopy(dataBytes_LeadLine_SPO2, 3, dataBytes, 14, 1);
                Array.ConstrainedCopy(dataBytes_LeadLine_NIBP, 3, dataBytes, 15, 1);
                Array.ConstrainedCopy(dataBytes_LeadLine_Temperature, 3, dataBytes, 16, 2);

                return dataBytes;
            }

            static public byte[] GetDataBytes_LeadLine_ECG5(Modle.LeadLine_ECG5 LeadLine_ECG5)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x01;

                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_ECG5.RA.Status);
                dataBytes[4] = DataConverter.OperatorStatusToByte(LeadLine_ECG5.LA.Status);
                dataBytes[5] = DataConverter.OperatorStatusToByte(LeadLine_ECG5.RL.Status);
                dataBytes[6] = DataConverter.OperatorStatusToByte(LeadLine_ECG5.LL.Status);
                dataBytes[7] = DataConverter.OperatorStatusToByte(LeadLine_ECG5.V0.Status);

                return dataBytes;
            }

            static public byte[] GetDataBytes_LeadLine_ECG12(Modle.LeadLine_ECG12 LeadLine_ECG12)
            {
                byte[] dataBytes = new byte[9];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x02;

                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_ECG12.V1.Status);
                dataBytes[4] = DataConverter.OperatorStatusToByte(LeadLine_ECG12.V2.Status);
                dataBytes[5] = DataConverter.OperatorStatusToByte(LeadLine_ECG12.V3.Status);
                dataBytes[6] = DataConverter.OperatorStatusToByte(LeadLine_ECG12.V4.Status);
                dataBytes[7] = DataConverter.OperatorStatusToByte(LeadLine_ECG12.V5.Status);
                dataBytes[8] = DataConverter.OperatorStatusToByte(LeadLine_ECG12.V6.Status);

                return dataBytes;
            }

            static public byte[] GetDataBytes_LeadLine_SPO2(Modle.LeadLine_SPO2 LeadLine_SPO2)
            {
                byte[] dataBytes = new byte[4];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x03;

                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_SPO2.SPO2.Status);

                return dataBytes;
            }

            static public byte[] GetDataBytes_LeadLine_NIBP(Modle.LeadLine_NIBP LeadLine_NIBP)
            {
                byte[] dataBytes = new byte[4];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x04;

                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_NIBP.NIBP.Status);

                return dataBytes;
            }

            static public byte[] GetDataBytes_LeadLine_Temperature(Modle.LeadLine_Temperature LeadLine_Temperature)
            {
                byte[] dataBytes = new byte[5];

                dataBytes[0] = 0x38;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x05;

                dataBytes[3] = DataConverter.OperatorStatusToByte(LeadLine_Temperature.PeripheralTemperature.Status);
                dataBytes[4] = DataConverter.OperatorStatusToByte(LeadLine_Temperature.BloodTemperature.Status);

                return dataBytes;
            }


        }
    }
}
