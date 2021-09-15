﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace DefaultNamespace
{
    public class PlayerInput : MonoBehaviour
    {
        public Transform position;
        public LayerMask groundMask;
        public LayerMask treeMask;

        private GameObject circle;
        private bool circleHidden = true;

        private List<GameObject> gameObjectList = new List<GameObject>();

        private bool animateGeneration;

        private GameObject treeInFocus;
        private Color treeInFocusColor;

        private void Start()
        {
            circle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            circle.transform.localScale = new Vector3(1, 0.1f, 1);
            circle.GetComponent<Renderer>().material.color = Color.red;
        }

        private void setColor(GameObject tree, Color color)
        {
            var objectRenderer = tree.GetComponent<Renderer>();
            objectRenderer.material.SetColor("_Color", color);
        }

        // Update is called once per frame
        void Update()
        {
            if (!PauseMenu.gamePaused)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    List<GameObject> gameObjectList = GeneratorScript.getTreeList();
                    foreach (GameObject g in gameObjectList)
                    {
                        Destroy(g);
                        treeInFocus = null;
                        treeInFocusColor = Color.black;
                    }
                    UIController.setNumber(UIDisplay.uiTextsEnum.NumberOfTrees, 0);
                    UIController.setNumber(UIDisplay.uiTextsEnum.NumberOfObjects, 0);
                    GeneratorScript.clearTreeList();
                }
                else if (Physics.Raycast(position.position, position.forward, out var hitTree, Mathf.Infinity, treeMask))
                {
                    circleHidden = true;
                    circle.SetActive(false);
                    GameObject newTreeInFocus = hitTree.collider.gameObject;
                    if (Input.GetKeyDown(KeyCode.Delete))
                    {
                        int childCount = 0;
                        Destroy(newTreeInFocus);
                        for (int i = 0; i < newTreeInFocus.transform.childCount; i++)
                        {
                            Transform child = newTreeInFocus.transform.GetChild(i);
                            childCount++;
                            childCount += child.childCount;
                        }
                        treeInFocus = null;
                        treeInFocusColor = Color.black;
                        UIController.subtractNumber(UIDisplay.uiTextsEnum.NumberOfTrees, 1);
                        UIController.subtractNumber(UIDisplay.uiTextsEnum.NumberOfObjects, childCount);
                    }
                    else
                    {
                        if (treeInFocus == null)
                        {
                            treeInFocus = newTreeInFocus;
                            treeInFocusColor = treeInFocus.GetComponent<Renderer>().material.GetColor("_Color");
                            setColor(treeInFocus, Color.red);
                        }
                        if (newTreeInFocus.GetInstanceID() != treeInFocus.GetInstanceID())
                        {
                            setColor(treeInFocus, treeInFocusColor);
                            treeInFocus = newTreeInFocus;
                            treeInFocusColor = treeInFocus.GetComponent<Renderer>().material.GetColor("_Color");
                            setColor(treeInFocus, Color.red);
                        }
                    }
                }
                else
                {
                    if (treeInFocus != null)
                    {
                        setColor(treeInFocus, treeInFocusColor);
                        treeInFocus = null;
                        treeInFocusColor = Color.black;
                    }
                    if (Physics.Raycast(position.position, position.forward, out var hitGround, Mathf.Infinity, groundMask))
                    {
                        if (circleHidden)
                        {
                            circle.SetActive(true);
                        }

                        circle.transform.position = hitGround.point;

                        if (Input.GetMouseButtonDown(0))
                        {
                            if (gameObjectList.Count != 0)
                            {
                                CancelInvoke("renderBranches");
                                renderAllBranches();
                            }
                            bool newValue = OptionListScript.getOption(OptionListScript.OptionElement.animationRendering).value;

                            if (newValue != animateGeneration)
                            {
                                animateGeneration = newValue;
                            }

                            gameObjectList = GeneratorScript.startWeber(hitGround.point);
                            if (animateGeneration)
                            {
                                var repeatRate = 5f / gameObjectList.Count;
                                InvokeRepeating("renderBranches", 0.5f, repeatRate);
                            }
                            else
                            {
                                renderAllBranches();
                            }
                        }
                    }
                    else
                    {
                        circleHidden = true;
                        circle.SetActive(false);
                    };
                }
            }
        }

        private void renderAllBranches()
        {
            foreach (GameObject g in gameObjectList)
            {
                g.SetActive(true);
            }
            gameObjectList.Clear();
        }

        private void renderBranches()
        {
            if (gameObjectList.Count == 0)
            {
                CancelInvoke("renderBranches");
                return;
            }

            gameObjectList[0].SetActive(true);
            gameObjectList.RemoveAt(0);
        }
    }
}