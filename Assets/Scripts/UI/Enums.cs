using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public enum ViewName {
        Start,
        BreathingSettings,
        BreathingStart,
        BreathingRetention,
        BreathingRecovery,
        BreathingCompletion,
        ResultsCalendar,
        ResultsCalendarDay,
        ResultsCalendarSpecifics,
        PushUpsStart,
        PushUpsCompletion,
        BreathingMonthlyStatistics,
        PushUpsMonthlyStatistics,
        ColdShowersMonthlyStatistics,
        BreathingSpecifics,
        PushUpsSpecifics,
        ColdShowerSpecifics,
        ColdShowersStart,
        ColdShowersCompletion,
        MeditationStart,
        MeditationCompletion,
        MeditationMonthlyStatistics,
        MeditationSpecifics,
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

    public enum SavedResultType {
        Breathing,
        PushUps,
        ColdShowers,
        Meditation,
    }

}