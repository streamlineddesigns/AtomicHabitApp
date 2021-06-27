using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public class InputController : MonoBehaviour
    {
        public float clicked = 0;
        public float clicktime = 0;
        public float clickdelay = 1.0f;
    
        public void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                OnMouseDown();
            }

            if (clicked > 2 || Time.time - clicktime > 1) {
                clicked = 0;
                clicktime = 0;
            }
        }

        public void OnMouseDown()
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
    
            if (clicked > 1 && Mathf.Abs(Time.time - clicktime) < clickdelay)
            {
                clicked = 0;
                clicktime = 0;
                EventPublisher.PublishDoubleClick();
    
            }
        }

    }

}