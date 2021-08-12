using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class GeneratorScript : MonoBehaviour {
    /* Public settings */
    public int presetId = 1;
    private int objectCount = 0;

    // Preset-Helperclass
    private PresetParameters presetParameters;
    private TreePreset treePreset;

    // Start is called before the first frame update
    private void Start() {
        presetParameters = new PresetParameters();

        treePreset = presetParameters.getPreset(presetId);
        
        startWeber(Vector3.zero);
    }

    /// <summary>
    /// Starts Weber-Penn-algorithm
    /// </summary>
    /// <param name="startPosition">initial position of the tree</param>
    private void startWeber(Vector3 startPosition) {
        // Calculating values for stem
        var scale_tree = HelperFunctions.getScale_tree(treePreset.scale, treePreset.scaleV);
        var length_base = HelperFunctions.getLength_base(treePreset.baseSize, scale_tree); // Bare area without branches
        var length_trunk = HelperFunctions.getLength_trunk(treePreset.nLength[0], treePreset.nLengthV[0], scale_tree);
        var radius_trunk = HelperFunctions.getRadius_trunk(length_trunk, treePreset.ratio, treePreset.zeroScale);
        var topRadius = HelperFunctions.getTopRadius(radius_trunk, treePreset.nTaper[0]);
        var length_child_max = HelperFunctions.getLength_child_max(treePreset.nLength[1], treePreset.nLengthV[1]);
        
        // Number of children at stem and vertical distance between them
        var stems = treePreset.nBranches[1];
        var distanceBetweenChildren = (length_trunk - length_base) / stems;
            
        // Generating stem-object
        var stemObject = GetComponent<ConeGenerator>()
            .getCone(radius_trunk, topRadius, length_trunk, startPosition, Quaternion.identity);
        stemObject.name = "Stem";
        
        var angle = (treePreset.nRotate[1] + Random.Range(-20, 20)) % 360; // Rotation around the stem where next branch is spawned
        for (var i = 0; i < stems; i++) {
            // vertical offset of the child
            var start = length_base + distanceBetweenChildren * i;
            Vector3 position = startPosition;
            position.y = start;
            
            // Calculating child values
            var length_child = HelperFunctions.getLength_child_base(treePreset.shape, length_trunk, length_child_max, start, length_base);
            var radius_child = HelperFunctions.getRadius_child(radius_trunk, topRadius, length_child, length_trunk,
                start, treePreset.ratioPower);
            var numberOfChildren =
                HelperFunctions.getStems_base(treePreset.nBranches[2], length_child, length_trunk, length_child_max);

            // Next iteration of the algorithm, starting with the child-object
            weberIteration(
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
                start);
            
            angle = (angle + (treePreset.nRotate[1] + Random.Range(-20, 20))) % 360; // Next angle around the stem
        }
        
        print("Number of objects: " + objectCount); // Number of objects spawned
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
    private void weberIteration(
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
        float offset) {

        var topRadius = HelperFunctions.getTopRadius(currentRadius, treePreset.nTaper[depth]);
        Vector3 downangle_current;

        if (treePreset.nDownAngleV[depth] >= 0) {
            var angle = HelperFunctions.getDownAnglePositive(treePreset.nDownAngle[depth], treePreset.nDownAngleV[depth]);
            print(angle);
            downangle_current = new Vector3(0, rotateAngle,  angle); // TODO: angle should be passed as Vector3 to avoid erros in rotation
        }
        else {
            var angle = HelperFunctions.getDownAngleNegative(treePreset.nDownAngle[depth], treePreset.nDownAngleV[depth], length_parent, offset,
                length_base);
            print(angle);
            
            downangle_current = new Vector3(0, rotateAngle, angle); // TODO: angle should be passed as Vector3 to avoid erros in rotation
        }

        var branchObject =  GetComponent<ConeGenerator>().getCone(currentRadius, topRadius, currentLength, startPosition,
            Quaternion.Euler(downangle_current));

        branchObject.transform.parent = parent.transform;
        branchObject.name = "Branch " + id;
        
        objectCount++;
        
     //   if (depth < levels-1) {
        if (depth < 2) {
            var startOffset = currentLength / 10;
            var endOffset = currentLength / 5;
            var distanceBetweenChildren = (currentLength - (startOffset + endOffset)) / numberOfChildren;
            var angle = 90 + (treePreset.nRotate[depth] + Random.Range(-20, 20)) % 360;
        //    int i = 0;
            for (var i = 0; i < numberOfChildren; i++) {
                var start = startOffset + distanceBetweenChildren * i;
                var length_child_max = treePreset.nLength[depth + 1] + treePreset.nLengthV[depth + 1];
                var offset_child = currentLength * treePreset.baseSize;
                var length_child = HelperFunctions.getLength_child_iteration(length_child_max, currentLength, offset_child+start);
                var radius_child = HelperFunctions.getRadius_child(currentRadius, topRadius, length_child,
                    currentLength, start, treePreset.ratioPower);

                var stems = HelperFunctions.getStems_iteration(treePreset.nBranches[depth], offset_child, length_parent);
                var newPosition = startPosition + Vector3.Normalize(branchObject.transform.up) * start;

                weberIteration(
                    i,
                    branchObject,
                    depth+1, 
                    stems,
                    newPosition, 
                    radius_child, 
                    length_child, 
                    currentRadius, 
                    length_base, 
                    currentLength, 
                    angle,
                    offset + start);
            
                angle = 90 + (angle + (treePreset.nRotate[depth] + Random.Range(-20, 20))) % 360; 
            }
        }
    }
}