using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework
{
    public class DataConverter
    {
        /// <summary>
        /// int转byte
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public byte[] IntToBytes(int value)
        {

            byte[] src = new byte[4];
            src[0] = (byte)(value & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[3] = (byte)((value >> 24) & 0xFF);
            return src;

        }

        /// <summary>
        /// byte转int
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        static public int BytesToInt(byte[] src, int offset)
        {
            int value;
            value = (int)((src[offset] & 0xFF)
                | ((src[offset + 1] & 0xFF) << 8)
                | ((src[offset + 2] & 0xFF) << 16)
                | ((src[offset + 3] & 0xFF) << 24));
            return value;
        }

        /// <summary>
        /// 字节转整数
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public int ByteToInt32(byte DataByte)
        {
            int volume = 0;

            byte[] volueByte = new byte[4];

            volueByte[0] = DataByte;

            volume = BitConverter.ToInt32(volueByte, 0);

            return volume;
        }

        /// <summary>
        /// 字节数据转整数
        /// </summary>
        /// <param name="DataBytes"></param>
        /// <returns></returns>
        static public int BytesToInt32(byte[] DataBytes)
        {
            int volume = 0;

            byte[] volueByte = new byte[4];

            Array.ConstrainedCopy(DataBytes, 0, volueByte, 0, DataBytes.Length);

            volume = BitConverter.ToInt32(volueByte, 0);

            return volume;
        }            
        
        /// <summary>
        /// 操作状态转字节
        /// </summary>
        /// <param name="OperatorStatus"></param>
        /// <returns></returns>
        static public byte OperatorStatusToByte(OperatorStatus OperatorStatus)
        {
            byte DataByte = 0x00;

            if (OperatorStatus == OperatorStatus.Yes)
                DataByte = 0x01;

            return DataByte;
        }

        /// <summary>
        /// 字节转操作状态
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public OperatorStatus ByteToOperatorStatus(byte DataByte)
        {
            OperatorStatus operatorStatus = OperatorStatus.No;

            switch (DataByte)
            {
                case 0x00:
                    operatorStatus = OperatorStatus.No;
                    break;
                case 0x01:
                    operatorStatus = OperatorStatus.Yes;
                    break;
                default:
                    operatorStatus = OperatorStatus.No;
                    break;
            }

            return operatorStatus;
        }

        /// <summary>
        /// 字节转给药途径
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public Route ByteToRoute(byte DataByte)
        {
            Route route = Route.nothing;

            byte[] volueByte = new byte[4];

            volueByte[0] = DataByte;

            int volume = BitConverter.ToInt32(volueByte, 0);

            if (volume >= 0 && volume <= 6)
                route = (Route)volume;

            return route;
        }

        /// <summary>
        /// 给药途径转字节
        /// </summary>
        /// <param name="Route"></param>
        /// <returns></returns>
        static public byte RouteToByte(Route Route)
        {
            byte volueByte = new byte();

            byte[] volueBytes = IntToBytes((int)Route);

            volueByte = volueBytes[0];

            return volueByte;
        }

        /// <summary>
        /// 字节转播放模式
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public PlaybackMode ByteToPlaybackMode(byte DataByte)
        {
            PlaybackMode playbackMode = PlaybackMode.Stop;

            byte[] volueByte = new byte[4];

            volueByte[0] = DataByte;

            int volume = BitConverter.ToInt32(volueByte, 0);

            playbackMode = (PlaybackMode)volume;

            return playbackMode;
        }

        /// <summary>
        /// 播放模式转字节
        /// </summary>
        /// <param name="PlaybackMode"></param>
        /// <returns></returns>
        static public byte PlaybackModeToByte(PlaybackMode PlaybackMode)
        {
            byte volueByte = new byte();

            byte[] volueBytes = IntToBytes((int)PlaybackMode);

            volueByte = volueBytes[0];

            return volueByte;
        }

        /// <summary>
        /// 字节转音量
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public SoundVolume ByteToSoundVolume(byte DataByte)
        {
            SoundVolume soundVolume = SoundVolume.V0;

            byte[] volueBytes = new byte[4];

            volueBytes[0] = DataByte;

            int volume = BitConverter.ToInt32(volueBytes, 0);

            if (volume >= 0 && volume <= 10)
                soundVolume = (SoundVolume)volume;

            return soundVolume;
        }

        /// <summary>
        /// 音量转字节
        /// </summary>
        /// <param name="SoundVolume"></param>
        /// <returns></returns>
        static public byte SoundVolumeToByte(SoundVolume SoundVolume)
        {
            byte volueByte = new byte();

            byte[] volueBytes = IntToBytes((int)SoundVolume);

            volueByte = volueBytes[0];
                         
            return volueByte;
        }

        /// <summary>
        /// 字节转控制器状态
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public ControllerStatus ByteToControllerStatus(byte DataByte)
        {
            ControllerStatus controllerStatus = ControllerStatus.No;

            switch (DataByte)
            {
                case 0x00:
                    controllerStatus = ControllerStatus.No;
                    break;
                case 0x01:
                    controllerStatus = ControllerStatus.Yes;
                    break;
                default:
                    controllerStatus = ControllerStatus.No;
                    break;
            }

            return controllerStatus;
        }

        /// <summary>
        /// 控制器状态转字节
        /// </summary>
        /// <param name="ControllerStatus"></param>
        /// <returns></returns>
        static public byte ControllerStatusToByte(ControllerStatus ControllerStatus)
        {
            byte dataByte = new byte();

            switch (ControllerStatus)
            {
                case ControllerStatus.No:
                    dataByte = 0x00;
                    break;
                case ControllerStatus.Yes:
                    dataByte = 0x01;
                    break;
                default:
                    dataByte = 0x00;
                    break;
            }

            return dataByte;
        }

        /// <summary>
        /// 字节转眼脸状态
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public EyelidStatus ByteToEyelidStatus(byte DataByte)
        {
            EyelidStatus eyelidStatus = EyelidStatus.WideOpen;

            switch (DataByte)
            {
                case 0x01:
                    eyelidStatus = EyelidStatus.WideOpen;
                    break;
                case 0x02:
                    eyelidStatus = EyelidStatus.HalfOpen;
                    break;
                case 0x03:
                    eyelidStatus = EyelidStatus.Closed;
                    break;
                default:
                    break;
            }

            return eyelidStatus;
        }

        /// <summary>
        /// 眼脸状态转字节
        /// </summary>
        /// <param name="EyelidStatus"></param>
        /// <returns></returns>
        static public byte EyelidStatusToByte(EyelidStatus EyelidStatus)
        {
            byte dataByte = new byte();

            switch (EyelidStatus)
            {
                case  EyelidStatus.WideOpen:
                    dataByte = 0x01;
                    break;
                case EyelidStatus.HalfOpen:
                    dataByte = 0x02;
                    break;
                case EyelidStatus.Closed:
                    dataByte = 0x03;
                    break;
                default:
                    break;
            }
            return dataByte;
        }

        /// <summary>
        /// 字节转眨眼速度
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public BlinkingSpeed ByteToBlinkingSpeed(byte DataByte)
        {
            BlinkingSpeed blinkingSpeed = BlinkingSpeed.Normal;

            switch (DataByte)
            {
                case 0x01:
                    blinkingSpeed = BlinkingSpeed.Normal;
                    break;
                case 0x02:
                    blinkingSpeed = BlinkingSpeed.Infrequently;
                    break;
                case 0x03:
                    blinkingSpeed = BlinkingSpeed.Frequently;
                    break;
                default:
                    break;
            }

            return blinkingSpeed;
        }

        /// <summary>
        /// 眨眼速度转字节
        /// </summary>
        /// <param name="BlinkingSpeed"></param>
        /// <returns></returns>
        static public byte BlinkingSpeedToByte(BlinkingSpeed BlinkingSpeed)
        {           
            byte dataByte = new byte();
            switch (BlinkingSpeed)
            {
                case BlinkingSpeed.Normal:
                    dataByte = 0x01;
                    break;
                case BlinkingSpeed.Infrequently:
                    dataByte = 0x02;
                    break;
                case BlinkingSpeed.Frequently:
                    dataByte = 0x03;
                    break;
                default:
                    break;
            }

            return dataByte;
        }

        /// <summary>
        /// 字节转瞳孔大小
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public PupillSize ByteToPupillSize(byte DataByte)
        {
            PupillSize pupillSize = PupillSize.Normal;

            switch (DataByte)
            {
                case 0x01:
                    pupillSize = PupillSize.Normal;
                    break;
                case 0x02:
                    pupillSize = PupillSize.Narrow;
                    break;
                case 0x03:
                    pupillSize = PupillSize.Loose;
                    break;
                default:
                    break;
            }

            return pupillSize;
        }

        /// <summary>
        /// 瞳孔大小转字节
        /// </summary>
        /// <param name="PupillSize"></param>
        /// <returns></returns>
        static public byte PupillSizeToByte(PupillSize PupillSize)
        {
            byte dataByte = new byte();
            switch (PupillSize)
            {
                case PupillSize.Normal:
                    dataByte = 0x01;
                    break;
                case PupillSize.Narrow:
                    dataByte = 0x02;
                    break;
                case PupillSize.Loose:
                    dataByte = 0x03;
                    break;
                default:
                    break;
            }

            return dataByte;
        }

        /// <summary>
        /// 字节转对光灵敏度
        /// </summary>
        /// <param name="DataByte"></param>
        /// <returns></returns>
        static public LightSensitivity ByteToLightSensitivity(byte DataByte)
        {
            LightSensitivity lightSensitivity = LightSensitivity.Normal;

            switch (DataByte)
            {
                case 0x01:
                    lightSensitivity = LightSensitivity.Normal;
                    break;
                case 0x02:
                    lightSensitivity = LightSensitivity.Slow;
                    break;
                case 0x03:
                    lightSensitivity = LightSensitivity.None;
                    break;
                default:
                    break;
            }

            return lightSensitivity;
        }

        /// <summary>
        /// 对光灵敏度转字节
        /// </summary>
        /// <param name="LightSensitivity"></param>
        /// <returns></returns>
        static public byte LightSensitivityToByte (LightSensitivity LightSensitivity)
        {
            byte dataByte = new byte();

            switch (LightSensitivity)
            {
               
                case LightSensitivity.Normal:
                    dataByte = 0x01;
                    break;
                case LightSensitivity.Slow:
                    dataByte = 0x02;
                    break;
                case LightSensitivity.None:
                    dataByte = 0x03;
                    break;
                default:
                    break;
            }

            return dataByte;
        }

        /// <summary>
        /// struct转换为byte[]
        /// </summary>
        /// <param name="structObj"></param>
        /// <returns></returns>
        static public byte[] StructToBytes(object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// byte[]转换为struct
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public object BytesToStruct(byte[] bytes, Type type)
        {
            int size = Marshal.SizeOf(type);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, type);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
    }
}
