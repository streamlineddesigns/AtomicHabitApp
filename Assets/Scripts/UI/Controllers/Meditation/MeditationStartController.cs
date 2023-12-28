using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace whm {

    public class MeditationStartController : Controller {

        public GameObject StartContent;
        public GameObject CountdownContent;
        public GameObject SessionContent;
        public GameObject FinishButton;

        public float preparationCountdownTime = 0.0f;
        public Text preparationCountdownText;
        protected bool bPreparationCountdownActive;

        protected float userSetTime;//in seconds
        protected bool bMeditationTimerActive;
        public float TimeTilUserCanFinish = 5.0f;//in seconds
        public float timeLeft;//in seconds
        public Text timeLeftText;

        public void Start()
        {
            
        }

        public void OnEnable()
        {
            StartContent.SetActive(true);
            SessionContent.SetActive(false);
            CountdownContent.SetActive(false);
            FinishButton.SetActive(false);

            preparationCountdownTime = 0.0f;
            preparationCountdownText.text = preparationCountdownTime.ToString();
            bPreparationCountdownActive = false;
            bMeditationTimerActive = false;

            userSetTime = MeditationController.Singleton.Model.targetMeditationDuration;
        }

        public void OnDisable()
        {
            
        }

        void Update()
        {
            if (bPreparationCountdownActive) {
                if (preparationCountdownTime > 0.0f) {
                    preparationCountdownTime -= Time.deltaTime;
                    preparationCountdownText.text = preparationCountdownTime.ToString("0.00");
                } else {
                    bPreparationCountdownActive = false;
                    StartMeditationTimer();
                }
                return;
            }

            if (bMeditationTimerActive) {
                if (timeLeft > 0.0f) {
                    timeLeft -= Time.deltaTime;
                    if (! FinishButton.activeSelf && userSetTime - timeLeft > TimeTilUserCanFinish) {
                        FinishButton.SetActive(true);
                    }
                    updateTimeLeftText();
                } else {
                    bMeditationTimerActive = false;
                    TimerEndReached();
                }
            }
        }

        public void PlayButtonClick()
        {
            StartContent.SetActive(false);
            CountdownContent.SetActive(true);
            bPreparationCountdownActive = true;
            timeLeft = MeditationController.Singleton.Model.targetMeditationDuration;
        }

        protected void StartMeditationTimer()
        {
            bMeditationTimerActive = true;
            bPreparationCountdownActive = false;
            CountdownContent.SetActive(false);
            SessionContent.SetActive(true);
            MeditationAudioController.instance.PlayPingSound();
            MeditationAudioController.instance.PlayGuidedVoice();
        }

        protected void updateTimeLeftText()
        {
            timeLeftText.text = UIController.instance.GetReadableTime(timeLeft);
        }

        public void FinishButtonClick()
        {
            if (timeLeft <= 0) {
                MeditationController.Singleton.Model.actualMeditationDuration = MeditationController.Singleton.Model.targetMeditationDuration + 1.0f;
            } else {
                MeditationController.Singleton.Model.actualMeditationDuration = (MeditationController.Singleton.Model.targetMeditationDuration - timeLeft) + 1.0f;
            }

            MeditationAudioController.instance.StopAll();
            UIController.instance.Open(ViewName.MeditationCompletion);
        }

        public void TimerEndReached()
        {
            FinishButtonClick();
        }

    }

}