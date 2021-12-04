using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Paroxe.SuperCalendar;

namespace whm {

    public class ResultsCalendarController : Controller
    {
        public List<Controller> monthlyStatisticsControllers = new List<Controller>();
        protected List<IMonthlyStatisticsController> monthlyStatisticsInterfaces = new List<IMonthlyStatisticsController>();

        public void Awake()
        {
            for (int i = 0; i < monthlyStatisticsControllers.Count; i++) {
                monthlyStatisticsInterfaces.Add(monthlyStatisticsControllers[i] as IMonthlyStatisticsController);
            }
        }

        public void OnEnable()
        {
            EventPublisher.OnDayChange += OnDayChange;
            EventPublisher.OnMonthChange += OnMonthChange;

            UIController.instance.selectedMonth = DateTime.Now;
            SetStatisticsViews();
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
            foreach (SavedResultType e in Enum.GetValues(typeof(SavedResultType)))
            {
                if (GameController.instance.SavedResultModelRegistry.doesDayHaveData(e, dateTime)) {
                    UIController.instance.selectedDay = dateTime;
                    UIController.instance.OpenImmediately(ViewName.ResultsCalendarDay);
                    break;
                }
            }
        }

        /*
         * Called when a month on the calendar gets clicked
         */
        public void OnMonthChange(DateTime dateTime)
        {
            UIController.instance.selectedMonth = dateTime;
            SetStatisticsViews();
        }

        public void CloseButtonClick()
        {
            UIController.instance.Open(ViewName.Start);
        }

        public void SetStatisticsViews()
        {
            for (int i = 0; i < monthlyStatisticsInterfaces.Count; i++) {
                monthlyStatisticsInterfaces[i].SetMonthlyOverview();
            }
        }
    }

}