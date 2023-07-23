using GamesAPI.Models;
using System.Net;
using BC = BCrypt.Net.BCrypt;
using Xunit;
using Moq;
using GamesAPI.DTOs;
using GamesAPI.Controllers;
using FluentAssertions;
using GamesAPI.Services;

namespace GameAPI.Tests
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<ITokenService> _tokenService;

        public AuthControllerTest()
        {
            _authService = new Mock<IAuthService>();
            _tokenService = new Mock<ITokenService>();
            _userService = new Mock<IUserService>();
        }

        [Fact]
        public void Wrong_Data_Fails_Authentication()
        {
            var userList = GetUsers();
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

            var dto = new LoginDTO()
            {
                Email = "wrong@mail.com",
                Password = "password"
            };
            //arrange
            var controller = new AuthController(_authService.Object, _userService.Object, _tokenService.Object);

            //act
            var loginResult = controller.LogIn(dto);
            //assert

            loginResult.Should().NotBe((int)HttpStatusCode.OK);
        }

        [Fact]
        public void Correct_Data_Gets_Response()
        {
            var userList = GetUsers();
            _userService.Setup(u => u.GetAllUsers()).Returns(userList);
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
    }
}