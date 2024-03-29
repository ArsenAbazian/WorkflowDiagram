﻿using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace WorkflowDiagram.UI.Blazor.Helpers {
    public class EmailHelper : IEmailSender {
        static string SenderEmail = "lowcoderobot@acodelow.com";
        static string SenderPassword = "vY9cS4eX1x";
        static string SmtpAddress = "mail.hosting.reg.ru";
        static int SmptPort = 25;

        public async Task<bool> SendEmailAsync(string email, string subject, string message) {
            var emailMessage = new MailMessage();

            emailMessage.From = new MailAddress(SenderEmail, "Robot.LowCode");
            emailMessage.To.Add(new MailAddress(email, ""));
            emailMessage.Subject = subject;
            emailMessage.Body = message;
            emailMessage.IsBodyHtml = true;

            using(var client = new System.Net.Mail.SmtpClient(SmtpAddress, SmptPort) {
                UseDefaultCredentials = false,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(SenderEmail, SenderPassword)
            }) {
                try {
                    await client.SendMailAsync(emailMessage);
                }
                catch(Exception e) {
                    Debug.WriteLine(e.ToString());
                    return false;
                }
                return true;
            }
        }

        //public async Task<bool> SendEmailAsync(string email, string subject, string message) {
        //    var emailMessage = new MimeMessage();

        //    emailMessage.From.Add(new MailboxAddress("Robot.LowCode", SenderEmail));
        //    emailMessage.To.Add(new MailboxAddress("", email));
        //    emailMessage.Subject = subject;
        //    emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
        //        Text = message
        //    };

        //    using(var client = new MailKit.Net.Smtp.SmtpClient()) {

        //        try {
        //            await client.ConnectAsync(SmtpAddress, SmptPort, MailKit.Security.SecureSocketOptions.StartTls);
        //            await client.AuthenticateAsync(SenderEmail, SenderPassword);
        //            await client.SendAsync(emailMessage);

        //            await client.DisconnectAsync(true);
        //        }
        //        catch(Exception) {
        //            return false;
        //        }
        //        return true;
        //    }
        //}

        public static bool IsValid(string email) {
            var valid = true;
            try {
                var emailAddress = new MailAddress(email);
            }
            catch {
                valid = false;
            }

            return valid;
        }

        Task IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage) {
            return SendEmailAsync(email, subject, htmlMessage);
        }
    }
}
