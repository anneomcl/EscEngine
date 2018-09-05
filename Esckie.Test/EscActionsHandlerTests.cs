using System;
using System.Collections.Generic;
using Esckie.Actions;
using Esckie.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscActionsHandlerTests
    {
        private EscActionsHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.handler = new EscActionsHandler();
        }

        [TestMethod]
        public void HandlerShouldConvertAllInterfaceAssembliesToEscActions()
        {
            this.handler.ScriptActions.Should().NotBeNull();
            this.handler.ScriptActions.Should().BeEquivalentTo(GetExpectedEscActions());
        }

        private Dictionary<string, ActionInfo> GetExpectedEscActions()
        {
            return new Dictionary<string, ActionInfo>()
            {
                { "CameraToObject", new ActionInfo { Parameters = new List<Type> { typeof(string) }, ActionType = typeof(CameraEscActions) } },
                { "Say", new ActionInfo { Parameters = new List<Type> { typeof(string), typeof(string) }, ActionType = typeof(DefaultEscActions) } }
            };
        }
    }
}
