using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm{

    public class ResultsCalendarSpecificsController : Controller
    {
        public List<ViewName> SpecificsViews = new List<ViewName>();
        protected Dictionary<SavedResultType, ViewName> SpecificsRegistry = new Dictionary<SavedResultType, ViewName>();

        public void Awake()
        {
            for(int i = 0; i < SpecificsViews.Count; i++) {
                SpecificsRegistry[(SavedResultType)i] = SpecificsViews[i];
            }
        }

        public void OnEnable()
        {
            for(int i = 0; i < SpecificsViews.Count; i++) {
                UIController.instance.CloseImmediately(SpecificsViews[i]);
            }

            SavedResultType srt = UIController.instance.selectedResult.savedResultType;
            UIController.instance.OpenImmediately(SpecificsRegistry[srt]);
        }

        public void OnDisable()
        {

        }

        public void CloseButtonClick()
        {
            UIController.instance.CloseImmediately(ViewName.ResultsCalendarSpecifics);
        }
    }

}