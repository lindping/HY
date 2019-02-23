using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Simulator.Framework.DataDictionary
{
    public static class MyDictionary
    {

        /// <summary>
        /// 年龄
        /// </summary>
        public static Dictionary<int, string> Patient_Age = new Dictionary<int, string>() { { 0, "" }, { 1, "年" }, { 2, "月" }, { 3, "周" }, { 4, "天" }, { 5, "小时" }, { 6, "分钟" } };

        /// <summary>
        /// 病人类型
        /// </summary>
        public static Dictionary<int, string[]> Patient_Type = new Dictionary<int, string[]>()
        {
            { 0, new string[] { "0", "", "", "" }},
            { 1, new string[] {"成人", "18", "45", "年"  }},
            { 2, new string[] {"儿童", "2", "12", "年"  }},
            { 3, new string[] {"婴儿", "1", "24", "月" }},
            { 4, new string[] {"新生儿", "1", "7", "天" }},
            { 5, new string[] {"老人", "46", "199", "年" }}
        };

        /// <summary>
        /// 压力单位
        /// </summary>
        public static Dictionary<int, string> Pressure_Unit= new Dictionary<int, string>() { { 0, "" }, { 1, "mmHg" }, { 2, "kpa" } };

        /// <summary>
        /// 眼脸状态
        /// </summary>
        public static Dictionary<int, string> Eyes_EyelidStatus = new Dictionary<int, string>() { { 0, "" }, { 1, "全开" }, { 2, "半开" }, { 3, "关闭" } };

        /// <summary>
        /// 眨眼速度
        /// </summary>
        public static Dictionary<int, string> Eyes_BlinkingSpeed = new Dictionary<int, string>() { { 0, "" }, { 1, "正常" }, { 2, "不频密" }, { 3, "频密" } };

        /// <summary>
        /// 瞳孔大小
        /// </summary>
        public static Dictionary<int, string> Eyes_PupillSize = new Dictionary<int, string>() { { 0, "" }, { 1, "正常" }, { 2, "缩小" }, { 3, "散大" } };

        /// <summary>
        /// 对光灵敏度
        /// </summary>
        public static Dictionary<int, string> Eyes_LightSensitivity = new Dictionary<int, string>() { { 0, "" }, { 1, "正常" }, { 2, "缓慢" }, { 3, "没有" } };

        /// <summary>
        /// 紫绀程度
        /// </summary>
        public static Dictionary<int, string> Cyanosis_Degree = new Dictionary<int, string>() { { 0, "无" }, { 1, "轻度" }, { 2, "中度" }, { 3, "重度" } };

        /// <summary>
        /// 发声
        /// </summary>
        public static Dictionary<int, string> AnalogVocal_Vocal = new Dictionary<int, string>() {
            { 0, "无" },
            { 1, "咳嗽" },
            { 2, "呻吟" },
            { 3, "短促呼吸" },
            { 4, "呕吐" },
            { 5, "尖叫" } };
        /// <summary>
        /// 语音
        /// </summary>
        public static Dictionary<int, string> AnalogVocal_Voice = new Dictionary<int, string>() {
            { 0, "无" },
            { 1, "是" },
            { 2, "不是" }};

        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<int, string[]> Tongue_Edema = new Dictionary<int, string[]>() { { 0, new string[] { "0", "0%" } }, { 1, new string[] { "50", "50%" } }, { 2, new string[] { "100", "100%" } } };

        
        /// <summary>
        /// 除颤能量
        /// </summary>
        public static Dictionary<int, string[]> Defibrillation_Energy = new Dictionary<int, string[]>()
        {
            { 0, new string[] { "0", "0J" } },
            { 1, new string[] { "20", "20J" } },
            { 2, new string[] { "50", "50J" }},
            { 3, new string[] { "100", "100J" }},
            { 4, new string[] { "150", "150J" }},
            { 5, new string[] { "200", "200J" }},
            { 6, new string[] { "250", "250J" }},
            { 7, new string[] { "300", "300J" }},
            { 8, new string[] { "360", "360J" } }
        };

        /// <summary>
        /// 起搏电流
        /// </summary>
        public static Dictionary<int, string[]> Pacing_Current = new Dictionary<int, string[]>()
        {
            { 0, new string[]  { "0" , "0mA" }},
            { 1, new string[]  { "20" , "20mA" }},
            { 2, new string[]  { "30" , "30mA" }},
            { 3, new string[]  { "40" , "40mA" }},
            { 4, new string[]  { "50" , "50mA" }},
            { 5, new string[]  { "60" , "60mA" }},
            { 6, new string[]  { "70" , "70mA" }},
            { 7, new string[]  { "80" , "80mA" }},
            { 8, new string[]  { "90" , "90mA" }},
            { 9, new string[]  { "100" , "100mA" }},
            { 10, new string[] { "110" , "110mA" }},
            { 11, new string[] { "120" , "120mA" }},
            { 12, new string[] { "130" , "130mA" }},
            { 13, new string[] { "140" , "140mA" }},
            { 14, new string[] { "150" , "150mA" }},
            { 15, new string[] { "160" , "160mA" }},
            { 16, new string[] { "170" , "170mA" }},
            { 17, new string[] { "180" , "180mA" }},
            { 18, new string[] { "190" , "190mA" }},
            { 19, new string[] { "200" , "200mA" }}
        };
        /// <summary>
        /// 基本心律
        /// </summary>
        public static Dictionary<int, string> Rhythm_Basic = new Dictionary<int, string>()
        {
            { 0, "" },
            { 1, "1.窦性心律" },
            { 2, "2.伴有心肌缺血的窦律" },
            { 3, "3.窦律，心肌缺血后" },
            { 4, "4.有下急性心肌梗死的窦律，ST抬高" },
            { 5, "5.有前壁急性心肌梗死的窦律，ST抬高" },
            { 6, "6.有前壁急性心肌梗死的窦律，迟" },
            { 7, "7.伴有左束支阻滞的窦律" },
            { 8, "8.伴有右束支阻滞的窦律" },
            { 9, "9.左心室肥大的窦律" },
            { 10, "10.右心室肥大的窦律" },
            { 11, "11.高血钾的窦律" },
            { 12, "12.伴预激综合征（WPW）的窦律" },
            { 13, "13.电轴右偏的窦律" },
            { 14, "14.延长QT时间的窦律" },
            { 15, "15.一度房室传导阻滞" },
            { 16, "16.二度Ⅰ型房室传导阻滞" },
            { 17, "17.二度Ⅱ型房室传导阻滞" },
            { 18, "18.三度房室传导阻滞" },
            { 19, "19.心室停顿" },
            { 20, "20.室上性心动过速（SVT）" },
            { 21, "21.伴有游走起搏器的心房心动过速" },
            { 22, "22.心房扑动（AF）" },
            { 23, "23.心房颤动（AFib）" },
            { 24, "24.交界性逸博" },
            { 25, "25.交界性异博心率" },
            { 26, "26.室性心动过速（VT）" },
            { 27, "27.尖端扭动型室性心动过速" },
            { 28, "28.心室颤动（VF）" },
            { 29, "29.心博停止" },
            { 30, "30.起搏器心室" },
            { 31, "31.房室顺序起搏" },
            { 32, "32.起搏器失去夺获" }
        };

        /// <summary>
        /// 前期收缩
        /// </summary>
        public static Dictionary<int, string> Rhythm_Extrasystole = new Dictionary<int, string>()
        {
            { 0, "" },
            { 1, "1.没有" },
            { 2, "2.单源性室性早搏" },
            { 3, "3.连发室性早搏" },
            { 4, "4.RonT型室性早搏" },
            { 5, "5.房性/交界性早搏" },
            { 6, "6.有前壁急性心肌梗死的窦律，迟" }
        };

        /// <summary>
        /// 心音
        /// </summary>
        public static Dictionary<int, string[]> Stethoscopy_HeartSounds = new Dictionary<int, string[]>()
        {
            {0, new string[] {"0","", "" } },
            {1, new string[] {"1","正常心音", "" } },
            {2, new string[] {"2","收缩期吹风样杂音（二尖瓣关闭不全）", "" } },
            {3, new string[] {"3","舒张期叹气样杂音（主动脉瓣关闭不全）", "" } },
            {4, new string[] {"4","心房纤颤", "" } },
            {5, new string[] {"5","室性心动过速", "" } },
            {6, new string[] {"6","期前收缩（早搏）", "" } },
            {7, new string[] {"7","第三心音", "" } },
            {8, new string[] {"8","类乐音（感染性心内膜炎）", "" } },
            {9, new string[] {"9","收缩期前奔马律", "" } },
            {10, new string[] {"10","舒张中期奔马律", "" } },
            {11, new string[] {"11","舒张早期奔马律", "" } },
            {12, new string[] {"12","舒张晚期奔马律", "" } },
            {13, new string[] {"13","重叠性奔马律", "" } },
            {14, new string[] {"14","火车头奔马律", "" } },
            {15, new string[] {"15","舒张期隆隆样杂音（二尖瓣狭窄）", "" } },
            {16, new string[] {"16","主动脉瓣喷射音", "" } },
            {17, new string[] {"17","连续性机器样杂音（动脉导管未闭）", "" } },
            {18, new string[] {"18","主动脉收缩喷射音", "" } },
            {19, new string[] {"19","开瓣音", "" } },
            {20, new string[] {"20","主动脉瓣狭窄杂音", "" } },
            {21, new string[] {"21","主动脉关闭不全", "" } },
            {22, new string[] {"22","分裂音", "" } },
            {23, new string[] {"23","第二心音分裂", "" } },
            {24, new string[] {"24","第二心音宽阔分裂", "" } },
            {25, new string[] {"25","第二心音逆分裂", "" } },
            {26, new string[] {"26","肺动脉收缩喷射音", "" } },
            {27, new string[] {"27","心脏杂音", "" } },
            {28, new string[] {"28","房间隔缺损", "" } },
            {29, new string[] {"29","动脉导管未闭", "" } },
            {30, new string[] {"30","二尖瓣狭窄第一心音亢进开瓣音", "" } },
            {31, new string[] {"31","室间隔缺损杂音", "" } },
            {32, new string[] {"32","心包摩擦音", "" } },
            {33, new string[] {"33","室间隔缺损杂音", "" } },
            {34, new string[] {"34","三尖瓣关闭不全", "" } },
            {35, new string[] {"35","二尖瓣狭窄杂音", "" } },
            {36, new string[] {"36","完全性房室传导阻滞第一心音亢进", "" } },
            {37, new string[] {"37","二尖瓣关闭不全第一心音减弱", "" } },
            {38, new string[] {"38","第二心音增强", "" } },
            {39, new string[] {"39","第一心音分裂", "" } },
            {40, new string[] {"40","收缩中晚期喀嚓音", "" } },
            {41, new string[] {"41","第三心音", "" } },
            {42, new string[] {"42","二尖瓣关闭不全时的第三心音", "" } },
            {43, new string[] {"43","第四心音减弱", "" } },
            {44, new string[] {"44","二尖瓣狭窄杂音", "" } },
            {45, new string[] {"45","主动脉瓣乐性杂音", "" } },
            {46, new string[] {"46","收缩期反流性杂音", "" } },
            {47, new string[] {"47","收缩早中期杂音", "" } },
            {48, new string[] {"48","收缩晚期杂音", "" } },
            {49, new string[] {"49","舒张晚期杂音", "" } }
        };

        /// <summary>
        /// 肺音
        /// </summary>
        public static Dictionary<int, string[]> Stethoscopy_LungSounds = new Dictionary<int, string[]>()
        {
            {0, new string[] {"0","", "" } },
            {1, new string[] {"1","支气管呼吸音", "" } },
            {2, new string[] {"2","支气管呼吸音伴大湿啰音", "" } },
            {3, new string[] {"3","粗湿罗音合并中湿罗音", "" } },
            {4, new string[] {"4","中湿罗音", "" } },
            {5, new string[] {"5","吸气相哮鸣音", "" } },
            {6, new string[] {"6","呼气相哮鸣音", "" } },
            {7, new string[] {"7","双相哮鸣音", "" } },
            {8, new string[] {"8","低调干啰音", "" } },
            {9, new string[] {"9","粗湿啰音", "" } },
            {10 , new string[] {"10","中湿啰音合并呼气相哮鸣音", "" } },
            {11 , new string[] {"11","肺泡呼吸音", "" } },
            {12 , new string[] {"12","支气管肺泡呼吸音", "" } },
            {13 , new string[] {"13","哮鸣音（呼吸均有）", "" } },
            {14 , new string[] {"14","哮鸣音（以呼吸为主）", "" } },
            {15 , new string[] {"15","捻发音", "" } },
            {16 , new string[] {"16","水泡音", "" } },
            {17 , new string[] {"17","哨笛音（高调干罗音）", "" } },
            {18 , new string[] {"18","胸膜摩擦音", "" } }
        };

        /// <summary>
        /// 肠鸣音
        /// </summary>
        public static Dictionary<int, string[]> Stethoscopy_BowelSounds = new Dictionary<int, string[]>()
        {
            {0, new string[] {"0","", "" } },
            {1  , new string[] {" 1","正常肠鸣音", "" } },
            {2  , new string[] {" 2","肠鸣音亢进", "" } },
            {3  , new string[] {" 3","肠鸣音减弱", "" } }
        };

        /// <summary>
        /// 血管杂音
        /// </summary>
        public static Dictionary<int, string[]> Stethoscopy_VascularSounds = new Dictionary<int, string[]>()
        {
            {0, new string[] {"0","", "" } },
            {1  , new string[] {" 1", "血管杂音1", "" } },
            {2  , new string[] {" 2", "血管杂音2", "" } },
            {3  , new string[] {" 3", "血管杂音3", "" } }
        };

        /// <summary>
        /// 阵挛
        /// </summary>
        public static Dictionary<int, string[]> Convulsions_Clonic = new Dictionary<int, string[]>() { { 0, new string[] { "0", "0%" } }, { 1, new string[] { "50", "50%" } }, { 2, new string[] { "100", "100%" } } };

        /// <summary>
        /// 强直
        /// </summary>
        public static Dictionary<int, string[]> Convulsions_Tonic = new Dictionary<int, string[]>() { { 0, new string[] { "0", "0%" } }, { 1, new string[] { "50", "50%" } }, { 2, new string[] { "100", "100%" } } };

        /// <summary>
        /// 脉搏强弱
        /// </summary>
        public static Dictionary<int, string> Pulse_Strength = new Dictionary<int, string>() { { 0, "无" }, { 1, "弱" }, { 2, "正常" }, { 3, "强" } };

        /// <summary>
        /// 治疗操作
        /// </summary>
        public static Dictionary<int, string> Operate = new Dictionary<int, string>() { { 0, "无" }, { 1, "有" }};

        /// <summary>
        /// 音量
        /// </summary>
        public static Dictionary<int, string> SoundVolumeList = new Dictionary<int, string>() { { 0, "0%" }, { 1, "10%" }, { 2, "20%" }, { 3, "30%" }, { 4, "40%" }, { 5, "50%" }, { 6, "60%" }, { 7, "70%" }, { 8, "80%" }, { 9, "90%" }, { 10, "100%" } };

        /// <summary>
        /// 出血量
        /// </summary>
        public static Dictionary<int, string[]> Haemorrhage_Volume = new Dictionary<int, string[]>() { { 0, new string[] { "0", "0%" } }, { 1, new string[] { "50", "50%" } }, { 2, new string[] { "100", "100%" } } };

        /// <summary>
        /// 尿量
        /// </summary>
        public static Dictionary<int, string> Urine_UPD = new Dictionary<int, string>() { { 0, "无尿" }, { 1, "正常" }, { 2, "多尿" }, { 3, "频尿" } };

        /// <summary>
        /// 药物分类
        /// </summary>
        public static Dictionary<int, string[]> DrugCategory = new Dictionary<int, string[]>()
        {
            { 0 , new string[] {"0", "", "" } },
            { 1 , new string[] {"1", "中枢神经兴奋药", "" } },
            { 2 , new string[] {"2", "抗休克血管活性药", "" } },
            { 3 , new string[] {"3", "抗胆碱药", "" } },
            { 4 , new string[] {"4", "强心药", "" } },
            { 5 , new string[] {"5", "抗心律失常", "" } },
            { 6 , new string[] {"6", "抗心衰", "" } },
            { 7 , new string[] {"7", "降压药", "" } },
            { 8 , new string[] {"8", "扩血管药", "" } },
            { 9 , new string[] {"9", "镇静", "" } },
            { 10, new string[] {"10", "解热", "" } },
            { 11, new string[] {"11", "解痉、镇痛", "" } },
            { 12, new string[] {"12", "止血类", "" } },
            { 13, new string[] {"13", "抗过敏", "" } },
            { 14, new string[] {"14", "激素类", "" } },
            { 15, new string[] {"15", "水电解质类", "" } },
            { 16, new string[] {"16", "酒精中毒", "" } },
            { 17, new string[] {"17", "解毒类", "" } },
            { 18, new string[] {"18", "蛇药", "" } },
            { 19, new string[] {"19", "利尿类", "" } },
            { 20, new string[] {"20", "脱水类", "" } },
            { 21, new string[] {"21", "平喘类", "" } },
            { 22, new string[] {"22", "止吐类", "" } },
            { 23, new string[] {"23", "抗生素类", "" } },
            { 24, new string[] {"24", "抗病毒类", "" } },
            { 25, new string[] {"25", "降糖药", "" } } };

        /// <summary>
        /// 药物名称
        /// </summary>
        public static Dictionary<int, string[]> DrugName = new Dictionary<int, string[]>()
        {
            { 0 , new string[] {"0", "", "0", "" } },
            { 1 , new string[] {"1", "尼可刹米（可拉明）注射液", "1", "中枢神经兴奋药" } },
            { 2 , new string[] {"2", "回苏灵注射液", "1", "中枢神经兴奋药" } },
            { 3 , new string[] {"3", "山梗菜碱（洛贝林）注射液", "1", "中枢神经兴奋药" } },
            { 4 , new string[] {"1", "肾上腺素（副肾）注射液", "2", "抗休克血管活性药" } },
            { 5 , new string[] {"2", "去甲肾上腺素（正肾）注射液", "2", "抗休克血管活性药" } },
            { 6 , new string[] {"3", "异丙肾上腺素（异丙肾）注射液", "2", "抗休克血管活性药" } },
            { 7 , new string[] {"4", "间羟胺（阿拉明）注射液", "2", "抗休克血管活性药" } },
            { 8 , new string[] {"5", "多巴胺注射液", "2", "抗休克血管活性药" } },
            { 9 , new string[] {"6", "多巴酚丁胺注射液", "2", "抗休克血管活性药" } },
            { 10, new string[] {"1", "阿托品注射液", "3", "抗胆碱药" } },
            { 11, new string[] {"2", "山莨菪碱（654-2）注射液", "3", "抗胆碱药" } },
            { 12, new string[] {"3", "东莨菪碱（海俄辛）注射液", "3", "抗胆碱药" } },
            { 13, new string[] {"4", "戊乙奎醚（长托宁）注射液", "3", "抗胆碱药" } },
            { 14, new string[] {"1", "去乙酰毛花苷（西地兰）注射液", "4", "强心药" } },
            { 15, new string[] {"2", "毒毛花苷k（毒k）注射液", "4", "强心药" } },
            { 16, new string[] {"1", "利多卡因注射液", "5", "抗心律失常" } },
            { 17, new string[] {"2", "心律平（普罗帕酮）注射液", "5", "抗心律失常" } },
            { 18, new string[] {"3", "胺碘酮注射液", "5", "抗心律失常" } },
            { 19, new string[] {"1", "新活素", "6", "抗心衰" } },
            { 20, new string[] {"1", "利血平", "7", "降压药" } },
            { 21, new string[] {"2", "硫酸镁注射液", "7", "降压药" } },
            { 22, new string[] {"1", "硝酸甘油", "8", "扩血管" } },
            { 23, new string[] {"2", "亚硝酸异戊酯吸入剂", "8", "扩血管" } },
            { 24, new string[] {"3", "酚妥拉明（立其丁）注射液", "8", "扩血管" } },
            { 25, new string[] {"1", "安定注射液", "9", "镇静" } },
            { 26, new string[] {"2", "苯巴比妥钠（鲁米那）注射液", "9", "镇静" } },
            { 27, new string[] {"1", "对乙酰氨基酚注射液", "10", "解热" } },
            { 28, new string[] {"2", "复方氨基比林注射液", "10", "解热" } },
            { 29, new string[] {"1", "间苯三酚注射液", "11", "解痉、镇痛" } },
            { 30, new string[] {"2", "曲马多注射液", "11", "解痉、镇痛" } },
            { 31, new string[] {"3", "杜冷丁注射液", "11", "解痉、镇痛" } },
            { 32, new string[] {"4", "吗啡注射液", "11", "解痉、镇痛" } },
            { 33, new string[] {"1", "立止血（注射用血凝酶）", "12", "止血类" } },
            { 34, new string[] {"2", "止血环酸", "12", "止血类" } },
            { 35, new string[] {"3", "止血芳酸注射液", "12", "止血类" } },
            { 36, new string[] {"4", "止血敏注射液", "12", "止血类" } },
            { 37, new string[] {"5", "凝血酶原复合物", "12", "止血类" } },
            { 38, new string[] {"1", "盐酸异丙嗪注射液", "13", "抗过敏" } },
            { 39, new string[] {"2", "苯海拉明注射液", "13", "抗过敏" } },
            { 40, new string[] {"3", "葡萄糖酸钙注射液", "13", "抗过敏" } },
            { 41, new string[] {"1", "地塞米松注射液", "14", "激素类" } },
            { 42, new string[] {"2", "氢化可的松注射液", "14", "激素类" } },
            { 43, new string[] {"3", "甲基强的松龙注射液", "14", "激素类" } },
            { 44, new string[] {"1", "50%的葡萄糖注射液", "15", "水电解质类" } },
            { 45, new string[] {"2", "5%的葡萄糖注射液", "15", "水电解质类" } },
            { 46, new string[] {"3", "10%氯化钠注射液", "15", "水电解质类" } },
            { 47, new string[] {"4", "0.9%氯化钠注射液", "15", "水电解质类" } },
            { 48, new string[] {"5", "10%氯化钾注射液", "15", "水电解质类" } },
            { 49, new string[] {"6", "5%碳酸氢钠注射液", "15", "水电解质类" } },
            { 50, new string[] {"1", "纳洛酮", "16", "酒精中毒" } },
            { 51, new string[] {"1", "阿托品注射液", "17", "解毒类" } },
            { 52, new string[] {"2", "戊乙奎醚（长托宁）注射液", "17", "解毒类" } },
            { 53, new string[] {"3", "碘解磷定注射液", "17", "解毒类" } },
            { 54, new string[] {"4", "二巯基丙醇注射液", "17", "解毒类" } },
            { 55, new string[] {"5", "二巯基丙磺酸钠注射液", "17", "解毒类" } },
            { 56, new string[] {"6", "美兰（亚甲蓝）注射液", "17", "解毒类" } },
            { 57, new string[] {"7", "乙酰胺（解氟灵）注射液", "17", "解毒类" } },
            { 58, new string[] {"8", "氟马西尼注射液", "17", "解毒类" } },
            { 59, new string[] {"9", "依地酸钙（EDTA钙）注射液", "17", "解毒类" } },
            { 60, new string[] {"1", "抗蝮蛇毒血清", "18", "蛇药" } },
            { 61, new string[] {"2", "抗五步蛇毒血清", "18", "蛇药" } },
            { 62, new string[] {"3", "抗银环蛇毒血清", "18", "蛇药" } },
            { 63, new string[] {"1", "速尿注射液", "19", "利尿类" } },
            { 64, new string[] {"1", "甘露醇注射液", "20", "脱水类" } },
            { 65, new string[] {"1", "氨茶碱注射液", "21", "平喘类" } },
            { 66, new string[] {"1", "胃复安(盐酸甲氧氯普胺注射液)", "22", "止吐类" } },
            { 67, new string[] {"1", "青霉素钠", "23", "抗生素类" } },
            { 68, new string[] {"2", "庆大霉素", "23", "抗生素类" } },
            { 69, new string[] {"3", "四环素", "23", "抗生素类" } },
            { 70, new string[] {"4", "红霉素", "23", "抗生素类" } },
            { 71, new string[] {"5", "阿奇霉素", "23", "抗生素类" } },
            { 72, new string[] {"6", "克林霉素", "23", "抗生素类" } },
            { 73, new string[] {"7", "左氧氟沙星", "23", "抗生素类" } },
            { 74, new string[] {"8", "万古霉素", "23", "抗生素类" } },
            { 75, new string[] {"9", "链霉素", "23", "抗生素类" } },
            { 76, new string[] {"10", "异烟肼注射液", "23", "抗生素类" } },
            { 77, new string[] {"11", "利福平注射液", "23", "抗生素类" } },
            { 78, new string[] {"12", "乙胺丁醇", "23", "抗生素类" } },
            { 79, new string[] {"13", "氟康唑", "23", "抗生素类" } },
            { 80, new string[] {"1", "阿昔洛韦", "24", "抗病毒类" } },
            { 81, new string[] {"1", "胰岛素注射液", "25", "降糖药" } } };

        /// <summary>
        /// 给药途径
        /// </summary>
        public static Dictionary<int, string[]> DrugRoute = new Dictionary<int, string[]>() {
            { 0 , new string[] {"0", "", "" } },
            { 1 , new string[] {"1", "口服", "" } },
            { 2 , new string[] {"2", "静脉注射", "" } },
            { 3 , new string[] {"3", "静脉滴注", "" } },
            { 4 , new string[] {"4", "肌内注射", "" } },
            { 5 , new string[] {"5", "皮内注射", "" } },
            { 6 , new string[] {"6", "皮下注射", "" } } };

        /// <summary>
        /// 剂量单位
        /// </summary>
        public static Dictionary<int, string[]> DrugDoseUnit = new Dictionary<int, string[]>() {
            { 0 , new string[] {"0", "", "" } },
            { 1 , new string[] {"1", "mg", "" } },
            { 2 , new string[] {"2", "ml", "" } }};




        //public static string[] HeartRateAlarmUpperLimit = new string[] { "80", "85", "90", "95", "100", "105", "110", "115", "120", "125", "130", "135", "140", "145", "150", "155", "160", "165", "170", "175", "180" };
        //public static string[] HeartRateAlarmLowerLimit = new string[] { "120", "115", "110", "105", "100", "95", "90", "85", "80", "75", "70", "65", "60", "55", "50", "45", "40", "35", "30", "25", "20", "15", "10", "5", };

        //public static string[] SystolicSyAlarmUpperLimit = new string[] { "80", "90", "100", "110", "120", "130", "140" };
        //public static string[] SystolicAlarmLowerLimit = new string[] { "80", "90", "100", "110", "120", "130", "140" };

        //public static string[] DiastolicAlarmUpperLimit = new string[] { "100", "90", "80", "70", "60", "50", "40", "30", "20", "10" };
        //public static string[] DiastolicAlarmLowerLimit = new string[] { "100", "90", "80", "70", "60", "50", "40", "30", "20", "10" };

        //public static string[] AverageAlarmUpperLimit = new string[] { "50", "60", "70", "80", "90", "100", "110" };
        //public static string[] AverageAlarmLowerLimit = new string[] { "50", "60", "70", "80", "90", "100", "110" };

      

        //public static string[] SPO2AlarmUpperLimit = new string[] { "95", "96", "97", "98", "99", "100" };
        //public static string[] SPO2AlarmLowerLimit = new string[] { "97", "96", "95", "94", "93", "92" };

        //public static string[] PRAlarmUpperLimit = new string[] { "60", "70", "80", "90", "100", "110", "120", "130", "140" };
        //public static string[] PRAlarmLowerLimit = new string[] { "90", "80", "70", "60", "50", "40", "30", "20", "10" };

        //public static string[] RRAlarmUpperLimit = new string[] { "20", "30", "40", "50", "60" };
        //public static string[] RRAlarmLowerLimit = new string[] { "30", "20", "10", "5", "1" };

    }

}

