﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.ECGMonitor.WaveData.RESPWaveData
{
    public class RESPWaveData_032 : RESPWaveData
    {
        public RESPWaveData_032()
            : base()
        {
            // TODO: Complete member initialization

            ID = "R32";
            Name = "起搏器失去夺获";
            Remark = "";
            Rate = 8;
            InspCapacity = 3000;
            Ratio = 67;
            ETCO2 = 40;
            WaveData = resp_00;
        }

        public static float[,] resp_00 = new float[110,12] {
{32.94f,   32.54f,   32.04f,   32.54f,   33.57f,   33.57f,   33.57f,   33.46f,   33.06f,   32.54f,   33.06f,   32.04f },
{32.93f,   32.54f,   32.04f,   32.54f,   33.57f,   33.56f,   33.56f,   33.46f,   33.05f,   32.54f,   33.05f,   32.03f},
{32.93f,   32.54f,   32.04f,   32.54f,   33.57f,   33.55f,   33.55f,   33.46f,   33.04f,   32.54f,   33.04f,   32.02f},
{32.92f,   32.54f,   32.04f,   32.54f,   33.57f,   33.55f,   33.55f,   33.57f,   33.03f,   32.54f,   33.03f,   32.01f},
{32.91f,   32.54f,   32.04f,   32.54f,   33.57f,   33.54f,   33.54f,   33.46f,   33.02f,   32.54f,   33.02f,   32.01f},
{32.90f,   32.54f,   32.04f,   32.54f,   33.57f,   33.53f,   33.53f,   33.46f,   33.01f,   32.54f,   33.01f,   32.00f},
{33.00f,   32.54f,   32.04f,   32.54f,   33.57f,   33.52f,   33.52f,   33.46f,   33.00f,   32.54f,   33.00f,   31.99f},
{32.88f,   32.54f,   32.04f,   32.54f,   33.57f,   33.51f,   33.51f,   33.46f,   32.99f,   32.54f,   32.99f,   31.98f},
{32.87f,   32.54f,   32.04f,   32.54f,   33.57f,   33.50f,   33.50f,   33.46f,   32.99f,   32.54f,   32.99f,   31.97f},
{32.86f,   32.54f,   32.04f,   32.54f,   33.57f,   33.49f,   33.49f,   33.57f,   32.98f,   32.54f,   32.98f,   31.96f},
{32.86f,   32.54f,   32.04f,   32.54f,   33.57f,   33.48f,   33.48f,   33.46f,   32.97f,   32.54f,   32.97f,   31.95f},
{32.85f,   32.54f,   32.04f,   32.54f,   33.57f,   33.47f,   33.47f,   33.46f,   32.96f,   32.54f,   32.96f,   31.94f},
{32.84f,   32.54f,   32.04f,   32.54f,   33.57f,   33.47f,   33.47f,   33.46f,   32.95f,   32.54f,   32.95f,   31.94f},
{32.83f,   32.54f,   32.04f,   32.54f,   33.57f,   33.46f,   33.46f,   33.46f,   32.94f,   32.54f,   32.94f,   31.93f},
{32.82f,   32.54f,   32.04f,   32.54f,   33.57f,   33.45f,   33.45f,   33.46f,   32.93f,   32.54f,   32.93f,   31.92f},
{32.81f,   32.54f,   32.04f,   32.54f,   33.57f,   33.44f,   33.44f,   33.57f,   32.92f,   32.54f,   32.92f,   31.91f},
{32.80f,   32.54f,   32.04f,   32.54f,   33.57f,   33.43f,   33.43f,   33.57f,   32.91f,   32.54f,   32.91f,   31.90f},
{32.79f,   35.64f,   32.04f,   32.54f,   33.57f,   33.42f,   33.42f,   33.46f,   32.90f,   32.54f,   32.90f,   31.89f},
{32.78f,   1.00f,   32.04f,   1.20f,   1.20f,   33.41f,   33.41f,   33.46f,   32.89f,   32.54f,   32.89f,   31.88f},
{1.20f,   46.41f,   1.20f,   45.26f,   40.79f,   33.40f,   66.00f,   66.00f,   80.00f,   2.10f,   32.89f,   31.87f},
{42.34f,   59.35f,   40.62f,   57.29f,   44.92f,   66.00f,   1.21f,   1.20f,   50.69f,   50.15f,   1.20f,   1.20f},
{45.54f,   63.48f,   53.36f,   68.33f,   46.98f,   16.88f,   9.08f,   17.39f,   53.82f,   66.00f,   44.28f,   50.58f},
{51.71f,   57.29f,   59.46f,   56.15f,   44.92f,   10.12f,   26.14f,   25.00f,   43.80f,   50.07f,   66.00f,   62.18f},
{52.85f,   48.47f,   49.30f,   48.47f,   39.76f,   8.62f,   35.41f,   37.13f,   28.08f,   38.73f,   50.09f,   48.97f},
{54.19f,   39.76f,   39.60f,   39.19f,   33.00f,   11.70f,   36.43f,   39.19f,   15.50f,   33.46f,   41.44f,   33.28f},
{50.34f,   35.64f,   34.52f,   31.51f,   24.75f,   23.49f,   37.91f,   41.25f,   13.97f,   32.54f,   31.31f,   28.19f},
{46.04f,   34.60f,   31.03f,   29.91f,   19.59f,   36.43f,   39.97f,   43.31f,   20.25f,   32.08f,   28.04f,   27.73f},
{41.41f,   34.60f,   28.99f,   27.84f,   17.53f,   39.98f,   40.53f,   44.92f,   26.99f,   30.94f,   27.56f,   27.27f},
{35.31f,   34.60f,   25.95f,   25.78f,   20.17f,   41.00f,   42.01f,   44.34f,   31.76f,   29.91f,   26.97f,   26.70f},
{29.21f,   34.60f,   24.37f,   24.86f,   25.32f,   38.93f,   43.61f,   41.71f,   32.79f,   28.88f,   25.92f,   26.13f},
{24.69f,   35.64f,   23.92f,   24.29f,   29.45f,   36.40f,   44.06f,   37.13f,   33.83f,   28.42f,   25.44f,   25.67f},
{22.65f,   35.06f,   26.40f,   26.81f,   31.97f,   35.36f,   40.50f,   35.06f,   33.82f,   28.88f,   25.43f,   26.11f},
{20.61f,   34.03f,   30.01f,   30.48f,   33.57f,   35.35f,   36.82f,   34.60f,   34.86f,   30.94f,   25.42f,   28.69f},
{18.57f,   33.57f,   32.04f,   32.54f,   34.60f,   34.77f,   34.75f,   34.60f,   34.27f,   32.54f,   27.98f,   30.72f},
{19.57f,   33.57f,   33.06f,   33.46f,   34.60f,   34.30f,   34.28f,   34.60f,   32.75f,   33.46f,   31.11f,   31.72f},
{24.19f,   33.57f,   33.06f,   33.46f,   34.60f,   34.29f,   34.27f,   34.60f,   32.16f,   33.46f,   32.73f,   32.62f},
{29.15f,   33.57f,   33.06f,   33.46f,   34.60f,   34.28f,   34.26f,   34.60f,   34.24f,   33.46f,   33.65f,   32.61f},
{32.30f,   33.57f,   33.06f,   33.46f,   34.60f,   34.27f,   34.25f,   34.60f,   34.82f,   33.46f,   33.64f,   32.60f},
{33.64f,   33.57f,   33.06f,   33.46f,   34.60f,   34.26f,   34.25f,   34.60f,   33.76f,   33.46f,   33.63f,   32.59f},
{33.63f,   33.57f,   33.06f,   33.46f,   34.60f,   34.25f,   34.24f,   34.60f,   33.75f,   33.46f,   33.62f,   32.58f},
{33.62f,   33.57f,   33.06f,   33.46f,   34.60f,   34.25f,   34.23f,   34.60f,   33.74f,   33.46f,   33.62f,   32.69f},
{33.61f,   33.57f,   33.06f,   33.46f,   34.60f,   34.24f,   34.22f,   34.60f,   33.73f,   33.46f,   33.61f,   32.68f},
{33.61f,   33.57f,   33.06f,   33.46f,   34.60f,   34.23f,   34.21f,   34.60f,   33.72f,   33.46f,   33.60f,   32.67f},
{33.60f,   33.57f,   33.06f,   33.46f,   34.60f,   34.22f,   34.20f,   34.60f,   33.71f,   33.46f,   33.71f,   32.55f},
{33.59f,   33.57f,   33.06f,   33.57f,   34.60f,   34.21f,   34.19f,   34.60f,   33.71f,   33.46f,   33.70f,   32.54f},
{33.58f,   33.57f,   33.06f,   33.46f,   34.60f,   34.20f,   34.18f,   34.60f,   33.70f,   33.46f,   33.69f,   32.53f},
{33.68f,   33.57f,   33.06f,   33.46f,   34.60f,   34.19f,   34.17f,   34.60f,   33.69f,   33.57f,   33.56f,   32.52f},
{33.56f,   33.57f,   33.06f,   33.46f,   34.60f,   34.18f,   34.16f,   34.60f,   33.68f,   33.46f,   33.55f,   32.51f},
{33.55f,   33.57f,   33.06f,   33.46f,   34.60f,   34.17f,   34.16f,   34.60f,   33.67f,   33.46f,   33.54f,   32.50f},
{33.54f,   33.57f,   33.06f,   33.46f,   34.60f,   34.16f,   34.15f,   34.60f,   33.66f,   33.46f,   33.53f,   32.49f},
{33.54f,   33.57f,   33.06f,   33.46f,   34.60f,   34.16f,   34.14f,   34.60f,   33.65f,   33.46f,   33.52f,   32.48f},
{33.53f,   33.57f,   33.06f,   33.46f,   34.60f,   34.15f,   34.13f,   34.60f,   33.64f,   33.46f,   33.52f,   32.48f},
{33.52f,   33.57f,   33.06f,   33.46f,   34.60f,   34.14f,   34.12f,   34.60f,   33.63f,   33.57f,   33.51f,   32.47f},
{33.51f,   33.57f,   33.06f,   33.46f,   34.60f,   34.13f,   34.11f,   34.60f,   33.62f,   33.46f,   33.50f,   32.57f},
{33.50f,   33.57f,   33.06f,   33.46f,   34.60f,   34.12f,   34.10f,   34.60f,   33.61f,   33.46f,   33.49f,   32.56f},
{33.49f,   33.57f,   33.06f,   33.46f,   34.60f,   34.11f,   34.09f,   34.60f,   33.60f,   33.46f,   33.48f,   32.55f},
{33.48f,   33.57f,   32.49f,   33.46f,   34.60f,   34.10f,   34.08f,   34.60f,   33.60f,   33.46f,   33.59f,   32.43f},
{33.47f,   33.57f,   32.04f,   33.46f,   34.60f,   34.09f,   34.07f,   35.64f,   33.59f,   33.46f,   33.58f,   32.42f},
{33.46f,   33.57f,   32.04f,   33.46f,   34.60f,   34.08f,   34.07f,   35.64f,   33.58f,   33.46f,   33.45f,   32.41f},
{33.57f,   33.57f,   32.04f,   33.46f,   34.60f,   34.08f,   34.06f,   35.64f,   33.57f,   33.46f,   33.44f,   32.07f},
{33.45f,   33.57f,   32.04f,   33.46f,   34.60f,   34.07f,   35.08f,   36.67f,   32.98f,   33.11f,   33.43f,   31.49f},
{33.44f,   33.57f,   32.04f,   33.46f,   34.60f,   34.06f,   35.07f,   39.30f,   32.50f,   32.54f,   33.42f,   31.03f},
{33.43f,   33.00f,   32.04f,   32.08f,   34.60f,   34.05f,   31.97f,   41.71f,   32.49f,   32.54f,   33.42f,   30.46f},
{33.42f,   35.64f,   31.48f,   28.42f,   35.06f,   34.04f,   18.55f,   25.21f,   33.53f,   32.54f,   33.41f,   29.89f},
{33.41f,   52.14f,   34.52f,   37.70f,   39.19f,   34.03f,   6.17f,   10.08f,   34.57f,   64.33f,   31.42f,   29.43f},
{33.40f,   60.39f,   48.74f,   66.00f,   42.85f,   34.02f,   12.81f,   28.53f,   45.50f,   33.46f,   39.79f,   47.58f},
{33.39f,   45.38f,   50.32f,   37.13f,   40.22f,   35.04f,   24.71f,   39.19f,   53.68f,   31.51f,   62.05f,   60.44f},
{39.03f,   31.97f,   39.60f,   33.46f,   29.45f,   24.15f,   31.35f,   41.25f,   29.77f,   34.60f,   29.28f,   45.65f},
{49.62f,   30.48f,   36.10f,   33.57f,   25.78f,   8.21f,   33.98f,   43.43f,   23.48f,   35.64f,   31.83f,   32.33f},
{40.14f,   31.51f,   34.52f,   33.46f,   30.94f,   18.52f,   35.00f,   44.92f,   31.85f,   35.64f,   33.33f,   31.98f},
{24.90f,   32.54f,   34.07f,   33.46f,   34.60f,   36.50f,   34.99f,   46.41f,   34.98f,   36.67f,   33.32f,   32.31f},
{27.26f,   32.54f,   33.06f,   34.60f,   36.67f,   38.09f,   34.98f,   48.47f,   36.60f,   37.70f,   34.48f,   32.30f},
{31.42f,   33.57f,   33.06f,   34.60f,   36.67f,   34.42f,   34.97f,   51.10f,   37.64f,   39.30f,   34.47f,   33.42f},
{32.43f,   33.57f,   32.49f,   34.60f,   36.09f,   32.92f,   33.36f,   53.63f,   37.63f,   41.25f,   35.51f,   33.41f},
{33.32f,   34.60f,   32.04f,   34.60f,   35.64f,   32.34f,   32.32f,   52.59f,   39.14f,   43.89f,   36.55f,   33.40f},
{33.31f,   36.67f,   31.48f,   35.64f,   36.67f,   31.87f,   31.28f,   46.98f,   41.80f,   45.95f,   38.05f,   34.86f},
{34.43f,   38.73f,   30.46f,   36.67f,   38.73f,   31.29f,   30.24f,   41.25f,   44.36f,   45.83f,   40.60f,   36.88f},
{35.89f,   39.76f,   30.46f,   37.13f,   40.79f,   29.22f,   30.80f,   36.67f,   46.44f,   41.71f,   42.81f,   39.02f},
{37.91f,   38.16f,   32.49f,   36.09f,   40.22f,   27.14f,   32.86f,   35.06f,   47.48f,   37.70f,   39.65f,   39.91f},
{40.95f,   34.60f,   34.07f,   34.49f,   38.16f,   24.61f,   33.88f,   34.60f,   44.33f,   34.49f,   36.04f,   37.87f},
{43.54f,   33.57f,   35.09f,   33.46f,   35.64f,   24.03f,   33.87f,   34.60f,   38.62f,   33.46f,   33.82f,   34.36f},
{40.93f,   33.57f,   34.52f,   33.57f,   34.60f,   27.69f,   33.86f,   34.60f,   35.46f,   33.46f,   33.34f,   32.77f},
{36.41f,   33.57f,   34.07f,   33.46f,   34.60f,   31.23f,   33.85f,   34.60f,   34.41f,   33.57f,   33.22f,   32.20f},
{33.81f,   33.57f,   34.07f,   33.46f,   34.60f,   32.83f,   33.84f,   34.60f,   33.82f,   33.46f,   33.21f,   32.19f},
{33.23f,   33.57f,   33.51f,   33.46f,   34.60f,   32.82f,   33.83f,   34.60f,   33.34f,   33.46f,   33.20f,   32.18f},
{33.34f,   33.57f,   33.06f,   33.46f,   34.60f,   32.81f,   33.82f,   34.60f,   33.33f,   33.46f,   33.19f,   32.18f},
{33.22f,   33.57f,   33.06f,   33.46f,   34.60f,   32.80f,   33.82f,   34.60f,   33.32f,   33.46f,   33.18f,   32.17f},
{33.21f,   33.57f,   33.06f,   33.46f,   34.60f,   32.79f,   33.81f,   34.60f,   33.31f,   33.46f,   33.17f,   32.16f},
{33.20f,   33.57f,   33.06f,   33.46f,   34.60f,   32.78f,   33.80f,   34.60f,   33.30f,   33.46f,   33.16f,   32.15f},
{33.19f,   33.57f,   33.06f,   33.46f,   34.60f,   32.77f,   33.79f,   34.60f,   33.30f,   33.46f,   33.15f,   32.14f},
{33.18f,   33.57f,   33.06f,   33.46f,   34.60f,   33.22f,   33.78f,   50.28f,   33.29f,   33.46f,   33.14f,   32.13f},
{33.17f,   33.57f,   33.06f,   33.46f,   34.60f,   33.79f,   33.77f,   33.46f,   33.28f,   16.29f,   33.13f,   32.24f},
{33.16f,   33.57f,   33.06f,   32.54f,   34.60f,   33.78f,   33.76f,   33.46f,   33.27f,   32.54f,   33.13f,   32.23f},
{33.16f,   16.26f,   16.90f,   16.16f,   16.32f,   33.77f,   50.43f,   33.46f,   33.26f,   32.54f,   32.18f,   32.23f},
{33.15f,   32.54f,   32.04f,   32.54f,   33.57f,   33.76f,   32.71f,   33.57f,   33.25f,   32.54f,   16.10f,   31.19f},
{33.14f,   32.54f,   32.04f,   32.54f,   33.57f,   50.43f,   32.70f,   33.46f,   50.04f,   32.54f,   32.17f,   16.67f},
{33.14f,   32.54f,   32.04f,   32.54f,   33.57f,   32.71f,   32.69f,   33.46f,   32.18f,   32.54f,   32.17f,   31.18f},
{32.22f,   32.54f,   32.04f,   32.54f,   33.57f,   32.70f,   32.68f,   33.46f,   32.17f,   32.54f,   32.16f,   31.18f},
{18.34f,   32.54f,   32.04f,   32.54f,   33.57f,   32.69f,   32.68f,   33.46f,   32.17f,   32.54f,   32.15f,   31.17f},
{32.21f,   32.54f,   32.04f,   32.54f,   33.57f,   32.69f,   32.67f,   33.46f,   32.16f,   32.54f,   32.14f,   31.16f},
{32.20f,   32.54f,   32.04f,   32.54f,   33.57f,   32.68f,   32.66f,   33.57f,   32.15f,   32.54f,   32.13f,   31.15f},
{32.19f,   32.54f,   32.04f,   32.54f,   33.57f,   32.67f,   32.65f,   33.46f,   32.14f,   32.54f,   32.12f,   31.14f},
{32.18f,   32.54f,   32.04f,   32.54f,   33.57f,   32.66f,   32.64f,   33.46f,   32.13f,   32.54f,   32.11f,   31.13f},
{32.17f,   32.54f,   32.04f,   32.54f,   33.57f,   32.65f,   32.63f,   33.46f,   32.12f,   32.54f,   32.10f,   31.12f},
{32.16f,   32.54f,   32.04f,   32.54f,   33.57f,   32.64f,   32.62f,   33.46f,   32.11f,   32.54f,   32.09f,   31.11f},
{32.16f,   32.54f,   32.04f,   32.54f,   33.57f,   32.63f,   32.61f,   33.46f,   32.10f,   32.54f,   32.08f,   31.11f},
{32.15f,   32.54f,   32.04f,   32.54f,   33.57f,   32.62f,   32.60f,   33.57f,   32.09f,   32.54f,   32.07f,   31.10f},
{32.14f,   32.54f,   32.04f,   32.54f,   33.57f,   32.61f,   32.60f,   33.57f,   32.08f,   32.54f,   32.07f,   31.09f},
{32.13f,   32.54f,   32.04f,   32.54f,   33.57f,   32.60f,   32.59f,   33.46f,   32.07f,   32.54f,   32.06f,   31.08f},
{32.12f,   32.54f,   32.04f,   32.54f,   33.57f,   32.60f,   32.58f,   33.46f,   32.07f,   32.54f,   32.05f,   31.07f},

        };
    }

}
