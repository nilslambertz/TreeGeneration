using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    private List<char> alphabet;
    private List<char> konstanten;
    private String startAxiom;
    private Dictionary<string, string> regeln;

    private int maxIterationen = 10;
    private int iteration;

    private float scale = 1;
    
    // Turtle-Werte
    private float turtleX;
    private float turtleY;
    private float turtleZ;
    private Vector3 angle;


    // Start is called before the first frame update
    void Start()
    {
        alphabet = new List<char>();
        konstanten = new List<char>();
        regeln = new Dictionary<string, string>();
    }

    private string applyRules(string current)
    {
        return null;
    }

    private void renderSentence(string sentence)
    {
    }
}
