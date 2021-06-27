using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public SavedResultModelRegistry SavedResultModelRegistry;

        public void Awake()
        {
            if (instance == null) {
                init();
            } else {
                Destroy(instance);
            }
        }

        public void init()
        {
            instance = this;
            Application.targetFrameRate = 30;
            SavedResultModelRegistry = new SavedResultModelRegistry();
        }
    }

}