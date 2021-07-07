﻿using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorScript : MonoBehaviour {
    private float attractionUp;
    private float baseSize; // Länge des Stamms bis zum ersten Ast
    private float[] branches;
    private float[] curve;
    private float[] curveBack;
    private float[] curveRes;
    private float[] curveV;

    // Shared values
    private float[] downAngle;
    private float[] downAngleV;

    private float flare; // Exponentielle Expansion vom Stamm aus
    private float leafScale;
    private float leafScaleX;
    private float leafShape;

    // Blätter
    private float leaves;
    private float[] length;
    private float[] lengthV;

    private int levels; // Rekursionstiefe
    private float lobeDepth;

    // ??
    private int lobes;
    private float prunePowerHigh;
    private float prunePowerLow;

    // Pruning
    private float pruneRatio;
    private float pruneWidth;
    private float pruneWidthPeak;
    private float ratio; // Normalisierter Radius
    private float ratioPower; // Radius-Proportionen am Anfang eines Asts
    private float[] rotate;
    private float[] rotateV;

    // Größen und Skalierungen des Baums
    private float scale;
    private float scaleV;

    private float[] segSplits;

    /* Parameter */
    private int shape; // Shape-ID
    private float[] splitAngle;
    private float[] splitAngleV;
    private float[] taper;
    private float zeroBaseSplits;

    // 0...-Werte
    private float zeroScale;
    private float zeroScaleV;
    private float zScale;
    private float zScaleV;

    // Start is called before the first frame update
    private void Start() {
        PresetParameters.setPreset(1);
        setPresetValues();

        startWeber();
    }

    private void startWeber() {
        var scale_tree = HelperFunctions.getScale_tree(scale, scaleV);
        var length_base = HelperFunctions.getLength_base(baseSize, scale_tree); // Bare area without branches
        var length_trunk = HelperFunctions.getLength_trunk(length[0], lengthV[0], scale_tree);
        var radius_trunk = HelperFunctions.getRadius_trunk(length_trunk, ratio, zeroScale);
        var topRadius = HelperFunctions.getTopRadius(radius_trunk, taper[0]);
        var length_child_max = HelperFunctions.getLength_child_max(length[1], lengthV[1]);

        // var stems = HelperFunctions.getStems_base(branches[1], length_child, length_trunk, length_child_max);
        var stems = branches[1];
        var distanceBetweenChildren = (length_trunk - length_base) / stems;
            
        var stemObject = GetComponent<ConeGenerator>()
            .getCone(radius_trunk, topRadius, length_trunk, Vector3.zero, Quaternion.identity);
        stemObject.name = "Stamm";

        var angle = (rotate[1] + Random.Range(-20, 20)) % 360;

        for (var i = 0; i < stems; i++) {
            var start = length_base + distanceBetweenChildren * i;
            var length_child = HelperFunctions.getLength_child_base(shape, length_trunk, length_child_max, start, length_base);
            var radius_child = HelperFunctions.getRadius_child(radius_trunk, topRadius, length_child, length_trunk,
                start, ratioPower);

            weberIteration(
                stemObject,
                1, 
                new Vector3(0, start, 0), 
                radius_child, 
                length_child, 
                radius_trunk, 
                length_base, 
                length_trunk, 
                angle,
                start);
            
            angle = (angle + (rotate[1] + Random.Range(-20, 20))) % 360; 
        }
    }

    private void weberIteration(
        GameObject parent,
        int depth, 
        Vector3 startPosition,
        float currentRadius,
        float currentLength,
        float prevRadius,
        float length_base, 
        float length_parent, 
        float rotateAngle,
        float offset) {

        var topRadius = HelperFunctions.getTopRadius(currentRadius, taper[depth]);
        Vector3 downangle_current;

        if (downAngleV[depth] >= 0) {
            var angle = HelperFunctions.getDownAnglePositive(downAngle[depth], downAngleV[depth]);
            downangle_current = new Vector3(0, rotateAngle, angle);
        }
        else {
            var angle = HelperFunctions.getDownAngleNegative(downAngle[depth], downAngleV[depth], length_parent, offset,
                length_base);
            
            downangle_current = new Vector3(0, rotateAngle, angle);
        }

        var x =  GetComponent<ConeGenerator>().getCone(currentRadius, topRadius, currentLength, startPosition,
            Quaternion.Euler(downangle_current));

        x.transform.parent = parent.transform;
        
        

        if (depth < levels) {
            var length_child_max = length[depth + 1] + lengthV[depth + 1];
            var offset_child = currentLength * baseSize;
            var length_child = length_child_max * (currentLength - 0.6f * offset_child);
    
            var stems = branches[depth] * (1.0f - 0.5f * offset_child / length_parent);
        }
    }

    private void setPresetValues() {
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

    private float ShapeRatio(float ratioValue) {
        return ShapeRatio(shape, ratioValue);
    }

    private float ShapeRatio(int currentShape, float ratioValue) {
        switch (currentShape) {
            case 0: {
                return 0.2f + 0.8f * ratioValue;
            }
            case 1: {
                return 0.2f + 0.8f * (float) Math.Sin(Math.PI * ratioValue);
            }
            case 2: {
                return 0.2f + 0.8f * (float) Math.Sin(0.5 * Math.PI * ratioValue);
            }
            case 3: {
                return 1.0f;
            }
            case 4: {
                return 0.5f + 0.5f * ratioValue;
            }
            case 5: {
                if (ratioValue <= 0.7f) return ratioValue / 0.7f;

                return (1.0f - ratioValue) / 0.3f;
            }
            case 6: {
                return 1.0f - 0.8f * ratioValue;
            }
            case 7: {
                if (ratioValue <= 0.7f) return 0.5f + 0.5f * ratioValue / 0.7f;

                return 0.5f + 0.5f * (1.0f - ratioValue) / 0.3f;
            }
        }

        return -1f;
    }
}