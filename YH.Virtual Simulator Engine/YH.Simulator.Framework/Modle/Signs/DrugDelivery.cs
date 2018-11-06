using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{    
    /// <summary>
    /// 给药 Drug Delivery
    /// </summary>
    public struct DrugDelivery
    {
        static private DrugDelivery _default;

        static public DrugDelivery Default
        {
            get { return _default; }
        }

        static DrugDelivery()
        {
            _default.Drug = Drug.Default;
            _default.Dose = DataValue.Default;
        }

        /// <summary>
        /// 药物
        /// </summary>
        public Drug Drug;

        /// <summary>
        /// 剂量
        /// </summary>
        public DataValue Dose;
    }

    /// <summary>
    /// 药物
    /// </summary>
    public struct Drug
    {
        static private Drug _default;

        static public Drug Default
        {
            get { return _default; }
        }

        static Drug()
        {
            _default.DrugID = 0;
            _default.Route = Route.nothing;
        }

        public int DrugID;
        public Route Route;
    }

    public enum Route
    {
        nothing = 0,
        /// <summary>
        /// 口服
        /// </summary>
        po = 1,
        /// <summary>
        /// 静脉注射
        /// </summary>
        iv = 2,
        /// <summary>
        /// 静脉滴注
        /// </summary>
        ivgtt = 3,
        /// <summary>
        /// 肌内注射
        /// </summary>
        im = 4,
        /// <summary>
        /// 皮内注射
        /// </summary>
        id = 5,
        /// <summary>
        /// 皮下注射
        /// </summary>
        ih = 6,
    }

    /// <summary>
    /// 剂量单位
    /// </summary>
    public enum DoseUnit
    {
        /// <summary>
        /// 无
        /// </summary>
        nothing,
        /// <summary>
        /// 毫克
        /// </summary>
        mg,
        /// <summary>
        /// 毫升
        /// </summary>
        ml,
    }
}
