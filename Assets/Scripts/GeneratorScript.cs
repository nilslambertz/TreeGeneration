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
        PresetParameters.setPreset(1);
        setPresetValues();

        startWeber();
    }

    private void startWeber()
    {
        float scale_tree = scale + scaleV;
        float length_base = baseSize * scale_tree;
        print("length_base: " + length_base);
        
        float length_trunk = (length[0] + lengthV[0]) * scale;
        print("length_trunk: " + length_trunk);
        float radius_trunk = length_trunk * ratio * zeroScale;
        print("radius_trunk: " + radius_trunk);
        float topRadius = (radius_trunk * (1 - ((taper[0] <= 1 && taper[0] >= 0) ? taper[0] : 0)));
        print("topRadius: " + topRadius);
        var stem = GetComponent<ConeGenerator>().getCone(radius_trunk, topRadius, length_trunk, Vector3.zero, Quaternion.identity);
        stem.name = "Stem";

        float offset_child = length_trunk * baseSize;
        print("offset_child: " + offset_child);
        
        float length_child_max = length[1] + lengthV[1];
        print("length_child_max: " + length_child_max);
        float length_child = length_trunk * length_child_max * ShapeRatio((length_trunk - offset_child) / (length_trunk - length_base));
        print("length_child: " + length_child);
                          
        int stems = (int) Math.Floor(branches[1] * (0.2f + 0.8f * (length[1] / length_trunk) / length[1]));
        print("stems: " + stems);

        print("radius_trunk: " + radius_trunk);
        float radius_child = radius_trunk * (float) (Math.Pow(length_child / length_trunk, ratioPower));
        print("radius_child: " + radius_child);

        float distanceBetweenChildren = (length_trunk - offset_child) / stems;

        for (int i = 0; i < stems; i++)
        {
            float start = offset_child + distanceBetweenChildren * i;
            weberIteration(1, new Vector3(0, start, 0), length_child, radius_trunk, length_base, length_trunk, start);
        }
    }

    private void weberIteration(int depth, Vector3 startPosition, float currentLength, float prevRadius, float length_base, float length_parent, float offset)
    {
       // print("--- ITERATION " + depth + " ---");
        float down_n = downAngle[depth] + downAngleV[depth];
        
        float radius_n = (float) (prevRadius * Math.Pow(currentLength / length_parent, ratioPower));
      //  print("radius_n: " + radius_n);
        float topRadius = (radius_n * (1 - ((taper[depth] <= 1 && taper[depth] >= 0) ? taper[depth] : 0)));

        Vector3 downangle_current = new Vector3();

        if (downAngleV[depth] >= 0)
        {
            downangle_current = new Vector3(0, 0, downAngle[depth] + downAngleV[depth]);
        }
        else
        {
            downangle_current = new Vector3(0, 0,downAngle[depth] + (downAngleV[depth] *
                                                    (1 - 2 * ShapeRatio(0,
                                                        (length_parent - offset) / (length_parent - length_base)))));
         //   print("downangle_current: " + downangle_current);
        }
     //   print("downAngleV: " + downAngleV[1]);
            
        GetComponent<ConeGenerator>().getCone(radius_n, topRadius, currentLength, startPosition, Quaternion.Euler(downangle_current));

        if (depth < levels)
        {
            float length_child_max = length[depth+1] + lengthV[depth+1];
            float offset_child = currentLength * baseSize;
            float length_child = length_child_max * (currentLength - 0.6f * offset_child);
            
        /*    print("length_child_max: " + length_child_max);
            print("offset_child: " + offset_child);
            print("length_child: " + length_child);
*/
            float stems = branches[depth] * (1.0f - 0.5f * offset_child / length_parent);
  //          print("stems: " + stems);
        }
    }
    
    private float getStems_base(float stems_max, float length_child, float length_parent, float length_child_max)
    {
        return stems_max * (0.2f + 0.8f * (length_child / length_parent) / length_child_max);
    }

    private float getStems_iteration(float stems_max, float offset_child, float length_parent)
    {
        return stems_max * (1.0f - 0.5f * offset_child / length_parent);
    }

    private float getLength_child_base(float length_trunk, float length_child_max, float offset_child, float length_base)
    {
        return length_trunk * length_child_max * ShapeRatio(shape, (length_trunk - offset_child) / (length_trunk - length_base));
    }
    
    private float getLength_child_iteration(float length_child_max, float length_current, float offset_child)
    {
        return length_child_max * (length_current - 0.6f * offset_child);
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
        return ShapeRatio(shape, ratioValue);
    }

    private float ShapeRatio(int currentShape, float ratioValue)
    {
        switch (currentShape)
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
