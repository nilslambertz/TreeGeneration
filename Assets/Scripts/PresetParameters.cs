using System.Collections.Generic;

namespace DefaultNamespace {
    public class PresetParameters {

        private static List<Preset> presetList = new List<Preset>();

        public PresetParameters() {
            Preset quakingAspen = new Preset(
                7, 0.4f, 13, 3, 1, 0,
                3, 0.015f, 1.2f, 5, 0.07f,
                0.6f, 1, 0, new []{1, 0.3f, 0.6f, 0},
                new []{0f, 0, 0, 0}, new []{1f, 1, 1, 1},
                0, new []{0f, 0, 0, 0}, new []{0f, 0, 0, 0},
                new []{0f, 0, 0, 0}, new []{3f, 5, 3, 1},
                new []{0f, -40, -40, 0}, new []{0f, 0, 0, 0},
                new []{20f, 50, 75, 0}, new []{0f, 60, 45, 45},
                new []{0f, -50, 10, 10}, new []{0f, 140, 140, 77},
                new []{0f, 0, 0, 0}, new []{0, 50, 30, 10}
            );
            
            presetList.Add(quakingAspen);
        }

        /*
         * 0 = Quaking Aspen
         * 1 = Black Tupelo
         * 2 = Weeping Willow
         * 3 = CA Black Oak
         */
        private static int currentPreset;

        /* Shape */

        private static readonly int[] shape = {7, 4, 3, 2};

        /* BaseSize */

        private static readonly float[] baseSize = {0.4f, 0.2f, 0.05f, 0.05f};

        /* nScale */

        private static readonly int[][] nScale = {
            new[] {13, 1},
            new[] {23, 1},
            new[] {15, 1},
            new[] {10, 1}
        };

        private static readonly int[][] nScaleV = {
            new[] {2, 0},
            new[] {5, 0},
            new[] {5, 0},
            new[] {10, 0}
        };

        /* zScale */

        private static readonly int[] zScale = {1, 1, 1, 1};

        private static readonly int[] zScaleV = {0, 0, 0, 0};

        /* Levels */

        private static readonly int[] levels = {3, 4, 4, 3};

        /* Ratio */

        private static readonly float[] ratio = {0.015f, 0.015f, 0.03f, 0.018f};

        private static readonly float[] ratioPower = {1.2f, 1.3f, 2f, 1.3f};

        /* Lobes */

        private static readonly int[] lobes = {5, 3, 9, 5};

        private static readonly float[] lobeDepth = {0.07f, 0.1f, 0.03f, 0.1f};

        /* Flare */

        private static readonly float[] flare = {0.6f, 1f, 0.75f, 1.3f};

        /* nLength */

        private static readonly float[][] nLength = {
            new[] {1f, 0.3f, 0.6f, 0f},
            new[] {1f, 0.3f, 0.6f, 0.4f},
            new[] {0.8f, 0.5f, 1.5f, 0.1f},
            new[] {1f, 0.8f, 0.2f, 0f}
        };

