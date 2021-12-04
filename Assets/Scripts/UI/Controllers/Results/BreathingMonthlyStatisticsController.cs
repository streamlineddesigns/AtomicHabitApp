using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class BreathingMonthlyStatisticsController : Controller, IMonthlyStatisticsController
    {
        public Text averageRetentionTimeText;
        public Text longestRoundTimeText;
        public Text totalBreathingSessionsText;
        public Text totalRoundsText;
        public Text totalRetentionTimeText;
        public Text currentMonthText;

        public void SetMonthlyOverview()
        {
            currentMonthText.text = UIController.instance.selectedMonth.ToString("MMMM") + " Summary";

            if (GameController.instance.SavedResultModelRegistry.doesMonthHaveData(SavedResultType.Breathing, UIController.instance.selectedMonth)) {

                SetMonthlyOverViewWithData();

            } else {
                SetMonthlyOverviewToZero();
            }
        }

        public void SetMonthlyOverViewWithData()
        {
            List<SavedResultModel> monthlyResults = GameController.instance.SavedResultModelRegistry.getAnEntireMonthsResults(SavedResultType.Breathing, UIController.instance.selectedMonth); 
            int totalBreathingSessions = monthlyResults.Count;
            int totalRounds = 0;
            float longestRound = 0;
            float totalRetentionTime = 0;

            for (int i = 0; i < monthlyResults.Count; i++) {
                totalRounds += monthlyResults[i].floatNumberList.Count;

                for (int j = 0; j < monthlyResults[i].floatNumberList.Count; j++) {

                    totalRetentionTime += monthlyResults[i].floatNumberList[j];
                    
                    if (monthlyResults[i].floatNumberList[j] > longestRound) {
                        longestRound = monthlyResults[i].floatNumberList[j];
                    }

                }

            }

            totalBreathingSessionsText.text = totalBreathingSessions.ToString();
            totalRoundsText.text = totalRounds.ToString();
            longestRoundTimeText.text = BreathingLoopController.instance.GetReadableTime(longestRound);
            averageRetentionTimeText.text = BreathingLoopController.instance.GetReadableTime(totalRetentionTime / totalRounds);
            totalRetentionTimeText.text = BreathingLoopController.instance.GetReadableHourlyTime(totalRetentionTime);
        }

        public void SetMonthlyOverviewToZero()
        {
            averageRetentionTimeText.text = "0:00";
            longestRoundTimeText.text = "0:00";
            totalBreathingSessionsText.text = "0";
            totalRoundsText.text = "0";
            totalRetentionTimeText.text = "0h 00m";
        }
    }

}