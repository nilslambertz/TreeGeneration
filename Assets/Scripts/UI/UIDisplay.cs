using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {
    public Transform overlay;

    private Canvas bottomBar;
    
    private string numberOfObjectsLabel = "Number of objects: ";
    private int numberOfObjects = 0;
    private Text numberOfObjectsText;

    private void Start() {
        Transform bottomBarChild = overlay.Find("TopLeftCanvas");
        bottomBar = bottomBarChild.GetComponent<Canvas>();
        
        Transform numberOfObjectsChild = bottomBar.transform.Find("NumberOfObjects");
        numberOfObjectsText = numberOfObjectsChild.GetComponent<Text>();
        
        UIController.setUIDisplay(this);
    }

    void Update() {
        
    }

    public void addNumberOfObjects(int num) {
        numberOfObjects += num;
        numberOfObjectsText.text = numberOfObjectsLabel + numberOfObjects;
    }
}
