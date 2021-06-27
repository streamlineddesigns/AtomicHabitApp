using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class SettingsController : Controller
    {
        public SettingsModel Model;

        public void Start()
        {
            Load();
        }

        public void Load()
        {
            Model.GuidedVoiceButtonImage.color = (Model.bIsGuidedVoiceOn) ? Model.OnColor : Model.OffColor; 

            Model.BackgroundMusicButtonImage.color = (Model.bIsBackgroundMusicOn) ? Model.OnColor : Model.OffColor; 
            Model.PingGongSoundButtonImage.color = (Model.bIsPingGongSoundOn) ? Model.OnColor : Model.OffColor; 
            Model.BreathingSoundButtonImage.color = (Model.bIsBreathingSoundOn) ? Model.OnColor : Model.OffColor; 

            Model.BreathingPaceText.text = Model.getCurrentPaceString();
            AudioController.instance.SetStartGuidedVoice(Model.iBreathingPace);
        }

        public void BreathingPaceButtonClick()
        {
            switch(Model.currentPace)
            {
                case PaceName.Normal :
                    Model.currentPace = PaceName.Fast;
                    break;

                case PaceName.Fast :
                    Model.currentPace = PaceName.Slow;
                    break;

                case PaceName.Slow :
                    Model.currentPace = PaceName.Normal;
                    break;
            }

            Model.SetBreathingPace((int)Model.currentPace);
            Model.BreathingPaceText.text = Model.getCurrentPaceString();
            AudioController.instance.SetStartGuidedVoice(Model.iBreathingPace);
        }

        public void GuidedVoiceToggleClick()
        {
            Model.SetIsGuidedVoiceOn((Model.bIsGuidedVoiceOn) ? false : true);
            Model.GuidedVoiceButtonImage.color = (Model.bIsGuidedVoiceOn) ? Model.OnColor : Model.OffColor; 
        }

        public void SongButtonClick()
        {

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

        public void BreathingSoundToggleClick()
        {
            Model.SetIsBreathingSoundOn((Model.bIsBreathingSoundOn) ? false : true);
            Model.BreathingSoundButtonImage.color = (Model.bIsBreathingSoundOn) ? Model.OnColor : Model.OffColor; 
        }

        public void BreathingStartButtonClick()
        {
            BreathingLoopController.instance.StartBreathing();
        }
    }

}