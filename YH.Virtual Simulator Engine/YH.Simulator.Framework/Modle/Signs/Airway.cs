using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.Modle
{
    /// <summary>
    /// 气道
    /// </summary>
    public struct Airway
    {

        static private Airway _default;

        static public Airway Default;

        static Airway()
        {
            _default.TongueEdema = Controller.Default;
            _default.TongueFallback = Controller.Default;
            _default.FBAO = Controller.Default;
            _default.PharyngealObstruction = Controller.Default;
            _default.Laryngospasm = Controller.Default;
            _default.Trismus = Controller.Default;
            _default.NeckAnkylosis = Controller.Default;
            _default.Resistance = Resistance.Default;
            _default.Compliance = Compliance.Default;
            _default.Aerothorax = Aerothorax.Default;
            _default.StomachDistention = Controller.Default;
            _default.ExhaleCO2 = Controller.Default;
            _default.AutonomousRespiration = AutonomousRespiration.Default;

        }

        /// <summary>
        /// 舌水肿
        /// </summary>
        public Controller TongueEdema;

        /// <summary>
        /// 舌头后坠
        /// </summary>
        public Controller TongueFallback;

        /// <summary>
        /// 异物气道阻塞 FBAO:Foreign Body Airway Obstruction
        /// </summary>
        public Controller FBAO;


        /// <summary>
        /// 咽部阻塞 Pharyngeal obstruction
        /// </summary>
        public Controller PharyngealObstruction;

        /// <summary>
        /// 喉痉挛 Laryngospasm
        /// </summary>
        public Controller Laryngospasm;

        /// <summary>
        /// 牙关紧闭
        /// </summary>
        public Controller Trismus;

        /// <summary>
        /// 颈部强直 Neck ankylosis
        /// </summary>
        public Controller NeckAnkylosis;

        /// <summary>
        /// 肺阻力
        /// </summary>
        public Resistance Resistance;

        /// <summary>
        /// 顺应性 
        /// </summary>
        public Compliance Compliance;

        /// <summary>
        /// 气胸
        /// </summary>
        public Aerothorax Aerothorax;

        /// <summary>
        /// 胃胀气 
        /// </summary>
        public Controller StomachDistention;

        /// <summary>
        /// 呼出CO2
        /// </summary>
        public Controller ExhaleCO2;

        /// <summary>
        /// 自主呼吸
        /// </summary>
        public AutonomousRespiration AutonomousRespiration;

    }

    /// <summary>
    /// 肺阻力
    /// </summary>
    public struct Resistance
    {
        static private Resistance _default;

        static public Resistance Default
        {
            get { return _default; }
        }

        static Resistance()
        {
            _default.Right = Controller.Default;
            _default.Left = Controller.Default;
            
        }
        public Controller Right;
        public Controller Left;
       
    }

    /// <summary>
    /// 顺应性
    /// </summary>
    public struct Compliance
    {
        static private Compliance _default;

        static public Compliance Default
        {
            get { return _default; }
        }

        static Compliance()
        {
            _default.Right = Controller.Default;
            _default.Left = Controller.Default;            
        }

        public Controller Right;
        public Controller Left;        
    }

    /// <summary>
    /// 气胸
    /// </summary>
    public struct Aerothorax
    {
        static private Aerothorax _default;

        static public Aerothorax Default
        {
            get { return _default; }
        }

        static Aerothorax()
        {
            _default.Right = Controller.Default;
            _default.Left = Controller.Default;
        }

        public Controller Right;
        public Controller Left;

    }

    /// <summary>
    /// 自主呼吸
    /// </summary>
    public struct AutonomousRespiration
    {
        static private AutonomousRespiration _default;

        static public AutonomousRespiration Default
        {
            get { return _default; }
        }

        static AutonomousRespiration()
        {
            _default.Right = Controller.Default;
            _default.Left = Controller.Default;            
        }

        public Controller Right;
        public Controller Left;
        
    }

    
}
