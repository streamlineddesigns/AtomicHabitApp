using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class MeditationAudioController : MonoBehaviour
    {
        public static MeditationAudioController instance;
        public List<AudioSource> GuidedVoiceList = new List<AudioSource>();
        public AudioSource GuidedVoice;
        public AudioSource BackgroundMusic;
        public AudioSource PingSound;
        public AudioSource GongSound;
        public AudioSource RainSounds;
        
        public void Awake()
        {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(instance);
            }
        }

        public void StopAll()
        {
            if (GuidedVoice!=null) GuidedVoice.Stop();
            BackgroundMusic.Stop();
            PingSound.Stop();
            GongSound.Stop();
            RainSounds.Stop();
        }

        public void SetGuidedVoice(int i)
        {
            GuidedVoice = GuidedVoiceList[i];
        }

        public float GetGuidedVoiceLength()
        {
            return GuidedVoice.clip.length;
        }

        public void PlayGuidedVoice()
        {
            if (MeditationController.Singleton.SettingsModel.bIsGuidedVoiceOn) GuidedVoice.Play();
        }

        public void PlayPingSound()
        {
            if (MeditationController.Singleton.SettingsModel.bIsPingGongSoundOn) PingSound.Play();
        }

        public void PlayGongSound()
        {
            if (MeditationController.Singleton.SettingsModel.bIsPingGongSoundOn) GongSound.Play();
        }
    }

}