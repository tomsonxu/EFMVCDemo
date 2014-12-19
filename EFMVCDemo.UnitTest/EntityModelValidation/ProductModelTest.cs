using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFMVCDemo.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFMVCDemo.UnitTest.EntityModelValidation
{
    [TestClass]
    public class ProductModelTest
    {
        //LogTime (type is datetime) field is required, but if not set it, validate successfully!!!

        [TestMethod]
        public void Test_RequiredField_IsNull_Validate_Failed()
        {
            Product obj = new Product { };
            var validationErrorResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), validationErrorResults, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationErrorResults.Count);
        }

        [TestMethod]
        public void Test_Description_Field_Length_MoreThan30_Validate_Failed()
        {
            Product obj = new Product { Name="abc", Description = "1234567890123456789012345678901" };
            var validationErrorResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), validationErrorResults, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationErrorResults.Count);
        }

        [TestMethod]
        public void Test_RequiredField_IsNull_And_Description_Field_Length_MoreThan30_Validate_Failed_ErrorCount_Is_2()
        {
            Product obj = new Product { Description = "1234567890123456789012345678901" };
            var validationErrorResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), validationErrorResults, true);

            Assert.IsFalse(isValid);
            Assert.AreEqual(2, validationErrorResults.Count);  //validation error count is 2: required failed and length failed!
        }
    }
}