        private static readonly float[][] nLengthV = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0.05f, 0.1f, 0f},
            new[] {0f, 0.1f, 0f, 0f},
            new[] {0f, 0.1f, 0.05f, 0f}
        };

        /* nTaper */

        private static readonly float[][] nTaper = {
            new[] {1f, 1f, 1f, 1f},
            new[] {1.1f, 1f, 1f, 1f},
            new[] {1f, 1f, 1f, 1f},
            new[] {0.95f, 1f, 1f, 1f}
        };

        /* Splits */

        private static readonly int[] baseSplits = {0, 0, 2, 2};

        private static readonly float[][] nSegSplits = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0.1f, 0.2f, 0.2f, 0f},
            new[] {0.4f, 0.2f, 0.1f, 0f}
        };

        /* nSplitAngle */

        private static readonly float[][] nSplitAngle = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {3f, 30f, 45f, 0f},
            new[] {10f, 10f, 10f, 0f}
        };

        private static readonly float[][] nSplitAngleV = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 10f, 20f, 0f},
            new[] {0f, 10f, 10f, 0f}
        };

        /* nCurve */

        private static readonly float[][] nCurveRes = {
            new[] {3f, 5f, 3f, 1f},
            new[] {10f, 10f, 10f, 1f},
            new[] {8f, 16f, 12f, 1f},
            new[] {8f, 10f, 3f, 1f}
        };

        private static readonly float[][] nCurve = {
            new[] {0f, -40f, -40f, 0f},
            new[] {0f, 0f, -10f, 0f},
            new[] {0f, 40f, 0f, 0f},
            new[] {0f, 40f, 0f, 0f}
        };

        private static readonly float[][] nCurveBack = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {20f, 80f, 0f, 0f},
            new[] {0f, -70f, 0f, 0f}
        };

        private static readonly float[][] nCurveV = {
            new[] {20f, 50f, 75f, 0f},
            new[] {40f, 90f, 150f, 0f},
            new[] {120f, 90f, 0f, 0f},
            new[] {90f, 150f, -30f, 0f}
        };

        /* nDownAngle */

        private static readonly float[][] nDownAngle = {
            new[] {0f, 60f, 45f, 45f},
            new[] {0f, 60f, 30f, 45f},
            new[] {0f, 20f, 30f, 45f},
            new[] {0f, 30f, 45f, 45f}
        };

        private static readonly float[][] nDownAngleV = {
            new[] {0f, -50f, 10f, 10f},
            new[] {0f, -40f, 10f, 10f},
            new[] {0f, 10f, 10f, 10f},
            new[] {0f, -30f, 10f, 10f}
        };

        /* nRotate */

        private static readonly float[][] nRotate = {
            new[] {0f, 140f, 140f, 77f},
            new[] {0f, 140f, 140f, 140f},
            new[] {0f, -120f, -120f, 140f},
            new[] {0f, 80f, 140f, 140f}
        };

        private static readonly float[][] nRotateV = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 30f, 30f, 0f},
            new[] {0f, 0f, 0f, 0f}
        };

        /* nBranches */

        private static readonly float[][] nBranches = {
            new[] {0f, 50f, 30f, 10f},
            new[] {0f, 50f, 25f, 12f},
            new[] {0f, 25f, 10f, 300f},
            new[] {0f, 40f, 120f, 0f}
        };

        public static void setPreset(int id) {
            if (id >= 0 && id <= 3) currentPreset = id;
        }

        public static int getShape() {
            return shape[currentPreset];
        }

        public static float getBaseSize() {
            return baseSize[currentPreset];
        }


        public static int[] getScale() {
            return nScale[currentPreset];
        }

        public static int[] getScaleV() {
            return nScaleV[currentPreset];
        }

        public static int getZScale() {
            return zScale[currentPreset];
        }

        public static int getZScaleV() {
            return zScaleV[currentPreset];
        }

        public static int getLevels() {
            return levels[currentPreset];
        }

        public static float getRatio() {
            return ratio[currentPreset];
        }

        public static float getRatioPower() {
            return ratioPower[currentPreset];
        }

        public static int getLobes() {
            return lobes[currentPreset];
        }

        public static float getLobeDepth() {
            return lobeDepth[currentPreset];
        }

        public static float getFlare() {
            return flare[currentPreset];
        }

        public static float[] getNLength() {
            return nLength[currentPreset];
        }

        public static float[] getNLengthV() {
            return nLengthV[currentPreset];
        }

        public static float[] getNTaper() {
            return nTaper[currentPreset];
        }

        public static int getBaseSplits() {
            return baseSplits[currentPreset];
        }

        public static float[] getNSegSplits() {
            return nSegSplits[currentPreset];
        }

        public static float[] getNSplitAngle() {
            return nSplitAngle[currentPreset];
        }

        public static float[] getNSplitAngleV() {
            return nSplitAngleV[currentPreset];
        }

        public static float[] getNCurveRes() {
            return nCurveRes[currentPreset];
        }

        public static float[] getNCurve() {
            return nCurve[currentPreset];
        }

        public static float[] getNCurveBack() {
            return nCurveBack[currentPreset];
        }

        public static float[] getNCurveV() {
            return nCurveV[currentPreset];
        }

        public static float[] getNDownAngle() {
            return nDownAngle[currentPreset];
        }

        public static float[] getNDownAngleV() {
            return nDownAngleV[currentPreset];
        }

        public static float[] getNRotate() {
            return nRotate[currentPreset];
        }

        public static float[] getNRotateV() {
            return nRotateV[currentPreset];
        }

        public static float[] getNBranches() {
            return nBranches[currentPreset];
        }
    }
}