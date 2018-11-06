using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modle = YH.Simulator.Framework.Modle;

namespace YH.Simulator.Framework.Resolve
{
    
    /// <summary>
    /// 数据字节处理
    /// </summary>
    static public class Resolve
    {        
        /// <summary>
        /// 体征
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="Signs"></param>
        static public Modle.Function SetData_Signs(byte[] dataBytes, ref Modle.Signs Signs)
        {
            Modle.Function Function = 0;
            if (dataBytes.Length != 8)
                return Function;

            byte wordKey1 = dataBytes[0];
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey1)
            {
                case 0x20:          //眼睛 Eyes
                    Function = Modle.Function.Eyes;
                    Eyes.SetData_Eyes(dataBytes, ref Signs.Eyes);
                    break;
                case 0x21:          //紫绀 Cyanosis
                    Function = Modle.Function.Cyanosis;
                    Cyanosis.SetData_Cyanosis(dataBytes, ref Signs.Cyanosis);
                    break;
                case 0x22:          //声音 Analog Voice
                    Function = Modle.Function.AnalogVocal;
                    AnalogVocal.SetData_AnalogVoice(dataBytes, ref Signs.AnalogVocal);
                    break;
                case 0x23:          //气道 Airway
                    Function = Modle.Function.Airway;
                    Airway.SetData_Airway(dataBytes, ref Signs.Airway);
                    break;
                case 0x24:          //气管插管 TracheaCannula
                    Function = Modle.Function.TracheaCannula;
                    break;
                case 0x25:          //心肺复苏 CPR
                    Function = Modle.Function.CPR;
                    break;
                case 0x26:          //除颤 Defibrillation    
                    Function = Modle.Function.Defibrillation;
                    break;
                case 0x27:          //起搏 Pacing
                    Function = Modle.Function.Pacing;
                    break;
                case 0x28:          //生命体征 VitalSigns
                    Function = Modle.Function.VitalSigns;
                    VitalSigns.SetData_VitalSigns(dataBytes, ref Signs.VitalSigns);
                    break;
                case 0x29:          //触诊 Palpation
                    Function = Modle.Function.Palpation;
                    Palpation.SetData_Palpation(dataBytes, ref Signs.Palpation);
                    break;
                case 0x30:          //听诊 Stethoscopy
                    Function = Modle.Function.Stethoscopy;
                    Stethoscopy.SetData_Stethoscopy(dataBytes, ref Signs.Stethoscopy);
                    break;
                case 0x31:          //血压 Blood Pressure
                    Function = Modle.Function.BloodPressure;
                    MeasureBP.SetData_BloodPressure(dataBytes, ref Signs.BloodPressure);
                    break;
                case 0x32:          //惊厥 Convulsions
                    Function = Modle.Function.Convulsions;
                    Convulsions.SetData_Convulsions(dataBytes, ref Signs.Convulsions);
                    break;
                case 0x33:          //脉搏  Pulse
                    Function = Modle.Function.Pulse;
                    Pulse.SetData_Pulse(dataBytes, ref Signs.Pulse);
                    break;
                case 0x34:          //分泌 Secretions
                    Function = Modle.Function.Secretions;
                    Secretions.SetData_Secretions(dataBytes, ref Signs.Secretions);
                    break;
                case 0x35:          //出血  Haemorrhage
                    Function = Modle.Function.Haemorrhage;
                    Haemorrhage.SetData_Haemorrhage(dataBytes, ref Signs.Haemorrhage);
                    break;
                case 0x36:          //尿液 Urine
                    Function = Modle.Function.Catheterization;
                    Catheterization.SetData_Urine(dataBytes, ref Signs.Urine);
                    break;
                case 0x37:          //给药DrugDelivery
                    Function = Modle.Function.Medication;
                    Medication.SetData_DrugDelivery(dataBytes, ref Signs.DrugDelivery);
                    break;
                case 0x38:          //心电监护 导联线
                    Function = Modle.Function.ECG;
                    ECG.LeadLine.SetData_LeadLine(dataBytes, ref Signs.ECG.LeadLine);
                    break;
                default:
                    break;
            }

            return Function;
        }        

        /// <summary>
        /// ECG数据获取
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="Signs"></param>
        static public void GetData_VitalSigns(byte[] dataBytes, ref Modle.Signs Signs)
        {
            VitalSigns.GetData_VitalSigns(dataBytes, ref Signs.VitalSigns);
        }

