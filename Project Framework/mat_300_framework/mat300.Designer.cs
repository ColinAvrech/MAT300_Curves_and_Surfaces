namespace mat_300_framework
{
    partial class MAT300
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Clear = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Polyline = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Points = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Shell = new System.Windows.Forms.ToolStripMenuItem();
            this.methodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment1_DeCastlejau = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment1_Bernstein = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment2_DeCastlejau = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment2_Bernstein = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment2_Midpoint = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment3 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment3_Inter_Poly = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment4 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment4_Inter_Splines = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment7 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Assignment7_DeBoor = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Midpoint = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_DeBoor = new System.Windows.Forms.ToolStripMenuItem();
            this.Txt_knot = new System.Windows.Forms.TextBox();
            this.Lbl_knot = new System.Windows.Forms.Label();
            this.NUD = new System.Windows.Forms.NumericUpDown();
            this.NUD_label = new System.Windows.Forms.Label();
            this.CB_cont = new System.Windows.Forms.CheckBox();
            this.S_NUD = new System.Windows.Forms.NumericUpDown();
            this.S_NUD_label = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.S_NUD)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.methodToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Clear,
            this.Menu_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // Menu_Clear
            // 
            this.Menu_Clear.Name = "Menu_Clear";
            this.Menu_Clear.Size = new System.Drawing.Size(101, 22);
            this.Menu_Clear.Text = "&Clear";
            this.Menu_Clear.Click += new System.EventHandler(this.Menu_Clear_Click);
            // 
            // Menu_Exit
            // 
            this.Menu_Exit.Name = "Menu_Exit";
            this.Menu_Exit.Size = new System.Drawing.Size(101, 22);
            this.Menu_Exit.Text = "E&xit";
            this.Menu_Exit.Click += new System.EventHandler(this.Menu_Exit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Polyline,
            this.Menu_Points,
            this.Menu_Shell});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // Menu_Polyline
            // 
            this.Menu_Polyline.Checked = true;
            this.Menu_Polyline.CheckOnClick = true;
            this.Menu_Polyline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Polyline.Name = "Menu_Polyline";
            this.Menu_Polyline.Size = new System.Drawing.Size(116, 22);
            this.Menu_Polyline.Text = "&Polyline";
            this.Menu_Polyline.Click += new System.EventHandler(this.Menu_Polyline_Click);
            // 
            // Menu_Points
            // 
            this.Menu_Points.Checked = true;
            this.Menu_Points.CheckOnClick = true;
            this.Menu_Points.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Points.Name = "Menu_Points";
            this.Menu_Points.Size = new System.Drawing.Size(116, 22);
            this.Menu_Points.Text = "P&oints";
            this.Menu_Points.Click += new System.EventHandler(this.Menu_Points_Click);
            // 
            // Menu_Shell
            // 
            this.Menu_Shell.Checked = true;
            this.Menu_Shell.CheckOnClick = true;
            this.Menu_Shell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Shell.Name = "Menu_Shell";
            this.Menu_Shell.Size = new System.Drawing.Size(116, 22);
            this.Menu_Shell.Text = "&Shell";
            this.Menu_Shell.Click += new System.EventHandler(this.Menu_Shell_Click);
            // 
            // methodToolStripMenuItem
            // 
            this.methodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Assignment1,
            this.Menu_Assignment2,
            this.Menu_Assignment3,
            this.Menu_Assignment4,
            this.Menu_Assignment7});
            this.methodToolStripMenuItem.Name = "methodToolStripMenuItem";
            this.methodToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.methodToolStripMenuItem.Text = "&Method";
            // 
            // Menu_Assignment1
            // 
            this.Menu_Assignment1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Assignment1_DeCastlejau,
            this.Menu_Assignment1_Bernstein});
            this.Menu_Assignment1.Name = "Menu_Assignment1";
            this.Menu_Assignment1.Size = new System.Drawing.Size(354, 22);
            this.Menu_Assignment1.Text = "Assignment1 - Coefficients Polynomial";
            // 
            // Menu_Assignment1_DeCastlejau
            // 
            this.Menu_Assignment1_DeCastlejau.Name = "Menu_Assignment1_DeCastlejau";
            this.Menu_Assignment1_DeCastlejau.Size = new System.Drawing.Size(136, 22);
            this.Menu_Assignment1_DeCastlejau.Text = "DeCastlejau";
            this.Menu_Assignment1_DeCastlejau.Click += new System.EventHandler(this.Menu_Assignment1_DeCastlejau_Click);
            // 
            // Menu_Assignment1_Bernstein
            // 
            this.Menu_Assignment1_Bernstein.Name = "Menu_Assignment1_Bernstein";
            this.Menu_Assignment1_Bernstein.Size = new System.Drawing.Size(136, 22);
            this.Menu_Assignment1_Bernstein.Text = "Bernstein";
            this.Menu_Assignment1_Bernstein.Click += new System.EventHandler(this.Menu_Assignment1_Bernstein_Click);
            // 
            // Menu_Assignment2
            // 
            this.Menu_Assignment2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Assignment2_DeCastlejau,
            this.Menu_Assignment2_Bernstein,
            this.Menu_Assignment2_Midpoint});
            this.Menu_Assignment2.Name = "Menu_Assignment2";
            this.Menu_Assignment2.Size = new System.Drawing.Size(354, 22);
            this.Menu_Assignment2.Text = "Assignment2 - Polynomial";
            // 
            // Menu_Assignment2_DeCastlejau
            // 
            this.Menu_Assignment2_DeCastlejau.Name = "Menu_Assignment2_DeCastlejau";
            this.Menu_Assignment2_DeCastlejau.Size = new System.Drawing.Size(187, 22);
            this.Menu_Assignment2_DeCastlejau.Text = "DeCastlejau";
            this.Menu_Assignment2_DeCastlejau.Click += new System.EventHandler(this.Menu_Assignment2_DeCastlejau_Click);
            // 
            // Menu_Assignment2_Bernstein
            // 
            this.Menu_Assignment2_Bernstein.Name = "Menu_Assignment2_Bernstein";
            this.Menu_Assignment2_Bernstein.Size = new System.Drawing.Size(187, 22);
            this.Menu_Assignment2_Bernstein.Text = "Bernstein";
            this.Menu_Assignment2_Bernstein.Click += new System.EventHandler(this.Menu_Assignment2_Bernstein_Click);
            // 
            // Menu_Assignment2_Midpoint
            // 
            this.Menu_Assignment2_Midpoint.Name = "Menu_Assignment2_Midpoint";
            this.Menu_Assignment2_Midpoint.Size = new System.Drawing.Size(187, 22);
            this.Menu_Assignment2_Midpoint.Text = "Midpoint Subdivision";
            this.Menu_Assignment2_Midpoint.Click += new System.EventHandler(this.Menu_Assignment2_Midpoint_Click);
            // 
            // Menu_Assignment3
            // 
            this.Menu_Assignment3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Assignment3_Inter_Poly});
            this.Menu_Assignment3.Name = "Menu_Assignment3";
            this.Menu_Assignment3.Size = new System.Drawing.Size(354, 22);
            this.Menu_Assignment3.Text = "Assignment3 - Polynomial Interpolation";
            // 
            // Menu_Assignment3_Inter_Poly
            // 
            this.Menu_Assignment3_Inter_Poly.Name = "Menu_Assignment3_Inter_Poly";
            this.Menu_Assignment3_Inter_Poly.Size = new System.Drawing.Size(134, 22);
            this.Menu_Assignment3_Inter_Poly.Text = "&Polynomial";
            this.Menu_Assignment3_Inter_Poly.Click += new System.EventHandler(this.Menu_Assignment3_Inter_Poly_Click);
            // 
            // Menu_Assignment4
            // 
            this.Menu_Assignment4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Assignment4_Inter_Splines});
            this.Menu_Assignment4.Name = "Menu_Assignment4";
            this.Menu_Assignment4.Size = new System.Drawing.Size(354, 22);
            this.Menu_Assignment4.Text = "&Assignment4 - Spline Interpolation";
            // 
            // Menu_Assignment4_Inter_Splines
            // 
            this.Menu_Assignment4_Inter_Splines.Name = "Menu_Assignment4_Inter_Splines";
            this.Menu_Assignment4_Inter_Splines.Size = new System.Drawing.Size(111, 22);
            this.Menu_Assignment4_Inter_Splines.Text = "&Splines";
            this.Menu_Assignment4_Inter_Splines.Click += new System.EventHandler(this.Menu_Assignment4_Inter_Splines_Click);
            // 
            // Menu_Assignment7
            // 
            this.Menu_Assignment7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Assignment7_DeBoor});
            this.Menu_Assignment7.Name = "Menu_Assignment7";
            this.Menu_Assignment7.Size = new System.Drawing.Size(354, 22);
            this.Menu_Assignment7.Text = "Assignment7 - DeBoor Algorithm for B-Spline Curves";
            // 
            // Menu_Assignment7_DeBoor
            // 
            this.Menu_Assignment7_DeBoor.Name = "Menu_Assignment7_DeBoor";
            this.Menu_Assignment7_DeBoor.Size = new System.Drawing.Size(113, 22);
            this.Menu_Assignment7_DeBoor.Text = "DeBoo&r";
            this.Menu_Assignment7_DeBoor.Click += new System.EventHandler(this.Menu_Assignment7_DeBoor_Click);
            // 
            // Menu_Midpoint
            // 
            this.Menu_Midpoint.Name = "Menu_Midpoint";
            this.Menu_Midpoint.Size = new System.Drawing.Size(32, 19);
            // 
            // Menu_DeBoor
            // 
            this.Menu_DeBoor.Name = "Menu_DeBoor";
            this.Menu_DeBoor.Size = new System.Drawing.Size(32, 19);
            // 
            // Txt_knot
            // 
            this.Txt_knot.Location = new System.Drawing.Point(12, 541);
            this.Txt_knot.Name = "Txt_knot";
            this.Txt_knot.Size = new System.Drawing.Size(768, 20);
            this.Txt_knot.TabIndex = 1;
            this.Txt_knot.Visible = false;
            this.Txt_knot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_knot_KeyPress);
            // 
            // Lbl_knot
            // 
            this.Lbl_knot.AutoSize = true;
            this.Lbl_knot.Location = new System.Drawing.Point(12, 525);
            this.Lbl_knot.Name = "Lbl_knot";
            this.Lbl_knot.Size = new System.Drawing.Size(54, 13);
            this.Lbl_knot.TabIndex = 4;
            this.Lbl_knot.Text = "Knot Seq.";
            this.Lbl_knot.Visible = false;
            // 
            // NUD
            // 
            this.NUD.InterceptArrowKeys = false;
            this.NUD.Location = new System.Drawing.Point(741, 515);
            this.NUD.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD.Name = "NUD";
            this.NUD.ReadOnly = true;
            this.NUD.Size = new System.Drawing.Size(39, 20);
            this.NUD.TabIndex = 5;
            this.NUD.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.NUD.Visible = false;
            this.NUD.ValueChanged += new System.EventHandler(this.NUD_ValueChanged);
            // 
            // NUD_label
            // 
            this.NUD_label.AutoSize = true;
            this.NUD_label.Location = new System.Drawing.Point(693, 517);
            this.NUD_label.Name = "NUD_label";
            this.NUD_label.Size = new System.Drawing.Size(42, 13);
            this.NUD_label.TabIndex = 3;
            this.NUD_label.Text = "Degree";
            this.NUD_label.Visible = false;
            // 
            // CB_cont
            // 
            this.CB_cont.AutoSize = true;
            this.CB_cont.Checked = true;
            this.CB_cont.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_cont.Location = new System.Drawing.Point(72, 524);
            this.CB_cont.Name = "CB_cont";
            this.CB_cont.Size = new System.Drawing.Size(72, 17);
            this.CB_cont.TabIndex = 7;
            this.CB_cont.Text = "Continuity";
            this.CB_cont.UseVisualStyleBackColor = true;
            this.CB_cont.Visible = false;
            this.CB_cont.CheckedChanged += new System.EventHandler(this.CB_cont_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.S_NUD.InterceptArrowKeys = false;
            this.S_NUD.Location = new System.Drawing.Point(741, 481);
            this.S_NUD.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.S_NUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.S_NUD.Name = "numericUpDown1";
            this.S_NUD.ReadOnly = true;
            this.S_NUD.Size = new System.Drawing.Size(39, 20);
            this.S_NUD.TabIndex = 9;
            this.S_NUD.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.S_NUD.Visible = false;
            this.S_NUD.ValueChanged += new System.EventHandler(this.S_NUD_ValueChanged);
            // 
            // label1
            // 
            this.S_NUD_label.AutoSize = true;
            this.S_NUD_label.Location = new System.Drawing.Point(693, 483);
            this.S_NUD_label.Name = "label1";
            this.S_NUD_label.Size = new System.Drawing.Size(14, 13);
            this.S_NUD_label.TabIndex = 8;
            this.S_NUD_label.Text = "S";
            this.S_NUD_label.Visible = false;
            // 
            // MAT300
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.S_NUD);
            this.Controls.Add(this.S_NUD_label);
            this.Controls.Add(this.CB_cont);
            this.Controls.Add(this.NUD);
            this.Controls.Add(this.Lbl_knot);
            this.Controls.Add(this.NUD_label);
            this.Controls.Add(this.Txt_knot);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MAT300";
            this.Text = "MAT300Framework";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MAT300_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MAT300_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MAT300_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MAT300_MouseWheel);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.S_NUD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Exit;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Polyline;
        private System.Windows.Forms.ToolStripMenuItem Menu_Points;
        private System.Windows.Forms.ToolStripMenuItem methodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Menu_Shell;
        private System.Windows.Forms.ToolStripMenuItem Menu_Midpoint;
        private System.Windows.Forms.ToolStripMenuItem Menu_Clear;
        private System.Windows.Forms.ToolStripMenuItem Menu_DeBoor;
        private System.Windows.Forms.TextBox Txt_knot;
        private System.Windows.Forms.Label NUD_label;
        private System.Windows.Forms.Label Lbl_knot;
        private System.Windows.Forms.NumericUpDown NUD;
        private System.Windows.Forms.CheckBox CB_cont;

        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment1;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment1_DeCastlejau;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment1_Bernstein;
        
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment2;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment2_DeCastlejau;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment2_Bernstein;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment2_Midpoint;
        
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment3;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment3_Inter_Poly;

        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment4;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment4_Inter_Splines;

        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment7;
        private System.Windows.Forms.ToolStripMenuItem Menu_Assignment7_DeBoor;

        private System.Windows.Forms.NumericUpDown S_NUD;
        private System.Windows.Forms.Label S_NUD_label;
    }
}

