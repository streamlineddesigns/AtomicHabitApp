using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class BreathingRetentionController : Controller
    {
        public float timer = 0;
        public int wholeSeconds = 0;

        void OnEnable()
        {
            timer = 0;
            wholeSeconds = 0;
            BreathingAudioController.instance.PlayPingSound();
            BreathingAudioController.instance.PlayRetentionMusic();
            BreathingAudioController.instance.PlayRetentionGuidedVoice();
        }

        void Update()
        {
            timer += Time.deltaTime;
            if (timer - 1.0 > wholeSeconds) {
                wholeSeconds += 1;
                IncrementTimer();
            }
        }

        protected void IncrementTimer()
        {
            BreathingLoopController.instance.Model.RetentionTimer = wholeSeconds;

            if (((int) BreathingLoopController.instance.Model.RetentionTimer) % 60 == 0) BreathingAudioController.instance.PlayPingSound();

            if (BreathingLoopController.instance.Model.RetentionTimer > BreathingLoopController.instance.Model.retentionThresholdTilClick) {
                BreathingLoopController.instance.Model.BreathingRetentionClickMessage.SetActive(true);
                BreathingLoopController.instance.Model.canDoubleTap = true;
            }
            
            IncrementTimerText();
        }

        protected void IncrementTimerText()
        {
            BreathingLoopController.instance.Model.BreathingRetentionCenterText.text = BreathingLoopController.instance.GetReadableTime(BreathingLoopController.instance.Model.RetentionTimer);
        }
    }

}