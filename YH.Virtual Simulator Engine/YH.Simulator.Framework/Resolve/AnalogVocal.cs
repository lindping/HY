using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    /// <summary>
    /// 语音数据字节处理 0x22
    /// </summary>
    static public class AnalogVocal
    {
        /// <summary>
        /// 声音  Analog Voice
        /// </summary>
        /// <param name="dataBytes"></param>
        static public void SetData_AnalogVoice(byte[] dataBytes, ref Modle.AnalogVocal AnalogVocal)
        {
            if (dataBytes.Length != 8)
                return;

            byte wordKey1 = dataBytes[0];
            if (wordKey1 != 0x22)
                return;
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey2)
            {
                case 0x01:      //发声
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        byte wordData3 = dataBytes[5];
                        AnalogVocal.Vocal.SoundID = DataConverter.ByteToInt32(wordData1);
                        AnalogVocal.Vocal.PlaybackMode = DataConverter.ByteToPlaybackMode(wordData2);
                        AnalogVocal.Vocal.SoundVolume = DataConverter.ByteToSoundVolume(wordData3);
                    }
                    break;
                case 0x02:      //语音
                    if (wordKey3 == 0x01)
                    {
                        byte wordData1 = dataBytes[3];
                        byte wordData2 = dataBytes[4];
                        byte wordData3 = dataBytes[5];
                        AnalogVocal.Voice.SoundID = DataConverter.ByteToInt32(wordData1);
                        AnalogVocal.Voice.PlaybackMode = DataConverter.ByteToPlaybackMode(wordData2);
                        AnalogVocal.Voice.SoundVolume = DataConverter.ByteToSoundVolume(wordData3);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 模拟语音
        /// </summary>
        /// <param name="AnalogVocal"></param>
        /// <returns></returns>
        static public List<byte[]> SetDataBytes_AnalogVocal(Modle.AnalogVocal AnalogVocal)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.Add(SetDataBytes_AnalogVocal_Vocal(AnalogVocal.Vocal));
            dataBytes_List.Add(SetDataBytes_AnalogVocal_Voice(AnalogVocal.Voice));

            return dataBytes_List;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vocal"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_AnalogVocal_Vocal(Modle.Vocal Vocal)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x22;
            dataBytes[1] = 0x01;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(Vocal.SoundID)[0];
            dataBytes[4] = DataConverter.PlaybackModeToByte(Vocal.PlaybackMode);
            dataBytes[5] = DataConverter.SoundVolumeToByte(Vocal.SoundVolume);

            return dataBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Voice"></param>
        /// <returns></returns>
        static public byte[] SetDataBytes_AnalogVocal_Voice(Modle.Voice Voice)
        {
            byte[] dataBytes = new byte[8];

            dataBytes[0] = 0x22;
            dataBytes[1] = 0x02;
            dataBytes[2] = 0x01;
            dataBytes[3] = DataConverter.IntToBytes(Voice.SoundID)[0];
            dataBytes[4] = DataConverter.PlaybackModeToByte(Voice.PlaybackMode);
            dataBytes[5] = DataConverter.SoundVolumeToByte(Voice.SoundVolume);

            return dataBytes;
        }
    }
}
