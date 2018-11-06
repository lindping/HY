using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 除颤数据字节处理 0x26
    /// </summary>
    static public class Defibrillation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="defibrillation"></param>
        static public void GetData_Defibrillation(byte[] dataBytes, ref Modle.Defibrillation defibrillation)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x26)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x01)
            {
                //除颤检测
                if (wordKey3 == 0x01)
                {
                    //除颤能量
                    byte[] wordData1 = new byte[2];     //除颤能量
                    Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);
                    defibrillation.Energy = DataConverter.BytesToInt32(wordData1);
                }
            }
        }

        /// <summary>
        /// 除颤
        /// </summary>
        /// <param name="Defibrillation"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Defibrillation(Modle.Defibrillation Defibrillation)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x26;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;

            byte[] Bytes = DataConverter.IntToBytes(Defibrillation.Energy);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            return dataBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="DefibrillatorElectrode"></param>
        static public void GetData_DefibrillationElectrode(byte[] dataBytes, ref Modle.DefibrillatorElectrode DefibrillatorElectrode)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x26)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x01)
            {
                //除颤检测
                if (wordKey3 == 0x02)
                {
                    //除颤能量
                    byte wordData1 = dataBytes[3];     //胸骨(Sterno)
                    byte wordData2 = dataBytes[4];     //心尖搏动点(Apex)
                    DefibrillatorElectrode.Sterno.Status = DataConverter.ByteToOperatorStatus(wordData1);
                    DefibrillatorElectrode.Apex.Status = DataConverter.ByteToOperatorStatus(wordData2);
                }
            }
        }

        /// <summary>
        /// 除颤电极片
        /// </summary>
        /// <param name="DefibrillatorElectrode"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_DefibrillatorElectrode(Modle.DefibrillatorElectrode DefibrillatorElectrode)
        {
            byte[] dataBytes = new byte[5];

            dataBytes[0] = 0x26;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;
            dataBytes[3] = DataConverter.OperatorStatusToByte(DefibrillatorElectrode.Sterno.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(DefibrillatorElectrode.Apex.Status);

            return dataBytes;
        }


    }
}
