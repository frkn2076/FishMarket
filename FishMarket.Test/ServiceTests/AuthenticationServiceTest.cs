using FishMarket.Api.Services.Implementations;

namespace FishMarket.Test.ServiceTests;

public class AuthenticationServiceTest : ServiceTestBuilder
{
    [Fact]
    public void AuthenticationService_Returns_AuthenticationResponse_Properly()
    {
        var options = SetupJwtSettingOptions();

        var authenticationService = new AuthenticationService(options.Object);

        var response = authenticationService.GenerateToken("abc@yopmail.com");

        Assert.NotNull(response);

        Assert.Multiple(() => 
        {
            Assert.NotNull(response.AccessToken);
            Assert.NotNull(response.RefreshToken);
            Assert.True(response.AccessTokenExpiresInHours > 0);
            Assert.True(response.RefreshTokenExpiresInHours > 0);
        });
    }
}
