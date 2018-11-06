using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using DataDictionary = YH.Simulator.Framework.DataDictionary;
using System.Windows.Threading;
using Library = YH.Library;

namespace YH.Virtual_Simulator_Engine
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        bool IsMinSized = false;

        Modle.Mould _mould;

        //生命体征
        Modle.Signs _signs;

        //治疗、处理
        Modle.Treatment _treatment;

        Library.Launch launch1;

        private Hashtable m_ChannelSock;
        private NetServer m_NetServer;

        public MainWindow()
        {
            InitializeComponent();

            _signs = Modle.Signs.Default;

            _treatment = Modle.Treatment.Default;

            NetInitializeComponent();

            launch1 = new Library.Launch(1000);
            launch1.OnElapsed += Launch1_OnElapsed;
            //launch1.CreateWork();
            launch1.Start();

        }

        private void Launch1_OnElapsed()
        {
            //throw new NotImplementedException();
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                if (_mould.PatientMonitorON>0)
                {
                    Send_VitalSigns(_signs.VitalSigns);
                }
            }
            );
        }

        private void NetInitializeComponent()
        {
            m_ChannelSock = new Hashtable();
            m_NetServer = new NetServer(6500, 1024, new Coder(Coder.EncodingMothord.Default));

            //
            //m_NetServer.Resovlver = new DatagramResolver("]}");

            m_NetServer.Resovlver = new Network.Framework.DatagramResolver(new byte[4] { 0xfa, 0xfb, 0xfc, 0xfd });

            //处理客户端连接数已满事件
            m_NetServer.ServerFull += M_NetServer_ServerFull;
            //处理新客户端连接事件
            m_NetServer.ClientConnect += M_NetServer_ClientConnect;
            //处理客户端关闭事件
            m_NetServer.ClientClose += M_NetServer_ClientClose;
            //处理接收到数据事件
            m_NetServer.RecvData += M_NetServer_RecvData;

            //
            m_NetServer.Start();
        }

        private void M_NetServer_RecvData(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            //string info;

            //info = "Received data: " + e.ClientSession.Datagram + " From: " + e.ClientSession;            

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                Modle.Function Function = Resolve.Resolve.SetData_Signs(e.ClientSession.RecvDataBuffer, ref _signs);

                ShowData(Function, _signs);
            });
        }

        private void M_NetServer_ClientClose(object sender, NetEventArgs e)
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
                info = string.Format("A Client Session:{0} Normal Closed.", e.ClientSession.ID);
            }

            if (e.ClientSession.Channel != "")
            {
                this.m_ChannelSock.Remove(e.ClientSession.Channel);
            }

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                _mould.Link = m_NetServer.ClientCount;

            });
        }

        private void M_NetServer_ClientConnect(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info = string.Format("A Client:{0} connect server Session:{1}. Socket Handle:{2}",
                e.ClientSession.ClientSocket.RemoteEndPoint.ToString(),
                e.ClientSession.ID, e.ClientSession.ClientSocket.Handle);
            
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                //e.ClientSession.Channel = "A";

                //if (e.ClientSession.Channel != "")
                //{
                //    this.m_ChannelSock.Add(0,e.ClientSession.Channel);
                //}
                _mould.Link = m_NetServer.ClientCount;

            });
        }

        private void M_NetServer_ServerFull(object sender, NetEventArgs e)
        {
            //throw new NotImplementedException();
            string info = string.Format("Server is full. The Client:{0} is refused",
            e.ClientSession.ClientSocket.RemoteEndPoint.ToString());

            //Must do it
            e.ClientSession.Close();

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (System.Threading.ThreadStart)delegate ()
            {
                _mould.Link = m_NetServer.ClientCount;

            });
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void MyInitialize()
        {
            MyInitialize_Mould();

            MyInitialize_Signs();

            MyInitialize_Treatment();
        }

        private void MyInitialize_Mould()
        {
            _mould.Identifier = 0;
            _mould.Attribute.PatientType = 1;          //成人
            _mould.Attribute.PN = "Blaine YH-J181000";    //MD
            _mould.Attribute.SN = "18120001";   //18【年】12【月】0001【月序号】
            _mould.LanguageCode = 1;            //中文
            _mould.PatientMonitorON = 1;        //默认开
            _mould.DefibrillationCalibration = 0;
            _mould.BloodPressureCalibration = 0;
            _mould.Link = 0;

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
            _signs.AnalogVocal.Vocal.PlaybackMode = 0;
            _signs.AnalogVocal.Vocal.SoundVolume = Modle.SoundVolume.V5;
            //语音 Voice
            _signs.AnalogVocal.Voice.SoundID = 0;
            _signs.AnalogVocal.Voice.PlaybackMode = 0;
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
            _signs.Airway.Compliance.Left.Status = _signs.Airway.Compliance.Right.Status = Modle.ControllerStatus.Yes;
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
            _signs.Airway.AutonomousRespiration.Left.Status = _signs.Airway.AutonomousRespiration.Right.Status = Modle.ControllerStatus.Yes;
            _signs.Airway.AutonomousRespiration.Left.Value = _signs.Airway.AutonomousRespiration.Left.Value = 0;

            //生命体征 VitalSigns
            //循环 Cyclic
            _signs.VitalSigns.Cyclic.Rhythm.Basic = 1;
            _signs.VitalSigns.Cyclic.Rhythm.Extrasystole = 0;
            _signs.VitalSigns.Cyclic.HeartRate.Value = 1;
            _signs.VitalSigns.Cyclic.SpO2.Value = 98;
            _signs.VitalSigns.Cyclic.IBP = Modle.IBP.Default;
            _signs.VitalSigns.Cyclic.PAP = Modle.PAP.Default;
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
            _signs.Stethoscopy.HeartSounds.M.SoundID = 0;
            _signs.Stethoscopy.HeartSounds.M.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.T.SoundID = 0;
            _signs.Stethoscopy.HeartSounds.T.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.A.SoundID = 0;
            _signs.Stethoscopy.HeartSounds.A.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.P.SoundID = 0;
            _signs.Stethoscopy.HeartSounds.P.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.HeartSounds.E.SoundID = 0;
            _signs.Stethoscopy.HeartSounds.E.SoundVolume = Modle.SoundVolume.V5;
            //肺音 LungSounds
            //前
            _signs.Stethoscopy.LungSounds.ARUL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.ARUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ARML.SoundID = 0;
            _signs.Stethoscopy.LungSounds.ARML.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ARLL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.ARLL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ALUL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.ALUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.ALLL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.ALLL.SoundVolume = Modle.SoundVolume.V5;
            //后背
            _signs.Stethoscopy.LungSounds.PLUL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.PLUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.PLLL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.PLLL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.PRUL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.PRUL.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.LungSounds.PRLL.SoundID = 0;
            _signs.Stethoscopy.LungSounds.PRLL.SoundVolume = Modle.SoundVolume.V5;
            //腹部 AbdomenSounds
            _signs.Stethoscopy.AbdomenSounds.Vascular.SoundID = 0;
            _signs.Stethoscopy.AbdomenSounds.Vascular.SoundVolume = Modle.SoundVolume.V5;
            _signs.Stethoscopy.AbdomenSounds.Bowel.SoundID = 0;
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

        /// <summary>
        /// 
        /// </summary>
        private void MyInitialize_Treatment()
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

        private void Resolve_Signs()
        {
            Resolve_Signs_Eyes();
        }

        private void Resolve_Signs_Eyes()
        {
            byte[] dataBytes_Eyelid = Resolve.Eyes.SetDataBytes_Eyes_Eyelid(_signs.Eyes.Eyelid);
            byte[] dataBytes_Blinking = Resolve.Eyes.SetDataBytes_Eyes_Blinking(_signs.Eyes.Blinking);
            byte[] dataBytes_Pupill = Resolve.Eyes.SetDataBytes_Eyes_Pupill(_signs.Eyes.Pupill);
            byte[] dataBytes_Light = Resolve.Eyes.SetDataBytes_Eyes_Light(_signs.Eyes.Light);
        }

        /// <summary>
        /// 操作数据处理
        /// </summary>
        /// <param name="dataBytes"></param>
        private void Resolve_Treatment(byte[] dataBytes)
        {
            Resolve.Resolve.GetData_Treatment(dataBytes, ref _treatment);
        }
        /// <summary>
        /// 鼠标双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NotificationAreaIcon_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                this.Visibility = System.Windows.Visibility.Visible;
                this.Activate();
                //这里是最重要的，激活窗体后延迟窗体状态变为以前的状态，才有弹出的效果，否则没有弹出的效果
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.WindowState = System.Windows.WindowState.Normal;
                }));
            }
        }

        /// <summary>
        /// 点击图标展开程序
        /// </summary>
        private void OpenFromIcon()
        {

        }

        private void HideToIcon()
        {
            if (this.WindowState != WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
                this.ShowInTaskbar = false;
                this.WindowState = WindowState.Minimized;
            }
        }
        /// <summary>
        /// 关于软件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("开发单位：*************************\r\n软件版本：1.0.10\r\n最后更新：2013-07-01", "关于软件");
        }

        /// <summary>
        /// 打开程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, EventArgs e)
        {
            if (IsMinSized)
            {

            }
        }


        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_2(object sender, EventArgs e)
        {

            launch1.Stop();

            launch1 = null;

            this.Close();
        }


        /// <summary>
        /// 窗口尺寸变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Minimized) //最小化的时候托盘操作
            {
                //winform中是this.Hide();
                //this.Visibility = System.Windows.Visibility.Hidden;
                //this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = WindowState.Minimized;
            this.ShowInTaskbar = false;
        }

        private void NotificationAreaIcon_MouseClick(object sender, MouseButtonEventArgs e)
        {
            //if (e.LeftButton == MouseButtonState.Released)
            //{
            //    this.Visibility = System.Windows.Visibility.Visible;
            //    this.Activate();
            //    //这里是最重要的，激活窗体后延迟窗体状态变为以前的状态，才有弹出的效果，否则没有弹出的效果
            //    this.Dispatcher.Invoke(new Action(() =>
            //    {
            //        this.WindowState = System.Windows.WindowState.Normal;
            //    }));
            //}

            if (this.WindowState == System.Windows.WindowState.Minimized)
                this.WindowState = System.Windows.WindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
            this.Activate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindData();

            MyInitialize();

            ShowData(0, _signs);
        }

        #region 数据绑定

        private void BindData()
        {
            BindData_VitalSigns();
            BindData_Defibrillation();
            BindData_Pacing();
            BindData_Medication();
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

        #region 除颤

        private void BindData_Defibrillation()
        {
            BindData_Defibrillation_Energy();

        }

        private void BindData_Defibrillation_Energy()
        {
            //throw new NotImplementedException();

            comboBox_Defibrillation_Energy.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Defibrillation_Energy.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Defibrillation_Energy.Count; i++)
            {
                comboBox_Defibrillation_Energy.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Defibrillation_Energy[test[i]][0], ""));

            }

            comboBox_Defibrillation_Energy.SelectedIndex = 0;
        }

        #endregion

        #region 起搏

        private void BindData_Pacing()
        {
            BindData_Pacing_Current();

        }

        private void BindData_Pacing_Current()
        {
            //throw new NotImplementedException();

            comboBox_Pacing_Current.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.Pacing_Current.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.Pacing_Current.Count; i++)
            {
                comboBox_Pacing_Current.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.Pacing_Current[test[i]][0], ""));

            }

            comboBox_Pacing_Current.SelectedIndex = 0;
        }

        #endregion

        #region 药物治疗
        private void BindData_Medication()
        {
            BindData_Medication_Apply_Drug();

            BindData_Medication_Drug_DoseUnit();
        }

        private void BindData_Medication_Apply_Drug()
        {
            comboBox_Medication_Apply_PO_Drug.Items.Clear();
            comboBox_Medication_Apply_IV_Drug.Items.Clear();
            comboBox_Medication_Apply_IVGtt_Drug.Items.Clear();
            comboBox_Medication_Apply_IM_Drug.Items.Clear();


            List<int> test = new List<int>(DataDictionary.MyDictionary.DrugName.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.DrugName.Count; i++)
            {
                string itemstr = DataDictionary.MyDictionary.DrugName[test[i]][0] + "." + DataDictionary.MyDictionary.DrugName[test[i]][1] + "  " + DataDictionary.MyDictionary.DrugName[test[i]][2] + "." + DataDictionary.MyDictionary.DrugName[test[i]][3];
                comboBox_Medication_Apply_PO_Drug.Items.Add(new Library.ListItem(test[i].ToString(), itemstr  , ""));
                comboBox_Medication_Apply_IV_Drug.Items.Add(new Library.ListItem(test[i].ToString(), itemstr, ""));
                comboBox_Medication_Apply_IVGtt_Drug.Items.Add(new Library.ListItem(test[i].ToString(), itemstr, ""));
                comboBox_Medication_Apply_IM_Drug.Items.Add(new Library.ListItem(test[i].ToString(), itemstr, ""));

            }

            comboBox_Medication_Apply_PO_Drug.SelectedIndex = 0;
            comboBox_Medication_Apply_IV_Drug.SelectedIndex = 0;
            comboBox_Medication_Apply_IVGtt_Drug.SelectedIndex = 0;
            comboBox_Medication_Apply_IM_Drug.SelectedIndex = 0;
        }

        private void BindData_Medication_Drug_DoseUnit()
        {
            //throw new NotImplementedException();
            comboBox_Medication_Apply_PO_DoseUnit.Items.Clear();
            comboBox_Medication_Apply_IV_DoseUnit.Items.Clear();
            comboBox_Medication_Apply_IVGtt_DoseUnit.Items.Clear();
            comboBox_Medication_Apply_IM_DoseUnit.Items.Clear();

            List<int> test = new List<int>(DataDictionary.MyDictionary.DrugDoseUnit.Keys);

            for (int i = 0; i < DataDictionary.MyDictionary.DrugDoseUnit.Count; i++)
            {
                comboBox_Medication_Apply_PO_DoseUnit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugDoseUnit[test[i]][1], ""));
                comboBox_Medication_Apply_IV_DoseUnit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugDoseUnit[test[i]][1], ""));
                comboBox_Medication_Apply_IVGtt_DoseUnit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugDoseUnit[test[i]][1], ""));
                comboBox_Medication_Apply_IM_DoseUnit.Items.Add(new Library.ListItem(test[i].ToString(), DataDictionary.MyDictionary.DrugDoseUnit[test[i]][1], ""));

            }

            comboBox_Medication_Apply_PO_DoseUnit.SelectedIndex = 0;
            comboBox_Medication_Apply_IV_DoseUnit.SelectedIndex = 0;
            comboBox_Medication_Apply_IVGtt_DoseUnit.SelectedIndex = 0;
            comboBox_Medication_Apply_IM_DoseUnit.SelectedIndex = 0;
        }

        #endregion

        #endregion

        private void SendBytes(byte[] dataBytes)
        {
            try
            {
                m_NetServer.SendBytesToAll(dataBytes);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "错误");
            }
        }

        #region 显示 

        private void ShowData(Modle.Function Function, Modle.Signs Signs)
        {
            switch (Function)
            {
                case Modle.Function.Eyes:
                    ShowData_Eyes(Signs.Eyes);
                    break;
                case Modle.Function.Cyanosis:
                    ShowData_Cyanosis(Signs.Cyanosis);
                    break;
                case Modle.Function.AnalogVocal:
                    ShowData_AnalogVocal(Signs.AnalogVocal);
                    break;
                case Modle.Function.Airway:
                    ShowData_Airway(Signs.Airway);
                    break;
                case Modle.Function.VitalSigns:
                    ShowData_VitalSigns(Signs.VitalSigns);
                    break;
                case Modle.Function.Palpation:
                    ShowData_Palpation(Signs.Palpation);
                    break;
                case Modle.Function.Stethoscopy:
                    ShowData_Stethoscopy(Signs.Stethoscopy);
                    break;
                case Modle.Function.BloodPressure:
                    ShowData_BloodPressure(Signs.BloodPressure);
                    break;
                case Modle.Function.Convulsions:
                    ShowData_Convulsions(Signs.Convulsions);
                    break;
                case Modle.Function.Pulse:
                    ShowData_Pulse(Signs.Pulse);
                    break;
                case Modle.Function.Secretions:
                    ShowData_Secretions(Signs.Secretions);
                    break;
                case Modle.Function.Haemorrhage:
                    ShowData_Haemorrhage(Signs.Haemorrhage);
                    break;
                case Modle.Function.Catheterization:
                    ShowData_Catheterization(Signs.Urine);
                    break;
                case Modle.Function.Medication:
                    ShowData_DrugDelivery(Signs.DrugDelivery);
                    break;
                case Modle.Function.ECG:
                    _treatment.ECG = Signs.ECG;
                    Send_ECG_LeadLine(_treatment.ECG.LeadLine);
                    ShowData_ECG(Signs.ECG);
                    break;
                default:

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

                    _treatment.ECG = Signs.ECG;
                    Send_ECG_LeadLine(_treatment.ECG.LeadLine);
                    ShowData_ECG(Signs.ECG);
                    break;
            }

        }

        private string GetString_ControllerStatus(Modle.ControllerStatus ControllerStatus)
        {
            if (ControllerStatus == Simulator.Framework.Modle.ControllerStatus.Yes)
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

        #region  眼睛

        /// <summary>
        /// 眼睛
        /// </summary>
        /// <param name="Eyes"></param>
        private void ShowData_Eyes(Modle.Eyes Eyes)
        {
            ShowData_Eyes_Eyelid(Eyes.Eyelid);
            ShowData_Eyes_Blinking(Eyes.Blinking);
            ShowData_Eyes_Pupill(Eyes.Pupill);
            ShowData_Eyes_Light(Eyes.Light);
        }

        /// <summary>
        /// 眼脸
        /// </summary>
        /// <param name="Eyelid"></param>
        private void ShowData_Eyes_Eyelid(Modle.Eyelid Eyelid)
        {
            label_Eyes_Eyelid_Right.Content = DataDictionary.MyDictionary.Eyes_EyelidStatus[(int)Eyelid.Right];
            label_Eyes_Eyelid_Left.Content = DataDictionary.MyDictionary.Eyes_EyelidStatus[(int)Eyelid.Left];
        }

        /// <summary>
        /// 眨眼
        /// </summary>
        /// <param name="Blinking"></param>
        private void ShowData_Eyes_Blinking(Modle.Blinking Blinking)
        {
            label_Eyes_Blinking_Right.Content = DataDictionary.MyDictionary.Eyes_BlinkingSpeed[(int)Blinking.Right];
            label_Eyes_Blinking_Left.Content = DataDictionary.MyDictionary.Eyes_BlinkingSpeed[(int)Blinking.Left];
        }

        /// <summary>
        /// 瞳孔
        /// </summary>
        /// <param name="Pupill"></param>
        private void ShowData_Eyes_Pupill(Modle.Pupill Pupill)
        {
            label_Eyes_Pupill_Right.Content = DataDictionary.MyDictionary.Eyes_PupillSize[(int)Pupill.Right];
            label_Eyes_Pupill_Left.Content = DataDictionary.MyDictionary.Eyes_PupillSize[(int)Pupill.Left];
        }

        /// <summary>
        /// 对光
        /// </summary>
        /// <param name="Light"></param>
        private void ShowData_Eyes_Light(Modle.Light Light)
        {
            label_Eyes_Light_Right.Content = DataDictionary.MyDictionary.Eyes_LightSensitivity[(int)Light.Right];
            label_Eyes_Light_Left.Content = DataDictionary.MyDictionary.Eyes_LightSensitivity[(int)Light.Left];
        }


        #endregion

        #region  紫绀

        /// <summary>
        /// 眼睛
        /// </summary>
        /// <param name="Cyanosis"></param>
        private void ShowData_Cyanosis(Modle.Cyanosis Cyanosis)
        {
            ShowData_Cyanosis_Lips(Cyanosis.Lips);
            ShowData_Cyanosis_Fingernail(Cyanosis.Fingernail);
            ShowData_Cyanosis_FootNail(Cyanosis.FootNail);
        }

        private void ShowData_Cyanosis_Lips(Modle.Controller Lips)
        {
            //throw new NotImplementedException();
            label_Cyanosis_Lips_Status.Content = GetString_ControllerStatus(Lips.Status);
            label_Cyanosis_Lips_Value.Content = DataDictionary.MyDictionary.Cyanosis_Degree[(int)Lips.Value];
        }

        private void ShowData_Cyanosis_Fingernail(Modle.Controller Fingernail)
        {
            //throw new NotImplementedException();
            label_Cyanosis_Fingernail_Status.Content = GetString_ControllerStatus(Fingernail.Status);
            label_Cyanosis_Fingernail_Value.Content = DataDictionary.MyDictionary.Cyanosis_Degree[(int)Fingernail.Value];
        }

        private void ShowData_Cyanosis_FootNail(Modle.Controller FootNail)
        {
            //throw new NotImplementedException();
            label_Cyanosis_FootNail_Status.Content = GetString_ControllerStatus(FootNail.Status);
            label_Cyanosis_FootNail_Value.Content = DataDictionary.MyDictionary.Cyanosis_Degree[(int)FootNail.Value];
        }

        #endregion

        #region  模拟声音

        /// <summary>
        /// 眼睛
        /// </summary>
        /// <param name="Cyanosis"></param>
        private void ShowData_AnalogVocal(Modle.AnalogVocal AnalogVocal)
        {
            ShowData_AnalogVocal_Vocal(AnalogVocal.Vocal);
            ShowData_AnalogVocal_Voice(AnalogVocal.Voice);
        }

        private void ShowData_AnalogVocal_Vocal(Modle.Vocal Vocal)
        {
            //throw new NotImplementedException();
            label_AnalogVocal_Vocal_SoundID.Content = DataDictionary.MyDictionary.AnalogVocal_Vocal[(int)Vocal.SoundID];
            label_AnalogVocal_Vocal_SoundID_PlaybackMode.Content = Vocal.PlaybackMode.ToString();
            label_AnalogVocal_Vocal_SoundID_SoundVolume.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)Vocal.SoundVolume];
        }

        private void ShowData_AnalogVocal_Voice(Modle.Voice Voice)
        {
            //throw new NotImplementedException();
            label_AnalogVocal_Voice_SoundID.Content = DataDictionary.MyDictionary.AnalogVocal_Voice[(int)Voice.SoundID];
            label_AnalogVocal_Voice_SoundID_PlaybackMode.Content = Voice.PlaybackMode.ToString();
            label_AnalogVocal_Voice_SoundID_SoundVolume.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)Voice.SoundVolume];
        }



        #endregion

        #region 气道 

        private void ShowData_Airway(Modle.Airway Airway)
        {
            ShowData_Airway_TongueEdema(Airway.TongueEdema);
            ShowData_Airway_TongueFallback(Airway.TongueFallback);
            ShowData_Airway_FBAO(Airway.FBAO);
            ShowData_Airway_PharyngealObstruction(Airway.PharyngealObstruction);
            ShowData_Airway_Laryngospasm(Airway.Laryngospasm);
            ShowData_Airway_Trismus(Airway.Trismus);
            ShowData_Airway_NeckAnkylosis(Airway.NeckAnkylosis);
            ShowData_Airway_Resistance(Airway.Resistance);
            ShowData_Airway_Compliance(Airway.Compliance);
            ShowData_Airway_Aerothorax(Airway.Aerothorax);
            ShowData_Airway_AutonomousRespiration(Airway.AutonomousRespiration);
            ShowData_Airway_StomachDistention(Airway.StomachDistention);
            ShowData_Airway_ExhaleCO2(Airway.ExhaleCO2);
        }

        private void ShowData_Airway_TongueEdema(Modle.Controller TongueEdema)
        {
            //throw new NotImplementedException();
            label_Airway_TongueEdema_Status.Content = GetString_ControllerStatus(TongueEdema.Status);
            label_Airway_TongueEdema_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)TongueEdema.Value][1];
        }

        private void ShowData_Airway_TongueFallback(Modle.Controller TongueFallback)
        {
            //throw new NotImplementedException();
            label_Airway_TongueFallback_Status.Content = GetString_ControllerStatus(TongueFallback.Status);
            label_Airway_TongueFallback_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)TongueFallback.Value][1];
        }

        private void ShowData_Airway_FBAO(Modle.Controller FBAO)
        {
            //throw new NotImplementedException();
            label_Airway_FBAO_Status.Content = GetString_ControllerStatus(FBAO.Status);
            label_Airway_FBAO_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)FBAO.Value][1];
        }

        private void ShowData_Airway_PharyngealObstruction(Modle.Controller PharyngealObstruction)
        {
            //throw new NotImplementedException();
            label_Airway_PharyngealObstruction_Status.Content = GetString_ControllerStatus(PharyngealObstruction.Status);
            label_Airway_PharyngealObstruction_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)PharyngealObstruction.Value][1];
        }

        private void ShowData_Airway_Laryngospasm(Modle.Controller Laryngospasm)
        {
            //throw new NotImplementedException();
            label_Airway_Laryngospasm_Status.Content = GetString_ControllerStatus(Laryngospasm.Status);
            label_Airway_Laryngospasm_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Laryngospasm.Value][1];
        }

        private void ShowData_Airway_Trismus(Modle.Controller Trismus)
        {
            //throw new NotImplementedException();
            label_Airway_Trismus_Status.Content = GetString_ControllerStatus(Trismus.Status);
            label_Airway_Trismus_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Trismus.Value][1];
        }

        private void ShowData_Airway_NeckAnkylosis(Modle.Controller NeckAnkylosis)
        {
            //throw new NotImplementedException();
            label_Airway_NeckAnkylosis_Status.Content = GetString_ControllerStatus(NeckAnkylosis.Status);
            label_Airway_NeckAnkylosis_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)NeckAnkylosis.Value][1];
        }

        private void ShowData_Airway_Resistance(Modle.Resistance Resistance)
        {
            //throw new NotImplementedException();
            label_Airway_Resistance_Right_Status.Content = GetString_ControllerStatus(Resistance.Right.Status);
            label_Airway_Resistance_Right_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Resistance.Right.Value][1];
            label_Airway_Resistance_Left_Status.Content = GetString_ControllerStatus(Resistance.Left.Status);
            label_Airway_Resistance_Left_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Resistance.Left.Value][1];
        }

        private void ShowData_Airway_Compliance(Modle.Compliance Compliance)
        {
            //throw new NotImplementedException();
            label_Airway_Compliance_Right_Status.Content = GetString_ControllerStatus(Compliance.Right.Status);
            label_Airway_Compliance_Right_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Compliance.Right.Value][1];
            label_Airway_Compliance_Left_Status.Content = GetString_ControllerStatus(Compliance.Left.Status);
            label_Airway_Compliance_Left_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Compliance.Left.Value][1];
        }

        private void ShowData_Airway_Aerothorax(Modle.Aerothorax Aerothorax)
        {
            //throw new NotImplementedException();
            label_Airway_Aerothorax_Right_Status.Content = GetString_ControllerStatus(Aerothorax.Right.Status);
            label_Airway_Aerothorax_Right_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Aerothorax.Right.Value][1];
            label_Airway_Aerothorax_Left_Status.Content = GetString_ControllerStatus(Aerothorax.Left.Status);
            label_Airway_Aerothorax_Left_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)Aerothorax.Left.Value][1];
        }

        private void ShowData_Airway_AutonomousRespiration(Modle.AutonomousRespiration AutonomousRespiration)
        {
            //throw new NotImplementedException();
            label_Airway_AutonomousRespiration_Right_Status.Content = GetString_ControllerStatus(AutonomousRespiration.Right.Status);
            label_Airway_AutonomousRespiration_Right_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)AutonomousRespiration.Right.Value][1];
            label_Airway_AutonomousRespiration_Left_Status.Content = GetString_ControllerStatus(AutonomousRespiration.Left.Status);
            label_Airway_AutonomousRespiration_Left_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)AutonomousRespiration.Left.Value][1];
        }

        private void ShowData_Airway_StomachDistention(Modle.Controller StomachDistention)
        {
            //throw new NotImplementedException();
            label_Airway_StomachDistention_Status.Content = GetString_ControllerStatus(StomachDistention.Status);
            label_Airway_StomachDistention_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)StomachDistention.Value][1];
        }

        private void ShowData_Airway_ExhaleCO2(Modle.Controller ExhaleCO2)
        {
            //throw new NotImplementedException();
            label_Airway_ExhaleCO2_Status.Content = GetString_ControllerStatus(ExhaleCO2.Status);
            label_Airway_ExhaleCO2_Value.Content = DataDictionary.MyDictionary.Tongue_Edema[(int)ExhaleCO2.Value][1];
        }

        #endregion

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
            textBox_VitalSigns_Other_TOF.Text = Other.TOF.Numerical.Value.ToString();
            textBox_VitalSigns_Other_ICP.Text = Other.ICP.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_ICP_Unit, Other.ICP.Unit.ToString());
            ComboBoxChoose(comboBox_VitalSigns_Other_TOF_Unit, Other.TOF.Numerical.Unit.ToString());
            textBox_VitalSigns_Other_TOFRatio.Text = Other.TOF.Ratio.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_TOFRatio_Unit, Other.TOF.Ratio.Unit.ToString());
            textBox_VitalSigns_Other_PTC.Text = Other.PTC.Value.ToString();
            ComboBoxChoose(comboBox_VitalSigns_Other_PTC_Unit, Other.PTC.Unit.ToString());
        }

        #endregion

        #region 触诊

        private void ShowData_Palpation(Modle.Palpation Palpation)
        {
            label_Palpation_AbdominalTouch_RightUpper_Status.Content = GetString_ControllerStatus(Palpation.Abdominal.RightUpper.Status);
            label_Palpation_AbdominalTouch_LeftUpper_Status.Content = GetString_ControllerStatus(Palpation.Abdominal.LeftUpper.Status);
            label_Palpation_AbdominalTouch_Middle_Status.Content = GetString_ControllerStatus(Palpation.Abdominal.Middle.Status);
            label_Palpation_AbdominalTouch_RightLower_Status.Content = GetString_ControllerStatus(Palpation.Abdominal.RightLower.Status);
            label_Palpation_AbdominalTouch_LeftLower_Status.Content = GetString_ControllerStatus(Palpation.Abdominal.LeftLower.Status);
        }

        #endregion

        #region 听诊

        private void ShowData_Stethoscopy(Modle.Stethoscopy Stethoscopy)
        {
            ShowData_Stethoscopy_HeartSounds(Stethoscopy.HeartSounds);
            ShowData_Stethoscopy_LungSounds(Stethoscopy.LungSounds);
            ShowData_Stethoscopy_AbdomenSounds(Stethoscopy.AbdomenSounds);
        }

        private void ShowData_Stethoscopy_HeartSounds(Modle.HeartSounds HeartSounds)
        {
            //throw new NotImplementedException();
            label_Stethoscopy_HeartSounds_M_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_HeartSounds[(int)HeartSounds.M.SoundID][1];
            label_Stethoscopy_HeartSounds_M_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)HeartSounds.M.SoundVolume];

            label_Stethoscopy_HeartSounds_T_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_HeartSounds[(int)HeartSounds.T.SoundID][1];
            label_Stethoscopy_HeartSounds_T_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)HeartSounds.T.SoundVolume];

            label_Stethoscopy_HeartSounds_A_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_HeartSounds[(int)HeartSounds.A.SoundID][1];
            label_Stethoscopy_HeartSounds_A_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)HeartSounds.A.SoundVolume];

            label_Stethoscopy_HeartSounds_P_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_HeartSounds[(int)HeartSounds.P.SoundID][1];
            label_Stethoscopy_HeartSounds_P_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)HeartSounds.P.SoundVolume];

            label_Stethoscopy_HeartSounds_E_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_HeartSounds[(int)HeartSounds.E.SoundID][1];
            label_Stethoscopy_HeartSounds_E_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)HeartSounds.E.SoundVolume];
        }

        private void ShowData_Stethoscopy_LungSounds(Modle.LungSounds LungSounds)
        {
            //throw new NotImplementedException();

            label_Stethoscopy_LungSounds_ARUL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.ARUL.SoundID][1];
            label_Stethoscopy_LungSounds_ARUL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.ARUL.SoundVolume];

            label_Stethoscopy_LungSounds_ARML_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.ARML.SoundID][1];
            label_Stethoscopy_LungSounds_ARML_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.ARML.SoundVolume];

            label_Stethoscopy_LungSounds_ARLL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.ARLL.SoundID][1];
            label_Stethoscopy_LungSounds_ARLL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.ARLL.SoundVolume];

            label_Stethoscopy_LungSounds_ALUL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.ALUL.SoundID][1];
            label_Stethoscopy_LungSounds_ALUL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.ALUL.SoundVolume];

            label_Stethoscopy_LungSounds_ALLL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.ALLL.SoundID][1];
            label_Stethoscopy_LungSounds_ALLL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.ALLL.SoundVolume];

            label_Stethoscopy_LungSounds_PLUL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.PLUL.SoundID][1];
            label_Stethoscopy_LungSounds_PLUL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.PLUL.SoundVolume];

            label_Stethoscopy_LungSounds_PLLL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.PLLL.SoundID][1];
            label_Stethoscopy_LungSounds_PLLL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.PLLL.SoundVolume];

            label_Stethoscopy_LungSounds_PRUL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.PRUL.SoundID][1];
            label_Stethoscopy_LungSounds_PRUL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.PRUL.SoundVolume];

            label_Stethoscopy_LungSounds_PRLL_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_LungSounds[(int)LungSounds.PRLL.SoundID][1];
            label_Stethoscopy_LungSounds_PRLL_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)LungSounds.PRLL.SoundVolume];
        }

        private void ShowData_Stethoscopy_AbdomenSounds(Modle.AbdomenSounds AbdomenSounds)
        {
            //throw new NotImplementedException();

            label_Stethoscopy_AbdomenSounds_Bowel_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_BowelSounds[(int)AbdomenSounds.Bowel.SoundID][1];
            label_Stethoscopy_AbdomenSounds_Bowel_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)AbdomenSounds.Bowel.SoundVolume];

            label_Stethoscopy_AbdomenSounds_Vascular_SoundID.Content = DataDictionary.MyDictionary.Stethoscopy_VascularSounds[(int)AbdomenSounds.Vascular.SoundID][1];
            label_Stethoscopy_AbdomenSounds_Vascular_SoundVolume_Value.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)AbdomenSounds.Vascular.SoundVolume];
        }

        #endregion

        #region 血压

        private void ShowData_BloodPressure(Modle.BloodPressure BloodPressure)
        {
            ShowData_BloodPressure_NIBP(BloodPressure.NIBP);
            ShowData_BloodPressure_Korotkoff(BloodPressure.Korotkoff);
        }

        private void ShowData_BloodPressure_NIBP(Modle.NIBP NIBP)
        {
            //throw new NotImplementedException();
            label_BloodPressure_NIBP_Systolic.Content = NIBP.Systolic.Value.ToString();
            label_BloodPressure_NIBP_Diastolic.Content = NIBP.Diastolic.Value.ToString();
            label_BloodPressure_NIBP_Unit.Content = DataDictionary.MyDictionary.Pressure_Unit[(int)NIBP.Systolic.Unit];

        }

        private void ShowData_BloodPressure_Korotkoff(Modle.Korotkoff Korotkoff)
        {
            //throw new NotImplementedException();

            label_BloodPressure_Korotkoff_SoundVolume.Content = DataDictionary.MyDictionary.SoundVolumeList[(int)Korotkoff.SoundVolume];
            label_BloodPressure_Korotkoff_Priority_Status.Content = GetString_ControllerStatus(Korotkoff.Priority.Status);
        }

        #endregion

        #region  惊厥  Convulsions

        private void ShowData_Convulsions(Modle.Convulsions Convulsions)
        {
            ShowData_Convulsions_Clonic(Convulsions.Clonic);
            ShowData_Convulsions_Tonic(Convulsions.Tonic);
        }

        private void ShowData_Convulsions_Clonic(Modle.Clonic Clonic)
        {
            //throw new NotImplementedException();
            label_Convulsions_Clonic_RightArm_Status.Content = GetString_ControllerStatus(Clonic.RightArm.Status);
            label_Convulsions_Clonic_RightArm_Value.Content = DataDictionary.MyDictionary.Convulsions_Clonic[(int)Clonic.RightArm.Value][1];
            label_Convulsions_Clonic_LeftArm_Status.Content = GetString_ControllerStatus(Clonic.LeftArm.Status);
            label_Convulsions_Clonic_LeftArm_Value.Content = DataDictionary.MyDictionary.Convulsions_Clonic[(int)Clonic.LeftArm.Value][1];
        }

        private void ShowData_Convulsions_Tonic(Modle.Tonic Tonic)
        {
            //throw new NotImplementedException();
            label_Convulsions_Tonic_NeckAnkylosis_Status.Content = GetString_ControllerStatus(Tonic.NeckAnkylosis.Status);
            label_Convulsions_Tonic_NeckAnkylosis_Value.Content = DataDictionary.MyDictionary.Convulsions_Tonic[(int)Tonic.NeckAnkylosis.Value][1];
        }

        #endregion

        #region 脉搏  Pulse

        private void ShowData_Pulse(Modle.Pulse Pulse)
        {
            ShowData_Pulse_Body(Pulse.BodyPulse);
            ShowData_Pulse_RightArm(Pulse.RightArmPulse);
            ShowData_Pulse_LeftArm(Pulse.LeftArmPulse);
            ShowData_Pulse_RightLeg(Pulse.RightLegPulse);
            ShowData_Pulse_LeftLeg(Pulse.LeftLegPulse);
        }

        private void ShowData_Pulse_Body(Modle.BodyPulse BodyPulse)
        {
            //throw new NotImplementedException();
            label_Pulse_Body_Carotid_Right_Status.Content = GetString_ControllerStatus(BodyPulse.CarotidRight.Status);
            label_Pulse_Body_Carotid_Right_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)BodyPulse.CarotidRight.Value];
            label_Pulse_Body_Carotid_Left_Status.Content = GetString_ControllerStatus(BodyPulse.CarotidLeft.Status);
            label_Pulse_Body_Carotid_Left_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)BodyPulse.CarotidLeft.Value];
            label_Pulse_Body_Femoral_Right_Status.Content = GetString_ControllerStatus(BodyPulse.FemoralRight.Status);
            label_Pulse_Body_Femoral_Right_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)BodyPulse.FemoralRight.Value];
            label_Pulse_Body_Femoral_Left_Status.Content = GetString_ControllerStatus(BodyPulse.FemoralLeft.Status);
            label_Pulse_Body_Femoral_Left_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)BodyPulse.FemoralLeft.Value];
        }

        private void ShowData_Pulse_RightArm(Modle.RightArmPulse RightArmPulse)
        {
            //throw new NotImplementedException();
            label_Pulse_RightArm_Brachial_Status.Content = GetString_ControllerStatus(RightArmPulse.Brachial.Status);
            label_Pulse_RightArm_Brachial_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)RightArmPulse.Brachial.Value];
            label_Pulse_RightArm_Radial_Status.Content = GetString_ControllerStatus(RightArmPulse.Radial.Status);
            label_Pulse_RightArm_Radial_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)RightArmPulse.Radial.Value];
        }

        private void ShowData_Pulse_LeftArm(Modle.LeftArmPulse LeftArmPulse)
        {
            //throw new NotImplementedException();
            label_Pulse_LeftArm_Brachial_Status.Content = GetString_ControllerStatus(LeftArmPulse.Brachial.Status);
            label_Pulse_LeftArm_Brachial_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)LeftArmPulse.Brachial.Value];
            label_Pulse_LeftArm_Radial_Status.Content = GetString_ControllerStatus(LeftArmPulse.Radial.Status);
            label_Pulse_LeftArm_Radial_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)LeftArmPulse.Radial.Value];
        }

        private void ShowData_Pulse_RightLeg(Modle.RightLegPulse RightLegPulse)
        {
            //throw new NotImplementedException();
            label_Pulse_RightLeg_Popliteal_Status.Content = GetString_ControllerStatus(RightLegPulse.Popliteal.Status);
            label_Pulse_RightLeg_Popliteal_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)RightLegPulse.Popliteal.Value];
            label_Pulse_RightLeg_DorsalisPedis_Status.Content = GetString_ControllerStatus(RightLegPulse.DorsalisPedis.Status);
            label_Pulse_RightLeg_DorsalisPedis_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)RightLegPulse.DorsalisPedis.Value];
            label_Pulse_RightLeg_Heel_Status.Content = GetString_ControllerStatus(RightLegPulse.Heel.Status);
            label_Pulse_RightLeg_Heel_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)RightLegPulse.Heel.Value];
        }

        private void ShowData_Pulse_LeftLeg(Modle.LeftLegPulse LeftLegPulse)
        {
            //throw new NotImplementedException();
            label_Pulse_LeftLeg_Popliteal_Status.Content = GetString_ControllerStatus(LeftLegPulse.Popliteal.Status);
            label_Pulse_LeftLeg_Popliteal_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)LeftLegPulse.Popliteal.Value];
            label_Pulse_LeftLeg_DorsalisPedis_Status.Content = GetString_ControllerStatus(LeftLegPulse.DorsalisPedis.Status);
            label_Pulse_LeftLeg_DorsalisPedis_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)LeftLegPulse.DorsalisPedis.Value];
            label_Pulse_LeftLeg_Heel_Status.Content = GetString_ControllerStatus(LeftLegPulse.Heel.Status);
            label_Pulse_LeftLeg_Heel_Value.Content = DataDictionary.MyDictionary.Pulse_Strength[(int)LeftLegPulse.Heel.Value];
        }

        #endregion

        #region 分泌物  Secretions


        private void ShowData_Secretions(Modle.Secretions Secretions)
        {
            ShowData_Secretions_Sweat(Secretions.Sweat);
            ShowData_Secretions_Eyes(Secretions.Eyes);
            ShowData_Secretions_Mouth(Secretions.Mouth);
            ShowData_Secretions_Ears(Secretions.Ears);
            ShowData_Secretions_Nose(Secretions.Nose);
            ShowData_Secretions_Froth(Secretions.Froth);
        }

        private void ShowData_Secretions_Sweat(Modle.Controller Sweat)
        {
            //throw new NotImplementedException();
            label_Secretions_Sweat_Status.Content = GetString_ControllerStatus(Sweat.Status);
        }

        private void ShowData_Secretions_Eyes(Modle.Controller Eyes)
        {
            //throw new NotImplementedException();
            label_Secretions_Eyes_Status.Content = GetString_ControllerStatus(Eyes.Status);
        }

        private void ShowData_Secretions_Mouth(Modle.Controller Mouth)
        {
            //throw new NotImplementedException();
            label_Secretions_Mouth_Status.Content = GetString_ControllerStatus(Mouth.Status);
        }

        private void ShowData_Secretions_Ears(Modle.Controller Ears)
        {
            //throw new NotImplementedException();
            label_Secretions_Ears_Status.Content = GetString_ControllerStatus(Ears.Status);
        }

        private void ShowData_Secretions_Nose(Modle.Controller Nose)
        {
            //throw new NotImplementedException();
            label_Secretions_Nose_Status.Content = GetString_ControllerStatus(Nose.Status);
        }

        private void ShowData_Secretions_Froth(Modle.Controller Froth)
        {
            //throw new NotImplementedException();
            label_Secretions_Froth_Status.Content = GetString_ControllerStatus(Froth.Status);
        }

        #endregion

        #region 出血 Haemorrhage

        private void ShowData_Haemorrhage(Modle.Haemorrhage Haemorrhage)
        {
            ShowData_Haemorrhage_RightUpper(Haemorrhage.RightUpper);
            ShowData_Haemorrhage_LeftUpper(Haemorrhage.LeftUpper);
            ShowData_Haemorrhage_RightLower(Haemorrhage.RightLower);
            ShowData_Haemorrhage_LeftLower(Haemorrhage.LeftLower);
        }

        private void ShowData_Haemorrhage_RightUpper(Modle.BloodVessel RightUpper)
        {
            //throw new NotImplementedException();
            label_Haemorrhage_RightUpper_Arterial_Status.Content = GetString_ControllerStatus(RightUpper.Arterial.Status);
            label_Haemorrhage_RightUpper_Arterial_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)RightUpper.Arterial.Value][1];
            label_Haemorrhage_RightUpper_Venous_Status.Content = GetString_ControllerStatus(RightUpper.Venous.Status);
            label_Haemorrhage_RightUpper_Venous_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)RightUpper.Venous.Value][1];
        }

        private void ShowData_Haemorrhage_LeftUpper(Modle.BloodVessel LeftUpper)
        {
            //throw new NotImplementedException();
            label_Haemorrhage_LeftUpper_Arterial_Status.Content = GetString_ControllerStatus(LeftUpper.Arterial.Status);
            label_Haemorrhage_LeftUpper_Arterial_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)LeftUpper.Arterial.Value][1];
            label_Haemorrhage_LeftUpper_Venous_Status.Content = GetString_ControllerStatus(LeftUpper.Venous.Status);
            label_Haemorrhage_LeftUpper_Venous_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)LeftUpper.Venous.Value][1];
        }

        private void ShowData_Haemorrhage_RightLower(Modle.BloodVessel RightLower)
        {
            //throw new NotImplementedException();
            label_Haemorrhage_RightLower_Arterial_Status.Content = GetString_ControllerStatus(RightLower.Arterial.Status);
            label_Haemorrhage_RightLower_Arterial_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)RightLower.Arterial.Value][1];
            label_Haemorrhage_RightLower_Venous_Status.Content = GetString_ControllerStatus(RightLower.Venous.Status);
            label_Haemorrhage_RightLower_Venous_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)RightLower.Venous.Value][1];
        }

        private void ShowData_Haemorrhage_LeftLower(Modle.BloodVessel LeftLower)
        {
            //throw new NotImplementedException();
            label_Haemorrhage_LeftLower_Arterial_Status.Content = GetString_ControllerStatus(LeftLower.Arterial.Status);
            label_Haemorrhage_LeftLower_Arterial_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)LeftLower.Arterial.Value][1];
            label_Haemorrhage_LeftLower_Venous_Status.Content = GetString_ControllerStatus(LeftLower.Venous.Status);
            label_Haemorrhage_LeftLower_Venous_Value.Content = DataDictionary.MyDictionary.Haemorrhage_Volume[(int)LeftLower.Venous.Value][1];
        }

        #endregion

        #region 导尿 Catheterization

        private void ShowData_Catheterization(Modle.Urine Urine)
        {
            label_Catheterization_Urine_Urinate_Status.Content = GetString_ControllerStatus(Urine.Urinate.Status);
            label_Catheterization_Urine_UPD_Value.Content = DataDictionary.MyDictionary.Urine_UPD[(int)Urine.Urinate.Value];
        }
        #endregion

        #region 药物治疗

        private void ShowData_DrugDelivery(Modle.DrugDelivery DrugDelivery)
        {
            label_Medication_DrugDelivery_Drug_DrugCategory.Content = DataDictionary.MyDictionary.DrugCategory[int.Parse(DataDictionary.MyDictionary.DrugName[(int)DrugDelivery.Drug.DrugID][2])][1];
            label_Medication_DrugDelivery_Drug_DrugName.Content = DataDictionary.MyDictionary.DrugName[(int)DrugDelivery.Drug.DrugID][1];
            label_Medication_DrugDelivery_Drug_DrugCategory_Route_Value.Content = DataDictionary.MyDictionary.DrugRoute[(int)DrugDelivery.Drug.Route][1];
            label_Medication_DrugDelivery_Drug_DrugCategory_Dose_Value.Content = DrugDelivery.Dose.Value.ToString();
            label_Medication_DrugDelivery_Drug_DoseUnit_Value.Content = DataDictionary.MyDictionary.DrugDoseUnit[(int)DrugDelivery.Dose.Unit][1];
        }

        #endregion

        #region 心电监护 导联线

        private void ShowData_ECG(Modle.ECG ECG)
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

        #endregion

        #region 处理操作

        #region 瞳孔检查

        private void button_Eyes_LightCheck_Right_Click(object sender, RoutedEventArgs e)
        {
            _treatment.PupillaryLight.Right.Status = Simulator.Framework.Modle.OperatorStatus.Yes;
            //_treatment.PupillaryLight.Left.Status = Simulator.Framework.Modle.OperatorStatus.No;

            SendPupillaryLight(_treatment.PupillaryLight);
        }

        private void button_Eyes_LightCheck_Left_Click(object sender, RoutedEventArgs e)
        {
            //_treatment.PupillaryLight.Right.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.PupillaryLight.Left.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            SendPupillaryLight(_treatment.PupillaryLight);


        }



        private void SendPupillaryLight(Modle.PupillaryLight PupillaryLight)
        {
            byte[] dataBytes = Resolve.Eyes.GetDataBytes_Eyes_PupillaryLight(PupillaryLight);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        #endregion

        #region 气管插管
        private void button_TracheaCannula_InTrachea_Click(object sender, RoutedEventArgs e)
        {
            _treatment.TracheaCannula.InTrachea.Status = Simulator.Framework.Modle.OperatorStatus.Yes;
            _treatment.TracheaCannula.InEsophagus.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InRightBronchus.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InLeftBronchus.Status = Simulator.Framework.Modle.OperatorStatus.No;

            SendTracheaCannula(_treatment.TracheaCannula);
        }

        private void button_TracheaCannula_InEsophagus_Click(object sender, RoutedEventArgs e)
        {
            _treatment.TracheaCannula.InTrachea.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InEsophagus.Status = Simulator.Framework.Modle.OperatorStatus.Yes;
            _treatment.TracheaCannula.InRightBronchus.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InLeftBronchus.Status = Simulator.Framework.Modle.OperatorStatus.No;

            SendTracheaCannula(_treatment.TracheaCannula);
        }

        private void button_TracheaCannula_InRightBronchus_Click(object sender, RoutedEventArgs e)
        {
            _treatment.TracheaCannula.InTrachea.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InEsophagus.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InRightBronchus.Status = Simulator.Framework.Modle.OperatorStatus.Yes;
            _treatment.TracheaCannula.InLeftBronchus.Status = Simulator.Framework.Modle.OperatorStatus.No;

            SendTracheaCannula(_treatment.TracheaCannula);
        }

        private void button_TracheaCannula_InLeftBronchus_Click(object sender, RoutedEventArgs e)
        {
            _treatment.TracheaCannula.InTrachea.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InEsophagus.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InRightBronchus.Status = Simulator.Framework.Modle.OperatorStatus.No;
            _treatment.TracheaCannula.InLeftBronchus.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            SendTracheaCannula(_treatment.TracheaCannula);
        }

        private void SendTracheaCannula(Modle.TracheaCannula TracheaCannula)
        {
            byte[] dataBytes = Resolve.TracheaCannula.GetDataBytes_TracheaCannula(TracheaCannula);

            SendBytes(dataBytes);
        }

        #endregion

        #region 心肺复苏
        private void label_CPR_Q_Pat_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CPR_Q.Pat.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CPR_Q(_treatment.CPR_Q);
        }

        private void label_CPR_Q_Shout_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CPR_Q.Shout.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CPR_Q(_treatment.CPR_Q);
        }

        private void label_CPR_Q_CheckRightCarotid_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CPR_Q.CheckRightCarotid.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CPR_Q(_treatment.CPR_Q);
        }

        private void label_CPR_Q_CheckLeftCarotid_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CPR_Q.CheckLeftCarotid.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CPR_Q(_treatment.CPR_Q);
        }

        private void label_CPR_Q_HeadTiltChinLift_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CPR_Q.HeadTiltChinLift.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CPR_Q(_treatment.CPR_Q);
        }

        private void label_CPR_Q_RemovalOralForeignBody_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CPR_Q.RemovalOralForeignBody.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CPR_Q(_treatment.CPR_Q);
        }

        private void Send_CPR_Q(Modle.CPR_Q CPR_Q)
        {
            byte[] dataBytes = Resolve.CPR.GetDataBytes_CPR_Q(CPR_Q);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        private void slider_CPR_P_PressDepth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _treatment.CPR_P.PressDepth = Convert.ToInt32(slider_CPR_P_PressDepth.Value);

            Send_CPR_P(_treatment.CPR_P);
        }

        private void checkBox_CPR_P_PressPosition_Status_Checked(object sender, RoutedEventArgs e)
        {
            checkBox_CPR_P_PressPosition_Status_CheckChanged();
        }

        private void checkBox_CPR_P_PressPosition_Status_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBox_CPR_P_PressPosition_Status_CheckChanged();
        }

        private void checkBox_CPR_P_PressPosition_Status_CheckChanged()
        {
            int PressPosition = 0;

            if (checkBox_CPR_P_PressPosition_Status.IsChecked == true)
            {
                PressPosition = 1;

            }
            else
            {
                PressPosition = 0;
            }
            _treatment.CPR_P.PressPosition = PressPosition;

            Send_CPR_P(_treatment.CPR_P);
        }

        private void slider_CPR_P_BlowVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _treatment.CPR_P.BlowVolume = Convert.ToInt32(slider_CPR_P_BlowVolume.Value);

            Send_CPR_P(_treatment.CPR_P);
        }

        private void Send_CPR_P(Modle.CPR_P CPR_P)
        {
            byte[] dataBytes = Resolve.CPR.GetDataBytes_CPR_P(CPR_P);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        #endregion

        #region 除颤

        private void button_Defibrillation_Energy_Click(object sender, RoutedEventArgs e)
        {
            int Energy = 0;
            if (comboBox_Defibrillation_Energy.SelectedItem != null)
                Energy = int.Parse(((Library.ListItem)comboBox_Defibrillation_Energy.SelectedItem).Text);
            _treatment.Defibrillation.Energy = Energy;

            Send_Defibrillation(_treatment.Defibrillation);

        }

        private void Send_Defibrillation(Modle.Defibrillation Defibrillation)
        {
            byte[] dataBytes = Resolve.Defibrillation.GetDataBytes_Defibrillation(Defibrillation);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        private void label_Defibrillation_Electrode_Sterno_Click(object sender, RoutedEventArgs e)
        {

            _treatment.DefibrillatorElectrode.Sterno.Status = Modle.OperatorStatus.Yes;

            Send_DefibrillatorElectrode(_treatment.DefibrillatorElectrode);

        }

        private void label_Defibrillation_Electrode_Apex_Click(object sender, RoutedEventArgs e)
        {
            _treatment.DefibrillatorElectrode.Apex.Status = Modle.OperatorStatus.Yes;

            Send_DefibrillatorElectrode(_treatment.DefibrillatorElectrode);
        }

        private void Send_DefibrillatorElectrode(Modle.DefibrillatorElectrode DefibrillatorElectrode)
        {
            byte[] dataBytes = Resolve.Defibrillation.GetDataBytes_DefibrillatorElectrode(DefibrillatorElectrode);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        #endregion

        #region 起搏

        private void label_Pacing_Current_Click(object sender, RoutedEventArgs e)
        {
            int Current = 0;
            if (comboBox_Pacing_Current.SelectedItem != null)
                Current = int.Parse(((Library.ListItem)comboBox_Pacing_Current.SelectedItem).Text);
            _treatment.Pacing.Current = Current;

            Send_Pacing(_treatment.Pacing);
        }

        private void Send_Pacing(Modle.Pacing Pacing)
        {
            byte[] dataBytes = Resolve.Pacing.GetDataBytes_Pacing(Pacing);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        private void label_Pacing_Electrode_Sternum_Click(object sender, RoutedEventArgs e)
        {
            _treatment.PacerElectrode.Sternum.Status = Modle.OperatorStatus.Yes;

            Send_PacerElectrode(_treatment.PacerElectrode);
        }

        private void label_Pacing_Electrode_Back_Click(object sender, RoutedEventArgs e)
        {
            _treatment.PacerElectrode.Back.Status = Modle.OperatorStatus.Yes;

            Send_PacerElectrode(_treatment.PacerElectrode);
        }

        private void Send_PacerElectrode(Modle.PacerElectrode PacerElectrode)
        {
            byte[] dataBytes = Resolve.Pacing.GetDataBytes_PacerElectrode(PacerElectrode);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        #endregion

        #region 生命体征 

        private void Send_VitalSigns(Modle.VitalSigns VitalSigns)
        {
            Send_VitalSigns_Cyclic(VitalSigns.Cyclic);
            Send_VitalSigns_Breath(VitalSigns.Breath);
            Send_VitalSigns_Other(VitalSigns.Other);
        }                

        #region 循环 Cyclic

        private void Send_VitalSigns_Cyclic(Modle.Cyclic Cyclic)
        {
            //throw new NotImplementedException();

            Send_VitalSigns_Cyclic_Rhythm(Cyclic.Rhythm);
            Send_VitalSigns_Cyclic_HeartRate(Cyclic.HeartRate);
            Send_VitalSigns_Cyclic_IBP(Cyclic.IBP);
            Send_VitalSigns_Cyclic_SpO2(Cyclic.SpO2);
            Send_VitalSigns_Cyclic_PAP(Cyclic.PAP);
            Send_VitalSigns_Cyclic_CVP(Cyclic.CVP);
            Send_VitalSigns_Cyclic_PAWP(Cyclic.PAWP);
            Send_VitalSigns_Cyclic_C_O_(Cyclic.C_O_);
        }

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

        private void Send_VitalSigns_Breath(Modle.Breath Breath)
        {
            //throw new NotImplementedException();
            Send_VitalSigns_Breath_RespType(Breath.RespType);
            Send_VitalSigns_Breath_RespRate(Breath.RespRate);
            Send_VitalSigns_Breath_InspiratoryCapacity(Breath.InspiratoryCapacity);
            Send_VitalSigns_Breath_RespRatio(Breath.RespRatio);
            Send_VitalSigns_Breath_CO2(Breath.CO2);
            Send_VitalSigns_Breath_ETCO2(Breath.ETCO2);
            Send_VitalSigns_Breath_O2(Breath.O2);
            Send_VitalSigns_Breath_N2O(Breath.N2O);
            Send_VitalSigns_Breath_AGT(Breath.AGT);
        }

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

        private void Send_VitalSigns_Other(Modle.Other Other)
        {
            //throw new NotImplementedException();
            Send_VitalSigns_Other_PeripheralTemperature(Other.PeripheralTemperature);
            Send_VitalSigns_Other_BloodTemperature(Other.BloodTemperature);
            Send_VitalSigns_Other_pH(Other.pH);
            Send_VitalSigns_Other_ICP(Other.ICP);
            Send_VitalSigns_Other_TOF(Other.TOF);
            Send_VitalSigns_Other_PTC(Other.PTC);
        }

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

        #region 血压测量

        private void slider_BloodPressure_CuffPressure_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _treatment.MeasureBP.CuffPressure = Convert.ToInt32(slider_BloodPressure_CuffPressure.Value);
            label_BloodPressure_CuffPressure_Value.Content = _treatment.MeasureBP.CuffPressure.ToString();

            SendBloodPressureCuffPressure(_treatment.MeasureBP);
        }

        private void SendBloodPressureCuffPressure(Modle.MeasureBP MeasureBP)
        {
            byte[] dataBytes = Resolve.MeasureBP.GetDataBytes_MeasureBP(MeasureBP);

            SendBytes(dataBytes);
        }

        #endregion

        #region 脉搏检查

        private void button_Pulse_RightArm_Brachial_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckRightArmPulse.Brachial.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckRightArmPulse(_treatment.CheckPulse.CheckRightArmPulse);
        }

        private void button_Pulse_RightArm_Radial_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckRightArmPulse.Radial.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckRightArmPulse(_treatment.CheckPulse.CheckRightArmPulse);
        }

        private void Send_CheckRightArmPulse(Modle.CheckRightArmPulse RightArmPulseCheck)
        {
            byte[] dataBytes = Resolve.Pulse.GetDataBytes_CheckPulse_RightArm(RightArmPulseCheck);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        private void button_Pulse_Body_Carotid_Right_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckBodyPulse.Carotid_Right.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckBodyPulse(_treatment.CheckPulse.CheckBodyPulse);
        }

        private void button_Pulse_Body_Carotid_Left_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckBodyPulse.Carotid_Left.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckBodyPulse(_treatment.CheckPulse.CheckBodyPulse);
        }

        private void button_Pulse_Body_Femoral_Right_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckBodyPulse.Femoral_Right.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckBodyPulse(_treatment.CheckPulse.CheckBodyPulse);
        }

        private void button_Pulse_Body_Femoral_Left_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckBodyPulse.Femoral_Left.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckBodyPulse(_treatment.CheckPulse.CheckBodyPulse);
        }

        private void Send_CheckBodyPulse(Modle.CheckBodyPulse BodyPulseCheck)
        {
            byte[] dataBytes = Resolve.Pulse.GetDataBytes_CheckPulse_Body(BodyPulseCheck);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        private void button_Pulse_LeftArm_Brachial_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckLeftArmPulse.Brachial.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckLeftArmPulse(_treatment.CheckPulse.CheckLeftArmPulse);
        }

        private void button_Pulse_LeftArm_Radial_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckLeftArmPulse.Radial.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckLeftArmPulse(_treatment.CheckPulse.CheckLeftArmPulse);
        }

        private void Send_CheckLeftArmPulse(Modle.CheckLeftArmPulse LeftArmPulseCheck)
        {
            byte[] dataBytes = Resolve.Pulse.GetDataBytes_CheckPulse_LeftArm(LeftArmPulseCheck);

            m_NetServer.SendBytesToAll(dataBytes);
        }
        
        private void button_Pulse_RightLeg_Popliteal_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckRightLegPulse.Popliteal.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckRightLegPulse(_treatment.CheckPulse.CheckRightLegPulse);
        }

        private void button_Pulse_RightLeg_DorsalisPedis_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckRightLegPulse.DorsalisPedis.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckRightLegPulse(_treatment.CheckPulse.CheckRightLegPulse);
        }

        private void button_Pulse_RightLeg_Heel_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckRightLegPulse.Heel.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckRightLegPulse(_treatment.CheckPulse.CheckRightLegPulse);
        }

        private void Send_CheckRightLegPulse(Modle.CheckRightLegPulse RightLegPulseCheck)
        {
            byte[] dataBytes = Resolve.Pulse.GetDataBytes_CheckPulse_RightLeg(RightLegPulseCheck);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        private void button_Pulse_LeftLeg_Popliteal_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckLeftLegPulse.Popliteal.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckLeftLegPulse(_treatment.CheckPulse.CheckLeftLegPulse);
        }

        private void button_Pulse_LeftLeg_DorsalisPedis_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckLeftLegPulse.DorsalisPedis.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckLeftLegPulse(_treatment.CheckPulse.CheckLeftLegPulse);
        }

        private void button_Pulse_LeftLeg_Heel_Click(object sender, RoutedEventArgs e)
        {
            _treatment.CheckPulse.CheckLeftLegPulse.Heel.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            Send_CheckLeftLegPulse(_treatment.CheckPulse.CheckLeftLegPulse);
        }

        private void Send_CheckLeftLegPulse(Modle.CheckLeftLegPulse LeftLegPulseCheck)
        {
            byte[] dataBytes = Resolve.Pulse.GetDataBytes_CheckPulse_LeftLeg(LeftLegPulseCheck);

            m_NetServer.SendBytesToAll(dataBytes);
        }

        #endregion

        #region 导尿
        private void label_Catheterization_InsertionBladder_Click(object sender, RoutedEventArgs e)
        {
            _treatment.Catheterization.InsertionBladder.Status = Simulator.Framework.Modle.OperatorStatus.Yes;

            SendCatheterization(_treatment.Catheterization);
        }

        private void SendCatheterization(Modle.Catheterization Catheterization)
        {
            byte[] dataBytes = Resolve.Catheterization.GetDataBytes_Catheterization(Catheterization);

            SendBytes(dataBytes);
        }

        #endregion

        #region 腹部触诊

        private void button_Palpation_AbdominalTouch_RightUpper_Click(object sender, RoutedEventArgs e)
        {
            _treatment.AbdominalTouch.RightUpperTouch.Status = Modle.OperatorStatus.No;
            if (_signs.Palpation.Abdominal.RightUpper.Status == Modle.ControllerStatus.Yes)
            {
                _treatment.AbdominalTouch.RightUpperTouch.Status = Modle.OperatorStatus.Yes;
                Send_AbdominalTouch_RightUpperTouch(_treatment.AbdominalTouch.RightUpperTouch);
            }
        }

        private void Send_AbdominalTouch_RightUpperTouch(Modle.Operator RightUpperTouch)
        {
            byte[] dataBytes = Resolve.Palpation.GetDataBytes_AbdominalTouch_RightUpperTouch(RightUpperTouch);

            SendBytes(dataBytes);
        }

        private void button_Palpation_AbdominalTouch_LeftUpper_Click(object sender, RoutedEventArgs e)
        {
            _treatment.AbdominalTouch.LeftUpperTouch.Status = Modle.OperatorStatus.No;
            if (_signs.Palpation.Abdominal.LeftUpper.Status == Modle.ControllerStatus.Yes)
            {
                _treatment.AbdominalTouch.LeftUpperTouch.Status = Modle.OperatorStatus.Yes;
                Send_AbdominalTouch_LeftUpperTouch(_treatment.AbdominalTouch.LeftUpperTouch);
            }
        }

        private void Send_AbdominalTouch_LeftUpperTouch(Modle.Operator LeftUpperTouch)
        {
            byte[] dataBytes = Resolve.Palpation.GetDataBytes_AbdominalTouch_LeftUpperTouch(LeftUpperTouch);

            SendBytes(dataBytes);
        }

        private void button_Palpation_AbdominalTouch_Middle_Click(object sender, RoutedEventArgs e)
        {
            _treatment.AbdominalTouch.MiddleTouch.Status = Modle.OperatorStatus.No;
            if (_signs.Palpation.Abdominal.Middle.Status == Modle.ControllerStatus.Yes)
            {
                _treatment.AbdominalTouch.MiddleTouch.Status = Modle.OperatorStatus.Yes;
                Send_AbdominalTouch_MiddleTouch(_treatment.AbdominalTouch.MiddleTouch);
            }
        }

        private void Send_AbdominalTouch_MiddleTouch(Modle.Operator MiddleTouch)
        {
            byte[] dataBytes = Resolve.Palpation.GetDataBytes_AbdominalTouch_MiddleTouch(MiddleTouch);

            SendBytes(dataBytes);
        }

        private void button_Palpation_AbdominalTouch_RightLower_Click(object sender, RoutedEventArgs e)
        {
            _treatment.AbdominalTouch.RightLowerTouch.Status = Modle.OperatorStatus.No;
            if (_signs.Palpation.Abdominal.RightLower.Status == Modle.ControllerStatus.Yes)
            {
                _treatment.AbdominalTouch.RightLowerTouch.Status = Modle.OperatorStatus.Yes;
                Send_AbdominalTouch_RightLowerTouch(_treatment.AbdominalTouch.RightLowerTouch);
            }
        }

        private void Send_AbdominalTouch_RightLowerTouch(Modle.Operator RightLowerTouch)
        {
            byte[] dataBytes = Resolve.Palpation.GetDataBytes_AbdominalTouch_RightLowerTouch(RightLowerTouch);

            SendBytes(dataBytes);
        }

        private void button_Palpation_AbdominalTouch_LeftLower_Click(object sender, RoutedEventArgs e)
        {
            _treatment.AbdominalTouch.LeftLowerTouch.Status = Modle.OperatorStatus.No;
            if (_signs.Palpation.Abdominal.LeftLower.Status == Modle.ControllerStatus.Yes)
            {
                _treatment.AbdominalTouch.LeftLowerTouch.Status = Modle.OperatorStatus.Yes;
                Send_AbdominalTouch_LeftLowerTouch(_treatment.AbdominalTouch.LeftLowerTouch);
            }
        }

        private void Send_AbdominalTouch_LeftLowerTouch(Modle.Operator LeftLowerTouch)
        {
            byte[] dataBytes = Resolve.Palpation.GetDataBytes_AbdominalTouch_LeftLowerTouch(LeftLowerTouch);

            SendBytes(dataBytes);
        }

        #endregion

        #region  药物治疗

        private void label_Medication_Apply_PO_Click(object sender, RoutedEventArgs e)
        {
            int DrugID = 0;
            if (comboBox_Medication_Apply_PO_Drug.SelectedItem != null)
                DrugID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_PO_Drug.SelectedItem).Value);
            _treatment.Medication.PO.DrugID = DrugID;

            int Dose = 0;
            if (textBox_Medication_Apply_PO_Dose.Text.Trim() != "")
                Dose = int.Parse(textBox_Medication_Apply_PO_Dose.Text);
            _treatment.Medication.PO.Dose.Value = Dose;

            int DoseUnitID = 0;
            if (comboBox_Medication_Apply_PO_DoseUnit.SelectedItem != null)
                DoseUnitID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_PO_DoseUnit.SelectedItem).Value);
            _treatment.Medication.PO.Dose.Unit = DoseUnitID;

            Send_Medication_PO(_treatment.Medication.PO);
        }

        private void Send_Medication_PO(Modle.PO PO)
        {
            byte[] dataBytes = Resolve.Medication.GetDataBytes_Medication_PO(PO);

            SendBytes(dataBytes);
        }

        private void label_Medication_Apply_IV_Click(object sender, RoutedEventArgs e)
        {
            int DrugID = 0;
            if (comboBox_Medication_Apply_IV_Drug.SelectedItem != null)
                DrugID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_IV_Drug.SelectedItem).Value);
            _treatment.Medication.IV.DrugID = DrugID;

            int Dose = 0;
            if (textBox_Medication_Apply_IV_Dose.Text.Trim() != "")
                Dose = int.Parse(textBox_Medication_Apply_IV_Dose.Text);
            _treatment.Medication.IV.Dose.Value = Dose;

            int DoseUnitID = 0;
            if (comboBox_Medication_Apply_IV_DoseUnit.SelectedItem != null)
                DoseUnitID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_IV_DoseUnit.SelectedItem).Value);
            _treatment.Medication.IV.Dose.Unit = DoseUnitID;

            Send_Medication_IV(_treatment.Medication.IV);
        }

        private void Send_Medication_IV(Modle.IV IV)
        {
            byte[] dataBytes = Resolve.Medication.GetDataBytes_Medication_IV(IV);

            SendBytes(dataBytes);
        }

        private void label_Medication_Apply_IVGtt_Click(object sender, RoutedEventArgs e)
        {
            int DrugID = 0;
            if (comboBox_Medication_Apply_IVGtt_Drug.SelectedItem != null)
                DrugID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_IVGtt_Drug.SelectedItem).Value);
            _treatment.Medication.IVGtt.DrugID = DrugID;

            int Dose = 0;
            if (textBox_Medication_Apply_IVGtt_Dose.Text.Trim() != "")
                Dose = int.Parse(textBox_Medication_Apply_IVGtt_Dose.Text);
            _treatment.Medication.IVGtt.Dose.Value = Dose;

            int DoseUnitID = 0;
            if (comboBox_Medication_Apply_IVGtt_DoseUnit.SelectedItem != null)
                DoseUnitID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_IVGtt_DoseUnit.SelectedItem).Value);
            _treatment.Medication.IVGtt.Dose.Unit = DoseUnitID;

            Send_Medication_IVGtt(_treatment.Medication.IVGtt);
        }

        private void Send_Medication_IVGtt(Modle.IVGtt IVGtt)
        {
            byte[] dataBytes = Resolve.Medication.GetDataBytes_Medication_IVGtt(IVGtt);

            SendBytes(dataBytes);
        }

        private void label_Medication_Apply_IM_Click(object sender, RoutedEventArgs e)
        {
            int DrugID = 0;
            if (comboBox_Medication_Apply_IM_Drug.SelectedItem != null)
                DrugID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_IM_Drug.SelectedItem).Value);
            _treatment.Medication.IM.DrugID = DrugID;

            int Dose = 0;
            if (textBox_Medication_Apply_IM_Dose.Text.Trim() != "")
                Dose = int.Parse(textBox_Medication_Apply_IM_Dose.Text);
            _treatment.Medication.IM.Dose.Value = Dose;

            int DoseUnitID = 0;
            if (comboBox_Medication_Apply_IM_DoseUnit.SelectedItem != null)
                DoseUnitID = int.Parse(((Library.ListItem)comboBox_Medication_Apply_IM_DoseUnit.SelectedItem).Value);
            _treatment.Medication.IM.Dose.Unit = DoseUnitID;

            Send_Medication_IM(_treatment.Medication.IM);
        }

        private void Send_Medication_IM(Modle.IM IM)
        {
            byte[] dataBytes = Resolve.Medication.GetDataBytes_Medication_IM(IM);

            SendBytes(dataBytes);
        }





        #endregion

        #region 心电监护 导联线

        private void Send_ECG_LeadLine(Modle.LeadLine LeadLine)
        {
            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(LeadLine.LeadLine_ECG.LeadLine_ECG5);
            Send_ECG_LeadLine_LeadLineSPO2(LeadLine.LeadLine_SPO2);
            Send_ECG_LeadLine_LeadLineNIBP(LeadLine.LeadLine_NIBP);
            Send_ECG_LeadLine_LeadLineTemperature(LeadLine.LeadLine_Temperature);
        }

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
            Modle.OperatorStatus OperatorStatus =  Simulator.Framework.Modle.OperatorStatus.No;

            if (checkBox_VitalSigns_LeadLine_RA.IsChecked == true)
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.Yes;

            }
            else
            {
                OperatorStatus = Simulator.Framework.Modle.OperatorStatus.No;
            }
            _treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5.RA.Status = OperatorStatus;

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
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

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
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

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
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

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
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

            Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(_treatment.ECG.LeadLine.LeadLine_ECG.LeadLine_ECG5);
        }

        private void Send_ECG_LeadLine_LeadLineECG_LeadLineECG5(Modle.LeadLine_ECG5 LeadLine_ECG5)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.GetDataBytes_LeadLine_ECG5(LeadLine_ECG5);

            m_NetServer.SendBytesToAll(dataBytes);
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

            Send_ECG_LeadLine_LeadLineSPO2(_treatment.ECG.LeadLine.LeadLine_SPO2);
        }

        private void Send_ECG_LeadLine_LeadLineSPO2(Modle.LeadLine_SPO2 LeadLine_SPO2)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.GetDataBytes_LeadLine_SPO2(LeadLine_SPO2);

            m_NetServer.SendBytesToAll(dataBytes);
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

            Send_ECG_LeadLine_LeadLineNIBP(_treatment.ECG.LeadLine.LeadLine_NIBP);
        }

        private void Send_ECG_LeadLine_LeadLineNIBP(Modle.LeadLine_NIBP LeadLine_NIBP)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.GetDataBytes_LeadLine_NIBP(LeadLine_NIBP);

            m_NetServer.SendBytesToAll(dataBytes);
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

            Send_ECG_LeadLine_LeadLineTemperature(_treatment.ECG.LeadLine.LeadLine_Temperature);
        }
        private void Send_ECG_LeadLine_LeadLineTemperature(Modle.LeadLine_Temperature LeadLine_Temperature)
        {
            byte[] dataBytes = Resolve.ECG.LeadLine.GetDataBytes_LeadLine_Temperature(LeadLine_Temperature);

            m_NetServer.SendBytesToAll(dataBytes);
        }


        #endregion

        #endregion

        #region 设定

        
               

        #endregion

    }
}
