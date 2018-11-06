using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// CPR-Q 流程
    /// </summary>
    public struct CPR_Q
    {
        static private CPR_Q _default;

        static public CPR_Q Default
        {
            get { return _default; }
        }

        static CPR_Q()
        {
            _default.Pat = Operator.Default;
            _default.Shout = Operator.Default;
            _default.CheckRightCarotid = Operator.Default;
            _default.CheckLeftCarotid = Operator.Default;
            _default.HeadTiltChinLift = Operator.Default;
            _default.RemovalOralForeignBody = Operator.Default;

        }
        /// <summary>
        /// 拍打 Pat
        /// </summary>
        public Operator Pat;

        /// <summary>
        /// 呼叫 Shout
        /// </summary>
        public Operator Shout;

        /// <summary>
        /// 检查颈动脉
        /// </summary>
        public Operator CheckRightCarotid;

        /// <summary>
        /// 检查颈动脉
        /// </summary>
        public Operator CheckLeftCarotid;

        /// <summary>
        /// 仰头抬颏
        /// </summary>
        public Operator HeadTiltChinLift;

        /// <summary>
        /// 清除口腔异物
        /// </summary>
        public Operator RemovalOralForeignBody;
                
    }


    /// <summary>
    /// CPR-P操作
    /// </summary>
    public struct CPR_P
    {
        static private CPR_P _default;

        static public CPR_P Default
        {
            get { return _default; }
        }

        static CPR_P()
        {
            _default.PressDepth = 0;
            _default.PressPosition = 0;
            _default.BlowVolume = 0;

        }
        /// <summary>
        /// 按压深度
        /// </summary>
        public int PressDepth;

        /// <summary>
        /// 按压位置
        /// </summary>
        public int PressPosition;

        /// <summary>
        /// 吹气量
        /// </summary>
        public int BlowVolume;
    }
}
