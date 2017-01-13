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
    public partial class NewMethod : Form
    {
        public String Method
        {
            get
            {
                return txtValue.Text; 
            }
        }
        public NewMethod()
        {
            InitializeComponent();
            this.AcceptButton = cmdOK;
            this.CancelButton = cmdCancel;
            this.txtValue.Select();
        }

        private void NewMethod_Load(object sender, EventArgs e)
        {

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
