using FishMarket.Api.Utils;
using Microsoft.Extensions.Options;
using Moq;

namespace FishMarket.Test.ServiceTests;

public abstract class ServiceTestBuilder()
{
    protected Mock<IOptions<Smtp>> SetupSmtpOptions()
    {
        var options = new Mock<IOptions<Smtp>>();
        options
            .Setup(x => x.Value)
            .Returns(new Smtp()
            {
                EnableSsl = true,
                Host = "smtp.gmail.com",
                Port = 587,
                Sender = new Sender()
                {
                    Email = "company_address@yopmail.com",
                    Password = "company_password",
                }
            });

        return options;
    }

    protected Mock<IOptions<JWTSettings>> SetupJwtSettingOptions()
    {
        var options = new Mock<IOptions<JWTSettings>>();
        options
            .Setup(x => x.Value)
            .Returns(new JWTSettings()
            {
                SecretKey = "KbPeShVmYq3t6w9z$C&F)J@NcQfTjWnZr4u7x!A%D*G-KaPdSgVkXp2s5v8y/B?E",
                AccessTokenExpiresInHour = 60,
                RefreshTokenExpiresInHour = 3600
            });

        return options;
    }
}
