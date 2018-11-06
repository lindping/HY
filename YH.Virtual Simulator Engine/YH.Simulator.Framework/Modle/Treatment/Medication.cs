using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 药物治疗  Medication
    /// </summary>
    public struct Medication
    {
        static private Medication _default;

        static public Medication Default
        {
            get { return _default; }
        }

        static Medication()
        {
            _default.PO = PO.Default;
            _default.IV = IV.Default;
            _default.IVGtt = IVGtt.Default;
            _default.IM = IM.Default;
            _default.ID = ID.Default;
            _default.IH = IH.Default;
        }

        /// <summary>
        /// 口服
        /// </summary>
        public PO PO;

        /// <summary>
        /// 静脉注射
        /// </summary>
        public IV IV;

        /// <summary>
        /// 静脉滴注
        /// </summary>
        public IVGtt IVGtt;

        /// <summary>
        /// 肌内注射
        /// </summary>
        public IM IM;

        /// <summary>
        /// 皮内注射
        /// </summary>
        public ID ID;

        /// <summary>
        /// 皮下注射
        /// </summary>
        public IH IH;
    }

    /// <summary>
    /// 口服
    /// </summary>
    public struct PO
    {
        static private PO _default;

        static public PO Default
        {
            get { return _default; }
        }

        static PO()
        {
            _default.DrugID = 0;
            _default.Dose = DataValue.Default;
        }

        public int DrugID;
        public DataValue Dose;

    }

    /// <summary>
    /// 静脉注射
    /// </summary>
    public struct IV
    {
        static private IV _default;

        static public IV Default
        {
            get { return _default; }
        }

        static IV()
        {
            _default.DrugID = 0;
            _default.Dose = DataValue.Default;
        }

        public int DrugID;
        public DataValue Dose;
    }

    /// <summary>
    /// 静脉滴注
    /// </summary>
    public struct IVGtt
    {
        static private IVGtt _default;

        static public IVGtt Default
        {
            get { return _default; }
        }

        static IVGtt()
        {
            _default.DrugID = 0;
            _default.Dose = DataValue.Default;
        }

        public int DrugID;
        public DataValue Dose;
    }

    /// <summary>
    /// 肌内注射
    /// </summary>
    public struct IM
    {
        static private IM _default;

        static public IM Default
        {
            get { return _default; }
        }

        static IM()
        {
            _default.DrugID = 0;
            _default.Dose = DataValue.Default;
        }

        public int DrugID;
        public DataValue Dose;
    }

    /// <summary>
    /// 皮内注射
    /// </summary>
    public struct ID
    {
        static private ID _default;

        static public ID Default
        {
            get { return _default; }
        }

        static ID()
        {
            _default.DrugID = 0;
            _default.Dose = DataValue.Default;
        }

        public int DrugID;
        public DataValue Dose;
    }

    /// <summary>
    /// 皮下注射
    /// </summary>
    public struct IH
    {
        static private IH _default;

        static public IH Default
        {
            get { return _default; }
        }

        static IH()
        {
            _default.DrugID = 0;
            _default.Dose = DataValue.Default;
        }

        public int DrugID;
        public DataValue Dose;
    }
}
