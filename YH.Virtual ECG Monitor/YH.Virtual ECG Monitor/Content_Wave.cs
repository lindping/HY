using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YH.Simulator.Framework;

namespace YH.Virtual_ECG_Monitor
{                  
    public class Content_Wave
    {

        public Content_Wave()
        {

        }

        /// <summary>
        /// 设置ECG波形数据
        /// </summary>
        /// <param name="eRhythm"></param>
        /// <param name="nHeartRate"></param>
        public float[,] GetWaveData_ECG(Rhythm eRhythm, int nHeartRate, float baseAMP)
        {
            float[,] wavedata;
            //float baseAMP = 1.0f;                             
            WaveData_ECG wave_ECG = DataToObject.To<WaveData_ECG>((int)eRhythm); ;                   //获取指定编号ECG波形

            float[,] currentwavedata = wave_ECG.WaveData;    //获取ECG波形的数据
            int currentwaverate = wave_ECG.Rate;             //获取ECG波形的频率
    //        float baseAMP = wave_ECG.BaseAMP;                //原始数据放大倍数
            int plotCount = currentwavedata.GetLength(0);

            int newplotCount = 1;
            if (nHeartRate > 0)
            {
                if (eRhythm == Rhythm.Rhythm_29)
                {
                    newplotCount = 50;
                }
                else
                {
                    newplotCount = plotCount * currentwaverate / (nHeartRate);        //根据设定的频率计算的波形点个数
                }
            }

            int columnCpunt = currentwavedata.GetLength(1);
            if (columnCpunt > 12)
            {
                columnCpunt = 12;
            }
            wavedata = new float[newplotCount, columnCpunt];         //重新定义长度
            float[] waveOffsetY0 = new float[columnCpunt];                                           //首个点的Y值（用作调整波形偏移量使用）

            for (int k = 0; k < columnCpunt; k++)
            {
                waveOffsetY0[k] = currentwavedata[0, k] * baseAMP;
            }

            if (newplotCount > plotCount)
            {
                //频率低，点数多
                for (int i = 0; i < newplotCount; i++)
                {
                    if (i < plotCount)
                    {
                        for (int j = 0; j < columnCpunt; j++)
                        {
                            wavedata[i, j] = currentwavedata[i, j] * baseAMP - waveOffsetY0[j];

                        }
                        //ecgwavelastplotNum = i + 1;
                    }
                    else
                    {
                        for (int j = 0; j < columnCpunt; j++)
                        {
                            wavedata[i, j] = currentwavedata[0, j] * baseAMP - waveOffsetY0[j];
                        }
                    }
                }
            }
            else
            {
                //频率高，点数少，抽取数据
                float step = (float)plotCount / (float)newplotCount;
                for (int i = 0; i < newplotCount; i++)
                {
                    int o = (int)((float)i * step);
                    if (o > plotCount)
                    {
                        o = plotCount - 1;
                    }
                    for (int j = 0; j < columnCpunt; j++)
                    {
                        wavedata[i, j] = currentwavedata[o, j] * baseAMP - waveOffsetY0[j];
                    }
                }
            }

            return wavedata;
        }

