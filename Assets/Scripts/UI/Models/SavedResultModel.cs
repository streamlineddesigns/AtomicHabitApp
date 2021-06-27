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
        public List<float> roundTimes = new List<float>();

        public SavedResultModel(List<float> rt = null)
        {
            if (rt != null) {
                DateTimeOffset dt = DateTime.Now;
                roundTimes = rt;
                date = int.Parse(dt.ToString("yyyyMMdd"));
                dateTime = dt.Ticks;
            }
        }
    }

}