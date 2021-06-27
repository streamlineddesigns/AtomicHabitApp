using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class EventPublisher {
        public delegate void BreathingEvent(BreathingStep step);
        public delegate void InputEvent();
        public delegate void CalendarEvent(DateTime dateTime);

        public static event BreathingEvent OnStepChange;
        public static event InputEvent OnDoubleClick;
        public static event CalendarEvent OnDayChange;
        public static event CalendarEvent OnMonthChange;

        public static void PublishBreathingStepChange(BreathingStep step)
        {
            OnStepChange(step);
        }

        public static void PublishDoubleClick()
        {
            OnDoubleClick();
        }

        public static void PublishDayChange(DateTime dateTime)
        {
            OnDayChange(dateTime);
        }

        public static void PublishMonthChange(DateTime dateTime)
        {
            OnMonthChange(dateTime);
        }
    }

}