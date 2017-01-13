using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProgramPlaner2
{
    [Serializable]
    public class SectionClass
    {
        public const int START_BORDER = 50;
        public String SectionName { get; set; }
        public Rectangle Rectangle;
        public Boolean VariablesMinimized { get; set; }
        public Boolean MethodsMinimized { get; set; }
        private Boolean dragMode;
        public Boolean DragMode
        {
            get
            {
                return dragMode;
            }
            set
            {
                dragMode = value;
                if (DragModeChanged != null)
                    DragModeChanged();
            }
        }

        private Boolean isFocused;
        public Boolean IsFocused
        {
            get
            {
                return isFocused;
            }
            set
            {
                isFocused = value;
                if (FocusChanged != null)
                    FocusChanged();
            }
        }
        public SectionType SectionType { get; set; }

        public List<Connection> Connections { get; private set; }
        public List<Variable> Variables { get; private set; }
        public List<String> Methods { get; private set; }

        [field: NonSerialized]
        public event ValueChangedDelegate FocusChanged;
        [field: NonSerialized]
        public event ValueChangedDelegate DragModeChanged;

        public SectionClass()
        {
            this.Connections = new List<Connection>();
            this.Rectangle = new Rectangle(START_BORDER, START_BORDER, Section.CONTROL_WIDTH, Section.CONTROL_HEIGHT);
            this.DragMode = true;
            Connections = new List<Connection>();
            Variables = new List<Variable>();
            Methods = new List<string>();
        }
    }
}
