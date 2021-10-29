using NUnit.Framework;
using System;
using System.Collections.Generic;
using PolicyLibrary.Validators.Abstractions;
using PolicyLibrary.Validators.Rules;

namespace PolicyLibrary.Tests.RuleTests
{
    public class MinimumLengthRuleTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            List<IPolicyRule> _rules = new()
            {
                new MinimumLengthRule(12)
            };

            _validator = new Validator("MinimumLengthRuleTest", _rules);
        }

        [TestCase("ashdaskjdJsk2319@$21")]
        public void MinimumLengthRule_ValidInput_ShouldReturnTrue(string testCase)
        {
            // Arrange
            bool expected = true;

            // Arc
            bool actual = _validator.Validate(testCase);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual);
        }

        [TestCase("127389a43$s")]
        public void MinimumLengthRule_InvalidInput_ShouldReturnFalse(string testCase)
        {
            // Arrange
            bool expected = false;

            // Act
            bool actual = _validator.Validate(testCase);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual);
            Assert.IsFalse(actual);
        }

        [TestCase("asdhj23198")]
        public void MinimumLengthRule_InvalidInput_ShouldThrowArgumentException(string testCase)
        {
            // Arrange
            string msg = string.Empty;

            // Act
            bool actual = _validator.Validate(testCase);

            // Assert
            Assert.IsNotNull(actual);
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                foreach (var ex in _validator.GetExceptions)
                {
                    msg = ex.Message;
                    throw ex;
                }
            });

            Assert.That(ex.Message, Is.EqualTo(msg));
        }
    }
}
