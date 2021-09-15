using System;

namespace DefaultNamespace
{
    public class SharedValues
    {
        private static TreePreset currentPreset;
        private static int defaultPreset = 1;

        public static TreePreset getCurrentPreset()
        {
            if (currentPreset == null)
            {
                currentPreset = PresetParameters.getPreset(defaultPreset);
            }
            return currentPreset;
        }

        public static void setPreset(int id)
        {
            currentPreset = PresetParameters.getPreset(id);
            UIController.setValue(UIDisplay.UIDebugTextEnum.CurrentPreset, currentPreset.name);
        }
    }
}