public partial class Form1
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
        this.TabControl1 = new System.Windows.Forms.TabControl();
        this.TabPage1 = new System.Windows.Forms.TabPage();
        this.Panel1 = new System.Windows.Forms.Panel();
        this.PictureBox1 = new System.Windows.Forms.PictureBox();
        this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
        this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.OpenImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.CloseImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.SaveImageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.CustomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.WidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        this.ToolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
        this.HeightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
        this.ToolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
        this.ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
        this.ToolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
        this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.TabControl1.SuspendLayout();
        this.TabPage1.SuspendLayout();
        this.Panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
        this.MenuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // TabControl1
        // 
        this.TabControl1.Controls.Add(this.TabPage1);
        this.TabControl1.Location = new System.Drawing.Point(0, 24);
        this.TabControl1.Name = "TabControl1";
        this.TabControl1.SelectedIndex = 0;
        this.TabControl1.Size = new System.Drawing.Size(539, 358);
        this.TabControl1.TabIndex = 0;
        this.TabControl1.Visible = false;
        this.TabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
        // 
        // TabPage1
        // 
        this.TabPage1.Controls.Add(this.Panel1);
        this.TabPage1.Location = new System.Drawing.Point(4, 22);
        this.TabPage1.Name = "TabPage1";
        this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
        this.TabPage1.Size = new System.Drawing.Size(531, 332);
        this.TabPage1.TabIndex = 0;
        this.TabPage1.Text = "image1";
        this.TabPage1.UseVisualStyleBackColor = true;
        // 
        // Panel1
        // 
        this.Panel1.AutoScroll = true;
        this.Panel1.Controls.Add(this.PictureBox1);
        this.Panel1.Location = new System.Drawing.Point(0, 0);
        this.Panel1.Name = "Panel1";
        this.Panel1.Size = new System.Drawing.Size(531, 308);
        this.Panel1.TabIndex = 0;
        this.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseWheel);
        this.Panel1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseWheel);
        // 
        // PictureBox1
        // 
        this.PictureBox1.Location = new System.Drawing.Point(0, 0);
        this.PictureBox1.Name = "PictureBox1";
        this.PictureBox1.Size = new System.Drawing.Size(531, 308);
        this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.PictureBox1.TabIndex = 0;
        this.PictureBox1.TabStop = false;
        this.PictureBox1.MouseHover += new System.EventHandler(this.PictureBox1_MouseEnter);
        // 
        // MenuStrip1
        // 
        this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem});
        this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
        this.MenuStrip1.Name = "MenuStrip1";
        this.MenuStrip1.Size = new System.Drawing.Size(537, 24);
        this.MenuStrip1.TabIndex = 2;
        this.MenuStrip1.Text = "menuStrip1";
        // 
        // FileToolStripMenuItem
        // 
        this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenImageToolStripMenuItem,
            this.CloseImageToolStripMenuItem,
            this.SaveImageAsToolStripMenuItem});
        this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
        this.FileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
        this.FileToolStripMenuItem.Text = "File";
        // 
        // OpenImageToolStripMenuItem
        // 
        this.OpenImageToolStripMenuItem.Name = "OpenImageToolStripMenuItem";
        this.OpenImageToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
        this.OpenImageToolStripMenuItem.Text = "Open Image";
        this.OpenImageToolStripMenuItem.Click += new System.EventHandler(this.OpenImageToolStripMenuItem_Click);
        // 
        // CloseImageToolStripMenuItem
        // 
        this.CloseImageToolStripMenuItem.Name = "CloseImageToolStripMenuItem";
        this.CloseImageToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
        this.CloseImageToolStripMenuItem.Text = "Close Image";
        this.CloseImageToolStripMenuItem.Click += new System.EventHandler(this.CloseImageToolStripMenuItem_Click);
        // 
        // SaveImageAsToolStripMenuItem
        // 
        this.SaveImageAsToolStripMenuItem.Name = "SaveImageAsToolStripMenuItem";
        this.SaveImageAsToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
        this.SaveImageAsToolStripMenuItem.Text = "Save Image As";
        this.SaveImageAsToolStripMenuItem.Click += new System.EventHandler(this.SaveImageAsToolStripMenuItem_Click);
        // 
        // EditToolStripMenuItem
        // 
        this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
        this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
        this.EditToolStripMenuItem.Text = "Edit";
        // 
        // ViewToolStripMenuItem
        // 
        this.ViewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomToolStripMenuItem});
        this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
        this.ViewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
        this.ViewToolStripMenuItem.Text = "View";
        // 
        // ZoomToolStripMenuItem
        // 
        this.ZoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CustomToolStripMenuItem,
            this.ToolStripMenuItem2,
            this.ToolStripMenuItem3,
            this.ToolStripMenuItem4,
            this.ToolStripMenuItem5,
            this.ToolStripMenuItem6,
            this.ToolStripMenuItem7,
            this.ToolStripMenuItem8,
            this.ToolStripMenuItem9,
            this.ToolStripMenuItem10,
            this.ToolStripMenuItem11});
        this.ZoomToolStripMenuItem.Name = "ZoomToolStripMenuItem";
        this.ZoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
        this.ZoomToolStripMenuItem.Text = "Zoom";
        // 
        // CustomToolStripMenuItem
        // 
        this.CustomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WidthToolStripMenuItem,
            this.ToolStripMenuItem1,
            this.ToolStripTextBox1,
            this.HeightToolStripMenuItem,
            this.ToolStripMenuItem12,
            this.ToolStripTextBox2});
        this.CustomToolStripMenuItem.Name = "CustomToolStripMenuItem";
        this.CustomToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
        this.CustomToolStripMenuItem.Text = "Custom";
        // 
        // WidthToolStripMenuItem
        // 
        this.WidthToolStripMenuItem.Enabled = false;
        this.WidthToolStripMenuItem.Name = "WidthToolStripMenuItem";
        this.WidthToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
        this.WidthToolStripMenuItem.Text = "Width:";
        // 
        // ToolStripMenuItem1
        // 
        this.ToolStripMenuItem1.Name = "ToolStripMenuItem1";
        this.ToolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
        // 
        // ToolStripTextBox1
        // 
        this.ToolStripTextBox1.Name = "ToolStripTextBox1";
        this.ToolStripTextBox1.Size = new System.Drawing.Size(100, 23);
        this.ToolStripTextBox1.Leave += new System.EventHandler(this.ToolStripTextBox1_Leave);
        // 
        // HeightToolStripMenuItem
        // 
        this.HeightToolStripMenuItem.Enabled = false;
        this.HeightToolStripMenuItem.Name = "HeightToolStripMenuItem";
        this.HeightToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
        this.HeightToolStripMenuItem.Text = "Height:";
        // 
        // ToolStripMenuItem12
        // 
        this.ToolStripMenuItem12.Name = "ToolStripMenuItem12";
        this.ToolStripMenuItem12.Size = new System.Drawing.Size(157, 6);
        // 
        // ToolStripTextBox2
        // 
        this.ToolStripTextBox2.Name = "ToolStripTextBox2";
        this.ToolStripTextBox2.Size = new System.Drawing.Size(100, 23);
        this.ToolStripTextBox2.Leave += new System.EventHandler(this.ToolStripTextBox2_Leave);
        // 
        // ToolStripMenuItem2
        // 
        this.ToolStripMenuItem2.CheckOnClick = true;
        this.ToolStripMenuItem2.Name = "ToolStripMenuItem2";
        this.ToolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem2.Text = "25%";
        this.ToolStripMenuItem2.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem2_CheckedChanged);
        this.ToolStripMenuItem2.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem3
        // 
        this.ToolStripMenuItem3.CheckOnClick = true;
        this.ToolStripMenuItem3.Name = "ToolStripMenuItem3";
        this.ToolStripMenuItem3.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem3.Text = "50%";
        this.ToolStripMenuItem3.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem3_CheckedChanged);
        this.ToolStripMenuItem3.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem4
        // 
        this.ToolStripMenuItem4.CheckOnClick = true;
        this.ToolStripMenuItem4.Name = "ToolStripMenuItem4";
        this.ToolStripMenuItem4.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem4.Text = "75%";
        this.ToolStripMenuItem4.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem4_CheckedChanged);
        this.ToolStripMenuItem4.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem5
        // 
        this.ToolStripMenuItem5.CheckOnClick = true;
        this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
        this.ToolStripMenuItem5.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem5.Text = "100%";
        this.ToolStripMenuItem5.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem5_CheckedChanged);
        this.ToolStripMenuItem5.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem6
        // 
        this.ToolStripMenuItem6.CheckOnClick = true;
        this.ToolStripMenuItem6.Name = "ToolStripMenuItem6";
        this.ToolStripMenuItem6.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem6.Text = "125%";
        this.ToolStripMenuItem6.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem6_CheckedChanged);
        this.ToolStripMenuItem6.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem7
        // 
        this.ToolStripMenuItem7.CheckOnClick = true;
        this.ToolStripMenuItem7.Name = "ToolStripMenuItem7";
        this.ToolStripMenuItem7.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem7.Text = "150%";
        this.ToolStripMenuItem7.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem7_CheckedChanged);
        this.ToolStripMenuItem7.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem8
        // 
        this.ToolStripMenuItem8.CheckOnClick = true;
        this.ToolStripMenuItem8.Name = "ToolStripMenuItem8";
        this.ToolStripMenuItem8.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem8.Text = "200%";
        this.ToolStripMenuItem8.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem8_CheckedChanged);
        this.ToolStripMenuItem8.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem9
        // 
        this.ToolStripMenuItem9.CheckOnClick = true;
        this.ToolStripMenuItem9.Name = "ToolStripMenuItem9";
        this.ToolStripMenuItem9.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem9.Text = "400%";
        this.ToolStripMenuItem9.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem9_CheckedChanged);
        this.ToolStripMenuItem9.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem10
        // 
        this.ToolStripMenuItem10.CheckOnClick = true;
        this.ToolStripMenuItem10.Name = "ToolStripMenuItem10";
        this.ToolStripMenuItem10.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem10.Text = "800%";
        this.ToolStripMenuItem10.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem10_CheckedChanged);
        this.ToolStripMenuItem10.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // ToolStripMenuItem11
        // 
        this.ToolStripMenuItem11.CheckOnClick = true;
        this.ToolStripMenuItem11.Name = "ToolStripMenuItem11";
        this.ToolStripMenuItem11.Size = new System.Drawing.Size(116, 22);
        this.ToolStripMenuItem11.Text = "1000%";
        this.ToolStripMenuItem11.CheckedChanged += new System.EventHandler(this.ToolStripMenuItem11_CheckedChanged);
        this.ToolStripMenuItem11.Click += new System.EventHandler(this.ZoomToolStripMenuItem_Click);
        // 
        // OpenFileDialog1
        // 
        this.OpenFileDialog1.Filter = "PNG Images|*.png|JPG Images|*.jpg|BMP Images|*.bmp|GIF Images|*.gif";
        this.OpenFileDialog1.ShowReadOnly = true;
        this.OpenFileDialog1.Title = "Select Image to Open:";
        // 
        // SaveFileDialog1
        // 
        this.SaveFileDialog1.Filter = "PNG Images|*.png|JPG Images|*.jpg|BMP Images|*.bmp|GIF Images|*.gif";
        this.SaveFileDialog1.Title = "Save Image As:";
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(537, 381);
        this.Controls.Add(this.MenuStrip1);
        this.Controls.Add(this.TabControl1);
        this.DoubleBuffered = true;
        this.Name = "Form1";
        this.Text = "Image Zooming Test";
        this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
        this.Load += new System.EventHandler(this.Form1_Load);
        this.Resize += new System.EventHandler(this.Form1_Resize);
        this.TabControl1.ResumeLayout(false);
        this.TabPage1.ResumeLayout(false);
        this.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
        this.MenuStrip1.ResumeLayout(false);
        this.MenuStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.TabPage TabPage1;
    private System.Windows.Forms.Panel Panel1;
    private System.Windows.Forms.PictureBox PictureBox1;
    private System.Windows.Forms.MenuStrip MenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem OpenImageToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
    private System.Windows.Forms.ToolStripMenuItem CloseImageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ZoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem5;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem6;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem7;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem8;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem9;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem10;
    private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem11;
    private System.Windows.Forms.SaveFileDialog SaveFileDialog1;
    private System.Windows.Forms.ToolStripMenuItem SaveImageAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem CustomToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem WidthToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem1;
    private System.Windows.Forms.ToolStripTextBox ToolStripTextBox1;
    private System.Windows.Forms.ToolStripMenuItem HeightToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator ToolStripMenuItem12;
    private System.Windows.Forms.ToolStripTextBox ToolStripTextBox2;
    internal System.Windows.Forms.TabControl TabControl1;
}

