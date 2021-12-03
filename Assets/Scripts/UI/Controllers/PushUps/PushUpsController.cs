using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class PushUpsController : Controller
    {
        public static PushUpsController Singleton;
        public PushUpsModel Model;

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