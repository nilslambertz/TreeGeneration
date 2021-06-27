namespace DefaultNamespace
{
    public class PresetParameters
    {
        public static void setPreset(int id)
        {
            if (id >= 0 && id <= 3)
            {
                currentPreset = id;
            }
        }

        /*
         * 0 = Quaking Aspen
         * 1 = Black Tupelo
         * 2 = Weeping Willow
         * 3 = CA Black Oak
         */
        private static int currentPreset;

        /* Shape */
        
        private static int[] shape = {7, 4, 3, 2};

        public static int getShape()
        {
            return shape[currentPreset];
        }
        
        /* BaseSize */
        
        private static float[] baseSize = {0.4f, 0.2f, 0.05f, 0.05f};
        
        public static float getBaseSize()
        {
            return baseSize[currentPreset];
        }
        
        /* nScale */

        private static int[][] nScale = {
            new[] {13, 1},
            new[] {23, 1},
            new[] {15, 1},
            new[] {10, 1}
        };
        
        
        public static int[] getScale()
        {
            return nScale[currentPreset];
        }
        
        private static int[][] nScaleV = {
            new[] {2, 0},
            new[] {5, 0},
            new[] {5, 0},
            new[] {10, 0}
        };
        
        public static int[] getScaleV()
        {
            return nScaleV[currentPreset];
        }
        
        /* zScale */

        private static int[] zScale = {1, 1, 1, 1};
        
        public static int getZScale()
        {
            return zScale[currentPreset];
        }
        
        private static int[] zScaleV = {0, 0, 0, 0};
        
        public static int getZScaleV()
        {
            return zScaleV[currentPreset];
        }
        
        /* Levels */

        private static int[] levels = {3, 4, 4, 3};
        
        public static int getLevels()
        {
            return levels[currentPreset];
        }
        
        /* Ratio */

        private static float[] ratio = {0.015f, 0.015f, 0.03f, 0.018f};
        
        public static float getRatio()
        {
            return ratio[currentPreset];
        }

        private static float[] ratioPower = {1.2f, 1.3f, 2f, 1.3f};
        
        public static float getRatioPower()
        {
            return ratioPower[currentPreset];
        }
        
        /* Lobes */

        private static int[] lobes = {5, 3, 9, 5};
        
        public static int getLobes()
        {
            return lobes[currentPreset];
        }
        
        private static float[] lobeDepth = {0.07f, 0.1f, 0.03f, 0.1f};
        
        public static float getLobeDepth()
        {
            return lobeDepth[currentPreset];
        }
        
        /* Flare */

        private static float[] flare = {0.6f, 1f, 0.75f, 1.3f};
            
        public static float getFlare()
        {
            return flare[currentPreset];
        }
        
        /* nLength */

        private static float[][] nLength = {
            new[] {1f, 0.3f, 0.6f, 0f},
            new[] {1f, 0.3f, 0.6f, 0.4f},
            new[] {0.8f, 0.5f, 1.5f, 0.1f},
            new[] {1f, 0.8f, 0.2f, 0f}
        }; 
        
        public static float[] getNLength()
        {
            return nLength[currentPreset];
        }
        
        private static float[][] nLengthV = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0.05f, 0.1f, 0f},
            new[] {0f, 0.1f, 0f, 0f},
            new[] {0f, 0.1f, 0.05f, 0f},
        };
        
        public static float[] getNLengthV()
        {
            return nLengthV[currentPreset];
        }
        
        /* nTaper */
        
        private static float[][] nTaper = {
            new[] {1f, 1f, 1f, 1f},
            new[] {1.1f, 1f, 1f, 1f},
            new[] {1f, 1f, 1f, 1f},
            new[] {0.95f, 1f, 1f, 1f},
        };

        public static float[] getNTaper()
        {
            return nTaper[currentPreset];
        }
        
        /* Splits */

        private static int[] baseSplits = {0, 0, 2, 2};
        
        public static int getBaseSplits()
        {
            return baseSplits[currentPreset];
        }
        
        private static float[][] nSegSplits = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0.1f, 0.2f, 0.2f, 0f},
            new[] {0.4f, 0.2f, 0.1f, 0f}
        };
        
        public static float[] getNSegSplits()
        {
            return nSegSplits[currentPreset];
        }
        
        /* nSplitAngle */
        
        private static float[][] nSplitAngle = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {3f, 30f, 45f, 0f},
            new[] {10f, 10f, 10f, 0f}
        };
        
        public static float[] getNSplitAngle()
        {
            return nSplitAngle[currentPreset];
        }
        
        private static float[][] nSplitAngleV = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 10f, 20f, 0f},
            new[] {0f, 10f, 10f, 0f}
        };
        
        public static float[] getNSplitAngleV()
        {
            return nSplitAngleV[currentPreset];
        }
        
        /* nCurve */
        
        private static float[][] nCurveRes = {
            new[] {3f, 5f, 3f, 1f},
            new[] {10f, 10f, 10f, 1f},
            new[] {8f, 16f, 12f, 1f},
            new[] {8f, 10f, 3f, 1f}
        };
        
        public static float[] getNCurveRes()
        {
            return nCurveRes[currentPreset];
        }
        
        private static float[][] nCurve = {
            new[] {0f, -40f, -40f, 0f},
            new[] {0f, 0f, -10f, 0f},
            new[] {0f, 40f, 0f, 0f},
            new[] {0f, 40f, 0f, 0f}
        };
        
        public static float[] getNCurve()
        {
            return nCurve[currentPreset];
        }
        
        private static float[][] nCurveV = {
            new[] {20f, 50f, 75f, 0f},
            new[] {40f, 90f, 150f, 0f},
            new[] {120f, 90f, 0f, 0f},
            new[] {90f, 150f, -30f, 0f}
        };
        
        public static float[] getNCurveV()
        {
            return nCurveV[currentPreset];
        }
        
        /* nDownAngle */
        
        private static float[][] nDownAngle = {
            new[] {0f, 60f, 45f, 45f},
            new[] {0f, 60f, 30f, 45f},
            new[] {0f, 20f, 30f, 45f},
            new[] {0f, 30f, 45f, 45f}
        };
        
        public static float[] getNDownAngle()
        {
            return nDownAngle[currentPreset];
        }
        
        private static float[][] nDownAngleV = {
            new[] {0f, -50f, 10f, 10f},
            new[] {0f, -40f, 10f, 10f},
            new[] {0f, 10f, 10f, 10f},
            new[] {0f, -30f, 10f, 10f}
        };
        
        public static float[] getNDownAngleV()
        {
            return nDownAngleV[currentPreset];
        }
        
        /* nRotate */
        
        private static float[][] nRotate = {
            new[] {0f, 140f, 140f, 77f},
            new[] {0f, 140f, 140f, 140f},
            new[] {0f, -120f, -120f, 140f},
            new[] {0f, 80f, 140f, 140f}
        };
        
        public static float[] getNRotate()
        {
            return nRotate[currentPreset];
        }
        
        private static float[][] nRotateV = {
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 0f, 0f, 0f},
            new[] {0f, 30f, 30f, 0f},
            new[] {0f, 0f, 0f, 0f}
        };
        
        public static float[] getNRotateV()
        {
            return nRotateV[currentPreset];
        }
        
        /* nBranches */
        
        private static float[][] nBranches = {
            new[] {0f, 50f, 30f, 10f},
            new[] {0f, 50f, 25f, 12f},
            new[] {0f, 25f, 10f, 300f},
            new[] {0f, 40f, 120f, 0f}
        };
        
        public static float[] getNBranches()
        {
            return nBranches[currentPreset];
        }

    }
}