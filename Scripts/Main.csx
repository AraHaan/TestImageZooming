#r "System.Drawing"
#r "System.Windows.Forms"
#load "Forms\Form1.csx"
#load "Interfaces\IExtension.csx"
#load "Extensions\Extensions.csx"

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
Application.Run(new Form1());
