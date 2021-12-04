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
        public GameObject CountdownContent;
        public GameObject SessionContent;
        public GameObject FinishButton;
        public int PushUpsTilUserCanFinish = 1;

        public Text CountdownText;
        public float countdownTime = 10;
        protected bool bCountdown;

        public bool IsInit = false;
        public Text PushupLeftCountText;
        public int CurrentPushUpTimeIndex = 0;
        public List<float> VideoPushUpTimes = new List<float>();

        public void Start()
        {
            
        }

        public void OnEnable()
        {
            SallyUpVideo.loopPointReached += VideoEndReached;
            StartContent.SetActive(true);
            SessionContent.SetActive(false);
            CountdownContent.SetActive(false);
            VideoRenderTexture.SetActive(false);
            FinishButton.SetActive(false);
            SallyUpVideo.Stop();
            PushupLeftCountText.text = "30";
            CurrentPushUpTimeIndex = 0;
            countdownTime = 10;
            bCountdown = false;
            CountdownText.text = countdownTime.ToString();
        }

        public void OnDisable()
        {
            SallyUpVideo.loopPointReached -= VideoEndReached;
        }
        void Update()
        {
            if (! SallyUpVideo.isPlaying) {
                if (bCountdown) {
                    if (countdownTime > 0.0f) {
                        countdownTime -= Time.deltaTime;
                        CountdownText.text = countdownTime.ToString("0.00");
                    } else {
                        bCountdown = false;
                        StartVideo();
                    }
                }
                return;
            }

            int currentSallyUpVideoTime = (int) SallyUpVideo.time;

            if (CurrentPushUpTimeIndex < VideoPushUpTimes.Count && currentSallyUpVideoTime >= VideoPushUpTimes[CurrentPushUpTimeIndex]) {

                PushUpsController.Singleton.Model.completedPushUps = CurrentPushUpTimeIndex;
                CurrentPushUpTimeIndex++;

                if (! FinishButton.activeSelf && CurrentPushUpTimeIndex > PushUpsTilUserCanFinish) {
                    FinishButton.SetActive(true);
                }
                int pushupsLeft = 30 - (CurrentPushUpTimeIndex - 1);
                PushupLeftCountText.text = pushupsLeft.ToString();

            }
        }

        public void PlayButtonClick()
        {
            StartContent.SetActive(false);
            CountdownContent.SetActive(true);
            bCountdown = true;
        }

        protected void StartVideo()
        {
            bCountdown = false;
            CountdownContent.SetActive(false);
            SessionContent.SetActive(true);
            SallyUpVideo.Play();
            VideoRenderTexture.SetActive(true);
        }

        public void FinishButtonClick()
        {
            UIController.instance.Open(ViewName.PushUpsCompletion);
        }

        public void VideoEndReached(UnityEngine.Video.VideoPlayer vp)
        {
            FinishButtonClick();
        }
    }

}