using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProgramPlaner2
{
    static class ColorChooser
    {
        public static Color ChooseSectionBackgroundColor(SectionType sectionType)
        {
            int blueValue = (int)sectionType;
            return Color.FromArgb(50 * blueValue, 255 - (35 * blueValue), 255 - (35 * blueValue));
        }
    }
}
