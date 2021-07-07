using System;

namespace DefaultNamespace
{
    public class HelperFunctions {
        public static float getStems_base(float stems_max, float length_child, float length_parent, float length_child_max) {
            return stems_max * (0.2f + 0.8f * (length_child / length_parent) / length_child_max);
        }

        public static float getStems_iteration(float stems_max, float offset_child, float length_parent) {
            return stems_max * (1.0f - 0.5f * offset_child / length_parent);
        }

        public static float getLength_child_base(int shape, float length_trunk, float length_child_max, float offset_child,
            float length_base) {
            return length_trunk * length_child_max *
                   ShapeRatio(shape, (length_trunk - offset_child) / (length_trunk - length_base));
        }

        public static float getLength_child_iteration(float length_child_max, float length_current, float offset_child) {
            return length_child_max * (length_current - 0.6f * offset_child);
        }
        
        public static float ShapeRatio(int currentShape, float ratioValue) {
            switch (currentShape) {
                case 0: return 0.2f + 0.8f * ratioValue;
                case 1: return 0.2f + 0.8f * (float) Math.Sin(Math.PI * ratioValue);
                case 2: return 0.2f + 0.8f * (float) Math.Sin(0.5 * Math.PI * ratioValue);
                case 3: return 1.0f;
                case 4: return 0.5f + 0.5f * ratioValue;
                case 5: {
                    if (ratioValue <= 0.7f) return ratioValue / 0.7f;

                    return (1.0f - ratioValue) / 0.3f;
                }
                case 6: return 1.0f - 0.8f * ratioValue;
                case 7: {
                    if (ratioValue <= 0.7f) return 0.5f + 0.5f * ratioValue / 0.7f;

                    return 0.5f + 0.5f * (1.0f - ratioValue) / 0.3f;
                }
            }

            return -1f;
        }
    }
}
