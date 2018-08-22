namespace TestImageZooming
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private System.Collections.Generic.Dictionary<string, System.Windows.Forms.PictureBox> picboxList = new System.Collections.Generic.Dictionary<string, System.Windows.Forms.PictureBox>();
        private System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap> originalBitmaps = new System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap>();
        private System.Collections.Generic.Dictionary<string, System.Windows.Forms.Panel> panels = new System.Collections.Generic.Dictionary<string, System.Windows.Forms.Panel>();
        private System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap> recoloredBitmaps = new System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap>();
        private double prevResizeValue = 0d;

        // get a relative size and locations (if it just needs relocated
        // and not resized) for each control and tab page to use when
        // resizing the form based on the Form's new Size.
        private System.Drawing.Size tabPageDiff;
        private System.Drawing.Size tabDiff;
        private System.Drawing.Size panelDiff;

        public Form1() => this.InitializeComponent();

        private void openImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var result = this.openFileDialog1.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.Cancel)
            {
                var tmpBitmap = new System.Drawing.Bitmap(this.openFileDialog1.FileName);
                if (this.pictureBox1.Image != null)
                {
                    var page = new System.Windows.Forms.TabPage($"image{this.tabControl1.TabCount + 1}");
                    var panel = new System.Windows.Forms.Panel
                    {
                        AutoScroll = true,
                        Location = this.panel1.Location,
                        Name = "panel",
                        Size = this.panel1.Size,
                        TabIndex = 0
                    };
                    var picturebox = new System.Windows.Forms.PictureBox
                    {
                        Location = panel.Location,
                        Name = "picturebox",
                        Size = tmpBitmap.Size,
                        SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom,
                        TabIndex = 0,
                        TabStop = false,
                        Image = tmpBitmap
                    };
                    panel.Controls.Add(picturebox);
                    page.Controls.Add(panel);
                    this.tabControl1.TabPages.Add(page);
                    this.picboxList.Add(page.Text, picturebox);
                    this.originalBitmaps.Add(page.Text, tmpBitmap);
                    this.panels.Add(page.Text, panel);
                }
                else
                {
                    this.tabControl1.Visible = true;
                    this.pictureBox1.Size = tmpBitmap.Size;
                    this.pictureBox1.Image = tmpBitmap;
                    this.picboxList.Add(this.tabPage1.Text, this.pictureBox1);
                    this.originalBitmaps.Add(this.tabPage1.Text, tmpBitmap);
                    this.panels.Add(this.tabPage1.Text, this.panel1);
                }
            }
        }

        private void closeImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count > 0)
            {
                var page = this.tabControl1.SelectedTab;

                if (this.recoloredBitmaps.ContainsKey(page.Text))
                {
                    this.recoloredBitmaps[page.Text].Dispose();
                    this.recoloredBitmaps.Remove(page.Text);
                }

                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    item.Checked = item.Checked ? false : false;
                }

                this.originalBitmaps[page.Text].Dispose();
                this.originalBitmaps.Remove(page.Text);
                if (page.Text != "image1")
                {
                    for (var i = 0; i < page.Controls.Count; i++)
                    {
                        page.Controls[i].Dispose();
                    }

                    this.panels[page.Text].Dispose();
                    this.panels.Remove(page.Text);
                    this.picboxList[page.Text].Dispose();
                    this.picboxList.Remove(page.Text);
                    this.tabControl1.TabPages.Remove(page);
                    page.Dispose();
                }
                else
                {
                    this.panels.Remove(page.Text);
                    this.picboxList.Remove(page.Text);
                    this.pictureBox1.Image.Dispose();
                    this.pictureBox1.Image = null;
                    this.tabControl1.Visible = false;
                }
            }
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            this.tabControl1.Size = System.Drawing.Size.Subtract(this.Size, this.tabDiff);
            for (var i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                this.tabControl1.TabPages[i].Size = System.Drawing.Size.Subtract(this.Size, this.tabPageDiff);
            }

            foreach (var panel in this.panels.Values)
            {
                panel.Size = System.Drawing.Size.Subtract(this.Size, this.panelDiff);
            }
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.panelDiff = System.Drawing.Size.Subtract(this.Size, this.panel1.Size);
            this.tabPageDiff = System.Drawing.Size.Subtract(this.Size, this.tabPage1.Size);
            this.tabDiff = System.Drawing.Size.Subtract(this.Size, this.tabControl1.Size);
        }

        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            for (var i = 0; i < this.tabControl1.TabPages.Count; i++)
            {
                var page = this.tabControl1.TabPages[i];

                if (this.recoloredBitmaps.ContainsKey(page.Text))
                {
                    this.recoloredBitmaps[page.Text].Dispose();
                    this.recoloredBitmaps.Remove(page.Text);
                }

                for (var c = 0; c < page.Controls.Count; c++)
                {
                    page.Controls[c].Dispose();
                }

                if (this.panels.ContainsKey(page.Text))
                {
                    this.panels[page.Text].Dispose();
                    this.panels.Remove(page.Text);
                }

                if (this.picboxList.ContainsKey(page.Text))
                {
                    this.picboxList[page.Text].Dispose();
                    this.picboxList.Remove(page.Text);
                }

                page.Dispose();
                this.tabControl1.TabPages.Remove(page);
            }
        }

        /// <summary>
        /// Creates a new <see cref="System.Drawing.Color"/> from the input HSL values.
        /// </summary>
        /// <param name="hue">The hue to use for the new color.</param>
        /// <param name="saturation">The Saturation for the new color.</param>
        /// <param name="lumosity">The Lumosity of the new color.</param>
        /// <param name="alpha">The alpha  value of the new color between 0 and 255.</param>
        /// <returns>A new <see cref="System.Drawing.Color"/> with the Color that the HSL values represent.</returns>
        internal static System.Drawing.Color FromHsl(float hue, float saturation, float lumosity, int alpha)
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
            return System.Drawing.Color.FromArgb(
                alpha,
                (int)(System.Math.Round(255f * rgb[0]) + m),
                (int)(System.Math.Round(255f * rgb[1]) + m),
                (int)(System.Math.Round(255f * rgb[2]) + m));
        }

        private void recolorImageToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count > 0)
            {
                var result = this.colorDialog1.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.Cancel)
                {
                    var targethue = this.colorDialog1.Color.GetHue();
                    var page = this.tabControl1.SelectedTab;
                    var image = new System.Drawing.Bitmap(this.picboxList[page.Text].Image.Width, this.picboxList[page.Text].Image.Height);
                    for (var y = 0; y < image.Height; y++)
                    {
                        for (var x = 0; x < image.Width; x++)
                        {
                            var pixelCol = this.originalBitmaps[page.Text].GetPixel(x, y);
                            var pixelsat = pixelCol.GetSaturation();
                            var pixellum = pixelCol.GetBrightness();

                            // keep the alpha component, just recolor everything on the hue.
                            var alpha = pixelCol.A;
                            image.SetPixel(x, y, FromHsl(targethue, pixelsat, pixellum, alpha));
                        }
                    }

                    if (this.recoloredBitmaps.ContainsKey(page.Text))
                    {
                        this.recoloredBitmaps[page.Text].Dispose();
                        this.recoloredBitmaps.Remove(page.Text);
                        this.recoloredBitmaps.Add(page.Text, image);
                        this.picboxList[page.Text].Image = image;
                    }
                    else
                    {
                        this.recoloredBitmaps.Add(page.Text, image);
                        this.picboxList[page.Text].Image = image;
                    }
                }
            }
        }

        private void zoomToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count > 0)
            {
                var itemtext = string.Empty;
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Checked)
                    {
                        itemtext = item.Text;
                    }
                }

                if (!string.IsNullOrEmpty(itemtext))
                {
                    double.TryParse(itemtext.Replace("%", string.Empty), out var value);
                    value = value / 100;
                    if (value < this.prevResizeValue
                        || value > this.prevResizeValue
                        || value == this.prevResizeValue)
                    {
                        this.picboxList[this.tabControl1.SelectedTab.Text].Size = this.picboxList[this.tabControl1.SelectedTab.Text].Image.Size;
                    }

                    this.picboxList[this.tabControl1.SelectedTab.Text].Width = (int)(
                        this.picboxList[this.tabControl1.SelectedTab.Text].Width * value);
                    this.picboxList[this.tabControl1.SelectedTab.Text].Height = (int)(
                        this.picboxList[this.tabControl1.SelectedTab.Text].Height * value);
                    this.prevResizeValue = value;
                }
            }
        }

        private void toolStripMenuItem2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem2.Checked = false;
            }

            if (this.toolStripMenuItem2.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem2.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem3_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem3.Checked = false;
            }

            if (this.toolStripMenuItem3.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem3.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem4_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem4.Checked = false;
            }

            if (this.toolStripMenuItem4.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem4.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem5_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem5.Checked = false;
            }

            if (this.toolStripMenuItem5.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem5.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem6_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem6.Checked = false;
            }

            if (this.toolStripMenuItem6.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem6.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem7_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem7.Checked = false;
            }

            if (this.toolStripMenuItem7.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem7.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem8_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem8.Checked = false;
            }

            if (this.toolStripMenuItem8.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem8.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem9_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem9.Checked = false;
            }

            if (this.toolStripMenuItem9.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem9.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem10_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem10.Checked = false;
            }

            if (this.toolStripMenuItem10.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem10.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void toolStripMenuItem11_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.originalBitmaps.Count < 1)
            {
                // make sure this stays unchecked when no items are open.
                this.toolStripMenuItem11.Checked = false;
            }

            if (this.toolStripMenuItem11.Checked)
            {
                // ensure all other options are unchecked.
                for (var i = 0; i < this.zoomToolStripMenuItem.DropDownItems.Count; i++)
                {
                    var item = (System.Windows.Forms.ToolStripMenuItem)this.zoomToolStripMenuItem.DropDownItems[i];
                    if (item.Text != this.toolStripMenuItem11.Text && item.Checked)
                    {
                        item.Checked = false;
                    }
                }
            }
        }
    }
}
