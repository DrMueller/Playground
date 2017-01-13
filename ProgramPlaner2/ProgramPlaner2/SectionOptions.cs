using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramPlaner2
{
    public partial class SectionOptions : Form
    {

        private SectionClass sectionClass;
        public SectionOptions(SectionClass sectionClass)
        {
            InitializeComponent();

            this.AcceptButton = cmdOK;
            this.CancelButton = cmdCancel;

            this.sectionClass = sectionClass;

            try
            {
                Binding binding = new Binding("Text", sectionClass, "SectionName", false, DataSourceUpdateMode.Never);
                this.txtName.DataBindings.Add(binding);
                cmbType.DataSource = Enum.GetValues(typeof(SectionType));
                cmbType.SelectedItem = sectionClass.SectionType;


                if (sectionClass.SectionName != null)
                    this.Text = sectionClass.SectionName;
                else
                    this.Text = "Neuer Abschnitt";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private void SectionOptions_Load(object sender, EventArgs e)
        {
            this.txtName.Select();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            sectionClass.SectionName = txtName.Text;
            sectionClass.SectionType = (SectionType)cmbType.SelectedItem;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
