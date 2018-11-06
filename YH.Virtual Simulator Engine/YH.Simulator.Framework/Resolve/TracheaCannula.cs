using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 气管插管数据字节处理 0x24
    /// </summary>
    static public class TracheaCannula
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="TracheaCannula"></param>
        static public void GetData_TracheaCannula(byte[] dataBytes, ref Modle.TracheaCannula TracheaCannula)
        {

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x24)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey2)
            {
                case 0x01:          //插管检测
                    switch (wordKey3)
                    {
                        case 0x01:         //插管
                            byte wordData1 = dataBytes[3];      //插入气管
                            byte wordData2 = dataBytes[4];      //插入食管
                            byte wordData3 = dataBytes[5];      //插入右支气管
                            byte wordData4 = dataBytes[6];      //插入左支气管
                            TracheaCannula.InTrachea.Status = DataConverter.ByteToOperatorStatus(wordData1);
                            TracheaCannula.InEsophagus.Status = DataConverter.ByteToOperatorStatus(wordData2);
                            TracheaCannula.InRightBronchus.Status = DataConverter.ByteToOperatorStatus(wordData3);
                            TracheaCannula.InLeftBronchus.Status = DataConverter.ByteToOperatorStatus(wordData4);
                            break;
                        default:
                            break;
                    }
                    break;
                case 0x02:          //

                    break;              
                default:
                    break;
            }
        }

        /// <summary>
        /// 气管插管
        /// </summary>
        /// <param name="TracheaCannula"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_TracheaCannula(Modle.TracheaCannula TracheaCannula)
        {
            byte[] dataBytes = new byte[7];
            dataBytes[0] = 0x24;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.OperatorStatusToByte(TracheaCannula.InTrachea.Status);
            dataBytes[4] = DataConverter.OperatorStatusToByte(TracheaCannula.InEsophagus.Status);
            dataBytes[5] = DataConverter.OperatorStatusToByte(TracheaCannula.InRightBronchus.Status);
            dataBytes[6] = DataConverter.OperatorStatusToByte(TracheaCannula.InLeftBronchus.Status);
            return dataBytes;
        }
    }
}
