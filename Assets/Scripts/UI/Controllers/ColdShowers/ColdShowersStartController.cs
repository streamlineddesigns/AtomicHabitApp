using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

namespace whm {

    public class ColdShowersStartController : Controller
    {
        public GameObject StartContent;
        public GameObject CountdownContent;
        public GameObject SessionContent;
        public GameObject FinishButton;

        public float preparationCountdownTime = 10;
        public Text preparationCountdownText;
        protected bool bPreparationCountdownActive;

        public float minTime = 15.0f;//in seconds
        public float maxTime = 900.0f;//in seconds
        public float startTime = 60.0f;//in seconds
        public float timeIncrements = 15.0f;//in seconds
        protected float userSetTime;//in seconds
        public Text userSetTimeText;

        protected bool bColdShowerTimerActive;
        public float TimeTilUserCanFinish = 5.0f;//in seconds
        protected float timeLeft;//in seconds
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

            preparationCountdownTime = 10;
            preparationCountdownText.text = preparationCountdownTime.ToString();
            bPreparationCountdownActive = false;
            bColdShowerTimerActive = false;

            userSetTime = startTime;
            updateUserSetTimeText();
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
                    StartColdShowerTimer();
                }
                return;
            }

            if (bColdShowerTimerActive) {
                if (timeLeft > 0.0f) {
                    timeLeft -= Time.deltaTime;
                    if (! FinishButton.activeSelf && userSetTime - timeLeft > TimeTilUserCanFinish) {
                        FinishButton.SetActive(true);
                    }
                    updateTimeLeftText();
                } else {
                    bColdShowerTimerActive = false;
                    TimerEndReached();
                }
            }
        }

        public void PlayButtonClick()
        {
            StartContent.SetActive(false);
            CountdownContent.SetActive(true);
            bPreparationCountdownActive = true;
            ColdShowersController.Singleton.Model.targetShowerDuration = userSetTime - 1.0f;
            timeLeft = ColdShowersController.Singleton.Model.targetShowerDuration;
        }

        protected void StartColdShowerTimer()
        {
            bColdShowerTimerActive = true;
            bPreparationCountdownActive = false;
            CountdownContent.SetActive(false);
            SessionContent.SetActive(true);
            AudioController.instance.PlayPingSound();
        }

        protected void updateUserSetTimeText()
        {
            userSetTimeText.text = UIController.instance.GetReadableTime(userSetTime);
        }

        protected void updateTimeLeftText()
        {
            timeLeftText.text = UIController.instance.GetReadableTime(timeLeft);
        }

        public void IncrementButtonClick()
        {
            if (userSetTime < maxTime) userSetTime += timeIncrements;
            updateUserSetTimeText();
        }

        public void DecrementButtonClick()
        {
            if (userSetTime > minTime) userSetTime -= timeIncrements;
            updateUserSetTimeText();
        }


        public void FinishButtonClick()
        {
            if (timeLeft <= 0) {
                ColdShowersController.Singleton.Model.actualShowerDuration = ColdShowersController.Singleton.Model.targetShowerDuration + 1.0f;
            } else {
                ColdShowersController.Singleton.Model.actualShowerDuration = (ColdShowersController.Singleton.Model.targetShowerDuration - timeLeft) + 1.0f;
            }

            UIController.instance.Open(ViewName.ColdShowersCompletion);
        }

        public void TimerEndReached()
        {
            AudioController.instance.PlayPingSound();
            AudioController.instance.PlayGongSound();
            FinishButtonClick();
        }
    }
}