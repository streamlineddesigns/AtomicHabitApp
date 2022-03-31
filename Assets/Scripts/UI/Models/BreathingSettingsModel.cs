using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class BreathingSettingsModel : Model
    {
        public Dictionary<PaceName, string> Paces = new Dictionary<PaceName, string> {{PaceName.Normal, "NORMAL"},
                                                                                         {PaceName.Fast,   "FAST"},
                                                                                         {PaceName.Slow,   "SLOW"}};       
        public List<PaceName> paceKeys;
        
        public PaceName currentPace = PaceName.Normal;                                         
        public int  iBreathingPace;
        public bool bIsGuidedVoiceOn;
        public int  iSong;
        public bool bIsBackgroundMusicOn;
        public bool bIsPingGongSoundOn;
        public bool bIsBreathingSoundOn;

        public Color OffColor;
        public Color OnColor;

        public Text BreathingPaceText;
        public Image GuidedVoiceButtonImage;
        public Text SongText;
        public Image BackgroundMusicButtonImage;
        public Image PingGongSoundButtonImage;
        public Image BreathingSoundButtonImage;

        public void Awake()
        {
            Load();
        }

        public void Load()
        {
            paceKeys = new List<PaceName>(Paces.Keys);
            iBreathingPace       = (PlayerPrefs.HasKey("BreathingPace")) ? PlayerPrefs.GetInt("BreathingPace") : 0;
            currentPace = paceKeys[iBreathingPace];
            bIsGuidedVoiceOn     = (PlayerPrefs.HasKey("GuidedVoice")) ? (PlayerPrefs.GetInt("GuidedVoice") == 1 ? true : false) : true;
            iSong                = (PlayerPrefs.HasKey("Song")) ? PlayerPrefs.GetInt("Song") : 0;
            bIsBackgroundMusicOn = (PlayerPrefs.HasKey("BackgroundMusic")) ? (PlayerPrefs.GetInt("BackgroundMusic") == 1 ? true : false) : true;
            bIsPingGongSoundOn   = (PlayerPrefs.HasKey("PingGongSound")) ? (PlayerPrefs.GetInt("PingGongSound") == 1 ? true : false) : true;
            bIsBreathingSoundOn  = (PlayerPrefs.HasKey("BreathingSound")) ? (PlayerPrefs.GetInt("BreathingSound") == 1 ? true : false) : true;
        }

        public string getCurrentPaceString()
        {
           return Paces[paceKeys[iBreathingPace]];
        }

        public void SetBreathingPace(int i)
        {
            iBreathingPace = i;
            PlayerPrefs.SetInt("BreathingPace", iBreathingPace);
            PlayerPrefs.Save();
        }

        public void SetIsGuidedVoiceOn(bool b)
        {
            bIsGuidedVoiceOn = b;
            PlayerPrefs.SetInt("GuidedVoice", (bIsGuidedVoiceOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetSong(int i)
        {
            iSong = i;
            PlayerPrefs.SetInt("Song", iSong);
            PlayerPrefs.Save();
        }

        public void SetIsBackgroundMusicOn(bool b)
        {
            bIsBackgroundMusicOn = b;
            PlayerPrefs.SetInt("BackgroundMusic", (bIsBackgroundMusicOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetIsPingGongSoundOn(bool b)
        {
            bIsPingGongSoundOn = b;
            PlayerPrefs.SetInt("PingGongSound", (bIsPingGongSoundOn) ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetIsBreathingSoundOn(bool b)
        {
            bIsBreathingSoundOn = b;
            PlayerPrefs.SetInt("BreathingSound", (bIsBreathingSoundOn) ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

}