using HYS.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace YH.Virtual_ECG_Monitor
{
    public class AlermSoundPlayer
    {

        private SoundPlayer player;
        private Launch launch;
        const string NormalWavePath = @"sound\du.wav";
        const string WarnWavePath = @"sound\du2.wav";
      
        public int PlayState { get; set; } // 0=stop, 1=play,2=pause

        public AlermSoundPlayer()
        {
            player = new SoundPlayer();
            launch = new Launch(800);
            launch.OnElapsed += Launch_OnElapsed;

            OtherSettingData setting = Setting.Get<OtherSettingData>();       

        }

     


        private void Launch_OnElapsed()
        {
            Play();
        }

        public void PlayNormalSound()
        {
            PlaySound(NormalWavePath);
        }

        public void PlayNormalSoundOneTime()
        {
            //if (launch != null)
            //{
            //    launch.Stop();
            //}
            if (!player.SoundLocation.Contains(NormalWavePath))
                player.SoundLocation = NormalWavePath;
            Play();
        }

        public void PlayWarnSound()
        {
            PlaySound(WarnWavePath);
        }

        public void PlaySound(string path)
        {
            if (!player.SoundLocation.Contains(path))
                player.SoundLocation = path;
            player.Load();
            launch.Start();
            PlayState = 1;
        }

        public void Stop()
        {
            player.Stop();
            launch.Stop();
      //      player.SoundLocation = null;
            PlayState = 0;
        }

        public void Pause()
        {
            player.Stop();
            launch.Pause();
            PlayState = 2;
        }

        public void Play()
        {
            if (!string.IsNullOrWhiteSpace(player.SoundLocation))
                player.PlaySync();
             
        }

    }
}
