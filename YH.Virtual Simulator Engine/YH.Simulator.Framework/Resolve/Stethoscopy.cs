using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 听诊数据字节数据
    /// </summary>
    static public class Stethoscopy
    {
        /// <summary>
        /// 听诊  Stethoscopy 0x30
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_Stethoscopy(byte[] dataBytes, ref Modle.Stethoscopy Stethoscopy)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x30)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];
            switch (wordKey2)
            {
                case 0x01:      //心
                    Stethoscopy_HeartSound.SetData_Stethoscopy_HeartSound(dataBytes,ref Stethoscopy.HeartSounds);
                    break;
                case 0x02:      //肺
                    Stethoscopy_LungSound.SetData_Stethoscopy_LungSound(dataBytes, ref Stethoscopy.LungSounds);
                    break;
                case 0x03:      //腹
                    Stethoscopy_AbdomenSound.SetData_Stethoscopy_AbdomenSound(dataBytes, ref Stethoscopy.AbdomenSounds);
                    break;
                default:

                    break;
            }
        }

        /// <summary>
        /// 听诊
        /// </summary>
        /// <param name="Stethoscopy"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_Stethoscopy(Modle.Stethoscopy Stethoscopy)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.AddRange(Stethoscopy_HeartSound.SetDataBytes_Stethoscopy_HeartSound(Stethoscopy.HeartSounds));
            dataBytes_List.AddRange(Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound(Stethoscopy.LungSounds));
            dataBytes_List.AddRange(Stethoscopy_AbdomenSound.SetDataBytes_Stethoscopy_AbdomenSound(Stethoscopy.AbdomenSounds));            

            return dataBytes_List;
        }

        static  public class Stethoscopy_HeartSound
        {
            /// <summary>
            /// 听诊-心
            /// </summary>
            /// <param name="dataBytes"></param>
            static public void SetData_Stethoscopy_HeartSound(byte[] dataBytes, ref Modle.HeartSounds HeartSounds)
            {
                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x30)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2!=0x01)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x01:          //二尖瓣区
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        HeartSounds.M.SoundID = DataConverter.ByteToInt32(wordData1);
                        HeartSounds.M.SoundVolume = DataConverter.ByteToSoundVolume(wordData2);
                        break;
                    case 0x02:          //三尖瓣区
                        byte wordData3 = dataBytes[3];
                        byte wordData4 = dataBytes[4];
                        HeartSounds.T.SoundID = DataConverter.ByteToInt32(wordData3);
                        HeartSounds.T.SoundVolume = DataConverter.ByteToSoundVolume(wordData4);
                        break;
                    case 0x03:          //主动脉瓣区
                        byte wordData5 = dataBytes[3];
                        byte wordData6 = dataBytes[4];
                        HeartSounds.A.SoundID = DataConverter.ByteToInt32(wordData5);
                        HeartSounds.A.SoundVolume = DataConverter.ByteToSoundVolume(wordData6);
                        break;
                    case 0x04:          //肺动脉尖瓣区
                        byte wordData7 = dataBytes[3];
                        byte wordData8 = dataBytes[4];
                        HeartSounds.P.SoundID = DataConverter.ByteToInt32(wordData7);
                        HeartSounds.P.SoundVolume = DataConverter.ByteToSoundVolume(wordData8);
                        break;
                    case 0x05:          //主动脉瓣第二听诊区
                        byte wordData9 = dataBytes[3];
                        byte wordData10 = dataBytes[4];
                        HeartSounds.E.SoundID = DataConverter.ByteToInt32(wordData9);
                        HeartSounds.E.SoundVolume = DataConverter.ByteToSoundVolume(wordData10);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="HeartSounds"></param>
            /// <returns></returns>
            static public List<byte[]> SetDataBytes_Stethoscopy_HeartSound(Modle.HeartSounds HeartSounds)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_Stethoscopy_HeartSound_M(HeartSounds.M));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_HeartSound_T(HeartSounds.T));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_HeartSound_A(HeartSounds.A));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_HeartSound_P(HeartSounds.P));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_HeartSound_E(HeartSounds.E));

                return dataBytes_List;
            }

            /// <summary>
            /// 二尖瓣区 M Mitral area
            /// </summary>
            /// <param name="M"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_HeartSound_M(Modle.BodySound M)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x01;
                dataBytes[3] = DataConverter.IntToBytes(M.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(M.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 三尖瓣区 T Tricuspid area
            /// </summary>
            /// <param name="T"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_HeartSound_T(Modle.BodySound T)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x02;
                dataBytes[3] = DataConverter.IntToBytes(T.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(T.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 主动脉瓣区 A Aortic area
            /// </summary>
            /// <param name="A"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_HeartSound_A(Modle.BodySound A)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x03;
                dataBytes[3] = DataConverter.IntToBytes(A.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(A.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 肺动脉瓣区 P Pulmonary area
            /// </summary>
            /// <param name="P"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_HeartSound_P(Modle.BodySound P)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x04;
                dataBytes[3] = DataConverter.IntToBytes(P.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(P.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 主动脉瓣第二听诊区 E Erb
            /// </summary>
            /// <param name="E"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_HeartSound_E(Modle.BodySound E)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x01;
                dataBytes[2] = 0x05;
                dataBytes[3] = DataConverter.IntToBytes(E.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(E.SoundVolume);

                return dataBytes;
            }
        }

        static public class Stethoscopy_LungSound
        {
            /// <summary>
            /// 听诊-肺
            /// </summary>
            /// <param name="dataBytes"></param>
            static public void SetData_Stethoscopy_LungSound(byte[] dataBytes, ref Modle.LungSounds LungSounds)
            {
                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x30)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x02)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x01:          //前右上叶
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        LungSounds.ARUL.SoundID = DataConverter.ByteToInt32(wordData1);
                        LungSounds.ARUL.SoundVolume = DataConverter.ByteToSoundVolume(wordData2);
                        break;
                    case 0x02:          //前右中叶
                        byte wordData3 = dataBytes[3];
                        byte wordData4 = dataBytes[4];
                        LungSounds.ARML.SoundID = DataConverter.ByteToInt32(wordData3);
                        LungSounds.ARML.SoundVolume = DataConverter.ByteToSoundVolume(wordData4);
                        break;
                    case 0x03:          //前右下叶
                        byte wordData5 = dataBytes[3];
                        byte wordData6 = dataBytes[4];
                        LungSounds.ARLL.SoundID = DataConverter.ByteToInt32(wordData5);
                        LungSounds.ARLL.SoundVolume = DataConverter.ByteToSoundVolume(wordData6);
                        break;
                    case 0x04:          //前左上叶
                        byte wordData7 = dataBytes[3];
                        byte wordData8 = dataBytes[4];
                        LungSounds.ALUL.SoundID = DataConverter.ByteToInt32(wordData7);
                        LungSounds.ALUL.SoundVolume = DataConverter.ByteToSoundVolume(wordData8);
                        break;
                    case 0x05:          //前左下叶
                        byte wordData9 = dataBytes[3];
                        byte wordData10 = dataBytes[4];
                        LungSounds.ALLL.SoundID = DataConverter.ByteToInt32(wordData9);
                        LungSounds.ALLL.SoundVolume = DataConverter.ByteToSoundVolume(wordData10);
                        break;
                    case 0x06:          //后左下叶
                        byte wordData11 = dataBytes[3];
                        byte wordData12 = dataBytes[4];
                        LungSounds.PLUL.SoundID = DataConverter.ByteToInt32(wordData11);
                        LungSounds.PLUL.SoundVolume = DataConverter.ByteToSoundVolume(wordData12);
                        break;
                    case 0x07:          //后左下叶
                        byte wordData13 = dataBytes[3];
                        byte wordData14 = dataBytes[4];
                        LungSounds.PLLL.SoundID = DataConverter.ByteToInt32(wordData13);
                        LungSounds.PLLL.SoundVolume = DataConverter.ByteToSoundVolume(wordData14);
                        break;
                    case 0x08:          //后左下叶
                        byte wordData15 = dataBytes[3];
                        byte wordData16 = dataBytes[4];
                        LungSounds.PRUL.SoundID = DataConverter.ByteToInt32(wordData15);
                        LungSounds.PRUL.SoundVolume = DataConverter.ByteToSoundVolume(wordData16);
                        break;
                    case 0x09:          //后左下叶
                        byte wordData17 = dataBytes[3];
                        byte wordData18 = dataBytes[4];
                        LungSounds.PRLL.SoundID = DataConverter.ByteToInt32(wordData17);
                        LungSounds.PRLL.SoundVolume = DataConverter.ByteToSoundVolume(wordData18);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="LungSounds"></param>
            /// <returns></returns>
            static public List<byte[]> SetDataBytes_Stethoscopy_LungSound(Modle.LungSounds LungSounds)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_ARUL(LungSounds.ARUL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_ARML(LungSounds.ARML));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_ARLL(LungSounds.ARLL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_ALUL(LungSounds.ALUL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_ALLL(LungSounds.ALLL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_PLUL(LungSounds.PLUL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_PLLL(LungSounds.PLLL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_PRUL(LungSounds.PRUL));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_LungSound_PRLL(LungSounds.PRLL));

                return dataBytes_List;
            }

            /// <summary>
            /// 前右上叶 ARUL anterior Right upper lobe
            /// </summary>
            /// <param name="ARUL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_ARUL(Modle.BodySound ARUL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x01;
                dataBytes[3] = DataConverter.IntToBytes(ARUL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(ARUL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 前右中叶 ARML anterior Right middle lobe
            /// </summary>
            /// <param name="ARML"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_ARML(Modle.BodySound ARML)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x02;
                dataBytes[3] = DataConverter.IntToBytes(ARML.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(ARML.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 前右下叶 ARLL anterior Right lower lobe
            /// </summary>
            /// <param name="ARLL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_ARLL(Modle.BodySound ARLL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x03;
                dataBytes[3] = DataConverter.IntToBytes(ARLL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(ARLL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 前左上叶 ALUL anterior Left upper lobe
            /// </summary>
            /// <param name="ALUL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_ALUL(Modle.BodySound ALUL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x04;
                dataBytes[3] = DataConverter.IntToBytes(ALUL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(ALUL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 前左下叶 ALLL anterior Left lower lobe
            /// </summary>
            /// <param name="ALLL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_ALLL(Modle.BodySound ALLL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x05;
                dataBytes[3] = DataConverter.IntToBytes(ALLL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(ALLL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 背左上叶 PLUL posterior Left upper lobe
            /// </summary>
            /// <param name="PLUL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_PLUL(Modle.BodySound PLUL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x06;
                dataBytes[3] = DataConverter.IntToBytes(PLUL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(PLUL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 背左下叶 PLLL posterior Left lower lobe
            /// </summary>
            /// <param name="PLLL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_PLLL(Modle.BodySound PLLL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x07;
                dataBytes[3] = DataConverter.IntToBytes(PLLL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(PLLL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 背右上叶 PRUL posterior Right upper lobe
            /// </summary>
            /// <param name="PRUL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_PRUL(Modle.BodySound PRUL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x08;
                dataBytes[3] = DataConverter.IntToBytes(PRUL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(PRUL.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 背右下叶 PRLL posterior Right lower lobe
            /// </summary>
            /// <param name="PRLL"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_LungSound_PRLL(Modle.BodySound PRLL)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x02;
                dataBytes[2] = 0x09;
                dataBytes[3] = DataConverter.IntToBytes(PRLL.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(PRLL.SoundVolume);

                return dataBytes;
            }
        }

        static public class Stethoscopy_AbdomenSound
        {
            /// <summary>
            /// 听诊-腹
            /// </summary>
            /// <param name="dataBytes"></param>
            static public void SetData_Stethoscopy_AbdomenSound(byte[] dataBytes,ref Modle.AbdomenSounds AbdomenSounds)
            {
                byte wordKey1 = dataBytes[0];
                if (wordKey1 != 0x30)
                    return;

                byte wordKey2 = dataBytes[1];
                if (wordKey2 != 0x03)
                    return;

                byte wordKey3 = dataBytes[2];

                switch (wordKey3)
                {
                    case 0x01:          //肠鸣音
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        AbdomenSounds.Bowel.SoundID = DataConverter.ByteToInt32(wordData1);
                        AbdomenSounds.Bowel.SoundVolume = DataConverter.ByteToSoundVolume(wordData2);
                        break;
                    case 0x02:          //血管杂音
                        byte wordData3 = dataBytes[3];
                        byte wordData4 = dataBytes[4];
                        AbdomenSounds.Vascular.SoundID = DataConverter.ByteToInt32(wordData3);
                        AbdomenSounds.Vascular.SoundVolume = DataConverter.ByteToSoundVolume(wordData4);
                        break;
                    default:
                        break;
                }
            }

            /// <summary>
            /// 听诊-腹
            /// </summary>
            /// <param name="AbdomenSounds"></param>
            /// <returns></returns>
            static public List<byte[]> SetDataBytes_Stethoscopy_AbdomenSound(Modle.AbdomenSounds AbdomenSounds)
            {
                List<byte[]> dataBytes_List = new List<byte[]>();

                dataBytes_List.Add(SetDataBytes_Stethoscopy_AbdomenSound_Bowel(AbdomenSounds.Bowel));
                dataBytes_List.Add(SetDataBytes_Stethoscopy_AbdomenSound_Vascular(AbdomenSounds.Vascular));

                return dataBytes_List;
            }

            /// <summary>
            ///  肠鸣音 Bowel Sounds
            /// </summary>
            /// <param name="Bowel"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_AbdomenSound_Bowel(Modle.BodySound Bowel)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x01;
                dataBytes[3] = DataConverter.IntToBytes(Bowel.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(Bowel.SoundVolume);

                return dataBytes;
            }

            /// <summary>
            /// 血管音 Vascular Sounds
            /// </summary>
            /// <param name="Vascular"></param>
            /// <returns></returns>
            static public byte[] SetDataBytes_Stethoscopy_AbdomenSound_Vascular(Modle.BodySound Vascular)
            {
                byte[] dataBytes = new byte[8];

                dataBytes[0] = 0x30;
                dataBytes[1] = 0x03;
                dataBytes[2] = 0x02;
                dataBytes[3] = DataConverter.IntToBytes(Vascular.SoundID)[0];
                dataBytes[4] = DataConverter.SoundVolumeToByte(Vascular.SoundVolume);

                return dataBytes;
            }
        }        
    }
}
