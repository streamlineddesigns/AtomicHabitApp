using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {
    public class ResultsCalendarDayController : Controller
    {
        [Tooltip("Make sure these prefab's order matches their corresponding Enum's order")]
        public List<GameObject> SavedResultViewPrefabs = new List<GameObject>();
        public Dictionary<SavedResultType, GameObject> SavedResultViewPrefabRegistry = new Dictionary<SavedResultType, GameObject>();
        public GameObject SavedResultViewContainer;
        public List<GameObject> SavedResultViews;
        public Text dateText;

        void Awake()
        {
            for (int i = 0; i < SavedResultViewPrefabs.Count; i++) 
            {   
                SavedResultViewPrefabRegistry[(SavedResultType)i] = SavedResultViewPrefabs[i];
            }
        }

        public void OnEnable()
        {
            if (SavedResultViews != null) {
                for(int m = 0; m < SavedResultViews.Count; m++) {
                    Destroy(SavedResultViews[m]);
                }
            }

            SavedResultViews = new List<GameObject>();

            foreach (SavedResultType e in Enum.GetValues(typeof(SavedResultType))) {

                List<SavedResultModel> SavedResultModels = GameController.instance.SavedResultModelRegistry.getAnEntireDaysResults(e, UIController.instance.selectedDay);

                if (SavedResultModels != null) {
                    for (int i = 0; i < SavedResultModels.Count; i++) {
                        GameObject roundView = Instantiate(SavedResultViewPrefabRegistry[e], SavedResultViewContainer.transform);
                        SavedResultView SavedResultView = roundView.GetComponent<SavedResultView>();
                        SavedResultView.SessionCount.text = (i + 1).ToString();
                        SavedResultView.Model = SavedResultModels[i];
                        SavedResultViews.Add(roundView);
                    }
                }
                
            }

            dateText.text = UIController.instance.selectedDay.ToString("MMMM dd, yyyy");
        }

        public void OnDisable()
        {
            
        }

        public void CloseButtonClick()
        {
            UIController.instance.CloseImmediately(ViewName.ResultsCalendarDay);
        }
    }
}