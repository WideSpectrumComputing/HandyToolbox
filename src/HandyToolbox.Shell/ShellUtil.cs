
using System;
using System.Diagnostics;

namespace HandyToolbox.Shell
{
    public static class ShellUtil
    {
        /// <summary>
        /// Runs/executes provided command with optional parameters.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <remarks>
        ///<code> Usage sample:
        /// string cmdOutput = RunCommand("date", string.Empty);
        ///</code>
        /// </remarks>
        public static ShellCommandResult RunCommand(string command, string args)
        {
            using var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (string.IsNullOrEmpty(error))
            {
                return new ShellCommandResult(ShellCommandOutcome.Success, output);
            }
            else
            {
                return new ShellCommandResult(ShellCommandOutcome.Error, error);
            }
        }

        #region legacy

        private const string windowsShell = "cmd.exe";
        private const string macosShell = "bash";
        private const string unixShell = "bash";

        private static void RunCommand_AlternativeLegacy()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.RedirectStandardError = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                    psi.FileName = macosShell;
                    break;
                case PlatformID.Unix:
                    psi.FileName = unixShell;
                    break;
                default:
                    psi.FileName = windowsShell;
                    break;
            }
            psi.WorkingDirectory = @"/Users/Andrey";
            Process cmd = Process.Start(psi);

            cmd.StandardInput.WriteLine("ls");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            string commandOutput = cmd.StandardOutput.ReadToEnd();
            Debug.WriteLine(commandOutput);
            Console.WriteLine(commandOutput);
        }

        #endregion legacy
    }
}
