//
// ShellUtil.cs
//
// Author:
//       Andrey Kornich <akornich@gmail.com>
//
// Copyright (c) 2020 dotNeat
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

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
