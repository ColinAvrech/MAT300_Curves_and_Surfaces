using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace mat_300_framework
{
    public partial class MAT300 : Form
    {
        enum Method
        {
            None,
            DeCastlejau,
            Bernstein,
            MidpointSubdivision,
            Inter_Poly,
            Inter_Spline,
            DeBoor
        }

        public MAT300()
        {
            InitializeComponent();

            assignment_ = 0;
            method_ = Method.None;
            WindowSize_ = this.ClientSize;
            SelectedIndex_ = -1;
            //ProjectedMouse_ = new Point2D(0, 0);
            
            pts_ = new List<Point2D>();
            tVal_ = 0.5F;
            degree_ = 1;
            knot_ = new List<float>();
            EdPtCont_ = true;
            rnd_ = new Random();
            iterations_ = 5;

            CachedPascalsTriangle_ = new List<List<int>>();

            int index;
            for(int row = 1; row < 20; ++row)
            {
                index = 0;
                CachedPascalsTriangle_.Add(new List<int>());
                while(index < row)
                {
                    CachedPascalsTriangle_[row - 1].Add( GetPascalBinomialCoeff(row, index) );
                    ++index;
                }
            }
        }

        // Point class for general math use
        protected class Point2D : System.Object
        {
            public float x;
            public float y;
            public bool inscreenspace;

            public Point2D(float _x, float _y)
            {
                x = _x;
                y = _y;
                inscreenspace = true;
            }

            public Point2D(Point2D rhs)
            {
                x = rhs.x;
                y = rhs.y;
                inscreenspace = rhs.inscreenspace;
            }

            public override String ToString()
            {
                return "("  + x.ToString() + ", " + y.ToString() + ")";
            }

            // adds two points together; used for barycentric combos
            public static Point2D operator +(Point2D lhs, Point2D rhs)
            {
                return new Point2D(lhs.x + rhs.x, lhs.y + rhs.y);
            }

            public static Point2D operator -(Point2D lhs, Point2D rhs)
            {
                return new Point2D(lhs.x - rhs.x, lhs.y - rhs.y);
            }

            // gets a distance between two points. not actual distance; used for picking
            public static float operator %(Point2D lhs, Point2D rhs)
            {
                float dx = (lhs.x - rhs.x);
                float dy = (lhs.y - rhs.y);

                return (dx * dx + dy * dy);
            }

            // scalar multiplication of points; for barycentric combos
            public static Point2D operator *(float t, Point2D rhs)
            {
                return new Point2D(rhs.x * t, rhs.y * t);
            }

            // scalar multiplication of points; for barycentric combos
            public static Point2D operator *(Point2D rhs, float t)
            {
                return new Point2D(rhs.x * t, rhs.y * t);
            }

            //scalar division of points; for barycentric combos
            public static Point2D operator /(float t, Point2D rhs)
            {
              return (rhs * (1.0f / t));
            }

            //scalar division of points; for barycentric combos
            public static Point2D operator /(Point2D rhs, float t)
            {
              return (rhs * (1.0f / t));
            }

            public Point2D ToWorldSpace()
            {
                Point2D result = new Point2D( 2 * ((float)x/(float)WindowSize_.Width) - 0.5f,
                                              -8 * ((float)y/(float)WindowSize_.Height) + 4.0f);
                result.inscreenspace = false;
                return result;
            }

            public Point2D ToScreenSpace()
            {
                Point2D result = new Point2D( (0.5f * x + 0.25f) * WindowSize_.Width,
                                              ((-0.125f * y) + 0.5f) * WindowSize_.Height); //-0.125 = -1/8
                result.inscreenspace = true;
                return result;
            }

            // returns the drawing subsytems' version of a point for drawing.
            public System.Drawing.Point P()
            {
                return new System.Drawing.Point((int)ToScreenSpace().x, (int)ToScreenSpace().y);
            }
        };

        int assignment_;
        Method method_;
        Point2D MouseInWorld_;
        int SelectedIndex_;
        //Point2D ProjectedMouse_;
        static Size WindowSize_;
        List<List<int>> CachedPascalsTriangle_;
        List<List<Point2D>> SplineCoefficients_;

        List<Point2D> pts_; // the list of points used in internal algthms
        float tVal_; // t-value used for shell drawing
        int degree_; // degree of deboor subsplines
        int iterations_; //iterations for midpoint subdivision
        List<float> knot_; // knot sequence for deboor
        bool EdPtCont_; // end point continuity flag for std knot seq contruction
        Random rnd_; // random number generator
        


        // pickpt returns an index of the closest point to the passed in point
        //  -- usually a mouse position
        private int PickPt(Point2D m)
        {
            float closest = m % pts_[0];
            int closestIndex = 0;

            for (int i = 1; i < pts_.Count; ++i)
            {
                float dist = m % pts_[i];
                if (dist < closest)
                {
                    closest = dist;
                    closestIndex = i;
                }
            }

            return closestIndex;
        }

        private void Menu_Clear_Click(object sender, EventArgs e)
        {
            pts_.Clear();
            assignment_ = 0;
            method_ = Method.None;

            Refresh();
        }

        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MAT300_Resize(Object sender, EventHandler e)
        {
            // Set the size of button1 to the size of the client area of the form.
            WindowSize_ = this.ClientSize;
        }

        private void SetMousePosition(Point2D newpos)
        {
            MouseInWorld_ = new Point2D(newpos);
            /*
            ProjectedMouse_ = new Point2D(newpos);

            if (pts_.Count > 0 && assignment_ == 1)
            {
                if (method_ == Method.DeCastlejau)
                {
                    ProjectedMouse_ = DeCastlejau(MouseInWorld_.x);
                }
                else if (method_ == Method.Bernstein)
                {
                    ProjectedMouse_ = Bernstein(MouseInWorld_.x);
                }

                Refresh();
            }
            */
        }

        private void MAT300_MouseMove(object sender, MouseEventArgs e)
        {
            SetMousePosition(new Point2D(e.X, e.Y).ToWorldSpace());
            
            // if the right mouse button is being pressed
            if (pts_.Count != 0 && e.Button == MouseButtons.Right)
            {
                if(SelectedIndex_ == -1)
                {
                    // grab the closest point and snap it to the mouse
                    SelectedIndex_ = PickPt(MouseInWorld_);
                }
            }
            else if( e.Button == MouseButtons.None)
            {
                SelectedIndex_ = -1;
            }

            if(SelectedIndex_ != -1)
            {
                if(assignment_ == 1)
                {
                    pts_[SelectedIndex_].y = MouseInWorld_.y;
                }
                else
                {
                    pts_[SelectedIndex_] = MouseInWorld_;
                }
                Refresh();
            }
        }

        private void MAT300_MouseDown(object sender, MouseEventArgs e)
        {
            SetMousePosition(new Point2D(e.X, e.Y).ToWorldSpace());

            if(assignment_ != 1)
            {
                // if the left mouse button was clicked
                if (e.Button == MouseButtons.Left && SelectedIndex_ == -1)
                {
                    // add a new point to the controlPoints
                    pts_.Add(MouseInWorld_);

                    SelectedIndex_ = pts_.Count - 1;

                    if (Menu_DeBoor.Checked)
                    {
                        ResetKnotSeq();
                        UpdateKnotSeq();
                    }

                    Refresh();
                }

                // if there are points and the middle mouse button was pressed
                if (pts_.Count != 0 && e.Button == MouseButtons.Middle && assignment_ != 1)
                {
                    // then delete the closest point
                    int index = PickPt( MouseInWorld_ );

                    pts_.RemoveAt(index);

                    if (Menu_DeBoor.Checked)
                    {
                        ResetKnotSeq();
                        UpdateKnotSeq();
                    }

                    Refresh();
                }
            }
        }

        private void MAT300_MouseWheel(object sender, MouseEventArgs e)
        {
            // if the mouse wheel has moved
            if (e.Delta != 0)
            {
                // change the t-value for shell
                tVal_ += e.Delta / 120 * .02f;

                // handle edge cases
                tVal_ = (tVal_ < 0) ? 0 : tVal_;
                tVal_ = (tVal_ > 1) ? 1 : tVal_;

                Refresh();
            }
        }

        private void NUD_ValueChanged(object sender, EventArgs e)
        {
            if (pts_.Count == 0)
                return;

            switch(assignment_)
            {
                case 1:
                    degree_ = (int)NUD.Value;

                    NUD.Value = degree_;

                    pts_.Clear();

                    //Create Coefficient Points
                    for (int i = 0; i < degree_ + 1; ++i)
                    {
                        pts_.Add(new Point2D(((float)i / (float)degree_), 1.0f));
                    }
                    break;

                case 2:
                    if(method_ != Method.MidpointSubdivision)
                    {
                        tVal_ = (float)NUD.Value;
                        NUD.Value = (decimal)tVal_;
                    }
                    //else
                    //{
                    //    iterations_ = (int)NUD.Value;
                    //    NUD.Value = iterations_;
                    //}
                    break;
            }
            /*
            else if (Menu_DeBoor.Checked)
            {
                degree_ = (int)NUD.Value;

                ResetKnotSeq();
                UpdateKnotSeq();

                NUD.Value = degree_;
            }
            */

            Refresh();
        }

        private void CB_cont_CheckedChanged(object sender, EventArgs e)
        {
            EdPtCont_ = CB_cont.Checked;

            ResetKnotSeq();
            UpdateKnotSeq();

            Refresh();
        }

        private void Txt_knot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                // update knot seq
                string[] splits = Txt_knot.Text.ToString().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (splits.Length > pts_.Count + degree_ + 1)
                    return;

                knot_.Clear();
                foreach (string split in splits)
                {
                    knot_.Add(Convert.ToSingle(split));
                }

                for (int i = knot_.Count; i < (pts_.Count + degree_ + 1); ++i)
                    knot_.Add((float)(i - degree_));

                UpdateKnotSeq();
            }

            Refresh();
        }

        private void Menu_Polyline_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Menu_Points_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Menu_Shell_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void UpdateMethod(int newassignment, Method newmethod)
        {
            if(assignment_ == 1 || newassignment == 1)
            {
                pts_.Clear();
            }

            if(assignment_ == newassignment && method_ == newmethod)
            {
                assignment_ = 0;
                method_ = Method.None;
            }
            else
            {
                assignment_ = newassignment;
                method_ = newmethod;
            }

            switch(assignment_)
            {
                case 1:
                    //Create Coefficient Points
                    for (int i = 0; i < degree_ + 1; ++i)
                    {
                        pts_.Add(new Point2D(((float)i / (float)degree_), 1.0f));
                    }

                    switch(method_)
                    {
                        case Method.DeCastlejau:
                            Menu_Assignment1_DeCastlejau.Checked = !Menu_Assignment1_DeCastlejau.Checked;

                            Menu_Assignment1_Bernstein.Checked = false;
                            Menu_Assignment2_DeCastlejau.Checked = Menu_Assignment2_Bernstein.Checked = Menu_Assignment2_Midpoint.Checked = false;
                            
                            Menu_Polyline.Enabled = Menu_Polyline.Checked = true;
                            Menu_Points.Enabled = Menu_Points.Checked = true;
                            Menu_Shell.Enabled = Menu_Shell.Checked = true; 
                            break;

                        case Method.Bernstein:
                            Menu_Assignment1_Bernstein.Checked = !Menu_Assignment1_Bernstein.Checked;

                            Menu_Assignment1_DeCastlejau.Checked = false;
                            Menu_Assignment2_DeCastlejau.Checked = Menu_Assignment2_Bernstein.Checked = Menu_Assignment2_Midpoint.Checked = false;
                            
                            Menu_Polyline.Enabled = Menu_Polyline.Checked = true;
                            Menu_Points.Enabled = Menu_Points.Checked = true;
                            Menu_Shell.Enabled = Menu_Shell.Checked = true; 
                            break;
                    }
                    break;

                case 2:
                    switch (method_)
                    {
                        case Method.DeCastlejau:
                            Menu_Assignment2_DeCastlejau.Checked = !Menu_Assignment2_DeCastlejau.Checked;

                            Menu_Assignment1_DeCastlejau.Checked = Menu_Assignment1_Bernstein.Checked = false;
                            Menu_Assignment2_Bernstein.Checked = Menu_Assignment2_Midpoint.Checked = false;

                            Menu_Polyline.Enabled = Menu_Polyline.Checked = true;
                            Menu_Points.Enabled = Menu_Points.Checked = true;
                            Menu_Shell.Enabled = Menu_Shell.Checked = true; 
                            break;

                        case Method.Bernstein:
                            Menu_Assignment2_Bernstein.Checked = !Menu_Assignment2_Bernstein.Checked;
            
                            Menu_Assignment1_DeCastlejau.Checked = Menu_Assignment1_Bernstein.Checked = false;
                            Menu_Assignment2_DeCastlejau.Checked = Menu_Assignment2_Midpoint.Checked = false;

                            Menu_Polyline.Enabled = Menu_Polyline.Checked = true;
                            Menu_Points.Enabled = Menu_Points.Checked = true;
                            Menu_Shell.Enabled = Menu_Shell.Checked = false;
                            break;

                        case Method.MidpointSubdivision:
                            Menu_Assignment2_Midpoint.Checked = !Menu_Assignment2_Midpoint.Checked;

                            Menu_Assignment1_DeCastlejau.Checked = Menu_Assignment1_Bernstein.Checked = false;
                            Menu_Assignment2_DeCastlejau.Checked = Menu_Assignment2_Bernstein.Checked = false;
            
                            Menu_Polyline.Enabled = Menu_Polyline.Checked = true;
                            Menu_Points.Enabled = Menu_Points.Checked = true;
                            Menu_Shell.Enabled = Menu_Shell.Checked = false;
                            break;
                    }
                    break;

                case 3: 
                    Menu_Assignment3_Inter_Poly.Checked = !Menu_Assignment3_Inter_Poly.Checked;
                    
                    Menu_Assignment1_DeCastlejau.Checked = Menu_Assignment1_Bernstein.Checked = false;
                    Menu_Assignment2_DeCastlejau.Checked = Menu_Assignment2_Bernstein.Checked = Menu_Assignment2_Midpoint.Checked = false;
                    Menu_Assignment4_Inter_Splines.Checked = false;

                    Menu_Polyline.Enabled = Menu_Polyline.Checked = false;
                    Menu_Points.Enabled = Menu_Points.Checked = true;
                    Menu_Shell.Enabled = Menu_Shell.Checked = false;
                    break;

                case 4:
                    Menu_Assignment4_Inter_Splines.Checked = !Menu_Assignment4_Inter_Splines.Checked;

                    Menu_Assignment1_DeCastlejau.Checked = Menu_Assignment1_Bernstein.Checked = false;
                    Menu_Assignment2_DeCastlejau.Checked = Menu_Assignment2_Bernstein.Checked = Menu_Assignment2_Midpoint.Checked = false;
                    Menu_Assignment3_Inter_Poly.Checked = false;

                    Menu_Polyline.Enabled = Menu_Polyline.Checked = false;
                    Menu_Points.Enabled = Menu_Points.Checked = true;
                    Menu_Shell.Enabled = Menu_Shell.Checked = false;
                    break;

                case 5:
                    break;

                case 6:
                    break;
            }

            ToggleDeBoorHUD(method_ == Method.DeBoor);

            Refresh();
        }

        private void Menu_Assignment1_DeCastlejau_Click(object sender, EventArgs e)
        {
            UpdateMethod(1, Method.DeCastlejau);
        }

        private void Menu_Assignment1_Bernstein_Click(object sender, EventArgs e)
        {
            UpdateMethod(1, Method.Bernstein);
        }

        private void Menu_Assignment2_DeCastlejau_Click(object sender, EventArgs e)
        {
            UpdateMethod(2, Method.DeCastlejau);
        }

        private void Menu_Assignment2_Bernstein_Click(object sender, EventArgs e)
        {
            UpdateMethod(2, Method.Bernstein);
        }

        private void Menu_Assignment2_Midpoint_Click(object sender, EventArgs e)
        {
            UpdateMethod(2, Method.MidpointSubdivision);
        }

        private void Menu_Assignment3_Inter_Poly_Click(object sender, EventArgs e)
        {
            UpdateMethod(3, Method.Inter_Poly);
        }

        private void Menu_Assignment4_Inter_Splines_Click(object sender, EventArgs e)
        {
            UpdateMethod(4, Method.Inter_Spline);
        }

        /*
        private void Menu_Inter_Splines_Click(object sender, EventArgs e)
        {
            Menu_Assignment2.Checked = Menu_Bern.Checked = Menu_Midpoint.Checked = Menu_DeBoor.Checked = false;

            Menu_Assignment3_Inter_Poly.Checked = false;
            Menu_Inter_Splines.Checked = !Menu_Inter_Splines.Checked;

            Menu_Polyline.Enabled = Menu_Polyline.Checked = Menu_Shell.Enabled = Menu_Shell.Checked = false;
            Menu_Points.Enabled = true;

            ToggleDeBoorHUD(false);

            Refresh();
        }

        private void Menu_DeBoor_Click(object sender, EventArgs e)
        {
            Menu_Assignment2.Checked = Menu_Bern.Checked = Menu_Midpoint.Checked = false;

            Menu_Assignment3_Inter_Poly.Checked = Menu_Inter_Splines.Checked = false;

            Menu_DeBoor.Checked = !Menu_DeBoor.Checked;

            Menu_Polyline.Enabled = Menu_Points.Enabled = Menu_Shell.Enabled = true;

            ToggleDeBoorHUD(true);

            Refresh();
        }
        */

        private void DegreeClamp()
        {
            // handle edge cases
            degree_ = (degree_ > pts_.Count - 1) ? pts_.Count - 1 : degree_;
            degree_ = (degree_ < 1) ? 1 : degree_;
        }

        private void ResetKnotSeq( )
        {
            DegreeClamp();
            knot_.Clear();

            if (EdPtCont_)
            {
                for (int i = 0; i < degree_; ++i)
                    knot_.Add(0.0f);
                for (int i = 0; i <= (pts_.Count - degree_); ++i)
                    knot_.Add((float)i);
                for (int i = 0; i < degree_; ++i)
                    knot_.Add((float)(pts_.Count - degree_));
            }
            else
            {
                for (int i = -degree_; i <= (pts_.Count); ++i)
                    knot_.Add((float)i);
            }
        }

        private void UpdateKnotSeq()
        {
            Txt_knot.Clear();
            foreach (float knot in knot_)
            {
                Txt_knot.Text += knot.ToString() + " ";
            }
        }

        private void SetNUD()
        {
            switch(assignment_)
            {
                case 1:
                    NUD_label.Text = "&Degree";
                    NUD_label.TabIndex = 3;

                    NUD.TabIndex = 5;
                    NUD.DecimalPlaces = 0;
                    NUD.Increment = (decimal)1;
                    NUD.Minimum = (decimal)1;
                    NUD.Maximum = (decimal)20;
                    NUD.Value = (decimal)degree_;

                    NUD_label.Visible = true;
                    NUD.Visible = true;
                    break;

                case 2:
                    if(method_ == Method.DeCastlejau)
                    {
                        NUD_label.Text = "&T-Value";
                        NUD_label.TabIndex = 3;

                        NUD.TabIndex = 5;
                        NUD.DecimalPlaces = 2;
                        NUD.Increment = (decimal)0.01f;
                        NUD.Minimum = (decimal)0;
                        NUD.Maximum = (decimal)1;
                        NUD.Value = (decimal)tVal_;

                        NUD_label.Visible = true;
                        NUD.Visible = true;
                    }
                    //Used to debug Midpoint Subdivision
                    /*
                    else
                    {
                        NUD_label.Text = "&Iterations";
                        NUD_label.TabIndex = 3;

                        NUD.TabIndex = 5;
                        NUD.DecimalPlaces = 0;
                        NUD.Increment = (decimal)1;
                        NUD.Minimum = (decimal)1;
                        NUD.Maximum = (decimal)6;
                        NUD.Value = (decimal)4;
            
                        NUD_label.Visible = true;
                        NUD.Visible = true;
                    }
                    */
                    break;

                default:
                    NUD_label.Visible = false;
                    NUD.Visible = false;
                    break;
            }
            /*
            else if(Menu_DeBoor.Checked)
            {
                NUD_label.Text = "&Degree";
                NUD_label.TabIndex = 3;

                NUD.TabIndex = 5;
                NUD.DecimalPlaces = 0;
                NUD.Increment = (decimal)1;
                NUD.Minimum = (decimal)1;
                NUD.Maximum = (decimal)10;
                NUD.Value = (decimal)degree_;
 
                NUD_label.Visible = true;
                NUD.Visible = true;
            }
            */
        }

        private void ToggleDeBoorHUD( bool on )
        {
            SetNUD();

            // set up basic knot sequence
            if( on )
            {
                ResetKnotSeq();
                UpdateKnotSeq();
            }

            CB_cont.Visible = on;

            Lbl_knot.Visible = on;
            Txt_knot.Visible = on;
        }

        private void MAT300_Paint(object sender, PaintEventArgs e)
        {
            // pass the graphics object to the DrawScreen subroutine for processing
            DrawScreen(e.Graphics);
        }

        private void DrawScreen(System.Drawing.Graphics gfx)
        {
            // to prevent unecessary drawing
            if (pts_.Count == 0)
                return;

            // pens used for drawing elements of the display
            System.Drawing.Pen polyPen = new Pen(Color.Blue, 1.0f);
            System.Drawing.Pen shellPen = new Pen(Color.Red, 0.5f);
            System.Drawing.Pen splinePen = new Pen(Color.Black, 1.5f);

            if (Menu_Shell.Checked && Menu_Shell.Enabled)
            {
                // draw the shell
                DrawShell(gfx, shellPen, pts_, tVal_);
            }

            if (Menu_Polyline.Checked && Menu_Polyline.Enabled)
            {
                DrawPolyline(gfx, polyPen, pts_);
            }

            if (Menu_Points.Checked && Menu_Points.Enabled)
            {
                DrawPoints(gfx, polyPen, pts_);
            }

            if(assignment_ == 0)
            {
                return;
            }

            ///////////////////////////////////////////////////////////////////////////////
            // Drawing code for algorithms goes in here                                  //
            ///////////////////////////////////////////////////////////////////////////////

            // you can change these variables at will; i have just chosen there
            //  to be six sample points for every point placed on the screen
            float steps = pts_.Count * 6;
            float alpha = 1 / steps;

            //HUD drawing code
            Font arial = new Font("Arial", 12);
            int widthoffset, heightoffset;
            //bool somethingselected = true;
            //String DrawLabel;

            widthoffset = 10;
            heightoffset = 30;

            gfx.DrawString("Assignment " + assignment_.ToString() + ": " + method_.ToString(), arial, Brushes.Black, widthoffset, heightoffset);

            heightoffset += arial.Height;

            /*
            if (assignment_ == 1)
            {
                //Draws the mouse projected onto our curve
                gfx.DrawString("Mouse(" + ProjectedMouse_.x.ToString() + ", " + ProjectedMouse_.y.ToString() + ") ", arial, Brushes.Black, widthoffset, heightoffset);
                gfx.DrawEllipse(splinePen, ProjectedMouse_.P().X - 2.0f, ProjectedMouse_.P().Y - 2.0f, 4.0f, 4.0f);
                heightoffset += arial.Height;
            }
            */
            
            if(assignment_ == 1)
            {
                gfx.DrawString("Coefficients :" + pts_.Count.ToString(), arial, Brushes.Black, widthoffset, heightoffset);
            }
            else
            {
                gfx.DrawString("points: " + pts_.Count.ToString(), arial, Brushes.Black, widthoffset, heightoffset);
            
                if (pts_.Count > 0)
                {
                    widthoffset += 100;
                    gfx.DrawString("t-value: " + tVal_.ToString("F"), arial, Brushes.Black, widthoffset, heightoffset);

                    widthoffset += 150;
                    gfx.DrawString("t-step: " + alpha.ToString("F6"), arial, Brushes.Black, widthoffset, heightoffset);
                }
            }

            widthoffset = 10;
            heightoffset += arial.Height;

            if (assignment_ == 1)
            {
                for (int i = 0; i < pts_.Count; ++i)
                {
                    gfx.DrawString("A" + i.ToString() + ": " + pts_[i].y.ToString(), arial, Brushes.Black, widthoffset, heightoffset + i * arial.Height);
                }
            }
            else
            {
                for (int i = 0; i < pts_.Count; ++i)
                {
                    gfx.DrawString("points" + i.ToString() + ": " + pts_[i].ToString(), arial, Brushes.Black, widthoffset, heightoffset + i * arial.Height);
                }
            }

            ///////////////////////////////////////////////////////////////////////////////
            // Draw Axes for Assignment 11                                               //
            ///////////////////////////////////////////////////////////////////////////////

            if (assignment_ == 1)
            {
                //Draw Axes
                Point2D Origin = new Point2D(0.0f, 0.0f);
                Point2D XPos = new Point2D(1.0f, 0.0f);
                Point2D YPos = new Point2D(0.0f, 4.0f);
                Point2D YNeg = new Point2D(0.0f, -4.0f);


                gfx.DrawLine(polyPen, YNeg.P(), YPos.P());
                gfx.DrawLine(polyPen, Origin.P(), XPos.P());

                Point2D Tick1 = new Point2D(YPos);
                Point2D Tick2 = new Point2D(YPos);


                Tick1.x = -0.01f * (XPos.x - Origin.x);
                Tick2.x = 0.01f * (XPos.x - Origin.x);

                //Draw Tick Marks
                while (Tick1.y > YNeg.y)
                {
                    Tick2.y = Tick1.y;
                    gfx.DrawLine(polyPen, Tick1.P(), Tick2.P());
                    Tick1.y -= 1.0f;
                }

                Tick1.x = Tick2.x = XPos.x;
                Tick1.y = -0.01f * (YPos.y - YNeg.y);
                Tick2.y = 0.01f * (YPos.y - YNeg.y);

                gfx.DrawLine(polyPen, Tick1.P(), Tick2.P());
            }

            if (pts_.Count < 2)
            {
                return;
            }

            Point2D current_left, current_right;
            switch(method_)
            {
                case Method.DeCastlejau:
                    current_right = DeCastlejau(0);

                    for (float t = alpha; t < 1; t += alpha)
                    {
                        current_left = current_right;
                        current_right = DeCastlejau(t);
                        gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    }

                    current_left = current_right;
                    current_right = DeCastlejau(1);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    break;

                case Method.Bernstein:
                    current_right = Bernstein(0);

                    for (float t = alpha; t < 1; t += alpha)
                    {
                        current_left = current_right;
                        current_right = Bernstein(t);

                        gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    }

                    current_left = current_right;
                    current_right = Bernstein(1);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    break;

                case Method.MidpointSubdivision:
                    List<Point2D> points = GetMidpointSubdivision(iterations_, pts_);

                    for(int i = 0; i + 1 < points.Count; ++i)
                    {
                        gfx.DrawLine(splinePen, points[i].P(), points[i + 1].P());
                    }
                    //DrawMidpoint(gfx, splinePen, pts_, iterations_);
                    break;

                case Method.Inter_Poly:
                    current_right = PolyInterpolate(0);

                    for (float t = alpha; t < 1; t += alpha)
                    {
                      current_left = current_right;
                      current_right = PolyInterpolate(t);

                      gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    }

                    current_left = current_right;
                    current_right = PolyInterpolate(1);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    break;

                case Method.Inter_Spline:
                    // spline interpolation
                    current_right = new Point2D(SplineInterpolate(0));

                    for (float t = alpha; t < 1; t += alpha)
                    {
                        current_left = current_right;
                        current_right = SplineInterpolate(t);
                        gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                    }

                    gfx.DrawLine(splinePen, current_right.P(), SplineInterpolate(1).P());
                    break;

                case Method.DeBoor:
                    break;
            }
            
            ///////////////////////////////////////////////////////////////////////////////
            // Bezier Curves                                                             //
            ///////////////////////////////////////////////////////////////////////////////
            /*
            // deboor
            if (Menu_DeBoor.Checked && pts_.Count >= 2)
            {
                Point2D current_left;
                Point2D current_right = new Point2D(DeBoorAlgthm(knot_[degree_]));

                float lastT = knot_[knot_.Count - degree_ - 1] - alpha;
                for (float t = alpha; t < lastT; t += alpha)
                {
                    current_left = current_right;
                    current_right = DeBoorAlgthm(t);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                }

                gfx.DrawLine(splinePen, current_right.P(), DeBoorAlgthm(lastT).P());
            }
            */

            ///////////////////////////////////////////////////////////////////////////////
            // Drawing code end                                                          //
            ///////////////////////////////////////////////////////////////////////////////
        }

        private void DrawPoint(System.Drawing.Graphics gfx, System.Drawing.Pen pen, Point2D pt)
        {
            gfx.DrawEllipse(pen, pt.P().X - 2.0f, pt.P().Y - 2.0f, 4.0f, 4.0f);
        }

        private void DrawPoints(System.Drawing.Graphics gfx, System.Drawing.Pen pen, List<Point2D> pts)
        {
            // draw the control points
            for(int i = 0; i < pts_.Count; ++i)
            {
                DrawPoint(gfx, pen, pts[i]);
            }
        }

        private void DrawPolyline(System.Drawing.Graphics gfx, System.Drawing.Pen pen, List<Point2D> pts)
        {
            // draw the control poly
            for (int i = 1; i < pts.Count; ++i)
            {
                gfx.DrawLine(pen, pts[i - 1].P(), pts[i].P());
            }
        }

        private void DrawShell(System.Drawing.Graphics gfx, System.Drawing.Pen pen, List<Point2D> pts, float t)
        {
            if (pts.Count < 3 || t == 0.0f || t == 1.0f)
            {
                return;
            }
            else
            {
                float tcomplement = 1.0f - t;

                List<Point2D> oldpoints, newpoints;
                oldpoints = new List<Point2D>(pts_);
                newpoints = new List<Point2D>();

                while(oldpoints.Count > 1)
                {
                    newpoints.Clear();
                    for(int i = 0; i + 1 < oldpoints.Count; ++i)
                    {
                        newpoints.Add( lerp(oldpoints[i], oldpoints[i + 1], t) );
                    }
                    DrawPolyline(gfx, pen, newpoints);
                    oldpoints = new List<Point2D>(newpoints);
                }
            }
        }

        private Point2D Gamma(int start, int end, float t)
        {
            return new Point2D(0, 0);
        }

        // Precise method which guarantees v = v1 when t = 1.
        private Point2D lerp(Point2D P0, Point2D P1, float t)
        {
            return new Point2D((1.0f - t) * P0 + t * P1);
        }

        private float lerp(float lhs, float rhs, float t)
        {
            return (1.0f - t) * lhs + t * rhs;
        }

        private float BernsteinPolynomial(float t, int d, int i)
        {
            if (i < 0 || i > d)
                return 0.0f;
            else
                return (float)(GetPascalBinomialCoeff(d, i) * System.Math.Pow((1.0f - t), d - (i + 1)) * System.Math.Pow(t, i));
        }

        private float TPF(float t, float c, int d)
        {
            if (t < c)
                return 0.0f;
            else
                return (float)System.Math.Pow((t - c), d);
        }

        /*
        private float DeCastlejauP(float t)
        {
            float tcomplement = 1.0f - t;

            List<Point2D> oldpoints, newpoints;
            oldpoints = new List<Point2D>(pts_);
            newpoints = new List<Point2D>();
            
            while (oldpoints.Count > 1)
            {
                newpoints.Clear();
                for (int i = 0; i + 1 < oldpoints.Count; ++i)
                {
                    newpoints.Add( new Point2D(t, lerp(oldpoints[i].y, oldpoints[i + 1].y, t)) );
                }
                oldpoints = new List<Point2D>(newpoints);
            }

            return oldpoints[0].y;
        }
         * 
        private float BernsteinP(float t)
        {
            float tcomplement = 1.0f - t;
            float Result = 0;
            float binomialcoefficient;

            for(int i = 0; i < pts_.Count; ++i)
            {
                binomialcoefficient = GetPascalBinomialCoeff(pts_.Count, i);
                Result += (float)(pts_[i].y * binomialcoefficient * System.Math.Pow(tcomplement, pts_.Count - (1 + i)) * System.Math.Pow(t, i));
            }
            return Result;
        }
        */

        private Point2D DeCastlejau(float t)
        {
            Point2D Result;
            if(t == 0.0f)
            {
                Result = new Point2D(pts_[0]);
            }
            else if(t == 1.0f)
            {
                Result = new Point2D(pts_[pts_.Count - 1]);
            }
            else
            {
                float tcomplement = 1.0f - t;

                List<Point2D> oldpoints, newpoints;
                oldpoints = new List<Point2D>(pts_);
                newpoints = new List<Point2D>();

                while(oldpoints.Count > 1)
                {
                    newpoints.Clear();
                    for(int i = 0; i + 1 < oldpoints.Count; ++i)
                    {
                        newpoints.Add( lerp(oldpoints[i], oldpoints[i + 1], t) );
                    }
                    oldpoints = new List<Point2D>(newpoints);
                }

                Result = new Point2D(oldpoints[0]);
            }

            if(assignment_ == 1)
            {
                Result.x = t;
            }

            return Result;
        }

        private int GetPascalBinomialCoeff(int d, int i)
        {
            System.Diagnostics.Debug.Assert(!(d - 1 < 0) && !(i > d));

            if( !(CachedPascalsTriangle_.Count < d) && CachedPascalsTriangle_[d-1].Count > i)
            {
                return CachedPascalsTriangle_[d - 1][i];
            }
            else if (i == 0 || i == d - 1)
            {
                return 1;
            }
            else
            {
                return GetPascalBinomialCoeff(d - 1, i - 1) + GetPascalBinomialCoeff(d - 1, i);
            }
        }

        
        private Point2D Bernstein(float t)
        {
            int d = pts_.Count;
            int i = 0;
            Point2D Result = new Point2D(BernsteinPolynomial(t, d, i) * pts_[0]);

            for(i = 1; i < pts_.Count; ++i)
            {
                Result += BernsteinPolynomial(t, d, i) * pts_[i];
            }

            if(assignment_ == 1)
            {
                Result.x = t;
            }

            return Result;
        }

        private const float MAX_DIST = 6.0F;

        private Point2D GetMidpoint(Point2D left, Point2D right)
        {
            return new Point2D(left + 0.5f * (right - left));
        }

        private List<Point2D> GetMidpoints(List<Point2D> cPs)
        {
            List<Point2D> cMidpoints = new List<Point2D>();

            for (int i = 0; i + 1 < cPs.Count; ++i)
            {
                cMidpoints.Add( GetMidpoint(cPs[i], cPs[i+1]) );
            }

            return cMidpoints;
        }

        private List<Point2D> GetMidpointSubdivision(int iterations, List<Point2D> cPs)
        {
            if (iterations == 0)
            {
                return cPs;
            }
            else
            {
                List<Point2D> left = new List<Point2D>();
                List<Point2D> right = new List<Point2D>();
                List<Point2D> points = new List<Point2D>();
                List<Point2D> midpoints = new List<Point2D>(cPs);
                while (midpoints.Count > 1)
                {
                    left.Add(midpoints[0]);
                    right.Insert(0, midpoints[midpoints.Count - 1]);
                    midpoints = GetMidpoints(midpoints);
                }

                left.Add(midpoints[0]);
                right.Insert(0, midpoints[0]);

                --iterations;
                left = GetMidpointSubdivision(iterations, left);
                right = GetMidpointSubdivision(iterations, right);
                right.RemoveAt(0);  //Remove the common point

                points.AddRange(left);
                points.AddRange(right);

                return points;
            }
        }

        private void DrawMidpoint(System.Drawing.Graphics gfx, System.Drawing.Pen pen, List<Point2D> cPs, int iterations)
        {
            if (cPs.Count < 2)
                return;
           
            List<Point2D> points = GetMidpointSubdivision(iterations, cPs);

            for(int i = 0; i + 1 < points.Count; ++i)
            {
                gfx.DrawLine(pen, points[i].P(), points[i + 1].P());
            }
        }


        private Point2D PolyInterpolate(float p_t)
        {
          float t = p_t * (pts_.Count - 1);
          int i;
          Point2D Result = new Point2D(0.0f, 0.0f); ;
          List<List<double>> DDTX = new List<List<double>>();
          List<List<double>> DDTY = new List<List<double>>();
          
          DDTX.Add(new List<double>());
          DDTY.Add(new List<double>());

          for (i = 0; i < pts_.Count; ++i)
          {
            DDTX[0].Add(pts_[i].x);
            DDTY[0].Add(pts_[i].y);
          }

          for (i = 1; i < pts_.Count; ++i)
          {
            DDTX.Add(new List<double>());
            DDTY.Add(new List<double>());
            
            for (int j = 0; j < DDTX[i - 1].Count - 1; ++j)
            {
              DDTX[i].Add((DDTX[i - 1][j + 1] - DDTX[i - 1][j]) / i);
              DDTY[i].Add((DDTY[i - 1][j + 1] - DDTY[i - 1][j]) / i);
            }
          }

          i = 0;
          double value = 1.0f;
          double x, y;
          x = y = 0.0f;
          do
          {
            x += DDTX[i][0] * value;
            y += DDTY[i][0] * value;
            value *= (t - i);
            ++i;
          }
          while (i < pts_.Count);

          Result = new Point2D((float)x, (float)y);
          return Result;
        }

        private Point2D SplineInterpolate(float t)
        {
            //##TODO##
            Point2D Result = new Point2D(0.0f, 0.0f);

            int k = 0;
            int n = pts_.Count;
            t = t * n;
            int c = 0;

            float tempterm;

            double[,] matrix = new double[n+2, n+2];
            
            SplineCoefficients_ = new List<List<Point2D>>();
            for(int i = 0; i < n + 2; ++i)
            {
                SplineCoefficients_.Add(new List<Point2D>());
                if(i < n)
                {
                    for (int j = 0; j < n + 2; ++j)
                    {
                        if(j < 4)
                        {
                            tempterm = (float)System.Math.Pow(k, j);
                        }
                        else
                        {
                            ++c;
                            tempterm = TPF(k, c, 3);
                        }
                        SplineCoefficients_[i].Add( new Point2D(tempterm, tempterm) );
                    }
                }
                else //Handling of double prime p''(0) = p''(t) = 0 
                {
                    for (int j = 0; j < n + 2; ++j)
                    {
                        if( j < 2)
                        {
                            tempterm = 0.0f;
                        }
                        else
                        {
                            if( j == 3 && t == 0.0f)
                            {
                                tempterm = (float)(j * (j - 1) * System.Math.Pow(1, j - 2));
                            }
                            else
                            {
                                tempterm = (float)(j * (j - 1) * System.Math.Pow(t, j-2));
                            }
                        } 
                        SplineCoefficients_[i].Add(new Point2D(tempterm, tempterm));
                    }
                }
                ++k;
            }

            alglib.densesolverreport report;
            int info;
            double[] b = new double[n + 2];
            double[] xcoeff , ycoeff;

            for (int i = 0; i < n + 2; ++i)
            {
                if(i < n)
                    b[i] = pts_[i].x;
                else
                    b[i] = 0.0f;
                
                for(int j = 0; j < n + 2; ++j)
                {
                    matrix[i, j] = (double)SplineCoefficients_[i][j].x;
                }
            }

            alglib.rmatrixsolve(matrix, n + 2, b, out info, out report, out xcoeff);

            if(info == -3 && t == 0)
            {
                for(int i = 0; i < n + 2; ++i)
                {
                    xcoeff[i] = 1;
                }
            }

            for (int i = 0; i < n + 2; ++i)
            {
                if (i < n)
                    b[i] = pts_[i].y;
                else
                    b[i] = 0.0f;

                for (int j = 0; j < n + 2; ++j)
                {
                    matrix[i, j] = (double)SplineCoefficients_[i][j].y;
                }
            }

            alglib.rmatrixsolve( matrix, n+2, b, out info, out report, out ycoeff);
            
            if(info == -3 && t == 0)
            {
                for (int i = 0; i < n + 2; ++i)
                {
                    xcoeff[i] = 1;
                }
            }

            for(int i = 0; i < n + 2; ++i)
            {
                if(i < 4)
                {
                    Result.x += (float)(xcoeff[i] * System.Math.Pow(t, i));
                    Result.y += (float)(ycoeff[i] * System.Math.Pow(t, i));
                }
                else
                {
                    ++c;
                    Result.x += (float)(xcoeff[i] * TPF(t, c, 3));
                    Result.y += (float)(ycoeff[i] * TPF(t, c, 3));
                }
            }

            return Result;
        }

        private Point2D DeBoorAlgthm(float t)
        {
            return new Point2D(0, 0);
        }
    }
}