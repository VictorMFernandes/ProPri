using Microsoft.Extensions.DependencyInjection;
using Rise.Email.Api;
using Rise.Email.Api.Setup;
using System;

namespace Rise.Email.AntiCorruption
{
    public static class SendGridExtensions
    {
        public static EmailBuilder AddSendGrid(this EmailBuilder builder,
            Action<SendGridOptions> configureOptions)
        {
            var sendGridOptions = new SendGridOptions();
            configureOptions.Invoke(sendGridOptions);

            builder.Services.AddSingleton(c => sendGridOptions);
            builder.Services.AddSingleton<IMailerFacade, SendGridMailerFacade>();

            return builder;
        }
    }
}