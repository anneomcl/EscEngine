using System;
using System.Collections.Generic;
using System.IO;
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
            var result = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "Empty.esc"));
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void EscCompilerThrowsOnInvalidFileName()
        {
            Action action = () => EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "Invalid.esc"));
            action.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        public void EscCompilerDoesNotProcessCommentLine()
        {
            var result = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "Comment.esc"));
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void EscCompilerParsesEventsToTable()
        {
            var expected = this.GetExpectedResultForSayExamineSample();
            var result = EscCompiler.Instance.Compile(Path.Combine("../../TestData/", "SayExamineSample.esc"));
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected);
        }

        private Dictionary<string, EscEvent> GetExpectedResultForSayExamineSample()
        {
            return new Dictionary<string, EscEvent>
            {
                {
                    "talk",
                    new EscEvent("talk")
                    {
                        EventRoot = new EscCommand
                        {
                            Name = "Root",
                            Children = new List<EscCommand>()
                            {
                                new EscCommand
                                {
                                    Name = "Say",
                                    Parameters = new List<string>()
                                    {
                                        "Name", "\"Hello, world!\""
                                    }
                                }
                            }
                        }
                    }
                },
                {
                    "examine",
                    new EscEvent("examine")
                    {
                        EventRoot = new EscCommand
                        {
                            Name = "Root",
                            Children = new List<EscCommand>()
                            {
                                new EscCommand
                                {
                                    Name = "Say",
                                    Parameters = new List<string>()
                                    {
                                        "Default", "\"Look, it's a robot!\""
                                    }
                                },
                                new EscCommand
                                {
                                    Name = "CameraToObject",
                                    Parameters = new List<string>()
                                    {
                                        "Robot"
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
