namespace TestImageZooming
{
    using System;
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
            var scriptTask = CSharpScript.Create(
                File.OpenRead($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts{Path.DirectorySeparatorChar}Main.csx"),
                ScriptOptions.Default.WithFilePath($"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Scripts{Path.DirectorySeparatorChar}Main.csx")).RunAsync();

            // seems to throw the exception here.
            if (scriptTask.Result.Exception != null)
            {
            }
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            MessageBox.Show($"{ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.StackTrace}", "Script Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine($"{ex.InnerException.Message}{Environment.NewLine}{ex.InnerException.StackTrace}");
            Application.Exit();
        }
    }
}
