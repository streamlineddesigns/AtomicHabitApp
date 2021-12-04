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

        public void StartBreathingButtonClick()
        {
            UIController.instance.Open(ViewName.Settings);
        }

        public void StartPushUpsButtonClick()
        {
            UIController.instance.Open(ViewName.PushUpsStart);
        }

        public void ColdShowersButtonClick()
        {
            UIController.instance.Open(ViewName.ColdShowersStart);
        }
    }

}