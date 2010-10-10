using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using DeanIS.YBSquare.Web.Services;
using DeanIS.YBSquare.WebSite.Controllers;
using DeanIS.YBSquare.WebSite.Models;
using Moq;
using NUnit.Framework;

namespace DeanIS.YBSquare.Tests.Web.Controllers
{
    public abstract class when_using_the_account_controller : context
    {
        protected AccountController sut;
        protected Mock<IFormsAuthenticationService> formsAuthenticationServiceMock;
        protected Mock<IMembershipService> membershipService;
        protected Mock<HttpContextBase> httpContext;
        protected Mock<HttpRequestBase> httpRequest;
        protected ActionResult result;

        protected const string username = "someUser";

        public override void setupContext()
        {
            httpContext = new Mock<HttpContextBase>();
            formsAuthenticationServiceMock = new Mock<IFormsAuthenticationService>();
            membershipService = new Mock<IMembershipService>();

            httpRequest = new Mock<HttpRequestBase>();
            httpRequest.Setup(hr => hr.Url).Returns(new Uri("http://mysite.example.com/"));

            httpContext.Setup(hc => hc.User).Returns(new GenericPrincipal(new GenericIdentity(username), null));
            httpContext.Setup(hc => hc.Request).Returns(httpRequest.Object);

            var requestContext = new RequestContext(httpContext.Object, new RouteData());
            sut = new AccountController(formsAuthenticationServiceMock.Object, membershipService.Object)
                      {
                          Url = new UrlHelper(requestContext)
                      };

            sut.ControllerContext = new ControllerContext()
                                        {
                                            Controller = sut,
                                            RequestContext = requestContext
                                        };
        }
    }

    [TestFixture]
    public class when_requesting_to_change_my_password : when_using_the_account_controller
    {
        public override void act()
        {
            result = sut.ChangePassword();
        }

        [Test]
        public void model_is_setup_with_the_correct_data()
        {
            Assert.That(((ViewResult) result).ViewData["PasswordLength"],
                        Is.EqualTo(membershipService.Object.MinPasswordLength));
        }

        [Test]
        public void view_is_of_correct_type()
        {
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }
    }

    public abstract class when_changing_a_password : when_using_the_account_controller
    {
        protected abstract ChangePasswordModel model { get; }

        public override void act()
        {
            result = sut.ChangePassword(model);
        }
    }

    [TestFixture]
    public abstract class when_entering_an_acceptable_password : when_changing_a_password
    {
        protected override ChangePasswordModel model
        {
            get
            {
                return new ChangePasswordModel()
                           {
                               OldPassword = "goodOldPassword",
                               NewPassword = "goodNewPassword",
                               ConfirmPassword = "goodNewPassword"
                           };
            }
        }

        public override void extendContext()
        {
            membershipService.Setup(
                ms => ms.ChangePassword(username, model.NewPassword, model.ConfirmPassword)).Returns(true);
        }
        
        [Test]
        public void verify_that_password_is_changed_successfully()
        {
            membershipService.Verify(ms => ms.ChangePassword(username, model.OldPassword, model.NewPassword),
                                         Times.Once());

            var redirectToResult = result as RedirectToRouteResult;
            Assert.That(redirectToResult, Is.Not.Null);
            Assert.That(redirectToResult, Is.InstanceOf(typeof(RedirectToRouteResult)));
            Assert.That(redirectToResult.RouteValues["action"], Is.EqualTo("ChangePasswordSuccess"));
        }
    }

    [TestFixture]
    public class when_entering_an_invalid_password : when_changing_a_password
    {
        protected override ChangePasswordModel model
        {
            get
            {
                return new ChangePasswordModel()
                           {
                               OldPassword = "goodOldPassword",
                               NewPassword = "badNewPassword",
                               ConfirmPassword = "badNewPassword"
                           };
            }
        }

        [Test]
        public void the_model_data_is_setup_properly()
        {
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            var viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
        }

        [Test]
        public void the_correct_error_message_is_specified_for_the_view()
        {
            Assert.That(sut.ModelState[string.Empty].Errors[0].ErrorMessage,
                        Is.EqualTo("The current password is incorrect or the new password is invalid."));
        }

        [Test]
        public void the_password_length_is_set_in_the_model()
        {
            var viewResult = (ViewResult)result;
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }
    }

    [TestFixture]
    public class AccountControllerTest
    {
        [Test]
        public void ChangePassword_Post_ReturnsViewIfModelStateIsInvalid()
        {
            // Arrange
            AccountController controller = GetAccountController();
            ChangePasswordModel model = new ChangePasswordModel()
            {
                OldPassword = "goodOldPassword",
                NewPassword = "goodNewPassword",
                ConfirmPassword = "goodNewPassword"
            };
            controller.ModelState.AddModelError("", "Dummy error message.");

            // Act
            ActionResult result = controller.ChangePassword(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        [Test]
        public void ChangePasswordSuccess_ReturnsView()
        {
            // Arrange
            AccountController controller = GetAccountController();

            // Act
            ActionResult result = controller.ChangePasswordSuccess();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }

        [Test]
        public void LogOff_LogsOutAndRedirects()
        {
            // Arrange
            var controller = GetAccountController();

            // Act
            ActionResult result = controller.LogOff();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectToRouteResult)));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);

