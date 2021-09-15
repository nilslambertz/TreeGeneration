using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class UIDisplay : MonoBehaviour
    {
        public enum UIDebugTextEnum
        {
            NumberOfObjects,
            NumberOfTrees,
            CurrentPreset
        };
        public enum UIDebugTextValue
        {
            StrValue,
            IntValue,
            BoolValue
        };

        private UIDebugText[] uIDebugTexts;

        public struct UIDebugText
        {
            public UIDebugText(string n, UIDebugTextValue v, object initialValue)
            {
                name = n;
                valueType = v;

                strValue = null;
                intValue = -1;
                boolValue = false;

                if (valueType == UIDebugTextValue.StrValue)
                {
                    strValue = (string)initialValue;
                }
                else if (valueType == UIDebugTextValue.IntValue)
                {
                    intValue = (int)initialValue;
                }
                else if (valueType == UIDebugTextValue.BoolValue)
                {
                    boolValue = (bool)initialValue;
                }
            }

            public string getValue()
            {
                if (valueType == UIDebugTextValue.StrValue) return strValue;
                if (valueType == UIDebugTextValue.IntValue) return intValue.ToString();
                if (valueType == UIDebugTextValue.BoolValue) return boolValue.ToString();
                return "ERROR";
            }

            public void setValue(object x)
            {
                if (valueType == UIDebugTextValue.StrValue)
                {
                    strValue = (string)x;
                }
                else if (valueType == UIDebugTextValue.IntValue)
                {
                    intValue = (int)x;
                }
                else if (valueType == UIDebugTextValue.BoolValue)
                {
                    boolValue = (bool)x;
                }
            }

            public void addValue(object x)
            {
                if (valueType == UIDebugTextValue.IntValue)
                {
                    intValue += (int)x;
                }
            }

            public void subtractValue(object x)
            {
                if (valueType == UIDebugTextValue.IntValue)
                {
                    intValue -= (int)x;
                }
            }

            public UIDebugTextValue valueType;

            public string strValue;
            public int intValue;
            public bool boolValue;

            public string name;
        }

        private void generateUIDebugTexts()
        {
            uIDebugTexts = new UIDebugText[3];
            uIDebugTexts[(int)UIDebugTextEnum.NumberOfTrees] = new UIDebugText("Number of trees", UIDebugTextValue.IntValue, 0);
            uIDebugTexts[(int)UIDebugTextEnum.NumberOfObjects] = new UIDebugText("Number of objects", UIDebugTextValue.IntValue, 0);
            uIDebugTexts[(int)UIDebugTextEnum.CurrentPreset] = new UIDebugText("Current preset", UIDebugTextValue.StrValue, SharedValues.getCurrentPreset().name);
        }
        private bool changed = false;

        public Transform overlay;

        private Text uiText;

        private bool showDebug;

        private void Start()
        {
            generateUIDebugTexts();

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
            /*for (int i = 0; i < uiLabels.Length; i++)
            {
                s += uiLabels[i] + " " + uiNumbers[i] + "\n";
            }*/
            for (int i = 0; i < uIDebugTexts.Length; i++)
            {
                s += uIDebugTexts[i].name + ": " + uIDebugTexts[i].getValue() + "\n";
            }

            uiText.text = s;
        }

        public void setValue(UIDebugTextEnum e, object newValue)
        {
            uIDebugTexts[(int)e].setValue(newValue);
            changed = true;
        }

        public void addValue(UIDebugTextEnum e, object add)
        {
            uIDebugTexts[(int)e].addValue(add);
            changed = true;
        }

        public void subtractValue(UIDebugTextEnum e, object subtract)
        {
            uIDebugTexts[(int)e].subtractValue(subtract);
            changed = true;
        }
    }
}