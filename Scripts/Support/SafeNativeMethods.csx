using System;
using System.Runtime.InteropServices;

internal class SafeNativeMethods
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern bool DestroyIcon(IntPtr hicon);
}
