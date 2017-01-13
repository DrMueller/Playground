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
    public partial class NewVariable : Form
    {
        public Variable Variable
        {
            get
            {
                return new Variable(txtName.Text, cmbType.Text);
            }
        }
        public NewVariable()
        {
            InitializeComponent();
            this.AcceptButton = cmdOK;
            this.CancelButton = cmdCancel;
            this.cmbType.Select();
            this.cmbType.DataSource = FileHandler.LoadVariableTypes();
            this.cmbType.SelectedIndex = -1;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            saveVariableTypes();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void saveVariableTypes()
        {
            List<String> variableTypes = (List<String>)this.cmbType.DataSource;
            if (!variableTypes.Contains(cmbType.Text))
                variableTypes.Add(cmbType.Text);

            FileHandler.SaveVariableTypes(variableTypes);
        }
    }
}
