using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class MeditationSpecificsController : Controller
    {
        public Text DateTimeText;
        public Text TotalMeditationTimeText;

        public void OnEnable()
        {
            DateTime date = new DateTime(UIController.instance.selectedResult.dateTime);
            DateTimeText.text = date.ToString("MM/dd/yyyy HH:mm");

            TotalMeditationTimeText.text = UIController.instance.GetReadableTime(UIController.instance.selectedResult.intNumber);
        }
    }

}