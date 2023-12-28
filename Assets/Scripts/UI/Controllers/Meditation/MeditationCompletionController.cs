using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace whm {

    public class MeditationCompletionController : Controller {

        public Text MeditationTimeText;

        void OnEnable()
        {
            MeditationTimeText.text = UIController.instance.GetReadableTime(MeditationController.Singleton.Model.actualMeditationDuration);
            MeditationAudioController.instance.StopAll();
            MeditationAudioController.instance.PlayPingSound();
            MeditationAudioController.instance.PlayGongSound();
        }

        public void ResetButtonClick()
        {
            UIController.instance.Open(ViewName.Start);
        }

        public void SaveButtonClick()
        {
            SavedResultModel result = new SavedResultModel(SavedResultType.Meditation, 0, null, (int)MeditationController.Singleton.Model.actualMeditationDuration, null);
            GameController.instance.SavedResultModelRegistry.addASpecificResult(SavedResultType.Meditation, result);
            string json = JsonUtility.ToJson(result);
            File.AppendAllText(GameController.instance.SavedResultModelRegistry.saveFilePath, json + Environment.NewLine);
            ResetButtonClick();
        }

    }

}