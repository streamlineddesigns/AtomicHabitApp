using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class SavedResultView : MonoBehaviour
    {
        public Button button;
        public Text SessionCount;
        public SavedResultModel Model;

        public void Start()
        {
            //button = gameObject.GetComponent<Button>();
        }

        public void OnEnable()
        {
            button.onClick.AddListener(OnClick);
        }

        public void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        public void OnClick()
        {
            UIController.instance.selectedResult = Model;
            UIController.instance.OpenImmediately(ViewName.ResultsCalendarSpecifics);
        }
    }

}