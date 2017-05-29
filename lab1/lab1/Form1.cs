using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace lab1
{
    public partial class Form : System.Windows.Forms.Form
    {
        public int alpha = 255;
        public Cmy cmy;
        public Hsv hsv;
        public Luv luv;
        public Rgb rgb;
        public Xyz xyz;

        public Form()
        {
            InitializeComponent();
            alpha = alphaS.Value;
            rgb = new Rgb {R = redS.Value, G = greenS.Value, B = blueS.Value};
            hsv = rgb.To<Hsv>();
            luv = rgb.To<Luv>();
            cmy = rgb.To<Cmy>();
            xyz = rgb.To<Xyz>();
        }


        public void LUVvalueChanged()
        {
            luv = new Luv {L = LS.Value, U = US.Value, V = V2S.Value};
            if (luv.L == 0)
                rgb = new Rgb {R = 0, G = 0, B = 0};
            else
                rgb = luv.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            LS.Value = (int) luv.L;
            US.Value = (int) luv.U;
            V2S.Value = (int) luv.V;
            ll.Text = string.Format("L({0:000})", luv.L);
            ul.Text = string.Format("U({0:000})", luv.U);
            v2l.Text = string.Format("V({0:000})", luv.V);
        }

        public void LUVvalueChanged2()
        {
            //   luv = new Luv { L = LS.Value, U = US.Value, V = V2S.Value };
            if (luv.L == 0)
                rgb = new Rgb {R = 0, G = 0, B = 0};
            else
                rgb = luv.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            LS.Value = (int) luv.L;
            US.Value = (int) luv.U;
            V2S.Value = (int) luv.V;
            ll.Text = string.Format("L({0:000})", luv.L);
            ul.Text = string.Format("U({0:000})", luv.U);
            v2l.Text = string.Format("V({0:000})", luv.V);
        }

        public void RGBvalueChanged()
        {
            rgb = new Rgb {R = redS.Value, G = greenS.Value, B = blueS.Value};
            redS.Value = (int) rgb.R;
            greenS.Value = (int) rgb.G;
            blueS.Value = (int) rgb.B;
            alphaS.Value = alpha;
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            gl.Text = string.Format("G({0:000})", rgb.G);
            bl.Text = string.Format("B({0:000})", rgb.B);
            rl.Text = string.Format("R({0:000})", rgb.R);
            al.Text = string.Format("A({0:000})", alpha);
            hsv = rgb.To<Hsv>();
            luv = rgb.To<Luv>();
            cmy = rgb.To<Cmy>();
            xyz = rgb.To<Xyz>();
        }

        public void RGBvalueChanged2()
        {
            redS.Value = (int) rgb.R;
            greenS.Value = (int) rgb.G;
            blueS.Value = (int) rgb.B;
            alphaS.Value = alpha;
            rgb = new Rgb {R = redS.Value, G = greenS.Value, B = blueS.Value};
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            gl.Text = string.Format("G({0:000})", rgb.G);
            bl.Text = string.Format("B({0:000})", rgb.B);
            rl.Text = string.Format("R({0:000})", rgb.R);
            al.Text = string.Format("A({0:000})", alpha);
            hsv = rgb.To<Hsv>();
            luv = rgb.To<Luv>();
            cmy = rgb.To<Cmy>();
            xyz = rgb.To<Xyz>();
        }

        public void HSVvalueChanged()
        {
            double multiplier = SS.Maximum;
            hsv = new Hsv {H = HS.Value, S = SS.Value/multiplier, V = VS.Value/multiplier};
            rgb = hsv.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            hl.Text = string.Format("H({0:000})", hsv.H);
            sl.Text = string.Format("S({0:F2})", hsv.S);
            vl.Text = string.Format("V({0:F2})", hsv.V);
            HS.Value = (int) hsv.H;
            SS.Value = (int) (hsv.S*SS.Maximum);
            VS.Value = (int) (hsv.V*VS.Maximum);
        }

        public void HSVvalueChanged2()
        {
            rgb = hsv.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            hl.Text = string.Format("H({0:000})", hsv.H);
            sl.Text = string.Format("S({0:F2})", hsv.S);
            vl.Text = string.Format("V({0:F2})", hsv.V);
            HS.Value = (int) hsv.H;
            SS.Value = (int) (hsv.S*SS.Maximum);
            VS.Value = (int) (hsv.V*VS.Maximum);
        }

        public void CMYvalueChanged()
        {
            double multiplier = SS.Maximum;
            cmy = new Cmy {C = CS.Value/multiplier, M = MS.Value/multiplier, Y = YS.Value/multiplier};
            rgb = cmy.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            cl.Text = string.Format("C({0:F2})", cmy.C);
            ml.Text = string.Format("M({0:F2})", cmy.M);
            yl.Text = string.Format("Y({0:F2})", cmy.Y);
            CS.Value = (int) (cmy.C*CS.Maximum);
            MS.Value = (int) (cmy.M*MS.Maximum);
            YS.Value = (int) (cmy.Y*YS.Maximum);
        }

        public void CMYvalueChanged2()
        {
            rgb = cmy.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            cl.Text = string.Format("C({0:F2})", cmy.C);
            ml.Text = string.Format("M({0:F2})", cmy.M);
            yl.Text = string.Format("Y({0:F2})", cmy.Y);
            CS.Value = (int) (cmy.C*CS.Maximum);
            MS.Value = (int) (cmy.M*MS.Maximum);
            YS.Value = (int) (cmy.Y*YS.Maximum);
        }

        public void XYZvalueChanged()
        {
            xyz = new Xyz {X = XC.Value, Y = Y2C.Value, Z = ZC.Value};
            rgb = xyz.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            xl.Text = string.Format("X({0:F2})", xyz.X);
            y2l.Text = string.Format("Y({0:F2})", xyz.Y);
            zl.Text = string.Format("Z({0:F2})", xyz.Z);
            XC.Value = (int) xyz.X;
            Y2C.Value = (int) xyz.Y;
            ZC.Value = (int) xyz.Z;
        }

        public void XYZvalueChanged2()
        {
            rgb = xyz.To<Rgb>();
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            xl.Text = string.Format("X({0:F2})", xyz.X);
            y2l.Text = string.Format("Y({0:F2})", xyz.Y);
            zl.Text = string.Format("Z({0:F2})", xyz.Z);
            XC.Value = (int) xyz.X;
            Y2C.Value = (int) xyz.Y;
            ZC.Value = (int) xyz.Z;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            BOX.Update();
            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, Color.White, Color.Black, 90f);  
        }

        private void BOX_VisibleChanged(object sender, EventArgs e)
        {
            rgb = new Rgb {R = redS.Value, G = greenS.Value, B = blueS.Value};
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
        }

        private void HS_Scroll(object sender, EventArgs e)
        {
            HSVvalueChanged();
            RGBvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void SS_Scroll(object sender, EventArgs e)
        {
            HSVvalueChanged();
            RGBvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void VS_Scroll(object sender, EventArgs e)
        {
            HSVvalueChanged();
            RGBvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void redS_Scroll(object sender, EventArgs e)
        {
            RGBvalueChanged();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void greenS_Scroll(object sender, EventArgs e)
        {
            RGBvalueChanged();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void blueS_Scroll(object sender, EventArgs e)
        {
            RGBvalueChanged();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void alphaS_Scroll(object sender, EventArgs e)
        {
            alpha = alphaS.Value;
            BOX.BackColor = Color.FromArgb(alpha, (int) rgb.R, (int) rgb.G, (int) rgb.B);
            RGBvalueChanged();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void LS_Scroll(object sender, EventArgs e)
        {
            LUVvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void US_Scroll(object sender, EventArgs e)
        {
           
            LUVvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void V2S_Scroll(object sender, EventArgs e)
        {
           
            LUVvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            CMYvalueChanged2();
            XYZvalueChanged2();
        }

        private void CS_Scroll(object sender, EventArgs e)
        {
            CMYvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            LUVvalueChanged2();
            XYZvalueChanged2();
        }

        private void MS_Scroll(object sender, EventArgs e)
        {
            CMYvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            LUVvalueChanged2();
            XYZvalueChanged2();
        }

        private void YS_Scroll(object sender, EventArgs e)
        {
            CMYvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            LUVvalueChanged2();
            XYZvalueChanged2();
        }

        private void XC_Scroll(object sender, EventArgs e)
        {
            XYZvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
        }

        private void Y2C_Scroll(object sender, EventArgs e)
        {
            XYZvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
        }

        private void ZC_Scroll(object sender, EventArgs e)
        {
            XYZvalueChanged();
            RGBvalueChanged2();
            HSVvalueChanged2();
            LUVvalueChanged2();
            CMYvalueChanged2();
        }
    }
}