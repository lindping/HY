using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 药物治疗数据字节处理 0x37
    /// </summary>
    static public class Medication
    {
        /// <summary>
        /// 给药DrugDelivery
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_DrugDelivery(byte[] dataBytes, ref Modle.DrugDelivery DrugDelivery)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x37)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x01)
            {
                switch (wordKey3)
                {
                    case 0x01:
                        byte[] wordData1 = new byte[2];     //药物ID
                        byte wordData2 = dataBytes[5];      //给药途径
                        Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);
                        DrugDelivery.Drug.DrugID = DataConverter.BytesToInt32(wordData1);
                        DrugDelivery.Drug.Route = DataConverter.ByteToRoute(wordData2);
                        break;
                    case 0x02:
                        byte[] wordData3 = new byte[2];     //剂量
                        byte wordData4 = dataBytes[5];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData3, 0, 2);
                        DrugDelivery.Dose.Value = DataConverter.BytesToInt32(wordData3);
                        DrugDelivery.Dose.Unit = DataConverter.ByteToInt32(wordData4);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="Medication"></param>
        static public void GetData_Medication(byte[] dataBytes, ref Modle.Medication Medication)
        {
            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x37)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            if (wordKey2 == 0x02)
            {
                //药物识别
                switch (wordKey3)
                {
                    case 0x01:          //口服药
                        byte[] wordData1 = new byte[2];     //药物ID
                        byte[] wordData2 = new byte[2];     //药物剂量
                        byte wordData3 = dataBytes[7];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData1, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData2, 0, 2);
                        Medication.PO.DrugID = DataConverter.BytesToInt32(wordData1);
                        Medication.PO.Dose.Value = DataConverter.BytesToInt32(wordData2);
                        Medication.PO.Dose.Unit = DataConverter.ByteToInt32(wordData3);
                        break;
                    case 0x02:         //静脉注射 
                        byte[] wordData4 = new byte[2];     //药物ID
                        byte[] wordData5 = new byte[2];     //药物剂量
                        byte wordData6 = dataBytes[7];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData4, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData5, 0, 2);
                        Medication.IV.DrugID = DataConverter.BytesToInt32(wordData4);
                        Medication.IV.Dose.Value = DataConverter.BytesToInt32(wordData5);
                        Medication.IV.Dose.Unit = DataConverter.ByteToInt32(wordData6);
                        break;
                    case 0x03:         //静脉滴注 
                        byte[] wordData7 = new byte[2];     //药物ID
                        byte[] wordData8 = new byte[2];     //药物剂量
                        byte wordData9 = dataBytes[7];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData7, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData8, 0, 2);
                        Medication.IVGtt.DrugID = DataConverter.BytesToInt32(wordData7);
                        Medication.IVGtt.Dose.Value = DataConverter.BytesToInt32(wordData8);
                        Medication.IVGtt.Dose.Unit = DataConverter.ByteToInt32(wordData9);
                        break;
                    case 0x04:         //肌内注射 
                        byte[] wordData10 = new byte[2];     //药物ID
                        byte[] wordData11 = new byte[2];     //药物剂量
                        byte wordData12 = dataBytes[7];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData10, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData11, 0, 2);
                        Medication.IM.DrugID = DataConverter.BytesToInt32(wordData10);
                        Medication.IM.Dose.Value = DataConverter.BytesToInt32(wordData11);
                        Medication.IM.Dose.Unit = DataConverter.ByteToInt32(wordData12);
                        break;
                    case 0x05:         //皮内注射 
                        byte[] wordData13 = new byte[2];     //药物ID
                        byte[] wordData14 = new byte[2];     //药物剂量
                        byte wordData15 = dataBytes[7];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData13, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData14, 0, 2);
                        Medication.ID.DrugID = DataConverter.BytesToInt32(wordData13);
                        Medication.ID.Dose.Value = DataConverter.BytesToInt32(wordData14);
                        Medication.ID.Dose.Unit = DataConverter.ByteToInt32(wordData15);
                        break;
                    case 0x06:         //皮下注射 
                        byte[] wordData16 = new byte[2];     //药物ID
                        byte[] wordData17 = new byte[2];     //药物剂量
                        byte wordData18 = dataBytes[7];      //剂量单位
                        Array.ConstrainedCopy(dataBytes, 3, wordData16, 0, 2);
                        Array.ConstrainedCopy(dataBytes, 5, wordData17, 0, 2);
                        Medication.IH.DrugID = DataConverter.BytesToInt32(wordData16);
                        Medication.IH.Dose.Value = DataConverter.BytesToInt32(wordData17);
                        Medication.IH.Dose.Unit = DataConverter.ByteToInt32(wordData18);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DrugDelivery"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_DrugDelivery(Modle.DrugDelivery DrugDelivery)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_DrugDelivery_Drug(DrugDelivery.Drug));
            dataBytes_List.Add(SetDataBytes_DrugDelivery_Dose(DrugDelivery.Dose));

            return dataBytes_List;
        }

        /// <summary>
        /// 药物
        /// </summary>
        /// <param name="Drug"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_DrugDelivery_Drug(Modle.Drug Drug)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;

            byte[] Bytes = DataConverter.IntToBytes(Drug.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            dataBytes[5] = DataConverter.RouteToByte(Drug.Route);


            return dataBytes;
        }

        /// <summary>
        /// 剂量
        /// </summary>
        /// <param name="Dose"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_DrugDelivery_Dose(Modle.DataValue Dose)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x02;

            byte[] Bytes = DataConverter.IntToBytes(Dose.Value);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            dataBytes[5] = DataConverter.IntToBytes(Dose.Unit)[0];

            return dataBytes;
        }

        /// <summary>
        /// 口服
        /// </summary>
        /// <param name="PO"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Medication_PO(Modle.PO PO)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;

            byte[] Bytes = DataConverter.IntToBytes(PO.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(PO.Dose.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(PO.Dose.Unit)[0];

            return dataBytes;
        }

        /// <summary>
        /// 静脉注射
        /// </summary>
        /// <param name="IV"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Medication_IV(Modle.IV IV)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x02;

            byte[] Bytes = DataConverter.IntToBytes(IV.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(IV.Dose.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(IV.Dose.Unit)[0];

            return dataBytes;
        }

        /// <summary>
        /// 静脉滴注
        /// </summary>
        /// <param name="IVGtt"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Medication_IVGtt(Modle.IVGtt IVGtt)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x03;

            byte[] Bytes = DataConverter.IntToBytes(IVGtt.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(IVGtt.Dose.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(IVGtt.Dose.Unit)[0];

            return dataBytes;
        }

        /// <summary>
        /// 肌内注射 
        /// </summary>
        /// <param name="IM"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Medication_IM(Modle.IM IM)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x04;

            byte[] Bytes = DataConverter.IntToBytes(IM.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(IM.Dose.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(IM.Dose.Unit)[0];

            return dataBytes;
        }

        /// <summary>
        /// 皮内注射
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Medication_ID(Modle.ID ID)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x05;

            byte[] Bytes = DataConverter.IntToBytes(ID.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(ID.Dose.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(ID.Dose.Unit)[0];

            return dataBytes;
        }

        /// <summary>
        /// 皮下注射
        /// </summary>
        /// <param name="IH"></param>
        /// <returns></returns>
        static public byte[] GetDataBytes_Medication_IH(Modle.IH IH)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x37;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x06;

            byte[] Bytes = DataConverter.IntToBytes(IH.DrugID);

            dataBytes[3] = Bytes[0];
            dataBytes[4] = Bytes[1];

            byte[] Bytes2 = DataConverter.IntToBytes(IH.Dose.Value);

            dataBytes[5] = Bytes2[0];
            dataBytes[6] = Bytes2[1];

            dataBytes[7] = DataConverter.IntToBytes(IH.Dose.Unit)[0];

            return dataBytes;
        }
    }
}
