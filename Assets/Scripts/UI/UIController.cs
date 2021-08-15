using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    private static UIDisplay uiDisplay;

    public static void setUIDisplay(UIDisplay ui) {
        uiDisplay = ui;
    }

    public static void setNumberOfObjects(int number) {
        if (uiDisplay != null) {
            uiDisplay.addNumberOfObjects(number);
        }
    }
}
