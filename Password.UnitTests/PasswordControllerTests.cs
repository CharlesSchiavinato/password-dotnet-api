using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Password.Api.Controllers;
using Password.Api;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Password.UnitTests
{
    public class PasswordControllerTests
    {
        private readonly IConfiguration _configuration;
        private string passwordTemplate = "";
        private int passwordTemplateMaxLength = 50;

        public PasswordControllerTests()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false)
                .AddEnvironmentVariables()
                .Build();

            Password.Api.Password.MinLength = Int32.Parse(_configuration.GetSection("Password").GetSection("MinLength").Value);
            Password.Api.Password.MaxLength = Int32.Parse(_configuration.GetSection("Password").GetSection("MaxLength").Value);
            Password.Api.Password.MinNumber = Int32.Parse(_configuration.GetSection("Password").GetSection("MinNumber").Value);
            Password.Api.Password.MinUpperCase = Int32.Parse(_configuration.GetSection("Password").GetSection("MinUpperCase").Value);
            Password.Api.Password.MinLowerCase = Int32.Parse(_configuration.GetSection("Password").GetSection("MinLowerCase").Value);
            Password.Api.Password.MinSymbol = Int32.Parse(_configuration.GetSection("Password").GetSection("MinSymbol").Value);
            Password.Api.Password.CharSymbol = _configuration.GetSection("Password").GetSection("CharSymbol").Value;
            Password.Api.Password.RepeatChar = Convert.ToBoolean(_configuration.GetSection("Password").GetSection("RepeatChar").Value);

            string LowerCase = "abcdefghijklmnopqrstuvwxyz";
            string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string Number = "0123456789";

            for (int i = 0; i < LowerCase.Length; i++)
            {
                passwordTemplate += LowerCase[i];
                passwordTemplate += UpperCase[i];

                if (i < Number.Length) 
                {
                    passwordTemplate += Number[i];
                }

                if (i < Password.Api.Password.CharSymbol.Length) 
                {
                    passwordTemplate += Password.Api.Password.CharSymbol[i];
                }
            }

            if (Password.Api.Password.MaxLength > 0) {
                passwordTemplateMaxLength = Password.Api.Password.MaxLength;
            }
        }

        [Fact]
        public void Validate_Empty_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            var result = controller.Validate("");

            Assert.IsType<OkObjectResult>(result.Result);

            var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

            Assert.False(validate);
        }

        [Fact]
        public void Validate_WithSpace_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            var result = controller.Validate(" " + passwordTemplate.Substring(0, Password.Api.Password.MinLength));

            Assert.IsType<OkObjectResult>(result.Result);

            var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

            Assert.False(validate);
        }

        [Fact]
        public void Validate_MinLength_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinLength > 1) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, (Password.Api.Password.MinLength - 1)));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_MinLength_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinLength > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, Password.Api.Password.MinLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_MaxLength_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MaxLength > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, (Password.Api.Password.MaxLength + 1)));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_MaxLength_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MaxLength > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, Password.Api.Password.MaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_MinNumber_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinNumber > 0) 
            {
                var result = controller.Validate(Regex.Replace(passwordTemplate, @"[0-9]+", "").Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_MinNumber_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinNumber > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_MinUpperCase_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinUpperCase > 0) 
            {
                var result = controller.Validate(Regex.Replace(passwordTemplate, @"[A-Z]+", "").Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_MinUpperCase_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinUpperCase > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_MinLowerCase_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinLowerCase > 0) 
            {
                var result = controller.Validate(Regex.Replace(passwordTemplate, @"[A-Z]+", "").Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_MinLowerCase_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinLowerCase > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_MinSymbol_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinSymbol > 0) 
            {
                var result = controller.Validate(Regex.Replace(passwordTemplate, @"[" + Password.Api.Password.CharSymbol + "]+", "").Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_MinSymbol_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.MinSymbol > 0) 
            {
                var result = controller.Validate(passwordTemplate.Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_NotRepeatChar_ReturnFalse()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (!Password.Api.Password.RepeatChar) 
            {
                var result = controller.Validate((passwordTemplate[0] + passwordTemplate).Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.False(validate);
            } 
            else 
            {
                Assert.False(false);
            }
        }

        [Fact]
        public void Validate_NotRepeatChar_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (!Password.Api.Password.RepeatChar) 
            {
                var result = controller.Validate((passwordTemplate).Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void Validate_RepeatChar_ReturnTrue()
        {
            var loggerStub = new Mock<ILogger<PasswordController>>();

            var controller = new PasswordController(loggerStub.Object);

            if (Password.Api.Password.RepeatChar) 
            {
                var result = controller.Validate((passwordTemplate[0] + passwordTemplate).Substring(0, passwordTemplateMaxLength));

                Assert.IsType<OkObjectResult>(result.Result);

                var validate = Convert.ToBoolean((result.Result as OkObjectResult).Value);

                Assert.True(validate);
            } 
            else 
            {
                Assert.True(true);
            }
        }
    }
}
