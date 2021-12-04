using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public ViewRegistry ViewRegistry;
        protected View currentView;
        protected View previousView;

        //calendar stuff
        public DateTime selectedDay;
        public DateTime selectedMonth;
        public SavedResultModel selectedResult;

        public void Awake()
        {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(instance);
            }
        }

        public void Start()
        {
            selectedMonth = DateTime.Now;
            currentView = ViewRegistry.getView(ViewName.Start);
        }

        public void Open(ViewName e)
        {
            View v = ViewRegistry.getView(e);

            if (currentView != null) {
                previousView = currentView;
            }  
            currentView = v;

            previousView.gameObject.SetActive(false);
            currentView.gameObject.SetActive(true);
        }

        public void OpenImmediately(ViewName e)
        {
            View v = ViewRegistry.getView(e);
            v.gameObject.SetActive(true);
        }

        public void CloseImmediately(ViewName e)
        {
            View v = ViewRegistry.getView(e);
            v.gameObject.SetActive(false);
        }

        public void Back()
        {
            ViewName e = previousView.ViewName;
            Open(e);
        }

        public string GetReadableTime(float originalTime)
        {
            float minutes = (int) originalTime / 60;
            float seconds = originalTime % 60.0f;
            string readable = minutes.ToString("") + ":" + seconds.ToString("00");
            return readable;
        }
    }
    
}