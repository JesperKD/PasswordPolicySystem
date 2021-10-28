using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebLoginDemo.Data.Validators.Abstractions;
using WebLoginDemo.Data.Validators.Rules;

namespace WebLoginDemoTests.RuleTests
{
    public class LowercaseLetterRuleTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            List<IPolicyRule> _rules = new()
            {
                new LowercaseLetterRule()
            };

            _validator = new Validator("LowercaseLetterRuleTest", _rules);
        }

        [TestCase("ashdaskjdJsk2319@$21")]
        public void LowercaseLetterRule_ValidInput_ShouldReturnTrue(string testCase)
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

        [TestCase("123890$£@£12[]}//")]
        public void LowercaseLetterRule_InvalidInput_ShouldReturnFalse(string testCase)
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

        [TestCase("$327489112$£@[]}[}]£")]
        public void LowercaseLetterRule_InvalidInput_ShouldThrowArgumentException(string testCase)
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
