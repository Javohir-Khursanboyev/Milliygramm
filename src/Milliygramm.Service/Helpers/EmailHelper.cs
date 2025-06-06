﻿using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Milliygramm.Service.Helpers;

public static class EmailHelper
{
    public static async ValueTask SendMessageAsync(string To, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(EnvironmentHelper.EmailAddress));
        message.To.Add(MailboxAddress.Parse(To));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = body };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            EnvironmentHelper.SmtpHost,
            Convert.ToInt32(EnvironmentHelper.SmtpPort),
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            EnvironmentHelper.EmailAddress,
            EnvironmentHelper.EmailPassword);

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
