#load "..\Support\PicBoxZoom.csx"

using System;
using System.Collections.Generic;
using System.Windows.Forms;

public interface IExtension
{
    public void ImageManipulation(ref List<PicBoxZoom> picBoxZooms, ref TabPage page);
}
