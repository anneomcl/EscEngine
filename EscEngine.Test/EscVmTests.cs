using Esckie.Test.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Esckie.Test
{
    [TestClass]
    public class EscVmTests
    {
        [TestMethod]
        public void EscVmRunsSayEvent()
        {
            var events = EscCompiler.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), TestEscActions.ScriptActions);
            EscVirtualMachine.RunEvents(events, "Say", typeof(TestEscActions));
        }
    }
}
