using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Authenticator;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("lindping", "11111111111111111111", 300, 300);

           var result = tfa.ValidateTwoFactorPIN("11111111111111111111", "429586");

        }
    }
}
