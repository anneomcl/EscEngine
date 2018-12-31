using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    /// <summary>
    /// This class requires manual validation at the moment. It is to sverify the test files function without errors
    /// and that the debug output can be validated.
    /// </summary>
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

        [TestMethod]
        public void EscVmRunsControlLogicEventSuccess()
        {
            var events = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "IfSayExample.esc"), Assembly.GetExecutingAssembly());
            var vm = EscVirtualMachine.Instance;
            // Should be:
            // Hello, world!
            // Hello, world!
            vm.Globals.Add("TurtleIsPresent", true);
            vm.RunEvent(events, "talk");

            // Should be:
            // Hello, world!
            // Hello, false world!
            vm.Globals.Add("WaterIsWet", false);
            vm.RunEvent(events, "talk");

            // Should be:
            // Hello, false world!
            vm.Globals["TurtleIsPresent"] = false;
            vm.RunEvent(events, "talk");
        }
    }
}
