using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class MeditationSettingsModel : Model
    {
        public Color OffColor;
        public Color OnColor;

        public Text SessionDurationText;
        public Image GuidedVoiceButtonImage;
        public Image BackgroundMusicButtonImage;
        public Image PingGongSoundButtonImage;
        public Image RainSoundsButtonImage;

        public Dictionary<MeditationDuration, string> mediationDurations = new Dictionary<MeditationDuration, string>  {{MeditationDuration.FiveMinute, "5:00"},
                                                                                                                        {MeditationDuration.TenMinute,   "10:00"},
                                                                                                                        {MeditationDuration.FifteenMinute,   "15:00"},
                                                                                                                        {MeditationDuration.TwentyMinute,   "20:00"},
                                                                                                                        {MeditationDuration.TwentyFiveMinute,   "25:00"},
                                                                                                                        {MeditationDuration.ThirtyMinute,   "30:00"},
                                                                                                                        {MeditationDuration.FortyFiveMinute, "45:00"}};

        public Dictionary<MeditationDuration, float> mediationDurationFloats = new Dictionary<MeditationDuration, float>  {{MeditationDuration.FiveMinute, 5.0f},
                                                                                                                        {MeditationDuration.TenMinute,   10.0f},
                                                                                                                        {MeditationDuration.FifteenMinute,   15.0f},
                                                                                                                        {MeditationDuration.TwentyMinute,   20.0f},
                                                                                                                        {MeditationDuration.TwentyFiveMinute,   25.0f},
                                                                                                                        {MeditationDuration.ThirtyMinute,   30.0f},
                                                                                                                        {MeditationDuration.FortyFiveMinute, 45.0f}};       
        public List<MeditationDuration> mediationDurationKeys;
        public MeditationDuration currentMeditationDuration = MeditationDuration.FiveMinute;   

        public int  iMediationDuration;
        public bool bIsGuidedVoiceOn;
        public bool bIsBackgroundMusicOn;
        public bool bIsPingGongSoundOn;
        public bool bIsRainSoundsOn;

        public void Awake()
        {
            Load();
        }

        public void Load()
        {
            mediationDurationKeys = new List<MeditationDuration>(mediationDurations.Keys);
            iMediationDuration       = (PlayerPrefs.HasKey("MeditationDuration")) ? PlayerPrefs.GetInt("MeditationDuration") : 0;
            currentMeditationDuration =  mediationDurationKeys[iMediationDuration];
            bIsGuidedVoiceOn     = (PlayerPrefs.HasKey("MeditationGuidedVoice"))      ? (PlayerPrefs.GetInt("MeditationGuidedVoice") == 1      ? true : false) : true;
            bIsBackgroundMusicOn = (PlayerPrefs.HasKey("MeditationBackgroundMusic"))  ? (PlayerPrefs.GetInt("MeditationBackgroundMusic") == 1  ? true : false) : true;
            bIsPingGongSoundOn   = (PlayerPrefs.HasKey("MeditationPingGongSound"))    ? (PlayerPrefs.GetInt("MeditationPingGongSound") == 1    ? true : false) : true;
            bIsRainSoundsOn      = (PlayerPrefs.HasKey("MeditationRainSounds"))       ? (PlayerPrefs.GetInt("MeditationRainSounds") == 1       ? true : false) : true;
        }

        public string getCurrentMeditationDurationString()
        {
           return mediationDurations[mediationDurationKeys[iMediationDuration]];
        }

        public float getCurrentMeditationDurationFloat()
        {
           return (bIsGuidedVoiceOn) ? MeditationAudioController.instance.GetGuidedVoiceLength() : mediationDurationFloats[currentMeditationDuration] * 60.0f;
        }

        public void SetMeditationDuration(int i)
        {
            iMediationDuration = i;
            PlayerPrefs.SetInt("MeditationDuration", iMediationDuration);
            PlayerPrefs.Save();
        }

        public void SetIsGuidedVoiceOn(bool b)
        {
            bIsGuidedVoiceOn = b;
            PlayerPrefs.SetInt("MeditationGuidedVoice", (bIsGuidedVoiceOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetIsBackgroundMusicOn(bool b)
        {
            bIsBackgroundMusicOn = b;
            PlayerPrefs.SetInt("MeditationBackgroundMusic", (bIsBackgroundMusicOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetIsPingGongSoundOn(bool b)
        {
            bIsPingGongSoundOn = b;
            PlayerPrefs.SetInt("MeditationPingGongSound", (bIsPingGongSoundOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetIsRainSoundsOn(bool b)
        {
            bIsRainSoundsOn = b;
            PlayerPrefs.SetInt("MeditationRainSounds", (bIsRainSoundsOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

    }

}