using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PresetListScript : MonoBehaviour {
    public GameObject buttonTemplate;
    void Start() {
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
    }


    void buttonClicked(int id) {
        SharedValues.setPreset(id);
        updateList();
    }
}
