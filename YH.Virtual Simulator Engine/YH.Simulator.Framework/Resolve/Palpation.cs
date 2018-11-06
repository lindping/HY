using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 触诊数据字节处理
    /// </summary>
    static public class Palpation
    {
        /// <summary>
        /// 触诊 Palpation 0x29
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Palpation(byte[] dataBytes,ref Modle.Palpation Palpation)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x29)
                return;

            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //腹部压痛
                    {
                        switch (wordKey3)
                        {
                            case 0x01:      //右上腹压痛点
                                byte wordData1 = dataBytes[3];
                                Palpation.Abdominal.RightUpper.Status = DataConverter.ByteToControllerStatus(wordData1);
                                break;
                            case 0x02:      //左上腹压痛点
                                byte wordData2 = dataBytes[3];
                                Palpation.Abdominal.LeftUpper.Status = DataConverter.ByteToControllerStatus(wordData2);
                                break;
                            case 0x03:      //肚脐周围压痛点 
                                byte wordData3 = dataBytes[3];
                                Palpation.Abdominal.Middle.Status = DataConverter.ByteToControllerStatus(wordData3);
                                break;
                            case 0x04:      //右下腹压痛点
                                byte wordData4 = dataBytes[3];
                                Palpation.Abdominal.RightLower.Status = DataConverter.ByteToControllerStatus(wordData4);
                                break;
                            case 0x05:      //左下腹压痛点
                                byte wordData5 = dataBytes[3];
                                Palpation.Abdominal.LeftLower.Status = DataConverter.ByteToControllerStatus(wordData5);
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

        static public void GetData_Palpation(byte[] dataBytes, ref Modle.AbdominalTouch AbdominalTouch)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x29)
                return;

            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //腹部压痛
                    {
                        switch (wordKey3)
                        {
                            case 0x01:      //右上腹肋弓下
                                byte wordData1 = dataBytes[3];
                                AbdominalTouch.RightUpperTouch.Status = DataConverter.ByteToOperatorStatus(wordData1);
                                break;
                            case 0x02:      //左上腹压痛点
                                byte wordData2 = dataBytes[3];
                                AbdominalTouch.LeftUpperTouch.Status = DataConverter.ByteToOperatorStatus(wordData2);
                                break;
                            case 0x03:      //肚脐周围压痛点
                                byte wordData3 = dataBytes[3];
                                AbdominalTouch.MiddleTouch.Status = DataConverter.ByteToOperatorStatus(wordData3);
                                break;
                            case 0x04:      //右下腹压痛点
                                byte wordData4 = dataBytes[3];
                                AbdominalTouch.RightLowerTouch.Status = DataConverter.ByteToOperatorStatus(wordData4);
                                break;
                            case 0x05:      //左下腹压痛点
                                byte wordData5 = dataBytes[3];
                                AbdominalTouch.LeftLowerTouch.Status = DataConverter.ByteToOperatorStatus(wordData5);
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
        /// 触诊
        /// </summary>
        /// <param name="Palpation"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Palpation(Modle.Palpation Palpation)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.AddRange(SetDataBytes_Abdominal(Palpation.Abdominal));

            return dataBytes_List;
        }

        /// <summary>
        /// 腹部按压触诊
        /// </summary>
        /// <param name="Abdominal"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Abdominal(Modle.Abdominal Abdominal)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_Abdominal_RightUpper(Abdominal.RightUpper));
            dataBytes_List.Add(SetDataBytes_Abdominal_LeftUpper(Abdominal.LeftUpper));
            dataBytes_List.Add(SetDataBytes_Abdominal_Middle(Abdominal.Middle));
            dataBytes_List.Add(SetDataBytes_Abdominal_RightLower(Abdominal.RightLower));
            dataBytes_List.Add(SetDataBytes_Abdominal_LeftLower(Abdominal.LeftLower));

            return dataBytes_List;
        }

        /// <summary>
        /// 右上腹肋弓下 Right Upper
        /// </summary>
        /// <param name="RightUpper"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Abdominal_RightUpper(Modle.Controller RightUpper)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.ControllerStatusToByte(RightUpper.Status);

            return dataBytes;
        }

        /// <summary>
        /// 左上腹压痛点 Left Upper
        /// </summary>
        /// <param name="LeftUpper"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Abdominal_LeftUpper(Modle.Controller LeftUpper)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;
            dataBytes[3] = DataConverter.ControllerStatusToByte(LeftUpper.Status);

            return dataBytes;
        }

        /// <summary>
        /// 肚脐周围压痛点 Middle
        /// </summary>
        /// <param name="Middle"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Abdominal_Middle(Modle.Controller Middle)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x03;
            dataBytes[3] = DataConverter.ControllerStatusToByte(Middle.Status);

            return dataBytes;
        }

        /// <summary>
        /// 右下腹压痛点 Right Lower
        /// </summary>
        /// <param name="RightLower"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Abdominal_RightLower(Modle.Controller RightLower)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x04;
            dataBytes[3] = DataConverter.ControllerStatusToByte(RightLower.Status);

            return dataBytes;
        }

        /// <summary>
        /// 左下腹压痛点 Left Lower
        /// </summary>
        /// <param name="LeftLower"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_Abdominal_LeftLower(Modle.Controller LeftLower)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x05;
            dataBytes[3] = DataConverter.ControllerStatusToByte(LeftLower.Status);

            return dataBytes;
        }

        /// <summary>
        /// 按压全部
        /// </summary>
        /// <param name="AbdominalTouch"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_AbdominalTouch(Modle.AbdominalTouch AbdominalTouch)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x00;

            dataBytes[3] = DataConverter.OperatorStatusToByte(AbdominalTouch.RightUpperTouch.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(AbdominalTouch.LeftUpperTouch.Status);
            dataBytes[5] = DataConverter.OperatorStatusToByte(AbdominalTouch.MiddleTouch.Status);
            dataBytes[6] = DataConverter.OperatorStatusToByte(AbdominalTouch.RightLowerTouch.Status);
            dataBytes[7] = DataConverter.OperatorStatusToByte(AbdominalTouch.LeftLowerTouch.Status);

            return dataBytes;
        }

        /// <summary>
        /// 右上腹肋弓下
        /// </summary>
        /// <param name="RightUpperTouch"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_AbdominalTouch_RightUpperTouch(Modle.Operator RightUpperTouch)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;

            dataBytes[3] = DataConverter.OperatorStatusToByte(RightUpperTouch.Status);

            return dataBytes;
        }

        /// <summary>
        /// 左上腹压痛点
        /// </summary>
        /// <param name="LeftUpperTouch"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_AbdominalTouch_LeftUpperTouch(Modle.Operator LeftUpperTouch)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;

            dataBytes[3] = DataConverter.OperatorStatusToByte(LeftUpperTouch.Status);

            return dataBytes;
        }

        /// <summary>
        /// 肚脐周围压痛点
        /// </summary>
        /// <param name="MiddleTouch"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_AbdominalTouch_MiddleTouch(Modle.Operator MiddleTouch)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x03;

            dataBytes[3] = DataConverter.OperatorStatusToByte(MiddleTouch.Status);

            return dataBytes;
        }

        /// <summary>
        /// 右下腹压痛点
        /// </summary>
        /// <param name="RightLowerTouch"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_AbdominalTouch_RightLowerTouch(Modle.Operator RightLowerTouch)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x04;

            dataBytes[3] = DataConverter.OperatorStatusToByte(RightLowerTouch.Status);

            return dataBytes;
        }

        /// <summary>
        /// 左下腹压痛点
        /// </summary>
        /// <param name="LeftLowerTouch"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_AbdominalTouch_LeftLowerTouch(Modle.Operator LeftLowerTouch)
        {
            byte[] dataBytes = new byte[4];

            dataBytes[0] = 0x29;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x05;

            dataBytes[3] = DataConverter.OperatorStatusToByte(LeftLowerTouch.Status);

            return dataBytes;
        }

    }
}
