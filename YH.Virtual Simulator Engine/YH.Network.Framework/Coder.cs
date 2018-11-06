using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Network.Framework
{
    /// <summary>
    /// 名称：Coder
    /// 描述：通讯服务提供编码和解码服务.
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YH
    /// 用例：
    /// </summary>
    public class Coder
    {

        public enum EncodingMothord
        {
            Default = 0,
            Unicode,
            UTF8,
            ASCII,
        }
        /// <summary>
        /// 编码方式
        /// </summary>
        private EncodingMothord _encodingMothord;

        protected Coder()
        {

        }

        public Coder(EncodingMothord encodingMothord)
        {
            _encodingMothord = encodingMothord;
        }

        /// <summary>
        /// 通讯数据解码
        /// </summary>
        /// <param name="dataBytes">需要解码的数据</param>
        /// <returns>编码后的数据</returns>
        public virtual string GetEncodingString(byte[] dataBytes, int size)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetString(dataBytes, 0, size);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetString(dataBytes, 0, size);
                    }
                default:
                    {
                        throw (new Exception("未定义的编码格式"));
                    }
            }

        }

        public virtual byte[] GetDecodingBytes(byte[] dataBytes, int size)
        {
            int dataLength = ByteToInt32(dataBytes[4]);

            byte[] resultDataBytes = new byte[dataLength];

            Array.ConstrainedCopy(dataBytes, 5, resultDataBytes, 0, dataLength);

            return resultDataBytes;
        }

        static public int ByteToInt32(byte DataByte)
        {
            int volume = 0;

            byte[] volueByte = new byte[4];

            volueByte[0] = DataByte;

            volume = BitConverter.ToInt32(volueByte, 0);

            return volume;
        }

        public byte[] IntToBytes(int value)
        {

            byte[] src = new byte[4];
            src[0] = (byte)(value & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[3] = (byte)((value >> 24) & 0xFF);
            return src;
        }

        public virtual byte[] GetEncodingBytes(byte[] dataBytes, DatagramResolver resolver)
        {
            byte[] resultDataBytes = resolver.Resolve(ref dataBytes);

            return resultDataBytes;
        }

        /// <summary>
        /// 数据编码
        /// </summary>
        /// <param name="datagram">需要编码的报文</param>
        /// <returns>编码后的数据</returns>
        public virtual byte[] GetEncodingBytes(string datagram)
        {
            switch (_encodingMothord)
            {
                case EncodingMothord.Default:
                    {
                        return Encoding.Default.GetBytes(datagram);
                    }
                case EncodingMothord.Unicode:
                    {
                        return Encoding.Unicode.GetBytes(datagram);
                    }
                case EncodingMothord.UTF8:
                    {
                        return Encoding.UTF8.GetBytes(datagram);
                    }
                case EncodingMothord.ASCII:
                    {
                        return Encoding.ASCII.GetBytes(datagram);
                    }
                default:
                    {
                        throw (new Exception("未定义的编码格式"));
                    }
            }
        }
    }
}
