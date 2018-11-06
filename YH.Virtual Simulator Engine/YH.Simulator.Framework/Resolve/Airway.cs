using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 气道数据字节处理 0x23
    /// </summary>
    static public class Airway
    {
        /// <summary>
        /// 气道 Airway
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Airway(byte[] dataBytes, ref Modle.Airway Airway)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x23)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey2)
            {
                case 0x01:      //舌水肿 Tongue edema
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.TongueEdema.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.TongueEdema.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x02:      //舌头后坠 Tongue fallback
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.TongueFallback.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.TongueFallback.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x03:      //异物气道阻塞 FBAO:Foreign Body Airway Obstruction
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.FBAO.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.FBAO.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x04:      //咽部阻塞 Pharyngeal Obstruction
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.PharyngealObstruction.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.PharyngealObstruction.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x05:      //喉痉挛 Laryngospasm
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.Laryngospasm.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.Laryngospasm.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x06:      //牙关紧闭 Trismus
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.Trismus.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.Trismus.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x07:      //颈关节强直 Neck Ankylosis
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.NeckAnkylosis.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.NeckAnkylosis.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x08:      //肺阻力 Resistance
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        byte wordData3 = dataBytes[5];
                        byte wordData4 = dataBytes[6];
                        Airway.Resistance.Right.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.Resistance.Right.Value = DataConverter.ByteToInt32(wordData2);
                        Airway.Resistance.Left.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Airway.Resistance.Left.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                case 0x09:      //顺应性 Compliance
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        byte wordData3 = dataBytes[5];
                        byte wordData4 = dataBytes[6];
                        Airway.Compliance.Right.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.Compliance.Right.Value = DataConverter.ByteToInt32(wordData2);
                        Airway.Compliance.Left.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Airway.Compliance.Left.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                case 0x0A:      //气胸 Aerothorax
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        byte wordData3 = dataBytes[5];
                        byte wordData4 = dataBytes[6];
                        Airway.Aerothorax.Right.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.Aerothorax.Right.Value = DataConverter.ByteToInt32(wordData2);
                        Airway.Aerothorax.Left.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Airway.Aerothorax.Left.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                case 0x0B:      //胃胀气 Stomach distention
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.StomachDistention.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.StomachDistention.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x0C:      //呼出CO2 Exhale CO2
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        Airway.ExhaleCO2.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.ExhaleCO2.Value = DataConverter.ByteToInt32(wordData2);
                    }
                    break;
                case 0x0D:      //自主呼吸 Autonomous Respiration
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        byte wordData3 = dataBytes[5];
                        byte wordData4 = dataBytes[6];
                        Airway.AutonomousRespiration.Right.Status = DataConverter.ByteToControllerStatus(wordData1);
                        Airway.AutonomousRespiration.Right.Value = DataConverter.ByteToInt32(wordData2);
                        Airway.AutonomousRespiration.Left.Status = DataConverter.ByteToControllerStatus(wordData3);
                        Airway.AutonomousRespiration.Left.Value = DataConverter.ByteToInt32(wordData4);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 气道 Airway
        /// </summary>
        /// <param name="Airway"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Airway(Modle.Airway Airway)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Airway_TongueEdema(Airway.TongueEdema));
            dataBytes_List.Add(SetDataBytes_Airway_TongueFallback(Airway.TongueFallback));
            dataBytes_List.Add(SetDataBytes_Airway_FBAO(Airway.FBAO));
            dataBytes_List.Add(SetDataBytes_Airway_PharyngealObstruction(Airway.PharyngealObstruction));
            dataBytes_List.Add(SetDataBytes_Airway_Laryngospasm(Airway.Laryngospasm));
            dataBytes_List.Add(SetDataBytes_Airway_Trismus(Airway.Trismus));
            dataBytes_List.Add(SetDataBytes_Airway_NeckAnkylosis(Airway.NeckAnkylosis));
            dataBytes_List.Add(SetDataBytes_Airway_Resistance(Airway.Resistance));
            dataBytes_List.Add(SetDataBytes_Airway_Compliance(Airway.Compliance));
            dataBytes_List.Add(SetDataBytes_Airway_Aerothorax(Airway.Aerothorax));
            dataBytes_List.Add(SetDataBytes_Airway_StomachDistention(Airway.StomachDistention));
            dataBytes_List.Add(SetDataBytes_Airway_ExhaleCO2(Airway.ExhaleCO2));
            dataBytes_List.Add(SetDataBytes_Airway_AutonomousRespiration(Airway.AutonomousRespiration));

            return dataBytes_List;
        }

        /// <summary>
        /// 舌水肿 Tongue edema
        /// </summary>
        /// <param name="TongueEdema"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_TongueEdema(Modle.Controller TongueEdema)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(TongueEdema.Status);
            dataBytes[4] = DataConverter.IntToBytes(TongueEdema.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 舌头后坠 Tongue fallback
        /// </summary>
        /// <param name="TongueFallback"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_TongueFallback(Modle.Controller TongueFallback)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(TongueFallback.Status);
            dataBytes[4] = DataConverter.IntToBytes(TongueFallback.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 异物气道阻塞 FBAO:Foreign Body Airway Obstruction
        /// </summary>
        /// <param name="FBAO"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_FBAO(Modle.Controller FBAO)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(FBAO.Status);
            dataBytes[4] = DataConverter.IntToBytes(FBAO.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 咽部阻塞 Pharyngeal Obstruction
        /// </summary>
        /// <param name="PharyngealObstruction"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_PharyngealObstruction(Modle.Controller PharyngealObstruction)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(PharyngealObstruction.Status);
            dataBytes[4] = DataConverter.IntToBytes(PharyngealObstruction.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 喉痉挛 Laryngospasm
        /// </summary>
        /// <param name="TongueEdema"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_Laryngospasm(Modle.Controller Laryngospasm)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x05;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Laryngospasm.Status);
            dataBytes[4] = DataConverter.IntToBytes(Laryngospasm.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 牙关紧闭 Trismus
        /// </summary>
        /// <param name="Trismus"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_Trismus(Modle.Controller Trismus)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x06;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Trismus.Status);
            dataBytes[4] = DataConverter.IntToBytes(Trismus.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 颈部强直 Neck Ankylosis
        /// </summary>
        /// <param name="NeckAnkylosis"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_NeckAnkylosis(Modle.Controller NeckAnkylosis)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x07;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(NeckAnkylosis.Status);
            dataBytes[4] = DataConverter.IntToBytes(NeckAnkylosis.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 肺阻力 Resistance
        /// </summary>
        /// <param name="Resistance"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_Resistance(Modle.Resistance Resistance)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x08;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Resistance.Right.Status);
            dataBytes[4] = DataConverter.IntToBytes(Resistance.Right.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(Resistance.Left.Status);
            dataBytes[6] = DataConverter.IntToBytes(Resistance.Left.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 顺应性 Compliance
        /// </summary>
        /// <param name="Compliance"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_Compliance(Modle.Compliance Compliance)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x09;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Compliance.Right.Status);
            dataBytes[4] = DataConverter.IntToBytes(Compliance.Right.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(Compliance.Left.Status);
            dataBytes[6] = DataConverter.IntToBytes(Compliance.Left.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 气胸 Aerothorax
        /// </summary>
        /// <param name="Aerothorax"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_Aerothorax(Modle.Aerothorax Aerothorax)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x0A;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Aerothorax.Right.Status);
            dataBytes[4] = DataConverter.IntToBytes(Aerothorax.Right.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(Aerothorax.Left.Status);
            dataBytes[6] = DataConverter.IntToBytes(Aerothorax.Left.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 胃胀气 Stomach distention
        /// </summary>
        /// <param name="StomachDistention"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_StomachDistention(Modle.Controller StomachDistention)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x0B;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(StomachDistention.Status);
            dataBytes[4] = DataConverter.IntToBytes(StomachDistention.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 呼出CO2 Exhale CO2
        /// </summary>
        /// <param name="ExhaleCO2"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Airway_ExhaleCO2(Modle.Controller ExhaleCO2)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x0C;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(ExhaleCO2.Status);
            dataBytes[4] = DataConverter.IntToBytes(ExhaleCO2.Value)[0];

            return dataBytes;
        }

        static public byte[] SetDataBytes_Airway_AutonomousRespiration(Modle.AutonomousRespiration AutonomousRespiration)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x23;
            dataBytes[1] = 0x0D;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(AutonomousRespiration.Right.Status);
            dataBytes[4] = DataConverter.IntToBytes(AutonomousRespiration.Right.Value)[0];
            dataBytes[5] = DataConverter.ControllerStatusToByte(AutonomousRespiration.Left.Status);
            dataBytes[6] = DataConverter.IntToBytes(AutonomousRespiration.Left.Value)[0];

            return dataBytes;
        }


    }
}
