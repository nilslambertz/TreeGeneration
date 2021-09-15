using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace DefaultNamespace
{
    public class PlayerInput : MonoBehaviour
    {
        public Transform position; // Current position
        public LayerMask groundMask; // Mask of the floor plane
        public LayerMask treeMask; // Mask of the tree

        private GameObject circle; // Red circle, where new trees will be generated from
        private bool circleHidden = true; // If circle is hidden

        private List<GameObject> gameObjectList = new List<GameObject>(); // List of branches which need to be rendered

        private bool animateGeneration; // If generation should be animated

        private GameObject treeInFocus; // current tree where the mouse is pointing at
        private Color treeInFocusColor; // color of the focussed tree

        private void Start()
        {
            // Create circle
            circle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            circle.transform.localScale = new Vector3(1, 0.1f, 1);
            circle.GetComponent<Renderer>().material.color = Color.red;
        }

        // Sets color of a tree-stem
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
                    // Render all remaing branches to avoid error after deleting
                    CancelInvoke("renderBranches");
                    renderAllBranches();

                    // Get tree-list and destroy all of them
                    List<GameObject> treeList = GeneratorScript.getTreeList();
                    foreach (GameObject g in treeList)
                    {
                        Destroy(g);
                        treeInFocus = null;
                        treeInFocusColor = Color.black;
                    }
                    UIController.setValue(UIDisplay.UIDebugTextEnum.NumberOfTrees, 0);
                    UIController.setValue(UIDisplay.UIDebugTextEnum.NumberOfObjects, 0);
                    GeneratorScript.clearTreeList();
                }
                // Looking at a tree
                else if (Physics.Raycast(position.position, position.forward, out var hitTree, Mathf.Infinity, treeMask))
                {
                    // Hide circle
                    circleHidden = true;
                    circle.SetActive(false);

                    GameObject newTreeInFocus = hitTree.collider.gameObject;
                    if (Input.GetKeyDown(KeyCode.Delete))
                    {
                        // Render all remaing branches to avoid error after deleting
                        CancelInvoke("renderBranches");
                        renderAllBranches();

                        // Count children and grandchildren, then delete the tree
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
                        UIController.subtractValue(UIDisplay.UIDebugTextEnum.NumberOfTrees, 1);
                        UIController.subtractValue(UIDisplay.UIDebugTextEnum.NumberOfObjects, childCount);
                    }
                    else
                    {
                        // Check if not already looking at a tree
                        if (treeInFocus == null)
                        {
                            // Change the color to red
                            treeInFocus = newTreeInFocus;
                            treeInFocusColor = treeInFocus.GetComponent<Renderer>().material.GetColor("_Color");
                            setColor(treeInFocus, Color.red);
                        }
                        // If looking at a different tree than befor
                        if (newTreeInFocus.GetInstanceID() != treeInFocus.GetInstanceID())
                        {
                            // Change the color of the old tree back to the original color, change current color to red
                            setColor(treeInFocus, treeInFocusColor);
                            treeInFocus = newTreeInFocus;
                            treeInFocusColor = treeInFocus.GetComponent<Renderer>().material.GetColor("_Color");
                            setColor(treeInFocus, Color.red);
                        }
                    }
                }
                else
                {
                    // Change the color of the old focussed tree back to the original color
                    if (treeInFocus != null)
                    {
                        setColor(treeInFocus, treeInFocusColor);
                        treeInFocus = null;
                        treeInFocusColor = Color.black;
                    }

                    // If looking at the ground
                    if (Physics.Raycast(position.position, position.forward, out var hitGround, Mathf.Infinity, groundMask))
                    {
                        // Set circle active, if not already done
                        if (circleHidden)
                        {
                            circle.SetActive(true);
                        }

                        // Update position of the circle
                        circle.transform.position = hitGround.point;

                        // Left mouse button clicked
                        if (Input.GetMouseButtonDown(0))
                        {
                            // Render all remaining branches from the old generation
                            if (gameObjectList.Count != 0)
                            {
                                CancelInvoke("renderBranches");
                                renderAllBranches();
                            }

                            // Update animationRendering-value 
                            animateGeneration = OptionListScript.getOption(OptionListScript.OptionElement.animationRendering).value;

                            // Generate tree and render animation, if option is true, otherwise the tree is rendered directly
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
                        // If not looking at the ground, hide circle
                        circleHidden = true;
                        circle.SetActive(false);
                    };
                }
            }
        }

        // Renders all branches instantly
        private void renderAllBranches()
        {
            foreach (GameObject g in gameObjectList)
            {
                g.SetActive(true);
            }
            gameObjectList.Clear();
        }

        // Renderes next branch and removes it from the list
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