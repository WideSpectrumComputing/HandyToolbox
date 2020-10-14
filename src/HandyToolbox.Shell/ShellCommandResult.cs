//
// ShellCommandResult.cs
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

namespace HandyToolbox.Shell
{
    public class ShellCommandResult
    {

        private readonly ShellCommandOutcome _outcome;
        private readonly string _output;

        private ShellCommandResult()
        {
        }

        public ShellCommandResult(ShellCommandOutcome commandOutcome, string commandOutput)
        {
            this._outcome = commandOutcome;
            this._output = commandOutput;
        }

        public ShellCommandOutcome Outcome { get { return this._outcome; } }

        public string Output { get { return this._output; } }

        public override string ToString()
        {
            return $"{this._outcome.ToString().ToUpper()}: {this._output}";
        }
    }
}
