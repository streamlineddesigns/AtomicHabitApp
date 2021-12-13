using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class BreathingLoopController : Controller
    {
        public static BreathingLoopController instance;
        public BreathingLoopModel Model;
        public BreathingSettingsModel SettingsModel;
        protected EventPublisher EventPublisher;

        public void Awake()
        {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(instance);
            }

            EventPublisher = new EventPublisher();
        }

        public void OnEnable()
        {
            EventPublisher.OnStepChange += OnStepChange;
            EventPublisher.OnDoubleClick += OnDoubleClick;
        }

        public void OnDisable()
        {
            EventPublisher.OnStepChange -= OnStepChange;
            EventPublisher.OnDoubleClick -= OnDoubleClick;
        }

        public void FinishButtonClick(bool ImmediateFinish = false)
        {
            BreathingLoopController.instance.Model.canDoubleTap = false;
            AudioController.instance.StopAll();

            if (ImmediateFinish) {
                UIController.instance.Open(ViewName.Start);
                return;
            }

            if (Model.currentRound > 1) {
                Model.currentStep = BreathingStep.Completion;
                UIController.instance.Open(ViewName.BreathingCompletion);
            } else {
                UIController.instance.Open(ViewName.Start);
            }
        }

        public void OnStepChange(BreathingStep step)
        {
            switch(step) {

                case BreathingStep.Start :
                    Model.currentStep = BreathingStep.Retention;
                    UIController.instance.Open(ViewName.BreathingRetention);
                    break;

                case BreathingStep.Retention :
                    Model.currentStep = BreathingStep.Recovery;
                    UIController.instance.Open(ViewName.BreathingRecovery);
                    break;

                case BreathingStep.Recovery :
                    Model.currentStep = BreathingStep.Start;
                    Model.currentRound++;
                    UIController.instance.Open(ViewName.BreathingStart);
                    UpdateUI();
                    break;
            }
        }

        //Gets called OnStepChange only after Recovery Step is done :)
        public void UpdateUI()
        {
            Model.BreathingStartRoundText.text = "ROUND " + Model.currentRound.ToString();
            Model.BreathingRetentionRoundText.text = "ROUND " + Model.currentRound.ToString();
            Model.BreathingRecoveryRoundText.text = "ROUND " + Model.currentRound.ToString();

            if (Model.currentRound > 1) {
                Model.PreviousRoundData.SetActive(true);
                Model.PreviousRoundDataText.text = GetReadableTime(Model.RetentionTimer);
            } else {
                Model.PreviousRoundData.SetActive(false);
            }

            Model.BreathingStartCenterText.text = Model.breatheCount.ToString();
            Model.BreathingRetentionCenterText.text = GetReadableTime(Model.InitialRetentionTimer);
            Model.BreathingRecoveryCenterText.text = GetReadableTime(Model.InitialRecoveryTimer);
        }

        public string GetReadableTime(float originalTime)
        {
            float minutes = (int) originalTime / 60;
            float seconds = originalTime % 60.0f;
            string readable = minutes.ToString("") + ":" + seconds.ToString("00");
            return readable;
        }

        public string GetReadableHourlyTime(float originalTime)
        {
            float totalHours = originalTime / 3600;
            int hours = (int) totalHours;
            float minutes = (int) ((originalTime % 3600) / 60);
            string readable = "";
            if (minutes >= 10) {
                readable = hours.ToString("") + "h " + minutes.ToString("") + "m";
            } else {
                readable = hours.ToString("") + "h 0" + minutes.ToString("") + "m";
            }
            
            return readable;
        }

        public void StartBreathing()
        {
            Model.Load();
            UpdateUI();
            UIController.instance.Open(ViewName.BreathingStart);
        }

        public void OnDoubleClick()
        {
            if (Model.canDoubleTap) NextStep();
        }

        public void SkipButtonClick()
        {
            if (Model.canDoubleTap) NextStep();
        }

        public void NextStep()
        {
            EventPublisher.PublishBreathingStepChange(Model.currentStep);
        }

        public void SaveRound()
        {
            int round = Model.currentRound;
            float timeComplete = Model.RetentionTimer;
            //Debug.Log("Round: " + round + ". Time Completed: " + timeComplete);
            Model.RoundDataRegistry.Add(round, timeComplete);
        }
    }

}