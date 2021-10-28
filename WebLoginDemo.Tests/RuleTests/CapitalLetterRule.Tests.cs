using NUnit.Framework;
using System;
using System.Collections.Generic;
using WebLoginDemo.Data.Validators.Abstractions;
using WebLoginDemo.Data.Validators.Rules;

namespace WebLoginDemoTests.RuleTests
{
    public class CapitalLetterRuleTests
    {
        private Validator _validator;

        [SetUp]
        public void SetUp()
        {
            List<IPolicyRule> _rules = new()
            {
                new CapitalLetterRule()
            };

            _validator = new Validator("CapitalLetterRuleTest", _rules);
        }

        [TestCase("ashdaskjdJsk2319@$21")]
        public void CapitalLetterRule_ValidInput_ShouldReturnTrue(string testCase)
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

        [TestCase("asasd213212aslkjads$34w89")]
        public void CapitalLetterRule_InvalidInput_ShouldReturnFalse(string testCase)
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

        [TestCase("1238719€$£€2134")]
        public void CapitalLetterRule_NoLetters_ShouldThrowArgumentException(string testCase)
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

        [TestCase("asdkjas123123$£@as")]
        public void CapitalLetterRule_InvalidInput_ShouldThrowArgumentException(string testCase)
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

        [TestCase("sadhjkd21380sa123")]
        public void CapitalLetterRule_InvalidAmountOfCapitalLetters_ShouldThrowArgumentException(string testCase)
        {
            // Arrange
            List<IPolicyRule> _rules = new()
            {
                new CapitalLetterRule(2)
            };

            var validator = new Validator("CapitalLetterRuleTest", _rules);
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
        public void CapitalLetterRule_ValidAmountOfCapitalLetters_ShouldReturnTrue(string testCase)
        {
            // Arrange
            List<IPolicyRule> _rules = new()
            {
                new CapitalLetterRule(2)
            };

            var validator = new Validator("CapitalLetterRuleTest", _rules);
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
