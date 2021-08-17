using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    private static UIDisplay uiDisplay;

    public static void setUIDisplay(UIDisplay ui) {
        uiDisplay = ui;
    }

    public static void setNumber(UIDisplay.uiTextsEnum e, int number) {
        if (uiDisplay != null) {
            uiDisplay.setNumber(e, number);
        }
    }
    public static void addNumber(UIDisplay.uiTextsEnum e, int number) {
        if (uiDisplay != null) {
            uiDisplay.addNumber(e, number);
        }
    }
}
