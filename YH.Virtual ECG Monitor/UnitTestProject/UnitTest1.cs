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
//            谷歌身份认证账号 apay_lindping@apay.com
//谷歌身份认证秘钥 MQ3GCZBZGJSTAZBU

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();      
           var result = tfa.ValidateTwoFactorPIN("d6ad92e0d4", "492803");

        }
    }
}
