using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using whm;

namespace Paroxe.SuperCalendar.Internal
{
    public class CalendarCell : UIBehaviour
    {
        public Text m_DayNumber;

        public Sprite m_OtherSprite;
        public Sprite m_CurrentSprite;
        public Sprite m_SelectedSprite;

        public Color m_OtherColor;
        public Color m_CurrentColor;
        public Color m_SelectedColor;

        //public GameObject hasDateIndicator;
        public List<ResultIndicatorView> resultIndicatorViews = new List<ResultIndicatorView>();
        protected Dictionary<SavedResultType, ResultIndicatorView> resultIndicatorRegistry = new Dictionary<SavedResultType, ResultIndicatorView>();

        public ICalendarCellPicker m_CalendarCellPicker;

        private State m_State;

        public void Init() {
            for(int i = 0; i < resultIndicatorViews.Count; i++) {
                resultIndicatorRegistry[resultIndicatorViews[i].savedResultType] = resultIndicatorViews[i];
            }
        }

        public enum State
        {
            Current,
            Other,
            Selected,
        }

        public void Select()
        {
            if (m_CalendarCellPicker == null)
                m_CalendarCellPicker = (ICalendarCellPicker) GetComponentInParent(typeof (ICalendarCellPicker));
            m_CalendarCellPicker.OnCellSelected(this);
        }

        public void SetResultsIndicator(bool hasResults, SavedResultType srt)
        {
            if (hasResults) {
                if (resultIndicatorRegistry.ContainsKey(srt) && resultIndicatorRegistry[srt] != null) resultIndicatorRegistry[srt].gameObject.SetActive(true);
            } else {
                if (resultIndicatorRegistry.ContainsKey(srt) && resultIndicatorRegistry[srt] != null) resultIndicatorRegistry[srt].gameObject.SetActive(false);
            }
        }

        public void SetState(State state)
        {
            m_State = state;

            switch (m_State)
            {
                case State.Current:
                    GetComponent<Image>().sprite = m_CurrentSprite;
                    GetComponent<Image>().color = m_CurrentColor;
                    for(int i = 0; i < resultIndicatorRegistry.Count; i++) {
                        if (resultIndicatorRegistry[(SavedResultType)i] != null && resultIndicatorRegistry[(SavedResultType)i].gameObject.activeSelf) {
                            resultIndicatorRegistry[(SavedResultType)i].GetComponent<Image>().color = m_SelectedColor;
                        }
                    }
                    m_DayNumber.color = Color.black;
                    break;
                case State.Other:
                    GetComponent<Image>().sprite = m_OtherSprite;
                    GetComponent<Image>().color = m_OtherColor;
                    m_DayNumber.color = new Color(100.0f/255.0f, 100.0f/255.0f, 100.0f/255.0f, 1.0f);
                    for(int i = 0; i < resultIndicatorRegistry.Count; i++) {
                        if (resultIndicatorRegistry[(SavedResultType)i] != null) {
                            resultIndicatorRegistry[(SavedResultType)i].gameObject.SetActive(false);
                        }
                    }
                    break;
                case State.Selected:
                    GetComponent<Image>().sprite = m_SelectedSprite;
                    GetComponent<Image>().color = m_SelectedColor;
                    for(int i = 0; i < resultIndicatorRegistry.Count; i++) {
                        if (resultIndicatorRegistry[(SavedResultType)i] != null && resultIndicatorRegistry[(SavedResultType)i].gameObject.activeSelf) {
                            resultIndicatorRegistry[(SavedResultType)i].GetComponent<Image>().color = m_CurrentColor;
                        }
                    }
                    m_DayNumber.color = Color.white;
                    break;
            }
        }
    }
}