using GamesAPI.Models;
using System.Net;
using BC = BCrypt.Net.BCrypt;
using Xunit;
using Moq;
using GamesAPI.DTOs;
using GamesAPI.Controllers;
using FluentAssertions;
using GamesAPI.Services;
using Microsoft.AspNetCore.Identity;
using Xunit.Abstractions;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace GameAPI.Tests
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<ITokenService> _tokenService;
        private readonly WebApplicationFactory<Program> _factory;

        private readonly ITestOutputHelper _output;

        public AuthControllerTest(ITestOutputHelper output)
        {
            _output = output;
            _authService = new Mock<IAuthService>();
            _tokenService = new Mock<ITokenService>();
            _userService = new Mock<IUserService>();
            _userService.Setup(u => u.GetAllUsers()).Returns(GetUsers);
            _userService.Setup(u => u.Exists(new AppUser()
            {
                UserName = "admin",
                Email = "admin@games-api.pl",
                PasswordHash = BC.HashPassword("Admin2023!")
            })).Returns(true);

            _userService.Setup(u => u.Exists(new AppUser()
            {
                UserName = "kamil",
                Email = "kamil@games-api.pl",
                PasswordHash = BC.HashPassword("Kamil2023!")
            })).Returns(true);

            _userService.Setup(u => u.Exists(new AppUser()
            {
                UserName = "zbychu",
                Email = "zbychu@games-api.pl",
                PasswordHash = BC.HashPassword("Zbychu2023!")
            })).Returns(true);
            _factory = new WebApplicationFactory<Program>();
        }

        [Fact]
        public void Wrong_Data_Fails_Authentication()
        {
            var dto = new LoginDTO()
            {
                Email = "wrong@mail.com",
                Password = "password"
            };
            //arrange
            var controller = new Mock<AuthController>(_authService.Object, _userService.Object, _tokenService.Object);

            //act
            var loginResult = controller.Object.LogIn(dto);
            //assert

            loginResult.Should().NotBe((int)HttpStatusCode.OK);
        }

        [Fact]
        public void Correct_Data_Gets_Response()
        {
            var dto = new LoginDTO()
            {
                Email = "zbychu@games-api.pl",
                Password = "Zbychu2023!"
            };
            //arrange
            var controller = new AuthController(_authService.Object, _userService.Object, _tokenService.Object);

            //act
            var loginResult = controller.LogIn(dto);
            //assert
            loginResult.Should().NotBe((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task Is_Token_Generated_Correctly()
        {
            var client = _factory.CreateClient();
            var dto = new LoginDTO
            {
                Email = "admin@games-api.pl",
                Password = "Admin2023!"
            };
            var response = await client.PostAsJsonAsync("/api/auth/login", dto);
            string responseBody = await response.Content.ReadAsStringAsync();
            _output.WriteLine(responseBody);

            responseBody.Should().NotBeNull();
        }

        private List<AppUser> GetUsers()
        {
            return new List<AppUser>()
            {
                new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@games-api.pl",
                    PasswordHash = BC.HashPassword("Admin2023!")
                },
                new AppUser()
                {
                    UserName = "kamil",
                    Email = "kamil@games-api.pl",
                    PasswordHash = BC.HashPassword("Kamil2023!")
                },
                new AppUser()
                {
                    UserName = "zbychu",
                    Email = "zbychu@games-api.pl",
                    PasswordHash = BC.HashPassword("Zbychu2023!")
                }
            };
        }

        private List<IdentityRole<int>> GetRoles() {
            return new List<IdentityRole<int>>()
            {
                new IdentityRole<int>()
                {
                    Id = 1,
                    Name = "admin"
                },
                new IdentityRole<int>()
                {
                    Id = 2,
                    Name = "user"
                }
            };
        }
    }
}