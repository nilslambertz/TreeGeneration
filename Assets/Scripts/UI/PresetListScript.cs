using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PresetListScript : MonoBehaviour {
    private GameObject buttonTemplate;
    void Start() {
        buttonTemplate = this.transform.GetChild(0).gameObject;

        GameObject currentButton;
        List<TreePreset> presetList = PresetParameters.getPresetList();
        foreach (TreePreset preset in presetList) {
            currentButton = Instantiate(buttonTemplate, this.transform);
            currentButton.transform.GetChild(0).GetComponent<Text>().text = preset.name;
            if (SharedValues.getCurrentPreset().id == preset.id) {
                currentButton.GetComponent<Button>().interactable = false;
            }
        }
        Destroy(buttonTemplate);
    }
}
