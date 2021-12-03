using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {
    public class ResultsCalendarDayController : Controller
    {
        public GameObject SavedResultViewPrefab;
        public GameObject SavedResultViewContainer;
        public List<GameObject> SavedResultViews;
        public Text dateText;

        public void OnEnable()
        {
            if (SavedResultViews != null) {
                for(int m = 0; m < SavedResultViews.Count; m++) {
                    Destroy(SavedResultViews[m]);
                }
            }

            SavedResultViews = new List<GameObject>();

            List<SavedResultModel> SavedResultModels = GameController.instance.SavedResultModelRegistry.getAnEntireDaysResults(SavedResultType.Breathing, UIController.instance.selectedDay);

            for (int i = 0; i < SavedResultModels.Count; i++) {
                GameObject roundView = Instantiate(SavedResultViewPrefab, SavedResultViewContainer.transform);
                SavedResultView SavedResultView = roundView.GetComponent<SavedResultView>();
                SavedResultView.SessionCount.text = (i + 1).ToString();
                SavedResultView.Model = SavedResultModels[i];
                SavedResultViews.Add(roundView);
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