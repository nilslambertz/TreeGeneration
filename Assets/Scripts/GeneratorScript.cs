using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;

public class GeneratorScript : MonoBehaviour
{
    // Preset
    private static TreePreset treePreset;

    private static List<GameObject> treeList = new List<GameObject>();

    public static List<GameObject> getTreeList()
    {
        return treeList;
    }

    public static void clearTreeList()
    {
        treeList.Clear();
    }

    private static bool randomColors;

    /// <summary>
    /// Starts Weber-Penn-algorithm
    /// </summary>
    /// <param name="startPosition">initial position of the tree</param>
    public static List<GameObject> startWeber(Vector3 startPosition)
    {
        treePreset = SharedValues.getCurrentPreset();

        int objectCount = 0;
        List<GameObject> list = new List<GameObject>();

        // Calculating values for stem
        var scale_tree = HelperFunctions.getScale_tree(treePreset.scale, treePreset.scaleV);
        var length_base = HelperFunctions.getLength_base(treePreset.baseSize, scale_tree); // Bare area without branches
        var length_trunk = GetRandomizedValue(HelperFunctions.getLength_trunk(treePreset.nLength[0], treePreset.nLengthV[0], scale_tree), 0.25f);
        var radius_trunk = HelperFunctions.getRadius_trunk(length_trunk, treePreset.ratio, treePreset.zeroScale);
        var topRadius = HelperFunctions.getTopRadius(radius_trunk, treePreset.nTaper[0]);
        var length_child_max = HelperFunctions.getLength_child_max(treePreset.nLength[1], treePreset.nLengthV[1]);

        // Number of children at stem and vertical distance between them
        var stems = treePreset.nBranches[1];
        var distanceBetweenChildren = (length_trunk - length_base) / stems;

        randomColors = OptionListScript.getOption(OptionListScript.OptionElement.randomColors).value;

        // Generating stem-object
        /* var stemObject = GetComponent<ConeGenerator>()
             .getCone(radius_trunk, topRadius, length_trunk, startPosition, Quaternion.identity);*/
        var stemObject =
            ConeGenerator.getCone(radius_trunk, topRadius, length_trunk, startPosition, Quaternion.identity, GetColor(randomColors));
        stemObject.name = "Stem";
        stemObject.AddComponent<MeshCollider>();
        stemObject.layer = 8;
        treeList.Add(stemObject);
        objectCount++;

        var angle = UnityEngine.Random.Range(0, 360); // Rotation around the stem where next branch is spawned
        for (var i = 0; i < stems; i++)
        {
            // vertical offset of the child
            var start = length_base + distanceBetweenChildren * i;
            Vector3 position = startPosition;
            position.y = start;

            // Calculating child values
            var length_child = GetRandomizedValue(HelperFunctions.getLength_child_base(treePreset.shape, length_trunk, length_child_max, start, length_base), 0.1f);
            var radius_child = HelperFunctions.getRadius_child(radius_trunk, topRadius, length_child, length_trunk,
                start, treePreset.ratioPower);
            var numberOfChildren =
                HelperFunctions.getStems_base(treePreset.nBranches[2], length_child, length_trunk, length_child_max);

            bool curvedBranches = OptionListScript.getOption(OptionListScript.OptionElement.curvedBranches).value;

            List<GameObject> childs = new List<GameObject>();
            // Next iteration of the algorithm, starting with the child-object
            if (curvedBranches)
            {
                childs = weberIterationCurved(
               i,
               stemObject,
               1,
               numberOfChildren,
               position,
               radius_child,
               length_child,
               radius_trunk,
               length_base,
               length_trunk,
               angle,
               startPosition,
               start);
            }
            else
            {
                childs = weberIteration(
                i,
                stemObject,
                1,
                numberOfChildren,
                position,
                radius_child,
                length_child,
                radius_trunk,
                length_base,
                length_trunk,
                angle,
                startPosition,
                start);
            }

            list.AddRange(childs);

            angle = (int)(angle + (treePreset.nRotate[1] + Random.Range(-30, 30))) % 360; // Next angle around the stem
        }

        objectCount = list.Count;

        UIController.addValue(UIDisplay.UIDebugTextEnum.NumberOfObjects, objectCount);
        UIController.addValue(UIDisplay.UIDebugTextEnum.NumberOfTrees, 1);

        return list;
    }

