using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class ColdShowersSpecificsController : Controller
    {
        public Text DateTimeText;
        public Text ColdShowerDurationText;

        public void OnEnable()
        {
            DateTime date = new DateTime(UIController.instance.selectedResult.dateTime);
            DateTimeText.text = date.ToString("MM/dd/yyyy HH:mm");

            ColdShowerDurationText.text = UIController.instance.GetReadableTime(UIController.instance.selectedResult.intNumber);
        }
    }

}