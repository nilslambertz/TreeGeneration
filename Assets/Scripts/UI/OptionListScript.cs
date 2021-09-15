using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class OptionListScript : MonoBehaviour
    {
        public GameObject optionsElementTemplate;

        public GameObject optionListPanel;

        private static OptionElementInfo[] optionList;

        // Options-Enum
        public enum OptionElement
        {
            showDebug,
            curvedBranches,
            animationRendering,
            randomColors
        }

        // Options-Element with name, description and value
        public struct OptionElementInfo
        {
            public OptionElementInfo(string n, string d, bool v)
            {
                name = n;
                description = d;
                value = v;
            }
            public string name;
            public string description;
            public bool value;
            public void changeValue(bool newValue)
            {
                value = newValue;
            }
        }

        // Generates option-list
        private static void generateOptionList()
        {
            optionList = new OptionElementInfo[4];
            optionList[(int)OptionElement.showDebug] = new OptionElementInfo("Debug output", "Show debug information in the top left corner", true);
            optionList[(int)OptionElement.curvedBranches] = new OptionElementInfo("Curved branches", "Render curved branches (decreases performance dramatically!)", true);
            optionList[(int)OptionElement.animationRendering] = new OptionElementInfo("Animate generation", "Animate the generation of trees", true);
            optionList[(int)OptionElement.randomColors] = new OptionElementInfo("Random colors", "Spawn trees with random colors for each branch", false);
        }

        // Returns option from enum
        public static OptionElementInfo getOption(OptionElement o)
        {
            if (optionList == null)
            {
                generateOptionList();
            }
            return optionList[(int)o];
        }

        void Start()
        {
            generateOptionList();
            updateOptionsList();
        }

        // Updates the option-list in the menu with the correct values
        void updateOptionsList()
        {
            foreach (Transform child in optionListPanel.transform)
            {
                Destroy(child.gameObject);
            }

            GameObject currentOption;
            for (int i = 0; i < optionList.Length; i++)
            {
                OptionElementInfo o = optionList[i];

                currentOption = Instantiate(optionsElementTemplate, optionListPanel.transform);
                currentOption.transform.Find("Title").GetComponent<Text>().text = o.name;
                currentOption.transform.Find("Description").GetComponent<Text>().text = o.description;

                Toggle toggle = currentOption.transform.Find("Toggle").GetComponent<Toggle>();
                toggle.isOn = o.value;

                int x = i;
                toggle.onValueChanged.AddListener(delegate (bool newValue)
                {
                    optionList[x].changeValue(newValue);
                });
            }
        }
    }
}