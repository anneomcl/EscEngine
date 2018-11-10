using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Esckie.Test
{
    [TestClass]
    public class EscCompilerHelpersTests
    {
        [TestMethod]
        public void IsCommentHelperShouldReturnTrueForComment()
        {
            var comment = EscCompilerHelpers.CommentIndicator + "Say Turtle Comment";
            EscCompilerHelpers.IsComment(comment).Should().BeTrue();
        }

        [TestMethod]
        public void IsCommentHelperShouldReturnFalseForBlank()
        {
            var notComment = "";
            EscCompilerHelpers.IsComment(notComment).Should().BeFalse();
        }

        [TestMethod]
        public void IsCommentHelperShouldReturnFalseForComment()
        {
            var notComment = "Say Turtle Hi";
            EscCompilerHelpers.IsComment(notComment).Should().BeFalse();
        }

        [TestMethod]
        public void ParseLineToTokensShouldReturnExpectedValues()
        {
            var expectedValues = new List<string>() { "Examine", "Monkey", "Banana" };
            var line = "Examine Monkey Banana";
            EscCompilerHelpers.ParseLineToTokens(line).Should().BeEquivalentTo(expectedValues);
        }

        [TestMethod]
        public void IsEventProcessingFinishedShouldReturnTrueWhenReachingNewEvent()
        {
            var line = EscCompilerHelpers.EventIndicator + "newEvent";
            EscCompilerHelpers.IsEventProcessingFinished(line).Should().BeTrue();
        }

        [TestMethod]
        public void IsEventProcessingFinishedShouldReturnFalseForCurrentEventLine()
        {
            var line = "Look Turtle Chair";
            EscCompilerHelpers.IsEventProcessingFinished(line).Should().BeFalse();
        }

        [TestMethod]
        public void IsEventProcessingFinishedShouldReturnFalseForBlankLine()
        {
            var line = "";
            EscCompilerHelpers.IsEventProcessingFinished(line).Should().BeFalse();
        }

        [TestMethod]
        public void GetIndentationLevelShouldReturnExpectedValueForIndentedBlock()
        {
            var line = "\t\t\tHello";
            EscCompilerHelpers.GetIndentationLevel(line).Should().Be(3);
        }

        [TestMethod]
        public void GetIndentationLevelShouldReturnExpectedValueForNonIndentedLine()
        {
            var line = "";
            EscCompilerHelpers.GetIndentationLevel(line).Should().Be(0);
        }

        [TestMethod]
        public void TryParseEscEventShouldSuccessfullyParseValidEvent()
        {
            var expectedEventName = "newEvent";

            string eventName;
            var line = EscCompilerHelpers.EventIndicator + "newEvent";
            EscCompilerHelpers.TryParseEscEvent(line, out eventName).Should().BeTrue();
            eventName.Should().BeEquivalentTo(expectedEventName);
        }

        [TestMethod]
        public void TryParseEscEventShouldReturnNullForNonEvent()
        {
            string eventName;
            var line = "Say Dog Meow";
            EscCompilerHelpers.TryParseEscEvent(line, out eventName).Should().BeFalse();
            eventName.Should().BeNull();
        }

        [TestMethod]
        public void TryParseEscEventShouldReturnNullForBlank()
        {
            string eventName;
            var line = "";
            EscCompilerHelpers.TryParseEscEvent(line, out eventName).Should().BeFalse();
            eventName.Should().BeNull();
        }
    }
}
