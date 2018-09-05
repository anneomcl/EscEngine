using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscVmTests
    {
        private EscActionsHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.handler = new EscActionsHandler();
        }

        [TestMethod]
        public void EscVmRunsTalkEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), this.handler.ScriptActions);
            EscVirtualMachine.Instance.RunEvents(events, this.handler, "talk");
        }

        [TestMethod]
        public void EscVmRunsExamineEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), this.handler.ScriptActions);
            EscVirtualMachine.Instance.RunEvents(events, this.handler, "examine");
        }
    }
}
