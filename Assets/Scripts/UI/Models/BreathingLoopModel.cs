using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class BreathingLoopModel : Model
    {
        public Dictionary<int, float> RoundDataRegistry;
        public BreathingStep currentStep;
        public int currentRound;
        public int breatheCount;
        public float RecoveryTimer;
        public float InitialRecoveryTimer = 15;//15 seconds
        public float RetentionTimer;
        public float InitialRetentionTimer = 0;//0 seconds
        public bool canDoubleTap = false;

        public int breatheThresholdTilClick = 4;//4 breathes
        public float retentionThresholdTilClick = 4;//4 seconds

        public Text BreathingStartRoundText;
        public Text BreathingRetentionRoundText;
        public Text BreathingRecoveryRoundText;

        public Text BreathingStartCenterText;
        public Text BreathingRetentionCenterText;
        public Text BreathingRecoveryCenterText;
        public GameObject PreviousRoundData;
        public Text PreviousRoundDataText;

        public GameObject BreathingStartClickMessage;
        public GameObject BreathingRetentionClickMessage;

        public void Awake()
        {
            Load();
        }

        public void OnEnable()
        {
            EventPublisher.OnStepChange += OnStepChange;
        }

        public void OnDisable()
        {
            EventPublisher.OnStepChange -= OnStepChange;
        }

        //Gets called OnStepChange
        public void OnStepChange(BreathingStep step)
        {
            breatheCount = 1;
            RecoveryTimer = InitialRecoveryTimer;
            canDoubleTap = false;
            BreathingStartClickMessage.SetActive(false);
            BreathingRetentionClickMessage.SetActive(false);
        }

        //Gets called when breathing loop first starts
        public void Load()
        {
            RoundDataRegistry = new Dictionary<int, float>();
            currentStep = BreathingStep.Start;
            currentRound = 1;
            breatheCount = 1;
            RecoveryTimer = InitialRecoveryTimer;
            RetentionTimer = InitialRetentionTimer;
            canDoubleTap = false;
            BreathingStartClickMessage.SetActive(false);
            BreathingRetentionClickMessage.SetActive(false);
        }
    }

}