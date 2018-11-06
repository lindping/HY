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
using Library = YH.Library;
namespace YH.Simulator_Engine_Test
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

            NetInitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            BindData();

            Signs_Initialize();

            Treatment_Initialize();

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

            m_NetClient.Connect("192.168.1.166", 6500);
            //m_NetClient.Connect("10.10.100.254", 8899);
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
            //throw new NotImplementedException();
            //string info = string.Format("recv data:{0} from:{1}.", e.ClientSession.Datagram, e.ClientSession);                        

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                Modle.Function Function = Resolve.Resolve.GetData_Treatment(e.ClientSession.RecvDataBuffer, ref _treatment);
                SetData_Treatment(Function, _treatment);
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

        private void Signs_Initialize()
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

        private void Treatment_Initialize()
        {
            //对光
            _treatment.PupillaryLight.Left.Status = _treatment.PupillaryLight.Right.Status = Modle.OperatorStatus.No;

            //气管插管
            _treatment.TracheaCannula.InTrachea.Status = Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InEsophagus.Status = Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InRightBronchus.Status = Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InLeftBronchus.Status = Modle.OperatorStatus.No;

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
                    SetData_Treatment_PupillaryLight(Treatment.PupillaryLight);
                    break;
                case Modle.Function.Cyanosis:
                    break;
                case Modle.Function.AnalogVocal:
                    break;
                case Modle.Function.Airway:
                    break;
                case Modle.Function.TracheaCannula:
                    SetData_Treatment_TracheaCannula(Treatment.TracheaCannula);
                    break;
                case Modle.Function.CPR:
                    SetData_Treatment_CPR_Q(Treatment.CPR_Q);
                    SetData_Treatment_CPR_P(Treatment.CPR_P);
                    break;
                case Modle.Function.Defibrillation:
                    SetData_Treatment_Defibrillation(Treatment.Defibrillation);
                    SetData_Treatment_DefibrillatorElectrode(Treatment.DefibrillatorElectrode);
                    break;
                case Modle.Function.Pacing:
                    SetData_Treatment_Pacing(Treatment.Pacing);
                    SetData_Treatment_PacerElectrode(Treatment.PacerElectrode);
                    break;
                case Modle.Function.Palpation:
                    SetData_Treatment_Palpation(Treatment.AbdominalTouch);
                    break;
                case Modle.Function.BloodPressure:
                    SetData_Treatment_MeasureBP(Treatment.MeasureBP);
                    break;
                case Modle.Function.Pulse:
                    SetData_Treatment_PulseCheck(Treatment.CheckPulse);
                    break;
                case Modle.Function.Catheterization:
                    SetData_Treatment_Catheterization(Treatment.Catheterization);
                    break;
                case Modle.Function.Medication:
                    SetData_Treatment_Medication(Treatment.Medication);
                    break;
                case Modle.Function.ECG:
                    _signs.ECG = Treatment.ECG;
                    SetData_Treatment_ECG(Treatment.ECG);
                    break;
                default:
                    SetData_Treatment_PupillaryLight(Treatment.PupillaryLight);
                    SetData_Treatment_TracheaCannula(Treatment.TracheaCannula);
                    SetData_Treatment_CPR_Q(Treatment.CPR_Q);
                    SetData_Treatment_CPR_P(Treatment.CPR_P);
                    SetData_Treatment_Defibrillation(Treatment.Defibrillation);
                    SetData_Treatment_DefibrillatorElectrode(Treatment.DefibrillatorElectrode);
                    SetData_Treatment_Pacing(Treatment.Pacing);
                    SetData_Treatment_PacerElectrode(Treatment.PacerElectrode);
                    SetData_Treatment_Palpation(Treatment.AbdominalTouch);
                    SetData_Treatment_MeasureBP(Treatment.MeasureBP);
                    SetData_Treatment_PulseCheck(Treatment.CheckPulse);
                    SetData_Treatment_Catheterization(Treatment.Catheterization);
                    SetData_Treatment_Medication(Treatment.Medication);

                    _signs.ECG = Treatment.ECG;
                    SetData_Treatment_ECG(Treatment.ECG);
                    break;
            }
        }

        private void SetData_Treatment_PupillaryLight(Modle.PupillaryLight PupillaryLight)
        {
            label_Eyes_LightCheck_Right.Content = GetString_OperatorStatus(PupillaryLight.Right.Status);
            label_Eyes_LightCheck_Left.Content = GetString_OperatorStatus(PupillaryLight.Left.Status);
        }

        private void SetData_Treatment_TracheaCannula(Modle.TracheaCannula TracheaCannula)
        {
            label_TracheaCannula_InTrachea_Status.Content = GetString_OperatorStatus(TracheaCannula.InTrachea.Status);
            label_TracheaCannula_InEsophagus_Status.Content = GetString_OperatorStatus(TracheaCannula.InEsophagus.Status);
            label_TracheaCannula_InRightBronchus_Status.Content = GetString_OperatorStatus(TracheaCannula.InRightBronchus.Status);
            label_TracheaCannula_InLeftBronchus_Status.Content = GetString_OperatorStatus(TracheaCannula.InLeftBronchus.Status);
        }

        private void SetData_Treatment_CPR_Q(Modle.CPR_Q CPR_Q)
        {
            label_CPR_Q_Pat_Status.Content = GetString_OperatorStatus(CPR_Q.Pat.Status);
            label_CPR_Q_Shout_Status.Content = GetString_OperatorStatus(CPR_Q.Shout.Status);
            label_CPR_Q_CheckRightCarotid_Status.Content = GetString_OperatorStatus(CPR_Q.CheckRightCarotid.Status);
            label_CPR_Q_CheckLeftCarotid_Status.Content = GetString_OperatorStatus(CPR_Q.CheckLeftCarotid.Status);
            label_CPR_Q_HeadTiltChinLift_Status.Content = GetString_OperatorStatus(CPR_Q.HeadTiltChinLift.Status);
            label_CPR_Q_RemovalOralForeignBody_Status.Content = GetString_OperatorStatus(CPR_Q.RemovalOralForeignBody.Status);
        }
        private void SetData_Treatment_CPR_P(Modle.CPR_P CPR_P)
        {
            label_CPR_P_PressDepth_Value.Content = CPR_P.PressDepth.ToString();
            label_CPR_P_PressPosition_Value.Content = CPR_P.PressPosition.ToString();
            label_CPR_P_BlowVolume_Value.Content = CPR_P.BlowVolume.ToString();
        }
        private void SetData_Treatment_Defibrillation(Modle.Defibrillation Defibrillation)
        {
            label_Defibrillation_Energy_Value.Content = Defibrillation.Energy.ToString();
        }

        private void SetData_Treatment_DefibrillatorElectrode(Modle.DefibrillatorElectrode DefibrillatorElectrode)
        {
            checkBox_Defibrillation_Electrode_Sterno_Status.IsChecked = DefibrillatorElectrode.Sterno.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Defibrillation_Electrode_Apex_Status.IsChecked = DefibrillatorElectrode.Apex.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
        }

        private void SetData_Treatment_Pacing(Modle.Pacing Pacing)
        {
            label_Pacing_Current_Value.Content = Pacing.Current.ToString();
        }

        private void SetData_Treatment_PacerElectrode(Modle.PacerElectrode PacerElectrode)
        {
            checkBox_Pacing_Electrode_Sternum_Status.IsChecked = PacerElectrode.Sternum.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pacing_Electrode_Back_Status.IsChecked = PacerElectrode.Back.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
        }

        private void SetData_Treatment_Palpation(Modle.AbdominalTouch AbdominalTouch)
        {
            label_Palpation_AbdominalTouch_RightUpper_Status.Content = GetString_OperatorStatus(AbdominalTouch.RightUpperTouch.Status);
            label_Palpation_AbdominalTouch_LeftUpper_Status.Content = GetString_OperatorStatus(AbdominalTouch.LeftUpperTouch.Status);
            label_Palpation_AbdominalTouch_Middle_Status.Content = GetString_OperatorStatus(AbdominalTouch.MiddleTouch.Status);
            label_Palpation_AbdominalTouch_RightLower_Status.Content = GetString_OperatorStatus(AbdominalTouch.RightLowerTouch.Status);
            label_Palpation_AbdominalTouch_LeftLower_Status.Content = GetString_OperatorStatus(AbdominalTouch.LeftLowerTouch.Status);
        }

        private void SetData_Treatment_MeasureBP(Modle.MeasureBP MeasureBP)
        {
            label_BloodPressure_CuffPressure_Value.Content = MeasureBP.CuffPressure.ToString();
        }

        private void SetData_Treatment_PulseCheck(Modle.CheckPulse CheckPulse)
        {
            checkBox_Pulse_Body_Carotid_Right.IsChecked = CheckPulse.CheckBodyPulse.Carotid_Right.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_Body_Carotid_Left.IsChecked = CheckPulse.CheckBodyPulse.Carotid_Left.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_Body_Femoral_Right.IsChecked = CheckPulse.CheckBodyPulse.Femoral_Right.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_Body_Femoral_Left.IsChecked = CheckPulse.CheckBodyPulse.Femoral_Left.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;

            checkBox_Pulse_RightArm_Brachial.IsChecked = CheckPulse.CheckRightArmPulse.Brachial.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_RightArm_Radial.IsChecked = CheckPulse.CheckRightArmPulse.Radial.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;

            checkBox_Pulse_LeftArm_Brachial.IsChecked = CheckPulse.CheckLeftArmPulse.Brachial.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_LeftArm_Radial.IsChecked = CheckPulse.CheckLeftArmPulse.Radial.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;

            checkBox_Pulse_RightLeg_Popliteal.IsChecked = CheckPulse.CheckRightLegPulse.Popliteal.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_RightLeg_DorsalisPedis.IsChecked = CheckPulse.CheckRightLegPulse.DorsalisPedis.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_RightLeg_Heel.IsChecked = CheckPulse.CheckRightLegPulse.Heel.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;

            checkBox_Pulse_LeftLeg_Popliteal.IsChecked = CheckPulse.CheckLeftLegPulse.Popliteal.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_LeftLeg_DorsalisPedis.IsChecked = CheckPulse.CheckLeftLegPulse.DorsalisPedis.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;
            checkBox_Pulse_LeftLeg_Heel.IsChecked = CheckPulse.CheckLeftLegPulse.Heel.Status == Simulator.Framework.Modle.OperatorStatus.Yes ? true : false;

        }

        private void SetData_Treatment_Catheterization(Modle.Catheterization Catheterization)
        {
            label_Catheterization_InsertionBladder_Status.Content = GetString_OperatorStatus(Catheterization.InsertionBladder.Status);
        }

        private void SetData_Treatment_Medication(Modle.Medication Medication)
        {
            label_Medication_Apply_PO_Drug.Content = DataDictionary.MyDictionary.DrugName[(int)Medication.PO.DrugID][1];
            label_Medication_Apply_PO_Dose.Content = Medication.PO.Dose.Value.ToString();
            label_Medication_Apply_PO_DoseUnit.Content = DataDictionary.MyDictionary.DrugDoseUnit[(int)Medication.PO.Dose.Unit][1];

            label_Medication_Apply_IV_Drug.Content = DataDictionary.MyDictionary.DrugName[(int)Medication.IV.DrugID][1];
            label_Medication_Apply_IV_Dose.Content = Medication.IV.Dose.Value.ToString();
            label_Medication_Apply_IV_DoseUnit.Content = DataDictionary.MyDictionary.DrugDoseUnit[(int)Medication.IV.Dose.Unit][1];

            label_Medication_Apply_IVGtt_Drug.Content = DataDictionary.MyDictionary.DrugName[(int)Medication.IVGtt.DrugID][1];
            label_Medication_Apply_IVGtt_Dose.Content = Medication.IVGtt.Dose.Value.ToString();
            label_Medication_Apply_IVGtt_DoseUnit.Content = DataDictionary.MyDictionary.DrugDoseUnit[(int)Medication.IVGtt.Dose.Unit][1];

            label_Medication_Apply_IM_Drug.Content = DataDictionary.MyDictionary.DrugName[(int)Medication.IM.DrugID][1];
            label_Medication_Apply_IM_Dose.Content = Medication.IM.Dose.Value.ToString();
            label_Medication_Apply_IM_DoseUnit.Content = DataDictionary.MyDictionary.DrugDoseUnit[(int)Medication.IM.Dose.Unit][1];


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

        #region 数据绑定

        private void BindData()
        {
            BindData_Eyes();

            BindData_Cyanosis();

            BindData_AnalogVocal();

            BindData_Airway();

            BindData_VitalSigns();

            BindData_Stethoscopy();

            BindData_BloodPressure();

            BindData_Convulsions();

            BindData_Pulse();

            BindData_Haemorrhage();

            BindData_Catheterization();

            BindData_Medication();
        }

        #region 眼睛 Eyes

        private void BindData_Eyes()
        {
            BindData_Eyes_Eyelid();

            BindData_Eyes_Blinking();

            BindData_Eyes_Pupill();

            BindData_Eyes_Light();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindData_Eyes_Eyelid()
        {
            comboBox_Eyes_Eyelid_Right.Items.Clear();
            comboBox_Eyes_Eyelid_Left.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Eyes_EyelidStatus.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Eyes_EyelidStatus.Count; i++)
            {
                comboBox_Eyes_Eyelid_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_EyelidStatus[test[i]], ""));
                comboBox_Eyes_Eyelid_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_EyelidStatus[test[i]], ""));
            }

            comboBox_Eyes_Eyelid_Right.SelectedIndex = 0;
            comboBox_Eyes_Eyelid_Left.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindData_Eyes_Blinking()
        {
            comboBox_Eyes_Blinking_Right.Items.Clear();
            comboBox_Eyes_Blinking_Left.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Eyes_BlinkingSpeed.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Eyes_BlinkingSpeed.Count; i++)
            {
                comboBox_Eyes_Blinking_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_BlinkingSpeed[test[i]], ""));
                comboBox_Eyes_Blinking_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_BlinkingSpeed[test[i]], ""));
            }

            comboBox_Eyes_Blinking_Right.SelectedIndex = 0;
            comboBox_Eyes_Blinking_Left.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindData_Eyes_Pupill()
        {
            comboBox_Eyes_Pupill_Right.Items.Clear();
            comboBox_Eyes_Pupill_Left.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Eyes_PupillSize.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Eyes_BlinkingSpeed.Count; i++)
            {
                comboBox_Eyes_Pupill_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_PupillSize[test[i]], ""));
                comboBox_Eyes_Pupill_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_PupillSize[test[i]], ""));
            }

            comboBox_Eyes_Pupill_Right.SelectedIndex = 0;
            comboBox_Eyes_Pupill_Left.SelectedIndex = 0;
        }

        private void BindData_Eyes_Light()
        {
            comboBox_Eyes_Light_Right.Items.Clear();
            comboBox_Eyes_Light_Left.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Eyes_LightSensitivity.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Eyes_BlinkingSpeed.Count; i++)
            {
                comboBox_Eyes_Light_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_LightSensitivity[test[i]], ""));
                comboBox_Eyes_Light_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Eyes_LightSensitivity[test[i]], ""));
            }

            comboBox_Eyes_Light_Right.SelectedIndex = 0;
            comboBox_Eyes_Light_Left.SelectedIndex = 0;
        }

        

      
        #endregion

        #region 紫绀 Cyanosis

        private void BindData_Cyanosis()
        {
            BindData_Cyanosis_Degree();
        }
        private void BindData_Cyanosis_Degree()
        {

            comboBox_Cyanosis_Lips.Items.Clear();
            comboBox_Cyanosis_Fingernail.Items.Clear();
            comboBox_Cyanosis_FootNail.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Cyanosis_Degree.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Cyanosis_Degree.Count; i++)
            {
                comboBox_Cyanosis_Lips.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Cyanosis_Degree[test[i]], ""));
                comboBox_Cyanosis_Fingernail.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Cyanosis_Degree[test[i]], ""));
                comboBox_Cyanosis_FootNail.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Cyanosis_Degree[test[i]], ""));

            }

            comboBox_Cyanosis_Lips.SelectedIndex = 0;
            comboBox_Cyanosis_Fingernail.SelectedIndex = 0;
            comboBox_Cyanosis_FootNail.SelectedIndex = 0;

        }


        #endregion

        #region 声音 Analog Voice

        private void BindData_AnalogVocal()
        {
            BindData_AnalogVocal_Vocal_Sound();

            BindData_AnalogVocal_Voice_Sound();

            BindData_AnalogVocal_PlaybackMode();

            BindData_AnalogVocal_SoundVolume();
        }

        private void BindData_AnalogVocal_Vocal_Sound()
        {
            comboBox_AnalogVocal_Vocal_SoundID.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.AnalogVocal_Vocal.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.AnalogVocal_Vocal.Count; i++)
            {
                comboBox_AnalogVocal_Vocal_SoundID.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.AnalogVocal_Vocal[test[i]], ""));
            }

            comboBox_AnalogVocal_Vocal_SoundID.SelectedIndex = 0;
        }

        private void BindData_AnalogVocal_Voice_Sound()
        {
            comboBox_AnalogVocal_Voice_SoundID.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.AnalogVocal_Voice.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.AnalogVocal_Voice.Count; i++)
            {
                comboBox_AnalogVocal_Voice_SoundID.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.AnalogVocal_Voice[test[i]], ""));
            }

            comboBox_AnalogVocal_Voice_SoundID.SelectedIndex = 0;
        }

        private void BindData_AnalogVocal_PlaybackMode()
        {
            comboBox_AnalogVocal_Vocal_PlaybackMode.Items.Clear();
            comboBox_AnalogVocal_Voice_PlaybackMode.Items.Clear();

            foreach (int myCode in Enum.GetValues(typeof(Modle.PlaybackMode)))
            {
                string strName = Enum.GetName(typeof(Modle.PlaybackMode), myCode);//获取名称
                string strVaule = myCode.ToString();//获取值
                comboBox_AnalogVocal_Vocal_PlaybackMode.Items.Add(new Library.ListItem(strVaule, strName, ""));
                comboBox_AnalogVocal_Voice_PlaybackMode.Items.Add(new Library.ListItem(strVaule, strName, ""));
            }

            comboBox_AnalogVocal_Vocal_PlaybackMode.SelectedIndex = 0;
            comboBox_AnalogVocal_Voice_PlaybackMode.SelectedIndex = 0;
        }

        private void BindData_AnalogVocal_SoundVolume()
        {
            //throw new NotImplementedException();
            comboBox_AnalogVocal_Vocal_SoundVolume.Items.Clear();
            comboBox_AnalogVocal_Voice_SoundVolume.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.SoundVolumeList.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.SoundVolumeList.Count; i++)
            {
                comboBox_AnalogVocal_Vocal_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_AnalogVocal_Voice_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));

            }

            comboBox_AnalogVocal_Vocal_SoundVolume.SelectedIndex = 0;
            comboBox_AnalogVocal_Voice_SoundVolume.SelectedIndex = 0;
        }

        #endregion

        #region 气道 Airway

        private void BindData_Airway()
        {
            BindData_TongueEdema();
        }

        /// <summary>
        /// 舌水肿
        /// </summary>
        private void BindData_TongueEdema()
        {
            comboBox_Airway_TongueEdema.Items.Clear();
            comboBox_Airway_TongueFallback.Items.Clear();
            comboBox_Airway_FBAO.Items.Clear();
            comboBox_Airway_PharyngealObstruction.Items.Clear();
            comboBox_Airway_Laryngospasm.Items.Clear();
            comboBox_Airway_Trismus.Items.Clear();
            comboBox_Airway_NeckAnkylosis.Items.Clear();
            comboBox_Airway_Resistance_Right.Items.Clear();
            comboBox_Airway_Resistance_Left.Items.Clear();
            comboBox_Airway_Compliance_Right.Items.Clear();
            comboBox_Airway_Compliance_Left.Items.Clear();
            comboBox_Airway_Aerothorax_Right.Items.Clear();
            comboBox_Airway_Aerothorax_Left.Items.Clear();
            comboBox_Airway_AutonomousRespiration_Right.Items.Clear();
            comboBox_Airway_AutonomousRespiration_Left.Items.Clear();
            comboBox_Airway_StomachDistention.Items.Clear();
            comboBox_Airway_ExhaleCO2.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Tongue_Edema.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Tongue_Edema.Count; i++)
            {
                comboBox_Airway_TongueEdema.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_TongueFallback.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_FBAO.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_PharyngealObstruction.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Laryngospasm.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Trismus.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_NeckAnkylosis.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Resistance_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Resistance_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Compliance_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Compliance_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Aerothorax_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_Aerothorax_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_AutonomousRespiration_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_AutonomousRespiration_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_StomachDistention.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));
                comboBox_Airway_ExhaleCO2.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Tongue_Edema[test[i]][1], ""));

            }

            comboBox_Airway_TongueEdema.SelectedIndex = 0;
            comboBox_Airway_TongueFallback.SelectedIndex = 0;
            comboBox_Airway_FBAO.SelectedIndex = 0;
            comboBox_Airway_PharyngealObstruction.SelectedIndex = 0;
            comboBox_Airway_Laryngospasm.SelectedIndex = 0;
            comboBox_Airway_Trismus.SelectedIndex = 0;
            comboBox_Airway_NeckAnkylosis.SelectedIndex = 0;
            comboBox_Airway_Resistance_Right.SelectedIndex = 0;
            comboBox_Airway_Resistance_Left.SelectedIndex = 0;
            comboBox_Airway_Compliance_Right.SelectedIndex = 0;
            comboBox_Airway_Compliance_Left.SelectedIndex = 0;
            comboBox_Airway_Aerothorax_Right.SelectedIndex = 0;
            comboBox_Airway_Aerothorax_Left.SelectedIndex = 0;
            comboBox_Airway_AutonomousRespiration_Right.SelectedIndex = 0;
            comboBox_Airway_AutonomousRespiration_Left.SelectedIndex = 0;
            comboBox_Airway_StomachDistention.SelectedIndex = 0;
            comboBox_Airway_ExhaleCO2.SelectedIndex = 0;
        }

        #endregion

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
                comboBox_VitalSigns_Cyclic_Rhythm_Basic.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Rhythm_Basic[test[i]], ""));
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
                comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Rhythm_Extrasystole[test[i]], ""));
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
                comboBox_VitalSigns_Cyclic_IBP_Unit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                comboBox_VitalSigns_Cyclic_PAP_Unit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                comboBox_VitalSigns_Cyclic_CVP_Unit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                comboBox_VitalSigns_Cyclic_PAWP_Unit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));
                //comboBox_VitalSigns_Cyclic_ICP_Unit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));

            }

            comboBox_VitalSigns_Cyclic_IBP_Unit.SelectedIndex = 0;
            comboBox_VitalSigns_Cyclic_PAP_Unit.SelectedIndex = 0;
            comboBox_VitalSigns_Cyclic_CVP_Unit.SelectedIndex = 0;
            comboBox_VitalSigns_Cyclic_PAWP_Unit.SelectedIndex = 0;
            //comboBox_VitalSigns_Cyclic_ICP_Unit.SelectedIndex = 0;
        }

        #endregion

        #region 听诊 Stethoscopy

        private void BindData_Stethoscopy()
        {
            BindData_Stethoscopy_Sounds();

            BindData_Stethoscopy_SoundVolume();

        }

        private void BindData_Stethoscopy_Sounds()
        {
            //throw new NotImplementedException();
            BindData_Stethoscopy_Sounds_HeartSounds();
            BindData_Stethoscopy_Sounds_LungSounds();
            BindData_Stethoscopy_Sounds_BowelSounds();
            BindData_Stethoscopy_Sounds_VascularSounds();
        }

        private void BindData_Stethoscopy_Sounds_HeartSounds()
        {
            //throw new NotImplementedException();

            comboBox_Stethoscopy_HeartSounds_M.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_T.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_A.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_P.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_E.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Stethoscopy_HeartSounds.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Stethoscopy_HeartSounds.Count; i++)
            {
                comboBox_Stethoscopy_HeartSounds_M.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][1], ""));
                comboBox_Stethoscopy_HeartSounds_T.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][1], ""));
                comboBox_Stethoscopy_HeartSounds_A.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][1], ""));
                comboBox_Stethoscopy_HeartSounds_P.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][1], ""));
                comboBox_Stethoscopy_HeartSounds_E.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_HeartSounds[test[i]][1], ""));

            }

            comboBox_Stethoscopy_HeartSounds_M.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_T.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_A.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_P.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_E.SelectedIndex = 0;
        }

        private void BindData_Stethoscopy_Sounds_LungSounds()
        {
            //throw new NotImplementedException();

            comboBox_Stethoscopy_LungSounds_ARUL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ARML.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ARLL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ALUL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ALLL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PLUL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PLLL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PRUL.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PRLL.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Stethoscopy_LungSounds.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Stethoscopy_LungSounds.Count; i++)
            {
                comboBox_Stethoscopy_LungSounds_ARUL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_ARML.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_ARLL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_ALUL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_ALLL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_PLUL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_PLLL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_PRUL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));
                comboBox_Stethoscopy_LungSounds_PRLL.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_LungSounds[test[i]][1], ""));

            }

            comboBox_Stethoscopy_LungSounds_ARUL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ARML.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ARLL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ALUL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ALLL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PLUL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PLLL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PRUL.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PRLL.SelectedIndex = 0;
        }

        private void BindData_Stethoscopy_Sounds_BowelSounds()
        {
            //throw new NotImplementedException();

            comboBox_Stethoscopy_AbdomenSounds_Bowel.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Stethoscopy_BowelSounds.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Stethoscopy_BowelSounds.Count; i++)
            {
                comboBox_Stethoscopy_AbdomenSounds_Bowel.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_BowelSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_BowelSounds[test[i]][1], ""));

            }

            comboBox_Stethoscopy_AbdomenSounds_Bowel.SelectedIndex = 0;
        }

        private void BindData_Stethoscopy_Sounds_VascularSounds()
        {
            //throw new NotImplementedException();

            comboBox_Stethoscopy_AbdomenSounds_Vascular.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Stethoscopy_VascularSounds.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Stethoscopy_VascularSounds.Count; i++)
            {
                comboBox_Stethoscopy_AbdomenSounds_Vascular.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Stethoscopy_VascularSounds[test[i]][0] + "." + DataDictionary.MyDictionary.Stethoscopy_VascularSounds[test[i]][1], ""));

            }

            comboBox_Stethoscopy_AbdomenSounds_Vascular.SelectedIndex = 0;
        }

        private void BindData_Stethoscopy_SoundVolume()
        {
            //throw new NotImplementedException();

            comboBox_Stethoscopy_HeartSounds_M_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_T_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_A_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_P_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_HeartSounds_E_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ARUL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ARML_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ARLL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ALUL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_ALLL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PLUL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PLLL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PRUL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_LungSounds_PRLL_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_AbdomenSounds_Bowel_SoundVolume.Items.Clear();
            comboBox_Stethoscopy_AbdomenSounds_Vascular_SoundVolume.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.SoundVolumeList.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.SoundVolumeList.Count; i++)
            {
                comboBox_Stethoscopy_HeartSounds_M_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_HeartSounds_T_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_HeartSounds_A_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_HeartSounds_P_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_HeartSounds_E_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_ARUL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_ARML_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_ARLL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_ALUL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_ALLL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_PLUL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_PLLL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_PRUL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_LungSounds_PRLL_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_AbdomenSounds_Bowel_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));
                comboBox_Stethoscopy_AbdomenSounds_Vascular_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));

            }

            comboBox_Stethoscopy_HeartSounds_M_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_T_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_A_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_P_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_HeartSounds_E_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ARUL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ARML_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ARLL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ALUL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_ALLL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PLUL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PLLL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PRUL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_LungSounds_PRLL_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_AbdomenSounds_Bowel_SoundVolume.SelectedIndex = 0;
            comboBox_Stethoscopy_AbdomenSounds_Vascular_SoundVolume.SelectedIndex = 0;
        }

        #endregion

        #region 血压 BloodPressure

        private void BindData_BloodPressure()
        {

            BindData_BloodPressure_NIBP_Unit();

            BindData_BloodPressure_Korotkoff_SoundVolume();

        }


        private void BindData_BloodPressure_NIBP_Unit()
        {
            //throw new NotImplementedException();
            comboBox_BloodPressure_NIBP_Unit.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Pressure_Unit.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Pressure_Unit.Count; i++)
            {
                comboBox_BloodPressure_NIBP_Unit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pressure_Unit[test[i]], ""));

            }

            comboBox_BloodPressure_NIBP_Unit.SelectedIndex = 0;
        }

        private void BindData_BloodPressure_Korotkoff_SoundVolume()
        {
            //throw new NotImplementedException();
            comboBox_BloodPressure_Korotkoff_SoundVolume.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.SoundVolumeList.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.SoundVolumeList.Count; i++)
            {
                comboBox_BloodPressure_Korotkoff_SoundVolume.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.SoundVolumeList[test[i]], ""));

            }

            comboBox_BloodPressure_Korotkoff_SoundVolume.SelectedIndex = 0;
        }

        #endregion

        #region 惊厥 Convulsions

        private void BindData_Convulsions()
        {

            BindData_Convulsions_Clonic();

            BindData_Convulsions_Tonic();

        }

        private void BindData_Convulsions_Clonic()
        {
            //throw new NotImplementedException();
            comboBox_Convulsions_Clonic_RightArm.Items.Clear();
            comboBox_Convulsions_Clonic_LeftArm.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Convulsions_Clonic.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Convulsions_Clonic.Count; i++)
            {
                comboBox_Convulsions_Clonic_RightArm.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Convulsions_Clonic[test[i]][1], ""));
                comboBox_Convulsions_Clonic_LeftArm.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Convulsions_Clonic[test[i]][1], ""));

            }

            comboBox_Convulsions_Clonic_RightArm.SelectedIndex = 0;
            comboBox_Convulsions_Clonic_LeftArm.SelectedIndex = 0;
        }

        private void BindData_Convulsions_Tonic()
        {
            //throw new NotImplementedException();

            comboBox_Convulsions_Tonic_NeckAnkylosis.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Convulsions_Tonic.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Convulsions_Tonic.Count; i++)
            {
                comboBox_Convulsions_Tonic_NeckAnkylosis.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Convulsions_Tonic[test[i]][1], ""));

            }

            comboBox_Convulsions_Tonic_NeckAnkylosis.SelectedIndex = 0;

        }

        #endregion

        #region 脉搏 Pulse

        private void BindData_Pulse()
        {
            comboBox_Pulse_Body_Carotid_Right.Items.Clear();
            comboBox_Pulse_Body_Carotid_Left.Items.Clear();
            comboBox_Pulse_Body_Femoral_Right.Items.Clear();
            comboBox_Pulse_Body_Femoral_Left.Items.Clear();
            comboBox_Pulse_RightArm_Brachial.Items.Clear();
            comboBox_Pulse_RightArm_Radial.Items.Clear();
            comboBox_Pulse_LeftArm_Brachial.Items.Clear();
            comboBox_Pulse_LeftArm_Radial.Items.Clear();
            comboBox_Pulse_RightLeg_Popliteal.Items.Clear();
            comboBox_Pulse_RightLeg_DorsalisPedis.Items.Clear();
            comboBox_Pulse_RightLeg_Heel.Items.Clear();
            comboBox_Pulse_LeftLeg_Popliteal.Items.Clear();
            comboBox_Pulse_LeftLeg_DorsalisPedis.Items.Clear();
            comboBox_Pulse_LeftLeg_Heel.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Pulse_Strength.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Pulse_Strength.Count; i++)
            {
                comboBox_Pulse_Body_Carotid_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_Body_Carotid_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_Body_Femoral_Right.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_Body_Femoral_Left.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_RightArm_Brachial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_RightArm_Radial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_LeftArm_Brachial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_LeftArm_Radial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_RightLeg_Popliteal.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_RightLeg_DorsalisPedis.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_RightLeg_Heel.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_LeftLeg_Popliteal.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_LeftLeg_DorsalisPedis.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));
                comboBox_Pulse_LeftLeg_Heel.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pulse_Strength[test[i]], ""));

            }

            comboBox_Pulse_Body_Carotid_Right.SelectedIndex = 0;
            comboBox_Pulse_Body_Carotid_Left.SelectedIndex = 0;
            comboBox_Pulse_Body_Femoral_Right.SelectedIndex = 0;
            comboBox_Pulse_Body_Femoral_Left.SelectedIndex = 0;
            comboBox_Pulse_RightArm_Brachial.SelectedIndex = 0;
            comboBox_Pulse_RightArm_Radial.SelectedIndex = 0;
            comboBox_Pulse_LeftArm_Brachial.SelectedIndex = 0;
            comboBox_Pulse_LeftArm_Radial.SelectedIndex = 0;
            comboBox_Pulse_RightLeg_Popliteal.SelectedIndex = 0;
            comboBox_Pulse_RightLeg_DorsalisPedis.SelectedIndex = 0;
            comboBox_Pulse_RightLeg_Heel.SelectedIndex = 0;
            comboBox_Pulse_LeftLeg_Popliteal.SelectedIndex = 0;
            comboBox_Pulse_LeftLeg_DorsalisPedis.SelectedIndex = 0;
            comboBox_Pulse_LeftLeg_Heel.SelectedIndex = 0;
        }

        #endregion

        #region 出血  Haemorrhage

        private void BindData_Haemorrhage()
        {
            BindData_Haemorrhage_Volume();
        }
        private void BindData_Haemorrhage_Volume()
        {

            comboBox_Haemorrhage_RightUpper_Arterial.Items.Clear();
            comboBox_Haemorrhage_RightUpper_Venous.Items.Clear();
            comboBox_Haemorrhage_LeftUpper_Arterial.Items.Clear();
            comboBox_Haemorrhage_LeftUpper_Venous.Items.Clear();
            comboBox_Haemorrhage_RightLower_Arterial.Items.Clear();
            comboBox_Haemorrhage_RightLower_Venous.Items.Clear();
            comboBox_Haemorrhage_LeftLower_Arterial.Items.Clear();
            comboBox_Haemorrhage_LeftLower_Venous.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.Haemorrhage_Volume.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Haemorrhage_Volume.Count; i++)
            {
                comboBox_Haemorrhage_RightUpper_Arterial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_RightUpper_Venous.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_LeftUpper_Arterial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_LeftUpper_Venous.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_RightLower_Arterial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_RightLower_Venous.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_LeftLower_Arterial.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));
                comboBox_Haemorrhage_LeftLower_Venous.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Haemorrhage_Volume[test[i]][1], ""));

            }

            comboBox_Haemorrhage_RightUpper_Arterial.SelectedIndex = 0;
            comboBox_Haemorrhage_RightUpper_Venous.SelectedIndex = 0;
            comboBox_Haemorrhage_LeftUpper_Arterial.SelectedIndex = 0;
            comboBox_Haemorrhage_LeftUpper_Venous.SelectedIndex = 0;
            comboBox_Haemorrhage_RightLower_Arterial.SelectedIndex = 0;
            comboBox_Haemorrhage_RightLower_Venous.SelectedIndex = 0;
            comboBox_Haemorrhage_LeftLower_Arterial.SelectedIndex = 0;
            comboBox_Haemorrhage_LeftLower_Venous.SelectedIndex = 0;

        }


        #endregion

        #region 导尿  Catheterization

        private void BindData_Catheterization()
        {
            BindData_Catheterization_Urine();
        }
        private void BindData_Catheterization_Urine()
        {
            BindData_Catheterization_Urine_UPD();
        }

        private void BindData_Catheterization_Urine_UPD()
        {

            comboBox_Catheterization_Urine_UPD.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Urine_UPD.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Urine_UPD.Count; i++)
            {
                comboBox_Catheterization_Urine_UPD.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Urine_UPD[test[i]], ""));

            }

            comboBox_Catheterization_Urine_UPD.SelectedIndex = 0;

        }


        #endregion

        #region 药物治疗 Medication

        private void BindData_Medication()
        {
            BindData_Medication_Drug();
        }
        private void BindData_Medication_Drug()
        {
            BindData_Medication_Drug_DrugCategory();

            //string DrugCategory = "";
            //BindData_Medication_Drug_DrugName(DrugCategory);

            BindData_Medication_Drug_Route();

            //string DrugName, DrugRoute;
            //DrugName = DrugRoute = "";
            //BindData_Medication_Drug_DoseUnit(DrugName, DrugRoute);
        }



        private void BindData_Medication_Drug_DrugCategory()
        {
            //throw new NotImplementedException();
            comboBox_Medication_DrugDelivery_Drug_DrugCategory.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.DrugCategory.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.DrugCategory.Count; i++)
            {
                comboBox_Medication_DrugDelivery_Drug_DrugCategory.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugCategory[test[i]][0] + "." + DataDictionary.MyDictionary.DrugCategory[test[i]][1], ""));

            }

            comboBox_Medication_DrugDelivery_Drug_DrugCategory.SelectedIndex = 0;
        }

        private void BindData_Medication_Drug_DrugName(string DrugCategoryID)
        {
            //throw new NotImplementedException();
            comboBox_Medication_DrugDelivery_Drug_DrugName.Items.Clear();

            IEnumerable<KeyValuePair<int, string[]>> coll = DataDictionary.MyDictionary.DrugName.Where(k => k.Value[2] == DrugCategoryID);

            if (coll.Count() > 0)
            {
                foreach (KeyValuePair<int, string[]> kvp in coll)
                {
                    comboBox_Medication_DrugDelivery_Drug_DrugName.Items.Add(new Library.ListItem(kvp.Key.ToString(), kvp.Value[0] + "." + kvp.Value[1], ""));
                }
            }

            //List<int> test = new List<int>(DataDictionary.MyDictionary.DrugName.Keys);

            //for (int i = 0; i < DataDictionary.MyDictionary.DrugName.Count; i++)
            //{
            //    comboBox_Medication_DrugDelivery_Drug_DrugName.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugName[test[i]][1], ""));

            //}

            comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedIndex = 0;
        }

        private void BindData_Medication_Drug_Route()
        {
            //throw new NotImplementedException();
            comboBox_Medication_DrugDelivery_Drug_Route.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.DrugRoute.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.DrugRoute.Count; i++)
            {
                comboBox_Medication_DrugDelivery_Drug_Route.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugRoute[test[i]][1], ""));

            }

            comboBox_Medication_DrugDelivery_Drug_Route.SelectedIndex = 0;
        }

        private void BindData_Medication_Drug_DoseUnit(string DrugName, string DrugRoute)
        {
            //throw new NotImplementedException();
            comboBox_Medication_DrugDelivery_Drug_DoseUnit.Items.Clear();

            //IEnumerable<KeyValuePair<int, string[]>> coll = DataDictionary.MyDictionary.DrugName.Where(k => k.Value[0] == DrugCategory);

            List<int> test = new List<int>(DataDictionary.MyDictionary.DrugDoseUnit.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.DrugDoseUnit.Count; i++)
            {
                comboBox_Medication_DrugDelivery_Drug_DoseUnit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugDoseUnit[test[i]][1], ""));

            }

            comboBox_Medication_DrugDelivery_Drug_DoseUnit.SelectedIndex = 0;
        }


        #endregion

        #endregion

        #region 数据显示

        private void ShowData(Modle.Signs Signs)
        {
            ShowData_Eyes(Signs.Eyes);

            ShowData_Cyanosis(Signs.Cyanosis);

            ShowData_AnalogVocal(Signs.AnalogVocal);

            ShowData_Airway(Signs.Airway);

            ShowData_VitalSigns(Signs.VitalSigns);

            ShowData_Palpation(Signs.Palpation);

            ShowData_Stethoscopy(Signs.Stethoscopy);

            ShowData_BloodPressure(Signs.BloodPressure);

            ShowData_Convulsions(Signs.Convulsions);

            ShowData_Pulse(Signs.Pulse);

            ShowData_Secretions(Signs.Secretions);

            ShowData_Haemorrhage(Signs.Haemorrhage);

            ShowData_Catheterization(Signs.Urine);

            ShowData_DrugDelivery(Signs.DrugDelivery);

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
                if (((Library.ListItem)combox.Items[i]).Value == value)
                    combox.SelectedIndex = i;
            }
        }

        private void ShowData_Eyes(Modle.Eyes Eyes)
        {
            //throw new NotImplementedException();
            ShowData_Eyes_Eyelid(Eyes.Eyelid);
            ShowData_Eyes_Blinking(Eyes.Blinking);
            ShowData_Eyes_Pupill(Eyes.Pupill);
            ShowData_Eyes_Light(Eyes.Light);
        }
                
        private void ShowData_Eyes_Eyelid(Modle.Eyelid Eyelid)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_Eyes_Eyelid_Right, ((int)Eyelid.Right).ToString());
            ComboBoxChoose(comboBox_Eyes_Eyelid_Left, ((int)Eyelid.Left).ToString());
        }

        private void ShowData_Eyes_Blinking(Modle.Blinking Blinking)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_Eyes_Blinking_Right, ((int)Blinking.Right).ToString());
            ComboBoxChoose(comboBox_Eyes_Blinking_Left, ((int)Blinking.Left).ToString());
        }

        private void ShowData_Eyes_Pupill(Modle.Pupill Pupill)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_Eyes_Pupill_Right, ((int)Pupill.Right).ToString());
            ComboBoxChoose(comboBox_Eyes_Pupill_Left, ((int)Pupill.Left).ToString());
        }

        private void ShowData_Eyes_Light(Modle.Light Light)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_Eyes_Light_Right, ((int)Light.Right).ToString());
            ComboBoxChoose(comboBox_Eyes_Light_Left, ((int)Light.Left).ToString());
        }

        private void ShowData_Cyanosis(Modle.Cyanosis Cyanosis)
        {
            //throw new NotImplementedException();
            checkBox_Cyanosis_Lips.IsChecked = Cyanosis.Lips.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Cyanosis_Lips, Cyanosis.Lips.Value.ToString());
            checkBox_Cyanosis_Fingernail.IsChecked = Cyanosis.Fingernail.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Cyanosis_Fingernail, Cyanosis.Fingernail.Value.ToString());
            checkBox_Cyanosis_FootNail.IsChecked = Cyanosis.FootNail.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Cyanosis_FootNail, Cyanosis.FootNail.Value.ToString());
        }

        private void ShowData_AnalogVocal(Modle.AnalogVocal AnalogVocal)
        {
            //throw new NotImplementedException();
            ComboBoxChoose(comboBox_AnalogVocal_Vocal_SoundID, AnalogVocal.Vocal.SoundID.ToString());
            ComboBoxChoose(comboBox_AnalogVocal_Vocal_PlaybackMode, ((int)AnalogVocal.Vocal.PlaybackMode).ToString());
            ComboBoxChoose(comboBox_AnalogVocal_Vocal_SoundVolume, ((int)AnalogVocal.Vocal.SoundVolume).ToString());

            ComboBoxChoose(comboBox_AnalogVocal_Voice_SoundID, AnalogVocal.Voice.SoundID.ToString());
            ComboBoxChoose(comboBox_AnalogVocal_Voice_PlaybackMode, ((int)AnalogVocal.Voice.PlaybackMode).ToString());
            ComboBoxChoose(comboBox_AnalogVocal_Voice_SoundVolume, ((int)AnalogVocal.Voice.SoundVolume).ToString());
        }

        private void ShowData_Airway(Modle.Airway Airway)
        {
            //throw new NotImplementedException();
            checkBox_Airway_TongueEdema.IsChecked = Airway.TongueEdema.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_TongueEdema, Airway.TongueEdema.Value.ToString());
            checkBox_Airway_TongueFallback.IsChecked = Airway.TongueFallback.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_TongueFallback, Airway.TongueFallback.Value.ToString());
            checkBox_Airway_FBAO.IsChecked = Airway.FBAO.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_FBAO, Airway.FBAO.Value.ToString());
            checkBox_Airway_PharyngealObstruction.IsChecked = Airway.PharyngealObstruction.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_PharyngealObstruction, Airway.PharyngealObstruction.Value.ToString());
            checkBox_Airway_Laryngospasm.IsChecked = Airway.Laryngospasm.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Laryngospasm, Airway.Laryngospasm.Value.ToString());
            checkBox_Airway_Trismus.IsChecked = Airway.Trismus.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Trismus, Airway.Trismus.Value.ToString());
            checkBox_Airway_NeckAnkylosis.IsChecked = Airway.NeckAnkylosis.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_NeckAnkylosis, Airway.NeckAnkylosis.Value.ToString());
            checkBox_Airway_Resistance_Right.IsChecked = Airway.Resistance.Right.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Resistance_Right, Airway.Resistance.Right.Value.ToString());
            checkBox_Airway_Resistance_Left.IsChecked = Airway.Resistance.Left.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Resistance_Left, Airway.Resistance.Left.Value.ToString());
            checkBox_Airway_Compliance_Right.IsChecked = Airway.Compliance.Right.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Compliance_Right, Airway.Compliance.Right.Value.ToString());
            checkBox_Airway_Compliance_Left.IsChecked = Airway.Compliance.Left.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Compliance_Left, Airway.Compliance.Left.Value.ToString());
            checkBox_Airway_Aerothorax_Right.IsChecked = Airway.Aerothorax.Right.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Aerothorax_Right, Airway.Aerothorax.Right.Value.ToString());
            checkBox_Airway_Aerothorax_Left.IsChecked = Airway.Aerothorax.Left.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_Aerothorax_Left, Airway.Aerothorax.Left.Value.ToString());
            checkBox_Airway_AutonomousRespiration_Right.IsChecked = Airway.AutonomousRespiration.Right.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_AutonomousRespiration_Right, Airway.AutonomousRespiration.Right.Value.ToString());
            checkBox_Airway_AutonomousRespiration_Left.IsChecked = Airway.AutonomousRespiration.Left.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_AutonomousRespiration_Left, Airway.AutonomousRespiration.Left.Value.ToString());
            checkBox_Airway_StomachDistention.IsChecked = Airway.StomachDistention.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_StomachDistention, Airway.StomachDistention.Value.ToString());
            checkBox_Airway_ExhaleCO2.IsChecked = Airway.ExhaleCO2.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Airway_ExhaleCO2, Airway.ExhaleCO2.Value.ToString());
        }

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

        private void ShowData_Palpation(Modle.Palpation Palpation)
        {
            checkBox_Palpation_AbdominalTouch_RightUpper.IsChecked = Palpation.Abdominal.RightUpper.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Palpation_AbdominalTouch_LeftUpper.IsChecked = Palpation.Abdominal.LeftUpper.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Palpation_AbdominalTouch_Middle.IsChecked = Palpation.Abdominal.Middle.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Palpation_AbdominalTouch_RightLower.IsChecked = Palpation.Abdominal.RightLower.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Palpation_AbdominalTouch_LeftLower.IsChecked = Palpation.Abdominal.LeftLower.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
        }

        private void ShowData_Stethoscopy(Modle.Stethoscopy Stethoscopy)
        {
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_M, Stethoscopy.HeartSounds.M.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_M_SoundVolume, ((int)Stethoscopy.HeartSounds.M.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_T, Stethoscopy.HeartSounds.T.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_T_SoundVolume, ((int)Stethoscopy.HeartSounds.T.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_A, Stethoscopy.HeartSounds.A.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_A_SoundVolume, ((int)Stethoscopy.HeartSounds.A.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_P, Stethoscopy.HeartSounds.P.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_P_SoundVolume, ((int)Stethoscopy.HeartSounds.P.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_E, Stethoscopy.HeartSounds.E.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_HeartSounds_E_SoundVolume, ((int)Stethoscopy.HeartSounds.E.SoundVolume).ToString());

            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ARUL, Stethoscopy.LungSounds.ARUL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ARUL_SoundVolume, ((int)Stethoscopy.LungSounds.ARUL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ARML, Stethoscopy.LungSounds.ARML.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ARML_SoundVolume, ((int)Stethoscopy.LungSounds.ARML.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ARLL, Stethoscopy.LungSounds.ARLL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ARLL_SoundVolume, ((int)Stethoscopy.LungSounds.ARLL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ALUL, Stethoscopy.LungSounds.ALUL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ALUL_SoundVolume, ((int)Stethoscopy.LungSounds.ALUL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ALLL, Stethoscopy.LungSounds.ALLL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_ALLL_SoundVolume, ((int)Stethoscopy.LungSounds.ALLL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PLUL, Stethoscopy.LungSounds.PLUL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PLUL_SoundVolume, ((int)Stethoscopy.LungSounds.PLUL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PLLL, Stethoscopy.LungSounds.PLLL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PLLL_SoundVolume, ((int)Stethoscopy.LungSounds.PLLL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PRUL, Stethoscopy.LungSounds.PRUL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PRUL_SoundVolume, ((int)Stethoscopy.LungSounds.PRUL.SoundVolume).ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PRLL, Stethoscopy.LungSounds.PRLL.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_LungSounds_PRLL_SoundVolume, ((int)Stethoscopy.LungSounds.PRLL.SoundVolume).ToString());

            ComboBoxChoose(comboBox_Stethoscopy_AbdomenSounds_Bowel, Stethoscopy.AbdomenSounds.Bowel.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_AbdomenSounds_Bowel_SoundVolume, ((int)Stethoscopy.AbdomenSounds.Bowel.SoundVolume).ToString());

            ComboBoxChoose(comboBox_Stethoscopy_AbdomenSounds_Vascular, Stethoscopy.AbdomenSounds.Vascular.SoundID.ToString());
            ComboBoxChoose(comboBox_Stethoscopy_AbdomenSounds_Vascular_SoundVolume, ((int)Stethoscopy.AbdomenSounds.Vascular.SoundVolume).ToString());
        }

        private void ShowData_BloodPressure(Modle.BloodPressure BloodPressure)
        {
            //throw new NotImplementedException();
            textBox_BloodPressure_NIBP_Systolic.Text = BloodPressure.NIBP.Systolic.Value.ToString();
            textBox_BloodPressure_NIBP_Diastolic.Text = BloodPressure.NIBP.Diastolic.Value.ToString();

            ComboBoxChoose(comboBox_BloodPressure_Korotkoff_SoundVolume, ((int)BloodPressure.Korotkoff.SoundVolume).ToString());
            checkBox_BloodPressure_Korotkoff_Priority.IsChecked = BloodPressure.Korotkoff.Priority.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;

        }

        private void ShowData_Convulsions(Modle.Convulsions Convulsions)
        {
            checkBox_Convulsions_Clonic_RightArm.IsChecked = Convulsions.Clonic.RightArm.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Convulsions_Clonic_RightArm, Convulsions.Clonic.RightArm.Value.ToString());

            checkBox_Convulsions_Clonic_LeftArm.IsChecked = Convulsions.Clonic.LeftArm.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Convulsions_Clonic_LeftArm, Convulsions.Clonic.LeftArm.Value.ToString());

            checkBox_Convulsions_Tonic_NeckAnkylosis.IsChecked = Convulsions.Tonic.NeckAnkylosis.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Convulsions_Tonic_NeckAnkylosis, Convulsions.Tonic.NeckAnkylosis.Value.ToString());
        }

        private void ShowData_Pulse(Modle.Pulse Pulse)
        {
            //checkBox_Pulse_Body_Carotid_Right.IsChecked = Pulse.BodyPulse.CarotidRight.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_Body_Carotid_Right, Pulse.BodyPulse.CarotidRight.Value.ToString());
            //checkBox_Pulse_Body_Carotid_Left.IsChecked = Pulse.BodyPulse.CarotidLeft.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_Body_Carotid_Left, Pulse.BodyPulse.CarotidLeft.Value.ToString());
            //checkBox_Pulse_Body_Femoral_Right.IsChecked = Pulse.BodyPulse.FemoralRight.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_Body_Femoral_Right, Pulse.BodyPulse.FemoralRight.Value.ToString());
            //checkBox_Pulse_Body_Femoral_Left.IsChecked = Pulse.BodyPulse.FemoralLeft.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_Body_Femoral_Left, Pulse.BodyPulse.FemoralLeft.Value.ToString());

            //checkBox_Pulse_RightArm_Brachial.IsChecked = Pulse.RightArmPulse.Brachial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_RightArm_Brachial, Pulse.RightArmPulse.Brachial.Value.ToString());
            //checkBox_Pulse_RightArm_Radial.IsChecked = Pulse.RightArmPulse.Radial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_RightArm_Radial, Pulse.RightArmPulse.Radial.Value.ToString());

            //checkBox_Pulse_LeftArm_Brachial.IsChecked = Pulse.LeftArmPulse.Brachial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_LeftArm_Brachial, Pulse.LeftArmPulse.Brachial.Value.ToString());
            //checkBox_Pulse_LeftArm_Radial.IsChecked = Pulse.LeftArmPulse.Radial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_LeftArm_Radial, Pulse.LeftArmPulse.Radial.Value.ToString());

            //checkBox_Pulse_RightLeg_Popliteal.IsChecked = Pulse.RightLegPulse.Popliteal.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_RightLeg_Popliteal, Pulse.RightLegPulse.Popliteal.Value.ToString());
            //checkBox_Pulse_RightLeg_DorsalisPedis.IsChecked = Pulse.RightLegPulse.DorsalisPedis.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_RightLeg_DorsalisPedis, Pulse.RightLegPulse.DorsalisPedis.Value.ToString());
            //checkBox_Pulse_RightLeg_Heel.IsChecked = Pulse.RightLegPulse.Heel.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_RightLeg_Heel, Pulse.RightLegPulse.Heel.Value.ToString());

            //checkBox_Pulse_LeftLeg_Popliteal.IsChecked = Pulse.LeftLegPulse.Popliteal.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_LeftLeg_Popliteal, Pulse.LeftLegPulse.Popliteal.Value.ToString());
            //checkBox_Pulse_LeftLeg_DorsalisPedis.IsChecked = Pulse.LeftLegPulse.DorsalisPedis.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_LeftLeg_DorsalisPedis, Pulse.LeftLegPulse.DorsalisPedis.Value.ToString());
            //checkBox_Pulse_LeftLeg_Heel.IsChecked = Pulse.LeftLegPulse.Heel.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Pulse_LeftLeg_Heel, Pulse.LeftLegPulse.Heel.Value.ToString());

        }

        private void ShowData_Secretions(Modle.Secretions Secretions)
        {
            checkBox_Secretions_Sweat.IsChecked = Secretions.Sweat.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Secretions_Eyes.IsChecked = Secretions.Eyes.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Secretions_Mouth.IsChecked = Secretions.Mouth.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Secretions_Ears.IsChecked = Secretions.Ears.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Secretions_Nose.IsChecked = Secretions.Nose.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            checkBox_Secretions_Froth.IsChecked = Secretions.Froth.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;

        }

        private void ShowData_Haemorrhage(Modle.Haemorrhage Haemorrhage)
        {
            checkBox_Haemorrhage_RightUpper_Arterial.IsChecked = Haemorrhage.RightUpper.Arterial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_RightUpper_Arterial, Haemorrhage.RightUpper.Arterial.Value.ToString());

            checkBox_Haemorrhage_RightUpper_Venous.IsChecked = Haemorrhage.RightUpper.Venous.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_RightUpper_Venous, Haemorrhage.RightUpper.Venous.Value.ToString());

            checkBox_Haemorrhage_LeftUpper_Arterial.IsChecked = Haemorrhage.LeftUpper.Arterial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_LeftUpper_Arterial, Haemorrhage.LeftUpper.Arterial.Value.ToString());

            checkBox_Haemorrhage_LeftUpper_Venous.IsChecked = Haemorrhage.LeftUpper.Venous.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_LeftUpper_Venous, Haemorrhage.LeftUpper.Venous.Value.ToString());

            checkBox_Haemorrhage_RightLower_Arterial.IsChecked = Haemorrhage.RightLower.Arterial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_RightLower_Arterial, Haemorrhage.RightLower.Arterial.Value.ToString());

            checkBox_Haemorrhage_RightLower_Venous.IsChecked = Haemorrhage.RightLower.Venous.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_RightLower_Venous, Haemorrhage.RightLower.Venous.Value.ToString());

            checkBox_Haemorrhage_LeftLower_Arterial.IsChecked = Haemorrhage.LeftLower.Arterial.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_LeftLower_Arterial, Haemorrhage.LeftLower.Arterial.Value.ToString());

            checkBox_Haemorrhage_LeftLower_Venous.IsChecked = Haemorrhage.LeftLower.Venous.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Haemorrhage_LeftLower_Venous, Haemorrhage.LeftLower.Venous.Value.ToString());

        }

        private void ShowData_Catheterization(Modle.Urine Urine)
        {
            checkBox_Catheterization_Urine_Urinate.IsChecked = Urine.Urinate.Status == Simulator.Framework.Modle.ControllerStatus.Yes ? true : false;
            ComboBoxChoose(comboBox_Catheterization_Urine_UPD, Urine.Urinate.Value.ToString());
        }

        private void ShowData_DrugDelivery(Modle.DrugDelivery DrugDelivery)
        {
            ComboBoxChoose(comboBox_Medication_DrugDelivery_Drug_DrugCategory, "2");
            ComboBoxChoose(comboBox_Medication_DrugDelivery_Drug_DrugName, "2");
            ComboBoxChoose(comboBox_Medication_DrugDelivery_Drug_Route, "1");
            textBox_Medication_DrugDelivery_Dose.Text = "0";
            ComboBoxChoose(comboBox_Medication_DrugDelivery_Drug_DoseUnit, "1");

        }

        #endregion

        #region 设定

        #region 眼睛 Eyes

        private void comboBox_Eyes_Right_Eyelid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Eyelid.Right = (Modle.EyelidStatus)key;

                if (checkBox_Eyelid.IsChecked == true)
                {
                    comboBox_Eyes_Eyelid_Left.SelectedIndex = comboBox_Eyes_Eyelid_Right.SelectedIndex;
                }

                SendEyelid(_signs.Eyes.Eyelid);
            }
        }

        private void checkBox_Eyelid_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox_Eyelid.IsChecked == true)
            {
                comboBox_Eyes_Eyelid_Left.SelectedIndex = comboBox_Eyes_Eyelid_Right.SelectedIndex;
            }
        }

        private void comboBox_Eyes_Left_Eyelid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Eyelid.Left = (Modle.EyelidStatus)key;

                SendEyelid(_signs.Eyes.Eyelid);
            }
        }

        private void SendEyelid(Modle.Eyelid Eyelid)
        {
            byte[] dataBytes = Resolve.Eyes.SetDataBytes_Eyes_Eyelid(Eyelid);

            SendBytes(dataBytes);
        }


        private void comboBox_Eyes_Right_Blinking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Blinking.Right = (Modle.BlinkingSpeed)key;

                if (checkBox_Blinking.IsChecked == true)
                {
                    comboBox_Eyes_Blinking_Left.SelectedIndex = comboBox_Eyes_Blinking_Right.SelectedIndex;
                }

                SendBlinking(_signs.Eyes.Blinking);

            }
        }

        private void checkBox_Blinking_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox_Blinking.IsChecked == true)
            {
                comboBox_Eyes_Blinking_Left.SelectedIndex = comboBox_Eyes_Blinking_Right.SelectedIndex;
            }
        }

        private void comboBox_Eyes_Left_Blinking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Blinking.Left = (Modle.BlinkingSpeed)key;

                SendBlinking(_signs.Eyes.Blinking);
            }
        }

        private void SendBlinking(Modle.Blinking Blinking)
        {
            byte[] dataBytes = Resolve.Eyes.SetDataBytes_Eyes_Blinking(Blinking);

            SendBytes(dataBytes);
        }

        private void comboBox_Eyes_Right_Pupill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Pupill.Right = (Modle.PupillSize)key;

                if (checkBox_Pupill.IsChecked == true)
                {
                    comboBox_Eyes_Pupill_Left.SelectedIndex = comboBox_Eyes_Pupill_Right.SelectedIndex;
                }

                SendPupill(_signs.Eyes.Pupill);

            }
        }

        private void checkBox_Pupill_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox_Pupill.IsChecked == true)
            {
                comboBox_Eyes_Pupill_Left.SelectedIndex = comboBox_Eyes_Pupill_Right.SelectedIndex;
            }
        }


        private void comboBox_Eyes_Left_Pupill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Pupill.Left = (Modle.PupillSize)key;

                SendPupill(_signs.Eyes.Pupill);
            }
        }

        private void SendPupill(Modle.Pupill Pupill)
        {
            byte[] dataBytes = Resolve.Eyes.SetDataBytes_Eyes_Pupill(Pupill);

            SendBytes(dataBytes);
        }

        private void comboBox_Eyes_Right_Light_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Light.Right = (Modle.LightSensitivity)key;

                if (checkBox_Light.IsChecked == true)
                {
                    comboBox_Eyes_Light_Left.SelectedIndex = comboBox_Eyes_Light_Right.SelectedIndex;
                }

                SendLight(_signs.Eyes.Light);

            }
        }

        private void checkBox_Light_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox_Light.IsChecked == true)
            {
                comboBox_Eyes_Light_Left.SelectedIndex = comboBox_Eyes_Light_Right.SelectedIndex;
            }
        }

        private void comboBox_Eyes_Left_Light_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int key = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Eyes.Light.Left = (Modle.LightSensitivity)key;

                SendLight(_signs.Eyes.Light);
            }
        }

        private void SendLight(Modle.Light Light)
        {
            byte[] dataBytes = Resolve.Eyes.SetDataBytes_Eyes_Light(Light);

            SendBytes(dataBytes);
        }

        #endregion

        #region 紫绀 Cyanosis

        private void checkBox_Cyanosis_Lips_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Cyanosis_Lips_CheckChanged();
        }

        private void checkBox_Cyanosis_Lips_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Cyanosis_Lips_CheckChanged();
        }

        private void checkBox_Cyanosis_Lips_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Cyanosis_Lips.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Cyanosis_Lips.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Cyanosis_Lips.SelectedIndex = 0;
            }
            _signs.Cyanosis.Lips.Status = status;

            SendCyanosisLips(_signs.Cyanosis.Lips);
        }

        private void comboBox_Cyanosis_Lips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Cyanosis.Lips.Value = value;

                SendCyanosisLips(_signs.Cyanosis.Lips);
            }
        }

        private void SendCyanosisLips(Modle.Controller CyanosisLips)
        {
            byte[] dataBytes = Resolve.Cyanosis.SetDataBytes_Cyanosis_Lips(CyanosisLips);

            SendBytes(dataBytes);
        }

        private void checkBox_Cyanosis_Fingernail_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Cyanosis_Fingernail_CheckChanged();
        }

        private void checkBox_Cyanosis_Fingernail_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Cyanosis_Fingernail_CheckChanged();
        }

        private void checkBox_Cyanosis_Fingernail_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Cyanosis_Fingernail.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Cyanosis_Fingernail.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Cyanosis_Fingernail.SelectedIndex = 0;
            }

            _signs.Cyanosis.Fingernail.Status = status;

            SendCyanosisFingernail(_signs.Cyanosis.Fingernail);
        }

        private void comboBox_Cyanosis_Fingernail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Cyanosis.Fingernail.Value = value;

                SendCyanosisFingernail(_signs.Cyanosis.Fingernail);
            }
        }

        private void SendCyanosisFingernail(Modle.Controller CyanosisFingernail)
        {
            byte[] dataBytes = Resolve.Cyanosis.SetDataBytes_Cyanosis_Fingernail(CyanosisFingernail);

            SendBytes(dataBytes);
        }

        private void checkBox_Cyanosis_FootNail_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Cyanosis_FootNail_CheckChanged();
        }

        private void checkBox_Cyanosis_FootNail_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Cyanosis_FootNail_CheckChanged();
        }

        private void checkBox_Cyanosis_FootNail_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Cyanosis_FootNail.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Cyanosis_FootNail.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Cyanosis_FootNail.SelectedIndex = 0;
            }

            _signs.Cyanosis.FootNail.Status = status;

            SendCyanosisFootNail(_signs.Cyanosis.FootNail);
        }

        private void comboBox_Cyanosis_FootNail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Cyanosis.FootNail.Value = value;

                SendCyanosisFootNail(_signs.Cyanosis.FootNail);
            }
        }

        private void SendCyanosisFootNail(Modle.Controller CyanosisFootNail)
        {
            byte[] dataBytes = Resolve.Cyanosis.SetDataBytes_Cyanosis_FootNail(CyanosisFootNail);

            SendBytes(dataBytes);
        }


        #endregion

        #region  模拟声音 Analog Vocal

        private void comboBox_AnalogVocal_Vocal_SoundID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.AnalogVocal.Vocal.SoundID = value;

                SendAnalogVocal_Vocal(_signs.AnalogVocal.Vocal);
            }
        }

        private void comboBox_AnalogVocal_Vocal_PlaybackMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.AnalogVocal.Vocal.PlaybackMode = (Modle.PlaybackMode)value;

                SendAnalogVocal_Vocal(_signs.AnalogVocal.Vocal);
            }
        }

        private void comboBox_AnalogVocal_Vocal_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.AnalogVocal.Vocal.SoundVolume = (Modle.SoundVolume)value;

                SendAnalogVocal_Vocal(_signs.AnalogVocal.Vocal);
            }
        }

        private void SendAnalogVocal_Vocal(Modle.Vocal Vocal)
        {
            byte[] dataBytes = Resolve.AnalogVocal.SetDataBytes_AnalogVocal_Vocal(Vocal);

            SendBytes(dataBytes);
        }

        private void comboBox_AnalogVocal_Voice_SoundID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.AnalogVocal.Voice.SoundID = value;

                SendAnalogVocal_Voice(_signs.AnalogVocal.Voice);
            }
        }

        private void comboBox_AnalogVocal_Voice_PlaybackMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.AnalogVocal.Voice.PlaybackMode = (Modle.PlaybackMode)value;

                SendAnalogVocal_Voice(_signs.AnalogVocal.Voice);
            }
        }

        private void comboBox_AnalogVocal_Voice_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.AnalogVocal.Voice.SoundVolume = (Modle.SoundVolume)value;

                SendAnalogVocal_Voice(_signs.AnalogVocal.Voice);
            }
        }

        private void SendAnalogVocal_Voice(Modle.Voice Voice)
        {
            byte[] dataBytes = Resolve.AnalogVocal.SetDataBytes_AnalogVocal_Voice(Voice);

            SendBytes(dataBytes);
        }

        #endregion

        #region 气道 Airway

        #region  舌水肿 Tongue edema
        private void checkBox_Airway_TongueEdema_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_TongueEdema_CheckChanged();
        }

        private void checkBox_Airway_TongueEdema_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_TongueEdema_CheckChanged();
        }

        private void checkBox_Airway_TongueEdema_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_TongueEdema.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_TongueEdema.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_TongueEdema.SelectedIndex = 0;
            }
            _signs.Airway.TongueEdema.Status = status;

            SendAirwayTongueEdema(_signs.Airway.TongueEdema);
        }

        private void comboBox_Airway_TongueEdema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.TongueEdema.Value = value;

                SendAirwayTongueEdema(_signs.Airway.TongueEdema);
            }
        }

        private void SendAirwayTongueEdema(Modle.Controller TongueEdema)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_TongueEdema(TongueEdema);

            SendBytes(dataBytes);
        }

        #endregion

        #region 舌头后坠 Tongue fallback
        private void checkBox_Airway_TongueFallback_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_TongueFallback_CheckChanged();
        }

        private void checkBox_Airway_TongueFallback_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_TongueFallback_CheckChanged();
        }

        private void checkBox_Airway_TongueFallback_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_TongueFallback.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_TongueFallback.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_TongueFallback.SelectedIndex = 0;
            }
            _signs.Airway.TongueFallback.Status = status;

            SendAirwayTongueFallback(_signs.Airway.TongueFallback);
        }

        private void comboBox_Airway_TongueFallback_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.TongueFallback.Value = value;

                SendAirwayTongueFallback(_signs.Airway.TongueFallback);
            }
        }

        private void SendAirwayTongueFallback(Modle.Controller TongueFallback)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_TongueFallback(TongueFallback);

            SendBytes(dataBytes);
        }

        #endregion

        #region 异物气道阻塞 FBAO:Foreign Body Airway Obstruction
        private void checkBox_Airway_FBAO_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_FBAO_CheckChanged();
        }

        private void checkBox_Airway_FBAO_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_FBAO_CheckChanged();
        }

        private void checkBox_Airway_FBAO_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_FBAO.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_FBAO.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_FBAO.SelectedIndex = 0;
            }
            _signs.Airway.FBAO.Status = status;

            SendAirwayFBAO(_signs.Airway.FBAO);
        }

        private void comboBox_Airway_FBAO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.FBAO.Value = value;

                SendAirwayFBAO(_signs.Airway.FBAO);
            }
        }

        private void SendAirwayFBAO(Modle.Controller FBAO)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_FBAO(FBAO);

            SendBytes(dataBytes);
        }

        #endregion

        #region 咽部阻塞 Pharyngeal Obstruction

        private void checkBox_Airway_PharyngealObstruction_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_PharyngealObstruction_CheckChanged();
        }

        private void checkBox_Airway_PharyngealObstruction_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_PharyngealObstruction_CheckChanged();
        }

        private void checkBox_Airway_PharyngealObstruction_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_PharyngealObstruction.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_PharyngealObstruction.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_PharyngealObstruction.SelectedIndex = 0;
            }
            _signs.Airway.PharyngealObstruction.Status = status;

            SendAirwayPharyngealObstruction(_signs.Airway.PharyngealObstruction);
        }

        private void comboBox_Airway_PharyngealObstruction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.PharyngealObstruction.Value = value;

                SendAirwayPharyngealObstruction(_signs.Airway.PharyngealObstruction);
            }
        }

        private void SendAirwayPharyngealObstruction(Modle.Controller PharyngealObstruction)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_PharyngealObstruction(PharyngealObstruction);

            SendBytes(dataBytes);
        }

        #endregion

        #region 喉痉挛 Laryngospasm

        private void checkBox_Airway_Laryngospasm_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Laryngospasm_CheckChanged();
        }

        private void checkBox_Airway_Laryngospasm_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Laryngospasm_CheckChanged();
        }

        private void checkBox_Airway_Laryngospasm_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Laryngospasm.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Laryngospasm.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Laryngospasm.SelectedIndex = 0;
            }
            _signs.Airway.Laryngospasm.Status = status;

            SendAirwayLaryngospasm(_signs.Airway.Laryngospasm);
        }

        private void comboBox_Airway_Laryngospasm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Laryngospasm.Value = value;

                SendAirwayLaryngospasm(_signs.Airway.Laryngospasm);
            }
        }
        private void SendAirwayLaryngospasm(Modle.Controller Laryngospasm)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_Laryngospasm(Laryngospasm);

            SendBytes(dataBytes);
        }


        #endregion

        #region 牙关紧闭 Trismus

        private void checkBox_Airway_Trismus_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Trismus_CheckChanged();
        }

        private void checkBox_Airway_Trismus_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Trismus_CheckChanged();
        }

        private void checkBox_Airway_Trismus_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Trismus.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Trismus.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Trismus.SelectedIndex = 0;
            }
            _signs.Airway.Trismus.Status = status;

            SendAirwayTrismus(_signs.Airway.Trismus);
        }

        private void comboBox_Airway_Trismus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Trismus.Value = value;

                SendAirwayTrismus(_signs.Airway.Trismus);
            }
        }

        private void SendAirwayTrismus(Modle.Controller Trismus)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_Trismus(Trismus);

            SendBytes(dataBytes);
        }

        #endregion

        #region 颈部强直 Neck Ankylosis

        private void checkBox_Airway_NeckAnkylosis_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_NeckAnkylosis_CheckChanged();
        }

        private void checkBox_Airway_NeckAnkylosis_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_NeckAnkylosis_CheckChanged();
        }

        private void checkBox_Airway_NeckAnkylosis_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_NeckAnkylosis.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_NeckAnkylosis.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_NeckAnkylosis.SelectedIndex = 0;
            }
            _signs.Airway.NeckAnkylosis.Status = status;

            SendAirwayNeckAnkylosis(_signs.Airway.NeckAnkylosis);
        }

        private void comboBox_Airway_NeckAnkylosis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.NeckAnkylosis.Value = value;

                SendAirwayNeckAnkylosis(_signs.Airway.NeckAnkylosis);
            }
        }

        private void SendAirwayNeckAnkylosis(Modle.Controller NeckAnkylosis)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_NeckAnkylosis(NeckAnkylosis);

            SendBytes(dataBytes);
        }

        #endregion

        #region 肺阻力  Resistance

        private void checkBox_Airway_Resistance_Right_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Resistance_Right_CheckChanged();
        }

        private void checkBox_Airway_Resistance_Right_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Resistance_Right_CheckChanged();
        }

        private void checkBox_Airway_Resistance_Right_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Resistance_Right.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Resistance_Right.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Resistance_Right.SelectedIndex = 0;
            }
            _signs.Airway.Resistance.Right.Status = status;

            SendAirwayResistance(_signs.Airway.Resistance);
        }

        private void comboBox_Airway_Resistance_Right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Resistance.Right.Value = value;

                SendAirwayResistance(_signs.Airway.Resistance);
            }
        }

        private void checkBox_Airway_Resistance_Left_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Resistance_Left_CheckChanged();
        }

        private void checkBox_Airway_Resistance_Left_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Resistance_Left_CheckChanged();
        }

        private void checkBox_Airway_Resistance_Left_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Resistance_Left.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Resistance_Left.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Resistance_Left.SelectedIndex = 0;
            }
            _signs.Airway.Resistance.Left.Status = status;

            SendAirwayResistance(_signs.Airway.Resistance);
        }

        private void comboBox_Airway_Resistance_Left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Resistance.Left.Value = value;

                SendAirwayResistance(_signs.Airway.Resistance);
            }
        }

        private void SendAirwayResistance(Modle.Resistance Resistance)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_Resistance(Resistance);

            SendBytes(dataBytes);
        }

        #endregion

        #region 顺应性  Compliance

        private void checkBox_Airway_Compliance_Right_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Compliance_Right_CheckChanged();
        }

        private void checkBox_Airway_Compliance_Right_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Compliance_Right_CheckChanged();
        }

        private void checkBox_Airway_Compliance_Right_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Compliance_Right.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Compliance_Right.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Compliance_Right.SelectedIndex = 0;
            }
            _signs.Airway.Compliance.Right.Status = status;

            SendAirwayCompliance(_signs.Airway.Compliance);
        }

        private void comboBox_Airway_Compliance_Right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Compliance.Right.Value = value;

                SendAirwayCompliance(_signs.Airway.Compliance);
            }
        }

        private void checkBox_Airway_Compliance_Left_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Compliance_Left_CheckChanged();
        }

        private void checkBox_Airway_Compliance_Left_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Compliance_Left_CheckChanged();
        }

        private void checkBox_Airway_Compliance_Left_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Compliance_Left.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Compliance_Left.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Compliance_Left.SelectedIndex = 0;
            }
            _signs.Airway.Compliance.Left.Status = status;

            SendAirwayCompliance(_signs.Airway.Compliance);
        }

        private void comboBox_Airway_Compliance_Left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Compliance.Left.Value = value;

                SendAirwayCompliance(_signs.Airway.Compliance);
            }
        }

        private void SendAirwayCompliance(Modle.Compliance Compliance)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_Compliance(Compliance);

            SendBytes(dataBytes);
        }

        #endregion

        #region 气胸 Aerothorax

        private void checkBox_Airway_Aerothorax_Right_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Aerothorax_Right_CheckChanged();
        }

        private void checkBox_Airway_Aerothorax_Right_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Aerothorax_Right_CheckChanged();
        }

        private void checkBox_Airway_Aerothorax_Right_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Aerothorax_Right.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Aerothorax_Right.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Aerothorax_Right.SelectedIndex = 0;
            }
            _signs.Airway.Aerothorax.Right.Status = status;

            SendAirwayAerothorax(_signs.Airway.Aerothorax);
        }

        private void comboBox_Airway_Aerothorax_Right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Aerothorax.Right.Value = value;

                SendAirwayAerothorax(_signs.Airway.Aerothorax);
            }
        }

        private void checkBox_Airway_Aerothorax_Left_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Aerothorax_Left_CheckChanged();
        }

        private void checkBox_Airway_Aerothorax_Left_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_Aerothorax_Left_CheckChanged();
        }

        private void checkBox_Airway_Aerothorax_Left_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_Aerothorax_Left.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_Aerothorax_Left.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_Aerothorax_Left.SelectedIndex = 0;
            }
            _signs.Airway.Aerothorax.Left.Status = status;

            SendAirwayAerothorax(_signs.Airway.Aerothorax);
        }

        private void comboBox_Airway_Aerothorax_Left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.Aerothorax.Left.Value = value;

                SendAirwayAerothorax(_signs.Airway.Aerothorax);
            }
        }

        private void SendAirwayAerothorax(Modle.Aerothorax Aerothorax)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_Aerothorax(Aerothorax);

            SendBytes(dataBytes);
        }

        #endregion

        #region 自主呼吸 Autonomous Respiration

        private void checkBox_Airway_AutonomousRespiration_Right_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_AutonomousRespiration_Right_CheckChanged();
        }

        private void checkBox_Airway_AutonomousRespiration_Right_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_AutonomousRespiration_Right_CheckChanged();
        }

        private void checkBox_Airway_AutonomousRespiration_Right_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_AutonomousRespiration_Right.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_AutonomousRespiration_Right.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_AutonomousRespiration_Right.SelectedIndex = 0;
            }
            _signs.Airway.AutonomousRespiration.Right.Status = status;

            SendAirwayAutonomousRespiration(_signs.Airway.AutonomousRespiration);
        }

        private void comboBox_Airway_AutonomousRespiration_Right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.AutonomousRespiration.Right.Value = value;

                SendAirwayAutonomousRespiration(_signs.Airway.AutonomousRespiration);
            }
        }

        private void checkBox_Airway_AutonomousRespiration_Left_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_AutonomousRespiration_Left_CheckChanged();
        }

        private void checkBox_Airway_AutonomousRespiration_Left_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_AutonomousRespiration_Left_CheckChanged();
        }

        private void checkBox_Airway_AutonomousRespiration_Left_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_AutonomousRespiration_Left.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_AutonomousRespiration_Left.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_AutonomousRespiration_Left.SelectedIndex = 0;
            }
            _signs.Airway.AutonomousRespiration.Left.Status = status;

            SendAirwayAutonomousRespiration(_signs.Airway.AutonomousRespiration);
        }

        private void comboBox_Airway_AutonomousRespiration_Left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.AutonomousRespiration.Left.Value = value;

                SendAirwayAutonomousRespiration(_signs.Airway.AutonomousRespiration);
            }
        }

        private void SendAirwayAutonomousRespiration(Modle.AutonomousRespiration AutonomousRespiration)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_AutonomousRespiration(AutonomousRespiration);

            SendBytes(dataBytes);
        }

        #endregion

        #region 胃胀气 Stomach distention

        private void checkBox_Airway_StomachDistention_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_StomachDistention_CheckChanged();
        }

        private void checkBox_Airway_StomachDistention_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_StomachDistention_CheckChanged();
        }

        private void checkBox_Airway_StomachDistention_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_StomachDistention.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_StomachDistention.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_StomachDistention.SelectedIndex = 0;
            }
            _signs.Airway.StomachDistention.Status = status;

            SendAirwayStomachDistention(_signs.Airway.StomachDistention);
        }

        private void comboBox_Airway_StomachDistention_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.StomachDistention.Value = value;

                SendAirwayStomachDistention(_signs.Airway.StomachDistention);
            }
        }

        private void SendAirwayStomachDistention(Modle.Controller StomachDistention)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_StomachDistention(StomachDistention);

            SendBytes(dataBytes);
        }

        #endregion

        #region 呼出CO2 Exhale CO2

        private void checkBox_Airway_ExhaleCO2_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_ExhaleCO2_CheckChanged();
        }

        private void checkBox_Airway_ExhaleCO2_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Airway_ExhaleCO2_CheckChanged();
        }

        private void checkBox_Airway_ExhaleCO2_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Airway_ExhaleCO2.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Airway_ExhaleCO2.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Airway_ExhaleCO2.SelectedIndex = 0;
            }
            _signs.Airway.ExhaleCO2.Status = status;

            SendAirwayExhaleCO2(_signs.Airway.ExhaleCO2);
        }

        private void comboBox_Airway_ExhaleCO2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Airway.ExhaleCO2.Value = value;

                SendAirwayExhaleCO2(_signs.Airway.ExhaleCO2);
            }
        }

        private void SendAirwayExhaleCO2(Modle.Controller ExhaleCO2)
        {
            byte[] dataBytes = Resolve.Airway.SetDataBytes_Airway_ExhaleCO2(ExhaleCO2);

            SendBytes(dataBytes);
        }

        #endregion

        #endregion

        #region 生命体征 

        #region 循环 Cyclic

        private void comboBox_VitalSigns_Cyclic_Rhythm_Basic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Basic = 0;
            if (comboBox_VitalSigns_Cyclic_Rhythm_Basic.SelectedItem != null)
                Basic = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_Rhythm_Basic.SelectedItem).Value);

            _signs.VitalSigns.Cyclic.Rhythm.Basic = Basic;

            Send_VitalSigns_Cyclic_Rhythm(_signs.VitalSigns.Cyclic.Rhythm);
        }

        private void comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Extrasystole = 0;
            if (comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.SelectedItem != null)
                Extrasystole = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_Rhythm_Extrasystole.SelectedItem).Value);

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
                HeartRateUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_HeartRate_Unit.SelectedItem).Value);

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
                IBPUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_IBP_Unit.SelectedItem).Value);

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
                SpO2Unit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_SpO2_Unit.SelectedItem).Value);

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
                PAPUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_PAP_Unit.SelectedItem).Value);

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
                CVPUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_CVP_Unit.SelectedItem).Value);

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
                PAWPUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_PAWP_Unit.SelectedItem).Value);

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
                C_O_Unit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Cyclic_C_O__Unit.SelectedItem).Value);

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
                RespType = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_RespType.SelectedItem).Value);

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
                RespRateUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_RespRate_Unit.SelectedItem).Value);

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
                InspiratoryCapacityUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_InspiratoryCapacity_Unit.SelectedItem).Value);

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
                RespRatioUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_RespRatio_Unit.SelectedItem).Value);

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
                CO2Unit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_CO2_Unit.SelectedItem).Value);

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
                ETCO2Unit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_ETCO2_Unit.SelectedItem).Value);

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
                O2Unit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_O2_Unit.SelectedItem).Value);

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
                N2OUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_N2O_Unit.SelectedItem).Value);

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
                AGTUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Breath_AGT_Unit.SelectedItem).Value);

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
                PeripheralTemperatureUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_PeripheralTemperature_Unit.SelectedItem).Value);

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
                BloodTemperatureUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_BloodTemperature_Unit.SelectedItem).Value);

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
                pHUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_pH_Unit.SelectedItem).Value);

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
                ICPUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_ICP_Unit.SelectedItem).Value);

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
                TOFUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_TOF_Unit.SelectedItem).Value);

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
                TOFRatioUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_TOFRatio_Unit.SelectedItem).Value);

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
                PTCUnit = int.Parse(((Library.ListItem)comboBox_VitalSigns_Other_PTC_Unit.SelectedItem).Value);

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

        #region 触诊 Palpation

        private void checkBox_Palpation_AbdominalTouch_RightUpper_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_RightUpper_CheckChanged();
        }
        private void checkBox_Palpation_AbdominalTouch_RightUpper_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_RightUpper_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_RightUpper_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Palpation_AbdominalTouch_RightUpper.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;
            }
            _signs.Palpation.Abdominal.RightUpper.Status = status;

            Send_Palpation_AbdominalTouch_RightUpper(_signs.Palpation.Abdominal.RightUpper);
        }

        private void Send_Palpation_AbdominalTouch_RightUpper(Modle.Controller RightUpper)
        {
            byte[] dataBytes = Resolve.Palpation.SetDataBytes_Abdominal_RightUpper(RightUpper);

            SendBytes(dataBytes);
        }

        private void checkBox_Palpation_AbdominalTouch_LeftUpper_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_LeftUpper_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_LeftUpper_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_LeftUpper_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_LeftUpper_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Palpation_AbdominalTouch_LeftUpper.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;
            }
            _signs.Palpation.Abdominal.LeftUpper.Status = status;

            Send_Palpation_AbdominalTouch_LeftUpper(_signs.Palpation.Abdominal.LeftUpper);
        }

        private void Send_Palpation_AbdominalTouch_LeftUpper(Modle.Controller LeftUpper)
        {
            byte[] dataBytes = Resolve.Palpation.SetDataBytes_Abdominal_LeftUpper(LeftUpper);

            SendBytes(dataBytes);
        }

        private void checkBox_Palpation_AbdominalTouch_Middle_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_Middle_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_Middle_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_Middle_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_Middle_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Palpation_AbdominalTouch_Middle.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;
            }
            _signs.Palpation.Abdominal.Middle.Status = status;

            Send_Palpation_AbdominalTouch_Middle(_signs.Palpation.Abdominal.Middle);
        }

        private void Send_Palpation_AbdominalTouch_Middle(Modle.Controller Middle)
        {
            byte[] dataBytes = Resolve.Palpation.SetDataBytes_Abdominal_Middle(Middle);

            SendBytes(dataBytes);
        }

        private void checkBox_Palpation_AbdominalTouch_RightLower_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_RightLower_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_RightLower_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_RightLower_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_RightLower_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Palpation_AbdominalTouch_RightLower.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;
            }
            _signs.Palpation.Abdominal.RightLower.Status = status;

            Send_Palpation_AbdominalTouch_RightLower(_signs.Palpation.Abdominal.RightLower);
        }

        private void Send_Palpation_AbdominalTouch_RightLower(Modle.Controller RightLower)
        {
            byte[] dataBytes = Resolve.Palpation.SetDataBytes_Abdominal_RightLower(RightLower);

            SendBytes(dataBytes);
        }

        private void checkBox_Palpation_AbdominalTouch_LeftLower_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_LeftLower_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_LeftLower_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Palpation_AbdominalTouch_LeftLower_CheckChanged();
        }

        private void checkBox_Palpation_AbdominalTouch_LeftLower_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Palpation_AbdominalTouch_LeftLower.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;
            }
            _signs.Palpation.Abdominal.LeftLower.Status = status;

            Send_Palpation_AbdominalTouch_LeftLower(_signs.Palpation.Abdominal.LeftLower);
        }

        private void Send_Palpation_AbdominalTouch_LeftLower(Modle.Controller LeftLower)
        {
            byte[] dataBytes = Resolve.Palpation.SetDataBytes_Abdominal_LeftLower(LeftLower);

            SendBytes(dataBytes);
        }

        #endregion

        #region 听诊 Stethoscopy

        #region 心音

        private void comboBox_Stethoscopy_HeartSounds_M_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.M.SoundID = value;

                SendStethoscopyHeartSoundsM(_signs.Stethoscopy.HeartSounds.M);
            }
        }

        private void comboBox_Stethoscopy_HeartSounds_M_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.M.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyHeartSoundsM(_signs.Stethoscopy.HeartSounds.M);
            }
        }

        private void SendStethoscopyHeartSoundsM(Modle.BodySound M)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_HeartSound.SetDataBytes_Stethoscopy_HeartSound_M(M);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_HeartSounds_T_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.T.SoundID = value;

                SendStethoscopyHeartSoundsT(_signs.Stethoscopy.HeartSounds.T);
            }
        }

        private void comboBox_Stethoscopy_HeartSounds_T_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.T.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyHeartSoundsT(_signs.Stethoscopy.HeartSounds.T);
            }
        }

        private void SendStethoscopyHeartSoundsT(Modle.BodySound T)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_HeartSound.SetDataBytes_Stethoscopy_HeartSound_T(T);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_HeartSounds_A_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.A.SoundID = value;

                SendStethoscopyHeartSoundsA(_signs.Stethoscopy.HeartSounds.A);
            }
        }

        private void comboBox_Stethoscopy_HeartSounds_A_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.A.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyHeartSoundsA(_signs.Stethoscopy.HeartSounds.A);
            }
        }

        private void SendStethoscopyHeartSoundsA(Modle.BodySound A)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_HeartSound.SetDataBytes_Stethoscopy_HeartSound_A(A);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_HeartSounds_P_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.P.SoundID = value;

                SendStethoscopyHeartSoundsP(_signs.Stethoscopy.HeartSounds.P);
            }
        }

        private void comboBox_Stethoscopy_HeartSounds_P_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.P.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyHeartSoundsP(_signs.Stethoscopy.HeartSounds.P);
            }
        }

        private void SendStethoscopyHeartSoundsP(Modle.BodySound P)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_HeartSound.SetDataBytes_Stethoscopy_HeartSound_P(P);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_HeartSounds_E_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.E.SoundID = value;

                SendStethoscopyHeartSoundsE(_signs.Stethoscopy.HeartSounds.E);
            }
        }

        private void comboBox_Stethoscopy_HeartSounds_E_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.HeartSounds.E.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyHeartSoundsE(_signs.Stethoscopy.HeartSounds.E);
            }
        }

        private void SendStethoscopyHeartSoundsE(Modle.BodySound E)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_HeartSound.SetDataBytes_Stethoscopy_HeartSound_E(E);

            SendBytes(dataBytes);
        }

        #endregion

        #region 肺音

        private void comboBox_Stethoscopy_LungSounds_ARUL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ARUL.SoundID = value;

                SendStethoscopyLungSoundsARUL(_signs.Stethoscopy.LungSounds.ARUL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_ARUL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ARUL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsARUL(_signs.Stethoscopy.LungSounds.ARUL);
            }
        }

        private void SendStethoscopyLungSoundsARUL(Modle.BodySound ARUL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_ARUL(ARUL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_ALUL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ALUL.SoundID = value;

                SendStethoscopyLungSoundsALUL(_signs.Stethoscopy.LungSounds.ALUL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_ALUL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ALUL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsALUL(_signs.Stethoscopy.LungSounds.ALUL);
            }
        }

        private void SendStethoscopyLungSoundsALUL(Modle.BodySound ALUL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_ALUL(ALUL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_ARML_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ARML.SoundID = value;

                SendStethoscopyLungSoundsARML(_signs.Stethoscopy.LungSounds.ARML);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_ARML_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ARML.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsARML(_signs.Stethoscopy.LungSounds.ARML);
            }
        }

        private void SendStethoscopyLungSoundsARML(Modle.BodySound ARML)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_ARML(ARML);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_ARLL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ARLL.SoundID = value;

                SendStethoscopyLungSoundsARLL(_signs.Stethoscopy.LungSounds.ARLL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_ARLL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ARLL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsARLL(_signs.Stethoscopy.LungSounds.ARLL);
            }
        }

        private void SendStethoscopyLungSoundsARLL(Modle.BodySound ARLL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_ARLL(ARLL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_ALLL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ALLL.SoundID = value;

                SendStethoscopyLungSoundsALLL(_signs.Stethoscopy.LungSounds.ALLL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_ALLL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.ALLL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsALLL(_signs.Stethoscopy.LungSounds.ALLL);
            }
        }

        private void SendStethoscopyLungSoundsALLL(Modle.BodySound ALLL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_ALLL(ALLL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_PLUL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PLUL.SoundID = value;

                SendStethoscopyLungSoundsPLUL(_signs.Stethoscopy.LungSounds.PLUL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_PLUL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PLUL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsPLUL(_signs.Stethoscopy.LungSounds.PLUL);
            }
        }

        private void SendStethoscopyLungSoundsPLUL(Modle.BodySound PLUL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_PLUL(PLUL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_PRUL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PRUL.SoundID = value;

                SendStethoscopyLungSoundsPRUL(_signs.Stethoscopy.LungSounds.PRUL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_PRUL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PRUL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsPRUL(_signs.Stethoscopy.LungSounds.PRUL);
            }
        }

        private void SendStethoscopyLungSoundsPRUL(Modle.BodySound PRUL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_PRUL(PRUL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_PLLL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PLLL.SoundID = value;

                SendStethoscopyLungSoundsPLLL(_signs.Stethoscopy.LungSounds.PLLL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_PLLL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PLLL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsPLLL(_signs.Stethoscopy.LungSounds.PLLL);
            }
        }

        private void SendStethoscopyLungSoundsPLLL(Modle.BodySound PLLL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_PLLL(PLLL);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_LungSounds_PRLL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PRLL.SoundID = value;

                SendStethoscopyLungSoundsPRLL(_signs.Stethoscopy.LungSounds.PRLL);
            }
        }

        private void comboBox_Stethoscopy_LungSounds_PRLL_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.LungSounds.PRLL.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyLungSoundsPRLL(_signs.Stethoscopy.LungSounds.PRLL);
            }
        }

        private void SendStethoscopyLungSoundsPRLL(Modle.BodySound PRLL)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_LungSound.SetDataBytes_Stethoscopy_LungSound_PRLL(PRLL);

            SendBytes(dataBytes);
        }

        #endregion

        #region 腹部


        private void comboBox_Stethoscopy_AbdomenSounds_Bowel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.AbdomenSounds.Bowel.SoundID = value;

                SendStethoscopyAbdomenSoundsBowel(_signs.Stethoscopy.AbdomenSounds.Bowel);
            }
        }

        private void comboBox_Stethoscopy_AbdomenSounds_Bowel_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.AbdomenSounds.Bowel.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyAbdomenSoundsBowel(_signs.Stethoscopy.AbdomenSounds.Bowel);
            }
        }

        private void SendStethoscopyAbdomenSoundsBowel(Modle.BodySound Bowel)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_AbdomenSound.SetDataBytes_Stethoscopy_AbdomenSound_Bowel(Bowel);

            SendBytes(dataBytes);
        }

        private void comboBox_Stethoscopy_AbdomenSounds_Vascular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.AbdomenSounds.Vascular.SoundID = value;

                SendStethoscopyAbdomenSoundsVascular(_signs.Stethoscopy.AbdomenSounds.Vascular);
            }
        }

        private void comboBox_Stethoscopy_AbdomenSounds_Vascular_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Stethoscopy.AbdomenSounds.Vascular.SoundVolume = (Modle.SoundVolume)value;

                SendStethoscopyAbdomenSoundsVascular(_signs.Stethoscopy.AbdomenSounds.Vascular);
            }
        }

        private void SendStethoscopyAbdomenSoundsVascular(Modle.BodySound Vascular)
        {
            byte[] dataBytes = Resolve.Stethoscopy.Stethoscopy_AbdomenSound.SetDataBytes_Stethoscopy_AbdomenSound_Vascular(Vascular);

            SendBytes(dataBytes);
        }

        #endregion

        #endregion

        #region 血压 Blood Pressure

        private void textBox_BloodPressure_NIBP_Systolic_TextChanged(object sender, TextChangedEventArgs e)
        {

            BloodPressure_NIBP_TextChanged();
        }

        private void textBox_BloodPressure_NIBP_Diastolic_TextChanged(object sender, TextChangedEventArgs e)
        {
            BloodPressure_NIBP_TextChanged();
        }

        private void BloodPressure_NIBP_TextChanged()
        {
            if (textBox_BloodPressure_NIBP_Systolic.Text.Trim() == "")
                return;

            if (textBox_BloodPressure_NIBP_Diastolic.Text.Trim() == "")
                return;

            _signs.BloodPressure.NIBP.Systolic.Value = int.Parse(textBox_BloodPressure_NIBP_Systolic.Text.Trim());
            _signs.BloodPressure.NIBP.Diastolic.Value = int.Parse(textBox_BloodPressure_NIBP_Diastolic.Text.Trim());            

            SendBloodPressureNIBP(_signs.BloodPressure.NIBP);
        }

        private void comboBox_BloodPressure_NIBP_Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _signs.BloodPressure.NIBP.Systolic.Unit = _signs.BloodPressure.NIBP.Diastolic.Unit = int.Parse(((Library.ListItem)comboBox_BloodPressure_NIBP_Unit.SelectedItem).Value);

            SendBloodPressureNIBP(_signs.BloodPressure.NIBP);
        }

        private void SendBloodPressureNIBP(Modle.NIBP NIBP)
        {
            byte[] dataBytes = Resolve.MeasureBP.SetDataBytes_NIBP(NIBP);

            SendBytes(dataBytes);
        }

        private void comboBox_BloodPressure_Korotkoff_SoundVolume_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _signs.BloodPressure.Korotkoff.SoundVolume = (Modle.SoundVolume)int.Parse(((Library.ListItem)comboBox_BloodPressure_Korotkoff_SoundVolume.SelectedItem).Value);

            SendBloodPressureKorotkoff(_signs.BloodPressure.Korotkoff);
        }

        private void checkBox_BloodPressure_Korotkoff_Priority_Checked(object sender, RoutedEventArgs e)
        {
            BloodPressure_Korotkoff_Priority_CheckChanged();
        }

        private void checkBox_BloodPressure_Korotkoff_Priority_Unchecked(object sender, RoutedEventArgs e)
        {
            BloodPressure_Korotkoff_Priority_CheckChanged();
        }

        private void BloodPressure_Korotkoff_Priority_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_BloodPressure_Korotkoff_Priority.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;                
            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.BloodPressure.Korotkoff.Priority.Status = status;

            SendBloodPressureKorotkoff(_signs.BloodPressure.Korotkoff);
        }

        private void SendBloodPressureKorotkoff(Modle.Korotkoff Korotkoff)
        {
            byte[] dataBytes = Resolve.MeasureBP.SetDataBytes_Korotkoff(Korotkoff);

            SendBytes(dataBytes);
        }

        #endregion

        #region 惊厥 Convulsions

        private void checkBox_Convulsions_Clonic_RightArm_Checked(object sender, RoutedEventArgs e)
        {
            Convulsions_Clonic_RightArm_CheckChanged();
        }

        private void checkBox_Convulsions_Clonic_RightArm_Unchecked(object sender, RoutedEventArgs e)
        {
            Convulsions_Clonic_RightArm_CheckChanged();
        }

        private void Convulsions_Clonic_RightArm_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Convulsions_Clonic_RightArm.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Convulsions_Clonic_RightArm.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Convulsions_Clonic_RightArm.SelectedIndex = 0;
            }
            _signs.Convulsions.Clonic.RightArm.Status = status;

            SendConvulsionsClonic(_signs.Convulsions.Clonic);
        }

        private void comboBox_Convulsions_Clonic_RightArm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Convulsions.Clonic.RightArm.Value = value;

                SendConvulsionsClonic(_signs.Convulsions.Clonic);
            }
        }

        private void checkBox_Convulsions_Clonic_LeftArm_Checked(object sender, RoutedEventArgs e)
        {
            Convulsions_Clonic_LeftArm_CheckChanged();
        }

        private void checkBox_Convulsions_Clonic_LeftArm_Unchecked(object sender, RoutedEventArgs e)
        {
            Convulsions_Clonic_LeftArm_CheckChanged();
        }

        private void Convulsions_Clonic_LeftArm_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Convulsions_Clonic_LeftArm.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Convulsions_Clonic_LeftArm.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Convulsions_Clonic_LeftArm.SelectedIndex = 0;
            }
            _signs.Convulsions.Clonic.LeftArm.Status = status;

            SendConvulsionsClonic(_signs.Convulsions.Clonic);
        }

        private void comboBox_Convulsions_Clonic_LeftArm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Convulsions.Clonic.LeftArm.Value = value;

                SendConvulsionsClonic(_signs.Convulsions.Clonic);
            }
        }

        private void SendConvulsionsClonic(Modle.Clonic Clonic)
        {
            byte[] dataBytes = Resolve.Convulsions.SetDataBytes_Convulsions_Clonic(Clonic);

            SendBytes(dataBytes);
        }

        private void checkBox_Convulsions_Tonic_NeckAnkylosis_Checked(object sender, RoutedEventArgs e)
        {
            Convulsions_Tonic_NeckAnkylosis_CheckChanged();
        }

        private void checkBox_Convulsions_Tonic_NeckAnkylosis_Unchecked(object sender, RoutedEventArgs e)
        {
            Convulsions_Tonic_NeckAnkylosis_CheckChanged();
        }

        private void Convulsions_Tonic_NeckAnkylosis_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Convulsions_Tonic_NeckAnkylosis.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Convulsions_Tonic_NeckAnkylosis.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Convulsions_Tonic_NeckAnkylosis.SelectedIndex = 0;
            }
            _signs.Convulsions.Tonic.NeckAnkylosis.Status = status;

            SendConvulsionsTonic(_signs.Convulsions.Tonic);
        }

        private void comboBox_Convulsions_Tonic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Convulsions.Tonic.NeckAnkylosis.Value = value;

                SendConvulsionsTonic(_signs.Convulsions.Tonic);
            }
        }

        private void SendConvulsionsTonic(Modle.Tonic Tonic)
        {
            byte[] dataBytes = Resolve.Convulsions.SetDataBytes_Convulsions_Tonic(Tonic);

            SendBytes(dataBytes);
        }

        #endregion

        #region 脉搏 Pulse

        #region 右臂 Right Arm

        private void checkBox_Pulse_RightArm_Brachial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightArm_Brachial_CheckChanged();
        }

        private void checkBox_Pulse_RightArm_Brachial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightArm_Brachial_CheckChanged();
        }

        private void checkBox_Pulse_RightArm_Brachial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_RightArm_Brachial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_RightArm_Brachial.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_RightArm_Brachial.SelectedIndex = 0;
            }
            _signs.Pulse.RightArmPulse.Brachial.Status = status;

            Send_Pulse_RightArm(_signs.Pulse.RightArmPulse);
        }

        private void comboBox_Pulse_RightArm_Brachial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.RightArmPulse.Brachial.Value = value;

                Send_Pulse_RightArm(_signs.Pulse.RightArmPulse);
            }
        }

        private void checkBox_Pulse_RightArm_Radial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightArm_Radial_CheckChanged();
        }

        private void checkBox_Pulse_RightArm_Radial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightArm_Radial_CheckChanged();
        }

        private void checkBox_Pulse_RightArm_Radial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_RightArm_Radial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_RightArm_Radial.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_RightArm_Radial.SelectedIndex = 0;
            }
            _signs.Pulse.RightArmPulse.Radial.Status = status;

            Send_Pulse_RightArm(_signs.Pulse.RightArmPulse);
        }

        private void comboBox_Pulse_RightArm_Radial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.RightArmPulse.Radial.Value = value;

                Send_Pulse_RightArm(_signs.Pulse.RightArmPulse);
            }
        }

        private void Send_Pulse_RightArm(Modle.RightArmPulse RightArmPulse)
        {
            byte[] dataBytes = Resolve.Pulse.SetDataBytes_Pulse_RightArmPulse(RightArmPulse);

            SendBytes(dataBytes);
        }

        #endregion

        #region 身体 Body 
        

        private void checkBox_Pulse_Body_Carotid_Right_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Carotid_Right_CheckChanged();
        }

        private void checkBox_Pulse_Body_Carotid_Right_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Carotid_Right_CheckChanged();
        }

        private void checkBox_Pulse_Body_Carotid_Right_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_Body_Carotid_Right.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_Body_Carotid_Right.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_Body_Carotid_Right.SelectedIndex = 0;
            }
            _signs.Pulse.BodyPulse.CarotidRight.Status = status;

            Send_Pulse_Body(_signs.Pulse.BodyPulse);
        }

        private void comboBox_Pulse_Body_Carotid_Right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.BodyPulse.CarotidRight.Value = value;

                Send_Pulse_Body(_signs.Pulse.BodyPulse);
            }
        }

        private void checkBox_Pulse_Body_Carotid_Left_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Carotid_Left_CheckChanged();
        }

        private void checkBox_Pulse_Body_Carotid_Left_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Carotid_Left_CheckChanged();
        }

        private void checkBox_Pulse_Body_Carotid_Left_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_Body_Carotid_Left.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_Body_Carotid_Left.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_Body_Carotid_Left.SelectedIndex = 0;
            }
            _signs.Pulse.BodyPulse.CarotidLeft.Status = status;

            Send_Pulse_Body(_signs.Pulse.BodyPulse);
        }

        private void comboBox_Pulse_Body_Carotid_Left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.BodyPulse.CarotidLeft.Value = value;

                Send_Pulse_Body(_signs.Pulse.BodyPulse);
            }
        }

        private void checkBox_Pulse_Body_Femoral_Right_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Femoral_Right_CheckChanged();
        }

        private void checkBox_Pulse_Body_Femoral_Right_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Femoral_Right_CheckChanged();
        }

        private void checkBox_Pulse_Body_Femoral_Right_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_Body_Femoral_Right.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_Body_Femoral_Right.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_Body_Femoral_Right.SelectedIndex = 0;
            }
            _signs.Pulse.BodyPulse.FemoralRight.Status = status;

            Send_Pulse_Body(_signs.Pulse.BodyPulse);
        }

        private void comboBox_Pulse_Body_Femoral_Right_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.BodyPulse.FemoralRight.Value = value;

                Send_Pulse_Body(_signs.Pulse.BodyPulse);
            }
        }

        private void checkBox_Pulse_Body_Femoral_Left_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Femoral_Left_CheckChanged();
        }

        private void checkBox_Pulse_Body_Femoral_Left_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_Body_Femoral_Left_CheckChanged();
        }

        private void checkBox_Pulse_Body_Femoral_Left_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_Body_Femoral_Left.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_Body_Femoral_Left.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_Body_Femoral_Left.SelectedIndex = 0;
            }
            _signs.Pulse.BodyPulse.FemoralLeft.Status = status;

            Send_Pulse_Body(_signs.Pulse.BodyPulse);
        }

        private void comboBox_Pulse_Body_Femoral_Left_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.BodyPulse.FemoralLeft.Value = value;

                Send_Pulse_Body(_signs.Pulse.BodyPulse);
            }
        }

        private void Send_Pulse_Body(Modle.BodyPulse BodyPulse)
        {
            byte[] dataBytes = Resolve.Pulse.SetDataBytes_Pulse_BodyPulse(BodyPulse);

            SendBytes(dataBytes);
        }

        #endregion

        #region 左臂 Left Arm
        private void checkBox_Pulse_LeftArm_Brachial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftArm_Brachial_CheckChanged();
        }

        private void checkBox_Pulse_LeftArm_Brachial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftArm_Brachial_CheckChanged();
        }

        private void checkBox_Pulse_LeftArm_Brachial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_LeftArm_Brachial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_LeftArm_Brachial.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_LeftArm_Brachial.SelectedIndex = 0;
            }
            _signs.Pulse.LeftArmPulse.Brachial.Status = status;

            Send_Pulse_LeftArm(_signs.Pulse.LeftArmPulse);
        }

        private void comboBox_Pulse_LeftArm_Brachial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.LeftArmPulse.Brachial.Value = value;

                Send_Pulse_LeftArm(_signs.Pulse.LeftArmPulse);
            }
        }
        
        private void checkBox_Pulse_LeftArm_Radial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftArm_Radial_CheckChanged();
        }

        private void checkBox_Pulse_LeftArm_Radial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftArm_Radial_CheckChanged();
        }

        private void checkBox_Pulse_LeftArm_Radial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_LeftArm_Radial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_LeftArm_Radial.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_LeftArm_Radial.SelectedIndex = 0;
            }
            _signs.Pulse.LeftArmPulse.Radial.Status = status;

            Send_Pulse_LeftArm(_signs.Pulse.LeftArmPulse);
        }

        private void comboBox_Pulse_LeftArm_Radial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.LeftArmPulse.Radial.Value = value;

                Send_Pulse_LeftArm(_signs.Pulse.LeftArmPulse);
            }
        }

        private void Send_Pulse_LeftArm(Modle.LeftArmPulse LeftArmPulse)
        {
            byte[] dataBytes = Resolve.Pulse.SetDataBytes_Pulse_LeftArmPulse(LeftArmPulse);

            SendBytes(dataBytes);
        }

        #endregion

        #region 右腿 Right Leg

        private void checkBox_Pulse_RightLeg_Popliteal_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightLeg_Popliteal_CheckChanged();
        }

        private void checkBox_Pulse_RightLeg_Popliteal_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightLeg_Popliteal_CheckChanged();
        }

        private void checkBox_Pulse_RightLeg_Popliteal_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_RightLeg_Popliteal.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_RightLeg_Popliteal.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_RightLeg_Popliteal.SelectedIndex = 0;
            }
            _signs.Pulse.RightLegPulse.Popliteal.Status = status;

            Send_Pulse_RightLeg(_signs.Pulse.RightLegPulse);
        }

        private void comboBox_Pulse_RightLeg_Popliteal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.RightLegPulse.Popliteal.Value = value;

                Send_Pulse_RightLeg(_signs.Pulse.RightLegPulse);
            }
        }

        private void checkBox_Pulse_RightLeg_DorsalisPedis_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightLeg_DorsalisPedis_CheckChanged();
        }

        private void checkBox_Pulse_RightLeg_DorsalisPedis_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightLeg_DorsalisPedis_CheckChanged();
        }

        private void checkBox_Pulse_RightLeg_DorsalisPedis_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_RightLeg_DorsalisPedis.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_RightLeg_DorsalisPedis.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_RightLeg_DorsalisPedis.SelectedIndex = 0;
            }
            _signs.Pulse.RightLegPulse.DorsalisPedis.Status = status;

            Send_Pulse_RightLeg(_signs.Pulse.RightLegPulse);
        }

        private void comboBox_Pulse_RightLeg_DorsalisPedis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.RightLegPulse.DorsalisPedis.Value = value;

                Send_Pulse_RightLeg(_signs.Pulse.RightLegPulse);
            }
        }

        private void checkBox_Pulse_RightLeg_Heel_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightLeg_Heel_CheckChanged();
        }

        private void checkBox_Pulse_RightLeg_Heel_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_RightLeg_Heel_CheckChanged();
        }

        private void checkBox_Pulse_RightLeg_Heel_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_RightLeg_Heel.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_RightLeg_Heel.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_RightLeg_Heel.SelectedIndex = 0;
            }
            _signs.Pulse.RightLegPulse.Heel.Status = status;

            Send_Pulse_RightLeg(_signs.Pulse.RightLegPulse);
        }

        private void comboBox_Pulse_RightLeg_Heel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.RightLegPulse.Heel.Value = value;

                Send_Pulse_RightLeg(_signs.Pulse.RightLegPulse);
            }
        }

        private void Send_Pulse_RightLeg(Modle.RightLegPulse RightLegPulse)
        {
            byte[] dataBytes = Resolve.Pulse.SetDataBytes_Pulse_RightLegPulse(RightLegPulse);

            SendBytes(dataBytes);
        }

        #endregion

        #region 左腿 Left Leg

        private void checkBox_Pulse_LeftLeg_Popliteal_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftLeg_Popliteal_CheckChanged();
        }

        private void checkBox_Pulse_LeftLeg_Popliteal_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftLeg_Popliteal_CheckChanged();
        }

        private void checkBox_Pulse_LeftLeg_Popliteal_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_LeftLeg_Popliteal.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_LeftLeg_Popliteal.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_LeftLeg_Popliteal.SelectedIndex = 0;
            }
            _signs.Pulse.LeftLegPulse.Popliteal.Status = status;

            Send_Pulse_LeftLeg(_signs.Pulse.LeftLegPulse);
        }

        private void comboBox_Pulse_LeftLeg_Popliteal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.LeftLegPulse.Popliteal.Value = value;

                Send_Pulse_LeftLeg(_signs.Pulse.LeftLegPulse);
            }
        }

        private void checkBox_Pulse_LeftLeg_DorsalisPedis_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftLeg_DorsalisPedis_CheckChanged();
        }

        private void checkBox_Pulse_LeftLeg_DorsalisPedis_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftLeg_DorsalisPedis_CheckChanged();
        }

        private void checkBox_Pulse_LeftLeg_DorsalisPedis_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_LeftLeg_DorsalisPedis.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_LeftLeg_DorsalisPedis.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_LeftLeg_DorsalisPedis.SelectedIndex = 0;
            }
            _signs.Pulse.LeftLegPulse.DorsalisPedis.Status = status;

            Send_Pulse_LeftLeg(_signs.Pulse.LeftLegPulse);
        }

        private void comboBox_Pulse_LeftLeg_DorsalisPedis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.LeftLegPulse.DorsalisPedis.Value = value;

                Send_Pulse_LeftLeg(_signs.Pulse.LeftLegPulse);
            }
        }

        private void checkBox_Pulse_LeftLeg_Heel_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftLeg_Heel_CheckChanged();
        }

        private void checkBox_Pulse_LeftLeg_Heel_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Pulse_LeftLeg_Heel_CheckChanged();
        }

        private void checkBox_Pulse_LeftLeg_Heel_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Pulse_LeftLeg_Heel.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Pulse_LeftLeg_Heel.SelectedIndex = 2;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Pulse_LeftLeg_Heel.SelectedIndex = 0;
            }
            _signs.Pulse.LeftLegPulse.Heel.Status = status;

            Send_Pulse_LeftLeg(_signs.Pulse.LeftLegPulse);
        }

        private void comboBox_Pulse_LeftLeg_Heel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Pulse.LeftLegPulse.Heel.Value = value;

                Send_Pulse_LeftLeg(_signs.Pulse.LeftLegPulse);
            }
        }

        private void Send_Pulse_LeftLeg(Modle.LeftLegPulse LeftLegPulse)
        {
            byte[] dataBytes = Resolve.Pulse.SetDataBytes_Pulse_LeftLegPulse(LeftLegPulse);

            SendBytes(dataBytes);
        }

        #endregion

        #endregion

        #region 分泌物 Secretions

        #region 出汗 Sweat
        private void checkBox_Secretions_Sweat_Checked(object sender, RoutedEventArgs e)
        {
            Secretions_Sweat_CheckChanged();
        }

        private void checkBox_Secretions_Sweat_Unchecked(object sender, RoutedEventArgs e)
        {
            Secretions_Sweat_CheckChanged();
        }

        private void Secretions_Sweat_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Secretions_Sweat.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.Secretions.Sweat.Status = status;

            SendSecretionsSweat(_signs.Secretions.Sweat);
        }

        private void SendSecretionsSweat(Modle.Controller Sweat)
        {
            byte[] dataBytes = Resolve.Secretions.SetDataBytes_Secretions_Sweat(Sweat);

            SendBytes(dataBytes);
        }

        #endregion

        #region 眼睛 Eyes
        private void checkBox_Secretions_Eyes_Checked(object sender, RoutedEventArgs e)
        {
            Secretions_Eyes_CheckChanged();
        }

        private void checkBox_Secretions_Eyes_Unchecked(object sender, RoutedEventArgs e)
        {
            Secretions_Eyes_CheckChanged();
        }

        private void Secretions_Eyes_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Secretions_Eyes.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.Secretions.Eyes.Status = status;

            SendSecretionsEyes(_signs.Secretions.Eyes);
        }

        private void SendSecretionsEyes(Modle.Controller Eyes)
        {
            byte[] dataBytes = Resolve.Secretions.SetDataBytes_Secretions_Eyes(Eyes);

            SendBytes(dataBytes);
        }

        #endregion

        #region 口 Mouth

        private void checkBox_Secretions_Mouth_Checked(object sender, RoutedEventArgs e)
        {
            Secretions_Mouth_CheckChanged();
        }

        private void checkBox_Secretions_Mouth_Unchecked(object sender, RoutedEventArgs e)
        {
            Secretions_Mouth_CheckChanged();
        }
        private void Secretions_Mouth_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Secretions_Mouth.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.Secretions.Mouth.Status = status;

            SendSecretionsMouth(_signs.Secretions.Mouth);
        }

        private void SendSecretionsMouth(Modle.Controller Mouth)
        {
            byte[] dataBytes = Resolve.Secretions.SetDataBytes_Secretions_Mouth(Mouth);

            SendBytes(dataBytes);
        }

        #endregion

        #region 耳 Ears

        private void checkBox_Secretions_Ears_Checked(object sender, RoutedEventArgs e)
        {
            Secretions_Ears_CheckChanged();
        }

        private void checkBox_Secretions_Ears_Unchecked(object sender, RoutedEventArgs e)
        {
            Secretions_Ears_CheckChanged();
        }

        private void Secretions_Ears_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Secretions_Ears.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.Secretions.Ears.Status = status;

            SendSecretionsEars(_signs.Secretions.Ears);
        }

        private void SendSecretionsEars(Modle.Controller Ears)
        {
            byte[] dataBytes = Resolve.Secretions.SetDataBytes_Secretions_Ears(Ears);

            SendBytes(dataBytes);
        }

        #endregion

        #region 鼻 Nose
        private void checkBox_Secretions_Nose_Checked(object sender, RoutedEventArgs e)
        {
            Secretions_Nose_CheckChanged();
        }

        private void checkBox_Secretions_Nose_Unchecked(object sender, RoutedEventArgs e)
        {
            Secretions_Nose_CheckChanged();
        }

        private void Secretions_Nose_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Secretions_Nose.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.Secretions.Nose.Status = status;

            SendSecretionsNose(_signs.Secretions.Nose);
        }

        private void SendSecretionsNose(Modle.Controller Nose)
        {
            byte[] dataBytes = Resolve.Secretions.SetDataBytes_Secretions_Nose(Nose);

            SendBytes(dataBytes);
        }

        #endregion

        #region 白沫 Froth

        private void checkBox_Secretions_Froth_Checked(object sender, RoutedEventArgs e)
        {
            Secretions_Froth_CheckChanged();
        }

        private void checkBox_Secretions_Froth_Unchecked(object sender, RoutedEventArgs e)
        {
            Secretions_Froth_CheckChanged();
        }

        private void Secretions_Froth_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Secretions_Froth.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

            }
            else
            {
                status = Modle.ControllerStatus.No;

            }
            _signs.Secretions.Froth.Status = status;

            SendSecretionsFroth(_signs.Secretions.Froth);
        }

        private void SendSecretionsFroth(Modle.Controller Froth)
        {
            byte[] dataBytes = Resolve.Secretions.SetDataBytes_Secretions_Froth(Froth);

            SendBytes(dataBytes);
        }



        #endregion

        #endregion
        
        #region 出血 Haemorrhage

        #region 右上  Right Upper

        private void checkBox_Haemorrhage_RightUpper_Arterial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightUpper_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightUpper_Arterial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightUpper_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightUpper_Arterial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_RightUpper_Arterial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_RightUpper_Arterial.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_RightUpper_Arterial.SelectedIndex = 0;
            }
            _signs.Haemorrhage.RightUpper.Arterial.Status = status;

            SendHaemorrhageRightUpper(_signs.Haemorrhage.RightUpper);
        }

        private void comboBox_Haemorrhage_RightUpper_Arterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.RightUpper.Arterial.Value = value;

                SendHaemorrhageRightUpper(_signs.Haemorrhage.RightUpper);
            }
        }

        private void checkBox_Haemorrhage_RightUpper_Venous_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightUpper_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightUpper_Venous_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightUpper_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightUpper_Venous_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_RightUpper_Venous.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_RightUpper_Venous.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_RightUpper_Venous.SelectedIndex = 0;
            }
            _signs.Haemorrhage.RightUpper.Venous.Status = status;

            SendHaemorrhageRightUpper(_signs.Haemorrhage.RightUpper);
        }

        private void comboBox_Haemorrhage_RightUpper_Venous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.RightUpper.Venous.Value = value;

                SendHaemorrhageRightUpper(_signs.Haemorrhage.RightUpper);
            }
        }

        private void SendHaemorrhageRightUpper(Modle.BloodVessel RightUpper)
        {
            byte[] dataBytes = Resolve.Haemorrhage.SetDataBytes_Haemorrhage_RightUpper(RightUpper);

            SendBytes(dataBytes);
        }

        #endregion

        #region 左上  Left Upper

        private void checkBox_Haemorrhage_LeftUpper_Arterial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftUpper_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftUpper_Arterial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftUpper_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftUpper_Arterial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_LeftUpper_Arterial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_LeftUpper_Arterial.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_LeftUpper_Arterial.SelectedIndex = 0;
            }
            _signs.Haemorrhage.LeftUpper.Arterial.Status = status;

            SendHaemorrhageLeftUpper(_signs.Haemorrhage.LeftUpper);
        }

        private void comboBox_Haemorrhage_LeftUpper_Arterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.LeftUpper.Arterial.Value = value;

                SendHaemorrhageLeftUpper(_signs.Haemorrhage.LeftUpper);
            }
        }

        private void checkBox_Haemorrhage_LeftUpper_Venous_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftUpper_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftUpper_Venous_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftUpper_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftUpper_Venous_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_LeftUpper_Venous.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_LeftUpper_Venous.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_LeftUpper_Venous.SelectedIndex = 0;
            }
            _signs.Haemorrhage.LeftUpper.Venous.Status = status;

            SendHaemorrhageLeftUpper(_signs.Haemorrhage.LeftUpper);
        }

        private void comboBox_Haemorrhage_LeftUpper_Venous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.LeftUpper.Venous.Value = value;

                SendHaemorrhageLeftUpper(_signs.Haemorrhage.LeftUpper);
            }
        }

        private void SendHaemorrhageLeftUpper(Modle.BloodVessel LeftUpper)
        {
            byte[] dataBytes = Resolve.Haemorrhage.SetDataBytes_Haemorrhage_LeftUpper(LeftUpper);

            SendBytes(dataBytes);
        }

        #endregion

        #region 右下 Right Lower

        private void checkBox_Haemorrhage_RightLower_Arterial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightLower_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightLower_Arterial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightLower_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightLower_Arterial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_RightLower_Arterial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_RightLower_Arterial.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_RightLower_Arterial.SelectedIndex = 0;
            }
            _signs.Haemorrhage.RightLower.Arterial.Status = status;

            SendHaemorrhageRightLower(_signs.Haemorrhage.RightLower);
        }

        private void comboBox_Haemorrhage_RightLower_Arterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.RightLower.Arterial.Value = value;

                SendHaemorrhageRightLower(_signs.Haemorrhage.RightLower);
            }
        }

        private void checkBox_Haemorrhage_RightLower_Venous_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightLower_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightLower_Venous_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_RightLower_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_RightLower_Venous_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_RightLower_Venous.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_RightLower_Venous.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_RightLower_Venous.SelectedIndex = 0;
            }
            _signs.Haemorrhage.RightLower.Venous.Status = status;

            SendHaemorrhageRightLower(_signs.Haemorrhage.RightLower);
        }

        private void comboBox_Haemorrhage_RightLower_Venous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.RightLower.Venous.Value = value;

                SendHaemorrhageRightLower(_signs.Haemorrhage.RightLower);
            }
        }

        private void SendHaemorrhageRightLower(Modle.BloodVessel RightLower)
        {
            byte[] dataBytes = Resolve.Haemorrhage.SetDataBytes_Haemorrhage_RightLower(RightLower);

            SendBytes(dataBytes);
        }

        #endregion

        #region 左下 Left Lower

        private void checkBox_Haemorrhage_LeftLower_Arterial_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftLower_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftLower_Arterial_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftLower_Arterial_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftLower_Arterial_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_LeftLower_Arterial.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_LeftLower_Arterial.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_LeftLower_Arterial.SelectedIndex = 0;
            }
            _signs.Haemorrhage.LeftLower.Arterial.Status = status;

            SendHaemorrhageLeftLower(_signs.Haemorrhage.LeftLower);
        }

        private void comboBox_Haemorrhage_LeftLower_Arterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.LeftLower.Arterial.Value = value;

                SendHaemorrhageLeftLower(_signs.Haemorrhage.LeftLower);
            }
        }

        private void checkBox_Haemorrhage_LeftLower_Venous_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftLower_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftLower_Venous_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Haemorrhage_LeftLower_Venous_CheckChanged();
        }

        private void checkBox_Haemorrhage_LeftLower_Venous_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Haemorrhage_LeftLower_Venous.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Haemorrhage_LeftLower_Venous.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Haemorrhage_LeftLower_Venous.SelectedIndex = 0;
            }
            _signs.Haemorrhage.LeftLower.Venous.Status = status;

            SendHaemorrhageLeftLower(_signs.Haemorrhage.LeftLower);
        }

        private void comboBox_Haemorrhage_LeftLower_Venous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Haemorrhage.LeftLower.Venous.Value = value;

                SendHaemorrhageLeftLower(_signs.Haemorrhage.LeftLower);
            }
        }

        private void SendHaemorrhageLeftLower(Modle.BloodVessel LeftLower)
        {
            byte[] dataBytes = Resolve.Haemorrhage.SetDataBytes_Haemorrhage_LeftLower(LeftLower);

            SendBytes(dataBytes);
        }

        #endregion;

        #endregion

        #region 导尿 Catheterization
        private void checkBox_Catheterization_Urine_Urinate_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_Catheterization_Urine_Urinate_CheckChanged();
        }

        private void checkBox_Catheterization_Urine_Urinate_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_Catheterization_Urine_Urinate_CheckChanged();
        }

        private void checkBox_Catheterization_Urine_Urinate_CheckChanged()
        {
            Modle.ControllerStatus status;

            if (checkBox_Catheterization_Urine_Urinate.IsChecked == true)
            {
                status = Modle.ControllerStatus.Yes;

                comboBox_Catheterization_Urine_UPD.SelectedIndex = 1;
            }
            else
            {
                status = Modle.ControllerStatus.No;

                comboBox_Catheterization_Urine_UPD.SelectedIndex = 0;
            }
            _signs.Urine.Urinate.Status = status;

            SendCatheterizationUrine(_signs.Urine);
        }

        private void comboBox_Catheterization_Urine_UPD_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox != null)
            {
                int value = int.Parse(((Library.ListItem)comboBox.SelectedItem).Value.ToString());

                _signs.Urine.Urinate.Value = value;

                SendCatheterizationUrine(_signs.Urine);
            }
        }

        private void SendCatheterizationUrine(Modle.Urine Urine)
        {
            byte[] dataBytes = Resolve.Catheterization.SetDataBytes_Urine_Urinate(Urine);

            SendBytes(dataBytes);
        }

        #endregion

        #region  药物治疗

        private void comboBox_Medication_DrugDelivery_Drug_DrugCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string DrugCategory = "";
            if (comboBox_Medication_DrugDelivery_Drug_DrugCategory.SelectedItem != null)
                DrugCategory = ((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_DrugCategory.SelectedItem).Value;
            BindData_Medication_Drug_DrugName(DrugCategory);
        }

        private void comboBox_Medication_DrugDelivery_Drug_DrugName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string DrugName = "";
            if (comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedItem != null)
                DrugName = ((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedItem).Value;
            string DrugRoute = "";
            if (comboBox_Medication_DrugDelivery_Drug_Route.SelectedItem != null)
                DrugRoute = ((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_Route.SelectedItem).Value;
            BindData_Medication_Drug_DoseUnit(DrugName, DrugRoute);
        }

        private void comboBox_Medication_DrugDelivery_Drug_Route_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string DrugName = "";
            if (comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedItem != null)
                DrugName = ((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedItem).Value;
            string DrugRoute = "";
            if (comboBox_Medication_DrugDelivery_Drug_Route.SelectedItem != null)
                DrugRoute = ((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_Route.SelectedItem).Value;
            BindData_Medication_Drug_DoseUnit(DrugName, DrugRoute);
        }

        private void button_Medication_DrugDelivery_Click(object sender, RoutedEventArgs e)
        {

            int DrugID = 0;
            if (comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedItem != null)
                DrugID = int.Parse(((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_DrugName.SelectedItem).Value);
            _signs.DrugDelivery.Drug.DrugID = DrugID;

            int RouteID = 0;
            if (comboBox_Medication_DrugDelivery_Drug_Route.SelectedItem != null)
                RouteID = int.Parse(((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_Route.SelectedItem).Value);
            _signs.DrugDelivery.Drug.Route = (Modle.Route)RouteID;

            int Dose = 0;
            if (textBox_Medication_DrugDelivery_Dose.Text.Trim()!="")
                Dose = int.Parse(textBox_Medication_DrugDelivery_Dose.Text);
            _signs.DrugDelivery.Dose.Value = Dose;

            int DoseUnitID = 0;
            if (comboBox_Medication_DrugDelivery_Drug_DoseUnit.SelectedItem != null)
                DoseUnitID = int.Parse(((Library.ListItem)comboBox_Medication_DrugDelivery_Drug_DoseUnit.SelectedItem).Value);
            _signs.DrugDelivery.Dose.Unit = DoseUnitID;


            Send_DrugDelivery(_signs.DrugDelivery);
        }

        private void Send_DrugDelivery(Modle.DrugDelivery DrugDelivery)
        {
            Send_DrugDelivery_Drug(DrugDelivery.Drug);
            Send_DrugDelivery_Dose(DrugDelivery.Dose);
        }

        private void Send_DrugDelivery_Drug(Modle.Drug Drug)
        {
            byte[] dataBytes = Resolve.Medication.SetDataBytes_DrugDelivery_Drug(Drug);

            SendBytes(dataBytes);
        }

        private void Send_DrugDelivery_Dose(Modle.DataValue Dose)
        {
            byte[] dataBytes = Resolve.Medication.SetDataBytes_DrugDelivery_Dose(Dose);

            SendBytes(dataBytes);
        }

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


    }
}