    /// <summary>
    /// Iteration of the Weber-Penn-algorithm
    /// </summary>
    /// <param name="id">child-id</param>
    /// <param name="parent">parent-object</param>
    /// <param name="depth">current depth in algorithm</param>
    /// <param name="numberOfChildren">number of children this object will generate</param>
    /// <param name="startPosition">initial position of this object</param>
    /// <param name="currentRadius">radius of this branch</param>
    /// <param name="currentLength">length of this branch</param>
    /// <param name="prevRadius">radius of the parent-branch</param>
    /// <param name="length_base">length of branchless area at the stem</param>
    /// <param name="length_parent">length of the parent-branch</param>
    /// <param name="rotateAngle">angle around the parent-stem</param>
    /// <param name="offset">offset from the start of the stem</param>
    private static List<GameObject> weberIteration(
        int id,
        GameObject parent,
        int depth,
        int numberOfChildren,
        Vector3 startPosition,
        float currentRadius,
        float currentLength,
        float prevRadius,
        float length_base,
        float length_parent,
        float rotateAngle,
        Vector3 downangle_parent,
        float offset)
    {
        int objectCount = 0;
        List<GameObject> list = new List<GameObject>();

        var topRadius = HelperFunctions.getTopRadius(currentRadius, treePreset.nTaper[depth]);
        Vector3 downangle_current;

        if (treePreset.nDownAngleV[depth] >= 0)
        {
            var angle = HelperFunctions.getDownAnglePositive(treePreset.nDownAngle[depth], treePreset.nDownAngleV[depth]);
            downangle_current = new Vector3(0, rotateAngle, (0.5f * angle)) + downangle_parent;
        }
        else
        {
            var angle = HelperFunctions.getDownAngleNegative(treePreset.nDownAngle[depth], treePreset.nDownAngleV[depth], length_parent, offset,
                length_base);

            downangle_current = new Vector3(0, rotateAngle, angle) + downangle_parent;
        }

        var branchObject = ConeGenerator.getCone(currentRadius, topRadius, currentLength, startPosition,
            Quaternion.Euler(downangle_current), GetColor(randomColors));
        branchObject.SetActive(false);
        objectCount++;
        list.Add(branchObject);

        branchObject.transform.parent = parent.transform;
        branchObject.name = "Branch " + id;


        //   if (depth < levels-1) {
        if (depth < 2)
        {
            var startOffset = currentLength / 10;
            var endOffset = currentLength / 5;
            var distanceBetweenChildren = (currentLength - (startOffset + endOffset)) / numberOfChildren;
            var angle = UnityEngine.Random.Range(0, 360);
            //    int i = 0;
            for (var i = 0; i < numberOfChildren; i++)
            {
                var start = startOffset + distanceBetweenChildren * i;
                var length_child_max = treePreset.nLength[depth + 1] + treePreset.nLengthV[depth + 1];
                var offset_child = currentLength * treePreset.baseSize;
                var length_child = GetRandomizedValue(HelperFunctions.getLength_child_iteration(length_child_max, currentLength, offset_child + start), 0.1f);
                var radius_child = HelperFunctions.getRadius_child(currentRadius, topRadius, length_child,
                    currentLength, start, treePreset.ratioPower);

                var stems = HelperFunctions.getStems_iteration(treePreset.nBranches[depth], offset_child, length_parent);
                var newPosition = startPosition + Vector3.Normalize(branchObject.transform.up) * start;

                List<GameObject> childs = weberIteration(
                    i,
                    branchObject,
                    depth + 1,
                    stems,
                    newPosition,
                    radius_child,
                    length_child,
                    currentRadius,
                    length_base,
                    currentLength,
                    angle,
                    downangle_current,
                    offset + start);

                list.AddRange(childs);

                angle = (int)(90 + (angle + (treePreset.nRotate[depth] + Random.Range(-20, 20))) % 360);
            }
        }

        return list;
    }

