using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Network.Framework
{
    /// <summary>
    /// 名称：DatagramResolver
    /// 描述：数据报文分析器
    /// 版本：1.0
    /// 创建人：John liao
    /// 创建日期：2018.8.16
    /// 修改人：
    /// 修改日期：
    /// 版权所有：YH
    /// 用例：
    /// </summary>
    public class DatagramResolver
    {

        private byte[] _beginBytes = new byte[4];

        public byte[] BeginBytes
        {
            get
            {
                return _beginBytes;
            }
        }

        /// <summary>
        /// 报文结束标记
        /// </summary>
        private string _endTag;

        /// <summary>
        /// 返回结束标记
        /// </summary>
        string EndTag
        {
            get
            {
                return _endTag;
            }
        }

        /// <summary>
        /// 受保护的默认构造函数,提供给继承类使用
        /// </summary>
        protected DatagramResolver()
        {

        }

        public DatagramResolver(byte[] beginBytes)
        {
            _beginBytes = beginBytes;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endTag">报文结束标记</param>
        public DatagramResolver(string endTag)
        {           

            if (endTag == null)
            {
                throw (new ArgumentNullException("结束标记不能为null"));
            }

            if (endTag == "")
            {
                throw (new ArgumentException("结束标记符号不能为空字符串"));
            }

            this._endTag = endTag;
        }

        public virtual byte[] Resolve(ref byte[] dataBytes)
        {
            byte[] resultDataBytes = new byte[_beginBytes.Length + 1+ dataBytes.Length];

            Array.ConstrainedCopy(_beginBytes, 0, resultDataBytes, 0, 4);
            resultDataBytes[4] = IntToBytes(dataBytes.Length)[0];
            Array.ConstrainedCopy(dataBytes, 0, resultDataBytes, 5, dataBytes.Length);

            return resultDataBytes;
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

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="rawDatagram">原始数据,返回未使用的报文片断,
        /// 该片断会保存在Session的Datagram对象中</param>
        /// <returns>报文数组,原始数据可能包含多个报文</returns>
        public virtual string[] Resolve(ref string rawDatagram)
        {
            ArrayList datagrams = new ArrayList();

            //末尾标记位置索引
            int tagIndex = -1;

            while (true)
            {
                tagIndex = rawDatagram.IndexOf(_endTag, tagIndex + 1);

                if (tagIndex == -1)
                {
                    break;
                }
                else
                {
                    //按照末尾标记把字符串分为左右两个部分
                    string newDatagram = rawDatagram.Substring(0, tagIndex + _endTag.Length);

                    datagrams.Add(newDatagram);

                    if (tagIndex + _endTag.Length >= rawDatagram.Length)
                    {
                        rawDatagram = "";

                        break;
                    }

                    rawDatagram = rawDatagram.Substring(tagIndex + _endTag.Length,
                        rawDatagram.Length - newDatagram.Length);

                    //从开始位置开始查找
                    tagIndex = 0;
                }
            }

            string[] results = new string[datagrams.Count];

            datagrams.CopyTo(results);

            return results;
        }
    }
}
