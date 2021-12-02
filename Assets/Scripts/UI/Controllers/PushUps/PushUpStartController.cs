using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace whm {

    public class PushUpStartController : Controller
    {
        public VideoPlayer SallyUpVideo;
        public GameObject VideoRenderTexture;
        public GameObject StartContent;
        public GameObject SessionContent;

        public bool IsInit = false;
        public Text PushupLeftCountText;
        public int CurrentPushUpTimeIndex = 0;
        public List<float> VideoPushUpTimes = new List<float>();

        public void Start()
        {
            
        }

        public void OnEnable()
        {
            StartContent.SetActive(true);
            SessionContent.SetActive(false);
            VideoRenderTexture.SetActive(false);
            SallyUpVideo.Stop();
            PushupLeftCountText.text = "30";
            CurrentPushUpTimeIndex = 0;
            IsInit = false;
        }

        public void OnDisable()
        {

        }
        void Update()
        {
            if (! SallyUpVideo.isPlaying) {
                if (IsInit) {
                    //previously initialized and just finished the entire video
                }
                return;
            }

            int currentSallyUpVideoTime = (int) SallyUpVideo.time;

            if (CurrentPushUpTimeIndex < VideoPushUpTimes.Count && currentSallyUpVideoTime >= VideoPushUpTimes[CurrentPushUpTimeIndex]) {

                CurrentPushUpTimeIndex++;
                int pushupsLeft = 30 - (CurrentPushUpTimeIndex - 1);
                PushupLeftCountText.text = pushupsLeft.ToString();

            }
        }

        public void PlayButtonClick()
        {
            StartContent.SetActive(false);
            SessionContent.SetActive(true);
            SallyUpVideo.Play();
            VideoRenderTexture.SetActive(true);
            IsInit = true;
        }

        public void FinishButtonClick()
        {
            
        }
    }

}