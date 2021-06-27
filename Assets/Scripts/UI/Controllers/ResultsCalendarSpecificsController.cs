using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm{

    public class ResultsCalendarSpecificsController : Controller
    {
        public GameObject RoundDataModelPrefab;
        public GameObject RoundDataContainer;
        public List<GameObject> RoundDataViews;

        public Text DateTimeText;
        public Text AverageTimeText;

        protected float totalTime;

        public void OnEnable()
        {
            totalTime = 0;

            DateTime date = new DateTime(UIController.instance.selectedResult.dateTime);
            DateTimeText.text = date.ToString("MM/dd/yyyy HH:mm");

            if (RoundDataViews != null) {
                for(int m = 0; m < RoundDataViews.Count; m++) {
                    Destroy(RoundDataViews[m]);
                }
            }

            RoundDataViews = new List<GameObject>();
            

            for (int i = 0; i < UIController.instance.selectedResult.roundTimes.Count; i++) {
                GameObject roundView = Instantiate(RoundDataModelPrefab, RoundDataContainer.transform);
                RoundDataModel roundModel = roundView.GetComponent<RoundDataModel>();
                roundModel.RoundText.text = "ROUND " + (i + 1).ToString();
                roundModel.TimeToCompleteText.text = BreathingLoopController.instance.GetReadableTime(UIController.instance.selectedResult.roundTimes[i]);
                RoundDataViews.Add(roundView);
                totalTime += UIController.instance.selectedResult.roundTimes[i];
            }

            AverageTimeText.text = BreathingLoopController.instance.GetReadableTime(totalTime / UIController.instance.selectedResult.roundTimes.Count);
        }

        public void OnDisable()
        {

        }

        public void CloseButtonClick()
        {
            UIController.instance.CloseImmediately(ViewName.ResultsCalendarSpecifics);
        }
    }

}