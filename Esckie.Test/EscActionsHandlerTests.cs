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

        private Dictionary<string, ActionMetadata> GetExpectedEscActions()
        {
            return new Dictionary<string, ActionMetadata>()
            {
                { "CameraToObject", new ActionMetadata { Parameters = new List<Type> { typeof(string) }, ActionType = typeof(CameraEscAction) } },
                { "Say", new ActionMetadata { Parameters = new List<Type> { typeof(string), typeof(string) }, ActionType = typeof(DialogueEscAction) } }
            };
        }
    }
}
