using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace whm {

    public class MeditationSettingsController : Controller
    {
        public MeditationSettingsModel Model;

        public void Start()
        {
            Load();
        }

        public void Load()
        {
            Model.GuidedVoiceButtonImage.color = (Model.bIsGuidedVoiceOn) ? Model.OnColor : Model.OffColor; 
            Model.BackgroundMusicButtonImage.color = (Model.bIsBackgroundMusicOn) ? Model.OnColor : Model.OffColor; 
            Model.PingGongSoundButtonImage.color = (Model.bIsPingGongSoundOn) ? Model.OnColor : Model.OffColor; 
            Model.RainSoundsButtonImage.color = (Model.bIsRainSoundsOn) ? Model.OnColor : Model.OffColor; 

            Model.SessionDurationText.text = Model.getCurrentMeditationDurationString();
            MeditationAudioController.instance.SetGuidedVoice(Model.iMediationDuration);

            MeditationController.Singleton.SetTargetMeditationDuration(Model.getCurrentMeditationDurationFloat());
        }

        public void SessionDurationButtonClick()
        {
            switch(Model.currentMeditationDuration)
            {
                case MeditationDuration.FiveMinute :
                    Model.currentMeditationDuration = MeditationDuration.TenMinute;
                    break;

                case MeditationDuration.TenMinute :
                    Model.currentMeditationDuration = MeditationDuration.FifteenMinute;
                    break;

                case MeditationDuration.FifteenMinute :
                    Model.currentMeditationDuration = MeditationDuration.TwentyMinute;
                    break;
                
                case MeditationDuration.TwentyMinute :
                    Model.currentMeditationDuration = MeditationDuration.TwentyFiveMinute;
                    break;

                case MeditationDuration.TwentyFiveMinute :
                    Model.currentMeditationDuration = MeditationDuration.ThirtyMinute;
                    break;

                case MeditationDuration.ThirtyMinute :
                    Model.currentMeditationDuration = MeditationDuration.FortyFiveMinute;
                    break;

                case MeditationDuration.FortyFiveMinute :
                    Model.currentMeditationDuration = MeditationDuration.FiveMinute;
                    break;
            }

            Model.SetMeditationDuration((int)Model.currentMeditationDuration);
            Model.SessionDurationText.text = Model.getCurrentMeditationDurationString();
            MeditationAudioController.instance.SetGuidedVoice(Model.iMediationDuration);

            MeditationController.Singleton.SetTargetMeditationDuration(Model.getCurrentMeditationDurationFloat());
        }

        public void GuidedVoiceToggleClick()
        {
            Model.SetIsGuidedVoiceOn((Model.bIsGuidedVoiceOn) ? false : true);
            Model.GuidedVoiceButtonImage.color = (Model.bIsGuidedVoiceOn) ? Model.OnColor : Model.OffColor; 
            MeditationController.Singleton.SetTargetMeditationDuration(Model.getCurrentMeditationDurationFloat());
        }

        public void BackgroundMusicToggleClick()
        {
            Model.SetIsBackgroundMusicOn((Model.bIsBackgroundMusicOn) ? false : true);
            Model.BackgroundMusicButtonImage.color = (Model.bIsBackgroundMusicOn) ? Model.OnColor : Model.OffColor; 
        }

        public void PingGongSoundToggleClick()
        {
            Model.SetIsPingGongSoundOn((Model.bIsPingGongSoundOn) ? false : true);
            Model.PingGongSoundButtonImage.color = (Model.bIsPingGongSoundOn) ? Model.OnColor : Model.OffColor; 
        }

        public void RainSoundsToggleClick()
        {
            Model.SetIsRainSoundsOn((Model.bIsRainSoundsOn) ? false : true);
            Model.RainSoundsButtonImage.color = (Model.bIsRainSoundsOn) ? Model.OnColor : Model.OffColor; 
        }

        public void StartMeditation()
        {
            UIController.instance.Open(ViewName.MeditationStart);
        }
    }
}