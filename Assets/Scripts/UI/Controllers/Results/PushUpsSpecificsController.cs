using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class PushUpsSpecificsController : Controller
    {
        public Text DateTimeText;
        public Text TotalPushUpsText;

        public void OnEnable()
        {
            DateTime date = new DateTime(UIController.instance.selectedResult.dateTime);
            DateTimeText.text = date.ToString("MM/dd/yyyy HH:mm");

            TotalPushUpsText.text = UIController.instance.selectedResult.intNumber.ToString();
        }
    }

}