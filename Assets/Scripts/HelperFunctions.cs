using System;

namespace DefaultNamespace
{
    public class HelperFunctions {

        /* Scale */
        public static float getScale_tree(float scale, float scaleV) {
            return scale + scaleV;
        }

        /* Length */
        public static float getLength_base(float baseSize, float scale_tree) {
            return baseSize * scale_tree;
        }

        public static float getLength_trunk(float zeroLength, float zeroLengthV, float scale_tree) {
            return (zeroLength + zeroLengthV) * scale_tree;
        }

        public static float getLength_child_max(float nLength, float nLengthV) {
            return nLength + nLengthV;
        }

        /* Radius */
        public static float getRadius_trunk(float length_trunk, float ratio, float zeroScale) {
            return length_trunk * ratio * zeroScale;
        }

        public static float getRadius_child(float radius_parent_bottom, float radius_parent_top, float length_child, float length_parent, float offset_child,
            float ratioPower) {
            float radius = radius_parent_bottom - (radius_parent_bottom - radius_parent_top) * (offset_child / length_parent);
            return radius * (float) Math.Pow(length_child / length_parent, ratioPower);
        }

        public static float getTopRadius(float radius_bottom, float taper) {
            return radius_bottom * (taper <= 1 && taper >= 0 ? (1 - taper) : 1);
        }
        
        /* Stems */
        public static int getStems_base(float stems_max, float length_child, float length_parent, float length_child_max) {
            return (int) (stems_max * (0.2f + 0.8f * (length_child / length_parent) / length_child_max));
        }

        public static int getStems_iteration(float stems_max, float offset_child, float length_parent) {
            return (int) (stems_max * (1.0f - 0.5f * offset_child / length_parent));
        }
        
        /* DownAngle */

        public static float getDownAnglePositive(float nDownAngle, float nDownAngleV) {
            return nDownAngle + nDownAngleV;
        }
        public static float getDownAngleNegative(float nDownAngle, float nDownAngleV, float length_parent, float offset_child, float length_base) {
            return nDownAngle + (nDownAngleV *
                                 (1 - 2 * ShapeRatio(0,
                                     (length_parent - offset_child) / (length_parent - length_base))));
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
