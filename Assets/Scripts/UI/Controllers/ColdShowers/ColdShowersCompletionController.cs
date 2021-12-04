using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class ColdShowersCompletionController : Controller
    {
        public Text ShowerTimeText;

        void OnEnable()
        {
            ShowerTimeText.text = UIController.instance.GetReadableTime(ColdShowersController.Singleton.Model.actualShowerDuration);
        }

        public void ResetButtonClick()
        {
            UIController.instance.Open(ViewName.Start);
        }

        public void SaveButtonClick()
        {
            SavedResultModel result = new SavedResultModel(SavedResultType.ColdShowers, 0, null, (int)ColdShowersController.Singleton.Model.actualShowerDuration, null);
            GameController.instance.SavedResultModelRegistry.addASpecificResult(SavedResultType.ColdShowers, result);
            string json = JsonUtility.ToJson(result);
            File.AppendAllText(GameController.instance.SavedResultModelRegistry.saveFilePath, json + Environment.NewLine);
            ResetButtonClick();
        }
    }

}