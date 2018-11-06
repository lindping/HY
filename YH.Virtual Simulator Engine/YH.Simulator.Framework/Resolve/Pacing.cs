using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 起搏数据字节处理 0x27
    /// </summary>
    static public class Pacing
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="pacing"></param>
        static public void GetData_Pacing(byte[] dataBytes, ref Modle.Pacing pacing)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x27)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x01)
            {
                //起搏检测
                if (wordKey3 == 0x01)
                {
                    //起搏电流
                    byte[] wordData1 = new byte[2];     //起搏电流
                    Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);
                    pacing.Current = DataConverter.BytesToInt32(wordData1);
                }
            }
        }

        /// <summary>
        /// 起搏
        /// </summary>
        /// <param name="Pacing"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Pacing(Modle.Pacing Pacing)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x27;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;

            byte[] Bytes = DataConverter.IntToBytes(Pacing.Current);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            return dataBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="PacerElectrode"></param>
        static public void GetData_PacerElectrode(byte[] dataBytes, ref Modle.PacerElectrode PacerElectrode)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x27)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x01)
            {
                //起搏检测
                if (wordKey3 == 0x02)
                {
                    //起搏电极片检测
                    byte wordData1 = dataBytes[3];     //胸前(Sternum)
                    byte wordData2 = dataBytes[4];     //后背(Back)
                    PacerElectrode.Sternum.Status = DataConverter.ByteToOperatorStatus(wordData1);
                    PacerElectrode.Back.Status = DataConverter.ByteToOperatorStatus(wordData2);
                }
            }
        }

        /// <summary>
        /// 起搏电极片
        /// </summary>
        /// <param name="PacerElectrode"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_PacerElectrode(Modle.PacerElectrode PacerElectrode)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x27;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;
            dataBytes[3] = DataConverter.OperatorStatusToByte(PacerElectrode.Sternum.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(PacerElectrode.Back.Status);

            return dataBytes;
        }

        
    }
}
