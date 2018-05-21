using System;
using System.Collections.Generic;
using System.IO;
using Esckie.Test.TestData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscCompilerTests
    {
        [TestMethod]
        public void EscCompilerOpensEmptyFileSuccess()
        {
            var result = EscCompiler.Compile(Path.Combine("../../TestData/", "Empty.esc"), TestEscActions.ScriptActions);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void EscCompilerThrowsOnInvalidFileName()
        {
            Action action = () => EscCompiler.Compile(Path.Combine("../../TestData/", "Invalid.esc"), TestEscActions.ScriptActions);
            action.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        public void EscCompilerDoesNotProcessCommentLine()
        {
            var result = EscCompiler.Compile(Path.Combine("../../TestData/", "Comment.esc"), TestEscActions.ScriptActions);
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void EscCompilerParsesEventsToTable()
        {
            var result = EscCompiler.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"), TestEscActions.ScriptActions);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(GetExpectedResultForSayExamineSample());
        }

        private EscEventTable GetExpectedResultForSayExamineSample()
        {
            return new EscEventTable
            {
                eventTable = new Dictionary<string, EscEvent>()
                {
                    {
                        "talk",
                        new EscEvent
                        {
                            EventName = "talk",
                            EventRoot = new VmCommand
                            {
                                Links = new List<VmCommand>()
                                {
                                    new VmCommand
                                    {
                                        Name = "Say",
                                        Parameters = new string[]
                                        {
                                            "name", "\"Hello, world!\""
                                        }
                                    }
                                }
                            }
                        }
                    },
                    {
                        "examine",
                        new EscEvent
                        {
                            EventName = "examine",
                            EventRoot = new VmCommand
                            {
                                Links = new List<VmCommand>()
                                {
                                    new VmCommand
                                    {
                                        Name = "Say",
                                        Parameters = new string[]
                                        {
                                            "default", "\"Look, it's a robot!\""
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
