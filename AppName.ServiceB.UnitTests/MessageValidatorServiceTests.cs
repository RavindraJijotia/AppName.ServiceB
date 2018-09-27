using System;
using System.Collections.Generic;
using System.Text;
using AppName.ServiceB.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppName.ServiceB.UnitTests
{
    [TestClass]
    [TestCategory("MessageValidatorServiceTests")]
    public class MessageValidatorServiceTests
    {

        [TestMethod]
        public void MessageValidator_When_Empty_Input_Returns_False()
        {
            string message = string.Empty;
            var messageValidatorService = new MessageValidatorService();
            var result = messageValidatorService.IsValidMessage(message);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void MessageValidator_When_Valid_Input_Returns_True()
        {
            string message = "Hello my name is, name";
            var messageValidatorService = new MessageValidatorService();
            var result = messageValidatorService.IsValidMessage(message);

            Assert.AreEqual(true, result);
        }
    }
}
