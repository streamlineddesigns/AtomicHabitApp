using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace whm {

    public interface IMonthlyStatisticsController
    {
        void SetMonthlyOverview();
        void SetMonthlyOverViewWithData();
        void SetMonthlyOverviewToZero();
    }

}