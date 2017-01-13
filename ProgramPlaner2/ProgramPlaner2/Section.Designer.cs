namespace ProgramPlaner2
{
    partial class Section
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkDrag = new System.Windows.Forms.CheckBox();
            this.lsvVariables = new System.Windows.Forms.ListView();
            this.lsvMethods = new System.Windows.Forms.ListView();
            this.cmdVVisibility = new System.Windows.Forms.Button();
            this.cmdMVisibility = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(188, 20);
            this.txtName.TabIndex = 0;
            // 
            // chkDrag
            // 
            this.chkDrag.AutoSize = true;
            this.chkDrag.Location = new System.Drawing.Point(185, 4);
            this.chkDrag.Name = "chkDrag";
            this.chkDrag.Size = new System.Drawing.Size(15, 14);
            this.chkDrag.TabIndex = 1;
            this.chkDrag.UseVisualStyleBackColor = true;
            this.chkDrag.CheckedChanged += new System.EventHandler(this.chkDrag_CheckedChanged);
            // 
            // lsvVariables
            // 
            this.lsvVariables.Location = new System.Drawing.Point(12, 60);
            this.lsvVariables.Name = "lsvVariables";
            this.lsvVariables.Size = new System.Drawing.Size(188, 120);
            this.lsvVariables.TabIndex = 2;
            this.lsvVariables.UseCompatibleStateImageBehavior = false;
            // 
            // lsvMethods
            // 
            this.lsvMethods.Location = new System.Drawing.Point(12, 180);
            this.lsvMethods.Name = "lsvMethods";
            this.lsvMethods.Size = new System.Drawing.Size(188, 120);
            this.lsvMethods.TabIndex = 3;
            this.lsvMethods.UseCompatibleStateImageBehavior = false;
            // 
            // cmdVVisibility
            // 
            this.cmdVVisibility.Location = new System.Drawing.Point(151, 4);
            this.cmdVVisibility.Name = "cmdVVisibility";
            this.cmdVVisibility.Size = new System.Drawing.Size(12, 12);
            this.cmdVVisibility.TabIndex = 4;
            this.cmdVVisibility.Text = "...";
            this.cmdVVisibility.UseVisualStyleBackColor = true;
            this.cmdVVisibility.Click += new System.EventHandler(this.cmdVVisibility_Click);
            // 
            // cmdMVisibility
            // 
            this.cmdMVisibility.Location = new System.Drawing.Point(168, 4);
            this.cmdMVisibility.Name = "cmdMVisibility";
            this.cmdMVisibility.Size = new System.Drawing.Size(12, 12);
            this.cmdMVisibility.TabIndex = 5;
            this.cmdMVisibility.Text = "...";
            this.cmdMVisibility.UseVisualStyleBackColor = true;
            this.cmdMVisibility.Click += new System.EventHandler(this.cmdMVisibility_Click);
            // 
            // Section
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmdMVisibility);
            this.Controls.Add(this.cmdVVisibility);
            this.Controls.Add(this.lsvMethods);
            this.Controls.Add(this.lsvVariables);
            this.Controls.Add(this.chkDrag);
            this.Controls.Add(this.txtName);
            this.Name = "Section";
            this.Size = new System.Drawing.Size(211, 320);
            this.Load += new System.EventHandler(this.Section_Load);
            this.LocationChanged += new System.EventHandler(this.Section_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox chkDrag;
        private System.Windows.Forms.ListView lsvVariables;
        private System.Windows.Forms.ListView lsvMethods;
        private System.Windows.Forms.Button cmdVVisibility;
        private System.Windows.Forms.Button cmdMVisibility;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
