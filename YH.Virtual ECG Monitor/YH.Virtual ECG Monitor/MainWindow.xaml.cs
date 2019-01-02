using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YH.Network.Framework;
using Resolve = YH.Simulator.Framework.Resolve;
using Modle = YH.Simulator.Framework.Modle;
using System.Windows.Threading;
using DataDictionary = YH.Simulator.Framework.DataDictionary;
using System.Configuration;

namespace YH.Virtual_ECG_Monitor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //生命体征
        Modle.Signs _signs;

        //治疗、处理
        Modle.Treatment _treatment;

        NetClient m_NetClient;
        public MainWindow()
        {
            InitializeComponent();

          //  NetInitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindData();

            MyInitialize();

            ShowData(_signs);
        }

        #region 数据传输

        private void NetInitializeComponent()
        {
            m_NetClient = new NetClient(new Coder(Coder.EncodingMothord.Default));

            //m_NetClient.Resovlver=new DatagramResolver("]}");

            m_NetClient.Resovlver = new Network.Framework.DatagramResolver(new byte[4] { 0xfa, 0xfb, 0xfc, 0xfd });

            m_NetClient.ReceivedDatagram += M_NetClient_ReceivedDatagram;

            m_NetClient.DisConnectedServer += M_NetClient_DisConnectedServer;

            m_NetClient.ConnectedServer += M_NetClient_ConnectedServer;

            //  m_NetClient.Connect("127.0.0.1", 8899);
            //m_NetClient.Connect("10.10.100.254", 8899);
            string ip = ConfigurationManager.AppSettings["NetClientIP"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["NetClientPort"].ToString());
            m_NetClient.Connect(ip, port);
        }

        private void M_NetClient_ConnectedServer(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info = string.Format("A Client:{0} connect server :{1}", e.ClientSession,
             e.ClientSession.ClientSocket.RemoteEndPoint.ToString());

        }

        private void M_NetClient_DisConnectedServer(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info;

            if (e.ClientSession.TypeOfExit == Session.ExitType.ExceptionExit)
            {
                info = string.Format("A Client Session:{0} Exception Closed.",
                 e.ClientSession.ID);
            }
            else
            {
                info = string.Format("A Client Session:{0} Normal Closed.",
                 e.ClientSession.ID);
            }
        }

        private void M_NetClient_ReceivedDatagram(object sender, NetEventArgs e)
        {
                                 

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
              //  Modle.Function Function = Resolve.Resolve.GetData_Treatment(e.ClientSession.RecvDataBuffer, ref _treatment);
              //  SetData_Treatment(Function, _treatment);
            });
        }

        private void SendBytes(byte[] dataBytes)
        {
            try
            {
                m_NetClient.SendBytes(dataBytes);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "错误");
            }
        }


        #endregion


        #region 初始化

        private void MyInitialize()
        {

            MyInitialize_Signs();

            MyInitialize_Treatment();
        }

        private void MyInitialize_Signs()
        {

            //眼睛
            //眼脸状态 Eyelid
            _signs.Eyes.Eyelid.Left = _signs.Eyes.Eyelid.Right = Modle.EyelidStatus.WideOpen;
            //眨眼速度 Blinking
            _signs.Eyes.Blinking.Left = _signs.Eyes.Blinking.Right = Modle.BlinkingSpeed.Normal;
            //瞳孔大小 Pupill
            _signs.Eyes.Pupill.Left = _signs.Eyes.Pupill.Right = Modle.PupillSize.Normal;
            //对光反射 Light
            _signs.Eyes.Light.Left = _signs.Eyes.Light.Right = Modle.LightSensitivity.Normal;

            //紫绀 Cyanosis
            //中心性紫绀 Central Cyanosis
            //口唇 Lips_CyanosisDegree
            _signs.Cyanosis.Lips.Status = Modle.ControllerStatus.No;
            _signs.Cyanosis.Lips.Value = 0;
            //周围性紫绀 Peripheral Cyanosis
            //手指甲床 FingernailBed_CyanosisDegree
            _signs.Cyanosis.Fingernail.Status = Modle.ControllerStatus.No;
            _signs.Cyanosis.Fingernail.Value = 0;
            //脚指甲床 FootNailBed_CyanosisDegree
            _signs.Cyanosis.FootNail.Status = Modle.ControllerStatus.No;
            _signs.Cyanosis.FootNail.Value = 0;

            //声音 AnalogVocal
            //发声 Vocal
            _signs.AnalogVocal.Vocal.SoundID = 0;
            _signs.AnalogVocal.Vocal.PlaybackMode = Simulator.Framework.Modle.PlaybackMode.Stop;
            _signs.AnalogVocal.Vocal.SoundVolume = Modle.SoundVolume.V5;
            //语音 Voice
            _signs.AnalogVocal.Voice.SoundID = 0;
            _signs.AnalogVocal.Voice.PlaybackMode = Simulator.Framework.Modle.PlaybackMode.Stop;
            _signs.AnalogVocal.Voice.SoundVolume = Modle.SoundVolume.V5;

            //气道
            //舌水肿 TongueEdema
            _signs.Airway.TongueEdema.Status = Modle.ControllerStatus.No;
            _signs.Airway.TongueEdema.Value = 0;
            //舌头后坠 TongueFallback
            _signs.Airway.TongueFallback.Status = Modle.ControllerStatus.No;
            _signs.Airway.TongueFallback.Value = 0;
            //异物气道阻塞 FBAO:Foreign Body Airway Obstruction
            _signs.Airway.FBAO.Status = Modle.ControllerStatus.No;
            _signs.Airway.FBAO.Value = 0;
            //咽部阻塞 PharyngealObstruction
            _signs.Airway.PharyngealObstruction.Status = Modle.ControllerStatus.No;
            _signs.Airway.PharyngealObstruction.Value = 0;
            //喉痉挛 Laryngospasm
            _signs.Airway.Laryngospasm.Status = Modle.ControllerStatus.No;
            _signs.Airway.Laryngospasm.Value = 0;
            //牙关紧闭 Trismus
            _signs.Airway.Trismus.Status = Modle.ControllerStatus.No;
            _signs.Airway.Trismus.Value = 0;
            //颈部强直 NeckAnkylosis
            _signs.Airway.NeckAnkylosis.Status = Modle.ControllerStatus.No;
            _signs.Airway.NeckAnkylosis.Value = 0;
            //肺阻力 Resistance
            _signs.Airway.Resistance.Left.Status = _signs.Airway.Resistance.Right.Status = Modle.ControllerStatus.No;
            _signs.Airway.Resistance.Left.Value = _signs.Airway.Resistance.Left.Value = 0;
            //顺应性 Compliance
            _signs.Airway.Compliance.Left.Status = _signs.Airway.Compliance.Right.Status = Modle.ControllerStatus.No;
            _signs.Airway.Compliance.Left.Value = _signs.Airway.Compliance.Left.Value = 0;
            //气胸 Aerothorax
            _signs.Airway.Aerothorax.Left.Status = _signs.Airway.Aerothorax.Right.Status = Modle.ControllerStatus.No;
            _signs.Airway.Aerothorax.Left.Value = _signs.Airway.Aerothorax.Left.Value = 0;
            //胃胀气 StomachDistention
            _signs.Airway.StomachDistention.Status = Modle.ControllerStatus.No;
            _signs.Airway.StomachDistention.Value = 0;
            //呼出CO2 ExhaleCO2
            _signs.Airway.ExhaleCO2.Status = Modle.ControllerStatus.No;
            _signs.Airway.ExhaleCO2.Value = 0;
            //自主呼吸 AutonomousRespiration
            _signs.Airway.AutonomousRespiration.Left.Status = _signs.Airway.AutonomousRespiration.Right.Status = Modle.ControllerStatus.No;
            _signs.Airway.AutonomousRespiration.Left.Value = _signs.Airway.AutonomousRespiration.Left.Value = 0;

            //生命体征 VitalSigns
            //循环 Cyclic
            _signs.VitalSigns.Cyclic.Rhythm.Basic = 1;
            _signs.VitalSigns.Cyclic.Rhythm.Extrasystole = 0;
            _signs.VitalSigns.Cyclic.HeartRate.Value = 75;
            _signs.VitalSigns.Cyclic.SpO2.Value = 98;
            _signs.VitalSigns.Cyclic.IBP.Systolic.Value = 120;
            _signs.VitalSigns.Cyclic.IBP.Diastolic.Value = 80;
            _signs.VitalSigns.Cyclic.PAP.Systolic.Value = 40;
            _signs.VitalSigns.Cyclic.PAP.Diastolic.Value = 35;
            _signs.VitalSigns.Cyclic.CVP.Value = 0;
            _signs.VitalSigns.Cyclic.PAWP.Value = 0;
            _signs.VitalSigns.Cyclic.C_O_.Value = 0;
            //呼吸 Breath
            _signs.VitalSigns.Breath.RespType = 0;
            _signs.VitalSigns.Breath.RespRate.Value = 0;
            _signs.VitalSigns.Breath.InspiratoryCapacity.Value = 0;
            _signs.VitalSigns.Breath.RespRatio.Value = 0;
            _signs.VitalSigns.Breath.CO2.Value = 0;
            _signs.VitalSigns.Breath.ETCO2.Value = 0;
            _signs.VitalSigns.Breath.O2.inO2.Value = 0;
            _signs.VitalSigns.Breath.O2.exO2.Value = 0;
            _signs.VitalSigns.Breath.N2O.inN2O.Value = 0;
            _signs.VitalSigns.Breath.N2O.exN2O.Value = 0;
            _signs.VitalSigns.Breath.AGT.inAGT.Value = 0;
            _signs.VitalSigns.Breath.AGT.exAGT.Value = 0;
            //其它 Other
            _signs.VitalSigns.Other.PeripheralTemperature.Value = 0;
            _signs.VitalSigns.Other.BloodTemperature.Value = 0;
            _signs.VitalSigns.Other.pH.Value = 0;
            _signs.VitalSigns.Other.ICP.Value = 0;
            _signs.VitalSigns.Other.TOF.Numerical.Value = 0;
            _signs.VitalSigns.Other.TOF.Ratio.Value = 0;
            _signs.VitalSigns.Other.PTC.Value = 0;

            //听诊 Stethoscopy
            //心音 HeartSounds
            _signs.Stethoscopy.HeartSounds.M.SoundID = 1;
            _signs.Stethoscopy.HeartSounds.M.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.T.SoundID = 1;
            _signs.Stethoscopy.HeartSounds.T.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.A.SoundID = 1;
            _signs.Stethoscopy.HeartSounds.A.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.P.SoundID = 1;
            _signs.Stethoscopy.HeartSounds.P.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.E.SoundID = 1;
            _signs.Stethoscopy.HeartSounds.E.SoundVolume = Modle.SoundVolume.V5;
            //肺音 LungSounds
            //前
            _signs.Stethoscopy.LungSounds.ARUL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.ARUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ARML.SoundID = 1;
            _signs.Stethoscopy.LungSounds.ARML.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ARLL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.ARLL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ALUL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.ALUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ALLL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.ALLL.SoundVolume = Modle.SoundVolume.V5;
            //后背
            _signs.Stethoscopy.LungSounds.PLUL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.PLUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.PLLL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.PLLL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.PRUL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.PRUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.PRLL.SoundID = 1;
            _signs.Stethoscopy.LungSounds.PRLL.SoundVolume = Modle.SoundVolume.V5;
            //腹部 AbdomenSounds
            _signs.Stethoscopy.AbdomenSounds.Vascular.SoundID = 1;
            _signs.Stethoscopy.AbdomenSounds.Vascular.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.AbdomenSounds.Bowel.SoundID = 1;
            _signs.Stethoscopy.AbdomenSounds.Bowel.SoundVolume = Modle.SoundVolume.V5;

            //血压 BloodPressure
            _signs.BloodPressure.NIBP.Systolic.Value = 120;
            _signs.BloodPressure.NIBP.Diastolic.Value = 80;
            _signs.BloodPressure.Korotkoff.SoundVolume = Modle.SoundVolume.V5;
            _signs.BloodPressure.Korotkoff.Priority.Status = Modle.ControllerStatus.No;
            _signs.BloodPressure.Korotkoff.Priority.Value = 0;

            //惊厥 Convulsions
            _signs.Convulsions.Clonic.LeftArm.Status = _signs.Convulsions.Clonic.RightArm.Status = Modle.ControllerStatus.No;
            _signs.Convulsions.Tonic.NeckAnkylosis.Status = Modle.ControllerStatus.No;

            //脉搏 Pulse
            //身体 Body 
            _signs.Pulse.BodyPulse.CarotidLeft.Status = _signs.Pulse.BodyPulse.CarotidRight.Status = Modle.ControllerStatus.Yes;
            _signs.Pulse.BodyPulse.CarotidLeft.Value = _signs.Pulse.BodyPulse.CarotidRight.Value = 2;
            _signs.Pulse.BodyPulse.FemoralLeft.Status = _signs.Pulse.BodyPulse.FemoralRight.Status = Modle.ControllerStatus.Yes;
            _signs.Pulse.BodyPulse.FemoralLeft.Value = _signs.Pulse.BodyPulse.FemoralRight.Value = 2;
            //右臂 Right Arm
            _signs.Pulse.RightArmPulse.Radial.Status = _signs.Pulse.RightArmPulse.Brachial.Status = Modle.ControllerStatus.Yes;
            _signs.Pulse.RightArmPulse.Radial.Value = _signs.Pulse.RightArmPulse.Brachial.Value = 2;
            //左臂 Left Arm
            _signs.Pulse.LeftArmPulse.Radial.Status = _signs.Pulse.LeftArmPulse.Brachial.Status = Modle.ControllerStatus.Yes;
            _signs.Pulse.LeftArmPulse.Radial.Value = _signs.Pulse.LeftArmPulse.Brachial.Value = 2;
            //右腿 Right Leg
            _signs.Pulse.RightLegPulse.Heel.Status = _signs.Pulse.RightLegPulse.DorsalisPedis.Status = _signs.Pulse.RightLegPulse.Popliteal.Status = Modle.ControllerStatus.Yes;
            _signs.Pulse.RightLegPulse.Heel.Value = _signs.Pulse.RightLegPulse.DorsalisPedis.Value = _signs.Pulse.RightLegPulse.Popliteal.Value = 2;
            //左腿 Left Leg
            _signs.Pulse.LeftLegPulse.Heel.Status = _signs.Pulse.LeftLegPulse.DorsalisPedis.Status = _signs.Pulse.LeftLegPulse.Popliteal.Status = Modle.ControllerStatus.Yes;
            _signs.Pulse.LeftLegPulse.Heel.Value = _signs.Pulse.LeftLegPulse.DorsalisPedis.Value = _signs.Pulse.LeftLegPulse.Popliteal.Value = 2;

            //分泌 Secretions
            _signs.Secretions.Sweat = Modle.Controller.Default;
            _signs.Secretions.Eyes = Modle.Controller.Default;
            _signs.Secretions.Mouth = Modle.Controller.Default;
            _signs.Secretions.Ears = Modle.Controller.Default;
            _signs.Secretions.Nose = Modle.Controller.Default;
            _signs.Secretions.Froth = Modle.Controller.Default;

            //出血 Haemorrhage
            _signs.Haemorrhage.RightUpper.Arterial.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.RightUpper.Arterial.Value = 0;
            _signs.Haemorrhage.RightUpper.Venous.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.RightUpper.Venous.Value = 0;
            _signs.Haemorrhage.LeftUpper.Arterial.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.LeftUpper.Arterial.Value = 0;
            _signs.Haemorrhage.LeftUpper.Venous.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.LeftUpper.Venous.Value = 0;
            _signs.Haemorrhage.RightLower.Arterial.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.RightLower.Arterial.Value = 0;
            _signs.Haemorrhage.RightLower.Venous.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.RightLower.Venous.Value = 0;
            _signs.Haemorrhage.LeftLower.Arterial.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.LeftLower.Arterial.Value = 0;
            _signs.Haemorrhage.LeftLower.Venous.Status = Modle.ControllerStatus.No;
            _signs.Haemorrhage.LeftLower.Venous.Value = 0;

            //尿液 Urine
            _signs.Urine.Urinate.Status = Modle.ControllerStatus.No;
            _signs.Urine.Urinate.Value = 0;

            //药物 DrugDelivery
            _signs.DrugDelivery.Drug.DrugID = 0;
            _signs.DrugDelivery.Drug.Route = Modle.Route.nothing;
            _signs.DrugDelivery.Dose.Value = 0;
            _signs.DrugDelivery.Dose.Unit = 0;

            //触诊 Palpation
            _signs.Palpation.Abdominal.RightUpper.Status = Modle.ControllerStatus.No;
            _signs.Palpation.Abdominal.LeftUpper.Status = Modle.ControllerStatus.No;
            _signs.Palpation.Abdominal.Middle.Status = Modle.ControllerStatus.No;
            _signs.Palpation.Abdominal.RightLower.Status = Modle.ControllerStatus.No;
            _signs.Palpation.Abdominal.LeftLower.Status = Modle.ControllerStatus.No;
        }

        private void MyInitialize_Treatment()
        {
            //对光
            _treatment.PupillaryLight.Left.Status = _treatment.PupillaryLight.Right.Status = Modle.OperatorStatus.No;

            //气管插管
            _treatment.TracheaCannula.InTrachea.Status = Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InEsophagus.Status = Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InRightBronchus.Status = Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InLeftBronchus.Status = Modle.OperatorStatus.No;

            //生命体征 VitalSigns
            //循环 Cyclic
            _treatment.VitalSigns.Cyclic.Rhythm.Basic = 1;
            _treatment.VitalSigns.Cyclic.Rhythm.Extrasystole = 0;
            _treatment.VitalSigns.Cyclic.HeartRate.Value = 75;
            _treatment.VitalSigns.Cyclic.SpO2.Value = 98;
            _treatment.VitalSigns.Cyclic.IBP.Systolic.Value = 120;
            _treatment.VitalSigns.Cyclic.IBP.Diastolic.Value = 80;
            _treatment.VitalSigns.Cyclic.PAP.Systolic.Value = 40;
            _treatment.VitalSigns.Cyclic.PAP.Diastolic.Value = 35;
            _treatment.VitalSigns.Cyclic.CVP.Value = 0;
            _treatment.VitalSigns.Cyclic.PAWP.Value = 0;
            _treatment.VitalSigns.Cyclic.C_O_.Value = 0;
            //呼吸 Breath
            _treatment.VitalSigns.Breath.RespType = 0;
            _treatment.VitalSigns.Breath.RespRate.Value = 0;
            _treatment.VitalSigns.Breath.InspiratoryCapacity.Value = 0;
            _treatment.VitalSigns.Breath.RespRatio.Value = 0;
            _treatment.VitalSigns.Breath.CO2.Value = 0;
            _treatment.VitalSigns.Breath.ETCO2.Value = 0;
            _treatment.VitalSigns.Breath.O2.inO2.Value = 0;
            _treatment.VitalSigns.Breath.O2.exO2.Value = 0;
            _treatment.VitalSigns.Breath.N2O.inN2O.Value = 0;
            _treatment.VitalSigns.Breath.N2O.exN2O.Value = 0;
            _treatment.VitalSigns.Breath.AGT.inAGT.Value = 0;
            _treatment.VitalSigns.Breath.AGT.exAGT.Value = 0;
            //其它 Other
            _treatment.VitalSigns.Other.PeripheralTemperature.Value = 0;
            _treatment.VitalSigns.Other.BloodTemperature.Value = 0;
            _treatment.VitalSigns.Other.pH.Value = 0;
            _treatment.VitalSigns.Other.ICP.Value = 0;
            _treatment.VitalSigns.Other.TOF.Numerical.Value = 0;
            _treatment.VitalSigns.Other.TOF.Ratio.Value = 0;
            _treatment.VitalSigns.Other.PTC.Value = 0;

            //CPR
            //CPR-Q
            _treatment.CPR_Q.Pat.Status = Modle.OperatorStatus.No;
            _treatment.CPR_Q.Shout.Status = Modle.OperatorStatus.No;
            _treatment.CPR_Q.CheckRightCarotid.Status = Modle.OperatorStatus.No;
            _treatment.CPR_Q.CheckLeftCarotid.Status = Modle.OperatorStatus.No;
            _treatment.CPR_Q.HeadTiltChinLift.Status = Modle.OperatorStatus.No;
            _treatment.CPR_Q.RemovalOralForeignBody.Status = Modle.OperatorStatus.No;
            //CPR-P
            _treatment.CPR_P.PressDepth = 0;
            _treatment.CPR_P.PressPosition = 0;
            _treatment.CPR_P.BlowVolume = 0;

            //除颤
            //除颤能量
            _treatment.Defibrillation.Energy = 0;
            //除颤电极
            _treatment.DefibrillatorElectrode.Apex.Status = _treatment.DefibrillatorElectrode.Sterno.Status = Modle.OperatorStatus.No;

            //起搏
            //起搏电流
            _treatment.Pacing.Current = 0;
            //起搏电极
            _treatment.PacerElectrode.Back.Status = _treatment.PacerElectrode.Sternum.Status = Modle.OperatorStatus.No;

            //监护仪导联线
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG12.V1.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG12.V2.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG12.V3.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG12.V4.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG12.V5.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG12.V6.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_SPO2.SPO2.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_NIBP.NIBP.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_Temperature.PeripheralTemperature.Status = Modle.OperatorStatus.No;
            _treatment.ECG.LeadLine.LeadLine_Temperature.BloodTemperature.Status = Modle.OperatorStatus.No;

            //测量血压
            _treatment.MeasureBP.CuffPressure = 0;

            //脉搏检查            
            //身体脉搏 Body 
            _treatment.CheckPulse.CheckBodyPulse.Carotid_Left.Status = _treatment.CheckPulse.CheckBodyPulse.Carotid_Right.Status = Modle.OperatorStatus.No;
            _treatment.CheckPulse.CheckBodyPulse.Femoral_Left.Status = _treatment.CheckPulse.CheckBodyPulse.Femoral_Right.Status = Modle.OperatorStatus.No;
            //右臂 Right Arm
            _treatment.CheckPulse.CheckRightArmPulse.Radial.Status = _treatment.CheckPulse.CheckRightArmPulse.Brachial.Status = Modle.OperatorStatus.No;
            //左臂 Left Arm
            _treatment.CheckPulse.CheckLeftArmPulse.Radial.Status = _treatment.CheckPulse.CheckLeftArmPulse.Brachial.Status = Modle.OperatorStatus.No;
            //右腿 Right Leg
            _treatment.CheckPulse.CheckRightLegPulse.Heel.Status = _treatment.CheckPulse.CheckRightLegPulse.DorsalisPedis.Status = _treatment.CheckPulse.CheckRightLegPulse.Popliteal.Status = Modle.OperatorStatus.No;
            //左腿 Left Leg
            _treatment.CheckPulse.CheckLeftLegPulse.Heel.Status = _treatment.CheckPulse.CheckLeftLegPulse.DorsalisPedis.Status = _treatment.CheckPulse.CheckLeftLegPulse.Popliteal.Status = Modle.OperatorStatus.No;

            //导尿 Catheterization
            _treatment.Catheterization.InsertionBladder.Status = Modle.OperatorStatus.No;

            //药物治疗  Medication
            _treatment.Medication.PO.DrugID = 0;
            _treatment.Medication.PO.Dose.Value = 0;
            _treatment.Medication.PO.Dose.Unit = 0;
            _treatment.Medication.IV.DrugID = 0;
            _treatment.Medication.IV.Dose.Value = 0;
            _treatment.Medication.IV.Dose.Unit = 0;
            _treatment.Medication.IVGtt.DrugID = 0;
            _treatment.Medication.IVGtt.Dose.Value = 0;
            _treatment.Medication.IVGtt.Dose.Unit = 0;
            _treatment.Medication.IM.DrugID = 0;
            _treatment.Medication.IM.Dose.Value = 0;
            _treatment.Medication.IM.Dose.Unit = 0;
            _treatment.Medication.ID.DrugID = 0;
            _treatment.Medication.ID.Dose.Value = 0;
            _treatment.Medication.ID.Dose.Unit = 0;
            _treatment.Medication.IH.DrugID = 0;
            _treatment.Medication.IH.Dose.Value = 0;
            _treatment.Medication.IH.Dose.Unit = 0;

            //腹部触诊 Abdominal Touch
            _treatment.AbdominalTouch.RightUpperTouch.Status = Modle.OperatorStatus.No;
            _treatment.AbdominalTouch.LeftUpperTouch.Status = Modle.OperatorStatus.No;
            _treatment.AbdominalTouch.MiddleTouch.Status = Modle.OperatorStatus.No;
            _treatment.AbdominalTouch.RightLowerTouch.Status = Modle.OperatorStatus.No;
            _treatment.AbdominalTouch.LeftLowerTouch.Status = Modle.OperatorStatus.No;
        }

        #endregion

        #region 解析
        private void SetData_Treatment(Modle.Function Function, Modle.Treatment Treatment)
        {
            switch (Function)
            {
                case Modle.Function.Eyes:

                    break;
                case Modle.Function.Cyanosis:
                    break;
                case Modle.Function.AnalogVocal:
                    break;
                case Modle.Function.Airway:
                    break;
                case Modle.Function.TracheaCannula:

                    break;
                case Modle.Function.VitalSigns:
                    SetData_VitalSigns(Treatment.VitalSigns);
                    break;
                case Modle.Function.CPR:
                    //SetData_Treatment_CPR_Q(Treatment.CPR_Q);
                    //SetData_Treatment_CPR_P(Treatment.CPR_P);
                    break;
                case Modle.Function.Defibrillation:
                    //SetData_Treatment_Defibrillation(Treatment.Defibrillation);
                    //SetData_Treatment_DefibrillatorElectrode(Treatment.DefibrillatorElectrode);
                    break;
                case Modle.Function.Pacing:
                    //SetData_Treatment_Pacing(Treatment.Pacing);
                    //SetData_Treatment_PacerElectrode(Treatment.PacerElectrode);
                    break;
                case Modle.Function.Palpation:

                    break;
                case Modle.Function.BloodPressure:

                    break;
                case Modle.Function.Pulse:

                    break;
                case Modle.Function.Catheterization:

                    break;
                case Modle.Function.Medication:

                    break;
                case Modle.Function.ECG:
                    _signs.ECG = Treatment.ECG;
                    SetData_Treatment_ECG(Treatment.ECG);
                    break;
                default:

                    //SetData_Treatment_CPR_Q(Treatment.CPR_Q);
                    //SetData_Treatment_CPR_P(Treatment.CPR_P);
                    //SetData_Treatment_Defibrillation(Treatment.Defibrillation);
                    //SetData_Treatment_DefibrillatorElectrode(Treatment.DefibrillatorElectrode);
                    //SetData_Treatment_Pacing(Treatment.Pacing);
                    //SetData_Treatment_PacerElectrode(Treatment.PacerElectrode);

                    _signs.ECG = Treatment.ECG;
                    SetData_Treatment_ECG(Treatment.ECG);
                    break;
            }



        }


        private void SetData_VitalSigns(Modle.VitalSigns VitalSigns)
        {
            _signs.VitalSigns = VitalSigns;
            ShowData_VitalSigns(_signs.VitalSigns);
        }

        private void SetData_Treatment_CPR_Q(Modle.CPR_Q CPR_Q)
        {
            //label_CPR_Q_Pat_Status.Content = GetString_OperatorStatus(CPR_Q.Pat.Status);
            //label_CPR_Q_Shout_Status.Content = GetString_OperatorStatus(CPR_Q.Shout.Status);
            //label_CPR_Q_CheckRightCarotid_Status.Content = GetString_OperatorStatus(CPR_Q.CheckRightCarotid.Status);
            //label_CPR_Q_CheckLeftCarotid_Status.Content = GetString_OperatorStatus(CPR_Q.CheckLeftCarotid.Status);
            //label_CPR_Q_HeadTiltChinLift_Status.Content = GetString_OperatorStatus(CPR_Q.HeadTiltChinLift.Status);
            //label_CPR_Q_RemovalOralForeignBody_Status.Content = GetString_OperatorStatus(CPR_Q.RemovalOralForeignBody.Status);
        }
        private void SetData_Treatment_CPR_P(Modle.CPR_P CPR_P)
        {
            //label_CPR_P_PressDepth_Value.Content = CPR_P.PressDepth.ToString();
            //label_CPR_P_PressPosition_Value.Content = CPR_P.PressPosition.ToString();
            //label_CPR_P_BlowVolume_Value.Content = CPR_P.BlowVolume.ToString();
        }
        private void SetData_Treatment_Defibrillation(Modle.Defibrillation Defibrillation)
        {
            //label_Defibrillation_Energy_Value.Content = Defibrillation.Energy.ToString();
        }

        private void SetData_Treatment_DefibrillatorElectrode(Modle.DefibrillatorElectrode DefibrillatorElectrode)
        {
            //checkBox_Defibrillation_Electrode_Sterno_Status.IsChecked = DefibrillatorElectrode.Sterno.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            //checkBox_Defibrillation_Electrode_Apex_Status.IsChecked = DefibrillatorElectrode.Apex.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
        }

        private void SetData_Treatment_Pacing(Modle.Pacing Pacing)
        {
            //label_Pacing_Current_Value.Content = Pacing.Current.ToString();
        }

        private void SetData_Treatment_PacerElectrode(Modle.PacerElectrode PacerElectrode)
        {
            //checkBox_Pacing_Electrode_Sternum_Status.IsChecked = PacerElectrode.Sternum.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            //checkBox_Pacing_Electrode_Back_Status.IsChecked = PacerElectrode.Back.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
        }

        private void SetData_Treatment_ECG(Modle.ECG ECG)
        {

            checkBox_VitalSigns_LeadLine_RA.Checked -= checkBox_VitalSigns_LeadLine_RA_Checked;
            checkBox_VitalSigns_LeadLine_RA.Checked -= checkBox_VitalSigns_LeadLine_RA_Unchecked;
            checkBox_VitalSigns_LeadLine_LA.Checked -= checkBox_VitalSigns_LeadLine_LA_Checked;
            checkBox_VitalSigns_LeadLine_LA.Checked -= checkBox_VitalSigns_LeadLine_LA_Unchecked;
            checkBox_VitalSigns_LeadLine_RL.Checked -= checkBox_VitalSigns_LeadLine_RL_Checked;
            checkBox_VitalSigns_LeadLine_RL.Checked -= checkBox_VitalSigns_LeadLine_RL_Unchecked;
            checkBox_VitalSigns_LeadLine_LL.Checked -= checkBox_VitalSigns_LeadLine_LL_Checked;
            checkBox_VitalSigns_LeadLine_LL.Checked -= checkBox_VitalSigns_LeadLine_LL_Unchecked;
            checkBox_VitalSigns_LeadLine_V.Checked -= checkBox_VitalSigns_LeadLine_V_Checked;
            checkBox_VitalSigns_LeadLine_V.Checked -= checkBox_VitalSigns_LeadLine_V_Unchecked;
            checkBox_VitalSigns_LeadLine_SPO2.Checked -= checkBox_VitalSigns_LeadLine_SPO2_Checked;
            checkBox_VitalSigns_LeadLine_SPO2.Checked -= checkBox_VitalSigns_LeadLine_SPO2_Unchecked;
            checkBox_VitalSigns_LeadLine_NIBP.Checked -= checkBox_VitalSigns_LeadLine_NIBP_Checked;
            checkBox_VitalSigns_LeadLine_NIBP.Checked -= checkBox_VitalSigns_LeadLine_NIBP_Unchecked;
            checkBox_VitalSigns_LeadLine_Temp1.Checked -= checkBox_VitalSigns_LeadLine_Temp1_Checked;
            checkBox_VitalSigns_LeadLine_Temp1.Checked -= checkBox_VitalSigns_LeadLine_Temp1_Unchecked;

            checkBox_VitalSigns_LeadLine_RA.IsChecked = ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_LA.IsChecked = ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_RL.IsChecked = ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_LL.IsChecked = ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_V.IsChecked = ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_SPO2.IsChecked = ECG.LeadLine.LeadLine_SPO2.SPO2.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_NIBP.IsChecked = ECG.LeadLine.LeadLine_NIBP.NIBP.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_VitalSigns_LeadLine_Temp1.IsChecked = ECG.LeadLine.LeadLine_Temperature.PeripheralTemperature.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;

            checkBox_VitalSigns_LeadLine_RA.Checked += checkBox_VitalSigns_LeadLine_RA_Checked;
            checkBox_VitalSigns_LeadLine_RA.Checked += checkBox_VitalSigns_LeadLine_RA_Unchecked;
            checkBox_VitalSigns_LeadLine_LA.Checked += checkBox_VitalSigns_LeadLine_LA_Checked;
            checkBox_VitalSigns_LeadLine_LA.Checked += checkBox_VitalSigns_LeadLine_LA_Unchecked;
            checkBox_VitalSigns_LeadLine_RL.Checked += checkBox_VitalSigns_LeadLine_RL_Checked;
            checkBox_VitalSigns_LeadLine_RL.Checked += checkBox_VitalSigns_LeadLine_RL_Unchecked;
            checkBox_VitalSigns_LeadLine_LL.Checked += checkBox_VitalSigns_LeadLine_LL_Checked;
            checkBox_VitalSigns_LeadLine_LL.Checked += checkBox_VitalSigns_LeadLine_LL_Unchecked;
            checkBox_VitalSigns_LeadLine_V.Checked += checkBox_VitalSigns_LeadLine_V_Checked;
            checkBox_VitalSigns_LeadLine_V.Checked += checkBox_VitalSigns_LeadLine_V_Unchecked;
            checkBox_VitalSigns_LeadLine_SPO2.Checked += checkBox_VitalSigns_LeadLine_SPO2_Checked;
            checkBox_VitalSigns_LeadLine_SPO2.Checked += checkBox_VitalSigns_LeadLine_SPO2_Unchecked;
            checkBox_VitalSigns_LeadLine_NIBP.Checked += checkBox_VitalSigns_LeadLine_NIBP_Checked;
            checkBox_VitalSigns_LeadLine_NIBP.Checked += checkBox_VitalSigns_LeadLine_NIBP_Unchecked;
            checkBox_VitalSigns_LeadLine_Temp1.Checked += checkBox_VitalSigns_LeadLine_Temp1_Checked;
            checkBox_VitalSigns_LeadLine_Temp1.Checked += checkBox_VitalSigns_LeadLine_Temp1_Unchecked;
        }

        #endregion

        #region 绑定
        private void BindData()
        {

            BindData_VitalSigns();

        }

        #region 生命体征 VitalSigns

        private void BindData_VitalSigns()
        {
            BindData_VitalSigns_Rhythm_Basic();

            BindData_VitalSigns_Rhythm_Extrasystole();

            BindData_Pressure_Unit();
        }

        private void BindData_VitalSigns_Rhythm_Basic()
        {
            //throw new NotImplementedException();
            comboBox_VitalSigns_Cyclic_Rhythm_Basic.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Rhythm_Basic.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Rhythm_Basic.Count; i++)
            {
                comboBox_VitalSigns_Cyclic_Rhythm_Basic.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Rhythm_Basic[test[i]], ""));
            }

            comboBox_VitalSigns_Cyclic_Rhythm_Basic.SelectedIndex = 0;
        }

        private void BindData_VitalSigns_Rhythm_Extrasystole()
        {
            //throw new NotImplementedException();
            comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Rhythm_Extrasystole.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Rhythm_Extrasystole.Count; i++)
            {
                comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Rhythm_Extrasystole[test[i]], ""));
            }

            comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.SelectedIndex = 0;
        }

        private void BindData_Pressure_Unit()
        {
            //throw new NotImplementedException();
            comboBox_VitalSigns_Cyclic_IBP_Unit.Items.Clear();
            comboBox_VitalSigns_Cyclic_PAP_Unit.Items.Clear();
            comboBox_VitalSigns_Cyclic_CVP_Unit.Items.Clear();
            comboBox_VitalSigns_Cyclic_PAWP_Unit.Items.Clear();
            //comboBox_VitalSigns_Cyclic_ICP_Unit.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Pressure_Unit.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Pressure_Unit.Count; i++)
            {
                comboBox_VitalSigns_Cyclic_IBP_Unit.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                comboBox_VitalSigns_Cyclic_PAP_Unit.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                comboBox_VitalSigns_Cyclic_CVP_Unit.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                comboBox_VitalSigns_Cyclic_PAWP_Unit.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                //comboBox_VitalSigns_Cyclic_ICP_Unit.Items.Add(new ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));

            }

            comboBox_VitalSigns_Cyclic_IBP_Unit.SelectedIndex = 0;
            comboBox_VitalSigns_Cyclic_PAP_Unit.SelectedIndex = 0;
            comboBox_VitalSigns_Cyclic_CVP_Unit.SelectedIndex = 0;
            comboBox_VitalSigns_Cyclic_PAWP_Unit.SelectedIndex = 0;
            //comboBox_VitalSigns_Cyclic_ICP_Unit.SelectedIndex = 0;
        }

        #endregion

        #endregion

        #region 显示

        private void ShowData(Modle.Signs Signs)
        {

            ShowData_VitalSigns(Signs.VitalSigns);

        }

        private string GetString_OperatorStatus(Modle.OperatorStatus OperatorStatus)
        {
            if (OperatorStatus == Simulator.Framework.Modle.OperatorStatus.Yes)
                return "有";
            return "无";
        }

        private void ComboBoxChoose(ComboBox combox, string value)
        {
            for (int i = 0; i < combox.Items.Count; i++)
            {
                if (((ListItem)combox.Items[i]).Value == value)
                    combox.SelectedIndex = i;
            }
        }

        #region 生命体征

        private void ShowData_VitalSigns(Modle.VitalSigns VitalSigns)
        {
            ShowData_VitalSigns_Cyclic(VitalSigns.Cyclic);
            ShowData_VitalSigns_Breath(VitalSigns.Breath);
            ShowData_VitalSigns_Other(VitalSigns.Other);
        }

        private void ShowData_VitalSigns_Cyclic(Modle.Cyclic Cyclic)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_Rhythm_Basic, Cyclic.Rhythm.Basic.ToString());
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole, Cyclic.Rhythm.Extrasystole.ToString());
            textBox_VitalSigns_Cyclic_HeartRate.Text = Cyclic.HeartRate.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_HeartRate_Unit, Cyclic.HeartRate.Unit.ToString());
            textBox_VitalSigns_Cyclic_IBP_Systolic.Text = Cyclic.IBP.Systolic.Value.ToString();
            textBox_VitalSigns_Cyclic_IBP_Diastolic.Text = Cyclic.IBP.Diastolic.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_IBP_Unit, Cyclic.IBP.Systolic.Unit.ToString());
            textBox_VitalSigns_Cyclic_SpO2.Text = Cyclic.SpO2.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_SpO2_Unit, Cyclic.SpO2.Unit.ToString());
            textBox_VitalSigns_Cyclic_PAP_Systolic.Text = Cyclic.PAP.Systolic.Value.ToString();
            textBox_VitalSigns_Cyclic_PAP_Diastolic.Text = Cyclic.PAP.Diastolic.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_PAP_Unit, Cyclic.PAP.Systolic.Unit.ToString());

            textBox_VitalSigns_Cyclic_CVP.Text = Cyclic.CVP.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_CVP_Unit, Cyclic.CVP.Unit.ToString());
            textBox_VitalSigns_Cyclic_PAWP.Text = Cyclic.PAWP.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_PAWP_Unit, Cyclic.PAWP.Unit.ToString());
            textBox_VitalSigns_Cyclic_C_O_.Text = Cyclic.C_O_.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Cyclic_C_O__Unit, Cyclic.C_O_.Unit.ToString());

        }

        private void ShowData_VitalSigns_Breath(Modle.Breath Breath)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_VitalSigns_Breath_RespType, Breath.RespType.ToString());
            textBox_VitalSigns_Breath_RespRate.Text = Breath.RespRate.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_RespRate_Unit, Breath.RespRate.Unit.ToString());
            textBox_VitalSigns_Breath_InspiratoryCapacity.Text = Breath.InspiratoryCapacity.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_InspiratoryCapacity_Unit, Breath.InspiratoryCapacity.Unit.ToString());
            textBox_VitalSigns_Breath_RespRatio.Text = Breath.RespRatio.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_RespRatio_Unit, Breath.RespRatio.Unit.ToString());
            textBox_VitalSigns_Breath_CO2.Text = Breath.CO2.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_CO2_Unit, Breath.CO2.Unit.ToString());
            textBox_VitalSigns_Breath_ETCO2.Text = Breath.ETCO2.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_ETCO2_Unit, Breath.ETCO2.Unit.ToString());
            textBox_VitalSigns_Breath_O2_inO2.Text = Breath.O2.inO2.Value.ToString();
            textBox_VitalSigns_Breath_O2_exO2.Text = Breath.O2.exO2.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_O2_Unit, Breath.O2.inO2.Unit.ToString());
            textBox_VitalSigns_Breath_N2O_inN2O.Text = Breath.N2O.inN2O.Value.ToString();
            textBox_VitalSigns_Breath_N2O_exN2O.Text = Breath.N2O.exN2O.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_N2O_Unit, Breath.N2O.inN2O.Unit.ToString());
            textBox_VitalSigns_Breath_AGT_inAGT.Text = Breath.AGT.inAGT.Value.ToString();
            textBox_VitalSigns_Breath_AGT_exAGT.Text = Breath.AGT.exAGT.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Breath_AGT_Unit, Breath.AGT.inAGT.Unit.ToString());
        }

        private void ShowData_VitalSigns_Other(Modle.Other Other)
        {
            //throw new NotImplementedException();
            textBox_VitalSigns_Other_PeripheralTemperature.Text = Other.PeripheralTemperature.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_PeripheralTemperature_Unit, Other.PeripheralTemperature.Unit.ToString());
            textBox_VitalSigns_Other_BloodTemperature.Text = Other.BloodTemperature.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_BloodTemperature_Unit, Other.BloodTemperature.Unit.ToString());
            textBox_VitalSigns_Other_pH.Text = Other.pH.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_pH_Unit, Other.pH.Unit.ToString());
            textBox_VitalSigns_Other_ICP.Text = Other.ICP.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_ICP_Unit, Other.ICP.Unit.ToString());
            textBox_VitalSigns_Other_TOF.Text = Other.TOF.Numerical.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_TOF_Unit, Other.TOF.Numerical.Unit.ToString());
            textBox_VitalSigns_Other_TOFRatio.Text = Other.TOF.Ratio.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_TOFRatio_Unit, Other.TOF.Ratio.Unit.ToString());
            textBox_VitalSigns_Other_PTC.Text = Other.PTC.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_PTC_Unit, Other.PTC.Unit.ToString());
        }

        #endregion

        #endregion

        #region 设定

        #region 生命体征 

        #region 循环 Cyclic

        private void comboBox_VitalSigns_Cyclic_Rhythm_Basic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Basic = 0;
            if (comboBox_VitalSigns_Cyclic_Rhythm_Basic.SelectedItem != null)
                Basic = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_Rhythm_Basic.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.Rhythm.Basic = Basic;

            Send_VitalSigns_Cyclic_Rhythm(_signs.VitalSigns.Cyclic.Rhythm);
        }

        private void comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Extrasystole = 0;
            if (comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.SelectedItem != null)
                Extrasystole = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.Rhythm.Extrasystole = Extrasystole;

            Send_VitalSigns_Cyclic_Rhythm(_signs.VitalSigns.Cyclic.Rhythm);
        }

        private void Send_VitalSigns_Cyclic_Rhythm(Modle.Rhythm Rhythm)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_Rhythm(Rhythm);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_HeartRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            int HeartRate = 0;
            if (textBox_VitalSigns_Cyclic_HeartRate.Text.Trim() != "")
                HeartRate = int.Parse(textBox_VitalSigns_Cyclic_HeartRate.Text.Trim());

            _signs.VitalSigns.Cyclic.HeartRate.Value = HeartRate;

            Send_VitalSigns_Cyclic_HeartRate(_signs.VitalSigns.Cyclic.HeartRate);
        }

        private void comboBox_VitalSigns_Cyclic_HeartRate_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int HeartRateUnit = 0;
            if (comboBox_VitalSigns_Cyclic_HeartRate_Unit.SelectedItem != null)
                HeartRateUnit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_HeartRate_Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.HeartRate.Unit = HeartRateUnit;

            Send_VitalSigns_Cyclic_HeartRate(_signs.VitalSigns.Cyclic.HeartRate);
        }

        private void Send_VitalSigns_Cyclic_HeartRate(Modle.DataValue HeartRate)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_HeartRate(HeartRate);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_IBP_Systolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Systolic = 0;
            if (textBox_VitalSigns_Cyclic_IBP_Systolic.Text.Trim() != "")
                Systolic = int.Parse(textBox_VitalSigns_Cyclic_IBP_Systolic.Text.Trim());

            _signs.VitalSigns.Cyclic.IBP.Systolic.Value = Systolic;

            Send_VitalSigns_Cyclic_IBP(_signs.VitalSigns.Cyclic.IBP);
        }

        private void textBox_VitalSigns_Cyclic_IBP_Diastolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Diastolic = 0;
            if (textBox_VitalSigns_Cyclic_IBP_Diastolic.Text.Trim() != "")
                Diastolic = int.Parse(textBox_VitalSigns_Cyclic_IBP_Diastolic.Text.Trim());

            _signs.VitalSigns.Cyclic.IBP.Diastolic.Value = Diastolic;

            Send_VitalSigns_Cyclic_IBP(_signs.VitalSigns.Cyclic.IBP);
        }

        private void comboBox_VitalSigns_Cyclic_IBP_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int IBPUnit = 0;
            if (comboBox_VitalSigns_Cyclic_IBP_Unit.SelectedItem != null)
                IBPUnit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_IBP_Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.IBP.Systolic.Unit = _signs.VitalSigns.Cyclic.IBP.Diastolic.Unit = IBPUnit;

            Send_VitalSigns_Cyclic_IBP(_signs.VitalSigns.Cyclic.IBP);
        }

        private void Send_VitalSigns_Cyclic_IBP(Modle.IBP IBP)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_IBP(IBP);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_SpO2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int SpO2 = 0;
            if (textBox_VitalSigns_Cyclic_SpO2.Text.Trim() != "")
                SpO2 = int.Parse(textBox_VitalSigns_Cyclic_SpO2.Text.Trim());

            _signs.VitalSigns.Cyclic.SpO2.Value = SpO2;

            Send_VitalSigns_Cyclic_SpO2(_signs.VitalSigns.Cyclic.SpO2);
        }

        private void comboBox_VitalSigns_Cyclic_SpO2_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SpO2Unit = 0;
            if (comboBox_VitalSigns_Cyclic_SpO2_Unit.SelectedItem != null)
                SpO2Unit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_SpO2_Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.SpO2.Unit = SpO2Unit;

            Send_VitalSigns_Cyclic_SpO2(_signs.VitalSigns.Cyclic.SpO2);
        }

        private void Send_VitalSigns_Cyclic_SpO2(Modle.DataValue SpO2)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_SpO2(SpO2);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_PAP_Systolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Systolic = 0;
            if (textBox_VitalSigns_Cyclic_PAP_Systolic.Text.Trim() != "")
                Systolic = int.Parse(textBox_VitalSigns_Cyclic_PAP_Systolic.Text.Trim());

            _signs.VitalSigns.Cyclic.PAP.Systolic.Value = Systolic;

            Send_VitalSigns_Cyclic_PAP(_signs.VitalSigns.Cyclic.PAP);
        }

        private void textBox_VitalSigns_Cyclic_PAP_Diastolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Diastolic = 0;
            if (textBox_VitalSigns_Cyclic_PAP_Diastolic.Text.Trim() != "")
                Diastolic = int.Parse(textBox_VitalSigns_Cyclic_PAP_Diastolic.Text.Trim());

            _signs.VitalSigns.Cyclic.PAP.Diastolic.Value = Diastolic;

            Send_VitalSigns_Cyclic_PAP(_signs.VitalSigns.Cyclic.PAP);
        }

        private void comboBox_VitalSigns_Cyclic_PAP_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int PAPUnit = 0;
            if (comboBox_VitalSigns_Cyclic_PAP_Unit.SelectedItem != null)
                PAPUnit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_PAP_Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.PAP.Systolic.Unit = _signs.VitalSigns.Cyclic.PAP.Diastolic.Unit = PAPUnit;

            Send_VitalSigns_Cyclic_PAP(_signs.VitalSigns.Cyclic.PAP);
        }

        private void Send_VitalSigns_Cyclic_PAP(Modle.PAP PAP)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_PAP(PAP);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_CVP_TextChanged(object sender, TextChangedEventArgs e)
        {
            int CVP = 0;
            if (textBox_VitalSigns_Cyclic_CVP.Text.Trim() != "")
                CVP = int.Parse(textBox_VitalSigns_Cyclic_CVP.Text.Trim());

            _signs.VitalSigns.Cyclic.CVP.Value = CVP;

            Send_VitalSigns_Cyclic_CVP(_signs.VitalSigns.Cyclic.CVP);
        }

        private void comboBox_VitalSigns_Cyclic_CVP_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int CVPUnit = 0;
            if (comboBox_VitalSigns_Cyclic_CVP_Unit.SelectedItem != null)
                CVPUnit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_CVP_Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.CVP.Unit = CVPUnit;

            Send_VitalSigns_Cyclic_CVP(_signs.VitalSigns.Cyclic.CVP);
        }

        private void Send_VitalSigns_Cyclic_CVP(Modle.DataValue CVP)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_CVP(CVP);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_PAWP_TextChanged(object sender, TextChangedEventArgs e)
        {
            int PAWP = 0;
            if (textBox_VitalSigns_Cyclic_PAWP.Text.Trim() != "")
                PAWP = int.Parse(textBox_VitalSigns_Cyclic_PAWP.Text.Trim());

            _signs.VitalSigns.Cyclic.PAWP.Value = PAWP;

            Send_VitalSigns_Cyclic_PAWP(_signs.VitalSigns.Cyclic.PAWP);
        }

        private void comboBox_VitalSigns_Cyclic_PAWP_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int PAWPUnit = 0;
            if (comboBox_VitalSigns_Cyclic_PAWP_Unit.SelectedItem != null)
                PAWPUnit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_PAWP_Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.PAWP.Unit = PAWPUnit;

            Send_VitalSigns_Cyclic_PAWP(_signs.VitalSigns.Cyclic.PAWP);
        }

        private void Send_VitalSigns_Cyclic_PAWP(Modle.DataValue PAWP)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_PAWP(PAWP);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Cyclic_C_O__TextChanged(object sender, TextChangedEventArgs e)
        {
            int C_O_ = 0;
            if (textBox_VitalSigns_Cyclic_C_O_.Text.Trim() != "")
                C_O_ = int.Parse(textBox_VitalSigns_Cyclic_C_O_.Text.Trim());

            _signs.VitalSigns.Cyclic.C_O_.Value = C_O_;

            Send_VitalSigns_Cyclic_C_O_(_signs.VitalSigns.Cyclic.C_O_);
        }

        private void comboBox_VitalSigns_Cyclic_C_O__Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int C_O_Unit = 0;
            if (comboBox_VitalSigns_Cyclic_C_O__Unit.SelectedItem != null)
                C_O_Unit = int.Parse(((ListItem)comboBox_VitalSigns_Cyclic_C_O__Unit.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.C_O_.Unit = C_O_Unit;

            Send_VitalSigns_Cyclic_C_O_(_signs.VitalSigns.Cyclic.C_O_);
        }

        private void Send_VitalSigns_Cyclic_C_O_(Modle.DataValue C_O_)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Cyclic.SetDataBytes_VitalSigns_Cyclic_C_O_(C_O_);

            SendBytes(dataBytes);
        }

        #endregion

        #region 呼吸 Breath

        private void comboBox_VitalSigns_Breath_RespType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int RespType = 0;
            if (comboBox_VitalSigns_Breath_RespType.SelectedItem != null)
                RespType = int.Parse(((ListItem)comboBox_VitalSigns_Breath_RespType.SelectedItem).Value);

            _signs.VitalSigns.Breath.RespType = RespType;

            Send_VitalSigns_Breath_RespType(_signs.VitalSigns.Breath.RespType);
        }

        private void Send_VitalSigns_Breath_RespType(int RespType)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_RespType(RespType);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_RespRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            int RespRate = 0;
            if (textBox_VitalSigns_Breath_RespRate.Text.Trim() != "")
                RespRate = int.Parse(textBox_VitalSigns_Breath_RespRate.Text.Trim());

            _signs.VitalSigns.Breath.RespRate.Value = RespRate;

            Send_VitalSigns_Breath_RespRate(_signs.VitalSigns.Breath.RespRate);
        }

        private void comboBox_VitalSigns_Breath_RespRate_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int RespRateUnit = 0;
            if (comboBox_VitalSigns_Breath_RespRate_Unit.SelectedItem != null)
                RespRateUnit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_RespRate_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.RespRate.Value = RespRateUnit;

            Send_VitalSigns_Breath_RespRate(_signs.VitalSigns.Breath.RespRate);
        }

        private void Send_VitalSigns_Breath_RespRate(Modle.DataValue RespRate)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_RespRate(RespRate);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_InspiratoryCapacity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int InspiratoryCapacity = 0;
            if (textBox_VitalSigns_Breath_InspiratoryCapacity.Text.Trim() != "")
                InspiratoryCapacity = int.Parse(textBox_VitalSigns_Breath_InspiratoryCapacity.Text.Trim());

            _signs.VitalSigns.Breath.InspiratoryCapacity.Value = InspiratoryCapacity;

            Send_VitalSigns_Breath_InspiratoryCapacity(_signs.VitalSigns.Breath.InspiratoryCapacity);
        }

        private void comboBox_VitalSigns_Breath_InspiratoryCapacity_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int InspiratoryCapacityUnit = 0;
            if (comboBox_VitalSigns_Breath_InspiratoryCapacity_Unit.SelectedItem != null)
                InspiratoryCapacityUnit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_InspiratoryCapacity_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.InspiratoryCapacity.Value = InspiratoryCapacityUnit;

            Send_VitalSigns_Breath_InspiratoryCapacity(_signs.VitalSigns.Breath.InspiratoryCapacity);
        }

        private void Send_VitalSigns_Breath_InspiratoryCapacity(Modle.DataValue InspiratoryCapacity)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_InspiratoryCapacity(InspiratoryCapacity);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_RespRatio_TextChanged(object sender, TextChangedEventArgs e)
        {
            int RespRatio = 0;
            if (textBox_VitalSigns_Breath_RespRatio.Text.Trim() != "")
                RespRatio = int.Parse(textBox_VitalSigns_Breath_RespRatio.Text.Trim());

            _signs.VitalSigns.Breath.RespRatio.Value = RespRatio;

            Send_VitalSigns_Breath_RespRatio(_signs.VitalSigns.Breath.RespRatio);
        }

        private void comboBox_VitalSigns_Breath_RespRatio_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int RespRatioUnit = 0;
            if (comboBox_VitalSigns_Breath_RespRatio_Unit.SelectedItem != null)
                RespRatioUnit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_RespRatio_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.RespRatio.Value = RespRatioUnit;

            Send_VitalSigns_Breath_RespRatio(_signs.VitalSigns.Breath.RespRatio);
        }

        private void Send_VitalSigns_Breath_RespRatio(Modle.DataValue RespRatio)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_RespRatio(RespRatio);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_CO2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int CO2 = 0;
            if (textBox_VitalSigns_Breath_CO2.Text.Trim() != "")
                CO2 = int.Parse(textBox_VitalSigns_Breath_CO2.Text.Trim());

            _signs.VitalSigns.Breath.CO2.Value = CO2;

            Send_VitalSigns_Breath_CO2(_signs.VitalSigns.Breath.CO2);
        }

        private void comboBox_VitalSigns_Breath_CO2_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int CO2Unit = 0;
            if (comboBox_VitalSigns_Breath_CO2_Unit.SelectedItem != null)
                CO2Unit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_CO2_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.CO2.Value = CO2Unit;

            Send_VitalSigns_Breath_CO2(_signs.VitalSigns.Breath.CO2);
        }

        private void Send_VitalSigns_Breath_CO2(Modle.DataValue CO2)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_CO2(CO2);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_ETCO2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ETCO2 = 0;
            if (textBox_VitalSigns_Breath_ETCO2.Text.Trim() != "")
                ETCO2 = int.Parse(textBox_VitalSigns_Breath_ETCO2.Text.Trim());

            _signs.VitalSigns.Breath.ETCO2.Value = ETCO2;

            Send_VitalSigns_Breath_ETCO2(_signs.VitalSigns.Breath.ETCO2);
        }

        private void comboBox_VitalSigns_Breath_ETCO2_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ETCO2Unit = 0;
            if (comboBox_VitalSigns_Breath_ETCO2_Unit.SelectedItem != null)
                ETCO2Unit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_ETCO2_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.ETCO2.Value = ETCO2Unit;

            Send_VitalSigns_Breath_ETCO2(_signs.VitalSigns.Breath.ETCO2);
        }

        private void Send_VitalSigns_Breath_ETCO2(Modle.DataValue ETCO2)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_ETCO2(ETCO2);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_O2_inO2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inO2 = 0;
            if (textBox_VitalSigns_Breath_O2_inO2.Text.Trim() != "")
                inO2 = int.Parse(textBox_VitalSigns_Breath_O2_inO2.Text.Trim());

            _signs.VitalSigns.Breath.O2.inO2.Value = inO2;

            Send_VitalSigns_Breath_O2(_signs.VitalSigns.Breath.O2);
        }

        private void textBox_VitalSigns_Breath_O2_exO2_TextChanged(object sender, TextChangedEventArgs e)
        {
            int exO2 = 0;
            if (textBox_VitalSigns_Breath_O2_exO2.Text.Trim() != "")
                exO2 = int.Parse(textBox_VitalSigns_Breath_O2_exO2.Text.Trim());

            _signs.VitalSigns.Breath.O2.exO2.Value = exO2;

            Send_VitalSigns_Breath_O2(_signs.VitalSigns.Breath.O2);
        }

        private void comboBox_VitalSigns_Breath_O2_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int O2Unit = 0;
            if (comboBox_VitalSigns_Breath_O2_Unit.SelectedItem != null)
                O2Unit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_O2_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.O2.inO2.Unit = _signs.VitalSigns.Breath.O2.exO2.Unit = O2Unit;

            Send_VitalSigns_Breath_O2(_signs.VitalSigns.Breath.O2);
        }

        private void Send_VitalSigns_Breath_O2(Modle.O2 O2)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_O2(O2);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_N2O_inN2O_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inN2O = 0;
            if (textBox_VitalSigns_Breath_N2O_inN2O.Text.Trim() != "")
                inN2O = int.Parse(textBox_VitalSigns_Breath_N2O_inN2O.Text.Trim());

            _signs.VitalSigns.Breath.N2O.inN2O.Value = inN2O;

            Send_VitalSigns_Breath_N2O(_signs.VitalSigns.Breath.N2O);
        }

        private void textBox_VitalSigns_Breath_N2O_exN2O_TextChanged(object sender, TextChangedEventArgs e)
        {
            int exN2O = 0;
            if (textBox_VitalSigns_Breath_N2O_exN2O.Text.Trim() != "")
                exN2O = int.Parse(textBox_VitalSigns_Breath_N2O_exN2O.Text.Trim());

            _signs.VitalSigns.Breath.N2O.exN2O.Value = exN2O;

            Send_VitalSigns_Breath_N2O(_signs.VitalSigns.Breath.N2O);
        }

        private void comboBox_VitalSigns_Breath_N2O_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int N2OUnit = 0;
            if (comboBox_VitalSigns_Breath_N2O_Unit.SelectedItem != null)
                N2OUnit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_N2O_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.N2O.inN2O.Unit = _signs.VitalSigns.Breath.N2O.exN2O.Unit = N2OUnit;

            Send_VitalSigns_Breath_N2O(_signs.VitalSigns.Breath.N2O);
        }

        private void Send_VitalSigns_Breath_N2O(Modle.N2O N2O)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_N2O(N2O);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Breath_AGT_inAGT_TextChanged(object sender, TextChangedEventArgs e)
        {
            int inAGT = 0;
            if (textBox_VitalSigns_Breath_AGT_inAGT.Text.Trim() != "")
                inAGT = int.Parse(textBox_VitalSigns_Breath_AGT_inAGT.Text.Trim());

            _signs.VitalSigns.Breath.AGT.inAGT.Value = inAGT;

            Send_VitalSigns_Breath_AGT(_signs.VitalSigns.Breath.AGT);
        }

        private void textBox_VitalSigns_Breath_AGT_exAGT_TextChanged(object sender, TextChangedEventArgs e)
        {
            int exAGT = 0;
            if (textBox_VitalSigns_Breath_AGT_exAGT.Text.Trim() != "")
                exAGT = int.Parse(textBox_VitalSigns_Breath_AGT_exAGT.Text.Trim());

            _signs.VitalSigns.Breath.AGT.exAGT.Value = exAGT;

            Send_VitalSigns_Breath_AGT(_signs.VitalSigns.Breath.AGT);
        }

        private void comboBox_VitalSigns_Breath_AGT_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int AGTUnit = 0;
            if (comboBox_VitalSigns_Breath_AGT_Unit.SelectedItem != null)
                AGTUnit = int.Parse(((ListItem)comboBox_VitalSigns_Breath_AGT_Unit.SelectedItem).Value);

            _signs.VitalSigns.Breath.AGT.inAGT.Unit = _signs.VitalSigns.Breath.AGT.exAGT.Unit = AGTUnit;

            Send_VitalSigns_Breath_AGT(_signs.VitalSigns.Breath.AGT);
        }

        private void Send_VitalSigns_Breath_AGT(Modle.AGT AGT)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Breath.SetDataBytes_VitalSigns_Breath_AGT(AGT);

            SendBytes(dataBytes);
        }

        #endregion

        #region 其它 Other

        private void textBox_VitalSigns_Other_PeripheralTemperature_TextChanged(object sender, TextChangedEventArgs e)
        {
            int PeripheralTemperature = 0;
            if (textBox_VitalSigns_Other_PeripheralTemperature.Text.Trim() != "")
                PeripheralTemperature = int.Parse(textBox_VitalSigns_Other_PeripheralTemperature.Text.Trim());

            _signs.VitalSigns.Other.PeripheralTemperature.Value = PeripheralTemperature;

            Send_VitalSigns_Other_PeripheralTemperature(_signs.VitalSigns.Other.PeripheralTemperature);
        }

        private void comboBox_VitalSigns_Other_PeripheralTemperature_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int PeripheralTemperatureUnit = 0;
            if (comboBox_VitalSigns_Other_PeripheralTemperature_Unit.SelectedItem != null)
                PeripheralTemperatureUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_PeripheralTemperature_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.PeripheralTemperature.Unit = PeripheralTemperatureUnit;

            Send_VitalSigns_Other_PeripheralTemperature(_signs.VitalSigns.Other.PeripheralTemperature);
        }

        private void Send_VitalSigns_Other_PeripheralTemperature(Modle.DataValue PeripheralTemperature)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Other.SetDataBytes_VitalSigns_Other_PeripheralTemperature(PeripheralTemperature);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Other_BloodTemperature_TextChanged(object sender, TextChangedEventArgs e)
        {
            int BloodTemperature = 0;
            if (textBox_VitalSigns_Other_BloodTemperature.Text.Trim() != "")
                BloodTemperature = int.Parse(textBox_VitalSigns_Other_BloodTemperature.Text.Trim());

            _signs.VitalSigns.Other.BloodTemperature.Value = BloodTemperature;

            Send_VitalSigns_Other_BloodTemperature(_signs.VitalSigns.Other.BloodTemperature);
        }

        private void comboBox_VitalSigns_Other_BloodTemperature_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int BloodTemperatureUnit = 0;
            if (comboBox_VitalSigns_Other_BloodTemperature_Unit.SelectedItem != null)
                BloodTemperatureUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_BloodTemperature_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.BloodTemperature.Unit = BloodTemperatureUnit;

            Send_VitalSigns_Other_BloodTemperature(_signs.VitalSigns.Other.BloodTemperature);
        }

        private void Send_VitalSigns_Other_BloodTemperature(Modle.DataValue BloodTemperature)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Other.SetDataBytes_VitalSigns_Other_BloodTemperature(BloodTemperature);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Other_pH_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pH = 0;
            if (textBox_VitalSigns_Other_pH.Text.Trim() != "")
                pH = int.Parse(textBox_VitalSigns_Other_pH.Text.Trim());

            _signs.VitalSigns.Other.pH.Value = pH;

            Send_VitalSigns_Other_pH(_signs.VitalSigns.Other.pH);
        }

        private void comboBox_VitalSigns_Other_pH_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pHUnit = 0;
            if (comboBox_VitalSigns_Other_pH_Unit.SelectedItem != null)
                pHUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_pH_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.pH.Unit = pHUnit;

            Send_VitalSigns_Other_pH(_signs.VitalSigns.Other.pH);
        }

        private void Send_VitalSigns_Other_pH(Modle.DataValue pH)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Other.SetDataBytes_VitalSigns_Other_pH(pH);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Other_ICP_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ICP = 0;
            if (textBox_VitalSigns_Other_ICP.Text.Trim() != "")
                ICP = int.Parse(textBox_VitalSigns_Other_ICP.Text.Trim());

            _signs.VitalSigns.Other.ICP.Value = ICP;

            Send_VitalSigns_Other_ICP(_signs.VitalSigns.Other.ICP);
        }

        private void comboBox_VitalSigns_Other_ICP_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ICPUnit = 0;
            if (comboBox_VitalSigns_Other_ICP_Unit.SelectedItem != null)
                ICPUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_ICP_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.ICP.Unit = ICPUnit;

            Send_VitalSigns_Other_ICP(_signs.VitalSigns.Other.ICP);
        }

        private void Send_VitalSigns_Other_ICP(Modle.DataValue ICP)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Other.SetDataBytes_VitalSigns_Other_ICP(ICP);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Other_TOF_TextChanged(object sender, TextChangedEventArgs e)
        {
            int TOF = 0;
            if (textBox_VitalSigns_Other_TOF.Text.Trim() != "")
                TOF = int.Parse(textBox_VitalSigns_Other_TOF.Text.Trim());

            _signs.VitalSigns.Other.TOF.Numerical.Value = TOF;

            Send_VitalSigns_Other_TOF(_signs.VitalSigns.Other.TOF);
        }

        private void comboBox_VitalSigns_Other_TOF_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int TOFUnit = 0;
            if (comboBox_VitalSigns_Other_TOF_Unit.SelectedItem != null)
                TOFUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_TOF_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.TOF.Numerical.Unit = TOFUnit;

            Send_VitalSigns_Other_TOF(_signs.VitalSigns.Other.TOF);
        }

        private void textBox_VitalSigns_Other_TOFRatio_TextChanged(object sender, TextChangedEventArgs e)
        {
            int TOFRatio = 0;
            if (textBox_VitalSigns_Other_TOFRatio.Text.Trim() != "")
                TOFRatio = int.Parse(textBox_VitalSigns_Other_TOFRatio.Text.Trim());

            _signs.VitalSigns.Other.TOF.Ratio.Value = TOFRatio;

            Send_VitalSigns_Other_TOF(_signs.VitalSigns.Other.TOF);
        }

        private void comboBox_VitalSigns_Other_TOFRatio_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int TOFRatioUnit = 0;
            if (comboBox_VitalSigns_Other_TOFRatio_Unit.SelectedItem != null)
                TOFRatioUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_TOFRatio_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.TOF.Ratio.Unit = TOFRatioUnit;

            Send_VitalSigns_Other_TOF(_signs.VitalSigns.Other.TOF);
        }

        private void Send_VitalSigns_Other_TOF(Modle.TOF TOF)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Other.SetDataBytes_VitalSigns_Other_TOF(TOF);

            SendBytes(dataBytes);
        }

        private void textBox_VitalSigns_Other_PTC_TextChanged(object sender, TextChangedEventArgs e)
        {
            int PTC = 0;
            if (textBox_VitalSigns_Other_PTC.Text.Trim() != "")
                PTC = int.Parse(textBox_VitalSigns_Other_PTC.Text.Trim());

            _signs.VitalSigns.Other.PTC.Value = PTC;

            Send_VitalSigns_Other_PTC(_signs.VitalSigns.Other.PTC);
        }

        private void comboBox_VitalSigns_Other_PTC_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int PTCUnit = 0;
            if (comboBox_VitalSigns_Other_PTC_Unit.SelectedItem != null)
                PTCUnit = int.Parse(((ListItem)comboBox_VitalSigns_Other_PTC_Unit.SelectedItem).Value);

            _signs.VitalSigns.Other.PTC.Unit = PTCUnit;

            Send_VitalSigns_Other_PTC(_signs.VitalSigns.Other.PTC);
        }

        private void Send_VitalSigns_Other_PTC(Modle.DataValue PTC)
        {
            byte[] dataBytes = Resolve.VitalSigns.VitalSigns_Other.SetDataBytes_VitalSigns_Other_PTC(PTC);

            SendBytes(dataBytes);
        }

        #endregion              

        #endregion

        #region 心电监护 导联线

        #region 导联线 Lead Line

        private void checkBox_VitalSigns_LeadLine_RA_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_RA_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_RA_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_RA_CheckChanged();

        }

        private void checkBox_VitalSigns_LeadLine_RA_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_RA.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
        }

        private void checkBox_VitalSigns_LeadLine_LA_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_LA_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_LA_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_LA_CheckChanged();

        }

        private void checkBox_VitalSigns_LeadLine_LA_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_LA.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LA.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
        }

        private void checkBox_VitalSigns_LeadLine_RL_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_RL_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_RL_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_RL_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_RL_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_RL.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RL.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
        }

        private void checkBox_VitalSigns_LeadLine_LL_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_LL_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_LL_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_LL_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_LL_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_LL.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.LL.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
        }

        private void checkBox_VitalSigns_LeadLine_V_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_V_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_V_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_V_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_V_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_V.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.V0.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_signs.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
        }

        private void Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(Modle.LeadLine_ECG5 LeadLine_ECG5)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.SetDataBytes_LeadLineECG_LeadLineECG5(LeadLine_ECG5);

            SendBytes(dataBytes);
        }

        private void checkBox_VitalSigns_LeadLine_SPO2_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_SPO2_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_SPO2_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_SPO2_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_SPO2_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_SPO2.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_SPO2.SPO2.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_SPO2.SPO2.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineSPO2(_signs.ECG.LeadLine.LeadLine_SPO2);
        }

        private void Send_ECG_LeadLine_LeadLineSPO2(Modle.LeadLine_SPO2 LeadLine_SPO2)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.SetDataBytes_LeadLine_SPO2(LeadLine_SPO2);

            SendBytes(dataBytes);
        }

        private void checkBox_VitalSigns_LeadLine_NIBP_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_NIBP_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_NIBP_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_NIBP_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_NIBP_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_NIBP.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_NIBP.NIBP.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_NIBP.NIBP.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineNIBP(_signs.ECG.LeadLine.LeadLine_NIBP);
        }

        private void Send_ECG_LeadLine_LeadLineNIBP(Modle.LeadLine_NIBP LeadLine_NIBP)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.SetDataBytes_LeadLine_NIBP(LeadLine_NIBP);

            SendBytes(dataBytes);
        }

        private void checkBox_VitalSigns_LeadLine_Temp1_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_Temp1_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_Temp1_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_VitalSigns_LeadLine_Temp1_CheckChanged();
        }

        private void checkBox_VitalSigns_LeadLine_Temp1_CheckChanged()
        {
            Modle.OperatorStatus OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_Temp1.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_Temperature.PeripheralTemperature.Status = OperatorStatus;
            _signs.ECG.LeadLine.LeadLine_Temperature.PeripheralTemperature.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineTemperature(_signs.ECG.LeadLine.LeadLine_Temperature);
        }
        private void Send_ECG_LeadLine_LeadLineTemperature(Modle.LeadLine_Temperature LeadLine_Temperature)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.SetDataBytes_LeadLine_Temperature(LeadLine_Temperature);

            SendBytes(dataBytes);
        }


        #endregion

        #endregion

        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int rhythm = comboBox_VitalSigns_Cyclic_Rhythm_Basic.SelectedIndex;

            Button button = sender as Button;
            if (button == button_ecg)
            {
                new ECGmonitor(rhythm).ShowDialog();
            }
            else
            {
                new OtherWave().ShowDialog();
            }
          
        
        }

    }
}
