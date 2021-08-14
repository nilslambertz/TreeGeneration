using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    public Transform position;
    public LayerMask mask;

    private GameObject circle;
    private bool circleHidden = true;

    private void Start() {
        circle =  GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        circle.transform.localScale = new Vector3(1, 0.1f, 1);
        circle.GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        if (Physics.Raycast(position.position, position.forward, out var hit, Mathf.Infinity, mask)) {
            if (circleHidden) {
                circle.SetActive(true);
            }
            
            circle.transform.position = hit.point;
        }
        else {
            circleHidden = true;
            circle.SetActive(false);
        };
        
        if (Input.GetMouseButtonDown(0)) {
            GeneratorScript.startWeber(hit.point);
        }
    }
}
