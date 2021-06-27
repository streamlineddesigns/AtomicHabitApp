using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class AudioController : MonoBehaviour
    {
        public static AudioController instance;
        public AudioSource InhaleSound;
        public AudioSource ExhaleSound;
        public AudioSource PingSound;
        public AudioSource GongSound;

        public List<AudioSource> StartGuidedVoiceList = new List<AudioSource>();
        public AudioSource StartGuidedVoice;

        public AudioSource CompletionGuidedVoice;
        public AudioSource RetentionGuidedVoice;
        public AudioSource RecoveryGuidedVoice;

        public AudioSource RetentionMusic;
        public AudioSource RecoveryMusic;

        public void Awake()
        {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(instance);
            }
        }

        public void OnEnable()
        {
            EventPublisher.OnStepChange += OnStepChange;
        }

        public void OnDisable()
        {
            EventPublisher.OnStepChange -= OnStepChange;
        }

        public void OnStepChange(BreathingStep currentStep)
        {
            switch(currentStep) {

                case BreathingStep.Start :
                    StartGuidedVoice.Stop();
                    InhaleSound.Stop();
                    ExhaleSound.Stop();
                    break;

                case BreathingStep.Retention :
                    RetentionGuidedVoice.Stop();
                    RetentionMusic.Stop();
                    PingSound.Stop();
                    break;

                case BreathingStep.Recovery :
                    RecoveryGuidedVoice.Stop();
                    RecoveryMusic.Stop();
                    break;
            }
        }

        public void StopAll()
        {
            //start
            StartGuidedVoice.Stop();
            InhaleSound.Stop();
            ExhaleSound.Stop();
        
            //retention
            RetentionGuidedVoice.Stop();
            RetentionMusic.Stop();
            PingSound.Stop();

            //recovery
            RecoveryGuidedVoice.Stop();
            RecoveryMusic.Stop();

            //completion
            CompletionGuidedVoice.Stop();
            GongSound.Stop();   
        }

        public void SetStartGuidedVoice(int i)
        {
            StartGuidedVoice = StartGuidedVoiceList[i];
        }

        public void PlayInhaleSound()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsBreathingSoundOn) InhaleSound.Play();
        }

        public void PlayExhaleSound()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsBreathingSoundOn) ExhaleSound.Play();
        }

        public void PlayPingSound()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsPingGongSoundOn) PingSound.Play();
        }

        public void PlayGongSound()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsPingGongSoundOn) GongSound.Play();
        }

        public void PlayStartGuidedVoice()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsGuidedVoiceOn) StartGuidedVoice.Play();
        }

        public void PlayRetentionGuidedVoice()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsGuidedVoiceOn) RetentionGuidedVoice.Play();
        }

        public void PlayRecoveryGuidedVoice()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsGuidedVoiceOn) RecoveryGuidedVoice.Play();
        }

        public void PlayCompletionGuidedVoice()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsGuidedVoiceOn) CompletionGuidedVoice.Play();
        }

        public void PlayRecoveryMusic()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsBackgroundMusicOn) RecoveryMusic.Play();
        }

        public void PlayRetentionMusic()
        {
            if (BreathingLoopController.instance.SettingsModel.bIsBackgroundMusicOn) RetentionMusic.Play();
        }
    }

}