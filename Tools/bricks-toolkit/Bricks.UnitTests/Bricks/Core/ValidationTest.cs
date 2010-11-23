using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Bricks.Core
{
    [TestFixture]
    public class ValidationTest
    {
        [Test]
        public void ShouldAddAddErrorFromNormalErrorToListItemError()
        {
            Validation validation = new Validation();
            validation.AddError("test", "Hello");
            Validation listValidation = new Validation();
            listValidation.AddErrorFrom(1, validation);
            Assert.AreEqual(1, listValidation.Errors.Count);
        }

        [Test]
        public void ShouldNotAllowMultipleLogicalErrorsForMessageAndObjectPair()
        {
            Validation validation = new Validation();
            validation.AddError("test", "Hello", DateTime.Today.Date);
            validation.AddError("test", "Hello", DateTime.Today.Date);
            Assert.AreEqual(1, validation.ErrorOn(DateTime.Today.Date).Count);
        }

        [Test]
        public void RetrieveListErrorFromValidationWhichContainsNonListErrors()
        {
            Validation validation = new Validation();
            validation.AddError("a", "m");
            validation.AddError("a", "m", 0);
            validation.AddError("a", "m", 1);
            Assert.AreEqual(1, validation.ErrorOn(0).Count);
        }

        [Test]
        public void ErrorOnObject()
        {
            Validation validation = new Validation();
            validation.AddError("a", "m", DateTime.Today.Date);
            List<Error> errors = validation.ErrorOn(DateTime.Today.Date);
            Assert.AreEqual(1, errors.Count);
            errors = validation.ErrorOn(DateTime.Today.Date.AddDays(1));
            Assert.AreEqual(0, errors.Count);
        }
    }
}