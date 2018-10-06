#load "..\..\Interfaces\IExtension.csx"
#load "RecolorImageFrm.csx"

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class RecolorImageExtension : IExtension
{
    public string ExtensionRootMenu => "Edit";

    public ToolStripMenuItem ExtensionMenuItem
    {
        get
        {
            var item = new System.Windows.Forms.ToolStripMenuItem();
            item.Name = "RecolorImageToolStripMenuItem";
            item.Size = new Size(39, 20);
            item.Text = "Recolor Image";
            item.Click += new EventHandler(this.ExtensionMenuItem_Click);
            return item;
        }
    }

    public Form Parrent { get; set; }

    public void ExtensionMenuItem_Click(object sender, EventArgs e)
    {
        RecolorImageFrm.ColorDialog1 = new ColorDialog
        {
            AnyColor = true,
            Color = Color.Transparent,
            SolidColorOnly = true,
        };
        RecolorImageFrm.Result = RecolorImageFrm.ColorDialog1.ShowDialog();
        if (RecolorImageFrm.Result != DialogResult.Cancel)
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
