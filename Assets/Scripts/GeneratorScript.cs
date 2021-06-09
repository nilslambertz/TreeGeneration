using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public string = "Nils stinkt";

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
    
    // 0...-Werte
    private double zeroScale = 1;
    private double zeroScaleV = 0;
    private double zeroLength = 1;
    private double zeroLengthV = 0;
    private double zeroTaper = 1;
    private double zeroBaseSplits = 0;
    private double zeroSegSplits = 0;
    private double zeroSplitAngle = 0;
    private double zeroSplitAngleV = 0;
    private double zeroCurveRes = 3;
    private double zeroCurve = 0;
    private double zeroCurveBack = 0;
    private double zeroCurveV = 20;

    // 1...-Werte
    private double oneDownAngle = 60;
    private double oneDownAngleV = -50;
    private double oneRotate = 140;
    private double oneRotateV = 0;
    private double oneBranches = 50;
    private double oneLength = 0.3;
    private double oneLengthV = 0;
    private double oneTaper = 1;
    private double oneSegSplits = 0;
    private double oneSplitAngle = 0;
    private double oneSplitAngleV = 0;
    private double oneCurveRes = 5;
    private double oneCurve = -40;
    private double oneCurveBack = 0;
    private double oneCurveV = 50;
    
    // 2...-Werte
    private double twoDownAngle = 45;
    private double twoDownAngleV = 10;
    private double twoRotate = 140;
    private double twoRotateV = 0;
    private double twoBranches = 30;
    private double twoLength = 0.6;
    private double twoLengthV = 0;
    private double twoTaper = 1;
    private double twoSegSplits = 0;
    private double twoSplitAngle = 0;
    private double twoSplitAngleV = 0;
    private double twoCurveRes = 3;
    private double twoCurve = -40;
    private double twoCurveBack = 0;
    private double twoCurveV = 75;
    
    // 3...-Werte
    private double threeDownAngle = 45;
    private double threeDownAngleV = 10;
    private double threeRotate = 77;
    private double threeRotateV = 0;
    private double threeBranches = 10;
    private double threeLength = 0;
    private double threeLengthV = 0;
    private double threeTaper = 1;
    private double threeSegSplits = 0;
    private double threeSplitAngle = 0;
    private double threeSplitAngleV = 0;
    private double threeCurveRes = 1;
    private double threeCurve = 0;
    private double threeCurveBack = 0;
    private double threeCurveV = 0;

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
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