        /// <summary>
        /// 设置ABP波形数据
        /// </summary>
        /// <param name="nPlot"></param>
        /// <param name="nSpo2"></param>
        public float[] GetWaveData_ABP(int nPlot, int nSystolic, int nDiastolic, float baseAMP)
        {
            float[] wavedata = new float[0];
            //const float baseAMP = 0.5f;                                   //原始数据放大倍数  
            WaveData_ABP wave_ABP = DataToObject.To<WaveData_ABP>(0);                             //获取指定编号ABP波形

            float[] currentwavedata = wave_ABP.WaveData;       //获取ABP波形的数据
            int currentwaverate = wave_ABP.Rate;               //获取ABP波形的频率
         //   float baseAMP = wave_ABP.BaseAMP;                  //原始数据放大倍数  
            int plotCount = currentwavedata.GetLength(0);

            int newplotCount = nPlot;

            //if (nABPRate > 0)
            //    newplotCount = plotCount * currentwaverate / (nABPRate);        //根据设定的频率计算的波形点个数

            wavedata = new float[newplotCount];         //重新定义长度
            float waveOffsetY0 = currentwavedata[0] * (nSystolic / 100.0f) * baseAMP;         //首个点的Y值（用作调整波形偏移量使用）
            if (newplotCount > plotCount)
            {
                //频率低，点数多
                for (int i = 0; i < newplotCount; i++)
                {
                    if (i < plotCount)
                    {

                        wavedata[i] = currentwavedata[i] * (nSystolic / 100.0f) * baseAMP - waveOffsetY0;


                        //ecgwavelastplotNum = i + 1;
                    }
                    else
                    {
                        wavedata[i] = currentwavedata[0] * (nSystolic / 100.0f) * baseAMP - waveOffsetY0;
                    }
                }
            }
            else
            {
                //频率高，点数少，抽取数据
                float step = (float)plotCount / (float)newplotCount;
                for (int i = 0; i < newplotCount; i++)
                {
                    int o = (int)((float)i * step);
                    if (o > plotCount)
                    {
                        o = plotCount - 1;
                    }
                    wavedata[i] = currentwavedata[o] * (nSystolic / 100.0f) * baseAMP - waveOffsetY0;
                }
            }
            return wavedata;

        }


        /// <summary>
        /// 设置PLETH波形数据
        /// </summary>
        /// <param name="nPlethRate"></param>
        /// <param name="nSpo2"></param>
        public float[] GetWaveData_PLETH(int plot, int nSpo2, float baseAMP)
        {
            float[] wavedata = new float[0];
            //const float baseAMP = 0.5f;                                        //原始数据放大倍数  
            WaveData_PLETH wave_PLETH = DataToObject.To<WaveData_PLETH>(0);                         //获取指定编号PLETH波形

            float[] currentwavedata = wave_PLETH.WaveData;       //获取PLETH波形的数据
            int currentwaverate = wave_PLETH.Rate;               //获取PLETH波形的频率
        //    float baseAMP = wave_PLETH.BaseAMP;                  //原始数据放大倍数 
            int plotCount = currentwavedata.GetLength(0);

            int newplotCount = plot;

            //if (nPlethRate > 0)
            //    newplotCount = plotCount * currentwaverate / (nPlethRate);        //根据设定的频率计算的波形点个数

            wavedata = new float[newplotCount];         //重新定义长度
            float waveOffsetY0 = currentwavedata[0] * (nSpo2 / 100.0f) * baseAMP;         //首个点的Y值（用作调整波形偏移量使用）
            if (newplotCount > plotCount)
            {
                //频率低，点数多
                for (int i = 0; i < newplotCount; i++)
                {
                    if (i < plotCount)
                    {
                        wavedata[i] = currentwavedata[i] * (nSpo2 / 100.0f) * baseAMP - waveOffsetY0;
                        //ecgwavelastplotNum = i + 1;
                    }
                    else
                    {
                        wavedata[i] = currentwavedata[0] * (nSpo2 / 100.0f) * baseAMP - waveOffsetY0;

                    }
                }
            }
            else
            {
                //频率高，点数少，抽取数据
                float step = (float)plotCount / (float)newplotCount;
                for (int i = 0; i < newplotCount; i++)
                {
                    int o = (int)((float)i * step);
                    if (o > plotCount)
                    {
                        o = plotCount - 1;
                    }
                    wavedata[i] = currentwavedata[o] * (nSpo2 / 100.0f) * baseAMP - waveOffsetY0;
                }
            }
            return wavedata;
        }

