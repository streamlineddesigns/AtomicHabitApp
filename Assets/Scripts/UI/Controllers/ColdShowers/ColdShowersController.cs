using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class ColdShowersController : Controller
    {
        public static ColdShowersController Singleton;
        public ColdShowersModel Model;

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