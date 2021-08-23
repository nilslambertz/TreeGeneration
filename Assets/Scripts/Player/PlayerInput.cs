using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Transform position;
    public LayerMask mask;

    private GameObject circle;
    private bool circleHidden = true;

    private List<GameObject> gameObjectList = new List<GameObject>();

    private void Start() {
        circle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        circle.transform.localScale = new Vector3(1, 0.1f, 1);
        circle.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        if (!PauseMenu.gamePaused) {
            if (Physics.Raycast(position.position, position.forward, out var hit, Mathf.Infinity, mask)) {
                if (circleHidden) {
                    circle.SetActive(true);
                }

                circle.transform.position = hit.point;

                if (Input.GetMouseButtonDown(0)) {
                    gameObjectList.AddRange(GeneratorScript.startWeber(hit.point));

                    var repeatRate = 5f / gameObjectList.Count;
                    InvokeRepeating("renderBranches", 1f, repeatRate);
                }
            } else {
                circleHidden = true;
                circle.SetActive(false);
            };
        }
    }

    private void renderBranches() {
        if (gameObjectList.Count == 0) {
            CancelInvoke("renderBranches");
            return;
        }

        gameObjectList[0].SetActive(true);
        gameObjectList.RemoveAt(0);
    }
}
