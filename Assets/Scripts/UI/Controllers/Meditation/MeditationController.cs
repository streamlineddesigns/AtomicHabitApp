using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class MeditationController : Controller
    {
        public static MeditationController Singleton;
        public MeditationModel Model;

        public void Awake()
        {
            if (Singleton == null) {
                Singleton = this;
            } else {
                Destroy(Singleton);
            }
        }
    }

}