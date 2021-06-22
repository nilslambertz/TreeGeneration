using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    /* Parameter */
    private int shape = 7; // Shape-ID
    private double baseSize = 0.4; // Länge des Stamms bis zum ersten Ast
    
    // Größen und Skalierungen des Baums
    private double scale = 13;
    private double scaleV = 3;
    private double zScale = 1;
    private double zScaleV = 0;

    private int levels = 3; // Rekursionstiefe
    private double ratio = 0.015; // Normalisierter Radius
    private double ratioPower = 1.2; // Radius-Proportionen am Anfang eines Asts

    // ??
    private int lobes = 5;
    private double lobeDepth = 0.07;

    private double flare = 0.6; // Exponentielle Expansion vom Stamm aus
    
    // Shared values
    private double[] downAngle = new double[]{0, 60, 45, 45};
    private double[] downAngleV = new double[]{0, -50, 10, 10};
    private double[] length = new double[] {1, 0.3, 0.6, 0};
    private double[] lengthV = new double[] {0, 0, 0, 0};
    private double[] taper = new double[] {1, 1, 1, 1};
    private double[] segSplits = new double[] {0, 0, 0, 0};
    private double[] splitAngle = new double[] {0, 0, 0, 0};
    private double[] splitAngleV = new double[] {0, 0, 0, 0};
    private double[] curveRes = new double[] {3, 5, 3, 1};
    private double[] curve = new double[] {0, -40, -40, 0};
    private double[] curveBack = new double[] {0, 0, 0, 0};
    private double[] curveV = new double[] {20, 50, 75, 0};
    private double[] rotate = new double[] {0, 140, 140, 77};
    private double[] rotateV = new double[] {0, 0, 0, 0};
    private double[] branches = new double[] {0, 50, 30, 10};
    
    // 0...-Werte
    private double zeroScale = 1;
    private double zeroScaleV = 0;
    private double zeroBaseSplits = 0;

    // Blätter
    private double leaves = 25;
    private double leafShape = 0;
    private double leafScale = 0.17;
    private double leafScaleX = 1;
    
    private double attractionUp = 0.5;
    
    // Pruning
    private double pruneRatio = 0;
    private double pruneWidth = 0.5;
    private double pruneWidthPeak = 0.5;
    private double prunePowerLow = 0.5;
    private double prunePowerHigh = 0.5;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<ConeGenerator>().getCone(5, 3, 10, Vector3.zero, Quaternion.identity);
      //  var second =this.GetComponent<ConeGenerator>().getCone();


     //   Instantiate(first, new Vector3(0,5,0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
