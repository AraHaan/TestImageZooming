namespace TestImageZooming
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            Debug.WriteLineIf(!Directory.Exists($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts"), $"The script folder '{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts' was not found.");
#if DEBUG
            Debug.WriteLineIf(!Directory.Exists($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts"), "Trying with repository the root folder copy.");
            var scriptDir = Directory.Exists($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts")
                ? $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts"
                : $"{Environment.CurrentDirectory}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Scripts";
#else
            var scriptDir = Directory.Exists($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts")
                ? $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts"
                : throw new DirectoryNotFoundException($"The script folder '{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts' was not found.");
#endif
            using (var strm = new MemoryStream())
            {
                using (var fstream = File.OpenRead($"{scriptDir}{Path.DirectorySeparatorChar}Main.csx"))
                {
                    fstream.CopyTo(strm);
                }

                var options = ScriptOptions.Default.WithFilePath($"{scriptDir}{Path.DirectorySeparatorChar}Main.csx");
                options = options.WithEmitDebugInformation(true);
                var script = CSharpScript.Create(strm, options);
                var tmpcode = new byte[script.Code.Length];
                var index = 0;
                foreach (var @char in script.Code.ToCharArray())
                {
                    tmpcode[index] = (byte)@char;
                    index++;
                }

#if DEBUG
                using (var fstream = File.Create("tmpcode.cs"))
                {
                    fstream.Write(tmpcode, 0, tmpcode.Length);
                }
#endif

                try
                {
                    var scriptTask = script.RunAsync();
                }
                catch (CompilationErrorException ex)
                {
                    _ = MessageBox.Show($"{ex.Message}{Environment.NewLine}{ex.StackTrace}", "Script Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
                    Environment.Exit(ex.HResult);
                }
            }
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            _ = MessageBox.Show($"{ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.StackTrace}", "Script Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine($"{ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.StackTrace}");
            Application.Exit();
        }
    }
}
