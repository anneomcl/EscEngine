using System;
using System.Collections.Generic;
using System.Reflection;
using Esckie.Actions;
using Esckie.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscActionsHandlerTests
    {
        [TestMethod]
        public void InitializedHandlerShouldConvertAllInterfaceAssembliesToEscActions()
        {
            EscActionsHandler.Initialize(Assembly.GetExecutingAssembly());

            EscActionsHandler.ScriptActions.Should().NotBeNull();
            EscActionsHandler.ScriptActions.Should().BeEquivalentTo(GetExpectedEscActions());
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
