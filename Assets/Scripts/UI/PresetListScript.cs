using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace {
    public class PresetListScript : MonoBehaviour {
        public GameObject buttonTemplate;

        public GameObject parameterListPanel;

        public GameObject parameterTemplate;

        private static PresetParameters.PresetParameterInfo[] presetParameterList;

        void Start() {
            presetParameterList = PresetParameters.GetPresetParameterInfo();
            updateList();
        }

        void updateList() {
            foreach (Transform child in this.transform) {
                Destroy(child.gameObject);
            }

            GameObject currentButton;
            List<TreePreset> presetList = PresetParameters.getPresetList();
            foreach (TreePreset preset in presetList) {
                currentButton = Instantiate(buttonTemplate, this.transform);
                string buttonText = preset.name;
                if (SharedValues.getCurrentPreset().id == preset.id) {
                    currentButton.transform.GetChild(0).GetComponent<Text>().fontStyle = FontStyle.Bold;
                    buttonText = "> " + buttonText + " <";
                }
                currentButton.transform.GetChild(0).GetComponent<Text>().text = buttonText;
                currentButton.GetComponent<Button>().onClick.AddListener(delegate () {
                    buttonClicked(preset.id);
                });
                currentButton.SetActive(true);
            }

            updateParameterList();
        }

        void updateParameterList() {
            GameObject currentParameter;
            for (int i = 0; i < presetParameterList.Length; i++) {
                PresetParameters.PresetParameterInfo p = presetParameterList[i];

                currentParameter = Instantiate(parameterTemplate, parameterListPanel.transform);
                parameterTemplate.transform.Find("Title").GetComponent<Text>().text = p.name;
                parameterTemplate.transform.Find("Description").GetComponent<Text>().text = p.description;

                parameterTemplate.transform.Find("Dropdown").GetComponent<Dropdown>().gameObject.SetActive(false);
                parameterTemplate.transform.Find("Slider").GetComponent<Slider>().gameObject.SetActive(false);

                /*if (p.dateType == System.TypeCode.Int32) {
                    parameterTemplate.transform.Find("Dropdown").GetComponent<Dropdown>().gameObject.SetActive(true);
                    Dropdown dropdown = parameterTemplate.transform.Find("Dropdown").GetComponent<Dropdown>();
                    for (int j = (int)p.minValue; j < (int)p.maxValue; j++) {
                        Dropdown.OptionData data = new Dropdown.OptionData();
                        data.text = j + "";
                        dropdown.AddOptions(new List<Dropdown.OptionData> { data });
                    }
                } else if (p.dateType == System.TypeCode.Double) {*/
                parameterTemplate.transform.Find("Slider").GetComponent<Slider>().gameObject.SetActive(true);
                Slider slider = parameterTemplate.transform.Find("Slider").GetComponent<Slider>();
                slider.minValue = p.minValue;
                slider.maxValue = p.maxValue;
                slider.value = p.initialValue;
                // }
            }
        }


        void buttonClicked(int id) {
            SharedValues.setPreset(id);
            updateList();
        }
    }
}