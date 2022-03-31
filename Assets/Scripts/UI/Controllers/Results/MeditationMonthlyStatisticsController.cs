using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class MeditationMonthlyStatisticsController : Controller, IMonthlyStatisticsController
    {
        public Text AverageDurationText;
        public Text LongestDurationText;
        public Text totalRoundsText;
        public Text totalTimeText;
        public Text currentMonthText;

        public void SetMonthlyOverview()
        {
            currentMonthText.text = UIController.instance.selectedMonth.ToString("MMMM") + " Summary";

            if (GameController.instance.SavedResultModelRegistry.doesMonthHaveData(SavedResultType.Meditation, UIController.instance.selectedMonth)) {

                SetMonthlyOverViewWithData();

            } else {
                SetMonthlyOverviewToZero();
            }
        }

        public void SetMonthlyOverViewWithData()
        {
            List<SavedResultModel> monthlyResults = GameController.instance.SavedResultModelRegistry.getAnEntireMonthsResults(SavedResultType.Meditation, UIController.instance.selectedMonth); 
            int totalRounds = monthlyResults.Count;
            float longestDuration = 0;
            float totalTime = 0;

            for (int i = 0; i < monthlyResults.Count; i++) {

                totalTime += monthlyResults[i].intNumber;
                    
                if (monthlyResults[i].intNumber > longestDuration) {
                    longestDuration = monthlyResults[i].intNumber;
                }

            }

            AverageDurationText.text = UIController.instance.GetReadableTime(totalTime / totalRounds);
            LongestDurationText.text = UIController.instance.GetReadableTime(longestDuration);
            totalRoundsText.text = totalRounds.ToString();
            totalTimeText.text = UIController.instance.GetReadableHourlyTime(totalTime);
        }

        public void SetMonthlyOverviewToZero() 
        {
            AverageDurationText.text = "0:00";
            LongestDurationText.text = "0:00";
            totalRoundsText.text = "0";
            totalTimeText.text = "0h 00m";
        }
    }

}