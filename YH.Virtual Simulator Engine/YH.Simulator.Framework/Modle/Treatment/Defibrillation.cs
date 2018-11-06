using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 除颤
    /// </summary>
    public struct Defibrillation
    {

        static private Defibrillation _default;

        static public Defibrillation Default
        {
            get { return _default; }
        }

        static Defibrillation()
        {
            _default.Energy = 0;

        }
        
        public int Energy;
    }

    /// <summary>
    /// 除颤电极
    /// </summary>
    public struct DefibrillatorElectrode
    {

        static private DefibrillatorElectrode _default;

        static public DefibrillatorElectrode Default
        {
            get { return _default; }
        }

        static DefibrillatorElectrode()
        {
            _default.Sterno = Operator.Default;
            _default.Apex = Operator.Default;

        }

        /// <summary>
        /// 胸骨(Sterno)
        /// </summary>
        public Operator Sterno;

        /// <summary>
        /// 心尖搏动点(Apex)
        /// </summary>
        public Operator Apex;
    }

}
