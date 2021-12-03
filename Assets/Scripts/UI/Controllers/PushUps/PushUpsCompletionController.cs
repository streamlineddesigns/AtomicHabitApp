using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class PushUpsCompletionController : Controller
    {
        public Text PushUpsCountText;

        void OnEnable()
        {
            PushUpsCountText.text = PushUpsController.Singleton.Model.completedPushUps.ToString();
        }

        public void ResetButtonClick()
        {
            UIController.instance.Open(ViewName.Start);
        }

        public void SaveButtonClick()
        {
            SavedResultModel result = new SavedResultModel(SavedResultType.PushUps, 0, null, PushUpsController.Singleton.Model.completedPushUps, null);
            GameController.instance.SavedResultModelRegistry.addASpecificResult(SavedResultType.PushUps, result);
            string json = JsonUtility.ToJson(result);
            File.AppendAllText(GameController.instance.SavedResultModelRegistry.saveFilePath, json + Environment.NewLine);
            ResetButtonClick();
        }
    }

}