using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramPlaner2
{
    public partial class Section : UserControl
    {
        private const int LIST_HEIGHT = 120;

        public SectionClass SectionClass { get; private set; }
        public const int CONTROL_WIDTH = 212;
        public const int CONTROL_HEIGHT = 300;

        public static event ValueChangedDelegate RectangleChanged;

        public Section(SectionClass sectionClass)
        {
            InitializeComponent();
            
            sectionClass.FocusChanged += new ValueChangedDelegate(sectionClass_FocusChanged);
            sectionClass.DragModeChanged += new ValueChangedDelegate(sectionClass_DragModeChanged);
            this.SectionClass = sectionClass;
            setBindings();
            this.Location = new Point(sectionClass.Rectangle.X, sectionClass.Rectangle.Y);
            this.chkDrag.Checked = sectionClass.DragMode;

            this.Visible = true;
            this.Size = new Size(CONTROL_WIDTH, CONTROL_HEIGHT);
            this.BackColor = ColorChooser.ChooseSectionBackgroundColor(sectionClass.SectionType);
            this.lsvMethods.Height = LIST_HEIGHT;
            this.lsvVariables.Height = LIST_HEIGHT;
            buildControls();
        }

        void sectionClass_DragModeChanged()
        {
            chkDrag.Checked = SectionClass.DragMode;
        }

        private void buildControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c is ListView)
                {
                    ListView l = (ListView)c;
                    l.MultiSelect = true;
                    l.FullRowSelect = true;
                    l.View = View.Details;
                    l.GridLines = true; 
                }
            }

            lsvMethods.Columns.Add("Methode", lsvMethods.Width - 4);
            lsvMethods.ContextMenu = new System.Windows.Forms.ContextMenu();
            lsvVariables.Columns.Add("Typ", (lsvVariables.Width / 2) -2);
            lsvVariables.Columns.Add("Name", (lsvVariables.Width / 2) - 2);
            lsvVariables.ContextMenu = new ContextMenu();

            MenuItem mnu = new MenuItem("Neue Methode");
            mnu.Name = "mnuMethodNew";
            mnu.Click += new EventHandler(mnu_Click);
            lsvMethods.ContextMenu.MenuItems.Add(mnu);

            mnu = new MenuItem("Methode Löschen");
            mnu.Name = "mnuMethodDelete";
            mnu.Click += new EventHandler(mnu_Click);
            lsvMethods.ContextMenu.MenuItems.Add(mnu);

            mnu = new MenuItem("Neue Variable");
            mnu.Name = "mnuVariableNew";
            mnu.Click += new EventHandler(mnu_Click);
            lsvVariables.ContextMenu.MenuItems.Add(mnu);

            mnu = new MenuItem("Variable Löschen");
            mnu.Name = "mnuVariableDelete";
            mnu.Click += new EventHandler(mnu_Click);
            lsvVariables.ContextMenu.MenuItems.Add(mnu);

            ListViewItem lsv;
            foreach (Variable v in SectionClass.Variables)
            {
                lsv = new ListViewItem(v.VariableType);
                lsv.SubItems.Add(new ListViewItem.ListViewSubItem(lsv, v.VariableName));
                lsv.Tag = v;
                lsvVariables.Items.Add(lsv);
            }

            foreach (String m in SectionClass.Methods)
            {
                lsv = new ListViewItem(m);
                lsvMethods.Items.Add(lsv);
            }

            handleListVisibility();
            setToolTips();
        }

        /// <summary>
        /// Setzt die Tools für die Visibility-Buttons
        /// </summary>
        private void setToolTips()
        {
            if (SectionClass.MethodsMinimized)
                toolTip.SetToolTip(cmdMVisibility, "Methoden anzeigen");
            else
                toolTip.SetToolTip(cmdMVisibility, "Methoden minimieren");

            if (SectionClass.VariablesMinimized)
                toolTip.SetToolTip(cmdVVisibility, "Variablen anzeigen");
            else
                toolTip.SetToolTip(cmdVVisibility, "Variablen minimieren");
        }

        /// <summary>
        /// Setzt die Views Visibility und die Grösse des Controls
        /// </summary>
        /// <param name="type"></param>
        private void handleListVisibility()
        {            
            const int TOP_BORDER_LIST = 60; //Abstand + Textbox + Abstand 
            const int BOTTOM_BORDER_LIST = 20;

            lsvVariables.Visible = !SectionClass.VariablesMinimized;
            lsvMethods.Visible = !SectionClass.MethodsMinimized;

            //Höhe des Controls festlegen Start 
            int controlHeight = TOP_BORDER_LIST;

            if (SectionClass.VariablesMinimized == false && SectionClass.MethodsMinimized == false)
                controlHeight += (2 * LIST_HEIGHT) + BOTTOM_BORDER_LIST;
            else if (SectionClass.VariablesMinimized == false || SectionClass.MethodsMinimized == false)
                controlHeight += LIST_HEIGHT + BOTTOM_BORDER_LIST;

            this.Height = controlHeight;
            //Höhe des Controls festlegen Ende 

            if (SectionClass.VariablesMinimized && SectionClass.MethodsMinimized == false)
                lsvMethods.Top = TOP_BORDER_LIST;
            if (SectionClass.VariablesMinimized == false && SectionClass.MethodsMinimized)
                lsvVariables.Top = TOP_BORDER_LIST;

            if (SectionClass.VariablesMinimized == false && SectionClass.MethodsMinimized == false)
            {
                lsvVariables.Top = TOP_BORDER_LIST;
                lsvMethods.Top = TOP_BORDER_LIST + LIST_HEIGHT;
            }

            setToolTips();
            SectionClass.Rectangle = new Rectangle(this.Location, this.ClientSize);
            OnRectangleChanged();
        }

        private void OnRectangleChanged()
        {
            if (RectangleChanged != null)
                RectangleChanged();
        }

        void mnu_Click(object sender, EventArgs e)
        {
            MenuItem mnu = (MenuItem)sender;
            switch (mnu.Name)
            {
                case "mnuMethodNew":
                    handleNewMethod();
                    break;
                case "mnuMethodDelete":
                    handleDeleteMethod();
                    break;
                case "mnuVariableNew":
                    handleNewVariable();
                    break;
                case "mnuVariableDelete":
                    handleDeleteVariable();
                    break;
            }
        }

        private void handleNewMethod()
        {
            NewMethod n = new NewMethod();
            if (n.ShowDialog() == DialogResult.OK)
            {
                ListViewItem lsv = new ListViewItem(n.Method);
                lsvMethods.Items.Add(lsv);
                SectionClass.Methods.Add(n.Method);
            }
        }

        private void handleDeleteMethod()
        {
            ListView lsv = getListViewToType(SectionEntryType.Method);
            foreach (ListViewItem l in lsv.SelectedItems)
            {
                SectionClass.Methods.Remove(l.Text);
                lsv.Items.Remove(l);
            }
        }

        private void handleNewVariable()
        {
            NewVariable v = new NewVariable();
            if (v.ShowDialog() == DialogResult.OK)
            {
                ListViewItem l = new ListViewItem(v.Variable.VariableType);
                l.SubItems.Add(new ListViewItem.ListViewSubItem(l, v.Variable.VariableName));
                l.Tag = v.Variable;
                getListViewToType(SectionEntryType.Variable).Items.Add(l);
                SectionClass.Variables.Add(v.Variable);
            }
        }

        private void handleDeleteVariable()
        {
            ListView lsv = getListViewToType(SectionEntryType.Variable);
            foreach (ListViewItem l in lsv.SelectedItems)
            {
                SectionClass.Variables.Remove((Variable)l.Tag);
                lsv.Items.Remove(l);
            }
        }

        private ListView getListViewToType(SectionEntryType type)
        {
            ListView lsv = new ListView();
            switch (type)
            {
                case SectionEntryType.Variable:
                    lsv = lsvVariables;
                    break;
                case SectionEntryType.Method:
                    lsv = lsvMethods;
                    break;
            }
            return lsv;
        }

        void sectionClass_FocusChanged()
        {
            if (SectionClass.IsFocused)
                this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            else
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void setBindings()
        {
            txtName.DataBindings.Add("text", SectionClass, "SectionName", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Font font = new Font("ARIAL", 10, FontStyle.Bold))
            {
                e.Graphics.DrawString(SectionClass.SectionType.ToString(), font, Brushes.Black, new PointF(12, 2.5F));
            }
            base.OnPaint(e);
        }

        private void chkDrag_CheckedChanged(object sender, EventArgs e)
        {
            SectionClass.DragMode = chkDrag.Checked;
        }

        private void Section_LocationChanged(object sender, EventArgs e)
        {
            this.SectionClass.Rectangle = new Rectangle(this.Location, new Size(this.Width, this.Height));
        }

        private void Section_Load(object sender, EventArgs e)
        {

        }

        private void cmdVVisibility_Click(object sender, EventArgs e)
        {
            SectionClass.VariablesMinimized = !SectionClass.VariablesMinimized;
            handleListVisibility();
        }

        private void cmdMVisibility_Click(object sender, EventArgs e)
        {
            SectionClass.MethodsMinimized =! SectionClass.MethodsMinimized;
            handleListVisibility();
        }
    }
}
