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
    public partial class Connections : Form
    {
        private SectionClass sectionClass;
        

        public Connections(SectionClass sectionClass)
        {
            InitializeComponent();
            this.sectionClass = sectionClass;
            this.Text = "Verbindungen von " + sectionClass.SectionName;

            lsvConnections.Columns.Add("Ziel", 157);
            lsvConnections.Columns.Add("Typ", 95);
            lsvConnections.View = View.Details;

            foreach (Connection c in sectionClass.Connections)
            {
                ListViewItem listItem = new ListViewItem(c.ConnectionTarget.SectionName);
                listItem.SubItems.Add(new ListViewItem.ListViewSubItem(listItem, c.ConnectionType.ToString()));
                listItem.Tag = c;
                lsvConnections.Items.Add(listItem);
            }
        }

        private void Connections_Load(object sender, EventArgs e)
        {

        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            NewConnection newConn = new NewConnection(sectionClass);
            if (newConn.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ListViewItem lsv = new ListViewItem(newConn.Connection.ConnectionTarget.SectionName);
                lsv.SubItems.Add(new ListViewItem.ListViewSubItem(lsv, newConn.Connection.ConnectionType.ToString()));
                lsv.Tag = newConn.Connection;
                lsvConnections.Items.Add(lsv);
                sectionClass.Connections.Add(newConn.Connection);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (lsvConnections.SelectedItems != null)
            {
                foreach (ListViewItem item in lsvConnections.SelectedItems)
                {
                    sectionClass.Connections.Remove((Connection)item.Tag);
                    lsvConnections.Items.Remove(item);
                }
            }
        }
    }
}
