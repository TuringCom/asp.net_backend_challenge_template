using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moq;
using TuringBackend.Api.Core;
using TuringBackend.Models;
using TuringBackend.Models.Data;
using Xunit;

namespace TuringBackend.UnitTests
{
    public class AuthenticationServiceTests
    {
        private bool _calledRefreshToken = false;

        private Mock<IPasswordHasher> _passwordHasher;
        private Mock<ITokenHandler> _tokenHandler;

        private IAuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            SetupMocks();
            _authenticationService = new AuthenticationService(new TuringBackendContext(), _passwordHasher.Object, _tokenHandler.Object);
        }

        private void SetupMocks()
        {
            _passwordHasher = new Mock<IPasswordHasher>();
            _passwordHasher.Setup(ph => ph.PasswordMatches(It.IsAny<string>(), It.IsAny<string>()))
                           .Returns<string, string>((password, hash) => password == hash);

            _tokenHandler = new Mock<ITokenHandler>();
            _tokenHandler.Setup(h => h.CreateAccessToken(It.IsAny<Customer>()))
                         .Returns(new AccessToken
                                     (
                                        token: "abc",
                                        expiration: DateTime.UtcNow.AddSeconds(30).Ticks,
                                        refreshToken: new RefreshToken
                                                          (
                                                              token: "abc",
                                                              expiration: DateTime.UtcNow.AddSeconds(60).Ticks
                                                          )
                                     )
                                 );

            _tokenHandler.Setup(h => h.TakeRefreshToken("abc"))
                         .Returns(new RefreshToken("abc", DateTime.UtcNow.AddSeconds(60).Ticks));

            _tokenHandler.Setup(h => h.TakeRefreshToken("expired"))
                         .Returns(new RefreshToken("expired", DateTime.UtcNow.AddSeconds(-60).Ticks));

            _tokenHandler.Setup(h => h.TakeRefreshToken("invalid"))
                         .Returns<RefreshToken>(null);

            _tokenHandler.Setup(h => h.RevokeRefreshToken("abc"))
                         .Callback(() => _calledRefreshToken = true);
        }

        [Fact]
        public async Task Should_Not_Refresh_Token_When_Token_Is_Expired()
        {
            var response = await _authenticationService.RefreshTokenAsync("expired", "test@test.com");

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Expired refresh token.", response.Message);
        }

        [Fact]
        public async Task Should_Not_Refresh_Token_For_Invalid_User_Data()
        {
            var response = await _authenticationService.RefreshTokenAsync("invalid", "test@test.com");

            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Invalid refresh token.", response.Message);
        }

        [Fact]
        public void Should_Revoke_Refresh_Token()
        {
            _authenticationService.RevokeRefreshToken("abc");
            
            Assert.True(_calledRefreshToken);
        }
    }
}
