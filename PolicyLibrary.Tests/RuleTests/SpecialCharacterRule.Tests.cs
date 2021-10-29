using NUnit.Framework;
using System;
using System.Collections.Generic;
using PolicyLibrary.Validators.Abstractions;
using PolicyLibrary.Validators.Rules;

namespace PolicyLibrary.Tests.RuleTests
{
    public class SpecialCharacterRuleTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            List<IPolicyRule> _rules = new()
            {
                new SpecialCharacterRule()
            };

            _validator = new Validator("SpecialCharacterRuleTest", _rules);
        }

        [TestCase("ashdaskjdJsk2319@$21")]
        public void SpecialCharacterRule_ValidInput_ShouldReturnTrue(string testCase)
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

        [TestCase("asdhjk31802130sajkdl")]
        public void SpecialCharacterRule_InvalidInput_ShouldReturnFalse(string testCase)
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

        [TestCase("asdhkj213890sdj")]
        public void SpecialCharacterRule_InvalidInput_ShouldThrowArgumentException(string testCase)
        {
            // Arrange
            string msg = string.Empty;

            // Act
            bool actual = _validator.Validate(testCase);

            // Assert
            var ex = Assert.Throws<ArgumentException>(() =>
            {
                foreach (var ex in _validator.GetExceptions)
                {
                    msg = ex.Message;
                    throw ex;
                }
            });

            Assert.IsNotNull(ex);
            Assert.That(ex.Message, Is.EqualTo(msg));
        }

        [TestCase("sadhjkd21380sa123")]
        public void SpecialCharacterRule_InvalidAmountOfRequiredSpecialCharacters_ShouldThrowArgumentException(string testCase)
        {
            // Arrange
            List<IPolicyRule> _rules = new()
            {
                new SpecialCharacterRule(1)
            };

            var validator = new Validator("SpecialCharacterRuleTest", _rules);
            string msg = string.Empty;


            // Act
            bool actual = validator.Validate(testCase);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                foreach (var ex in validator.GetExceptions)
                {
                    msg = ex.Message;
                    throw ex;
                }
            });

            Assert.That(ex.Message, Is.EqualTo(msg));
        }

        [TestCase("ashjSd2318790Fdsfd@34")]
        public void SpecialCharacterRule_ValidAmountOfRequiredSpecialCharacters_ShouldReturnTrue(string testCase)
        {
            // Arrange
            List<IPolicyRule> _rules = new()
            {
                new CapitalLetterRule(1)
            };

            var validator = new Validator("SpecialCharacterRuleTest", _rules);
            bool expected = true;


            // Act
            bool actual = validator.Validate(testCase);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