            //Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignOut_WasCalled);
        }

        [Test]
        public void LogOn_Get_ReturnsView()
        {
            // Arrange
            AccountController controller = GetAccountController();

            // Act
            ActionResult result = controller.LogOn();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
        }

        [Test]
        public void LogOn_Post_ReturnsRedirectOnSuccess_WithoutReturnUrl()
        {
            // Arrange
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                Email = "someEmail",
                Password = "goodPassword",
                RememberMe = false
            };

            // Act
            ActionResult result = controller.LogOn(model, null);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectToRouteResult)));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            //Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        }

        [Test]
        public void LogOn_Post_ReturnsRedirectOnSuccess_WithLocalReturnUrl()
        {
            // Arrange
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                Email = "someEmail",
                Password = "goodPassword",
                RememberMe = false
            };

            // Act
            ActionResult result = controller.LogOn(model, "/someUrl");

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectResult)));
            RedirectResult redirectResult = (RedirectResult)result;
            Assert.AreEqual("/someUrl", redirectResult.Url);
            //Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        }

        [Test]
        public void LogOn_Post_ReturnsRedirectToHomeOnSuccess_WithExternalReturnUrl()
        {
            // Arrange
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                Email = "someEmail",
                Password = "goodPassword",
                RememberMe = false
            };

            // Act
            ActionResult result = controller.LogOn(model, "http://malicious.example.net");

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectToRouteResult)));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            //Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        }

        [Test]
        public void LogOn_Post_ReturnsViewIfModelStateIsInvalid()
        {
            // Arrange
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                Email = "someEmail",
                Password = "goodPassword",
                RememberMe = false
            };
            controller.ModelState.AddModelError("", "Dummy error message.");

            // Act
            ActionResult result = controller.LogOn(model, null);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
        }

        [Test]
        public void LogOn_Post_ReturnsViewIfValidateUserFails()
        {
            // Arrange
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                Email = "someEmail",
                Password = "badPassword",
                RememberMe = false
            };

            // Act
            ActionResult result = controller.LogOn(model, null);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual("The user name or password provided is incorrect.", controller.ModelState[""].Errors[0].ErrorMessage);
        }

        [Test]
        public void Register_Get_ReturnsView()
        {
            // Arrange
            AccountController controller = GetAccountController();

            // Act
            ActionResult result = controller.Register();

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            Assert.AreEqual(10, ((ViewResult)result).ViewData["PasswordLength"]);
        }

        [Test]
        public void Register_Post_ReturnsRedirectOnSuccess()
        {
            // Arrange
            AccountController controller = GetAccountController();
            RegisterModel model = new RegisterModel()
            {
                Email = "goodEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };

            // Act
            ActionResult result = controller.Register(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(RedirectToRouteResult)));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        }

        [Test]
        public void Register_Post_ReturnsViewIfRegistrationFails()
        {
            // Arrange
            AccountController controller = GetAccountController();
            RegisterModel model = new RegisterModel()
            {
                Email = "duplicateEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };

            // Act
            ActionResult result = controller.Register(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual("Username already exists. Please enter a different user name.", controller.ModelState[""].Errors[0].ErrorMessage);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        [Test]
        public void Register_Post_ReturnsViewIfModelStateIsInvalid()
        {
            // Arrange
            AccountController controller = GetAccountController();
            RegisterModel model = new RegisterModel()
            {
                Email = "goodEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };
            controller.ModelState.AddModelError("", "Dummy error message.");

            // Act
            ActionResult result = controller.Register(model);

            // Assert
            Assert.That(result, Is.InstanceOf(typeof(ViewResult)));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        private static AccountController GetAccountController()
        {
            var formsAuthenticationServiceMock = new Mock<IFormsAuthenticationService>();
            var membershipSeviceMock = new Mock<IMembershipService>();

            var requestContext = new RequestContext(new MockHttpContext(), new RouteData());
            var controller = new AccountController(formsAuthenticationServiceMock.Object, membershipSeviceMock.Object)
                                 {
                                     Url = new UrlHelper(requestContext)
                                 };

            controller.ControllerContext = new ControllerContext()
                                               {
                                                   Controller = controller,
                                                   RequestContext = requestContext
                                               };
            return controller;
        }

        private class MockFormsAuthenticationService : IFormsAuthenticationService
        {
            public bool SignIn_WasCalled;
            public bool SignOut_WasCalled;

            public void SignIn(string userName, bool createPersistentCookie)
            {
                // verify that the arguments are what we expected
                Assert.AreEqual("someUser", userName);
                Assert.IsFalse(createPersistentCookie);

                SignIn_WasCalled = true;
            }

            public void SignOut()
            {
                SignOut_WasCalled = true;
            }
        }

        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someUser"), null /* roles */);
            private readonly HttpRequestBase _request = new MockHttpRequest();

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return _request;
                }
            }
        }

        private class MockHttpRequest : HttpRequestBase
        {
            private readonly Uri _url = new Uri("http://mysite.example.com/");

            public override Uri Url
            {
                get
                {
                    return _url;
                }
            }
        }

        private class MockMembershipService : IMembershipService
        {
            public int MinPasswordLength
            {
                get { return 10; }
            }

            public bool ValidateUser(string email, string password)
            {
                return (email == "email@email.com" && password == "goodPassword");
            }

            public MembershipCreateStatus CreateUser(string email, string password)
            {
                if (email == "email@email.com")
                {
                    return MembershipCreateStatus.DuplicateUserName;
                }

                // verify that values are what we expected
                Assert.AreEqual("goodPassword", password);
                Assert.AreEqual("goodEmail", email);

                return MembershipCreateStatus.Success;
            }

            public bool ChangePassword(string email, string oldPassword, string newPassword)
            {
                return (email == "email@email.com" && oldPassword == "goodOldPassword" && newPassword == "goodNewPassword");
            }
        }

    }
}
