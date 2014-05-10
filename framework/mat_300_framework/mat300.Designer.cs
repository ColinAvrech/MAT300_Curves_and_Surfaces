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
            this.Menu_DeCast = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Bern = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Midpoint = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Inter = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Inter_Poly = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Inter_Splines = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_DeBoor = new System.Windows.Forms.ToolStripMenuItem();
            this.Txt_knot = new System.Windows.Forms.TextBox();
            this.Lbl_degree = new System.Windows.Forms.Label();
            this.Lbl_knot = new System.Windows.Forms.Label();
            this.NUD_degree = new System.Windows.Forms.NumericUpDown();
            this.CB_cont = new System.Windows.Forms.CheckBox();
            this.TValueUD = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_degree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TValueUD)).BeginInit();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1056, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Clear,
            this.Menu_Exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // Menu_Clear
            // 
            this.Menu_Clear.Name = "Menu_Clear";
            this.Menu_Clear.Size = new System.Drawing.Size(112, 24);
            this.Menu_Clear.Text = "&Clear";
            this.Menu_Clear.Click += new System.EventHandler(this.Menu_Clear_Click);
            // 
            // Menu_Exit
            // 
            this.Menu_Exit.Name = "Menu_Exit";
            this.Menu_Exit.Size = new System.Drawing.Size(112, 24);
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
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // Menu_Polyline
            // 
            this.Menu_Polyline.Checked = true;
            this.Menu_Polyline.CheckOnClick = true;
            this.Menu_Polyline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Polyline.Name = "Menu_Polyline";
            this.Menu_Polyline.Size = new System.Drawing.Size(130, 24);
            this.Menu_Polyline.Text = "&Polyline";
            this.Menu_Polyline.Click += new System.EventHandler(this.Menu_Polyline_Click);
            // 
            // Menu_Points
            // 
            this.Menu_Points.Checked = true;
            this.Menu_Points.CheckOnClick = true;
            this.Menu_Points.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Points.Name = "Menu_Points";
            this.Menu_Points.Size = new System.Drawing.Size(130, 24);
            this.Menu_Points.Text = "P&oints";
            this.Menu_Points.Click += new System.EventHandler(this.Menu_Points_Click);
            // 
            // Menu_Shell
            // 
            this.Menu_Shell.Checked = true;
            this.Menu_Shell.CheckOnClick = true;
            this.Menu_Shell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menu_Shell.Name = "Menu_Shell";
            this.Menu_Shell.Size = new System.Drawing.Size(130, 24);
            this.Menu_Shell.Text = "&Shell";
            this.Menu_Shell.Click += new System.EventHandler(this.Menu_Shell_Click);
            // 
            // methodToolStripMenuItem
            // 
            this.methodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_DeCast,
            this.Menu_Bern,
            this.Menu_Midpoint,
            this.Menu_Inter,
            this.Menu_DeBoor});
            this.methodToolStripMenuItem.Name = "methodToolStripMenuItem";
            this.methodToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.methodToolStripMenuItem.Text = "&Method";
            // 
            // Menu_DeCast
            // 
            this.Menu_DeCast.Name = "Menu_DeCast";
            this.Menu_DeCast.Size = new System.Drawing.Size(175, 24);
            this.Menu_DeCast.Text = "&DeCastlejau";
            this.Menu_DeCast.Click += new System.EventHandler(this.Menu_DeCast_Click);
            // 
            // Menu_Bern
            // 
            this.Menu_Bern.Name = "Menu_Bern";
            this.Menu_Bern.Size = new System.Drawing.Size(157, 24);
            this.Menu_Bern.Text = "&Bernstein";
            this.Menu_Bern.Click += new System.EventHandler(this.Menu_Bern_Click);
            // 
            // Menu_Midpoint
            // 
            this.Menu_Midpoint.Name = "Menu_Midpoint";
            this.Menu_Midpoint.Size = new System.Drawing.Size(157, 24);
            this.Menu_Midpoint.Text = "&Midpoint";
            this.Menu_Midpoint.Click += new System.EventHandler(this.Menu_Midpoint_Click);
            // 
            // Menu_Inter
            // 
            this.Menu_Inter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Inter_Poly,
            this.Menu_Inter_Splines});
            this.Menu_Inter.Name = "Menu_Inter";
            this.Menu_Inter.Size = new System.Drawing.Size(157, 24);
            this.Menu_Inter.Text = "&Interpolate";
            // 
            // Menu_Inter_Poly
            // 
            this.Menu_Inter_Poly.Name = "Menu_Inter_Poly";
            this.Menu_Inter_Poly.Size = new System.Drawing.Size(152, 24);
            this.Menu_Inter_Poly.Text = "&Polynomial";
            this.Menu_Inter_Poly.Click += new System.EventHandler(this.Menu_Inter_Poly_Click);
            // 
            // Menu_Inter_Splines
            // 
            this.Menu_Inter_Splines.Name = "Menu_Inter_Splines";
            this.Menu_Inter_Splines.Size = new System.Drawing.Size(152, 24);
            this.Menu_Inter_Splines.Text = "&Splines";
            this.Menu_Inter_Splines.Click += new System.EventHandler(this.Menu_Inter_Splines_Click);
            // 
            // Menu_DeBoor
            // 
            this.Menu_DeBoor.Name = "Menu_DeBoor";
            this.Menu_DeBoor.Size = new System.Drawing.Size(157, 24);
            this.Menu_DeBoor.Text = "DeBoo&r";
            this.Menu_DeBoor.Click += new System.EventHandler(this.Menu_DeBoor_Click);
            // 
            // Txt_knot
            // 
            this.Txt_knot.Location = new System.Drawing.Point(16, 666);
            this.Txt_knot.Margin = new System.Windows.Forms.Padding(4);
            this.Txt_knot.Name = "Txt_knot";
            this.Txt_knot.Size = new System.Drawing.Size(1023, 22);
            this.Txt_knot.TabIndex = 1;
            this.Txt_knot.Visible = false;
            this.Txt_knot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Txt_knot_KeyPress);
            // 
            // Lbl_degree
            // 
            this.Lbl_degree.AutoSize = true;
            this.Lbl_degree.Location = new System.Drawing.Point(924, 636);
            this.Lbl_degree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_degree.Name = "Lbl_degree";
            this.Lbl_degree.Size = new System.Drawing.Size(55, 17);
            this.Lbl_degree.TabIndex = 3;
            this.Lbl_degree.Text = "Degree";
            this.Lbl_degree.Visible = false;
            // 
            // Lbl_knot
            // 
            this.Lbl_knot.AutoSize = true;
            this.Lbl_knot.Location = new System.Drawing.Point(16, 646);
            this.Lbl_knot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Lbl_knot.Name = "Lbl_knot";
            this.Lbl_knot.Size = new System.Drawing.Size(70, 17);
            this.Lbl_knot.TabIndex = 4;
            this.Lbl_knot.Text = "Knot Seq.";
            this.Lbl_knot.Visible = false;
            // 
            // NUD_degree
            // 
            this.NUD_degree.InterceptArrowKeys = false;
            this.NUD_degree.Location = new System.Drawing.Point(988, 634);
            this.NUD_degree.Margin = new System.Windows.Forms.Padding(4);
            this.NUD_degree.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_degree.Name = "NUD_degree";
            this.NUD_degree.ReadOnly = true;
            this.NUD_degree.Size = new System.Drawing.Size(52, 22);
            this.NUD_degree.TabIndex = 5;
            this.NUD_degree.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_degree.Visible = false;
            this.NUD_degree.ValueChanged += new System.EventHandler(this.NUD_degree_ValueChanged);
            // 
            // CB_cont
            // 
            this.CB_cont.AutoSize = true;
            this.CB_cont.Checked = true;
            this.CB_cont.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_cont.Location = new System.Drawing.Point(96, 645);
            this.CB_cont.Margin = new System.Windows.Forms.Padding(4);
            this.CB_cont.Name = "CB_cont";
            this.CB_cont.Size = new System.Drawing.Size(92, 21);
            this.CB_cont.TabIndex = 7;
            this.CB_cont.Text = "Continuity";
            this.CB_cont.UseVisualStyleBackColor = true;
            this.CB_cont.Visible = false;
            this.CB_cont.CheckedChanged += new System.EventHandler(this.CB_cont_CheckedChanged);
            // 
            // TValueUD
            // 
            this.TValueUD.AutoSize = true;
            this.TValueUD.DecimalPlaces = 2;
            this.TValueUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.TValueUD.Location = new System.Drawing.Point(244, 4);
            this.TValueUD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TValueUD.Name = "TValueUD";
            this.TValueUD.Size = new System.Drawing.Size(52, 22);
            this.TValueUD.TabIndex = 8;
            this.TValueUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TValueUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(180, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "&T-Value";
            // 
            // MAT300
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1056, 705);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TValueUD);
            this.Controls.Add(this.CB_cont);
            this.Controls.Add(this.NUD_degree);
            this.Controls.Add(this.Lbl_knot);
            this.Controls.Add(this.Lbl_degree);
            this.Controls.Add(this.Txt_knot);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MAT300";
            this.Text = "MAT300Framework";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MAT300_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MAT300_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MAT300_MouseMove);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MAT300_MouseWheel);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_degree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TValueUD)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem Menu_DeCast;
        private System.Windows.Forms.ToolStripMenuItem Menu_Bern;
        private System.Windows.Forms.ToolStripMenuItem Menu_Midpoint;
        private System.Windows.Forms.ToolStripMenuItem Menu_Clear;
        private System.Windows.Forms.ToolStripMenuItem Menu_Inter;
        private System.Windows.Forms.ToolStripMenuItem Menu_DeBoor;
        private System.Windows.Forms.ToolStripMenuItem Menu_Inter_Poly;
        private System.Windows.Forms.ToolStripMenuItem Menu_Inter_Splines;
        private System.Windows.Forms.TextBox Txt_knot;
        private System.Windows.Forms.Label Lbl_degree;
        private System.Windows.Forms.Label Lbl_knot;
        private System.Windows.Forms.NumericUpDown NUD_degree;
        private System.Windows.Forms.CheckBox CB_cont;
        private System.Windows.Forms.NumericUpDown TValueUD;
        private System.Windows.Forms.Label label1;
    }
}

