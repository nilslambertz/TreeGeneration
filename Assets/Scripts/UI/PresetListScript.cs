using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PresetListScript : MonoBehaviour
    {
        public GameObject buttonTemplate;

        public GameObject parameterListPanel;

        public GameObject parameterTemplate;

        public GameObject addButtonTemplate;

        private static PresetParameters.PresetParameterInfo[] presetParameterList;

        void Start()
        {
            presetParameterList = PresetParameters.GetPresetParameterInfo();
            updateList();
        }

        void updateList()
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }

            GameObject currentButton;
            List<TreePreset> presetList = PresetParameters.getPresetList();
            foreach (TreePreset preset in presetList)
            {
                currentButton = Instantiate(buttonTemplate, this.transform);
                string buttonText = preset.name;
                if (SharedValues.getCurrentPreset().id == preset.id)
                {
                    currentButton.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
                    buttonText = "> " + buttonText + " <";
                }
                currentButton.transform.GetChild(0).GetComponent<Text>().text = buttonText;
                currentButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    buttonClicked(preset.id);
                });
                currentButton.SetActive(true);
            }

            currentButton = Instantiate(addButtonTemplate, this.transform);
            currentButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    createNewPreset();
                    updateList();
                });

            updateParameterList();
        }

        void updateParameterList()
        {
            foreach (Transform child in parameterListPanel.transform)
            {
                Destroy(child.gameObject);
            }

            GameObject currentParameter;
            for (int i = 0; i < presetParameterList.Length; i++)
            {
                PresetParameters.PresetParameterInfo p = presetParameterList[i];

                currentParameter = Instantiate(parameterTemplate, parameterListPanel.transform);
                currentParameter.transform.Find("Title").GetComponent<Text>().text = p.name;
                currentParameter.transform.Find("Description").GetComponent<Text>().text = p.description;

                currentParameter.transform.Find("Dropdown").GetComponent<Dropdown>().gameObject.SetActive(false);
                currentParameter.transform.Find("Slider").GetComponent<Slider>().gameObject.SetActive(false);
                currentParameter.transform.Find("Slider").GetComponent<Slider>().gameObject.SetActive(true);
                Slider slider = currentParameter.transform.Find("Slider").GetComponent<Slider>();
                slider.minValue = p.minValue;
                slider.maxValue = p.maxValue;
                slider.value = p.initialValue;
                int x = i;
                slider.onValueChanged.AddListener(delegate (float newValue)
                {
                    PresetParameters.changePresetValue(SharedValues.getCurrentPreset().id, x, newValue);
                });
            }
        }

        void test(int i)
        {
            print(i);
        }


        void buttonClicked(int id)
        {
            SharedValues.setPreset(id);
            updateList();
        }

        void createNewPreset()
        {
            PresetParameters.createCustomTreePreset();
        }
    }
}