using NUnit.Framework;
using System;
using System.Collections.Generic;
using PolicyLibrary.Validators.Abstractions;
using PolicyLibrary.Validators.Rules;

namespace PolicyLibrary.Tests.RuleTests
{
    class DigitRuleTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            List<IPolicyRule> _rules = new()
            {
                new DigitRule()
            };

            _validator = new Validator("DigitRuleTest", _rules);
        }

        [TestCase("ashdaskjdJsk2319@$21")]
        public void DigitRule_ValidInput_ShouldReturnTrue(string testCase)
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

        [TestCase("dsjkfjsdl$£@{€[")]
        public void DigitRule_InvalidInput_ShouldReturnFalse(string testCase)
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

        [TestCase("ash@£$£}[")]
        public void DigitRule_NoDigits_ShouldThrowArgumentException(string testCase)
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

            Assert.That(ex.Message, Is.EqualTo(msg));
        }

        [TestCase("wdascvcxøv£@as")]
        public void DigitRule_InvalidInput_ShouldThrowArgumentException(string testCase)
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

            Assert.That(ex.Message, Is.EqualTo(msg));
        }

        [TestCase("ashkjd12hlkgfæ")]
        public void DigitRule_InvalidAmountOfDigits_ShouldThrowArgumentException(string testCase)
        {
            // Arrange
            List<IPolicyRule> _rules = new()
            {
                new DigitRule(3)
            };

            var validator = new Validator("DigitRuleTest", _rules);
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
        public void DigitRule_ValidAmountOfDigits_ShouldReturnTrue(string testCase)
        {
            // Arrange
            List<IPolicyRule> _rules = new()
            {
                new DigitRule(3)
            };

            var validator = new Validator("DigitRuleTest", _rules);
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
