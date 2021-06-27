using System;

namespace DefaultNamespace
{
    public class PresetParameters
    {
        public static void setPreset(int id)
        {
            if (id >= 0 && id <= 3)
            {
                presetId = id;
            }
        }

        private static int presetId = 0;

        /*
         * 0 = Quaking Aspen
         * 1 = Black Tupelo
         * 2 = Weeping Willow
         * 3 = CA Black Oak
         */
        private int currentPreset = 0;

        private int[] shape = new[] {7, 4, 3, 2};
        private float[] baseSize = new[] {0.4f, 0.2f, 0.05f, 0.05f};

        private int[][] nScale = new[]
        {
            new[] {13, 1},
            new[] {23, 1},
            new[] {15, 1},
            new[] {10, 1}
        };
        
        private int[][] nScaleV = new[]
        {
            new[] {2, 0},
            new[] {5, 0},
            new[] {5, 0},
            new[] {10, 0}
        };

        private int[] zScale = new[] {1, 1, 1, 1};
        
        private int[] zScaleV = new[] {0, 0, 0, 0};

        private int[] levels = new[] {3, 4, 4, 3};

        private float[] ratio = new [] {0.015f, 0.015f, 0.03f, 0.018f};

        private float[] ratioPower = new[] {1.2f, 1.3f, 2f, 1.3f};

        private int[] lobes = new[] {5, 3, 9, 5};
        
        private float[] lobeDepth = new[] {0.07f, 0.1f, 0.03f, 0.1f};
        
        private float[] flare = new[] {0.6f, 1f, 0.75f, 1.3f};

        private float[][] nLength = new[]
        {
            new[] {1f, 0.3f, 0.6f, 0f},
            new[] {1f, 0.3f, 0.6f, 0.4f},
            new[] {0.8f, 0.5f, 1.5f, 0.1f},
            new[] {1f, 0.8f, 0.2f, 0f}
        };
        
        private float[][] nLengthV = new[]
        {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0.05f, 0.1f, 0f},
            new[] {0f, 0.1f, 0f, 0f},
            new[] {0f, 0.1f, 0.05f, 0f},
        };
        
        private float[][] nTaper = new[]
        {
            new[] {1f, 1f, 1f, 1f},
            new[] {1.1f, 1f, 1f, 1f},
            new[] {1f, 1f, 1f, 1f},
            new[] {0.95f, 1f, 1f, 1f},
        };

        private int[] baseSplits = new[] {0, 0, 2, 2};
        
        private float[][] nSegSplits = new[]
        {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0.1f, 0.2f, 0.2f, 0f},
            new[] {0.4f, 0.2f, 0.1f, 0f}
        };
        
        private float[][] nSplitAngle = new[]
        {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {3f, 30f, 45f, 0f},
            new[] {10f, 10f, 10f, 0f}
        };
        
        private float[][] nSplitAngleV = new[]
        {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 10f, 20f, 0f},
            new[] {0f, 10f, 10f, 0f}
        };
        
        private float[][] nCurveRes = new[]
        {
            new[] {3f, 5f, 3f, 1f},
            new[] {10f, 10f, 10f, 1f},
            new[] {8f, 16f, 12f, 1f},
            new[] {8f, 10f, 3f, 1f}
        };
        
        private float[][] nCurve = new[]
        {
            new[] {0f, -40f, -40f, 0f},
            new[] {0f, 0f, -10f, 0f},
            new[] {0f, 40f, 0f, 0f},
            new[] {0f, 40f, 0f, 0f}
        };
        
        private float[][] nCurveV = new[]
        {
            new[] {20f, 50f, 75f, 0f},
            new[] {40f, 90f, 150f, 0f},
            new[] {120f, 90f, 0f, 0f},
            new[] {90f, 150f, -30f, 0f}
        };
        
        private float[][] nDownAngle = new[]
        {
            new[] {0f, 60f, 45f, 45f},
            new[] {0f, 60f, 30f, 45f},
            new[] {0f, 20f, 30f, 45f},
            new[] {0f, 30f, 45f, 45f}
        };
        
        private float[][] nDownAngleV = new[]
        {
            new[] {0f, -50f, 10f, 10f},
            new[] {0f, -40f, 10f, 10f},
            new[] {0f, 10f, 10f, 10f},
            new[] {0f, -30f, 10f, 10f}
        };
        
        private float[][] nRotate = new[]
        {
            new[] {0f, 140f, 140f, 77f},
            new[] {0f, 140f, 140f, 140f},
            new[] {0f, -120f, -120f, 140f},
            new[] {0f, 80f, 140f, 140f}
        };
        
        private float[][] nRotateV = new[]
        {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 30f, 30f, 0f},
            new[] {0f, 0f, 0f, 0f}
        };
        
        private float[][] nBranches = new[]
        {
            new[] {0f, 50f, 30f, 10f},
            new[] {0f, 50f, 25f, 12f},
            new[] {0f, 25f, 10f, 300f},
            new[] {0f, 40f, 120f, 0f}
        };

    }
}