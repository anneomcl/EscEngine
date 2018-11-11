using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscVmTests
    {
        [TestMethod]
        public void EscVmRunsTalkEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"));
            EscVirtualMachine.Instance.RunEvent(events, "talk");
        }

        [TestMethod]
        public void EscVmRunsExamineEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"));
            EscVirtualMachine.Instance.RunEvent(events, "examine");
        }
    }
}
