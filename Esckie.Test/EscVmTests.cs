using System.IO;
using Esckie.Actions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscVmTests
    {
        private EscActionsHandler handler = EscActionsHandler.Instance;

        [TestMethod]
        public void EscVmRunsTalkEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), handler.ScriptActions);
            EscVirtualMachine.Instance.RunEvents(events, handler, "talk");
        }

        [TestMethod]
        public void EscVmRunsExamineEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), handler.ScriptActions);
            EscVirtualMachine.Instance.RunEvents(events, handler, "examine");
        }
    }
}
