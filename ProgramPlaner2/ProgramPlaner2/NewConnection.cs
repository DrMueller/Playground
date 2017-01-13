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
    public partial class NewConnection : Form
    {
        public static event GetAllSectionClassesDelegate GetallSectionClasses;
        public Connection Connection
        {
            get
            {
                return new Connection((SectionClass)cmbTarget.SelectedItem, (ConnectionType)cmbType.SelectedItem);
            }
        }
        public NewConnection(SectionClass parentSectionClass)
        {
            InitializeComponent();
            this.AcceptButton = cmdOK;
            this.CancelButton = cmdCancel;
            this.cmbTarget.Select();

            cmbType.DataSource = Enum.GetValues(typeof(ConnectionType));

            if (GetallSectionClasses != null)
            {
                List<SectionClass> sectionClasses;
                sectionClasses = GetallSectionClasses();
                cmbTarget.DisplayMember = "SectionName";

                foreach (SectionClass s in sectionClasses)
                {
                    if (s.Equals(parentSectionClass) == false && checkTarget(parentSectionClass.Connections, s) == false)
                        cmbTarget.Items.Add(s);
                }
            }
        }

        private bool checkTarget(List<Connection> list, SectionClass s)
        {
            foreach (Connection c in list)
            {
                if (c.ConnectionTarget == s)
                    return true;
            }
            return false;
        }

        private void NewConnection_Load(object sender, EventArgs e)
        {
            
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (validateEntries())
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private bool validateEntries()
        {
            if (cmbTarget.SelectedItem == null)
            {
                MessageBox.Show("Bitte wähle Sie ein Ziel aus.");
                return false;
            }
            else if (cmbType.SelectedItem == null)
            {
                MessageBox.Show("Bitte wählen Sie einen Typ aus.");
                return false;
            }
            return true;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
