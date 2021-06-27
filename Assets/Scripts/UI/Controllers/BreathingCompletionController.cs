using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class BreathingCompletionController : Controller
    {
        public Text AverageTimeText;
        public float totalTime;
        public GameObject RoundDataModelPrefab;
        public GameObject RoundDataContainer;
        public List<GameObject> RoundDataViews;

        void OnEnable()
        {
            resetRoundDataUI();

            generateNewRoundDataUI();

            AudioController.instance.StopAll();
            AudioController.instance.PlayGongSound();
            AudioController.instance.PlayCompletionGuidedVoice();
        }

        protected void resetRoundDataUI()
        {
            totalTime = 0;
            AverageTimeText.text = BreathingLoopController.instance.GetReadableTime(totalTime);

            if (RoundDataViews != null) {
                for(int m = 0; m < RoundDataViews.Count; m++) {
                    Destroy(RoundDataViews[m]);
                }
            }

            RoundDataViews = new List<GameObject>();
        }

        protected void generateNewRoundDataUI()
        {
            if (BreathingLoopController.instance.Model.RoundDataRegistry.Count > 0) {
                List<int> keyList = new List<int>(BreathingLoopController.instance.Model.RoundDataRegistry.Keys);

                for (int i = 0; i < keyList.Count; i++) {
                    GameObject roundView = Instantiate(RoundDataModelPrefab, RoundDataContainer.transform);
                    RoundDataModel roundModel = roundView.GetComponent<RoundDataModel>();
                    roundModel.RoundText.text = "ROUND " + keyList[i].ToString();
                    roundModel.TimeToCompleteText.text = BreathingLoopController.instance.GetReadableTime(BreathingLoopController.instance.Model.RoundDataRegistry[keyList[i]]);
                    RoundDataViews.Add(roundView);
                    totalTime += BreathingLoopController.instance.Model.RoundDataRegistry[keyList[i]];
                }

                AverageTimeText.text = BreathingLoopController.instance.GetReadableTime(totalTime / BreathingLoopController.instance.Model.RoundDataRegistry.Count);
            }
        }

        public void SaveButtonClick()
        {
            List<float> roundTimes = new List<float>();
            
            for (int i = 1; i < BreathingLoopController.instance.Model.RoundDataRegistry.Count + 1; i++) {
                //Debug.Log("Round: " + i + " Time: " + BreathingLoopController.instance.GetReadableTime(BreathingLoopController.instance.Model.RoundDataRegistry[i]));
                roundTimes.Add(BreathingLoopController.instance.Model.RoundDataRegistry[i]);
            }

            SavedResultModel result = new SavedResultModel(roundTimes);
            GameController.instance.SavedResultModelRegistry.addASpecificResult(result);
            string json = JsonUtility.ToJson(result);
            File.AppendAllText(GameController.instance.SavedResultModelRegistry.saveFilePath, json + Environment.NewLine);
            
            BreathingLoopController.instance.FinishButtonClick(true);
        }
    }

}