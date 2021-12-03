using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    [Serializable]
    public class SavedResultModel
    {
        public int date;
        public long dateTime;
        public SavedResultType savedResultType; 

        /*
         * Generic types we can save. Examples next to them
         */
        public float floatNumber;
        public List<float> floatNumberList = new List<float>();//EXAMPLE: Breathe hold times
        public int intNumber;//EXAMPLE: Push up count
        public List<int> intNumberList = new List<int>();

        public SavedResultModel(SavedResultType sRT, float fN = 0, List<float> fNL = null, int iN = 0, List<int> iNL = null)
        {
            savedResultType = sRT;
            if (savedResultType != null) {
                DateTimeOffset dt = DateTime.Now;
                date = int.Parse(dt.ToString("yyyyMMdd"));
                dateTime = dt.Ticks;
            }

            //Breathing Initialization
            if (fNL != null && sRT == SavedResultType.Breathing) {
                floatNumberList = fNL;
            }

            //Push Ups Innitialization
            if (iN != 0 && sRT == SavedResultType.PushUps) {
                intNumber = iN;
            }
        }
    }

}