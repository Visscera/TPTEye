using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePowderToyFilterColorTool
{
    public partial class frmMain : Form
    {
        // TODO: Color picker
        // TODO: F/C

        /*
         * Colors:
         *  2xGrey (unavailable)    #444444
         *  9xRed                   #550000 => #FF0000
         *  3xYellow                #555500
         *  6xGreen                 #005500
         *  3xCyan                  #005555
         *  9xBlue                  #000055
         *  
         *  Red:            #FF0000
         *  Yellow:         #FFFF00
         *  Red+Yellow:     #FFE900
         *  Green:          #00FF00
         *  Green+Yellow:   #FFFF00
         *  Cyan:           #00FFFF
         *  Cyan+Green:     #00FFFF
         *  Blue:           #0000FF
         *  Blue+Cyan:      #00E9FF
         *  
         *  Red+Green+Blue: #FFFFFF
         *  Yellow+Cyan:    #FFFFFF
         *  Blue+Yellow:    #E9E9FF
         *  Red+Cyan:       #FFE9E9
         *  
         * */

        // Bit numeration always goes from left to right, 0=>31, to least significance
        int colorsSkip = 2;
        Color colorSkip = colorFromRgb(0x444444);
        int colorsRed = 9;
        Color colorRed = colorFromRgb(0xFF0000);
        Color colorRedDim = colorFromRgb(0x550000);
        int colorsYellow = 3;
        Color colorYellow = colorFromRgb(0xFFFF00);
        Color colorYellowDim = colorFromRgb(0x555500);
        int colorsGreen = 6;
        Color colorGreen = colorFromRgb(0x00FF00);
        Color colorGreenDim = colorFromRgb(0x005500);
        int colorsCyan = 3;
        Color colorCyan = colorFromRgb(0x00FFFF);
        Color colorCyanDim = colorFromRgb(0x005555);
        int colorsBlue = 9;
        Color colorBlue = colorFromRgb(0x0000FF);
        Color colorBlueDim = colorFromRgb(0x000055);
        
        const int totalColorBits = 30;
        bool[] currentColor = new bool[32];

        bool suppressTextboxChangeEvents = false; // Feels dirty, but seems like the best solution so far

        const int scaleMult = 2;
        const int colorBitWidth = 9* scaleMult, colorBitHeight = 15* scaleMult, colorBitPaging = 1* scaleMult;

        static Color colorFromRgb(int rgb)
        {
            return Color.FromArgb((int)(0xFF00_0000 | rgb));
        }
        enum SpectrumKind
        {
            Red, Green, Blue,
            Yellow, Cyan,
            None = 0
        }
        struct ColorBitInfo
        {
            public Color bitColor, bitColorDim;
            public bool dummy;
            public int bitSignificance;
            public int totalColorBits;
            public SpectrumKind spectrumKind;
            public override string ToString()
            {
                return String.Format("BC[{0}/{1}, {2}/{3}, D:{4}",
                                        bitColor.ToString(), bitColorDim.ToString(),
                                        bitSignificance, totalColorBits-1,
                                        dummy ? "T" : "F");
            }
        }
        ColorBitInfo getColorBit(int id) // id starts at 0-LSB.Blue up to 31-DummyGray
        {
            var tmp = new ColorBitInfo();
            if (id<colorsBlue)
            {
                tmp.bitSignificance = id;
                tmp.bitColor = colorBlue;
                tmp.bitColorDim = colorBlueDim;
                tmp.totalColorBits = colorsBlue;
                tmp.dummy = false;
                tmp.spectrumKind = SpectrumKind.Blue;
                return tmp;
            }
            id -= colorsBlue;

            if (id < colorsCyan)
            {
                tmp.bitSignificance = id;
                tmp.bitColor = colorCyan;
                tmp.bitColorDim = colorCyanDim;
                tmp.totalColorBits = colorsCyan;
                tmp.dummy = false;
                tmp.spectrumKind = SpectrumKind.Cyan;
                return tmp;
            }
            id -= colorsCyan;

            if (id < colorsGreen)
            {
                tmp.bitSignificance = id;
                tmp.bitColor = colorGreen;
                tmp.bitColorDim = colorGreenDim;
                tmp.totalColorBits = colorsGreen;
                tmp.dummy = false;
                tmp.spectrumKind = SpectrumKind.Green;
                return tmp;
            }
            id -= colorsGreen;

            if (id < colorsYellow)
            {
                tmp.bitSignificance = id;
                tmp.bitColor = colorYellow;
                tmp.bitColorDim = colorYellowDim;
                tmp.totalColorBits = colorsYellow;
                tmp.dummy = false;
                tmp.spectrumKind = SpectrumKind.Yellow;
                return tmp;
            }
            id -= colorsYellow;

            if (id < colorsRed)
            {
                tmp.bitSignificance = id;
                tmp.bitColor = colorRed;
                tmp.bitColorDim = colorRedDim;
                tmp.totalColorBits = colorsRed;
                tmp.dummy = false;
                tmp.spectrumKind = SpectrumKind.Red;
                return tmp;
            }
            id -= colorsRed;

            tmp.bitSignificance = id;
            tmp.bitColor = colorSkip;
            tmp.bitColorDim = colorSkip;
            tmp.totalColorBits = colorsSkip;
            tmp.dummy = true;
            tmp.spectrumKind = SpectrumKind.None;
            return tmp;
        }
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            updateColor(ColorRepresentationInt);
        }

        private void picColors_Click(object sender, EventArgs e)
        {
            picColors.Refresh();
        }

        private void picColors_Paint(object sender, PaintEventArgs e)
        {
            Font f = new Font("Consolas", 8);
            e.Graphics.Clear(Color.Black);
            foreach(var ir in getColorRectangles())
            {
                int i = ir.Item1;
                Rectangle r = ir.Item2;
                var cbi = getColorBit(i);
                Color color = currentColor[i] ? cbi.bitColor : cbi.bitColorDim;
                e.Graphics.FillRectangle(new SolidBrush(color), r);

                var brush = currentColor[i] ? Brushes.DarkGray : Brushes.LightGray;
                var str = (i == 30 ? "E" : (i == 31 ? "F" : i.ToString()));
                e.Graphics.DrawString(str, f, brush, r.X, r.Y + r.Height - 8*scaleMult);
            }

        }

        bool? bitSetAction;
        private void picColors_MouseDown(object sender, MouseEventArgs e)
        {
            picColors.Focus();
            if (!e.Button.HasFlag(MouseButtons.Left))
                return;
            int bit = getBitIdAtPos(e.X, e.Y);
            if (bit == -1)
            {
                bitSetAction = null;
                return;
            }
            if (bit < totalColorBits)
                currentColor[bit] = !currentColor[bit];
            if (bit == 30)
                bitSetAction = false;
            else if (bit == 31)
                bitSetAction = true;
            else
                bitSetAction = currentColor[bit];
            updateColor(ColorRepresentationInt);
        }

        private void picColors_MouseMove(object sender, MouseEventArgs e)
        {
            if (!e.Button.HasFlag(MouseButtons.Left))
                return;
            if (bitSetAction == null) return;
            int bit = getBitIdAtPos(e.X, e.Y);
            if (bit == -1)
                return;
            if (bit < totalColorBits)
                currentColor[bit] = bitSetAction.Value;
            updateColor(ColorRepresentationInt);
        }
        void updateColor(int colorInt)
        {
            picColors.Refresh();
            suppressTextboxChangeEvents = true;
            try
            {
                if (!txtCurrentColor.Focused)
                    txtCurrentColor.Text = colorInt.ToString();
                if (!txtCurrentColorHex.Focused)
                    txtCurrentColorHex.Text = string.Format("{0:X8}", colorInt);
            } finally
            {
                suppressTextboxChangeEvents = false;
            }
            picColorRender.BackColor = getCurrentColorAsRGB();
        }
        int ColorRepresentationInt
        {
            get
            {
                int thisInt = 0;
                for (int i=0;i<totalColorBits;i++)
                {
                    thisInt += currentColor[i] ? (1<<i) : 0;
                }
                return thisInt;
            }
            set
            {
                if (value == ColorRepresentationInt)
                    return;

                for (int i = 0; i < totalColorBits; i++)
                {
                    currentColor[i] = (value % 2 == 1);
                    value >>= 1;
                }
                updateColor(ColorRepresentationInt);
            }
            
        }

        private void picColors_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void txtCurrentColor_TextChanged(object sender, EventArgs e)
        {
            int color;
            if (!suppressTextboxChangeEvents)
            {
                if (!int.TryParse(txtCurrentColor.Text, out color))
                {
                    Console.Beep();
                    return;
                }

                if (color == ColorRepresentationInt)
                    return;

                ColorRepresentationInt = color;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            const string website = "https://powdertoy.co.uk/Wiki/W/Element:FILT.html";
            Process.Start(website);
        }

        private void txtCurrentColorHex_TextChanged(object sender, EventArgs e)
        {
            int unhex = ColorRepresentationInt;
            if (!suppressTextboxChangeEvents)
            {
                try
                {
                    unhex = Convert.ToInt32(txtCurrentColorHex.Text, 16);
                }
                catch (FormatException)
                {
                    Console.Beep();
                    return;
                }
            }
            ColorRepresentationInt = unhex;
        }

        IEnumerable<Tuple<int, Rectangle>> getColorRectangles()
        {
            int drawX = colorBitPaging, drawY = 1;
            for (int i = 31; i >= 0; i--)
            {
                yield return new Tuple<int, Rectangle>(i, new Rectangle(drawX, drawY, colorBitWidth, colorBitHeight));
                drawX += colorBitWidth + colorBitPaging;
            }
        }
        int getBitIdAtPos(int x, int y)
        {
            foreach(var ir in getColorRectangles())
            {
                var r = ir.Item2;
                if (r.Left <= x && r.Right >= x && r.Top <= y && r.Bottom >= y)
                    return ir.Item1;
            }
            return -1;
        }

        /*Color getCurrentColorAsRGB()
        {
            // Approximatory function - not sure if that's how TPT renders colors, exactly, but seems good so far!
            const float multRGB = 1.0f;
            const float multCY = 0.91f;

            float r=0, g=0, b=0;
            float mr = 0, mg = 0, mb = 0; // Max R, G and B
            for (int i=0;i<totalColorBits; i++)
            {
                var bit = getColorBit(i);
                if (currentColor[i])
                {
                    float multSpectrum = 0;
                    if (bit.spectrumKind == SpectrumKind.Red || bit.spectrumKind == SpectrumKind.Green || bit.spectrumKind == SpectrumKind.Blue)
                        multSpectrum = multRGB;
                    else if (bit.spectrumKind == SpectrumKind.Yellow || bit.spectrumKind == SpectrumKind.Cyan)
                        multSpectrum = multCY;
                    else
                        throw new Exception("Oh! .0.");

                    float multBin = (1 << bit.bitSignificance) * 1.0f / (1 << bit.totalColorBits);
                    float multTotal = multBin * multSpectrum;

                    r += (bit.bitColor.R / 255.0f)*multTotal;
                    g += (bit.bitColor.G / 255.0f)*multTotal;
                    b += (bit.bitColor.B / 255.0f)*multTotal;
                    mr += multTotal;
                    mg += multTotal;
                    mb += multTotal;
                }
            }

            byte br, bg, bb;
            br = (Byte)(r / mr * 255);
            bg = (Byte)(g / mg * 255);
            bb = (Byte)(b / mb * 255);
            return Color.FromArgb(br, bg, bb);
        }*/
        Color getCurrentColorAsRGB()
        {
            /*
             * Original algorithm reference - https://github.com/The-Powder-Toy/The-Powder-Toy/blob/master/src/simulation/elements/PHOT.cpp, line 121
                int x = 0;
	            *colr = *colg = *colb = 0;
	            for (x=0; x<12; x++) {
		            *colr += (cpart->ctype >> (x+18)) & 1;
		            *colb += (cpart->ctype >>  x)     & 1;
	            }
	            for (x=0; x<12; x++)
		            *colg += (cpart->ctype >> (x+9))  & 1;
	            x = 624/(*colr+*colg+*colb+1);
	            *colr *= x;
	            *colg *= x;
	            *colb *= x;
             * */
            int ctype = ColorRepresentationInt;
            int colr, colg, colb;
            colr = colg = colb = 0;
            for (int x = 0; x < 12; x++)
            {
                colr += (ctype >> (x + 18)) & 1;
                colb += (ctype >> x) & 1;
            }
            for (int x = 0; x < 12; x++)
                colg += (ctype >> (x + 9)) & 1;

            

            float y = 624.0f / (colr + colg + colb + 1);
            colr = (int)(colr * y);
            colg = (int)(colg * y);  
            colb = (int)(colb * y);

            //this.Text = string.Format("{0}/{1}/{2}", colr, colg, colb);

            // Dunno why. But it just works. 0w0
            colr *= 2;
            colg *= 2;
            colb *= 2;

            // huh?..
            colr = ((colr>255) ? 255: ((colr<0) ? 0: colr));
            colg = ((colg > 255) ? 255 : ((colg < 0) ? 0 : colg));
            colb = ((colb > 255) ? 255 : ((colb < 0) ? 0 : colb));
            
            return Color.FromArgb(colr, colg, colb);
            //return Color.Black;
        }
    }
}
