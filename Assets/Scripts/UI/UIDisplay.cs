using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIDisplay : MonoBehaviour
    {
        public enum uiTextsEnum
        {
            NumberOfObjects = 0,
            NumberOfTrees = 1
        };

        private string[] uiLabels = {
            "Number of objects:",
            "Number of trees:"
        };

        private int[] uiNumbers = {
            0,
            0
        };

        private bool changed = false;

        public Transform overlay;

        private Text uiText;

        private bool showDebug;

        private void Start()
        {
            Transform bottomBarChild = overlay.Find("TopLeftCanvas");
            Canvas debugCanvas = bottomBarChild.GetComponent<Canvas>();

            Transform x = debugCanvas.transform.Find("DebugText");
            uiText = x.GetComponent<Text>();

            UIController.setUIDisplay(this);
            updateUiText();

            OptionListScript.getOption(OptionListScript.OptionElement.showDebug);
        }

        void Update()
        {
            if (changed)
            {
                if (OptionListScript.getOption(OptionListScript.OptionElement.showDebug).value)
                {
                    updateUiText();
                }
                changed = false;
            }
            bool newValue = OptionListScript.getOption(OptionListScript.OptionElement.showDebug).value;
            if (newValue != showDebug)
            {
                showDebug = newValue;
                overlay.gameObject.SetActive(showDebug);
            }
        }

        private void updateUiText()
        {
            string s = "";
            for (int i = 0; i < uiLabels.Length; i++)
            {
                s += uiLabels[i] + " " + uiNumbers[i] + "\n";
            }

            uiText.text = s;
        }

        public void setNumber(uiTextsEnum e, int number)
        {
            uiNumbers[(int)e] = number;
            changed = true;
        }

        public void addNumber(uiTextsEnum e, int number)
        {
            uiNumbers[(int)e] += number;
            changed = true;
        }
    }
}