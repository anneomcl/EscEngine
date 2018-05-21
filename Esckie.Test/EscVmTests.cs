using Esckie.Test.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Esckie.Test
{
    [TestClass]
    public class EscVmTests
    {
        [TestMethod]
        public void EscVmRunsTalkEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), TestEscActions.ScriptActions);
            EscVirtualMachine.RunEvents(events, "talk", typeof(TestEscActions));
        }

        [TestMethod]
        public void EscVmRunsExamineEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), TestEscActions.ScriptActions);
            EscVirtualMachine.RunEvents(events, "examine", typeof(TestEscActions));
        }
    }
}
