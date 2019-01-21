﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework;
namespace YH.Virtual_ECG_Monitor
{
    public class WaveData_ECG_023 : WaveData_ECG
    {
        public WaveData_ECG_023()
            : base()
        {
            // TODO: Complete member initialization

            ID = "R23";
            Name = "心房颤动（AFib）";
            Remark = "";
            Rate = 80;    
            WaveData = resp_00;
        }

        public static float[,] resp_00 = new float[43,12] {
{36.55f,   32.04f,   33.02f,   34.07f,   34.07f,   34.03f,   33.51f,   38.13f,   34.52f,   32.49f,   33.06f,   35.09f },
{35.54f,   30.46f,   33.62f,   33.06f,   36.10f,   36.09f,   35.09f,   36.10f,   33.51f,   32.49f,   33.06f,   34.07f},
{33.51f,   33.06f,   32.04f,   33.06f,   35.09f,   36.67f,   35.54f,   34.07f,   33.51f,   38.13f,   33.06f,   33.51f},
{33.51f,   34.52f,   28.97f,   29.45f,   32.49f,   34.60f,   35.09f,   33.06f,   36.55f,   43.21f,   28.99f,   31.03f},
{31.03f,   32.49f,   31.05f,   28.43f,   33.51f,   33.57f,   36.10f,   33.06f,   39.15f,   40.16f,   27.98f,   31.48f},
{27.98f,   30.46f,   36.23f,   33.51f,   36.55f,   34.60f,   35.09f,   34.07f,   39.60f,   37.12f,   33.06f,   34.52f},
{30.46f,   32.04f,   37.86f,   37.12f,   36.55f,   33.57f,   33.51f,   34.07f,   36.55f,   35.09f,   37.57f,   34.07f},
{35.54f,   35.54f,   36.28f,   36.10f,   35.09f,   32.54f,   33.06f,   33.06f,   34.07f,   31.48f,   37.12f,   30.01f},
{37.12f,   50.74f,   37.34f,   35.54f,   36.10f,   34.03f,   34.52f,   33.06f,   32.49f,   29.45f,   36.10f,   31.48f},
{36.10f,   41.18f,   36.33f,   36.55f,   33.51f,   35.06f,   32.04f,   36.10f,   32.04f,   33.51f,   37.57f,   35.09f},
{36.55f,   38.13f,   37.96f,   39.15f,   29.45f,   33.57f,   20.32f,   3.35f,   33.51f,   35.54f,   66.30f,   44.23f},
{37.12f,   32.04f,   42.10f,   42.62f,   29.45f,   32.54f,   25.38f,   27.98f,   35.09f,   31.48f,   34.52f,   48.24f},
{46.19f,   28.43f,   36.40f,   36.10f,   32.49f,   33.57f,   33.51f,   34.07f,   34.52f,   28.99f,   33.06f,   38.58f},
{39.15f,   28.43f,   34.93f,   35.09f,   33.51f,   30.48f,   35.09f,   32.49f,   34.07f,   32.49f,   28.99f,   37.12f},
{34.52f,   28.43f,   31.29f,   31.03f,   40.12f,   20.17f,   35.09f,   32.04f,   36.10f,   39.60f,   25.95f,   31.48f},
{34.52f,   27.42f,   30.28f,   28.43f,   30.04f,   28.42f,   36.10f,   33.06f,   38.13f,   66.00f,   26.40f,   29.45f},
{31.03f,   28.43f,   31.34f,   28.99f,   34.52f,   36.09f,   37.12f,   33.06f,   31.03f,   25.95f,   26.40f,   31.03f},
{30.46f,   32.04f,   29.30f,   28.43f,   33.51f,   35.06f,   36.10f,   33.06f,   3.10f,   31.03f,   25.38f,   31.48f},
{31.48f,   33.06f,   30.35f,   27.42f,   34.07f,   32.54f,   35.54f,   34.52f,   32.49f,   26.40f,   31.03f,   30.46f},
{30.46f,   32.04f,   35.53f,   32.04f,   37.12f,   33.00f,   34.52f,   36.10f,   32.04f,   26.96f,   38.13f,   34.07f},
{32.04f,   33.51f,   38.65f,   37.57f,   37.57f,   34.03f,   32.49f,   37.12f,   32.04f,   28.43f,   39.15f,   36.10f},
{37.57f,   36.10f,   36.61f,   38.13f,   35.09f,   33.57f,   32.04f,   37.12f,   32.49f,   27.98f,   36.10f,   34.07f},
{40.16f,   36.10f,   35.14f,   36.10f,   34.07f,   34.03f,   34.07f,   36.55f,   32.04f,   28.43f,   35.09f,   31.48f},
{39.15f,   35.09f,   34.14f,   34.07f,   34.07f,   34.60f,   35.09f,   35.54f,   33.06f,   33.51f,   34.52f,   34.07f},
{35.09f,   35.09f,   34.16f,   34.07f,   32.49f,   34.60f,   35.54f,   35.09f,   37.12f,   37.12f,   32.04f,   37.12f},
{34.07f,   35.09f,   32.12f,   31.48f,   30.46f,   32.54f,   35.09f,   34.07f,   40.16f,   35.09f,   30.01f,   37.12f},
{33.51f,   31.03f,   33.17f,   30.46f,   32.04f,   31.97f,   35.09f,   35.09f,   40.62f,   34.52f,   33.06f,   36.10f},
{31.03f,   28.99f,   35.72f,   33.51f,   35.54f,   34.60f,   35.54f,   34.07f,   37.12f,   37.12f,   35.09f,   36.55f},
{31.48f,   31.03f,   35.28f,   35.09f,   36.10f,   36.09f,   35.09f,   32.49f,   34.52f,   38.58f,   31.48f,   33.06f},
{34.52f,   33.51f,   31.64f,   32.04f,   34.07f,   35.64f,   35.09f,   32.04f,   33.51f,   36.55f,   29.45f,   28.43f},
{34.07f,   50.69f,   32.70f,   30.01f,   35.09f,   35.64f,   36.10f,   35.54f,   33.06f,   36.55f,   32.49f,   28.99f},
{30.01f,   43.66f,   35.35f,   32.49f,   35.09f,   35.06f,   33.06f,   3.40f,   33.06f,   36.10f,   66.66f,   33.51f},
{31.48f,   36.10f,   38.47f,   38.13f,   33.51f,   32.54f,   20.34f,   22.90f,   34.07f,   29.45f,   37.12f,   39.15f},
{35.09f,   32.04f,   47.20f,   49.69f,   34.07f,   30.94f,   23.89f,   32.49f,   33.06f,   26.40f,   31.48f,   46.21f},
{44.23f,   29.45f,   41.61f,   41.63f,   36.10f,   32.54f,   31.03f,   33.06f,   32.04f,   30.46f,   25.95f,   35.09f},
{52.24f,   32.04f,   37.97f,   38.58f,   34.52f,   32.54f,   34.07f,   33.06f,   34.07f,   37.12f,   25.95f,   35.09f},
{38.58f,   33.51f,   32.38f,   33.06f,   34.52f,   21.66f,   35.09f,   34.07f,   39.15f,   66.00f,   26.96f,   32.49f},
{37.12f,   30.46f,   30.34f,   28.43f,   26.46f,   24.29f,   36.55f,   34.52f,   3.19f,   19.29f,   25.38f,   32.49f},
{31.48f,   28.43f,   31.39f,   28.43f,   30.46f,   33.57f,   37.57f,   33.51f,   30.46f,   28.99f,   28.43f,   36.55f},
{29.45f,   29.45f,   30.39f,   29.45f,   32.04f,   34.60f,   36.55f,   34.07f,   33.06f,   28.43f,   33.06f,   37.12f},
{31.03f,   30.46f,   29.84f,   28.43f,   34.52f,   33.57f,   36.10f,   35.09f,   32.04f,   32.49f,   33.06f,   33.51f},
{31.48f,   29.45f,   32.95f,   29.45f,   38.13f,   34.60f,   35.54f,   36.10f,   32.04f,   32.49f,   33.06f,   35.09f},
{36.55f,   32.04f,   35.04f,   33.06f,   34.07f,   34.03f,   34.52f,   38.13f,   34.52f,   32.49f,   33.06f,   35.09f},

        };
    }

}
