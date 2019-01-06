using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  YH.Virtual_ECG_Monitor
{
    public class WaveData_ABP_000 : WaveData_ABP
    {

        public WaveData_ABP_000() : base()
        {
            Rate = 80;
            BaseAMP = 0.2f;
            WaveData = abp_01;
        }

        public static float[] abp_01 = new float[64] {
             21.03f,            25.60f,            33.24f,            40.20f,            53.32f,
            70.95f,            87.35f,            106.49f,            129.87f,            152.97f,
            160.48f,            165.12f,            167.43f,            166.86f,            163.70f,
            157.12f,            153.54f,            150.93f,            147.76f,            147.61f,
            148.42f,            149.49f,            149.75f,            148.09f,            144.52f,
            138.62f,            131.35f,            126.00f,            119.56f,            110.51f,
            105.43f,            102.68f,            101.70f,            102.78f,            106.46f,
            107.95f,            108.89f,            109.70f,            108.86f,            106.65f,
            102.67f,            98.96f,            94.84f,            89.21f,            85.64f,
            81.38f,            76.17f,            71.91f,            66.97f,            64.08f,
            59.41f,            53.92f,            50.49f,            47.46f,            43.34f,
            40.72f,            37.70f,            33.85f,            31.64f,            27.93f,
            25.46f,            23.52f,            21.04f,            21.03f
        };
    }
}
