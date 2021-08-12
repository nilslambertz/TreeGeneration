using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Transform overlay;

    private Canvas bottomBar;
    
    private string numberOfObjectsLabel = "Number of objects: ";
    private int numberOfObjects;
    private Text numberOfObjectsText;

    private void Start() {
        Transform bottomBarChild = overlay.Find("BottomBar");
        bottomBar = bottomBar.GetComponent<Canvas>();
        
        Transform numberOfObjectsChild = bottomBar.transform.Find("NumberOfObjects");
        numberOfObjectsText = numberOfObjectsChild.GetComponent<Text>();
    }

    void Update() {
        numberOfObjectsText.text = numberOfObjectsLabel + numberOfObjects;
    }

    public void setNumberOfObjects(int numberOfObjects) {
        this.numberOfObjects = numberOfObjects;
    }
}
