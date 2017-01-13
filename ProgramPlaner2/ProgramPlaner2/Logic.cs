using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ProgramPlaner2
{
    class Logic
    {
        private Form board;
        private Point relativePosition;


        public Logic(Form board)
        {
            this.board = board;
            setEvents();
        }

        public Logic(Form board, string fileToLoad)
        {
            this.board = board;
            this.NewBoard();
            SaveObject saveObj = FileHandler.LoadOptions(fileToLoad);
            if (saveObj != null)
            {
                board.ClientSize = saveObj.FormSize;
                board.Location = saveObj.FormLocation;
                board.WindowState = saveObj.FormWindowState;

                MapClasses(saveObj.SectionClasses);

                foreach (SectionClass s in saveObj.SectionClasses)
                    addSectionControl(s);
            }
            setEvents();



            board.Invalidate();
        }

        private void MapClasses(List<SectionClass> list)
        {
            foreach (var entry in list)
            {
                //entry.SectionName = entry.SectionType.ToString();
            }
        }

        private void setEvents()
        {
            board.DragEnter += new DragEventHandler(board_DragEnter);
            board.DragDrop += new DragEventHandler(board_DragDrop);
            board.Paint += new PaintEventHandler(board_Paint);
            NewConnection.GetallSectionClasses += new GetAllSectionClassesDelegate(NewConnection_GetallSectionClasses);
            Section.RectangleChanged += new ValueChangedDelegate(Section_RectangleChanged);
        }

        void Section_RectangleChanged()
        {
            board.Invalidate();
        }

        void board_Paint(object sender, PaintEventArgs e)
        {
            foreach (SectionClass s in getSectionClasses())
            {
                foreach (Connection c in s.Connections)
                {
                    ConnectionHandler.DrawConnection(s, c, e.Graphics);
                }
            }
        }

        List<SectionClass> NewConnection_GetallSectionClasses()
        {
            return getSectionClasses();
        }

        void board_DragDrop(object sender, DragEventArgs e)
        {
            Section s = (e.Data.GetData((e.Data.GetFormats()[0])) as Section);
            Point pnt = board.PointToClient((new Point(e.X, e.Y)));
            pnt.X -= relativePosition.X;
            pnt.Y -= relativePosition.Y;
            s.Location = pnt;
            board.Invalidate();
        }

        void board_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void section_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Section s = (Section)sender;
                if (s.SectionClass.DragMode)
                {
                    relativePosition = new Point(e.X, e.Y);
                    s.DoDragDrop(sender, DragDropEffects.Move);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Section s = (Section)sender;

                if (s.SectionClass.IsFocused)
                    s.SectionClass.IsFocused = false;
                else
                {
                    foreach (Control c in board.Controls)
                    {
                        if (c is Section)
                            (c as Section).SectionClass.IsFocused = false;
                    }
                    s.SectionClass.IsFocused = true;
                }
            }
        }

        public void AddNewSection()
        {
            SectionClass newSectionClass = new SectionClass();
            SectionOptions opts = new SectionOptions(newSectionClass);
            if (opts.ShowDialog() == DialogResult.OK)
                addSectionControl(newSectionClass);
        }

        private void addSectionControl(SectionClass s)
        {
            Section section = new Section(s);
            section.MouseDown += new MouseEventHandler(section_MouseDown);
            board.Controls.Add(section);
        }

        public void DeleteSection()
        {
            Section sel = getSelectedSection();
            if (sel != null)
            {
                deleteConnections(sel.SectionClass);
                board.Controls.Remove(sel);
                board.Invalidate(); //Connections neu zeichnen
            }
        }

        /// <summary>
        /// Löscht die Connections zu dieser SectionClass
        /// </summary>
        /// <param name="sel"></param>
        private void deleteConnections(SectionClass sec)
        {
            List<SectionClass> sectionClasses = getSectionClasses();
            List<Connection> toRemove = new List<Connection>();
            foreach (SectionClass c in sectionClasses)
            {
                foreach (Connection conn in c.Connections)
                {
                    if (conn.ConnectionTarget == sec)
                    {
                        toRemove.Add(conn);
                    }
                }
                foreach (Connection conn in toRemove)
                {
                    c.Connections.Remove(conn);
                }
            }
        }

        private Section getSelectedSection()
        {
            foreach (Control c in board.Controls)
            {
                if ((c is Section) && (c as Section).SectionClass.IsFocused)
                    return (c as Section);           
            }
            return null;
        }

        public void PrintBoard()
        {
            PrintHandler.Print(board);
        }

        public void LoadConnections()
        {
            Section s = getSelectedSection();
            if (s != null)
            {
                Connections cons = new Connections(s.SectionClass);
                cons.ShowDialog();
                board.Invalidate();
            }
        }

        public void Save(string fileName)
        {
            SaveObject o = new  SaveObject(getSectionClasses(), board.WindowState, board.ClientSize, board.Location);
            FileHandler.SaveOptions(o, fileName);
        }

        private List<SectionClass> getSectionClasses()
        {
            List<SectionClass> s = new List<SectionClass>();

            foreach (Control c in board.Controls)
            {
                if (c is Section)
                {
                    s.Add((c as Section).SectionClass);
                }
            }
            return s;
        }

        public void NewBoard()
        {
            try
            {
                for (int i = board.Controls.Count - 1; i >= 0; i--)
                {
                    if (board.Controls[i] is Section)
                        board.Controls.RemoveAt(i);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            board.ClientSize = new Size(Board.STANDARD_SIZE, Board.STANDARD_SIZE);
        }

        public void SetSectionsMovable(bool movable)
        {
            foreach (SectionClass sec in getSectionClasses())
            {
                sec.DragMode = movable;
            }
        }

        public void ClearVariables()
        {
            FileHandler.DeleteVariableTypes();
        }
    }
}
