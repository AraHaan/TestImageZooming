#load "Form1.Designer.csx"
#load "..\Support\PicBoxZoom.csx"
#load "..\Support\SafeNativeMethods.csx"
#load "..\Support\ExtensionLoader.csx"

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

partial class Form1 : Form
{
    private double prevResizeValue = 0d;
    private IntPtr hicon = IntPtr.Zero;

    // get a relative size and locations (if it just needs relocated
    // and not resized) for each control and tab page to use when
    // resizing the form based on the Form's new Size.
    private Size tabPageDiff;
    private Size tabDiff;
    private Size panelDiff;

    public Form1() => this.InitializeComponent();

    internal static Dictionary<string, PicBoxZoom> PicboxList { get; set; }

    internal static Dictionary<string, Bitmap> OriginalBitmaps { get; set; }

    internal static Dictionary<string, Panel> Panels { get; set; }

    internal static Dictionary<string, Bitmap> RecoloredBitmaps { get; set; }

    internal static List<IExtension> Extensions { get; set; }

    private void OpenImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = this.OpenFileDialog1.ShowDialog();
        if (result != DialogResult.Cancel)
        {
            var tmpBitmap = new Bitmap(this.OpenFileDialog1.FileName);
            if (this.PictureBox1.Image != null)
            {
                var page = new TabPage($"image{this.TabControl1.TabCount + 1}");
                var panel = new Panel
                {
                    AutoScroll = true,
                    Location = this.Panel1.Location,
                    Name = "panel",
                    Size = this.Panel1.Size,
                    TabIndex = 0,
                };
                panel.MouseWheel += new MouseEventHandler(this.Panel1_MouseWheel);
                panel.MouseDown += new MouseEventHandler(this.Panel1_MouseWheel);
                var picturebox = new PictureBox
                {
                    Location = panel.Location,
                    Name = "picturebox",
                    Size = tmpBitmap.Size,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    TabIndex = 0,
                    TabStop = false,
                    Image = tmpBitmap,
                };
                picturebox.MouseEnter += new EventHandler(this.PictureBox1_MouseEnter);
                panel.Controls.Add(picturebox);
                page.Controls.Add(panel);
                this.TabControl1.TabPages.Add(page);
                var picBoxZoom = new PicBoxZoom
                {
                    Picturebox = picturebox,
                    Zoom = 1.0,
                };
                PicboxList.Add(page.Text, picBoxZoom);
                OriginalBitmaps.Add(page.Text, tmpBitmap);
                Panels.Add(page.Text, panel);
            }
            else
            {
                this.TabControl1.Visible = true;
                this.PictureBox1.Size = tmpBitmap.Size;
                this.PictureBox1.Image = tmpBitmap;
                this.Panel1.Size = Size.Subtract(this.Size, this.panelDiff);
                var picBoxZoom = new PicBoxZoom
                {
                    Picturebox = this.PictureBox1,
                    Zoom = 1.0,
                };
                PicboxList.Add(this.TabPage1.Text, picBoxZoom);
                OriginalBitmaps.Add(this.TabPage1.Text, tmpBitmap);
                Panels.Add(this.TabPage1.Text, this.Panel1);
            }
        }
    }

    private void CloseImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count > 0)
        {
            var page = this.TabControl1.SelectedTab;

            if (RecoloredBitmaps.ContainsKey(page.Text))
            {
                RecoloredBitmaps[page.Text].Dispose();
                RecoloredBitmaps.Remove(page.Text);
            }

            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                item.Checked = item.Checked ? false : false;
            }

            OriginalBitmaps[page.Text].Dispose();
            OriginalBitmaps.Remove(page.Text);
            if (page.Text != "image1")
            {
                for (var i = 0; i < page.Controls.Count; i++)
                {
                    page.Controls[i].Dispose();
                }

                Panels[page.Text].Dispose();
                Panels.Remove(page.Text);
                PicboxList[page.Text].Picturebox.Dispose();
                PicboxList.Remove(page.Text);
                var selectedindex = this.TabControl1.SelectedIndex;
                this.TabControl1.SelectedIndex = selectedindex - 1;
                this.TabControl1.TabPages.Remove(page);
                page.Dispose();
            }
            else
            {
                Panels.Remove(page.Text);
                PicboxList.Remove(page.Text);
                this.PictureBox1.Image.Dispose();
                this.PictureBox1.Image = null;
                this.TabControl1.Visible = false;
                this.Icon = null;
            }
        }
    }

    private void Form1_Resize(object sender, EventArgs e)
    {
        this.TabControl1.Size = Size.Subtract(this.Size, this.tabDiff);
        for (var i = 0; i < this.TabControl1.TabPages.Count; i++)
        {
            this.TabControl1.TabPages[i].Size = Size.Subtract(this.Size, this.tabPageDiff);
        }

        foreach (var panel in Panels.Values)
        {
            panel.Size = Size.Subtract(this.Size, this.panelDiff);
        }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        Extensions = LoadExtensions();
        foreach (var extension in Extensions)
        {
            // TODO: populate the extensions in the menustrip.
            ToolStripMenuItem item = null;
            if (extension.ExtensionRootMenu.Equals(this.FileToolStripMenuItem.Text))
            {
                item = this.FileToolStripMenuItem;
            }
            else if (extension.ExtensionRootMenu.Equals(this.EditToolStripMenuItem.Text))
            {
                item = this.EditToolStripMenuItem;
            }
            else if (extension.ExtensionRootMenu.Equals(this.ViewToolStripMenuItem.Text))
            {
                item = this.ViewToolStripMenuItem;
            }
            else
            {
                // create a new menu for the root menu entry name.
            }

            // starts to fail to add the entry to the menu item at the supplied index.
            // var item = (ToolStripMenuItem)this.MenuStrip1.Items[index];
            // var extensionItem = new ToolStripMenuItem();
            // extensionItem.Name = extension.ExtensionMenuItem;
            // extensionItem.Size = extension.ExtensionMenuSize;
            // extensionItem.Text = extension.ExtensionMenuItem;
            extension.Parrent = this;
            // extensionItem.Click += new EventHandler(extension.ExtensionMenuItem_Click);
            item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                extension.ExtensionMenuItem});
        }

        PicboxList = new Dictionary<string, PicBoxZoom>();
        OriginalBitmaps = new Dictionary<string, Bitmap>();
        Panels = new Dictionary<string, Panel>();
        RecoloredBitmaps = new Dictionary<string, Bitmap>();
        this.panelDiff = Size.Subtract(this.Size, this.Panel1.Size);
        this.tabPageDiff = Size.Subtract(this.Size, this.TabPage1.Size);
        this.tabDiff = Size.Subtract(this.Size, this.TabControl1.Size);
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        for (var i = 0; i < this.TabControl1.TabPages.Count; i++)
        {
            var page = this.TabControl1.TabPages[i];

            if (RecoloredBitmaps.ContainsKey(page.Text))
            {
                RecoloredBitmaps[page.Text].Dispose();
                RecoloredBitmaps.Remove(page.Text);
            }

            for (var c = 0; c < page.Controls.Count; c++)
            {
                page.Controls[c].Dispose();
            }

            if (Panels.ContainsKey(page.Text))
            {
                Panels[page.Text].Dispose();
                Panels.Remove(page.Text);
            }

            if (PicboxList.ContainsKey(page.Text))
            {
                PicboxList[page.Text].Picturebox.Dispose();
                PicboxList.Remove(page.Text);
            }

            page.Dispose();
            this.TabControl1.TabPages.Remove(page);
        }
    }

    private void ZoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count > 0)
        {
            var itemtext = string.Empty;
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
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
                    PicboxList[this.TabControl1.SelectedTab.Text].Picturebox.Size = PicboxList[this.TabControl1.SelectedTab.Text].Picturebox.Image.Size;
                }

                PicboxList[this.TabControl1.SelectedTab.Text].Picturebox.Width = (int)(
                    PicboxList[this.TabControl1.SelectedTab.Text].Picturebox.Width * value);
                PicboxList[this.TabControl1.SelectedTab.Text].Picturebox.Height = (int)(
                    PicboxList[this.TabControl1.SelectedTab.Text].Picturebox.Height * value);
                this.prevResizeValue = value;
                var picBoxZoom = PicboxList[this.TabControl1.SelectedTab.Text];
                picBoxZoom.Zoom = this.prevResizeValue;
                PicboxList[this.TabControl1.SelectedTab.Text] = picBoxZoom;
            }
        }
    }

    private void ToolStripMenuItem2_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem2.Checked = false;
        }

        if (this.ToolStripMenuItem2.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem2.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem3_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem3.Checked = false;
        }

        if (this.ToolStripMenuItem3.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem3.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem4_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem4.Checked = false;
        }

        if (this.ToolStripMenuItem4.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem4.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem5_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem5.Checked = false;
        }

        if (this.ToolStripMenuItem5.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem5.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem6_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem6.Checked = false;
        }

        if (this.ToolStripMenuItem6.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem6.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem7_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem7.Checked = false;
        }

        if (this.ToolStripMenuItem7.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem7.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem8_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem8.Checked = false;
        }

        if (this.ToolStripMenuItem8.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem8.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem9_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem9.Checked = false;
        }

        if (this.ToolStripMenuItem9.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem9.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem10_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem10.Checked = false;
        }

        if (this.ToolStripMenuItem10.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem10.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void ToolStripMenuItem11_CheckedChanged(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count < 1)
        {
            // make sure this stays unchecked when no items are open.
            this.ToolStripMenuItem11.Checked = false;
        }

        if (this.ToolStripMenuItem11.Checked)
        {
            // ensure all other options are unchecked.
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                if (item.Text != this.ToolStripMenuItem11.Text && item.Checked)
                {
                    item.Checked = false;
                }
            }
        }
    }

    private void SaveImageAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count > 0)
        {
            var result = this.SaveFileDialog1.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                var picbox = PicboxList[this.TabControl1.SelectedTab.Text].Picturebox;
                var tmp = new Bitmap(picbox.Width, picbox.Height);
                picbox.DrawToBitmap(tmp, picbox.ClientRectangle);

                // TODO: Ensure saved animated images does not lose their frames
                // (if saved to gif or any format supporting animations).
                ImageFormat imageFormat = null;
                var loweredFileName = this.SaveFileDialog1.FileName.ToLower();
                if (loweredFileName.EndsWith("png"))
                {
                    imageFormat = ImageFormat.Png;
                }
                else if (loweredFileName.EndsWith("jpg"))
                {
                    imageFormat = ImageFormat.Jpeg;
                }
                else if (loweredFileName.EndsWith("bmp"))
                {
                    imageFormat = ImageFormat.Bmp;
                }
                else if (loweredFileName.EndsWith("gif"))
                {
                    imageFormat = ImageFormat.Gif;
                }

                if (imageFormat == null)
                {
                    tmp.Save(this.SaveFileDialog1.FileName);
                }
                else
                {
                    var numberOfFrames = picbox.Image.GetFrameCount(FrameDimension.Time);
                    if (numberOfFrames > 1)
                    {
                        picbox.Image.SelectActiveFrame(FrameDimension.Time, 0);
                        tmp.Dispose();
                        tmp = new Bitmap(picbox.Width, picbox.Height);
                        picbox.DrawToBitmap(tmp, picbox.ClientRectangle);
                        ImageCodecInfo codecInfo = null;
                        if (imageFormat == ImageFormat.Gif)
                        {
                            var codecInfos = ImageCodecInfo.GetImageEncoders();
                            foreach (var codecinfo in codecInfos)
                            {
                                if (codecinfo.MimeType == "image/gif")
                                {
                                    codecInfo = codecinfo;
                                }
                            }
                        }
                        else if (imageFormat == ImageFormat.Tiff)
                        {
                            var codecInfos = ImageCodecInfo.GetImageEncoders();
                            foreach (var codecinfo in codecInfos)
                            {
                                if (codecinfo.MimeType == "image/tiff")
                                {
                                    codecInfo = codecinfo;
                                }
                            }
                        }

                        var encoder = Encoder.SaveFlag;
                        var encoderparameter = new EncoderParameter(encoder, (long)EncoderValue.MultiFrame);
                        var encoderparameters = new EncoderParameters(1);
                        encoderparameters.Param[0] = encoderparameter;
                        tmp.Save(this.SaveFileDialog1.FileName, codecInfo, encoderparameters);
                        encoderparameter.Dispose();
                        for (var i = 1; i < numberOfFrames; i++)
                        {
                            picbox.Image.SelectActiveFrame(FrameDimension.Time, i);
                            var tmp2 = new Bitmap(picbox.Width, picbox.Height);
                            picbox.DrawToBitmap(tmp2, picbox.ClientRectangle);
                            encoderparameter = new EncoderParameter(encoder, (long)EncoderValue.FrameDimensionPage);
                            encoderparameters.Param[0] = encoderparameter;
                            tmp.SaveAdd(tmp2, encoderparameters);
                        }

                        encoderparameter.Dispose();
                        encoderparameters.Dispose();
                    }
                    else
                    {
                        tmp.Save(this.SaveFileDialog1.FileName, imageFormat);
                    }
                }

                tmp.Dispose();
            }
        }
    }

    private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        var page = this.TabControl1.SelectedTab;
        var maxSize = new Size(256, 256);
        if (page != null)
        {
            var originalBitmap = OriginalBitmaps[page.Text];
            if (this.hicon != IntPtr.Zero)
            {
                // Destroy it.
                SafeNativeMethods.DestroyIcon(this.hicon);
                this.hicon = IntPtr.Zero;
            }

            if (originalBitmap.Size.Width > maxSize.Width || originalBitmap.Size.Height > maxSize.Height)
            {
                var origimage = originalBitmap.GetThumbnailImage(
                    Convert.ToInt32(originalBitmap.Width * 0.25),
                    Convert.ToInt32(originalBitmap.Height * 0.25),
                    null,
                    IntPtr.Zero);
                var image = new Bitmap(256, 256, originalBitmap.PixelFormat);
                var g = Graphics.FromImage(image);
                g.DrawImage(
                    origimage,
                    new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height),
                    GraphicsUnit.Pixel);
                g.Dispose();
                this.hicon = image.GetHicon();
                image.Dispose();
                origimage.Dispose();
            }

            this.Icon = Icon.FromHandle(this.hicon);
            var zoomlabel = $"{Convert.ToInt32(PicboxList[page.Text].Zoom * 100.0)}%";
            for (var i = 0; i < this.ZoomToolStripMenuItem.DropDownItems.Count; i++)
            {
                var item = (ToolStripMenuItem)this.ZoomToolStripMenuItem.DropDownItems[i];
                item.Checked = zoomlabel.Equals(item.Text) ? true : false;
            }
        }
    }

    private void ToolStripTextBox1_Leave(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count > 0)
        {
            var page = this.TabControl1.SelectedTab;
            var width = Convert.ToInt32(this.ToolStripTextBox1.Text);
            var picboxzoom = PicboxList[page.Text];
            picboxzoom.Picturebox.Width = width;
            picboxzoom.Zoom = 0;
            PicboxList[page.Text] = picboxzoom;
        }
    }

    private void ToolStripTextBox2_Leave(object sender, EventArgs e)
    {
        if (OriginalBitmaps.Count > 0)
        {
            var page = this.TabControl1.SelectedTab;
            var height = Convert.ToInt32(this.ToolStripTextBox2.Text);
            var picboxzoom = PicboxList[page.Text];
            picboxzoom.Picturebox.Height = height;
            picboxzoom.Zoom = 0;
            PicboxList[page.Text] = picboxzoom;
        }
    }

    private void PictureBox1_MouseEnter(object sender, EventArgs e)
    {
        var pnlContain = Panels[this.TabControl1.SelectedTab.Text];
        pnlContain.Focus();
        Panels[this.TabControl1.SelectedTab.Text] = pnlContain;
    }

    private void Panel1_MouseWheel(object sender, MouseEventArgs e)
    {
        var pnlContain = Panels[this.TabControl1.SelectedTab.Text];
        if (e.Delta > 0)
        {
            if (pnlContain.VerticalScroll.Value - 2 >= pnlContain.VerticalScroll.Minimum)
            {
                pnlContain.VerticalScroll.Value -= 2;
            }
            else
            {
                pnlContain.VerticalScroll.Value = pnlContain.VerticalScroll.Minimum;
            }
        }
        else
        {
            if (pnlContain.VerticalScroll.Value + 2 <= pnlContain.VerticalScroll.Minimum)
            {
                pnlContain.VerticalScroll.Value += 2;
            }
            else
            {
                pnlContain.VerticalScroll.Value = pnlContain.VerticalScroll.Maximum;
            }
        }

        Panels[this.TabControl1.SelectedTab.Text] = pnlContain;
    }
}
