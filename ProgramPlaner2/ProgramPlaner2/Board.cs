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
    public partial class Board : Form
    {
        public const int STANDARD_SIZE = 600;
        private Logic logic;
        private string fileName = "";

        public Board()
        {
            InitializeComponent();
            initControls();
            this.AllowDrop = true;
            setDialogs();
            logic = new Logic(this);
        }

        private void setDialogs()
        {
            dlgSaveFile.FileName = "Projectplaner.pp";
            dlgSaveFile.Filter = "Projekt-Planer Dateien(*.pp)|";

            dlgOpenFile.FileName = "";
            dlgOpenFile.Filter = "Projekt-Planer Dateien(*.pp)|";
        }

        private void initControls()
        {
            ToolStripMenuItem tmp; 
            ToolStripMenuItem mnuMain = new ToolStripMenuItem("Optionen");

            tmp = new ToolStripMenuItem("Neu");
            tmp.Name = "mnuOptionsNew";
            tmp.Click += new EventHandler(tmp_Click);
            tmp.ShortcutKeys = Keys.Control | Keys.E;
            mnuMain.DropDownItems.Add(tmp);

            tmp = new ToolStripMenuItem("Laden");
            tmp.Name = "mnuOptionsLoad";
            tmp.Click += new EventHandler(tmp_Click);
            tmp.ShortcutKeys = Keys.Control | Keys.L;
            mnuMain.DropDownItems.Add(tmp);

            tmp = new ToolStripMenuItem("Speichern");
            tmp.Name = "mnuOptionsSave";
            tmp.Click += new EventHandler(tmp_Click);
            tmp.ShortcutKeys = Keys.Control | Keys.S;
            mnuMain.DropDownItems.Add(tmp);

            tmp = new ToolStripMenuItem("Speichern unter...");
            tmp.Name = "mnuOptionsSaveAs";
            tmp.Click += new EventHandler(tmp_Click);
            tmp.ShortcutKeys = Keys.Control | Keys.U;
            mnuMain.DropDownItems.Add(tmp);

            mnuMain.DropDownItems.Add(new ToolStripSeparator());

            tmp = new ToolStripMenuItem("Drucken");
            tmp.Name = "mnuOptionsPrint";
            tmp.Click += new EventHandler(tmp_Click);
            tmp.ShortcutKeys = Keys.Control | Keys.P;
            mnuMain.DropDownItems.Add(tmp);

            mnuMain.DropDownItems.Add(new ToolStripSeparator());

            tmp = new ToolStripMenuItem("Beenden");
            tmp.Name = "mnuOptionsEnd";
            tmp.Click += new EventHandler(tmp_Click);
            tmp.ShortcutKeys = Keys.Control | Keys.B; 
            mnuMain.DropDownItems.Add(tmp);

            stripMain.Items.Add(mnuMain);


            mnuMain = new ToolStripMenuItem("Abschnitt");
            tmp = new ToolStripMenuItem("Neu");
            tmp.Click += new EventHandler(tmp_Click);
            tmp.Name = "mnuSectionNew";
            tmp.ShortcutKeys = Keys.Control | Keys.N;
            mnuMain.DropDownItems.Add(tmp);

            tmp = new ToolStripMenuItem("Löschen");
            tmp.Click += new EventHandler(tmp_Click);
            tmp.Name = "mnuSectionDelete";
            tmp.ShortcutKeys =  Keys.Control | Keys.D;
            mnuMain.DropDownItems.Add(tmp);

            tmp = new ToolStripMenuItem("Verweise");
            tmp.Click += new EventHandler(tmp_Click);
            tmp.Name = "mnuSectionConnections";
            tmp.ShortcutKeys = Keys.Control | Keys.V;
            mnuMain.DropDownItems.Add(tmp);

            mnuMain.DropDownItems.Add(new ToolStripSeparator());

            tmp = new ToolStripMenuItem("Alle Verschiebbar");
            tmp.Click += new EventHandler(tmp_Click);
            tmp.Name = "mnuSectionsMovable";
            tmp.ShortcutKeys = Keys.Control | Keys.A;
            mnuMain.DropDownItems.Add(tmp);

            tmp = new ToolStripMenuItem("Alle Gesperrt");
            tmp.Click += new EventHandler(tmp_Click);
            tmp.Name = "mnuSectionsLocked";
            tmp.ShortcutKeys = Keys.Control | Keys.G;
            mnuMain.DropDownItems.Add(tmp);

            stripMain.Items.Add(mnuMain);

            mnuMain = new ToolStripMenuItem("Handling");
            tmp = new ToolStripMenuItem("Variablen bereinigen");
            tmp.Click += new EventHandler(tmp_Click);
            tmp.Name = "mnuHandlingClearVariables";
            tmp.ShortcutKeys = Keys.Control | Keys.R;
            mnuMain.DropDownItems.Add(tmp);

            stripMain.Items.Add(mnuMain);
        }

        void tmp_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            switch (item.Name)
            {
                case "mnuOptionsNew":
                    logic.NewBoard();
                    break;
                case "mnuOptionsSave":
                    saveBoard(false);
                    break;
                case "mnuOptionsSaveAs":
                    saveBoard(true);
                    break;
                case "mnuOptionsLoad":
                    loadBoard();
                    break;
                case "mnuOptionsPrint":
                    logic.PrintBoard();
                    break;
                case "mnuOptionsEnd":
                    this.Close();
                    break;
                case "mnuSectionNew":
                    logic.AddNewSection();
                    break;
                case "mnuSectionDelete":
                    logic.DeleteSection();
                    break;
                case "mnuSectionConnections":
                    logic.LoadConnections();
                    break;
                case "mnuSectionsMovable":
                    logic.SetSectionsMovable(true);
                    break;
                case "mnuSectionsLocked":
                    logic.SetSectionsMovable(false);
                    break;
                case "mnuHandlingClearVariables":
                    logic.ClearVariables();
                    break;
            }
        }

        private void loadBoard()
        {
            if (dlgOpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                logic = new Logic(this, dlgOpenFile.FileName);
                fileName = dlgOpenFile.FileName;
            }
        }

        private void saveBoard(bool askFileName)
        {
            if (askFileName || fileName.Length == 0)
            {
                if (dlgSaveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    logic.Save(dlgSaveFile.FileName);
                    fileName = dlgSaveFile.FileName;
                }
            }
            else
                logic.Save(fileName);
        }

        private void Board_Load(object sender, EventArgs e)
        {
            logic.NewBoard();
        }
    }
}
