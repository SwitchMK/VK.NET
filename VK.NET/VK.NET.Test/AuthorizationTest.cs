using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VK.NET;

namespace VK.NET.Test
{
    [TestClass]
    public class AuthorizationTest
    {
        [TestMethod]
        public void Should_Authorize_When_Authorize_Method_Called()
        {
            var authorization = new Authorization(234234);

            string token, id, url = "http://REDIRECT_URI#access_token=533bacf01e11f55b536a565b57531ad114461ae8736d6506a3&expires_in=86400&user_id=8492";
            bool auth;

            string expectedToken = "533bacf01e11f55b536a565b57531ad114461ae8736d6506a3";

            Authorization.Authorize(url, out token, out id, out auth);

            Assert.AreEqual(expectedToken, token);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void Should_Exception_Be_Thrown()
        {
            var authorization = new Authorization(234234);

            string token, id, url = "http://REDIRECT_URI#533bacf01e11f55b536a565b57531ad114461ae8736d6506a3&expires_in=86400&user_id=8492";
            bool auth;

            Authorization.Authorize(url, out token, out id, out auth);
        }
    }
}
