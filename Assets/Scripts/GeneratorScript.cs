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
    }

    private void startWeber()
    {
        float scale_tree = scale + scaleV;
        float length_base = baseSize * scale_tree;
        
        float length_trunk = (length[0] + lengthV[0]) * scale;
        float radius_trunk = length_trunk * ratio * zeroScale;
        float topRadius = (radius_trunk * (1 - ((taper[0] <= 1 && taper[0] >= 0) ? taper[0] : 0)));
        GetComponent<ConeGenerator>().getCone(radius_trunk, topRadius, length_trunk, Vector3.zero, Quaternion.identity);

        float offset_child = length_trunk * baseSize;
        print(offset_child);
        
        float length_child_max = length[1] + lengthV[1];
        print(length_child_max);
        float length_child = length_trunk * length_child_max * ShapeRatio((length_trunk - offset_child) / (length_trunk - length_base));
        print(length_child);
                          
        float stems = (branches[1] * (0.2f + 0.8f * (length[1] / length_trunk) / length[1]));
        
        weberIteration(1, new Vector3(0, offset_child, 0), length_child, radius_trunk, length_trunk);
        
        print(stems);
    }

    private void weberIteration(int depth, Vector3 startPosition, float currentLength, float prevRadius, float length_parent)
    {
        float down_n = downAngle[depth] + downAngleV[depth];
        
        float radius_n = (float) (prevRadius * Math.Pow(currentLength / length_parent, ratioPower));
        float topRadius = (radius_n * (1 - ((taper[depth] <= 1 && taper[depth] >= 0) ? taper[depth] : 0)));
        
        GetComponent<ConeGenerator>().getCone(radius_n, topRadius, currentLength, startPosition, Quaternion.Euler(new Vector3(0, 60, 20)));
        
        if (levels < depth)
        {
            float length_child_max = length[depth+1] + lengthV[depth+1];
            float offset_child = currentLength * baseSize;
            float length_child = length_child_max * (length_parent - 0.6f * offset_child);
        }
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