    private static List<GameObject> weberIterationCurved(
        int id,
        GameObject parent,
        int depth,
        int numberOfChildren,
        Vector3 startPosition,
        float currentRadius,
        float currentLength,
        float prevRadius,
        float length_base,
        float length_parent,
        float rotateAngle,
        Vector3 downangle_parent,
        float offset)
    {
        int objectCount = 0;
        List<GameObject> list = new List<GameObject>();
        List<GameObject> segments = new List<GameObject>();

        var topRadius = HelperFunctions.getTopRadius(currentRadius, treePreset.nTaper[depth]);

        int curve_res = (int)treePreset.nCurveRes[depth];
        float segment_length = currentLength / treePreset.nCurveRes[depth];
        float[] segment_radius = new float[(curve_res + 1)];
        for (int i = 0; i <= curve_res; i++)
        {
            segment_radius[i] = currentRadius - i * ((currentRadius - topRadius) / curve_res);
        }
        Vector3[] segment_angle = new Vector3[curve_res];

        Vector3 downangle_current;

        if (treePreset.nDownAngleV[depth] >= 0)
        {
            var angle = HelperFunctions.getDownAnglePositive(treePreset.nDownAngle[depth], treePreset.nDownAngleV[depth]);
            downangle_current = new Vector3(0, rotateAngle, (0.5f * angle)) + downangle_parent;
        }
        else
        {
            var angle = HelperFunctions.getDownAngleNegative(treePreset.nDownAngle[depth], treePreset.nDownAngleV[depth], length_parent, offset,
                length_base);

            downangle_current = new Vector3(0, rotateAngle, angle) + downangle_parent;
        }
        segment_angle[0] = downangle_current;

        var branchObject = ConeGenerator.getCone(currentRadius, segment_radius[1], segment_length, startPosition,
            Quaternion.Euler(downangle_current), GetColor(randomColors));
        branchObject.SetActive(false);
        objectCount++;
        list.Add(branchObject);
        segments.Add(branchObject);

        branchObject.transform.parent = parent.transform;
        branchObject.name = "Branch " + id + " segment 0";
        id++;

        float segment_curve;
        for (int i = 1; i < curve_res; i++)
        {
            GameObject segmentObject;
            Vector3 prevSegEnd = new Vector3();
            prevSegEnd = segments[i - 1].transform.position + Vector3.Normalize(segments[i - 1].transform.up) * segment_length;

            if (treePreset.nCurveBack[depth] == 0)
            {
                segment_curve = treePreset.nCurve[depth] / treePreset.nCurveRes[depth];
            }
            else
            {
                if ((i * segment_length) < (currentLength / 2))
                {
                    segment_curve = treePreset.nCurve[depth] / (treePreset.nCurveRes[depth] / 2);
                }
                else
                {
                    segment_curve = treePreset.nCurveBack[depth] / (treePreset.nCurveRes[depth] / 2);
                }
            }

            downangle_current = new Vector3(0, 0, segment_curve) + segment_angle[i - 1];

            segment_angle[i] = downangle_current;

            segmentObject = ConeGenerator.getCone(segment_radius[i], segment_radius[i + 1], segment_length, prevSegEnd,
                Quaternion.Euler(downangle_current), GetColor(randomColors));
            segmentObject.transform.parent = parent.transform;
            segmentObject.name = "Branch " + id + " segment " + i;

            segmentObject.SetActive(false);
            objectCount++;
            list.Add(segmentObject);
            segments.Add(segmentObject);
            id++;
        }


        //   if (depth < levels-1) {
        if (depth < 2)
        {
            var startOffset = currentLength / 10;
            var endOffset = currentLength / 5;
            var distanceBetweenChildren = (currentLength - (startOffset + endOffset)) / numberOfChildren;
            var angle = UnityEngine.Random.Range(0, 360);
            //    int i = 0;
            for (var i = 0; i < numberOfChildren; i++)
            {
                var start = startOffset + distanceBetweenChildren * i;
                var length_child_max = treePreset.nLength[depth + 1] + treePreset.nLengthV[depth + 1];
                var offset_child = currentLength * treePreset.baseSize;
                var length_child = GetRandomizedValue(HelperFunctions.getLength_child_iteration(length_child_max, currentLength, offset_child + start), 0.1f);
                var radius_child = HelperFunctions.getRadius_child(currentRadius, topRadius, length_child,
                    currentLength, start, treePreset.ratioPower);

                var stems = HelperFunctions.getStems_iteration(treePreset.nBranches[depth], offset_child, length_parent);

                // finding the new position on the segments
                int tmp_seg_index = (int)Math.Floor((start / segment_length));
                var tmp_distance = start % segment_length;

                var newPosition = segments[tmp_seg_index].transform.position + Vector3.Normalize(segments[tmp_seg_index].transform.up) * tmp_distance;

                List<GameObject> childs = weberIterationCurved(
                    i,
                    segments[tmp_seg_index],
                    depth + 1,
                    stems,
                    newPosition,
                    radius_child,
                    length_child,
                    currentRadius,
                    length_base,
                    currentLength,
                    angle,
                    segment_angle[tmp_seg_index],
                    offset + start);

                list.AddRange(childs);

                angle = (int)(90 + (angle + (treePreset.nRotate[depth] + Random.Range(-20, 20))) % 360);
            }
        }

        return list;
    }

    private static Color GetColor(bool random)
    {
        if (random)
        {
            return new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.01f, 0.9f), 1f);
        }
        else
        {
            return new Color(Random.Range(0.19f, 0.21f), Random.Range(0.11f, 0.13f), Random.Range(0.01f, 0.02f), 1f);
        }
    }

    private static float GetRandomizedValue(float value, float percent)
    {
        return value + Random.Range(-value * percent, value * percent);
    }
}