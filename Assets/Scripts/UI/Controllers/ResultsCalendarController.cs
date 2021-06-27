using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Paroxe.SuperCalendar;

namespace whm {

    public class ResultsCalendarController : Controller
    {
        public Text averageRetentionTimeText;
        public Text longestRoundTimeText;
        public Text totalBreathingSessionsText;
        public Text totalRoundsText;
        public Text totalRetentionTimeText;
        public Text currentMonthText;

        public void OnEnable()
        {
            EventPublisher.OnDayChange += OnDayChange;
            EventPublisher.OnMonthChange += OnMonthChange;

            UIController.instance.selectedMonth = DateTime.Now;
            SetMonthlyOverview();
        }

        public void OnDisable()
        {
            EventPublisher.OnDayChange -= OnDayChange;
            EventPublisher.OnMonthChange -= OnMonthChange;
        }

        /*
         * Called when a day on the calendar gets clicked
         */
        public void OnDayChange(DateTime dateTime)
        {
            if (GameController.instance.SavedResultModelRegistry.doesDayHaveData(dateTime)) {
                UIController.instance.selectedDay = dateTime;
                UIController.instance.OpenImmediately(ViewName.ResultsCalendarDay);
            }
        }

        /*
         * Called when a month on the calendar gets clicked
         */
        public void OnMonthChange(DateTime dateTime)
        {
            UIController.instance.selectedMonth = dateTime;
            SetMonthlyOverview();
        }

        public void CloseButtonClick()
        {
            UIController.instance.Open(ViewName.Start);
        }

        public void SetMonthlyOverview()
        {
            currentMonthText.text = UIController.instance.selectedMonth.ToString("MMMM") + " Summary";

            if (GameController.instance.SavedResultModelRegistry.doesMonthHaveData(UIController.instance.selectedMonth)) {

                SetMonthlyOverViewWithData();

            } else {
                SetMonthlyOverviewToZero();
            }
        }

        public void SetMonthlyOverViewWithData()
        {
            List<SavedResultModel> monthlyResults = GameController.instance.SavedResultModelRegistry.getAnEntireMonthsResults(UIController.instance.selectedMonth); 
            int totalBreathingSessions = monthlyResults.Count;
            int totalRounds = 0;
            float longestRound = 0;
            float totalRetentionTime = 0;

            for (int i = 0; i < monthlyResults.Count; i++) {
                totalRounds += monthlyResults[i].roundTimes.Count;

                for (int j = 0; j < monthlyResults[i].roundTimes.Count; j++) {

                    totalRetentionTime += monthlyResults[i].roundTimes[j];
                    
                    if (monthlyResults[i].roundTimes[j] > longestRound) {
                        longestRound = monthlyResults[i].roundTimes[j];
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