using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 脉搏数据字节处理
    /// </summary>
    static public class Pulse
    {
        /// <summary>
        /// 脉搏 Pulse  0x33
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Pulse(byte[] dataBytes, ref Modle.Pulse Pulse)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x33)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //身体
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];      //右颈动脉
                        byte wordData2 = dataBytes[4];      //左颈动脉
                        byte wordData3 = dataBytes[5];      //右股动脉
                        byte wordData4 = dataBytes[6];      //左股动脉
                        Pulse.BodyPulse.CarotidRight.Value = DataConverter.ByteToInt32(wordData1);
                        Pulse.BodyPulse.CarotidRight.Status = Pulse.BodyPulse.CarotidRight.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.BodyPulse.CarotidLeft.Value = DataConverter.ByteToInt32(wordData2);
                        Pulse.BodyPulse.CarotidLeft.Status = Pulse.BodyPulse.CarotidLeft.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.BodyPulse.FemoralRight.Value = DataConverter.ByteToInt32(wordData3);
                        Pulse.BodyPulse.FemoralRight.Status = Pulse.BodyPulse.FemoralRight.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.BodyPulse.FemoralLeft.Value = DataConverter.ByteToInt32(wordData4);
                        Pulse.BodyPulse.FemoralLeft.Status = Pulse.BodyPulse.FemoralLeft.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                    }
                    break;
                case 0x02:      //右臂
                    if (wordKey3 == 0x01)
                    {
                        byte wordData5 = dataBytes[3];      //肱动脉
                        byte wordData6 = dataBytes[4];      //桡动脉
                        Pulse.RightArmPulse.Brachial.Value = DataConverter.ByteToInt32(wordData5);
                        Pulse.RightArmPulse.Brachial.Status = Pulse.RightArmPulse.Brachial.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.RightArmPulse.Radial.Value = DataConverter.ByteToInt32(wordData6);
                        Pulse.RightArmPulse.Radial.Status = Pulse.RightArmPulse.Radial.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;

                    }
                    break;
                case 0x03:      //左臂
                    if (wordKey3 == 0x01)
                    {
                        byte wordData7 = dataBytes[3];      //肱动脉
                        byte wordData8 = dataBytes[4];      //桡动脉
                        Pulse.LeftArmPulse.Brachial.Value = DataConverter.ByteToInt32(wordData7);
                        Pulse.LeftArmPulse.Brachial.Status = Pulse.LeftArmPulse.Brachial.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.LeftArmPulse.Radial.Value = DataConverter.ByteToInt32(wordData8);
                        Pulse.LeftArmPulse.Radial.Status = Pulse.LeftArmPulse.Radial.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                    }
                    break;
                case 0x04:      //右腿
                    if (wordKey3 == 0x01)
                    {
                        byte wordData9 = dataBytes[3];      //腘动脉
                        byte wordData10 = dataBytes[4];      //足背动脉
                        byte wordData11 = dataBytes[5];      //足跟动脉
                        Pulse.RightLegPulse.Popliteal.Value = DataConverter.ByteToInt32(wordData9);
                        Pulse.RightLegPulse.Popliteal.Status = Pulse.RightLegPulse.Popliteal.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.RightLegPulse.DorsalisPedis.Value = DataConverter.ByteToInt32(wordData10);
                        Pulse.RightLegPulse.DorsalisPedis.Status = Pulse.RightLegPulse.DorsalisPedis.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.RightLegPulse.Heel.Value = DataConverter.ByteToInt32(wordData11);
                        Pulse.RightLegPulse.Heel.Status = Pulse.RightLegPulse.Heel.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                    }
                    break;
                case 0x05:      //左腿
                    if (wordKey3 == 0x01)
                    {
                        byte wordData12 = dataBytes[3];      //腘动脉
                        byte wordData13 = dataBytes[4];      //足背动脉
                        byte wordData14 = dataBytes[5];      //足跟动脉
                        Pulse.LeftLegPulse.Popliteal.Value = DataConverter.ByteToInt32(wordData12);
                        Pulse.LeftLegPulse.Popliteal.Status = Pulse.LeftLegPulse.Popliteal.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.LeftLegPulse.DorsalisPedis.Value = DataConverter.ByteToInt32(wordData13);
                        Pulse.LeftLegPulse.DorsalisPedis.Status = Pulse.LeftLegPulse.DorsalisPedis.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
                        Pulse.LeftLegPulse.Heel.Value = DataConverter.ByteToInt32(wordData14);
                        Pulse.LeftLegPulse.Heel.Status = Pulse.LeftLegPulse.Heel.Value > 0 ? Modle.ControllerStatus.Yes : Modle.ControllerStatus.No;
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
        /// <param name="CheckPulse"></param>
        static public void GetData_CheckPulse(byte[] dataBytes, ref Modle.CheckPulse CheckPulse)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x33)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //身体
                    if (wordKey3 == 0x02)
                    {
                        byte wordData1 = dataBytes[3];      //右颈动脉
                        byte wordData2 = dataBytes[4];      //左颈动脉
                        byte wordData3 = dataBytes[5];      //右股动脉
                        byte wordData4 = dataBytes[6];      //左股动脉
                        CheckPulse.CheckBodyPulse.Carotid_Right.Status = DataConverter.ByteToOperatorStatus(wordData1);
                        CheckPulse.CheckBodyPulse.Carotid_Left.Status = DataConverter.ByteToOperatorStatus(wordData2);
                        CheckPulse.CheckBodyPulse.Femoral_Right.Status = DataConverter.ByteToOperatorStatus(wordData3);
                        CheckPulse.CheckBodyPulse.Femoral_Left.Status = DataConverter.ByteToOperatorStatus(wordData4);
                    }
                    break;
                case 0x02:      //右臂
                    if (wordKey3 == 0x02)
                    {
                        byte wordData5 = dataBytes[3];      //肱动脉
                        byte wordData6 = dataBytes[4];      //桡动脉
                        CheckPulse.CheckRightArmPulse.Brachial.Status = DataConverter.ByteToOperatorStatus(wordData5);
                        CheckPulse.CheckRightArmPulse.Radial.Status = DataConverter.ByteToOperatorStatus(wordData6);
                    }
                    break;
                case 0x03:      //左臂
                    if (wordKey3 == 0x02)
                    {
                        byte wordData7 = dataBytes[3];      //肱动脉
                        byte wordData8 = dataBytes[4];      //桡动脉
                        CheckPulse.CheckLeftArmPulse.Brachial.Status = DataConverter.ByteToOperatorStatus(wordData7);
                        CheckPulse.CheckLeftArmPulse.Radial.Status = DataConverter.ByteToOperatorStatus(wordData8);
                    }
                    break;
                case 0x04:      //右腿
                    if (wordKey3 == 0x02)
                    {
                        byte wordData9 = dataBytes[3];      //腘动脉
                        byte wordData10 = dataBytes[4];      //足背动脉
                        byte wordData11 = dataBytes[5];      //足跟动脉
                        CheckPulse.CheckRightLegPulse.Popliteal.Status = DataConverter.ByteToOperatorStatus(wordData9);
                        CheckPulse.CheckRightLegPulse.DorsalisPedis.Status = DataConverter.ByteToOperatorStatus(wordData10);
                        CheckPulse.CheckRightLegPulse.Heel.Status = DataConverter.ByteToOperatorStatus(wordData11);

                    }
                    break;
                case 0x05:      //左腿
                    if (wordKey3 == 0x02)
                    {
                        byte wordData12 = dataBytes[3];      //腘动脉
                        byte wordData13 = dataBytes[4];      //足背动脉
                        byte wordData14 = dataBytes[5];      //足跟动脉
                        CheckPulse.CheckLeftLegPulse.Popliteal.Status = DataConverter.ByteToOperatorStatus(wordData12);
                        CheckPulse.CheckLeftLegPulse.DorsalisPedis.Status = DataConverter.ByteToOperatorStatus(wordData13);
                        CheckPulse.CheckLeftLegPulse.Heel.Status = DataConverter.ByteToOperatorStatus(wordData14);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 脉搏
        /// </summary>
        /// <param name="Pulse"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Pulse(Modle.Pulse Pulse)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Pulse_BodyPulse(Pulse.BodyPulse));
            dataBytes_List.Add(SetDataBytes_Pulse_RightArmPulse(Pulse.RightArmPulse));
            dataBytes_List.Add(SetDataBytes_Pulse_LeftArmPulse(Pulse.LeftArmPulse));
            dataBytes_List.Add(SetDataBytes_Pulse_RightLegPulse(Pulse.RightLegPulse));
            dataBytes_List.Add(SetDataBytes_Pulse_LeftLegPulse(Pulse.LeftLegPulse));

            return dataBytes_List;
        }

        /// <summary>
        /// 脉搏 - 身体 Body 
        /// </summary>
        /// <param name="BodyPulse"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Pulse_BodyPulse(Modle.BodyPulse BodyPulse)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(BodyPulse.CarotidRight.Value)[0];
            dataBytes[4] = DataConverter.IntToBytes(BodyPulse.CarotidLeft.Value)[0];
            dataBytes[5] = DataConverter.IntToBytes(BodyPulse.FemoralRight.Value)[0];
            dataBytes[6] = DataConverter.IntToBytes(BodyPulse.FemoralLeft.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 脉搏 - 右臂 Right Arm
        /// </summary>
        /// <param name="RightArmPulse"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Pulse_RightArmPulse(Modle.RightArmPulse RightArmPulse)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(RightArmPulse.Brachial.Value)[0];
            dataBytes[4] = DataConverter.IntToBytes(RightArmPulse.Radial.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 脉搏 - 左臂 Left Arm
        /// </summary>
        /// <param name="LeftArmPulse"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Pulse_LeftArmPulse(Modle.LeftArmPulse LeftArmPulse)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(LeftArmPulse.Brachial.Value)[0];
            dataBytes[4] = DataConverter.IntToBytes(LeftArmPulse.Radial.Value)[0];


            return dataBytes;
        }

        /// <summary>
        /// 脉搏 - 右腿 Right Leg
        /// </summary>
        /// <param name="RightLegPulse"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Pulse_RightLegPulse(Modle.RightLegPulse RightLegPulse)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(RightLegPulse.Popliteal.Value)[0];
            dataBytes[4] = DataConverter.IntToBytes(RightLegPulse.DorsalisPedis.Value)[0];
            dataBytes[5] = DataConverter.IntToBytes(RightLegPulse.Heel.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 脉搏 - 左腿 Left Leg
        /// </summary>
        /// <param name="LeftLegPulse"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Pulse_LeftLegPulse(Modle.LeftLegPulse LeftLegPulse)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x05;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(LeftLegPulse.Popliteal.Value)[0];
            dataBytes[4] = DataConverter.IntToBytes(LeftLegPulse.DorsalisPedis.Value)[0];
            dataBytes[5] = DataConverter.IntToBytes(LeftLegPulse.Heel.Value)[0];

            return dataBytes;
        }

        /// <summary>
        /// 全部脉搏检查
        /// </summary>
        /// <param name="CheckPulse"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CheckPulse(Modle.CheckPulse CheckPulse)
        {
            byte[] dataBytes = new byte[17];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x00;
            dataBytes[2] = 0x02;

            byte[] dataBytes_BodyPulseCheck = GetDataBytes_CheckPulse_Body(CheckPulse.CheckBodyPulse);
            byte[] dataBytes_RightArmPulseCheck = GetDataBytes_CheckPulse_RightArm(CheckPulse.CheckRightArmPulse);
            byte[] dataBytes_LeftArmPulseCheck = GetDataBytes_CheckPulse_LeftArm(CheckPulse.CheckLeftArmPulse);
            byte[] dataBytes_RightLegPulseCheck = GetDataBytes_CheckPulse_RightLeg(CheckPulse.CheckRightLegPulse);
            byte[] dataBytes_LeftLegPulseCheck = GetDataBytes_CheckPulse_LeftLeg(CheckPulse.CheckLeftLegPulse);

            Array.ConstrainedCopy(dataBytes_BodyPulseCheck, 3, dataBytes, 3, 4);
            Array.ConstrainedCopy(dataBytes_RightArmPulseCheck, 3, dataBytes, 7, 2);
            Array.ConstrainedCopy(dataBytes_LeftArmPulseCheck, 3, dataBytes, 9, 2);
            Array.ConstrainedCopy(dataBytes_RightLegPulseCheck, 3, dataBytes, 11, 3);
            Array.ConstrainedCopy(dataBytes_LeftLegPulseCheck, 3, dataBytes, 14, 3);

            return dataBytes;
        }

        /// <summary>
        /// 身体脉搏检查
        /// </summary>
        /// <param name="CheckBodyPulse"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CheckPulse_Body(Modle.CheckBodyPulse CheckBodyPulse)
        {
            byte[] dataBytes = new byte[7];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(CheckBodyPulse.Carotid_Right.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(CheckBodyPulse.Carotid_Left.Status);
            dataBytes[5] = DataConverter.OperatorStatusToByte(CheckBodyPulse.Femoral_Right.Status);
            dataBytes[6] = DataConverter.OperatorStatusToByte(CheckBodyPulse.Femoral_Left.Status);

            return dataBytes;
        }

        /// <summary>
        /// 右臂脉搏检查
        /// </summary>
        /// <param name="CheckRightArmPulse"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CheckPulse_RightArm(Modle.CheckRightArmPulse CheckRightArmPulse)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(CheckRightArmPulse.Brachial.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(CheckRightArmPulse.Radial.Status);

            return dataBytes;
        }

        /// <summary>
        /// 左臂脉搏检查
        /// </summary>
        /// <param name="CheckLeftArmPulse"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CheckPulse_LeftArm(Modle.CheckLeftArmPulse CheckLeftArmPulse)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x03;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(CheckLeftArmPulse.Brachial.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(CheckLeftArmPulse.Radial.Status);

            return dataBytes;
        }

        /// <summary>
        /// 右腿脉搏检查
        /// </summary>
        /// <param name="CheckRightLegPulse"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CheckPulse_RightLeg(Modle.CheckRightLegPulse CheckRightLegPulse)
        {
            byte[] dataBytes = new byte[6];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x04;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(CheckRightLegPulse.Popliteal.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(CheckRightLegPulse.DorsalisPedis.Status);
            dataBytes[5] = DataConverter.OperatorStatusToByte(CheckRightLegPulse.Heel.Status);

            return dataBytes;
        }

        /// <summary>
        /// 左腿脉搏检查
        /// </summary>
        /// <param name="CheckLeftLegPulse"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CheckPulse_LeftLeg(Modle.CheckLeftLegPulse CheckLeftLegPulse)
        {
            byte[] dataBytes = new byte[6];

            dataBytes[0] = 0x33;
            dataBytes[1] = 0x05;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(CheckLeftLegPulse.Popliteal.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(CheckLeftLegPulse.DorsalisPedis.Status);
            dataBytes[5] = DataConverter.OperatorStatusToByte(CheckLeftLegPulse.Heel.Status);

            return dataBytes;
        }
    }
}
