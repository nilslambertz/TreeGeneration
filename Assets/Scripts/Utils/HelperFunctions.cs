using System;

namespace DefaultNamespace {
    public class HelperFunctions {

        /* Scale */
        /// <summary>
        /// Calculates scale of the tree (Page 4 in the paper)
        /// </summary>
        /// <param name="scale">scale</param>
        /// <param name="scaleV">scaleV</param>
        /// <returns>scale of the tree</returns>
        public static float getScale_tree(float scale, float scaleV) {
            return scale + scaleV;
        }

        /* Length */
        /// <summary>
        /// Calculates bare area at the start of the stem (Page 4 in the paper)
        /// </summary>
        /// <param name="baseSize">baseSize</param>
        /// <param name="scale_tree">scale_tree</param>
        /// <returns>length of bare area at the start of the stem</returns>
        public static float getLength_base(float baseSize, float scale_tree) {
            return baseSize * scale_tree;
        }

        /// <summary>
        /// Calculates length of the stem/trunk (Page 4 in the paper)
        /// </summary>
        /// <param name="zeroLength">0Length</param>
        /// <param name="zeroLengthV">0LengthV</param>
        /// <param name="scale_tree">scale_tree</param>
        /// <returns>length of the stem/trunk</returns>
        public static float getLength_trunk(float zeroLength, float zeroLengthV, float scale_tree) {
            return (zeroLength + zeroLengthV) * scale_tree;
        }

        /// <summary>
        /// Calculates max-length of a child-branch (Page 3 in the paper)
        /// </summary>
        /// <param name="nLength">nLength</param>
        /// <param name="nLengthV">nLengthV</param>
        /// <returns>max-length of child-branch</returns>
        public static float getLength_child_max(float nLength, float nLengthV) {
            return nLength + nLengthV;
        }

        /// <summary>
        /// Calculates length of a child of stem (Page 3 in the paper)
        /// </summary>
        /// <param name="shape">shape</param>
        /// <param name="length_trunk">length_trunk</param>
        /// <param name="length_child_max">length_child_max</param>
        /// <param name="offset_child">offset_child</param>
        /// <param name="length_base">length_base</param>
        /// <returns>length of child</returns>
        public static float getLength_child_base(int shape, float length_trunk, float length_child_max, float offset_child,
            float length_base) {
            return length_trunk * length_child_max *
                   ShapeRatio(shape, (length_trunk - offset_child) / (length_trunk - length_base));
        }

        /// <summary>
        /// Calculates length of a child at any other depth (Page 4 in the paper)
        /// </summary>
        /// <param name="length_child_max">length_child_max</param>
        /// <param name="length_current">length_current</param>
        /// <param name="offset_child">offset_child</param>
        /// <returns>length of child</returns>
        public static float getLength_child_iteration(float length_child_max, float length_current, float offset_child) {
            return length_child_max * (length_current - 0.6f * offset_child);
        }

        /* Radius */
        /// <summary>
        /// Calculates radius of the stem/trunk (Page 4 in the paper)
        /// </summary>
        /// <param name="length_trunk">length_trunk</param>
        /// <param name="ratio">ratio</param>
        /// <param name="zeroScale">0Scale</param>
        /// <returns>radius of the stem/trunk</returns>
        public static float getRadius_trunk(float length_trunk, float ratio, float zeroScale) {
            return length_trunk * ratio * zeroScale;
        }

        /// <summary>
        /// Calculates radius of a child-branch (Page 4 in the paper)
        /// </summary>
        /// <param name="radius_parent_bottom">bottom radius of the parent</param>
        /// <param name="radius_parent_top">top radius of the parent</param>
        /// <param name="length_child">length_child</param>
        /// <param name="length_parent">length_parent</param>
        /// <param name="offset_child">offset along the parent</param>
        /// <param name="ratioPower">RatioPower</param>
        /// <returns>radius of child-branch</returns>
        public static float getRadius_child(float radius_parent_bottom, float radius_parent_top, float length_child, float length_parent, float offset_child,
            float ratioPower) {
            float radius = radius_parent_bottom - (radius_parent_bottom - radius_parent_top) * (offset_child / length_parent);
            return radius * (float)Math.Pow(length_child / length_parent, ratioPower);
        }

        /// <summary>
        /// Calculates topRadius of a branch (Page 3 in the paper)
        /// </summary>
        /// <param name="radius_bottom">radius_bottom</param>
        /// <param name="taper">Taper</param>
        /// <returns>topRadius of branch</returns>
        public static float getTopRadius(float radius_bottom, float taper) {
            return radius_bottom * (taper <= 1 && taper >= 0 ? (1 - taper) : 0.5f);
        }

        /* Stems */
        /// <summary>
        /// Calculates number of children, that every branch at the first level will have (grandchildren of stem) (Page 3 in the paper)
        /// </summary>
        /// <param name="stems_max">stems_max</param>
        /// <param name="length_child">length_child</param>
        /// <param name="length_parent">length_parent</param>
        /// <param name="length_child_max">length_child_max</param>
        /// <returns>number of grandchildren of stem</returns>
        public static int getStems_base(float stems_max, float length_child, float length_parent, float length_child_max) {
            return (int)(stems_max * (0.2f + 0.8f * (length_child / length_parent) / length_child_max));
        }

        /// <summary>
        /// Calculates number of grandchildren of a branch at any other depth in the tree (Page 3 in the paper)
        /// </summary>
        /// <param name="stems_max">stems_max</param>
        /// <param name="offset_child">offset_child</param>
        /// <param name="length_parent">length_parent</param>
        /// <returns>number of grandchildren of branch</returns>
        public static int getStems_iteration(float stems_max, float offset_child, float length_parent) {
            return (int)(stems_max * (1.0f - 0.5f * offset_child / length_parent));
        }

        /* DownAngle */
        /// <summary>
        /// Calculates the downAngle, if nDownAngleV is positive (Page 4 in the paper)
        /// </summary>
        /// <param name="nDownAngle">nDownAngle</param>
        /// <param name="nDownAngleV">nDownAngleV</param>
        /// <returns>downAngle</returns>
        public static float getDownAnglePositive(float nDownAngle, float nDownAngleV) {
            return nDownAngle + nDownAngleV;
        }

        /// <summary>
        /// Calculates the downAngle, if nDownAngleV is negative (Page 4 in the paper)
        /// </summary>
        /// <param name="nDownAngle">nDownAngle</param>
        /// <param name="nDownAngleV">nDownAngleV</param>
        /// <param name="length_parent">length_parent</param>
        /// <param name="offset_child">offset_child</param>
        /// <param name="length_base">length_base</param>
        /// <returns>downAngle</returns>
        public static float getDownAngleNegative(float nDownAngle, float nDownAngleV, float length_parent, float offset_child, float length_base) {
            return nDownAngle + (nDownAngleV *
                                 (1 - 2 * ShapeRatio(0,
                                     (length_parent - offset_child) / (length_parent - length_base))));
        }

        /// <summary>
        /// ShaperRatio-function (Page 3 in the paper)
        /// </summary>
        /// <param name="currentShape">shape</param>
        /// <param name="ratioValue">ratio</param>
        /// <returns>ShapeRatio</returns>
        public static float ShapeRatio(int currentShape, float ratioValue) {
            switch (currentShape) {
                case 0: return 0.2f + 0.8f * ratioValue;
                case 1: return 0.2f + 0.8f * (float)Math.Sin(Math.PI * ratioValue);
                case 2: return 0.2f + 0.8f * (float)Math.Sin(0.5 * Math.PI * ratioValue);
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
