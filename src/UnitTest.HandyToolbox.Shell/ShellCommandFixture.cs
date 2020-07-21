using System;
using System.Linq;

using HandyToolbox.Shell;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.HandyToolbox.Shell
{
    [TestClass]
    public class ShellCommandFixture
    {
        [TestMethod]
        public void RunCommand()
        {
            var cmdResult = ShellUtil.RunCommand("date", "+%T");
            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(ShellCommandOutcome.Success, cmdResult.Outcome);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(cmdResult.Output));
            var dateTime = DateTime.Parse(cmdResult.Output);
            Assert.IsTrue(DateTime.Now.Subtract(dateTime) < TimeSpan.FromSeconds(1));

            cmdResult = ShellUtil.RunCommand("git", "branch --show-current");
            Assert.IsNotNull(cmdResult);
            Assert.AreEqual(ShellCommandOutcome.Success, cmdResult.Outcome);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(cmdResult.Output));
            string[] likelyBranches = new string[] { "master", "development", };
            Assert.IsTrue(likelyBranches.Contains(cmdResult.Output.Replace(Environment.NewLine, string.Empty)));
        }
    }
}