        static public List<byte[]> SetDataBytes_Signs(Modle.Signs Signs)
        {
            List<byte[]> dataBytes_List = new List<byte[]>();

            dataBytes_List.AddRange(Eyes.SetDataBytes_Eyes(Signs.Eyes));
            dataBytes_List.AddRange(Cyanosis.SetDataBytes_Cyanosis(Signs.Cyanosis));
            dataBytes_List.AddRange(AnalogVocal.SetDataBytes_AnalogVocal(Signs.AnalogVocal));
            dataBytes_List.AddRange(Airway.SetDataBytes_Airway(Signs.Airway));
            dataBytes_List.AddRange(VitalSigns.SetDataBytes_VitalSigns(Signs.VitalSigns));
            dataBytes_List.AddRange(Palpation.SetDataBytes_Palpation(Signs.Palpation));
            dataBytes_List.AddRange(Stethoscopy.SetDataBytes_Stethoscopy(Signs.Stethoscopy));
            dataBytes_List.AddRange(MeasureBP.SetDataBytes_BloodPressure(Signs.BloodPressure));
            dataBytes_List.AddRange(Convulsions.SetDataBytes_Convulsions(Signs.Convulsions));
            dataBytes_List.AddRange(Pulse.SetDataBytes_Pulse(Signs.Pulse));
            dataBytes_List.AddRange(Secretions.SetDataBytes_Secretions(Signs.Secretions));
            dataBytes_List.AddRange(Haemorrhage.SetDataBytes_Haemorrhage(Signs.Haemorrhage));
            dataBytes_List.AddRange(Catheterization.SetDataBytes_Urine(Signs.Urine));
            dataBytes_List.AddRange(Medication.SetDataBytes_DrugDelivery(Signs.DrugDelivery));

            return dataBytes_List;
        }

        /// <summary>
        /// 治疗处理数据获取
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="Treatment"></param>
        /// <param name="Signs"></param>
        static public Modle.Function GetData_Treatment(byte[] dataBytes, ref Modle.Treatment Treatment)
        {
            Modle.Function Function = 0;

            byte wordKey1 = dataBytes[0];
            byte wordKey2 = dataBytes[1];
            byte wordKey3 = dataBytes[2];

            switch (wordKey1)
            {
                case 0x20:          //眼睛 Eyes
                    Function = Modle.Function.Eyes;
                    Eyes.GetData_Eyes_PupillaryLight(dataBytes, ref Treatment.PupillaryLight);
                    break;
                case 0x21:          //紫绀 Cyanosis
                    Function = Modle.Function.Cyanosis;

                    break;
                case 0x22:          //声音 Analog Voice
                    Function = Modle.Function.AnalogVocal;
                    //AnalogVoice.SetData_AnalogVoice(dataBytes, ref Signs.AnalogVocal);
                    break;
                case 0x23:          //气道 Airway
                    Function = Modle.Function.Airway;

                    break;
                case 0x24:          //气管插管
                    Function = Modle.Function.TracheaCannula;
                    TracheaCannula.GetData_TracheaCannula(dataBytes, ref Treatment.TracheaCannula);
                    break;
                case 0x25:          //心肺复苏
                    Function = Modle.Function.CPR;
                    CPR.GetData_CPR_Q(dataBytes, ref Treatment.CPR_Q);
                    CPR.GetData_CPR_P(dataBytes, ref Treatment.CPR_P);
                    break;
                case 0x26:          //除颤
                    Function = Modle.Function.Defibrillation;
                    Defibrillation.GetData_Defibrillation(dataBytes, ref Treatment.Defibrillation);
                    Defibrillation.GetData_DefibrillationElectrode(dataBytes, ref Treatment.DefibrillatorElectrode);
                    break;
                case 0x27:          //起搏
                    Function = Modle.Function.Pacing;
                    Pacing.GetData_Pacing(dataBytes, ref Treatment.Pacing);
                    Pacing.GetData_PacerElectrode(dataBytes, ref Treatment.PacerElectrode);
                    break;
                case 0x28:          //生命体征 VitalSigns -ECG 导联线
                    Function = Modle.Function.VitalSigns;
                    VitalSigns.GetData_VitalSigns(dataBytes, ref Treatment.VitalSigns);
                    break;
                case 0x29:          //触诊 Palpation
                    Function = Modle.Function.Palpation;
                    Palpation.GetData_Palpation(dataBytes, ref Treatment.AbdominalTouch);
                    break;
                case 0x30:          //听诊 Stethoscopy
                    Function = Modle.Function.Stethoscopy;
                    break;
                case 0x31:          //血压 Blood Pressure
                    Function = Modle.Function.BloodPressure;
                    MeasureBP.GetData_MeasureBP(dataBytes, ref Treatment.MeasureBP);
                    break;
                case 0x32:          //惊厥 Convulsions
                    Function = Modle.Function.Convulsions;

                    break;
                case 0x33:          //脉搏  Pulse
                    Function = Modle.Function.Pulse;
                    Pulse.GetData_CheckPulse(dataBytes, ref Treatment.CheckPulse);
                    break;
                case 0x34:          //分泌 Secretions
                    Function = Modle.Function.Secretions;

                    break;
                case 0x35:          //出血  Haemorrhage
                    Function = Modle.Function.Haemorrhage;

                    break;
                case 0x36:          //尿液 Urine
                    Function = Modle.Function.Catheterization;
                    Catheterization.GetData_Catheterization(dataBytes, ref Treatment.Catheterization);
                    break;
                case 0x37:          //给药DrugDelivery
                    Function = Modle.Function.Medication;
                    Medication.GetData_Medication(dataBytes, ref Treatment.Medication);
                    break;
                case 0x38:
                    Function = Modle.Function.ECG;
                    ECG.LeadLine.GetData_LeadLine(dataBytes, ref Treatment.ECG.LeadLine);
                    break;
                default:
                    break;
            }

            return Function;
        }
    }
}
