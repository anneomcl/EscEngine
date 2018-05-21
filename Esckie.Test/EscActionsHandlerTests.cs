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
        [TestMethod]
        public void HandlerShouldConvertAllInterfaceAssembliesToEscActions()
        {
            var handler = EscActionsHandler.Instance;
            handler.ScriptActions.Should().NotBeNull();
            handler.ScriptActions.Should().BeEquivalentTo(GetExpectedEscActions());
        }

        private Dictionary<string, ActionInfo> GetExpectedEscActions()
        {
            return new Dictionary<string, ActionInfo>()
            {
                { "CameraToObject", new ActionInfo { Parameters = new List<Type> { typeof(string) }, ActionType = typeof(CameraActions) } },
                { "Say", new ActionInfo { Parameters = new List<Type> { typeof(string), typeof(string) }, ActionType = typeof(DefaultEscActions) } }
            };
        }
    }
}
