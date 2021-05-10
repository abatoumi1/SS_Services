using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityDemo.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            SendEmail(email, subject, message);
            return Task.CompletedTask;
        }

        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                using (MailMessage mail = new MailMessage(fromAddress, email))
                {
                   // mail.From = new MailAddress(fromAddress);
                   // mail.To.Add(email);
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = false;
                   // smtpClient.UseDefaultCredentials = true;
                    //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                    using (SmtpClient smtp = new SmtpClient(host, port))
                    {
                        smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
                        smtp.EnableSsl = enableSSL;
                        smtp.UseDefaultCredentials = true;
                        smtp.Send(mail);
                       
                    }
                }
            }catch(Exception exp)
            {
                var err = exp.Message;
            }
        }

            static string fromAddress = "djanguiforpeople@gmail.com";
        static string fromPassword = "djanguiforpeople007";
        static string host = "smtp.gmail.com";
        static int port = 587;
        static bool initialized = false;
        static bool enableSSL = true;

        static Regex securityCheck = new Regex("<script>", RegexOptions.IgnoreCase);
        static Regex htmlCheck = new Regex("^<html>", RegexOptions.IgnoreCase);

        public static event Action<Exception> OnSendFail;
        public static event Action<MailMessage> OnSendComplete;

        /// <summary>
        /// Initializes the email server and from address to use for all messages
        /// </summary>
        /// <param name="fromAddress">The address to sign e-mail messages from</param>
        /// <param name="host">The location of the email server</param>
        public static void Initialize(string fromAddress, string host, int port = 587, bool enableSSL = false, string fromPassword = null)
        {
            EmailSender.fromAddress = fromAddress;
            EmailSender.host = host;
            EmailSender.port = port;
            EmailSender.enableSSL = enableSSL;
            EmailSender.fromPassword = fromPassword;

            initialized = true;
        }

        /// <summary>
        /// Creates a new client
        /// </summary>
        public EmailSender()
        {
        }

        /// <summary>
        /// Sends a message to a single recipient.  Must have called Initialize at some point before this.
        /// </summary>
        /// <param name="to">The address to send the message to</param>
        /// <param name="subject">The subject line of the message</param>
        /// <param name="body">The body of the message</param>
        public string Send(string to, string subject, string body, IEnumerable<Attachment> images = null)
        {
            return Send(new List<string> { to }, subject, body, images);
        }

        /// <summary>
        /// Sends a message to multiple recipients.  This will send a single message with multiple addresses in the to field that all recipients can see.  Must have called Initialize at some point before this.
        /// </summary>
        /// <param name="to">An enumerable type containing addresses of all intended recipients</param>
        /// <param name="subject">The subject line of the message</param>
        /// <param name="body">The body of the message</param>
        public string Send(IEnumerable<string> to, string subject, string body, IEnumerable<Attachment> images = null)
        {
            string response = "No Response";
            //if (!initialized)
            //{
            //    throw new InvalidOperationException("Must call STSEmailClient.Initialize before sending");
            //}

            if (securityCheck.IsMatch(body))
            {
                throw new FormatException("E-mails cannot contain scripting tags for security purposes");
            }

            if (!htmlCheck.IsMatch(body))
            {
                body = string.Format("<html><body>{0}</body></html>", body);
            }

            string altBody = body;

            if (images == null)
            {
                images = new List<Attachment>();
            }

            int iAttachment = 1;

            // Now go through and add in linked resources
            foreach (var attachment in images)
            {
                string tag = string.Format("%IMG{0}%", iAttachment);

                attachment.ContentId = Guid.NewGuid().ToString();
                body = body.Replace(tag, string.Format("<img src=\"cid:{0}\" />", attachment.ContentId));

                if (body.Contains(string.Format("cid:{0}", attachment.ContentId)))
                {
                    attachment.ContentDisposition.Inline = true;
                }
            }

            ThreadPool.QueueUserWorkItem(o =>
            {
                try
                {
                    using (var client = new SmtpClient
                    {
                        Host = host,
                        Port = port,
                        EnableSsl = enableSSL
                    })
                    {
                        if (fromPassword != null)
                        {
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential(fromAddress, fromPassword);
                        }

                        using (var message = new MailMessage())
                        {
                            foreach (var address in to)
                            {
                                message.To.Add(new MailAddress(address));
                            }

                            message.Subject = subject;
                            message.Body = body;
                            message.From = new MailAddress(fromAddress);

                            foreach (var attachment in images)
                            {
                                message.Attachments.Add(attachment);
                            }

                            //message.BodyEncoding = System.Text.Encoding.UTF8;
                            message.IsBodyHtml = true;

                            client.Send(message);

                            if (OnSendComplete != null)
                            {
                                OnSendComplete(message);
                            }
                            response = "Success";
                        }
                    }

                }
                catch (Exception e)
                {
                    if (OnSendFail != null)
                    {
                        OnSendFail(e);
                    }
                    response = e.Message;
                }

            });
            return response;
        }
    }
}
