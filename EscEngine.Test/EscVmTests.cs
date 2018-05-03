using EscEngine.Test.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace EscEngine.Test
{
    [TestClass]
    public class EscVmTests
    {
        [TestMethod]
        public void EscVmRunsTalkEvent()
        {
            var events = EscCompiler.Compile(Path.Combine("../../TestData/", "TalkExamineSample.esc"), TestEscActions.ScriptActions);
            EscVirtualMachine.RunEvents(events, "talk", typeof(TestEscActions));
        }
    }
}
