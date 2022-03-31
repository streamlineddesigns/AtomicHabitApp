using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class StartController : Controller
    {
        public void ResultsButtonClick()
        {
            UIController.instance.Open(ViewName.ResultsCalendar);
        }

        public void BreathingButtonClick()
        {
            UIController.instance.Open(ViewName.BreathingSettings);
        }

        public void PushUpsButtonClick()
        {
            UIController.instance.Open(ViewName.PushUpsStart);
        }

        public void ColdShowersButtonClick()
        {
            UIController.instance.Open(ViewName.ColdShowersStart);
        }

        public void MeditationButtonClick()
        {
            UIController.instance.Open(ViewName.MeditationSettings);
        }
    }

}