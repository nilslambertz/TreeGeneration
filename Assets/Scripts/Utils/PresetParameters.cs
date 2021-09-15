using System.Collections.Generic;

namespace DefaultNamespace
{
    public class PresetParameters
    {

        public enum PresetParameterIndex
        {
            baseSize,
            zeroLength,
            oneLength,
            twoLength,
            oneBranches,
            twoBranches
        }

        public struct PresetParameterInfo
        {
            public PresetParameterInfo(string n, string d, float minV, float maxV, float iV, System.TypeCode dT)
            {
                name = n;
                description = d;
                minValue = minV;
                maxValue = maxV;
                initialValue = iV;
                dateType = dT;
            }

            public string name;
            public string description;
            public float minValue;
            public float maxValue;
            public float initialValue;
            public System.TypeCode dateType;
        }

        private static PresetParameterInfo[] presetParameterList;

        private static List<TreePreset> presetList;

        private static int customPresetId = 0;

        // Returns PresetParameterInfo-list
        public static PresetParameterInfo[] GetPresetParameterInfo()
        {
            if (presetParameterList == null)
            {
                setDefaultPresetParameterInfos();
            }
            return presetParameterList;
        }

        // returns parameters of current preset
        public static float[] getCurrentPresetParameterValues()
        {
            float[] values = new float[6];
            TreePreset p = SharedValues.getCurrentPreset();
            values[(int)PresetParameterIndex.baseSize] = p.baseSize;
            values[(int)PresetParameterIndex.oneBranches] = p.nBranches[1];
            values[(int)PresetParameterIndex.twoBranches] = p.nBranches[2];
            values[(int)PresetParameterIndex.zeroLength] = p.nLength[0];
            values[(int)PresetParameterIndex.oneLength] = p.nLength[1];
            values[(int)PresetParameterIndex.twoLength] = p.nLength[2];
            return values;
        }

        // Sets default values for parameters (same as "Black Tupelo")
        private static void setDefaultPresetParameterInfos()
        {
            presetParameterList = new PresetParameterInfo[6];
            presetParameterList[((int)PresetParameterIndex.baseSize)] = new PresetParameterInfo("Base Size", "Branchless area at the start of the stem", 0.01f, 0.5f, 0.2f, System.TypeCode.Double);
            presetParameterList[((int)PresetParameterIndex.oneBranches)] = new PresetParameterInfo("Branches (1)", "Number of branches at depth 1 (stem)", 1f, 50f, 20f, System.TypeCode.Int32);
            presetParameterList[((int)PresetParameterIndex.twoBranches)] = new PresetParameterInfo("Branches (2)", "Number of branches at depth 2", 1f, 50f, 20f, System.TypeCode.Int32);
            presetParameterList[((int)PresetParameterIndex.zeroLength)] = new PresetParameterInfo("Length (1)", "Length of stem", 0.1f, 1f, 1f, System.TypeCode.Double);
            presetParameterList[((int)PresetParameterIndex.oneLength)] = new PresetParameterInfo("Length (2)", "Length of branches at depth 2", 0.1f, 1f, 1f, System.TypeCode.Double);
            presetParameterList[((int)PresetParameterIndex.twoLength)] = new PresetParameterInfo("Length (3)", "Length of branches at depth 3", 0.1f, 1f, 1f, System.TypeCode.Double);
        }

