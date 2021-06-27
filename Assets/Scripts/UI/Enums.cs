using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public enum ViewName {
        Start,
        Settings,
        BreathingStart,
        BreathingRetention,
        BreathingRecovery,
        BreathingCompletion,
        ResultsCalendar,
        ResultsCalendarDay,
        ResultsCalendarSpecifics,
    }

    public enum PaceName {
        Normal,
        Fast,
        Slow,
    }

    public enum BreathingStep {
        Start,
        Retention,
        Recovery,
        Completion,
    }

}