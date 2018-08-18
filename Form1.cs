﻿namespace TestImageZooming
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        public Form1() => InitializeComponent();
        private System.Collections.Generic.Dictionary<string, System.Windows.Forms.PictureBox> picboxList = new System.Collections.Generic.Dictionary<string, System.Windows.Forms.PictureBox>();
        private System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap> originalBitmaps = new System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap>();
        private System.Collections.Generic.Dictionary<string, System.Windows.Forms.ComboBox> comboBoxes = new System.Collections.Generic.Dictionary<string, System.Windows.Forms.ComboBox>();
        private System.Collections.Generic.Dictionary<string, System.Windows.Forms.Panel> panels = new System.Collections.Generic.Dictionary<string, System.Windows.Forms.Panel>();
        private System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap> resizedBitmaps = new System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap>();
        private int OrigWidth = 0;
        private int OrigHeight = 0;

        private void openImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                var tmpBitmap = new System.Drawing.Bitmap(openFileDialog1.FileName);
                if (pictureBox1.Image != null)
                {
                    var page = new System.Windows.Forms.TabPage($"image{tabControl1.TabCount + 1}");
                    var panel = new System.Windows.Forms.Panel
                    {
                        AutoScroll = true,
                        Location = panel1.Location,
                        Name = "panel",
                        Size = panel1.Size,
                        TabIndex = 0
                    };
                    var picturebox = new System.Windows.Forms.PictureBox
                    {
                        Location = panel.Location,
                        Name = "picturebox",
                        Size = tmpBitmap.Size,
                        TabIndex = 0,
                        TabStop = false,
                        Image = tmpBitmap
                    };
                    var combobox = new System.Windows.Forms.ComboBox
                    {
                        FormattingEnabled = true,
                        Location = comboBox1.Location,
                        Name = "combobox",
                        Size = comboBox1.Size,
                        TabIndex = 1
                    };
                    combobox.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
                    // do not relist the items, copy them from first listbox.
                    var items = new object[comboBox1.Items.Count];
                    comboBox1.Items.CopyTo(items, 0);
                    combobox.Items.AddRange(items);
                    panel.Controls.Add(picturebox);
                    page.Controls.Add(panel);
                    page.Controls.Add(combobox);
                    tabControl1.TabPages.Add(page);
                    picboxList.Add(page.Text, picturebox);
                    originalBitmaps.Add(page.Text, tmpBitmap);
                    comboBoxes.Add(page.Text, combobox);
                    panels.Add(page.Text, panel);
                }
                else
                {
                    tabControl1.Visible = true;
                    pictureBox1.Size = tmpBitmap.Size;
                    pictureBox1.Image = tmpBitmap;
                    picboxList.Add(tabPage1.Text, pictureBox1);
                    originalBitmaps.Add(tabPage1.Text, tmpBitmap);
                    comboBoxes.Add(tabPage1.Text, comboBox1);
                    panels.Add(tabPage1.Text, panel1);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (resizedBitmaps.ContainsKey(tabControl1.SelectedTab.Text))
            {
                resizedBitmaps[tabControl1.SelectedTab.Text].Dispose();
                resizedBitmaps.Remove(tabControl1.SelectedTab.Text);
            }
            double.TryParse(comboBoxes[tabControl1.SelectedTab.Text].SelectedItem.ToString().Replace("%", string.Empty), out var value);
            value = value / 100;
            // create new bitmap enlarged.
            var originalBitmap = originalBitmaps[tabControl1.SelectedTab.Text];
            var newBitmap = new System.Drawing.Bitmap(
                originalBitmap,
                (int)(originalBitmap.Width * value), (int)(originalBitmap.Height * value));
            if (value == 1.0)
            {
                // if at 100% then use original image instead.
                newBitmap.Dispose();
                picboxList[tabControl1.SelectedTab.Text].Image = originalBitmap;
                picboxList[tabControl1.SelectedTab.Text].Size = originalBitmap.Size;
            }
            else
            {
                picboxList[tabControl1.SelectedTab.Text].Image = newBitmap;
                picboxList[tabControl1.SelectedTab.Text].Size = newBitmap.Size;
                resizedBitmaps.Add(tabControl1.SelectedTab.Text, newBitmap);
            }
        }

        private void closeImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var page = tabControl1.SelectedTab;
            if (resizedBitmaps.ContainsKey(page.Text))
            {
                resizedBitmaps[page.Text].Dispose();
                resizedBitmaps.Remove(page.Text);
            }
            originalBitmaps[page.Text].Dispose();
            originalBitmaps.Remove(page.Text);
            if (page.Text != "image1")
            {
                for (var i = 0; i < page.Controls.Count; i++)
                {
                    page.Controls[i].Dispose();
                }
                comboBoxes[page.Text].Dispose();
                comboBoxes.Remove(page.Text);
                panels[page.Text].Dispose();
                panels.Remove(page.Text);
                picboxList[page.Text].Dispose();
                picboxList.Remove(page.Text);
                tabControl1.TabPages.Remove(page);
                page.Dispose();
            }
            else
            {
                comboBoxes.Remove(page.Text);
                panels.Remove(page.Text);
                picboxList.Remove(page.Text);
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
                tabControl1.Visible = false;
            }
        }

        // does not resize the controls and place the comboxboxes on every tab correctly.
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (OrigHeight < Height)
            {
                tabControl1.Height += 1;
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    tabControl1.TabPages[i].Height += 1;
                }
                foreach (var panel in panels.Values)
                {
                    panel.Height += 1;
                }
                foreach (var combobox in comboBoxes.Values)
                {
                    var newloc = new System.Drawing.Point(combobox.Location.X + 1, combobox.Location.Y);
                    combobox.Location = newloc;
                }
                OrigHeight += 1;
            }
            else if (OrigHeight > Height)
            {
                tabControl1.Height -= 1;
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    tabControl1.TabPages[i].Height -= 1;
                }
                foreach (var panel in panels.Values)
                {
                    panel.Height -= 1;
                }
                foreach (var combobox in comboBoxes.Values)
                {
                    var newloc = new System.Drawing.Point(combobox.Location.X - 1, combobox.Location.Y);
                    combobox.Location = newloc;
                }
                OrigHeight -= 1;
            }
            if (OrigWidth < Width)
            {
                tabControl1.Width += 1;
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    tabControl1.TabPages[i].Width += 1;
                }
                foreach (var panel in panels.Values)
                {
                    panel.Width += 1;
                }
                foreach (var combobox in comboBoxes.Values)
                {
                    var newloc = new System.Drawing.Point(combobox.Location.X, combobox.Location.Y + 1);
                    combobox.Location = newloc;
                }
                OrigWidth += 1;
            }
            else if (OrigWidth > Width)
            {
                tabControl1.Width -= 1;
                for (var i = 0; i < tabControl1.TabPages.Count; i++)
                {
                    tabControl1.TabPages[i].Width -= 1;
                }
                foreach (var panel in panels.Values)
                {
                    panel.Width -= 1;
                }
                foreach (var combobox in comboBoxes.Values)
                {
                    var newloc = new System.Drawing.Point(combobox.Location.X, combobox.Location.Y - 1);
                    combobox.Location = newloc;
                }
                OrigWidth -= 1;
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            OrigWidth = Width;
            OrigHeight = Height;
        }

        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            for (var i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var page = tabControl1.TabPages[i];
                if (resizedBitmaps.ContainsKey(page.Text))
                {
                    resizedBitmaps[page.Text].Dispose();
                    resizedBitmaps.Remove(page.Text);
                }
                for (var c = 0; c < page.Controls.Count; c++)
                {
                    page.Controls[c].Dispose();
                }
                comboBoxes[page.Text].Dispose();
                comboBoxes.Remove(page.Text);
                panels[page.Text].Dispose();
                panels.Remove(page.Text);
                picboxList[page.Text].Dispose();
                picboxList.Remove(page.Text);
                page.Dispose();
                tabControl1.TabPages.Remove(page);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var result = colorDialog1.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                var targethue = colorDialog1.Color.GetHue();
                var page = tabControl1.SelectedTab;
                var image = resizedBitmaps.ContainsKey(page.Text)
                    ? new System.Drawing.Bitmap(resizedBitmaps[page.Text].Width, resizedBitmaps[page.Text].Height)
                    : new System.Drawing.Bitmap(picboxList[page.Text].Image.Width, picboxList[page.Text].Image.Height);
                for (var y = 0; y < image.Height; y++)
                {
                    for (var x = 0; x < image.Width; x++)
                    {
                        var pixelCol = resizedBitmaps.ContainsKey(page.Text)
                            ? resizedBitmaps[page.Text].GetPixel(x, y)
                            : originalBitmaps[page.Text].GetPixel(x, y);
                        var pixelsat = pixelCol.GetSaturation();
                        var pixellum = pixelCol.GetBrightness();
                        // keep the alpha component, just recolor everything on the hue.
                        var alpha = pixelCol.A;
                        image.SetPixel(x, y, FromHsl(targethue / 360.0f, pixelsat, pixellum, alpha));
                    }
                }
                if (resizedBitmaps.ContainsKey(page.Text))
                {
                    resizedBitmaps[page.Text].Dispose();
                    resizedBitmaps.Remove(page.Text);
                    resizedBitmaps.Add(page.Text, image);
                    picboxList[page.Text].Image = image;
                }
                else
                {
                    resizedBitmaps.Add(page.Text, image);
                    picboxList[page.Text].Image = image;
                }
            }
        }

        // this works thankfully.
        /// <summary>
        /// Creates a new <see cref="System.Drawing.Color"/> from the input HSL values.
        /// </summary>
        /// <param name="hue">The hue to use for the new color.</param>
        /// <param name="saturation">The Saturation for the new color.</param>
        /// <param name="lumosity">The Lumosity of the new color.</param>
        /// <param name="alpha">The alpha  value of the new color between 0 and 255.</param>
        /// <returns>A new <see cref="System.Drawing.Color"/> with the Color that the HSL values represent.</returns>
        public static System.Drawing.Color FromHsl(float hue, float saturation, float lumosity, int alpha)
        {
            var chroma = (1 - System.Math.Abs((2 * lumosity) - 1)) * saturation;
            var hue2 = hue / 60;
            var x = chroma * (1 - System.Math.Abs((hue2 % 2) - 1));
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
            var m = System.Math.Round(255f * (lumosity - (chroma / 2)));
            return System.Drawing.Color.FromArgb(alpha,
                (int)(System.Math.Round(255f * rgb[0]) + m),
                (int)(System.Math.Round(255f * rgb[1]) + m),
                (int)(System.Math.Round(255f * rgb[2]) + m));
        }
    }
}
