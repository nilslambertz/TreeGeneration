using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    /* Parameter */
    private int shape; // Shape-ID
    private float baseSize; // Länge des Stamms bis zum ersten Ast
    
    // Größen und Skalierungen des Baums
    private float scale;
    private float scaleV;
    private float zScale;
    private float zScaleV;

    private int levels; // Rekursionstiefe
    private float ratio; // Normalisierter Radius
    private float ratioPower; // Radius-Proportionen am Anfang eines Asts

    // ??
    private int lobes;
    private float lobeDepth;

    private float flare; // Exponentielle Expansion vom Stamm aus
    
    // Shared values
    private float[] downAngle;
    private float[] downAngleV;
    private float[] length;
    private float[] lengthV;
    private float[] taper;
    private float[] segSplits;
    private float[] splitAngle;
    private float[] splitAngleV;
    private float[] curveRes;
    private float[] curve;
    private float[] curveBack;
    private float[] curveV;
    private float[] rotate;
    private float[] rotateV;
    private float[] branches;
    
    // 0...-Werte
    private float zeroScale;
    private float zeroScaleV;
    private float zeroBaseSplits;

    // Blätter
    private float leaves;
    private float leafShape;
    private float leafScale;
    private float leafScaleX;
    
    private float attractionUp;
    
    // Pruning
    private float pruneRatio;
    private float pruneWidth;
    private float pruneWidthPeak;
    private float prunePowerLow;
    private float prunePowerHigh;

    // Start is called before the first frame update
    void Start()
    {
        setPresetValues();
        
        startWeber();
     
        /*
     this.GetComponent<ConeGenerator>().getCone(2, 1, 30, Vector3.zero, Quaternion.identity);
     
     var x = this.GetComponent<ConeGenerator>().getCone(1, 0.5f, 10, new Vector3(0, 10, 0), Quaternion.Euler(new Vector3(0, 0, 30)));
     var y = this.GetComponent<ConeGenerator>().getCone(0.8f, 0.2f, 10, new Vector3(0, 15, 0), Quaternion.Euler(new Vector3(0, 0, -30)));*/

     //  Instantiate(x, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, -90)));


     //   Instantiate(first, new Vector3(0,5,0), Quaternion.identity);
    }

    private void startWeber()
    {
        float trunkLength = (scale - scaleV) * (length[0] + lengthV[0]);
        float bottomRadius = (trunkLength * ratio * (zeroScale + zeroScaleV));
        float topRadius = (bottomRadius * (1 - ((taper[0] <= 1 && taper[0] >= 0) ? taper[0] : 0)));
        GetComponent<ConeGenerator>().getCone(bottomRadius, topRadius, trunkLength, Vector3.zero, Quaternion.identity);

        float stems = (float) (branches[0] * (0.2 + 0.8 * (length[1] / length[0]) / length[1]));
        print(stems);
    }

    private void weberIteration(int depth, int prevRadius, int prevLength)
    {
        
    }

    private void setPresetValues()
    {
        shape = PresetParameters.getShape();
        baseSize = PresetParameters.getBaseSize();
        
        scale = PresetParameters.getScale()[0];
        scaleV = PresetParameters.getScaleV()[0];
        zScale = PresetParameters.getZScale();
        zScaleV = PresetParameters.getZScaleV();

        levels = PresetParameters.getLevels();
        ratio = PresetParameters.getRatio();
        ratioPower = PresetParameters.getRatioPower();

        lobes = PresetParameters.getLobes();
        lobeDepth = PresetParameters.getLobeDepth();
        flare = PresetParameters.getFlare();

        zeroScale = PresetParameters.getScale()[1];
        zeroScaleV = PresetParameters.getScaleV()[1];

        length = PresetParameters.getNLength();
        lengthV = PresetParameters.getNLengthV();
        taper = PresetParameters.getNTaper();
        zeroBaseSplits = PresetParameters.getBaseSplits();

        segSplits = PresetParameters.getNSegSplits();
        splitAngle = PresetParameters.getNSplitAngle();
        splitAngleV = PresetParameters.getNSplitAngleV();

        curveRes = PresetParameters.getNCurveRes();
        curve = PresetParameters.getNCurve();
        curveBack = PresetParameters.getNCurveBack();
        curveV = PresetParameters.getNCurveV();

        downAngle = PresetParameters.getNDownAngle();
        downAngleV = PresetParameters.getNDownAngleV();
        rotate = PresetParameters.getNRotate();
        rotateV = PresetParameters.getNRotateV();
        branches = PresetParameters.getNBranches();
    }

    private float ShapeRatio(float ratioValue)
    {
        switch (shape)
        {
            case 0:
            {
                return 0.2f + 0.8f * ratioValue;
            }
            case 1:
            {
                return 0.2f + 0.8f * (float) Math.Sin(Math.PI * ratioValue);
            }
            case 2:
            {
                return 0.2f + 0.8f * (float) Math.Sin(0.5 * Math.PI * ratioValue);
            }
            case 3:
            {
                return 1.0f;
            }
            case 4:
            {
                return 0.5f + 0.5f * ratioValue;
            }
            case 5:
            {
                if (ratioValue <= 0.7f)
                {
                    return ratioValue / 0.7f;
                }

                return (1.0f - ratioValue) / 0.3f;
            }
            case 6:
            {
                return 1.0f - 0.8f * ratioValue;
            }
            case 7:
            {
                if (ratioValue <= 0.7f)
                {
                    return 0.5f + 0.5f * ratioValue / 0.7f;
                }

                return 0.5f + 0.5f * (1.0f - ratioValue) / 0.3f;
            }
        }

        return -1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
