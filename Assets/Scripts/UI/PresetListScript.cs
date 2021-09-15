using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PresetListScript : MonoBehaviour
    {
        public GameObject buttonTemplate; // Button prefab

        public GameObject parameterListPanel;

        public GameObject parameterTemplate;

        public GameObject addButtonTemplate;

        private static PresetParameters.PresetParameterInfo[] presetParameterList; // list of parameters

        void Start()
        {
            presetParameterList = PresetParameters.GetPresetParameterInfo();
            updateList();
        }

        // Updates preset-list in menu
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

                // If current preset, highlight it
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
                if (preset.basedOn != null)
                {
                    currentButton.transform.GetChild(1).GetComponent<Text>().text = "based on " + preset.basedOn;
                }
                currentButton.SetActive(true);
            }

            // Create add-preset-button
            currentButton = Instantiate(addButtonTemplate, this.transform);
            currentButton.GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    createNewPreset();
                });

            updateParameterList();
        }

        // updates parameter-list
        void updateParameterList()
        {
            foreach (Transform child in parameterListPanel.transform)
            {
                Destroy(child.gameObject);
            }

            float[] values = PresetParameters.getCurrentPresetParameterValues();

            GameObject currentParameter;
            for (int i = 0; i < presetParameterList.Length; i++)
            {
                PresetParameters.PresetParameterInfo p = presetParameterList[i];

                currentParameter = Instantiate(parameterTemplate, parameterListPanel.transform);
                currentParameter.transform.Find("Title").GetComponent<Text>().text = p.name;
                currentParameter.transform.Find("Description").GetComponent<Text>().text = p.description;

                // Set slider values
                currentParameter.transform.Find("Slider").GetComponent<Slider>().gameObject.SetActive(false);
                currentParameter.transform.Find("Slider").GetComponent<Slider>().gameObject.SetActive(true);
                Slider slider = currentParameter.transform.Find("Slider").GetComponent<Slider>();
                slider.minValue = p.minValue;
                slider.maxValue = p.maxValue;
                slider.value = values[i];
                int x = i;
                slider.onValueChanged.AddListener(delegate (float newValue)
                {
                    PresetParameters.changePresetValue(SharedValues.getCurrentPreset().id, x, newValue);
                });
            }
        }

        // Change current preset and update List
        void buttonClicked(int id)
        {
            SharedValues.setPreset(id);
            updateList();
        }

        // Creates new preset
        void createNewPreset()
        {
            PresetParameters.createCustomTreePreset();
            updateList();
        }
    }
}