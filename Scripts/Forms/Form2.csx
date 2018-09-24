#load "Form2.Designer.csx"

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class Form2 : Form
{
    public Form2() => this.InitializeComponent();

    internal static ColorDialog ColorDialog1 { get; set; }

    internal static DialogResult Result { get; set; }

    /// <summary>
    /// Creates a new <see cref="Color"/> from the input HSL values.
    /// </summary>
    /// <param name="hue">The hue to use for the new color.</param>
    /// <param name="saturation">The Saturation for the new color.</param>
    /// <param name="lumosity">The Lumosity of the new color.</param>
    /// <param name="alpha">The alpha  value of the new color between 0 and 255.</param>
    /// <example>For exmaple:
    ///   <code>
    ///     var sourcecol = Color.HotPink;
    ///     var targetcol = FromHsl(sourcecol.GetHue(), sourcecol.GetSaturation(), sourcecol.GetBrightness(), sourcecol.A);
    ///     // use targetcol.
    ///   </code>
    /// </example>
    /// <returns>A new <see cref="Color"/> with the Color that the HSL values represent.</returns>
    internal static Color FromHsl(float hue, float saturation, float lumosity, int alpha)
    {
        var chroma = (1 - Math.Abs((2 * lumosity) - 1)) * saturation;
        var hue2 = hue / 60;
        var x = chroma * (1 - Math.Abs((hue2 % 2) - 1));
        var rgb = new float[3];
        if (hue2 < 1)
        {
            rgb[0] = chroma;
            rgb[1] = x;
        }
        else if (hue2 < 2)
        {
            rgb[0] = x;
            rgb[1] = chroma;
        }
        else if (hue2 < 3)
        {
            rgb[1] = chroma;
            rgb[2] = x;
        }
        else if (hue2 < 4)
        {
            rgb[1] = x;
            rgb[2] = chroma;
        }
        else if (hue2 < 5)
        {
            rgb[0] = x;
            rgb[2] = chroma;
        }
        else if (hue2 < 6)
        {
            rgb[0] = chroma;
            rgb[2] = x;
        }

        var m = Math.Round(255f * (lumosity - (chroma / 2)));
        return Color.FromArgb(
            alpha,
            (int)(Math.Round(255f * rgb[0]) + m),
            (int)(Math.Round(255f * rgb[1]) + m),
            (int)(Math.Round(255f * rgb[2]) + m));
    }

    private void Form2_Load(object sender, EventArgs e) => Task.Factory.StartNew(() => this.RecolorImage());

    private void RecolorImage()
    {
        if (Form1.OriginalBitmaps.Count > 0)
        {
            if (Result != DialogResult.Cancel)
            {
                var targethue = ColorDialog1.Color.GetHue();
                var targetsat = ColorDialog1.Color.GetSaturation();
                TabPage page = null;
                this.Owner.Invoke((MethodInvoker)(() =>
                {
                    page = ((Form1)this.Owner).TabControl1.SelectedTab;
                }));
                var image = new Bitmap(
                    Form1.PicboxList[page.Text].Picturebox.Image.Width,
                    Form1.PicboxList[page.Text].Picturebox.Image.Height);
                this.Invoke((MethodInvoker)(() =>
                {
                    this.progressBar1.Maximum = image.Width * image.Height;
                }));
                var pixelIndex = 0;
                for (var y = 0; y < image.Height; y++)
                {
                    for (var x = 0; x < image.Width; x++)
                    {
                        this.Invoke((MethodInvoker)(() =>
                        {
                            this.progressBar1.Value = pixelIndex;
                        }));
                        var pixelCol = Form1.OriginalBitmaps[page.Text].GetPixel(x, y);
                        var pixelsat = pixelCol.GetSaturation();
                        var pixellum = pixelCol.GetBrightness();

                        // keep the alpha component, just recolor everything on the hue.
                        var alpha = pixelCol.A;
                        image.SetPixel(x, y, FromHsl(targethue, targetsat, pixellum, alpha));
                        pixelIndex = ++pixelIndex;
                    }
                }

                /*
                var rect = new Rectangle(0, 0, image.Width, image.Height);
                var bmpData = image.LockBits(
                    rect,
                    System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    image.PixelFormat);
                var ptr = bmpData.Scan0;
                var bytes = Math.Abs(bmpData.Stride) * image.Height;
                var rgbValues = new byte[bytes];
                Marshal.Copy(ptr, rgbValues, 0, bytes);
                for (var counter = 0; counter < rgbValues.Length; counter += 3)
                {
                    this.Invoke((MethodInvoker)(() =>
                    {
                        this.progressBar1.Value = counter;
                    }));
                    var pixelCol = Color.FromArgb(
                        rgbValues[counter],
                        rgbValues[counter + 1],
                        rgbValues[counter + 2],
                        rgbValues[counter + 3]);
                    var pixellum = pixelCol.GetBrightness();
                    var targetCol = FromHsl(targethue, targetsat, pixellum, pixelCol.A);
                    rgbValues[counter] = targetCol.A;
                    rgbValues[counter + 1] = targetCol.R;
                    rgbValues[counter + 2] = targetCol.G;
                    rgbValues[counter + 3] = targetCol.B;
                }

                Marshal.Copy(rgbValues, 0, ptr, rgbValues.Length);
                image.UnlockBits(bmpData);
                */
                if (Form1.RecoloredBitmaps.ContainsKey(page.Text))
                {
                    Form1.RecoloredBitmaps[page.Text].Dispose();
                    Form1.RecoloredBitmaps.Remove(page.Text);
                    Form1.RecoloredBitmaps.Add(page.Text, image);
                    Form1.PicboxList[page.Text].Picturebox.Image = image;
                }
                else
                {
                    Form1.RecoloredBitmaps.Add(page.Text, image);
                    Form1.PicboxList[page.Text].Picturebox.Image = image;
                }
            }
        }

        this.Invoke((MethodInvoker)(() =>
        {
            this.Close();
        }));
    }
}
