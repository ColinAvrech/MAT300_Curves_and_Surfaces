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
        public MAT300()
        {
            InitializeComponent();

            pts_ = new List<Point2D>();
            tVal_ = 0.5F;
            degree_ = 0;
            knot_ = new List<float>();
            EdPtCont_ = true;
            rnd_ = new Random();
        }

        // Point class for general math use
        protected class Point2D : System.Object
        {
            public float x;
            public float y;

            public Point2D(float _x, float _y)
            {
                x = _x;
                y = _y;
            }

            public Point2D(Point2D rhs)
            {
                x = rhs.x;
                y = rhs.y;
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

            // returns the drawing subsytems' version of a point for drawing.
            public System.Drawing.Point P()
            {
                return new System.Drawing.Point((int)x, (int)y);
            }
        };

        List<Point2D> pts_; // the list of points used in internal algthms
        float tVal_; // t-value used for shell drawing
        int degree_; // degree of deboor subsplines
        List<float> knot_; // knot sequence for deboor
        bool EdPtCont_; // end point continuity flag for std knot seq contruction
        Random rnd_; // random number generator

        int iterations_;

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
            Refresh();
        }

        private void Menu_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MAT300_MouseMove(object sender, MouseEventArgs e)
        {
            // if the right mouse button is being pressed
            if (pts_.Count != 0 && e.Button == MouseButtons.Right)
            {
                // grab the closest point and snap it to the mouse
                int index = PickPt(new Point2D(e.X, e.Y));

                pts_[index].x = e.X;
                pts_[index].y = e.Y;

                Refresh();
            }
        }

        private void MAT300_MouseDown(object sender, MouseEventArgs e)
        {
            // if the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // add a new point to the controlPoints
                pts_.Add(new Point2D(e.X, e.Y));

                if (Menu_DeBoor.Checked)
                {
                    ResetKnotSeq();
                    UpdateKnotSeq();
                }

                Refresh();
            }

            // if there are points and the middle mouse button was pressed
            if (pts_.Count != 0 && e.Button == MouseButtons.Middle)
            {
                // then delete the closest point
                int index = PickPt(new Point2D(e.X, e.Y));

                pts_.RemoveAt(index);

                if (Menu_DeBoor.Checked)
                {
                    ResetKnotSeq();
                    UpdateKnotSeq();
                }

                Refresh();
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

        private void TValueUD_ValueChanged(object sender, EventArgs e)
        {
            if (pts_.Count == 0)
                return;

            tVal_ = (float)TValueUD.Value;

            TValueUD.Value = (decimal)tVal_;

            Refresh();
        }

        private void IterationsUD_ValueChanged(object sender, EventArgs e)
        {
            if (pts_.Count == 0)
                return;

            iterations_ = (int)IterationsUD.Value;

            IterationsUD.Value = iterations_;

            Refresh();
        }

        private void NUD_degree_ValueChanged(object sender, EventArgs e)
        {
            if (pts_.Count == 0)
                return;

            degree_ = (int)NUD_degree.Value;

            ResetKnotSeq();
            UpdateKnotSeq();

            NUD_degree.Value = degree_;

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

        private void Menu_DeCast_Click(object sender, EventArgs e)
        {
            Menu_DeCast.Checked = !Menu_DeCast.Checked;
            Menu_Bern.Checked = Menu_Midpoint.Checked = Menu_DeBoor.Checked = false;

            Menu_Inter_Poly.Checked = Menu_Inter_Splines.Checked = false;

            Menu_Polyline.Enabled = Menu_Points.Enabled = Menu_Shell.Enabled = true;

            ToggleTHUD(true);
            ToggleIterationsHUD(false);
            ToggleDeBoorHUD(false);

            Refresh();
        }

        private void Menu_Bern_Click(object sender, EventArgs e)
        {
            Menu_Bern.Checked = !Menu_Bern.Checked;
            Menu_DeCast.Checked = Menu_Midpoint.Checked = Menu_DeBoor.Checked = false;

            Menu_Inter_Poly.Checked = Menu_Inter_Splines.Checked = false;

            Menu_Polyline.Enabled = Menu_Points.Enabled = Menu_Shell.Enabled = true;

            ToggleTHUD(true);
            ToggleIterationsHUD(false);
            ToggleDeBoorHUD(false);

            Refresh();
        }

        private void Menu_Midpoint_Click(object sender, EventArgs e)
        {
            Menu_Midpoint.Checked = !Menu_Midpoint.Checked;
            Menu_DeCast.Checked = Menu_Bern.Checked = Menu_DeBoor.Checked = false;

            Menu_Inter_Poly.Checked = Menu_Inter_Splines.Checked = false;

            Menu_Polyline.Enabled = Menu_Points.Enabled = Menu_Shell.Enabled = true;

            ToggleTHUD(false);
            ToggleIterationsHUD(true);
            ToggleDeBoorHUD(false);

            Refresh();
        }

        private void Menu_Inter_Poly_Click(object sender, EventArgs e)
        {
            Menu_DeCast.Checked = Menu_Bern.Checked = Menu_Midpoint.Checked = Menu_DeBoor.Checked = false;

            Menu_Inter_Poly.Checked = !Menu_Inter_Poly.Checked;
            Menu_Inter_Splines.Checked = false;


            Menu_Polyline.Enabled = Menu_Polyline.Checked = Menu_Shell.Enabled = Menu_Shell.Checked = false;
            Menu_Points.Enabled = true;

            ToggleDeBoorHUD(false);

            Refresh();
        }

        private void Menu_Inter_Splines_Click(object sender, EventArgs e)
        {
            Menu_DeCast.Checked = Menu_Bern.Checked = Menu_Midpoint.Checked = Menu_DeBoor.Checked = false;

            Menu_Inter_Poly.Checked = false;
            Menu_Inter_Splines.Checked = !Menu_Inter_Splines.Checked;

            Menu_Polyline.Enabled = Menu_Polyline.Checked = Menu_Shell.Enabled = Menu_Shell.Checked = false;
            Menu_Points.Enabled = true;

            ToggleDeBoorHUD(false);

            Refresh();
        }

        private void Menu_DeBoor_Click(object sender, EventArgs e)
        {
            Menu_DeCast.Checked = Menu_Bern.Checked = Menu_Midpoint.Checked = false;

            Menu_Inter_Poly.Checked = Menu_Inter_Splines.Checked = false;

            Menu_DeBoor.Checked = !Menu_DeBoor.Checked;

            Menu_Polyline.Enabled = Menu_Points.Enabled = Menu_Shell.Enabled = true;

            ToggleDeBoorHUD(true);

            Refresh();
        }

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

        private void ToggleIterationsHUD( bool on )
        {
            label2.Visible = on;
            IterationsUD.Visible = on;
        }

        private void ToggleTHUD( bool on)
        {
            label1.Visible = on;
            TValueUD.Visible = on;
        }

        private void ToggleDeBoorHUD( bool on )
        {
            // set up basic knot sequence
            if( on )
            {
                ResetKnotSeq();
                UpdateKnotSeq();
            }

            CB_cont.Visible = on;

            Lbl_knot.Visible = on;
            Txt_knot.Visible = on;

            Lbl_degree.Visible = on;
            NUD_degree.Visible = on;
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
            System.Drawing.Pen polyPen = new Pen(Color.Gray, 1.0f);
            System.Drawing.Pen shellPen = new Pen(Color.LightGray, 0.5f);
            System.Drawing.Pen splinePen = new Pen(Color.Black, 1.5f);

            if (Menu_Shell.Checked)
            {
                // draw the shell
                DrawShell(gfx, shellPen, pts_, tVal_);
            }

            if (Menu_Polyline.Checked)
            {
                // draw the control poly
                for (int i = 1; i < pts_.Count; ++i)
                {
                    gfx.DrawLine(polyPen, pts_[i - 1].P(), pts_[i].P());
                }
            }

            if (Menu_Points.Checked)
            {
                // draw the control points
                foreach (Point2D pt in pts_)
                {
                    gfx.DrawEllipse(polyPen, pt.x - 2.0F, pt.y - 2.0F, 4.0F, 4.0F);
                }
            }

            // you can change these variables at will; i have just chosen there
            //  to be six sample points for every point placed on the screen
            float steps = pts_.Count * 6;
            float alpha = 1 / steps;

            ///////////////////////////////////////////////////////////////////////////////
            // Drawing code for algorithms goes in here                                  //
            ///////////////////////////////////////////////////////////////////////////////

            // DeCastlejau algorithm
            if (Menu_DeCast.Checked)
            {
                Point2D current_left;
                Point2D current_right = new Point2D(DeCastlejau(0));

                for (float t = alpha; t < 1; t += alpha)
                {
                    current_left = current_right;
                    current_right = DeCastlejau(t);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                }

                gfx.DrawLine(splinePen, current_right.P(), DeCastlejau(1).P());
            }

            // Bernstein polynomial
            if (Menu_Bern.Checked)
            {
                Point2D current_left;
                Point2D current_right = new Point2D(Bernstein(0));

                for (float t = alpha; t < 1; t += alpha)
                {
                    current_left = current_right;
                    current_right = Bernstein(t);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                }

                gfx.DrawLine(splinePen, current_right.P(), Bernstein(1).P());
            }

            // Midpoint algorithm
            if (Menu_Midpoint.Checked)
            {
                DrawMidpoint(gfx, splinePen, pts_, (int)IterationsUD.Value);
            }

            // polygon interpolation
            if (Menu_Inter_Poly.Checked)
            {
                Point2D current_left;
                Point2D current_right = new Point2D(PolyInterpolate(0));

                for (float t = alpha; t < 1; t += alpha)
                {
                    current_left = current_right;
                    current_right = PolyInterpolate(t);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                }

                gfx.DrawLine(splinePen, current_right.P(), PolyInterpolate(1).P());
            }

            // spline interpolation
            if (Menu_Inter_Splines.Checked)
            {
                Point2D current_left;
                Point2D current_right = new Point2D(SplineInterpolate(0));

                for (float t = alpha; t < 1; t += alpha)
                {
                    current_left = current_right;
                    current_right = SplineInterpolate(t);
                    gfx.DrawLine(splinePen, current_left.P(), current_right.P());
                }

                gfx.DrawLine(splinePen, current_right.P(), SplineInterpolate(1).P());
            }

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

            ///////////////////////////////////////////////////////////////////////////////
            // Drawing code end                                                          //
            ///////////////////////////////////////////////////////////////////////////////


            // Heads up Display drawing code

            Font arial = new Font("Arial", 12);

            if (Menu_DeCast.Checked)
            {
                gfx.DrawString("DeCasteljau", arial, Brushes.Black, 0, 30);
            }
            else if (Menu_Midpoint.Checked)
            {
                gfx.DrawString("Midpoint", arial, Brushes.Black, 0, 30);
            }
            else if (Menu_Bern.Checked)
            {
                gfx.DrawString("Bernstein", arial, Brushes.Black, 0, 30);
            }
            else if (Menu_DeBoor.Checked)
            {
                gfx.DrawString("DeBoor", arial, Brushes.Black, 0, 30);
            }

            gfx.DrawString("t-value: " + tVal_.ToString("F"), arial, Brushes.Black, 100, 30);

            gfx.DrawString("t-step: " + alpha.ToString("F6"), arial, Brushes.Black, 300, 30);

            gfx.DrawString(pts_.Count.ToString(), arial, Brushes.Black, 750, 30);
        }

        private void DrawShell(System.Drawing.Graphics gfx, System.Drawing.Pen pen, List<Point2D> pts, float t)
        {
            Point2D temppt;
            List<Point2D> points = new List<Point2D>(pts);
            List<Point2D> shellpts = new List<Point2D>();

            while(points.Count > 1)
            {
                for (int i = 0; i + 1 < shellpts.Count; ++i)
                {
                    temppt = (1 - t) * points[i] + t * points[i + 1];
                    gfx.DrawEllipse(pen, temppt.x - 2.0F, temppt.y - 2.0F, 4.0F, 4.0F);
                    shellpts.Add(temppt);
                }
            
                for (int i = 0; i + 1 < shellpts.Count; ++i)
                {
                    gfx.DrawLine(pen, shellpts[i].P(), shellpts[i + 1].P());
                }

                points = shellpts;
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

        private Point2D DeCastlejau(float t)
        {
            if(t == 0.0f)
            {
                return pts_[0];
            }
            else if(t == 1.0f)
            {
                return pts_[pts_.Count - 1];
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

                return oldpoints[0];
            }
        }

        private int GetPascalBinomialCoeff(int d, int i)
        {
            System.Diagnostics.Debug.Assert(!(d < 0) && !(i > d));

            if (i == 0 || i == d - 1)
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
            float tcomplement = 1.0f - t;

            int binomialcoefficient = GetPascalBinomialCoeff(pts_.Count, 0);
            Point2D Result = (float)(binomialcoefficient * System.Math.Pow(tcomplement, pts_.Count - 1) * System.Math.Pow(t, 0)) * pts_[0];

            for(int i = 1; i < pts_.Count; ++i)
            {
                binomialcoefficient = GetPascalBinomialCoeff(pts_.Count, i);
                Result += (float)(binomialcoefficient * System.Math.Pow(tcomplement, pts_.Count - (1 + i)) * System.Math.Pow(t, i)) * pts_[i];
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

        private Point2D PolyInterpolate(float t)
        {
            return new Point2D(0, 0);
        }

        private Point2D SplineInterpolate(float t)
        {
            return new Point2D(0, 0);
        }

        private Point2D DeBoorAlgthm(float t)
        {
            return new Point2D(0, 0);
        }
    }
}