#load "..\Support\PicBoxZoom.csx"

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// interface for extensions.
public interface IExtension
{
    // the menu name to place the extension command in.
    // if menu already exists in the main form it will be added to it at runtime.
    string ExtensionRootMenu { get; }

    // the menu item.
    ToolStripMenuItem ExtensionMenuItem { get; }

    // the form to use as the parrent.
    Form Parrent { get; set; }

    void ExtensionMenuItem_Click(object sender, EventArgs e);
}
