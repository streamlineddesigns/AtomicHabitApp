using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class ControllerRegistry : MonoBehaviour
    {
        protected Dictionary<ViewName, Controller> Registry = new Dictionary<ViewName, Controller>();

        public void Start() 
        {
            foreach(ViewName e in Enum.GetValues(typeof(ViewName))) 
            {
                View v = UIController.instance.ViewRegistry.getView(e);
                Controller c = v.GetComponent<Controller>();
                addController(e, c);
            }

        }

        public Controller getController(ViewName e)
        {
            return Registry[e];
        }

        public void addController(ViewName e, Controller c)
        {
            Registry.Add(e, c);
        }

    }

}