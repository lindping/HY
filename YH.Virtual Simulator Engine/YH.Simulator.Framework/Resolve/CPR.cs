using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// CPR 数据字节处理 0x25
    /// </summary>
    static public class CPR
    {

        /// <summary>
        /// CPR 程序
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="CPR_Q"></param>
        static public void GetData_CPR_Q(byte[] dataBytes, ref Modle.CPR_Q CPR_Q)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x25)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            if (wordKey2 == 0x01)
            {
                //心肺复苏检测
                if (wordKey3 == 0x01)
                {
                    //CPR 程序
                    byte wordData1 = dataBytes[3];      //拍打    
                    byte wordData2 = dataBytes[4];      //呼叫
                    byte wordData3 = dataBytes[5];      //检查右颈动脉
                    byte wordData4 = dataBytes[6];      //检查左颈动脉
                    byte wordData5 = dataBytes[7];      //仰头抬颏
                    byte wordData6 = dataBytes[8];      //清除口腔异物
                    CPR_Q.Pat.Status = DataConverter.ByteToOperatorStatus(wordData1);
                    CPR_Q.Shout.Status = DataConverter.ByteToOperatorStatus(wordData2);
                    CPR_Q.CheckRightCarotid.Status = DataConverter.ByteToOperatorStatus(wordData3);
                    CPR_Q.CheckRightCarotid.Status = DataConverter.ByteToOperatorStatus(wordData4);
                    CPR_Q.HeadTiltChinLift.Status = DataConverter.ByteToOperatorStatus(wordData5);
                    CPR_Q.RemovalOralForeignBody.Status = DataConverter.ByteToOperatorStatus(wordData6);

                }
            }
        }

        /// <summary>
        /// CPR_Q
        /// </summary>
        /// <param name="CPR_Q"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CPR_Q(Modle.CPR_Q CPR_Q)
        {
            byte[] dataBytes = new byte[9];

            dataBytes[0] = 0x25;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.OperatorStatusToByte(CPR_Q.Pat.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(CPR_Q.Shout.Status);
            dataBytes[5] = DataConverter.OperatorStatusToByte(CPR_Q.CheckRightCarotid.Status);
            dataBytes[6] = DataConverter.OperatorStatusToByte(CPR_Q.CheckLeftCarotid.Status);
            dataBytes[7] = DataConverter.OperatorStatusToByte(CPR_Q.HeadTiltChinLift.Status);
            dataBytes[8] = DataConverter.OperatorStatusToByte(CPR_Q.RemovalOralForeignBody.Status);

            return dataBytes;
        }

        /// <summary>
        /// CPR 操作
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="CPR_P"></param>
        static public void GetData_CPR_P(byte[] dataBytes, ref Modle.CPR_P CPR_P)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x25)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            if (wordKey2 == 0x01)
            {
                //心肺复苏检测
                if (wordKey3 == 0x02)
                {
                    //CPR 操作
                    byte wordData1 = dataBytes[3];      //吹气量    
                    byte wordData2 = dataBytes[4];      //按压深度
                    byte wordData3 = dataBytes[5];      //按压位置                   
                    CPR_P.BlowVolume = DataConverter.ByteToInt32(wordData1);
                    CPR_P.PressDepth = DataConverter.ByteToInt32(wordData2);
                    CPR_P.PressPosition = DataConverter.ByteToInt32(wordData3);

                }
            }
        }

        /// <summary>
        /// CPR P
        /// </summary>
        /// <param name="CPR_P"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_CPR_P(Modle.CPR_P CPR_P)
        {
            byte[] dataBytes = new byte[9];

            dataBytes[0] = 0x25;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;
            dataBytes[3] = DataConverter.IntToBytes(CPR_P.BlowVolume)[0];
            dataBytes[4] = DataConverter.IntToBytes(CPR_P.PressDepth)[0];
            dataBytes[5] = DataConverter.IntToBytes(CPR_P.PressPosition)[0];
            return dataBytes;
        }
        
    }
}