        /// <summary>
        /// 设置Resp波形数据
        /// </summary>
        /// <param name="eRespType"></param>
        /// <param name="nRespRate"></param>
        /// <param name="nInspCapacity"></param>
        /// <param name="nRespRatio"></param>
        public float[] GetWaveData_RESP(RespType eRespType, int plot, int nRespRate, int nInspCapacity, int nRespRatio,float baseAMP)
        {
            float[] wavedata = new float[0];
            //const float baseAMP = 30.0f;
            Wave_RESP wave_RESP = DataToObject.To<Wave_RESP>((int)eRespType);                           //获取指定编号PLETH波形

            float[] currentwavedata = wave_RESP.WaveData;       //获取RESP波形的数据
            int currentwaverate = wave_RESP.Rate;               //获取RESP波形的频率
          //  float baseAMP = wave_RESP.BaseAMP;                    //原始数据放大倍数 

            int plotCount = 1, leftplotCount = 1, rightplotCount = 0;
            plotCount = currentwavedata.GetLength(0);

            float[] currentwavedatas = wave_RESP.WaveData;       //获取RESP波形的数据

            if (plotCount > 1)
            {
                //预设波形数据
                leftplotCount = plotCount * wave_RESP.Ratio / 100;                    //预存RESP波形左边点个数
                rightplotCount = plotCount - leftplotCount;                                        //预存RESP波形右边点个数

            }

            int newplotCount = 1, newleftplotCount = 1, newrightplotCount = 0;
            if (nRespRate > 0)
            {
                //根据设定的频率计算的波形
                newplotCount = plot; // plotCount * currentwaverate / nRespRate;                                                     //RESP波形点个数               

                newleftplotCount = newplotCount * nRespRatio / 100;
                newrightplotCount = newplotCount - newleftplotCount;
                if (newplotCount >= plotCount)
                {
                    //修改波形左右两边点数
                    newleftplotCount = plotCount * nRespRatio / 100;
                    newrightplotCount = newplotCount - newleftplotCount;
                }
            }

            wavedata = new float[newplotCount];         //重新定义长度
            float waveOffsetY0 = currentwavedata[0] * (nInspCapacity / 3000.0f) * baseAMP;                //首个点的Y值（用作调整波形偏移量使用）
            //左边波形
            if (newleftplotCount > leftplotCount)
            {

                float step = newleftplotCount / (newleftplotCount - leftplotCount);
                int o = 0;
                for (int i = 0; i < leftplotCount; i++)
                {
                    wavedata[i + o] = currentwavedata[i] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    if ((i + 1) % step == 0)
                    {
                        ++o;
                        wavedata[(o * (int)step) + o - 1] = currentwavedata[i] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    }
                }
                if (leftplotCount - 1 + o < newleftplotCount)
                {
                    for (int j = leftplotCount - 1 + o; j < newleftplotCount; j++)
                    {
                        wavedata[j] = currentwavedata[leftplotCount - 1] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    }
                }
            }
            else
            {
                float step = (float)leftplotCount / (float)newleftplotCount;
                for (int i = 0; i < newleftplotCount; i++)
                {
                    int o = (int)((float)i * step);
                    if (o > leftplotCount)
                    {
                        o = leftplotCount - 1;
                    }
                    wavedata[i] = currentwavedata[o] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                }
            }

            //右边波形
            if (newleftplotCount > leftplotCount)
            {

                float step = 0;
                if (newrightplotCount > rightplotCount) step = newrightplotCount / (newrightplotCount - rightplotCount);
                int o = 0;
                for (int i = 0; i < rightplotCount; i++)
                {
                    wavedata[newleftplotCount - 1 + i + o] = currentwavedata[leftplotCount - 1 + i] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    if ((i + 1) % step == 0)
                    {
                        ++o;
                        wavedata[newleftplotCount - 1 + (o * (int)step) + o - 1] = currentwavedata[leftplotCount - 1 + i] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    }
                }
            }
            else
            {
                float step = (float)rightplotCount / (float)newrightplotCount;
                for (int i = 0; i < newrightplotCount; i++)
                {
                    int o = (int)((float)i * step);
                    if (o > rightplotCount)
                    {
                        o = rightplotCount - 1;
                    }
                    if (newleftplotCount > 0)
                    {
                        wavedata[newleftplotCount - 1 + i] = currentwavedata[leftplotCount - 1 + o] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    }
                    else
                    {
                        wavedata[i] = currentwavedata[leftplotCount - 1 + o] * (nInspCapacity / 3000.0f) * baseAMP - waveOffsetY0;
                    }
                }

            }

            return wavedata;
        }

    }

    //public enum WaveTypes
    //{
    //    动脉血压=0,
    //    二氧化碳=1,
    //    肺动脉压=2,
    //    血氧饱和度=3
    //}

   

}
