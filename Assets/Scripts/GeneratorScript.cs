using System;
using DefaultNamespace;
using UnityEngine;

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
        var stemObject = GetComponent<ConeGenerator>()
            .getCone(radius_trunk, topRadius, length_trunk, Vector3.zero, Quaternion.identity);

        var length_child_max = length[1] + lengthV[1];
        var length_child = length_trunk * length_child_max *
                           ShapeRatio((length_trunk - length_base) / (length_trunk - length_base));

        var stems = (int) Math.Floor(branches[1] * (0.2f + 0.8f * (length[1] / length_trunk) / length[1]));

        var radius_child = radius_trunk * (float) Math.Pow(length_child / length_trunk, ratioPower);

        var distanceBetweenChildren = (length_trunk - length_base) / stems;

        for (var i = 0; i < stems; i++) {
            var start = length_base + distanceBetweenChildren * i;
            weberIteration(1, new Vector3(0, start, 0), length_child, radius_trunk, length_base, length_trunk, start);
        }
    }

    private void weberIteration(int depth, Vector3 startPosition, float currentLength, float prevRadius,
        float length_base, float length_parent, float offset) {
        // print("--- ITERATION " + depth + " ---");
        var down_n = downAngle[depth] + downAngleV[depth];

        var radius_n = (float) (prevRadius * Math.Pow(currentLength / length_parent, ratioPower));
        //  print("radius_n: " + radius_n);
        var topRadius = HelperFunctions.getTopRadius(radius_n, taper[depth]);
        var downangle_current = new Vector3();

        if (downAngleV[depth] >= 0)
            downangle_current = new Vector3(0, 0, downAngle[depth] + downAngleV[depth]);
        else
            downangle_current = new Vector3(0, 0, downAngle[depth] + downAngleV[depth] *
                (1 - 2 * ShapeRatio(0,
                    (length_parent - offset) / (length_parent - length_base))));
        //   print("downangle_current: " + downangle_current);
        //   print("downAngleV: " + downAngleV[1]);

        GetComponent<ConeGenerator>().getCone(radius_n, topRadius, currentLength, startPosition,
            Quaternion.Euler(downangle_current));

        if (depth < levels) {
            var length_child_max = length[depth + 1] + lengthV[depth + 1];
            var offset_child = currentLength * baseSize;
            var length_child = length_child_max * (currentLength - 0.6f * offset_child);

            /*    print("length_child_max: " + length_child_max);
                print("offset_child: " + offset_child);
                print("length_child: " + length_child);
    */
            var stems = branches[depth] * (1.0f - 0.5f * offset_child / length_parent);
            //          print("stems: " + stems);
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