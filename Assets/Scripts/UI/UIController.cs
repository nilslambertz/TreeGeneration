using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIController : MonoBehaviour
    {
        private static UIDisplay uiDisplay;

        public static void setUIDisplay(UIDisplay ui)
        {
            uiDisplay = ui;
        }

        public static void setValue(UIDisplay.UIDebugTextEnum e, object newValue)
        {
            if (uiDisplay != null)
            {
                uiDisplay.setValue(e, newValue);
            }
        }
        public static void addValue(UIDisplay.UIDebugTextEnum e, object add)
        {
            if (uiDisplay != null)
            {
                uiDisplay.addValue(e, add);
            }
        }
        public static void subtractValue(UIDisplay.UIDebugTextEnum e, object subtract)
        {
            if (uiDisplay != null)
            {
                uiDisplay.subtractValue(e, subtract);
            }
        }
    }
}