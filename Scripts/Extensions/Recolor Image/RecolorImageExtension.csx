#load "RecolorImageFrm.csx"

using System;
using System.Drawing;
using System.Windows.Forms;

public class RecolorImageExtension : IExtension
{
    public string ExtensionRootMenu => "Edit";

    public /*System.Windows.Forms.*/ToolStripMenuItem ExtensionMenuItem
    {
        get
        {
            var item = new /*System.Windows.Forms.*/ToolStripMenuItem();
            item.Name = "RecolorImageToolStripMenuItem";
            item.Size = new /*System.Drawing.*/Size(39, 20);
            item.Text = "Recolor Image";
            item.Click += new System.EventHandler(this.ExtensionMenuItem_Click);
            return item;
        }
    }

    public /*System.Windows.Forms.*/Form Parrent { get; set; }

    public void ExtensionMenuItem_Click(object sender, /*System.*/EventArgs e)
    {
        RecolorImageFrm.ColorDialog1 = new /*System.Windows.Forms.*/ColorDialog
        {
            AnyColor = true,
            Color = /*System.Drawing.*/Color.Transparent,
            SolidColorOnly = true,
        };
        RecolorImageFrm.Result = RecolorImageFrm.ColorDialog1.ShowDialog();
        if (RecolorImageFrm.Result != /*System.Windows.Forms.*/DialogResult.Cancel)
        {
            var recolorForm = new RecolorImageFrm
            {
                Owner = this.Parrent,
            };
            recolorForm.ShowDialog();
            recolorForm.Dispose();
            recolorForm = null;
        }

        RecolorImageFrm.ColorDialog1.Dispose();
    }
}
