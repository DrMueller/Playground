using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace ProgramPlaner2
{
    /// <summary>
    /// Enthält alle Daten, welche gespeichert und geladen werden 
    /// </summary>
    /// 
    [Serializable]
    class SaveObject
    {
        public List<SectionClass> SectionClasses { get; private set; }
        public FormWindowState FormWindowState { get; private set; }
        public Size FormSize { get; private set; }
        public Point FormLocation { get; private set; }

        public SaveObject(List<SectionClass> sectionClasses, FormWindowState formWindowState, Size formSize, Point formLocation)
        {
            this.SectionClasses = sectionClasses;
            this.FormWindowState = formWindowState;
            this.FormSize = formSize;
            this.FormLocation = formLocation;
        }
    }
}
