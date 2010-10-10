using System;
using DeanIS.Framework.Testing;
using DeanIS.Net;
using NUnit.Framework;

namespace DeanIS.YBSquare.Api.Tests.Net
{
    [TestFixture, Category("Integration")]
    public class when_making_a_valid_call_to_the_api_with_correct_username_and_password : context
    {
        #region Credentials

        public const string CorrectUsername = "testusername";
        public const string CorrectPassword = "testpassword";
        public const string InCorrectPassword = "testpasword";

        #endregion

        private DigestAuthenticationClient sut;

        protected string response;

        private const string _correctUrl = "http://api.yellowbook.com/yb_rest/GetListings.ashx?what=pizza&where=kop";

        public override void setupContext()
        {
            sut = new DigestAuthenticationClient();
        }

        public override void act()
        {
            response = sut.GetResponseString(CorrectUsername, CorrectPassword,
                                             new Uri(_correctUrl));
        }

        [Test]
        public void the_response_is_not_null()
        {
            Assert.That(response, Is.Not.Null);
        }
    }
}
