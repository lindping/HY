﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YH.Virtual_ECG_Monitor
{
    public class WaveData_ECG_026 : WaveData_ECG
    {
        public WaveData_ECG_026()
            : base()
        {
            // TODO: Complete member initialization

            ID = "R26";
            Name = "室性心动过速（VT）";
            Remark = "";
            Rate = 80;
        
            WaveData = resp_00;
        }

        public static float[,] resp_00 = new float[310,12] {
{30.48f,   49.50f,   34.49f,   40.33f,   26.81f,   28.87f,   25.32f,   37.87f,   44.58f,   40.22f,   39.60f,   42.00f } ,
{34.60f,   51.10f,   42.63f,   46.52f,   18.56f,   27.24f,   22.23f,   40.63f,   49.24f,   48.47f,   52.80f,   50.00f},
{37.12f,   51.09f,   53.97f,   53.16f,   33.57f,   26.20f,   19.59f,   41.11f,   51.92f,   38.16f,   61.93f,   56.56f},
{39.18f,   50.05f,   47.44f,   43.30f,   25.21f,   24.69f,   21.66f,   38.95f,   41.44f,   29.45f,   59.45f,   41.00f},
{39.75f,   46.38f,   33.57f,   33.79f,   36.09f,   29.93f,   26.81f,   37.87f,   34.57f,   25.78f,   45.68f,   29.56f},
{38.14f,   42.82f,   29.45f,   28.29f,   31.05f,   38.31f,   28.99f,   35.83f,   27.82f,   19.59f,   30.45f,   25.56f},
{36.65f,   37.09f,   24.29f,   23.82f,   39.76f,   41.34f,   30.48f,   33.06f,   23.05f,   13.98f,   15.78f,   17.56f},
{32.98f,   31.47f,   19.59f,   19.57f,   41.82f,   44.60f,   32.08f,   31.38f,   19.90f,   10.89f,   4.04f,   9.00f},
{30.45f,   26.30f,   15.93f,   17.05f,   43.31f,   47.28f,   33.46f,   29.69f,   17.34f,   8.25f,   4.59f,   2.56f},
{26.78f,   20.57f,   16.39f,   15.56f,   44.92f,   48.68f,   34.60f,   28.73f,   15.71f,   7.22f,   14.74f,   1.56f},
{22.66f,   15.98f,   13.41f,   14.41f,   46.98f,   48.69f,   35.64f,   27.65f,   15.25f,   6.19f,   25.91f,   1.56f},
{21.62f,   19.07f,   12.83f,   13.95f,   49.04f,   48.81f,   36.67f,   27.65f,   15.25f,   7.22f,   32.00f,   2.00f},
{26.77f,   26.74f,   13.98f,   14.98f,   48.47f,   47.76f,   36.67f,   27.05f,   16.30f,   10.31f,   38.09f,   8.56f},
{32.50f,   34.52f,   15.58f,   18.98f,   45.49f,   45.67f,   36.09f,   26.57f,   17.81f,   16.50f,   48.69f,   18.00f},
{36.62f,   40.13f,   18.68f,   24.25f,   42.40f,   43.58f,   35.06f,   26.57f,   20.95f,   22.69f,   59.85f,   25.00f},
{39.71f,   43.79f,   25.90f,   28.49f,   39.30f,   41.95f,   34.03f,   26.57f,   24.68f,   25.32f,   62.44f,   29.56f},
{42.23f,   46.88f,   33.11f,   32.04f,   38.16f,   39.97f,   33.00f,   26.57f,   27.24f,   27.39f,   48.67f,   31.56f},
{44.86f,   49.97f,   36.67f,   33.98f,   36.67f,   38.35f,   31.51f,   28.13f,   30.96f,   33.00f,   28.93f,   33.56f},
{47.38f,   52.48f,   41.36f,   37.64f,   34.60f,   36.72f,   28.42f,   31.50f,   37.71f,   41.82f,   18.21f,   39.56f},
{48.98f,   54.54f,   47.44f,   44.29f,   31.97f,   34.63f,   22.69f,   35.10f,   44.00f,   48.47f,   13.69f,   50.56f},
{50.01f,   53.50f,   54.20f,   52.08f,   20.74f,   29.39f,   23.26f,   32.58f,   42.95f,   37.70f,   10.64f,   57.00f},
{49.43f,   51.43f,   56.15f,   46.92f,   8.36f,   23.69f,   28.88f,   30.30f,   36.20f,   24.29f,   9.62f,   49.00f},
{48.97f,   49.36f,   41.36f,   37.63f,   12.38f,   26.72f,   31.51f,   30.90f,   29.92f,   17.07f,   11.09f,   38.56f},
{48.40f,   46.84f,   48.13f,   27.78f,   21.66f,   28.94f,   33.00f,   30.90f,   26.19f,   12.38f,   15.71f,   27.56f},
{46.91f,   42.71f,   35.64f,   19.52f,   30.02f,   29.99f,   34.60f,   30.90f,   23.63f,   10.31f,   21.80f,   19.00f},
{43.81f,   36.97f,   30.02f,   15.97f,   35.06f,   31.51f,   35.64f,   31.98f,   21.53f,   8.82f,   27.89f,   15.56f},
{40.14f,   32.38f,   23.15f,   12.76f,   38.73f,   33.14f,   36.67f,   31.98f,   21.53f,   7.22f,   33.41f,   14.00f},
{37.61f,   24.58f,   19.14f,   10.35f,   42.74f,   34.07f,   37.70f,   31.98f,   20.95f,   6.76f,   38.04f,   14.56f},
{31.88f,   22.52f,   15.93f,   9.77f,   45.95f,   34.08f,   37.70f,   31.98f,   19.90f,   7.22f,   50.22f,   18.56f},
{36.00f,   29.27f,   12.38f,   9.77f,   45.38f,   34.66f,   37.70f,   31.98f,   20.49f,   9.85f,   61.83f,   25.00f},
{42.19f,   34.88f,   10.89f,   12.29f,   43.31f,   34.66f,   37.70f,   31.98f,   20.49f,   13.41f,   63.41f,   32.00f},
{46.31f,   39.00f,   10.89f,   15.95f,   40.79f,   33.15f,   37.70f,   33.06f,   20.95f,   16.50f,   55.73f,   36.56f},
{49.97f,   42.66f,   11.92f,   18.47f,   38.16f,   31.53f,   37.70f,   35.10f,   23.63f,   18.56f,   43.09f,   39.00f},
{52.49f,   45.75f,   14.09f,   22.70f,   36.09f,   30.48f,   37.70f,   37.39f,   26.19f,   23.26f,   30.34f,   41.56f},
{54.55f,   49.29f,   19.59f,   27.17f,   34.49f,   29.44f,   37.70f,   38.95f,   28.29f,   31.51f,   18.15f,   46.56f},
{56.15f,   52.95f,   27.39f,   30.37f,   33.00f,   27.81f,   36.67f,   42.20f,   33.52f,   40.22f,   10.59f,   55.56f},
{57.64f,   55.01f,   32.54f,   36.56f,   31.51f,   26.30f,   32.08f,   41.60f,   40.86f,   44.34f,   9.01f,   45.00f},
{58.21f,   56.03f,   35.52f,   46.41f,   17.99f,   23.74f,   27.84f,   34.62f,   36.20f,   31.97f,   7.99f,   32.00f},
{57.18f,   55.45f,   38.96f,   50.42f,   4.58f,   18.51f,   34.49f,   29.81f,   27.82f,   22.23f,   9.57f,   19.00f},
{54.54f,   52.93f,   49.27f,   40.22f,   5.61f,   20.02f,   40.22f,   29.21f,   24.10f,   18.56f,   19.16f,   9.56f},
{49.95f,   47.77f,   46.41f,   30.93f,   14.44f,   24.80f,   42.28f,   28.13f,   20.49f,   12.95f,   31.90f,   6.56f},
{45.25f,   44.09f,   42.05f,   24.74f,   25.21f,   27.36f,   44.34f,   27.65f,   17.34f,   5.73f,   36.41f,   4.56f},
{42.73f,   42.02f,   36.67f,   17.52f,   34.49f,   29.46f,   45.95f,   28.73f,   17.34f,   2.64f,   42.05f,   6.00f},
{40.66f,   37.43f,   28.42f,   12.82f,   41.25f,   32.14f,   45.38f,   30.30f,   18.39f,   15.01f,   53.67f,   9.00f},
{35.50f,   32.27f,   17.53f,   12.82f,   46.98f,   34.71f,   44.92f,   32.46f,   21.53f,   25.32f,   62.80f,   14.56f},
{29.31f,   32.72f,   9.85f,   13.84f,   51.56f,   36.34f,   43.89f,   35.22f,   24.68f,   29.91f,   57.26f,   20.00f},
{28.73f,   36.39f,   9.05f,   14.87f,   55.69f,   37.39f,   41.25f,   38.35f,   28.87f,   37.70f,   42.03f,   24.00f},
{31.37f,   38.44f,   5.61f,   15.90f,   57.75f,   37.86f,   37.13f,   41.60f,   34.11f,   46.98f,   26.23f,   30.00f},
{32.39f,   39.93f,   4.70f,   17.96f,   54.20f,   36.35f,   34.03f,   44.36f,   39.34f,   38.73f,   19.69f,   38.56f},
{33.42f,   44.62f,   5.96f,   25.63f,   48.47f,   35.30f,   31.05f,   46.04f,   42.95f,   31.97f,   10.55f,   47.00f},
{35.94f,   49.77f,   11.69f,   35.94f,   44.46f,   33.21f,   28.42f,   47.73f,   46.10f,   29.91f,   7.50f,   54.00f},
{39.60f,   52.28f,   17.53f,   41.10f,   38.73f,   31.12f,   26.24f,   49.17f,   51.92f,   25.32f,   6.48f,   38.00f},
{42.12f,   53.88f,   22.23f,   47.40f,   31.05f,   27.86f,   23.26f,   49.77f,   56.11f,   22.23f,   6.93f,   27.00f},
{43.72f,   54.33f,   26.35f,   54.61f,   24.29f,   25.42f,   25.32f,   47.73f,   46.10f,   20.17f,   7.94f,   23.00f},
{44.18f,   52.84f,   34.03f,   46.36f,   32.08f,   28.45f,   29.91f,   46.04f,   39.81f,   19.59f,   14.59f,   15.56f},
{42.69f,   50.20f,   45.38f,   33.29f,   41.25f,   34.74f,   32.08f,   43.28f,   34.57f,   18.56f,   25.76f,   7.00f},
{40.05f,   48.13f,   44.92f,   28.13f,   47.55f,   36.84f,   34.03f,   40.15f,   30.96f,   18.10f,   33.88f,   1.56f},
{36.95f,   44.57f,   33.11f,   24.69f,   50.99f,   38.94f,   35.64f,   38.47f,   29.33f,   19.59f,   37.37f,   1.00f},
{32.82f,   39.87f,   26.35f,   21.48f,   54.20f,   40.57f,   36.67f,   36.79f,   28.29f,   23.26f,   45.49f,   1.00f},
{29.27f,   34.70f,   21.20f,   19.42f,   56.72f,   41.50f,   37.70f,   35.70f,   26.19f,   29.91f,   57.22f,   3.00f},
{24.11f,   27.02f,   15.93f,   17.92f,   59.81f,   42.67f,   38.73f,   34.62f,   24.68f,   36.09f,   63.76f,   10.56f},
{18.95f,   23.92f,   12.38f,   16.32f,   62.45f,   43.72f,   39.76f,   34.14f,   22.58f,   39.19f,   57.21f,   21.00f},
{21.01f,   28.04f,   9.28f,   15.86f,   62.91f,   44.77f,   39.76f,   35.22f,   22.00f,   41.82f,   37.92f,   29.56f},
{26.16f,   33.19f,   6.76f,   16.31f,   59.81f,   43.73f,   39.30f,   35.22f,   21.53f,   48.01f,   21.67f,   34.56f},
{30.28f,   36.73f,   5.61f,   20.89f,   55.69f,   42.57f,   38.16f,   35.22f,   22.00f,   56.72f,   14.56f,   37.00f},
{32.34f,   38.33f,   6.19f,   26.62f,   51.56f,   41.06f,   37.13f,   35.83f,   24.68f,   62.45f,   9.93f,   40.56f},
{33.83f,   39.36f,   8.36f,   29.71f,   48.93f,   40.13f,   35.64f,   36.79f,   26.77f,   49.50f,   7.89f,   48.00f},
{35.43f,   39.35f,   11.92f,   30.28f,   46.98f,   37.92f,   34.03f,   38.95f,   27.24f,   34.03f,   7.44f,   58.56f},
{36.46f,   40.38f,   20.05f,   31.88f,   44.92f,   37.34f,   31.51f,   42.20f,   31.43f,   25.32f,   9.47f,   52.56f},
{37.49f,   39.80f,   27.39f,   38.06f,   41.36f,   36.41f,   25.32f,   44.96f,   38.76f,   18.10f,   14.99f,   39.56f},
{38.52f,   39.79f,   31.40f,   45.74f,   29.33f,   32.23f,   25.78f,   41.11f,   42.49f,   14.44f,   21.65f,   26.56f},
{39.54f,   39.33f,   35.06f,   41.04f,   16.61f,   28.50f,   30.48f,   38.47f,   38.76f,   10.31f,   28.19f,   15.00f},
{40.57f,   37.72f,   41.82f,   32.33f,   20.63f,   32.23f,   32.54f,   36.79f,   33.06f,   7.22f,   33.26f,   9.00f},
{40.57f,   36.68f,   49.96f,   25.10f,   29.45f,   34.80f,   34.03f,   36.31f,   29.33f,   4.70f,   41.38f,   5.00f},
{39.99f,   34.15f,   51.68f,   17.88f,   37.24f,   37.48f,   35.64f,   35.70f,   27.82f,   3.67f,   57.17f,   2.56f},
{38.96f,   30.02f,   33.34f,   14.21f,   41.71f,   40.16f,   36.67f,   34.62f,   26.77f,   6.19f,   64.73f,   4.56f},
{35.86f,   25.89f,   39.99f,   11.57f,   44.92f,   42.14f,   36.67f,   33.54f,   26.77f,   10.31f,   60.21f,   10.56f},
{34.37f,   20.16f,   30.36f,   9.62f,   48.47f,   44.24f,   36.67f,   32.58f,   27.82f,   13.41f,   49.49f,   19.56f},
{31.27f,   14.54f,   24.75f,   8.59f,   51.56f,   45.87f,   36.67f,   31.98f,   28.87f,   16.04f,   37.30f,   27.00f},
{29.67f,   19.69f,   17.53f,   8.59f,   50.53f,   46.92f,   36.09f,   31.98f,   29.92f,   21.20f,   24.10f,   32.56f},
{36.42f,   25.87f,   12.38f,   10.19f,   48.47f,   45.88f,   35.64f,   33.06f,   30.96f,   29.45f,   13.94f,   38.00f},
{40.55f,   29.41f,   9.28f,   12.59f,   46.41f,   43.78f,   34.60f,   34.62f,   32.48f,   34.60f,   9.43f,   45.56f},
{43.64f,   32.50f,   6.19f,   15.80f,   45.38f,   40.64f,   34.03f,   37.27f,   35.15f,   43.31f,   8.41f,   57.00f},
{45.70f,   34.56f,   3.67f,   20.38f,   44.92f,   38.55f,   33.00f,   40.03f,   38.76f,   38.73f,   8.40f,   55.00f},
{47.18f,   36.61f,   2.64f,   24.61f,   44.34f,   36.46f,   31.51f,   42.80f,   41.90f,   25.78f,   12.91f,   42.56f},
{48.78f,   39.24f,   3.09f,   26.10f,   43.89f,   33.32f,   28.88f,   47.13f,   46.10f,   20.17f,   25.10f,   29.00f},
{49.81f,   41.30f,   6.19f,   31.25f,   43.31f,   30.65f,   24.29f,   47.13f,   54.48f,   16.04f,   34.23f,   18.00f},
{49.81f,   41.75f,   12.83f,   40.99f,   31.51f,   26.46f,   18.56f,   41.11f,   57.62f,   9.28f,   36.82f,   13.56f},
{48.77f,   40.71f,   21.20f,   44.08f,   19.82f,   20.18f,   23.83f,   36.79f,   49.82f,   5.16f,   45.39f,   10.56f},
{46.14f,   38.65f,   26.81f,   33.31f,   21.66f,   21.23f,   16.96f,   35.10f,   44.58f,   3.09f,   59.16f,   10.56f},
{41.55f,   34.52f,   30.36f,   22.99f,   31.05f,   25.54f,   29.45f,   33.54f,   39.81f,   2.06f,   60.16f,   12.56f},
{37.42f,   30.38f,   33.11f,   17.37f,   40.68f,   29.15f,   33.00f,   33.06f,   33.52f,   1.60f,   46.96f,   16.56f},
{34.32f,   28.89f,   43.89f,   10.61f,   48.47f,   32.88f,   36.67f,   33.06f,   30.96f,   4.13f,   31.16f,   20.56f},
{31.69f,   26.25f,   45.83f,   6.48f,   53.74f,   36.49f,   38.73f,   34.02f,   29.33f,   15.01f,   15.93f,   23.56f},
{27.56f,   21.08f,   38.61f,   5.45f,   56.72f,   39.63f,   40.22f,   35.22f,   29.92f,   26.81f,   6.79f,   28.56f},
{21.37f,   18.56f,   34.60f,   5.90f,   59.35f,   42.20f,   41.82f,   36.79f,   30.96f,   31.97f,   3.29f,   36.00f},
{21.36f,   22.10f,   28.30f,   6.93f,   61.99f,   43.83f,   42.74f,   38.95f,   32.01f,   36.67f,   1.70f,   43.56f},
{25.03f,   25.76f,   30.94f,   8.53f,   61.99f,   44.88f,   42.28f,   41.11f,   34.57f,   44.92f,   3.28f,   49.00f},
{27.09f,   28.28f,   22.80f,   10.59f,   56.15f,   44.88f,   39.76f,   43.28f,   37.25f,   43.89f,   4.74f,   33.00f},
{27.54f,   31.37f,   18.10f,   18.84f,   48.47f,   44.30f,   38.16f,   45.44f,   39.81f,   31.97f,   11.85f,   21.00f},
{31.66f,   36.52f,   16.50f,   29.15f,   42.28f,   41.63f,   37.13f,   47.13f,   41.44f,   27.84f,   24.03f,   16.56f},
{37.39f,   41.21f,   15.93f,   33.15f,   36.09f,   36.98f,   35.64f,   49.29f,   44.00f,   22.23f,   32.71f,   8.00f},
{41.51f,   43.27f,   18.68f,   39.00f,   29.33f,   32.91f,   33.46f,   50.25f,   49.24f,   16.50f,   36.77f,   2.00f},
{44.60f,   44.29f,   25.32f,   45.64f,   20.97f,   28.02f,   31.05f,   47.61f,   43.53f,   12.38f,   45.91f,   4.56f},
{47.12f,   43.71f,   33.00f,   36.81f,   24.75f,   29.65f,   33.00f,   46.52f,   32.01f,   9.85f,   58.09f,   13.00f},
{47.69f,   41.18f,   37.70f,   23.52f,   36.09f,   34.43f,   38.73f,   43.88f,   25.14f,   8.25f,   64.18f,   22.00f},
{47.12f,   38.54f,   41.36f,   18.24f,   41.71f,   35.48f,   41.25f,   41.11f,   17.81f,   7.79f,   57.06f,   28.00f},
{45.62f,   33.95f,   48.01f,   13.54f,   44.92f,   35.48f,   43.31f,   40.15f,   13.62f,   9.28f,   37.77f,   31.56f},
{43.56f,   29.82f,   58.32f,   10.10f,   46.98f,   35.49f,   45.38f,   38.47f,   10.01f,   12.38f,   20.95f,   35.00f},
{41.95f,   25.12f,   55.23f,   7.46f,   48.47f,   34.32f,   46.98f,   36.31f,   7.33f,   18.10f,   13.39f,   42.56f},
{37.82f,   18.47f,   39.76f,   6.43f,   51.10f,   35.96f,   47.90f,   33.54f,   6.87f,   25.78f,   8.76f,   54.00f},
{32.66f,   16.86f,   29.45f,   6.43f,   53.74f,   37.59f,   49.04f,   32.58f,   6.87f,   31.51f,   5.71f,   58.00f},
{34.26f,   22.01f,   27.27f,   7.45f,   54.77f,   38.64f,   48.47f,   31.38f,   8.38f,   34.60f,   5.26f,   48.00f},
{39.42f,   28.76f,   19.14f,   10.09f,   52.14f,   39.69f,   47.55f,   30.30f,   10.48f,   39.19f,   6.27f,   35.56f},
{43.54f,   33.91f,   12.38f,   16.73f,   48.47f,   39.11f,   45.95f,   29.21f,   14.20f,   47.44f,   11.80f,   22.56f},
{46.06f,   36.43f,   7.22f,   24.52f,   44.92f,   38.65f,   43.89f,   28.13f,   19.44f,   56.72f,   19.47f,   11.56f},
{47.66f,   38.48f,   3.67f,   29.10f,   43.31f,   38.65f,   41.71f,   27.65f,   23.63f,   51.10f,   27.59f,   5.56f},
{48.69f,   40.08f,   2.64f,   31.16f,   41.25f,   38.66f,   39.76f,   28.25f,   26.77f,   36.67f,   33.68f,   2.56f},
{49.71f,   41.56f,   4.24f,   34.71f,   39.76f,   38.66f,   37.13f,   30.78f,   33.06f,   29.45f,   43.26f,   2.00f},
{50.74f,   43.16f,   6.76f,   42.95f,   37.13f,   38.66f,   34.72f,   32.46f,   41.90f,   22.23f,   58.49f,   7.56f},
{51.20f,   44.19f,   10.89f,   51.32f,   27.04f,   32.96f,   29.91f,   29.33f,   45.63f,   18.10f,   63.56f,   16.00f},
{49.71f,   45.21f,   18.68f,   48.10f,   15.01f,   27.03f,   25.32f,   26.57f,   40.86f,   16.04f,   57.01f,   22.56f},
{47.64f,   45.21f,   25.32f,   39.97f,   15.01f,   29.24f,   30.48f,   24.89f,   34.11f,   13.41f,   44.82f,   27.00f},
{45.57f,   44.63f,   28.65f,   33.20f,   23.83f,   30.29f,   35.06f,   23.92f,   29.33f,   11.34f,   31.06f,   31.56f},
{42.48f,   43.59f,   33.00f,   26.90f,   40.91f,   31.34f,   37.13f,   22.72f,   26.77f,   10.89f,   19.88f,   38.00f},
{38.81f,   40.49f,   40.79f,   23.92f,   33.46f,   32.39f,   38.73f,   22.24f,   24.10f,   12.95f,   14.35f,   49.00f},
{33.65f,   37.39f,   50.53f,   22.31f,   43.31f,   32.40f,   40.22f,   21.16f,   23.63f,   16.50f,   11.76f,   59.56f},
{30.09f,   32.80f,   52.59f,   20.82f,   47.44f,   33.45f,   41.82f,   20.08f,   24.10f,   20.17f,   10.74f,   57.56f},
{27.46f,   27.64f,   38.16f,   19.78f,   52.71f,   32.40f,   42.85f,   18.99f,   23.63f,   23.26f,   14.34f,   44.56f},
{20.23f,   33.82f,   25.90f,   21.27f,   56.26f,   31.82f,   43.89f,   20.08f,   23.63f,   26.35f,   24.95f,   32.56f},
{22.29f,   39.43f,   17.53f,   23.33f,   56.72f,   32.41f,   43.89f,   21.64f,   23.63f,   33.00f,   33.07f,   21.00f},
{28.48f,   42.06f,   12.38f,   25.85f,   55.11f,   32.41f,   43.89f,   25.01f,   24.10f,   42.28f,   35.09f,   13.00f},
{33.17f,   44.12f,   9.28f,   29.05f,   53.74f,   32.42f,   43.89f,   29.21f,   26.19f,   51.10f,   43.21f,   11.00f},
{37.29f,   45.14f,   6.19f,   33.63f,   51.56f,   32.42f,   43.89f,   33.06f,   28.87f,   46.98f,   56.41f,   10.00f},
{40.38f,   46.17f,   3.67f,   37.75f,   50.53f,   31.84f,   43.77f,   36.79f,   31.43f,   33.57f,   63.52f,   13.00f},
{42.90f,   47.19f,   2.64f,   39.93f,   49.04f,   31.38f,   43.31f,   40.87f,   35.15f,   27.39f,   59.00f,   18.00f},
{44.96f,   48.22f,   2.64f,   44.05f,   48.47f,   31.38f,   42.28f,   42.80f,   42.49f,   22.23f,   44.78f,   24.56f},
{46.56f,   47.64f,   4.70f,   51.84f,   46.98f,   29.75f,   39.30f,   38.35f,   44.58f,   14.44f,   28.98f,   31.00f},
{47.59f,   46.14f,   9.85f,   58.94f,   37.47f,   25.10f,   33.46f,   35.22f,   36.20f,   8.82f,   13.75f,   36.56f},
{47.02f,   42.47f,   16.50f,   50.11f,   24.29f,   27.67f,   34.49f,   33.06f,   30.96f,   5.73f,   4.61f,   43.00f},
{44.49f,   37.88f,   21.08f,   37.28f,   20.63f,   31.39f,   41.71f,   30.30f,   25.72f,   3.09f,   1.55f,   50.56f},
{40.82f,   34.78f,   22.80f,   30.63f,   26.81f,   32.44f,   45.38f,   28.25f,   19.90f,   2.06f,   8.20f,   57.56f},
{37.27f,   33.17f,   24.64f,   23.75f,   36.09f,   33.49f,   47.90f,   27.65f,   17.81f,   2.64f,   20.38f,   46.56f},
{36.23f,   31.10f,   34.03f,   18.59f,   46.52f,   34.55f,   50.07f,   27.65f,   16.30f,   12.38f,   28.50f,   34.00f},
{33.59f,   26.51f,   48.01f,   17.67f,   53.74f,   35.60f,   51.10f,   27.65f,   16.30f,   22.69f,   32.00f,   28.56f},
{28.43f,   23.87f,   45.83f,   17.67f,   57.29f,   36.65f,   51.10f,   28.13f,   17.34f,   26.81f,   39.67f,   20.00f},
{24.88f,   28.56f,   31.97f,   17.66f,   59.81f,   35.60f,   51.10f,   29.21f,   19.44f,   31.51f,   51.85f,   10.00f},
{28.43f,   32.11f,   23.15f,   18.58f,   62.45f,   33.51f,   50.07f,   30.90f,   22.00f,   39.76f,   60.98f,   3.00f},
{31.52f,   34.74f,   15.93f,   20.75f,   62.91f,   31.42f,   47.55f,   31.38f,   25.14f,   38.73f,   59.40f,   1.56f},
{33.58f,   38.86f,   7.22f,   28.43f,   58.21f,   28.74f,   44.34f,   34.02f,   28.29f,   26.81f,   43.15f,   1.00f},
{36.67f,   44.01f,   2.06f,   37.70f,   51.56f,   27.70f,   41.71f,   36.79f,   30.96f,   21.66f,   23.40f,   2.56f},
{41.36f,   47.55f,   8.36f,   40.79f,   46.52f,   26.65f,   39.76f,   39.07f,   35.62f,   17.07f,   11.66f,   9.00f},
{45.95f,   49.61f,   16.16f,   43.89f,   39.76f,   26.08f,   36.67f,   41.60f,   42.95f,   10.89f,   7.15f,   18.56f},
{49.61f,   51.21f,   21.31f,   50.64f,   34.03f,   24.68f,   34.03f,   41.11f,   39.34f,   6.19f,   3.53f,   26.00f},
{52.13f,   50.17f,   24.75f,   49.61f,   25.21f,   28.76f,   32.08f,   40.63f,   29.33f,   3.67f,   2.06f,   30.56f},
{53.73f,   47.07f,   33.00f,   35.63f,   23.83f,   36.68f,   36.67f,   40.03f,   25.14f,   2.06f,   2.51f,   32.56f},
{53.15f,   43.97f,   45.38f,   41.36f,   33.46f,   39.82f,   39.76f,   37.87f,   19.90f,   2.06f,   7.14f,   35.00f},
{51.66f,   39.27f,   44.92f,   29.55f,   40.68f,   42.85f,   41.25f,   37.27f,   16.76f,   4.70f,   14.24f,   41.56f},
{49.59f,   34.68f,   31.51f,   23.36f,   44.92f,   46.12f,   42.74f,   36.79f,   15.71f,   9.28f,   22.36f,   51.56f},
{46.96f,   29.97f,   23.15f,   18.55f,   48.01f,   48.21f,   43.89f,   35.83f,   14.67f,   16.50f,   29.92f,   55.56f},
{43.86f,   23.32f,   18.22f,   13.96f,   49.96f,   48.68f,   44.92f,   34.14f,   13.62f,   21.66f,   36.57f,   53.00f},
{37.21f,   22.28f,   20.28f,   11.32f,   51.56f,   47.75f,   45.95f,   33.54f,   13.15f,   24.29f,   50.78f,   41.00f},
{33.08f,   27.89f,   15.47f,   8.80f,   54.20f,   46.13f,   46.98f,   33.06f,   14.67f,   28.88f,   62.40f,   28.56f},
{36.17f,   34.65f,   11.46f,   8.34f,   56.26f,   43.57f,   46.52f,   33.06f,   16.30f,   36.09f,   61.38f,   16.00f},
{38.69f,   40.26f,   7.79f,   8.33f,   55.11f,   40.43f,   45.95f,   33.06f,   18.86f,   45.38f,   52.23f,   8.00f},
{40.29f,   43.92f,   6.19f,   9.36f,   51.56f,   36.71f,   44.92f,   33.06f,   23.63f,   41.82f,   38.02f,   5.00f},
{40.29f,   46.43f,   5.61f,   14.97f,   47.44f,   34.50f,   43.31f,   33.06f,   27.24f,   27.84f,   23.80f,   2.00f},
{40.29f,   48.49f,   7.33f,   21.16f,   44.34f,   31.48f,   41.71f,   34.02f,   29.92f,   22.23f,   14.20f,   2.00f},
{39.71f,   50.54f,   10.31f,   24.36f,   42.28f,   29.38f,   40.22f,   35.70f,   35.62f,   16.50f,   9.58f,   6.56f},
{38.67f,   52.14f,   15.47f,   25.74f,   39.76f,   27.18f,   37.70f,   39.07f,   44.00f,   13.41f,   7.09f,   14.56f},
{38.21f,   53.17f,   21.20f,   28.37f,   37.13f,   23.10f,   33.00f,   37.99f,   47.14f,   12.38f,   6.07f,   22.00f},
{38.21f,   52.59f,   28.88f,   32.60f,   26.93f,   17.87f,   27.96f,   32.58f,   44.58f,   11.34f,   13.18f,   27.00f},
{39.24f,   51.09f,   35.06f,   39.25f,   13.98f,   18.80f,   32.08f,   31.98f,   36.67f,   10.89f,   23.33f,   31.56f},
{40.27f,   49.02f,   40.22f,   41.77f,   12.83f,   22.53f,   35.64f,   28.73f,   29.33f,   11.34f,   26.37f,   36.56f},
{40.26f,   46.96f,   45.95f,   32.48f,   21.66f,   25.21f,   36.67f,   27.05f,   25.14f,   13.98f,   28.85f,   44.00f},
{40.26f,   43.86f,   53.97f,   32.60f,   31.51f,   27.78f,   37.70f,   25.97f,   20.95f,   17.53f,   40.58f,   56.56f},
{38.20f,   38.69f,   36.67f,   23.31f,   39.30f,   30.46f,   38.73f,   23.80f,   18.86f,   21.66f,   53.78f,   62.00f},
{36.13f,   34.56f,   29.45f,   16.55f,   44.34f,   33.49f,   38.73f,   22.24f,   17.81f,   24.75f,   61.90f,   52.56f},
{34.52f,   27.34f,   23.15f,   12.88f,   48.47f,   35.70f,   38.73f,   20.08f,   16.30f,   27.84f,   59.30f,   40.00f},
{29.94f,   26.30f,   19.59f,   10.93f,   52.71f,   36.75f,   38.16f,   18.99f,   14.67f,   35.06f,   45.65f,   27.00f},
{35.09f,   32.48f,   15.93f,   8.75f,   55.11f,   36.17f,   37.13f,   18.99f,   14.20f,   44.92f,   30.41f,   15.56f},
{40.70f,   36.60f,   12.38f,   8.29f,   54.77f,   35.24f,   36.67f,   20.56f,   15.71f,   53.63f,   15.18f,   9.56f},
{43.33f,   39.69f,   11.46f,   8.74f,   52.71f,   34.08f,   37.13f,   23.32f,   18.39f,   49.50f,   11.10f,   6.56f},
{45.85f,   41.74f,   10.89f,   10.80f,   49.50f,   32.57f,   37.70f,   25.97f,   22.00f,   36.67f,   21.70f,   7.00f},
{47.45f,   43.23f,   12.38f,   13.43f,   45.95f,   30.48f,   38.73f,   28.73f,   26.19f,   30.48f,   27.34f,   9.56f},
{48.48f,   45.86f,   15.01f,   16.52f,   43.77f,   28.85f,   38.73f,   31.98f,   29.92f,   25.78f,   33.43f,   13.56f},
{49.51f,   47.91f,   19.59f,   21.10f,   41.71f,   26.76f,   38.73f,   36.19f,   36.67f,   18.10f,   44.03f,   19.00f},
{48.93f,   48.94f,   25.32f,   25.34f,   39.30f,   24.79f,   36.09f,   40.15f,   45.63f,   13.41f,   55.76f,   24.00f},
{47.90f,   48.36f,   28.42f,   26.83f,   28.88f,   19.43f,   31.05f,   36.79f,   42.95f,   9.85f,   62.86f,   29.00f},
{45.37f,   46.29f,   28.99f,   30.49f,   14.55f,   16.41f,   32.54f,   32.58f,   36.20f,   7.79f,   59.81f,   36.56f},
{42.28f,   42.73f,   37.70f,   38.62f,   8.82f,   20.49f,   40.68f,   29.81f,   32.48f,   6.76f,   44.58f,   45.00f},
{39.18f,   38.60f,   50.07f,   47.44f,   13.41f,   23.17f,   44.34f,   25.37f,   25.72f,   8.82f,   25.73f,   53.56f},
{36.54f,   35.96f,   53.63f,   42.28f,   23.26f,   24.22f,   46.52f,   22.72f,   19.90f,   18.56f,   15.12f,   49.00f},
{36.08f,   34.46f,   42.85f,   30.94f,   32.08f,   25.85f,   47.90f,   21.76f,   17.34f,   28.88f,   11.51f,   33.56f},
{35.05f,   30.33f,   47.55f,   24.18f,   38.73f,   27.37f,   47.90f,   21.16f,   16.30f,   33.57f,   9.03f,   29.56f},
{30.92f,   26.20f,   34.60f,   26.24f,   43.31f,   28.42f,   47.90f,   21.16f,   16.30f,   38.73f,   8.46f,   23.56f},
{28.85f,   27.23f,   26.81f,   21.08f,   46.41f,   29.47f,   47.44f,   21.16f,   17.34f,   48.01f,   11.05f,   16.00f},
{34.01f,   31.35f,   22.23f,   16.04f,   49.50f,   28.89f,   45.38f,   21.40f,   17.81f,   47.44f,   15.56f,   8.56f},
{38.13f,   33.86f,   11.92f,   12.37f,   52.14f,   27.26f,   41.71f,   22.72f,   19.90f,   37.13f,   21.20f,   5.56f},
{41.22f,   36.49f,   4.24f,   12.36f,   49.50f,   25.29f,   38.27f,   23.20f,   22.58f,   33.57f,   27.29f,   5.56f},
{44.77f,   41.07f,   2.06f,   13.85f,   42.28f,   24.13f,   34.49f,   25.01f,   25.14f,   28.88f,   32.36f,   5.56f},
{49.46f,   45.19f,   1.03f,   15.91f,   36.09f,   23.67f,   33.00f,   28.73f,   28.29f,   23.26f,   35.86f,   8.56f},
{53.01f,   46.79f,   3.09f,   18.08f,   29.91f,   23.20f,   32.08f,   31.98f,   34.11f,   20.17f,   47.59f,   17.00f},
{55.07f,   47.81f,   8.36f,   21.06f,   25.21f,   22.62f,   30.94f,   35.70f,   37.71f,   18.56f,   60.22f,   26.00f},
{57.13f,   46.77f,   15.58f,   25.18f,   19.02f,   24.84f,   31.05f,   36.79f,   27.82f,   17.07f,   60.78f,   32.00f},
{57.70f,   44.13f,   21.66f,   35.03f,   20.40f,   33.22f,   36.67f,   36.79f,   22.00f,   15.47f,   50.06f,   34.56f},
{56.09f,   40.00f,   26.81f,   43.28f,   13.98f,   38.35f,   41.71f,   36.79f,   16.76f,   15.47f,   37.42f,   36.00f},
{53.57f,   34.84f,   35.64f,   46.94f,   27.84f,   42.54f,   44.92f,   36.31f,   12.11f,   18.10f,   24.67f,   40.56f},
{50.93f,   29.68f,   46.98f,   53.59f,   16.84f,   47.32f,   47.90f,   36.31f,   10.48f,   22.23f,   12.48f,   49.00f},
{46.80f,   24.51f,   52.14f,   52.55f,   36.09f,   50.46f,   50.99f,   36.31f,   8.38f,   28.42f,   5.93f,   59.00f},
{43.25f,   18.32f,   39.76f,   38.57f,   41.25f,   47.78f,   53.74f,   36.79f,   7.92f,   32.54f,   4.35f,   57.00f},
{37.52f,   12.70f,   26.35f,   29.28f,   44.92f,   51.51f,   55.11f,   35.70f,   9.43f,   34.60f,   3.90f,   46.00f},
{34.99f,   15.79f,   22.23f,   24.24f,   48.01f,   52.56f,   56.26f,   35.83f,   12.11f,   38.73f,   5.92f,   34.00f},
{39.11f,   23.00f,   18.10f,   18.97f,   51.10f,   51.98f,   55.69f,   36.31f,   15.25f,   45.38f,   16.07f,   22.00f},
{41.17f,   29.64f,   14.55f,   14.38f,   54.77f,   50.94f,   54.20f,   37.27f,   18.86f,   53.63f,   28.71f,   15.00f},
{44.72f,   34.33f,   11.92f,   12.77f,   57.29f,   48.85f,   52.59f,   38.47f,   24.68f,   49.04f,   33.78f,   12.56f},
{46.32f,   36.85f,   10.31f,   11.74f,   56.15f,   46.87f,   51.10f,   38.95f,   30.96f,   35.06f,   39.42f,   10.56f},
{47.35f,   38.90f,   9.85f,   11.16f,   52.71f,   45.71f,   48.93f,   40.03f,   35.62f,   28.42f,   50.59f,   11.56f},
{48.38f,   40.96f,   10.89f,   12.76f,   48.93f,   43.62f,   46.98f,   42.32f,   41.44f,   21.66f,   60.74f,   16.56f},
{49.41f,   43.02f,   12.83f,   15.97f,   46.52f,   41.99f,   44.34f,   45.44f,   50.29f,   18.10f,   61.75f,   24.56f},
{50.44f,   44.61f,   19.14f,   21.92f,   44.92f,   40.48f,   39.30f,   49.17f,   57.62f,   16.50f,   51.03f,   32.00f},
{49.86f,   44.61f,   27.39f,   26.62f,   42.85f,   37.92f,   33.57f,   46.04f,   56.57f,   15.01f,   34.32f,   37.00f},
{48.37f,   44.03f,   31.97f,   28.79f,   40.22f,   32.22f,   38.16f,   39.55f,   49.24f,   13.41f,   17.51f,   41.00f},
{46.30f,   42.53f,   36.09f,   29.71f,   34.49f,   32.23f,   42.28f,   35.10f,   42.49f,   13.41f,   4.31f,   45.56f},
{43.66f,   40.47f,   41.36f,   34.40f,   22.57f,   35.26f,   43.89f,   31.98f,   37.71f,   15.47f,   6.32f,   53.00f},
{41.14f,   37.37f,   48.01f,   42.08f,   14.55f,   37.35f,   45.38f,   28.73f,   34.57f,   18.56f,   16.47f,   47.00f},
{37.01f,   33.23f,   56.15f,   45.28f,   18.56f,   39.57f,   46.98f,   25.97f,   33.06f,   22.23f,   21.55f,   35.56f},
{32.31f,   29.10f,   46.98f,   36.46f,   29.45f,   41.55f,   46.98f,   22.84f,   32.48f,   24.75f,   26.17f,   25.00f},
{28.76f,   21.88f,   31.97f,   30.38f,   38.73f,   43.18f,   47.90f,   18.87f,   30.96f,   27.39f,   35.30f,   19.00f},
{22.57f,   21.87f,   26.35f,   23.16f,   42.85f,   44.23f,   47.90f,   15.75f,   29.92f,   34.03f,   45.91f,   17.00f},
{17.87f,   28.05f,   20.63f,   17.43f,   45.95f,   45.29f,   47.55f,   14.67f,   29.33f,   43.31f,   56.06f,   17.56f},
{23.59f,   31.60f,   18.10f,   14.33f,   48.93f,   45.75f,   46.98f,   17.31f,   28.87f,   51.10f,   62.71f,   21.00f},
{28.17f,   34.23f,   15.01f,   12.15f,   50.53f,   44.24f,   46.52f,   20.68f,   29.92f,   46.98f,   55.60f,   25.56f},
{31.84f,   36.29f,   12.83f,   10.77f,   48.47f,   42.04f,   45.95f,   25.37f,   30.96f,   33.00f,   35.74f,   31.56f},
{34.35f,   37.31f,   11.92f,   10.77f,   45.95f,   40.06f,   45.38f,   30.78f,   33.52f,   27.39f,   17.01f,   37.56f},
{36.41f,   38.79f,   12.83f,   12.02f,   43.31f,   37.97f,   44.34f,   35.83f,   35.15f,   22.23f,   7.87f,   43.00f},
{38.01f,   40.85f,   13.98f,   14.31f,   40.79f,   35.29f,   42.85f,   41.23f,   39.81f,   15.47f,   4.26f,   49.56f},
{39.50f,   41.42f,   18.10f,   16.95f,   39.19f,   32.74f,   41.25f,   46.52f,   47.14f,   10.31f,   2.22f,   56.56f},
{40.07f,   40.38f,   24.75f,   20.38f,   37.81f,   30.06f,   36.32f,   45.44f,   41.90f,   7.79f,   2.22f,   41.00f},
{39.49f,   37.74f,   31.51f,   25.19f,   37.13f,   24.83f,   32.08f,   42.20f,   33.06f,   6.19f,   4.81f,   35.56f},
{37.43f,   34.64f,   35.06f,   27.71f,   35.64f,   21.69f,   38.16f,   41.11f,   28.29f,   5.73f,   9.88f,   28.56f},
{34.33f,   30.51f,   36.67f,   30.80f,   22.23f,   26.00f,   44.34f,   38.47f,   22.00f,   7.79f,   16.42f,   19.00f},
{30.20f,   28.44f,   42.85f,   38.01f,   8.82f,   29.61f,   46.98f,   36.79f,   16.76f,   17.53f,   22.51f,   10.00f},
{28.14f,   27.40f,   53.05f,   46.26f,   9.85f,   32.29f,   49.04f,   36.31f,   15.25f,   28.42f,   27.59f,   6.00f},
{26.65f,   24.30f,   41.71f,   41.56f,   17.99f,   34.74f,   50.07f,   36.31f,   14.20f,   33.57f,   32.21f,   5.56f},
{22.52f,   20.17f,   33.11f,   29.75f,   27.27f,   38.00f,   50.53f,   37.39f,   14.20f,   39.19f,   36.72f,   4.56f},
{17.36f,   22.23f,   22.80f,   21.39f,   34.49f,   40.68f,   50.53f,   37.39f,   15.25f,   48.01f,   51.95f,   8.00f},
{18.39f,   27.38f,   13.98f,   15.31f,   39.76f,   42.19f,   49.50f,   38.95f,   16.76f,   48.01f,   61.65f,   16.56f},
{22.51f,   31.04f,   9.28f,   9.69f,   43.31f,   43.25f,   47.55f,   41.11f,   19.44f,   37.13f,   62.10f,   24.56f},
{25.03f,   34.13f,   6.76f,   7.06f,   45.95f,   43.25f,   45.26f,   42.80f,   22.00f,   33.57f,   53.97f,   30.00f},
{27.09f,   39.73f,   5.61f,   7.05f,   47.44f,   42.67f,   42.85f,   44.36f,   24.68f,   31.51f,   41.33f,   32.56f},
{31.21f,   45.92f,   8.71f,   7.05f,   45.95f,   41.04f,   40.22f,   46.04f,   28.29f,   25.78f,   27.56f,   33.56f},
{36.36f,   49.58f,   15.93f,   8.08f,   39.76f,   39.07f,   38.16f,   47.13f,   34.57f,   21.66f,   14.93f,   37.56f},
{40.03f,   52.09f,   22.69f,   9.68f,   32.08f,   36.39f,   36.67f,   49.29f,   39.34f,   19.14f,   4.20f,   46.00f},
{42.54f,   53.69f,   27.84f,   13.69f,   25.78f,   34.30f,   34.49f,   48.21f,   29.92f,   17.07f,   13.33f,   55.00f},
{44.14f,   53.11f,   31.74f,   24.00f,   20.05f,   33.72f,   37.13f,   45.44f,   24.10f,   15.47f,   22.01f,   53.56f},
{43.57f,   50.58f,   44.11f,   32.82f,   13.41f,   40.13f,   42.85f,   43.28f,   19.90f,   14.44f,   24.04f,   43.00f},
{42.53f,   47.94f,   55.23f,   38.54f,   6.76f,   43.74f,   45.38f,   40.63f,   16.30f,   15.01f,   33.63f,   31.00f},
{40.01f,   44.84f,   45.38f,   46.79f,   15.01f,   45.84f,   48.47f,   38.47f,   14.67f,   17.53f,   49.42f,   19.00f},
{37.37f,   41.74f,   33.00f,   48.16f,   25.21f,   47.47f,   51.56f,   36.79f,   13.62f,   23.26f,   60.58f,   12.56f},
{34.85f,   36.12f,   28.88f,   35.90f,   32.08f,   48.40f,   53.17f,   34.62f,   13.15f,   28.42f,   58.55f,   9.56f},
{30.15f,   28.90f,   24.75f,   28.68f,   36.09f,   49.57f,   53.74f,   31.98f,   13.15f,   30.48f,   45.35f,   8.00f},
{24.99f,   29.35f,   22.80f,   25.12f,   39.76f,   49.57f,   55.11f,   30.30f,   13.62f,   32.54f,   31.58f,   9.00f},
{26.02f,   34.04f,   20.63f,   22.48f,   42.74f,   49.00f,   55.11f,   29.21f,   15.25f,   37.70f,   17.92f,   14.00f},
{30.71f,   38.62f,   17.53f,   20.87f,   46.52f,   47.49f,   54.20f,   28.13f,   16.76f,   45.38f,   11.26f,   23.00f},
{34.26f,   41.25f,   15.93f,   19.84f,   49.04f,   44.35f,   53.63f,   27.05f,   20.95f,   52.14f,   9.79f,   31.00f},
{36.32f,   42.27f,   15.93f,   19.38f,   49.50f,   41.09f,   52.59f,   25.97f,   25.72f,   42.28f,   9.23f,   36.56f},
{37.92f,   42.27f,   15.93f,   19.38f,   46.52f,   39.11f,   51.10f,   25.37f,   28.87f,   29.45f,   8.77f,   41.56f},
{38.95f,   42.26f,   20.17f,   19.37f,   41.71f,   37.02f,   48.93f,   25.37f,   33.06f,   22.69f,   9.79f,   47.00f},
{39.98f,   41.68f,   26.35f,   23.04f,   38.27f,   33.77f,   46.98f,   25.97f,   40.39f,   18.10f,   14.30f,   55.56f},
{40.43f,   41.22f,   29.45f,   29.68f,   35.64f,   32.37f,   43.89f,   29.69f,   47.14f,   16.50f,   25.46f,   53.00f},
{41.00f,   41.67f,   30.36f,   34.72f,   33.46f,   29.12f,   37.70f,   29.33f,   46.10f,   15.47f,   32.57f,   40.56f},
{40.43f,   41.21f,   34.03f,   37.35f,   31.05f,   24.00f,   36.55f,   24.89f,   39.81f,   14.44f,   35.16f,   27.56f},
{39.39f,   39.60f,   40.79f,   38.95f,   27.27f,   24.00f,   40.22f,   23.20f,   33.52f,   13.98f,   41.70f,   20.56f},
{38.36f,   38.56f,   48.58f,   44.10f,   15.58f,   27.49f,   41.82f,   22.72f,   29.92f,   15.47f,   52.87f,   17.00f},
{37.32f,   36.03f,   41.82f,   52.35f,   3.55f,   30.18f,   41.82f,   21.76f,   26.77f,   18.10f,   60.99f,   16.00f},
{35.26f,   31.90f,   28.88f,   54.41f,   9.85f,   32.86f,   41.82f,   21.16f,   25.72f,   21.20f,   64.03f,   18.00f},
{31.13f,   28.23f,   30.94f,   45.13f,   21.20f,   35.54f,   41.25f,   20.56f,   25.72f,   23.72f,   53.42f,   21.00f},
{28.61f,   21.58f,   27.27f,   37.90f,   30.48f,   37.52f,   40.22f,   19.48f,   25.14f,   25.78f,   32.09f,   26.00f},
{23.90f,   21.57f,   22.23f,   29.65f,   37.24f,   39.15f,   39.30f,   18.99f,   24.68f,   29.91f,   16.29f,   30.56f},
{21.38f,   27.75f,   18.10f,   23.00f,   42.28f,   40.78f,   37.70f,   18.99f,   24.68f,   37.13f,   9.75f,   34.56f},
{28.02f,   31.87f,   15.93f,   19.79f,   47.44f,   42.30f,   36.09f,   20.56f,   26.19f,   45.38f,   5.68f,   41.56f},
{33.75f,   34.38f,   14.55f,   16.24f,   51.56f,   41.72f,   35.06f,   22.72f,   28.29f,   48.47f,   3.08f,   50.00f},
{38.33f,   35.98f,   13.41f,   14.17f,   53.17f,   40.79f,   34.60f,   25.97f,   30.96f,   36.09f,   3.65f,   58.56f},
{41.99f,   37.01f,   12.83f,   13.14f,   53.17f,   39.75f,   34.03f,   30.30f,   34.57f,   25.78f,   5.67f,   53.00f},
{45.08f,   38.49f,   13.98f,   13.13f,   51.56f,   38.59f,   33.00f,   34.02f,   35.62f,   22.69f,   11.20f,   37.56f},
{48.18f,   40.09f,   15.01f,   15.77f,   49.50f,   37.54f,   32.08f,   38.47f,   40.39f,   18.56f,   17.85f,   33.56f},
{50.23f,   41.12f,   16.96f,   19.20f,   48.58f,   36.50f,   29.91f,   42.68f,   48.77f,   13.41f,   23.94f,   28.00f},
{51.72f,   40.54f,   21.66f,   23.44f,   47.44f,   34.87f,   25.32f,   39.43f,   51.92f,   10.31f,   28.45f,   19.56f},
{52.29f,   39.04f,   25.90f,   28.48f,   46.52f,   30.80f,   19.59f,   35.83f,   43.53f,   9.85f,   36.57f,   11.56f},
{51.72f,   35.94f,   28.07f,   32.25f,   45.38f,   28.59f,   25.32f,   33.06f,   38.30f,   9.85f,   51.80f,    8.56f},
{48.62f,   31.24f,   29.45f,   35.69f,   33.00f,   33.48f,   31.51f,   30.30f,   33.06f,   11.34f,   61.95f,   8.56f},
{44.03f,   26.07f,   33.00f,   43.48f,   21.20f,   36.51f,   34.49f,   27.65f,   26.77f,   17.07f,   62.96f,   8.56f},
{40.36f,   24.01f,   45.38f,   51.72f,   22.23f,   38.61f,   37.70f,   25.97f,   24.10f,   28.88f,   55.40f,   12.00f},
{37.27f,   20.91f,   51.68f,   56.30f,   31.05f,   40.83f,   40.22f,   25.49f,   22.58f,   39.76f,   43.21f,   21.00f},
{32.68f,   15.29f,   38.61f,   48.05f,   40.68f,   42.81f,   42.28f,   25.37f,   22.58f,   43.31f,   30.46f,   29.56f},
{26.49f,   11.15f,   26.01f,   37.28f,   47.90f,   44.32f,   43.89f,   26.57f,   22.58f,   49.50f,   0.54f,   35.00f},
{25.91f,   14.70f,   28.07f,   30.17f,   52.71f,   45.49f,   44.92f,   26.57f,   24.10f,   57.75f,   3.44f,   38.00f},
{29.00f,   17.79f,   24.18f,   24.44f,   55.69f,   45.96f,   43.77f,   27.65f,   26.19f,   46.41f,   0.38f,   40.00f},
{29.57f,   19.39f,   20.63f,   19.17f,   58.32f,   44.92f,   41.71f,   28.73f,   28.87f,   37.70f,   0.63f,   44.00f},
{31.06f,   22.47f,   13.41f,   17.10f,   60.84f,   43.87f,   40.22f,   29.81f,   31.43f,   33.00f,   0.78f,   52.00f},
{32.66f,   28.08f,   7.79f,   17.21f,   60.96f,   41.78f,   38.73f,   31.38f,   33.52f,   27.39f,   1.25f,   61.56f},
{37.24f,   32.77f,   5.16f,   18.70f,   54.20f,   39.22f,   36.09f,   33.06f,   37.25f,   21.20f,   0.88f,   57.56f},
{42.39f,   35.29f,   4.70f,   20.76f,   46.52f,   37.13f,   33.46f,   35.10f,   43.53f,   17.53f,   4.19f,   45.56f},
{46.06f,   36.89f,   5.16f,   22.82f,   40.22f,   34.45f,   29.91f,   34.02f,   38.76f,   15.01f,   0.03f,   32.56f},
{48.58f,   36.88f,   10.89f,   27.40f,   34.49f,   34.46f,   32.08f,   32.58f,   28.29f,   12.95f,   64.43f,   19.00f},
{49.60f,   34.81f,   19.02f,   38.28f,   26.81f,   40.86f,   36.67f,   30.30f,   22.58f,   11.92f,   64.48f,   10.56f},
{49.14f,   32.17f,   26.70f,   45.96f,   20.05f,   43.89f,   38.16f,   27.65f,   16.30f,   12.38f,   64.58f,   6.56f},
{48.11f,   29.07f,   31.40f,   48.93f,   27.84f,   45.99f,   39.76f,   25.97f,   13.15f,   15.01f,   65.69f,   3.56f},
{45.47f,   24.94f,   34.49f,   53.86f,   37.13f,   48.21f,   40.68f,   24.89f,   10.48f,   20.63f,   65.84f,   3.00f}, 


        };
    }

}
