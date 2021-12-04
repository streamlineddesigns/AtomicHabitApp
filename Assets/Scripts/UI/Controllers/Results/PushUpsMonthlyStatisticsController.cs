using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class PushUpsMonthlyStatisticsController : Controller, IMonthlyStatisticsController
    {
        public Text AveragePushUpsText;
        public Text MostPushUpsText;
        public Text totalRoundsText;
        public Text totalPushUpsText;
        public Text currentMonthText;

        public void SetMonthlyOverview()
        {
            currentMonthText.text = UIController.instance.selectedMonth.ToString("MMMM") + " Summary";

            if (GameController.instance.SavedResultModelRegistry.doesMonthHaveData(SavedResultType.PushUps, UIController.instance.selectedMonth)) {

                SetMonthlyOverViewWithData();

            } else {
                SetMonthlyOverviewToZero();
            }
        }

        public void SetMonthlyOverViewWithData()
        {
            List<SavedResultModel> monthlyResults = GameController.instance.SavedResultModelRegistry.getAnEntireMonthsResults(SavedResultType.PushUps, UIController.instance.selectedMonth); 
            int totalRounds = monthlyResults.Count;
            float mostPushUps = 0;
            float totalPushUps = 0;

            for (int i = 0; i < monthlyResults.Count; i++) {

                totalPushUps += monthlyResults[i].intNumber;
                    
                if (monthlyResults[i].intNumber > mostPushUps) {
                    mostPushUps = monthlyResults[i].intNumber;
                }

            }

            AveragePushUpsText.text = (totalPushUps / totalRounds).ToString("0.00");
            MostPushUpsText.text = mostPushUps.ToString();
            totalRoundsText.text = totalRounds.ToString();
            totalPushUpsText.text = totalPushUps.ToString();
        }

        public void SetMonthlyOverviewToZero() 
        {
            AveragePushUpsText.text = "0";
            MostPushUpsText.text = "0";
            totalRoundsText.text = "0";
            totalPushUpsText.text = "0";
        }
    }

}