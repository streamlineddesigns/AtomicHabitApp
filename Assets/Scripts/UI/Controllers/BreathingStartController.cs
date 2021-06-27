using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class BreathingStartController : Controller
    {
        public GameObject image;

        protected float floatMaxXScale = 1.0f;
        protected float floatMaxYScale = 1.0f;
        protected float floatMinXScale = 0.5f;
        protected float floatMinYScale = 0.5f;

        protected bool isBreathingIn = true;
        protected Dictionary<PaceName, float> paceSpeeds = new Dictionary<PaceName, float>{{PaceName.Normal, 1.75f},
                                                                                           {PaceName.Fast, 1.25f},
                                                                                           {PaceName.Slow, 2.25f}};
        public float scaleTime;
        protected float scaleStep;

        void OnEnable()
        {
            scaleTime = paceSpeeds[BreathingLoopController.instance.SettingsModel.currentPace];
            scaleStep = (0.5f / 30.0f) / scaleTime;
            isBreathingIn = true;
            image.transform.localScale = new Vector3(floatMinXScale, floatMinYScale, image.transform.localScale.z);
            AudioController.instance.PlayInhaleSound();


            AudioController.instance.PlayStartGuidedVoice();
        }

        void Update()
        {
            if (isBreathingIn) {
                if (image.transform.localScale.x <= floatMaxXScale || image.transform.localScale.y <= floatMaxYScale) {
                    image.transform.localScale = new Vector3(image.transform.localScale.x + scaleStep, image.transform.localScale.y + scaleStep, image.transform.localScale.z);
                } else {
                    isBreathingIn = false;
                    AudioController.instance.PlayExhaleSound();
                }
            } else {
                if (image.transform.localScale.x >= floatMinXScale || image.transform.localScale.y >= floatMinYScale) {
                    image.transform.localScale = new Vector3(image.transform.localScale.x - scaleStep, image.transform.localScale.y - scaleStep, image.transform.localScale.z);
                }  else {
                    isBreathingIn = true;
                    AudioController.instance.PlayInhaleSound();
                    IncrementBreatheCount();
                }
            }
        }

        protected void IncrementBreatheCount()
        {
            BreathingLoopController.instance.Model.breatheCount++;
            if (BreathingLoopController.instance.Model.breatheCount > BreathingLoopController.instance.Model.breatheThresholdTilClick) {
                BreathingLoopController.instance.Model.BreathingStartClickMessage.SetActive(true);
                BreathingLoopController.instance.Model.canDoubleTap = true;
            }
            SetBreatheCountText();
        }

        protected void SetBreatheCountText()
        {
            BreathingLoopController.instance.Model.BreathingStartCenterText.text = BreathingLoopController.instance.Model.breatheCount.ToString();
        }
    }

}