        // Initialises given presets
        public static void initialisePresets()
        {
            setDefaultPresetParameterInfos();
            presetList = new List<TreePreset>();

            TreePreset quakingAspen = new TreePreset("Quaking Aspen",
                7, 0.4f, 13, 3, 1, 0,
                3, 0.015f, 1.2f, 5, 0.07f,
                0.6f, 1, 0, new[] { 1, 0.3f, 0.6f, 0 },
                new[] { 0f, 0, 0, 0 }, new[] { 1f, 1, 1, 1 },
                0, new[] { 0f, 0, 0, 0 }, new[] { 0f, 0, 0, 0 },
                new[] { 0f, 0, 0, 0 }, new[] { 3f, 5, 3, 1 },
                new[] { 0f, -40, -40, 0 }, new[] { 0f, 0, 0, 0 },
                new[] { 20f, 50, 75, 0 }, new[] { 0f, 60, 45, 45 },
                new[] { 0f, -50, 10, 10 }, new[] { 0f, 140, 140, 77 },
                new[] { 0f, 0, 0, 0 }, new[] { 0, 50, 30, 10 }
            );

            TreePreset blackTupelo = new TreePreset("Black Tupelo",
                4, 0.2f, 23, 5, 1, 0,
                4, 0.015f, 1.3f, 3, 0.1f,
                1f, 1, 0, new[] { 1, 0.3f, 0.6f, 0.4f },
                new[] { 0f, 0.05f, 0.1f, 0 }, new[] { 1.1f, 1, 1, 1 },
                0, new[] { 0f, 0, 0, 0 }, new[] { 0f, 0, 0, 0 },
                new[] { 0f, 0, 0, 0 }, new[] { 10f, 10, 10, 1 },
                new[] { 0f, 0, -10, 0 }, new[] { 0f, 0, 0, 0 },
                new[] { 40f, 90, 150, 0 }, new[] { 0f, 60, 30, 45 },
                new[] { 0f, -40, 10, 10 }, new[] { 0f, 140, 140, 140 },
                new[] { 0f, 0, 0, 0 }, new[] { 0, 50, 25, 12 }
            );

            TreePreset weepingWillow = new TreePreset("Weeping Willow",
                3, 0.05f, 15, 5, 1, 0,
                4, 0.03f, 2f, 9, 0.03f,
                0.75f, 1, 0, new[] { 0.8f, 0.5f, 1.5f, 0.1f },
                new[] { 0f, 0.1f, 0f, 0 }, new[] { 1f, 1, 1, 1 },
                2, new[] { 0.1f, 0.2f, 0.2f, 0 }, new[] { 3f, 30, 45, 0 },
                new[] { 0f, 10, 20, 0 }, new[] { 8f, 16, 12, 1 },
                new[] { 0f, 40, 0, 0 }, new[] { 20f, 80, 0, 0 },
                new[] { 120f, 90, 0, 0 }, new[] { 0f, 20, 30, 20 },
                new[] { 0f, 10, 10, 10 }, new[] { 0f, -120, -120, 140 },
                new[] { 0f, 30, 30, 0 }, new[] { 0, 25, 10, 300 }
            );

            TreePreset blackOak = new TreePreset("Black Oak",
                2, 0.05f, 10, 10, 1, 0,
                3, 0.018f, 1.3f, 5, 0.1f,
                1.2f, 1, 0, new[] { 1f, 0.8f, 0.2f, 0.4f },
                new[] { 0f, 0.1f, 0.05f, 0 }, new[] { 0.95f, 1, 1, 1 },
                2, new[] { 0.4f, 0.2f, 0.1f, 0 }, new[] { 10f, 10, 10, 0 },
                new[] { 0f, 10, 10, 0 }, new[] { 8f, 10, 3, 1 },
                new[] { 0f, 40, 0, 0 }, new[] { 0f, -70, 0, 0 },
                new[] { 90f, 150, -30, 0 }, new[] { 0f, 30, 45, 45 },
                new[] { 0f, -30, 10, 10 }, new[] { 0f, 80, 140, 140 },
                new[] { 0f, 0, 0, 0 }, new[] { 0, 40, 120, 0 }
            );

            presetList.Add(quakingAspen);
            presetList.Add(blackTupelo);
            presetList.Add(weepingWillow);
            presetList.Add(blackOak);
        }

        public static TreePreset getPreset(int index)
        {
            if (presetList == null)
            {
                initialisePresets();
            }
            if (index < presetList.Count)
            {
                return presetList[index];
            }

            return null;
        }

        // Returns preset-list
        public static List<TreePreset> getPresetList()
        {
            if (presetList == null)
            {
                initialisePresets();
            }
            return presetList;
        }

        // Creates new preset
        public static void createCustomTreePreset()
        {
            TreePreset custom = SharedValues.getCurrentPreset().getCopy("Preset #" + (customPresetId++));
            custom.basedOn = SharedValues.getCurrentPreset().name;

            presetList.Add(custom);

            SharedValues.setPreset(presetList.Count - 1);
        }

        // Changes value of a preset
        public static void changePresetValue(int id, int index, float newValue)
        {

            if (id < presetList.Count)
            {
                TreePreset tree = presetList[id];
                if (index == (int)PresetParameterIndex.baseSize)
                {
                    tree.baseSize = newValue;
                }
                if (index == (int)PresetParameterIndex.oneBranches)
                {
                    tree.nBranches[1] = (int)newValue;
                }
                if (index == (int)PresetParameterIndex.twoBranches)
                {
                    tree.nBranches[2] = (int)newValue;
                }
                if (index == (int)PresetParameterIndex.zeroLength)
                {
                    tree.nLength[0] = newValue;
                }
                if (index == (int)PresetParameterIndex.oneLength)
                {
                    tree.nLength[1] = newValue;
                }
                if (index == (int)PresetParameterIndex.twoLength)
                {
                    tree.nLength[2] = newValue;
                }
            }
        }
    }
}