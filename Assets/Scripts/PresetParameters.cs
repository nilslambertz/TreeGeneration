namespace DefaultNamespace
{
    public class PresetParameters
    {
        /*
         * 0 = Quaking Aspen
         * 1 = Black Tupelo
         * 2 = Weeping Willow
         * 3 = CA Black Oak
         */
        private int currentPreset = 0;

        private int[] shape = new[] {7, 4, 3, 2};
        private float[] baseSize = new[] {0.4f, 0.2f, 0.05f, 0.05f};

        private int[][] scale = new[]
        {
            new[] {13, 1},
            new[] {23, 1},
            new[] {15, 1},
            new[] {10, 1}
        };
        
        private int[][] scaleV = new[]
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

        private float[][] length = new[]
        {
            new[] {1f, 0.3f, 0.6f, 0f},
            new[] {1f, 0.3f, 0.6f, 0.4f},
            new[] {0.8f, 0.5f, 1.5f, 0.1f},
            new[] {1f, 0.8f, 0.2f, 0f}
        };
        
        private float[][] lengthV = new[]
        {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0.05f, 0.1f, 0f},
            new[] {0f, 0.1f, 0f, 0f},
            new[] {0f, 0.1f, 0.05f, 0f},
        };
        
        private float[][] taper = new[]
        {
            new[] {1f, 1f, 1f, 1f},
            new[] {1.1f, 1f, 1f, 1f},
            new[] {1f, 1f, 1f, 1f},
            new[] {0.95f, 1f, 1f, 1f},
        };

        private int[] baseSplits = new[] {0, 0, 2, 2};
    }
}