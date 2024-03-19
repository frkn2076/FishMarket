using FishMarket.Api.Services.Implementations;

namespace FishMarket.Test.ServiceTests;

public class EmailServiceTest : ServiceTestBuilder
{
    [Fact]
    public async Task EmailSender_Sends_Email_Properly_With_Valid_Email()
    {
        var options = SetupSmtpOptions();

        var emailSender = new EmailService(options.Object);

        await emailSender.SendAsync("123456", "abc@yopmail.com");
    }

    [Fact]
    public void EmailSender_Throws_Exception_With_Invalid_Email()
    {
        var options = SetupSmtpOptions();

        var emailSender = new EmailService(options.Object);

        Assert.ThrowsAsync<Exception>(async () => await emailSender.SendAsync("123456", "abc"));
    }
}