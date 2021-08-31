namespace DefaultNamespace {

    public class TreePreset {
        private static int idCount;

        public int id;

        public string name;
        public float baseSize;
        public float flare;
        public int levels;
        public float lobeDepth;
        public int lobes;
        public int nBaseSplits;
        public int[] nBranches;
        public float[] nCurve;
        public float[] nCurveBack;
        public float[] nCurveRes;
        public float[] nCurveV;
        public float[] nDownAngle;
        public float[] nDownAngleV;
        public float[] nLength;
        public float[] nLengthV;
        public float[] nRotate;
        public float[] nRotateV;
        public float[] nSegSplits;
        public float[] nSplitAngle;
        public float[] nSplitAngleV;
        public float[] nTaper;
        public float ratio;
        public float ratioPower;
        public float scale;
        public float scaleV;
        public int shape;
        public float zeroScale;
        public float zeroScaleV;
        public float zScale;
        public float zscaleV;

        public TreePreset(string name, int shape,
            float baseSize, float scale, float scaleV, float zScale, float zscaleV, int levels, float ratio,
            float ratioPower, int lobes, float lobeDepth, float flare, float zeroScale, float zeroScaleV,
            float[] nLength, float[] nLengthV, float[] nTaper, int nBaseSplits, float[] nSegSplits,
            float[] nSplitAngle, float[] nSplitAngleV, float[] nCurveRes, float[] nCurve, float[] nCurveBack,
            float[] nCurveV, float[] nDownAngle, float[] nDownAngleV, float[] nRotate, float[] nRotateV,
            int[] nBranches) {
            this.id = idCount++;
            this.name = name;
            this.shape = shape;
            this.baseSize = baseSize;
            this.scale = scale;
            this.scaleV = scaleV;
            this.zScale = zScale;
            this.zscaleV = zscaleV;
            this.levels = levels;
            this.ratio = ratio;
            this.ratioPower = ratioPower;
            this.lobes = lobes;
            this.lobeDepth = lobeDepth;
            this.flare = flare;
            this.zeroScale = zeroScale;
            this.zeroScaleV = zeroScaleV;
            this.nLength = nLength;
            this.nLengthV = nLengthV;
            this.nTaper = nTaper;
            this.nBaseSplits = nBaseSplits;
            this.nSegSplits = nSegSplits;
            this.nSplitAngle = nSplitAngle;
            this.nSplitAngleV = nSplitAngleV;
            this.nCurveRes = nCurveRes;
            this.nCurve = nCurve;
            this.nCurveBack = nCurveBack;
            this.nCurveV = nCurveV;
            this.nDownAngle = nDownAngle;
            this.nDownAngleV = nDownAngleV;
            this.nRotate = nRotate;
            this.nRotateV = nRotateV;
            this.nBranches = nBranches;
        }
    }
}