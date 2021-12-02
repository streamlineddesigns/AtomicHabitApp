using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class BreathingRecoveryController : Controller
    {
        public float timer = 0;
        protected float delay;
        public float initialDelay = 3.0f;
        public int wholeSeconds = 0;

        void OnEnable()
        {
            timer = 0;
            delay = initialDelay;
            wholeSeconds = 0;
            AudioController.instance.PlayRecoveryMusic();
            AudioController.instance.PlayRecoveryGuidedVoice();
        }

        void Update()
        {
            if (delay > 0.0f) {
                delay -= Time.deltaTime;
                return;
            }

            timer += Time.deltaTime;
            if (timer - 1.0 > wholeSeconds) {
                wholeSeconds += 1;
                DecrementTimer();
            }
        }

        protected void DecrementTimer()
        {
            BreathingLoopController.instance.Model.RecoveryTimer -= 1;

            if (BreathingLoopController.instance.Model.RecoveryTimer < -1.0f) {//-1 for an extra second of waiting
                BreathingLoopController.instance.SaveRound();
                BreathingLoopController.instance.NextStep();
            }
            
            if (BreathingLoopController.instance.Model.RecoveryTimer >= 0) SetTimerText();
        }

        protected void SetTimerText()
        {
            BreathingLoopController.instance.Model.BreathingRecoveryCenterText.text = BreathingLoopController.instance.GetReadableTime(BreathingLoopController.instance.Model.RecoveryTimer);
        }
    }

}