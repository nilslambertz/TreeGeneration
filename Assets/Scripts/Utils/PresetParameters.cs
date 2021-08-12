using System.Collections.Generic;

namespace DefaultNamespace {
    public class PresetParameters {

        private static List<TreePreset> presetList = new List<TreePreset>();

        public PresetParameters() {
            TreePreset quakingAspen = new TreePreset("Quaking Aspen",
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
            
            TreePreset blackTupelo = new TreePreset("Black Tupelo",
                4, 0.2f, 23, 5, 1, 0,
                4, 0.015f, 1.3f, 3, 0.1f,
                1f, 1, 0, new []{1, 0.3f, 0.6f, 0.4f},
                new []{0f, 0.05f, 0.1f, 0}, new []{1.1f, 1, 1, 1},
                0, new []{0f, 0, 0, 0}, new []{0f, 0, 0, 0},
                new []{0f, 0, 0, 0}, new []{10f, 10, 10, 1},
                new []{0f, 0, -10, 0}, new []{0f, 0, 0, 0},
                new []{40f, 90, 150, 0}, new []{0f, 60, 30, 45},
                new []{0f, -40, 10, 10}, new []{0f, 140, 140, 140},
                new []{0f, 0, 0, 0}, new []{0, 50, 25, 12}
            );
            
            TreePreset weepingWillow = new TreePreset("Weeping Willow",
                3, 0.05f, 15, 5, 1, 0,
                4, 0.03f, 2f, 9, 0.03f,
                0.75f, 1, 0, new []{0.8f, 0.5f, 1.5f, 0.1f},
                new []{0f, 0.1f, 0f, 0}, new []{1f, 1, 1, 1},
                2, new []{0.1f, 0.2f, 0.2f, 0}, new []{3f, 30, 45, 0},
                new []{0f, 10, 20, 0}, new []{8f, 16, 12, 1},
                new []{0f, 40, 0, 0}, new []{20f, 80, 0, 0},
                new []{120f, 90, 0, 0}, new []{0f, 20, 30, 20},
                new []{0f, 10, 10, 10}, new []{0f, -120, -120, 140},
                new []{0f, 30, 30, 0}, new []{0, 25, 10, 300}
            );
            
            TreePreset blackOak = new TreePreset("Black Oak",
                2, 0.05f, 10, 10, 1, 0,
                3, 0.018f, 1.3f, 5, 0.1f,
                1.2f, 1, 0, new []{1f, 0.8f, 0.2f, 0.4f},
                new []{0f, 0.1f, 0.05f, 0}, new []{0.95f, 1, 1, 1},
                2, new []{0.4f, 0.2f, 0.1f, 0}, new []{10f, 10, 10, 0},
                new []{0f, 10, 10, 0}, new []{8f, 10, 3, 1},
                new []{0f, 40, 0, 0}, new []{0f, -70, 0, 0},
                new []{90f, 150, -30, 0}, new []{0f, 30, 45, 45},
                new []{0f, -30, 10, 10}, new []{0f, 80, 140, 140},
                new []{0f, 0, 0, 0}, new []{0, 40, 120, 0}
            );
            
            presetList.Add(quakingAspen);
            presetList.Add(blackTupelo);
            presetList.Add(weepingWillow);
            presetList.Add(blackOak);
        }

        public TreePreset getPreset(int index) {
            if (index < presetList.Count) {
                return presetList[index];
            }

            return null;
        }
    }
}