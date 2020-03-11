using System.Collections.Generic;
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
            var controller = new EscController(Assembly.GetExecutingAssembly());
            var events = controller.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"));
            controller.RunEvent(events, "talk");
        }

        [TestMethod]
        public void EscVmRunsExamineEventSuccess()
        {
            var controller = new EscController(Assembly.GetExecutingAssembly());
            var events = controller.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"));
            controller.RunEvent(events, "examine");
        }

        [TestMethod]
        public void EscVmRunsControlLogicEventSuccess()
        {
            var globals = new Dictionary<string, bool>();
            var controller = new EscController(globals, Assembly.GetExecutingAssembly());
            var events = controller.Compile(Path.Combine("../../TestData/", "IfSayExample.esc"));

            // Should be:
            // Hello, world!
            // Hello, world!
            globals.Add("TurtleIsPresent", true);
            controller.RunEvent(events, "talk");

            // Should be:
            // Hello, world!
            // Hello, false world!
            globals.Add("WaterIsWet", false);
            controller.RunEvent(events, "talk");

            // Should be:
            // Hello, false world!
            globals["TurtleIsPresent"] = false;
            controller.RunEvent(events, "talk");
        }
    }
}
