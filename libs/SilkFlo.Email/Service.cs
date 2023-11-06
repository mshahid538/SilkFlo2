using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using SilkFlo.Data.Core;
using SilkFlo.Data.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SilkFlo.Email
{
    public class Service
    {
        private static string Address => "smtp-relay.sendinblue.com";

        private static int Port => 587;

        private static string Username => "hello@silkflo.com";

        private static string Password => "T79rZdyXCq23j1Mp";

        public static string Email => "hello@silkflo.com";

        public static string Name => "SilkFlo";

        private static SecureSocketOptions SecureSocketOptions => (SecureSocketOptions)1;

        public static bool IsProduction { get; private set; }

        public static string TestEmailAddress { get; set; }

        public static string ApplicationEmailAddress => "hello@silkflo.com";

        public static string Domain { get; private set; }

        public static void Setup(bool isProduction, string testEmailAddress, string domain)
        {
            if (Configuration.Default.ApiKey.Count == 0)
                Configuration.Default.ApiKey.Add("api-key", "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");
            Service.IsProduction = isProduction;
            Service.TestEmailAddress = testEmailAddress;
            Service.Domain = domain;
        }

        public static async Task<string> ValidateEmailAsync(string email, bool checkIsCorporate = true)
        {
            if (!Service.IsEmailFormatValid(email))
                return "Email format is not correct.";
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "https://api.quickemailverification.com/v1/verify?email=" + HttpUtility.UrlEncode(email) + "&apikey=c84b8d4a73a542e1bf6b7b71b0b23594683de096de9f36a528e6f51aa8e0";
                //Verification model = await httpClient.GetFromJsonAsync<Verification>(apiUrl);

                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error with api.quickemailverification.com");
                }

                string json = await response.Content.ReadAsStringAsync();
                Verification model = JsonConvert.DeserializeObject<Verification>(json);



                if (model == null)
                    throw new Exception("Error with api.quickemailverification.com");
                if (model.Success.IndexOf("true", StringComparison.OrdinalIgnoreCase) != 0)
                    return "Email address is not valid.";
                if (!checkIsCorporate || model.Free.IndexOf("true", StringComparison.OrdinalIgnoreCase) != 0)
                    return "";
                string[] parts = email.Split('@');
                return "Cannot use ...@" + parts[1] + " email addresses.";
            }
        }

        public static bool IsEmailFormatValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                email = Regex.Replace(email, "(@)(.+)$", new MatchEvaluator(DomainMapper), RegexOptions.None, TimeSpan.FromMilliseconds(200.0));
            }
            catch (RegexMatchTimeoutException ex)
            {
                return false;
            }
            catch (ArgumentException ex)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email, "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250.0));
            }
            catch (RegexMatchTimeoutException ex)
            {
                return false;
            }

            string DomainMapper(Match match)
            {
                string ascii = new IdnMapping().GetAscii(match.Groups[2].Value);
                return match.Groups[1].Value + ascii;
            }
        }

        public static async Task<string> SendAsync(string subject, Template templateId, MailBox sender, MailBox receiver, BookMark[] bookmarks, bool isUserInvite = false)
        {
            try
            {
                string receiverAddress = receiver.Address;
                if (!Service.IsProduction)
                    receiverAddress = Service.TestEmailAddress;
                if (string.IsNullOrWhiteSpace(receiverAddress))
                    return "";
                TransactionalEmailsApi apiInstance = new TransactionalEmailsApi((Configuration)null);
                GetSmtpTemplateOverview resultTemplate = await apiInstance.GetSmtpTemplateAsync(new long?((long)templateId));
                string html = resultTemplate.HtmlContent;
                BookMark[] bookMarkArray = bookmarks;
                for (int index = 0; index < bookMarkArray.Length; ++index)
                {
                    BookMark bookmark = bookMarkArray[index];
                    string name = bookmark.Name.ToUpper();
                    if (name.IndexOf("contact.", StringComparison.Ordinal) != 0)
                        name = "contact." + name;
                    if (bookmark.GetType().Name == "BookmarkLink")
                    {
                        BookmarkLink bookmark2 = (BookmarkLink)bookmark;
                        string value = bookmark2.Value;
                        if (value.IndexOf(Service.Domain, StringComparison.OrdinalIgnoreCase) == -1 && isUserInvite)
                        {
                            if (value.IndexOf("/", StringComparison.Ordinal) != 0)
                                value = "/" + value;

                            value = Service.Domain + value;
                        }
                        string displayName = bookmark2.DisplayName;
                        if (string.IsNullOrWhiteSpace(displayName))
                            displayName = value;
                        StringBuilder interpolatedStringHandler = new StringBuilder();
                        interpolatedStringHandler.Append("<a href=\"");
                        interpolatedStringHandler.Append(value);
                        interpolatedStringHandler.Append("\">");
                        interpolatedStringHandler.Append(displayName);
                        interpolatedStringHandler.Append("</a>");
                        value = interpolatedStringHandler.ToString();
                        html = html.Replace("{{ " + name + " }}", value);
                        bookmark2 = (BookmarkLink)null;
                        value = (string)null;
                        displayName = (string)null;
                    }
                    else
                        html = html.Replace("{{ " + name + " }}", bookmark.Value);
                    name = (string)null;
                    bookmark = (BookMark)null;
                }
                bookMarkArray = (BookMark[])null;
                SendSmtpEmailSender smtpEmailSender = new SendSmtpEmailSender(sender.Name, sender.Address, new long?());
                SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(receiverAddress, receiver.Name);
                List<SendSmtpEmailTo> to = new List<SendSmtpEmailTo>()
                {
                  smtpEmailTo
                };
                SendSmtpEmail sendSmtpEmail = new SendSmtpEmail(smtpEmailSender, to, (List<SendSmtpEmailBcc>)null, (List<SendSmtpEmailCc>)null, html, "not used", subject, (SendSmtpEmailReplyTo)null, (List<SendSmtpEmailAttachment>)null, (object)null, new long?(), (object)null, (List<SendSmtpEmailMessageVersions>)null, (List<string>)null);
                CreateSmtpEmail createSmtpEmail = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<Tuple<bool, string>> SendAsync(string emailTo, string nameTo, string subject, string body,
            bool isHtml = false, Func<Exception, string, string, string, int, bool> logError = null)
        {
            Dictionary<string, string> toDictionary = !string.IsNullOrWhiteSpace(emailTo) ? new Dictionary<string, string>()
            {
                {
                  emailTo,
                  nameTo
                }
            } : throw new ArgumentNullException(nameof(emailTo));
            
            Tuple<bool, string> tuple = await Service.SendAsync(toDictionary, subject, body, isHtml, logError);
            toDictionary = (Dictionary<string, string>)null;
            
            return tuple;
        }

        public static async Task<Tuple<bool, string>> SendAsync(Dictionary<string, string> toDictionary, string subject, string body,
            bool isHtml = false, Func<Exception, string, string, string, int, bool> logError = null)
        {
            if (toDictionary == null)
                throw new ArgumentNullException(nameof(toDictionary));
            
            using (SmtpClient client = new SmtpClient())
            {
                try
                {
                    ((MailService)client).ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)((s, c, h, e) => true);
                    ((MailService)client).AuthenticationMechanisms.Remove("XOAUTH2");
                    ((MailService)client).Connect(Service.Address, Service.Port, Service.SecureSocketOptions, new CancellationToken());
                }
                catch (SmtpCommandException ex)
                {
                    Func<Exception, string, string, string, int, bool> func = logError;
                    if (func != null)
                    {
                        int num = func((Exception)ex, "", nameof(SendAsync), "SilkFlo.Email\\Service.cs", 116) ? 1 : 0;
                    }
                    return new Tuple<bool, string>(false, "");
                }
                catch (SmtpProtocolException ex)
                {
                    Func<Exception, string, string, string, int, bool> func = logError;
                    if (func != null)
                    {
                        int num = func((Exception)ex, "Protocol error while trying to connect", nameof(SendAsync), "SilkFlo.Email\\Service.cs", 133) ? 1 : 0;
                    }
                    return new Tuple<bool, string>(false, "Protocol error while trying to connect");
                }
                
                if (((Enum)(object)client.Capabilities).HasFlag((Enum)(object)(SmtpCapabilities)8))
                {
                    try
                    {
                        ((MailService)client).Authenticate(Service.Username, Service.Password, new CancellationToken());
                    }
                    catch (AuthenticationException ex)
                    {
                        Func<Exception, string, string, string, int, bool> func = logError;
                        if (func != null)
                        {
                            int num = func((Exception)ex, "Invalid user name or password.", nameof(SendAsync), "SilkFlo.Email\\Service.cs", 152) ? 1 : 0;
                        }
                        return new Tuple<bool, string>(false, "Invalid user name or password.");
                    }
                    catch (SmtpCommandException ex)
                    {
                        Func<Exception, string, string, string, int, bool> func = logError;
                        if (func != null)
                        {
                            int num = func((Exception)ex, "Error trying to authenticate", nameof(SendAsync), "SilkFlo.Email\\Service.cs", 160) ? 1 : 0;
                        }
                        return new Tuple<bool, string>(false, "Error trying to authenticate");
                    }
                    catch (SmtpProtocolException ex)
                    {
                        Func<Exception, string, string, string, int, bool> func = logError;
                        if (func != null)
                        {
                            int num = func((Exception)ex, "Protocol error while trying to authenticate", nameof(SendAsync), "SilkFlo.Email\\Service.cs", 168) ? 1 : 0;
                        }
                        return new Tuple<bool, string>(false, "Protocol error while trying to authenticate");
                    }
                }
                
                try
                {
                    MimeMessage mimeMessage = new MimeMessage();
                    foreach (KeyValuePair<string, string> entry in toDictionary)
                    {
                        mimeMessage.To.Add((InternetAddress)new MailboxAddress(entry.Value, entry.Key));
                        //entry.Key = (string)null;
                        //entry.Value = (string)null;
                    }
                    mimeMessage.Subject = subject;
                    MimeMessage mimeMessage1 = mimeMessage;
                    TextPart textPart;
                    if (!isHtml)
                    {
                        textPart = new TextPart("plain") { Text = body };
                    }
                    else
                    {
                        textPart = new TextPart("html");
                        textPart.Text = body;
                    }
                    mimeMessage1.Body = (MimeEntity)textPart;
                    mimeMessage.Sender = MailboxAddress.Parse(Service.Email);
                    if (!string.IsNullOrEmpty(Service.Name))
                        ((InternetAddress)mimeMessage.Sender).Name = Service.Name;
                    mimeMessage.From.Add((InternetAddress)mimeMessage.Sender);
                    string str1 = await ((MailTransport)client).SendAsync(mimeMessage, new CancellationToken(), (ITransferProgress)null);
                    mimeMessage = (MimeMessage)null;
                }
                catch (SmtpCommandException ex)
                {
                    string userReturnMessage = "Error sending message";
                    SmtpErrorCode errorCode = ex.ErrorCode;
                    switch ((int)errorCode)
                    {
                        case 0:
                            userReturnMessage += "\r\n\tMessage not accepted.";
                            break;
                        case 1:
                            string str2 = userReturnMessage;
                            StringBuilder interpolatedStringHandler1 = new StringBuilder();
                            interpolatedStringHandler1.Append("\r\n\tSender not accepted: ");
                            interpolatedStringHandler1.Append(ex.Mailbox);
                            string stringAndClear1 = interpolatedStringHandler1.ToString();
                            userReturnMessage = str2 + stringAndClear1;
                            break;
                        case 2:
                            string str3 = userReturnMessage;
                            StringBuilder interpolatedStringHandler2 = new StringBuilder();
                            interpolatedStringHandler2.Append("\r\n\tRecipient not accepted: ");
                            interpolatedStringHandler2.Append(ex.Mailbox);
                            string stringAndClear2 = interpolatedStringHandler2.ToString();
                            userReturnMessage = str3 + stringAndClear2;
                            break;
                        case 3:
                            userReturnMessage += "\r\n\tMessage not accepted.";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    Func<Exception, string, string, string, int, bool> func = logError;
                    if (func != null)
                    {
                        int num = func((Exception)ex, userReturnMessage, nameof(SendAsync), "SilkFlo.Email\\Service.cs", 203) ? 1 : 0;
                    }
                    return new Tuple<bool, string>(false, userReturnMessage);
                }
                catch (SmtpProtocolException ex)
                {
                    Func<Exception, string, string, string, int, bool> func = logError;
                    if (func != null)
                    {
                        int num = func((Exception)ex, "Protocol error while sending message", nameof(SendAsync), "SilkFlo.Email\\Service.cs", 211) ? 1 : 0;
                    }
                    return new Tuple<bool, string>(false, "Protocol error while sending message");
                }
              ((MailService)client).Disconnect(true, new CancellationToken());
            }
            return new Tuple<bool, string>(true, "");
        }

        public static async Task<Tuple<bool, string>> SendEmailContactAsync(User user, string subject, string body, string domain)
        {
            StringBuilder interpolatedStringHandler = new StringBuilder();
            interpolatedStringHandler.Append("<p>First Name: ");
            interpolatedStringHandler.Append(user.FirstName);
            interpolatedStringHandler.Append("<br>\r\n");
            interpolatedStringHandler.Append("Last Name: ");
            interpolatedStringHandler.Append(user.LastName);
            interpolatedStringHandler.Append("<br>\r\n");
            interpolatedStringHandler.Append("Email: ");
            interpolatedStringHandler.Append(user.Email);
            interpolatedStringHandler.Append("</p>\r\n\r\n");
            interpolatedStringHandler.Append("<p><a href=\"");
            interpolatedStringHandler.Append(domain);
            interpolatedStringHandler.Append("/account/MuteEmail/");
            interpolatedStringHandler.Append(user.Id);
            interpolatedStringHandler.Append("\">Mute User</a>");
            interpolatedStringHandler.Append("<p>");
            interpolatedStringHandler.Append(body);
            interpolatedStringHandler.Append("</p>");
            string bodyInquiry = interpolatedStringHandler.ToString();

            Tuple<bool, string> result = await Service.SendAsync(Service.Email, Service.Name, Service.Name + " Inquiry: " + subject, bodyInquiry);
            if (!result.Item1)
                return result;
            result = await Service.SendAsync(user.Email, user.Fullname, Service.Name + " Inquiry: " + subject, body);
            return result;
        }

        public static async Task<string> InviteTeamMemberAsync(User proposer, User invitee)
        {
            BookMark[] bookmarks = new BookMark[3]
            {
                new BookMark("INVITEE_FULLNAME", invitee.Fullname),
                new BookMark("FULLNAME", proposer.Fullname),
                (BookMark) new BookmarkLink("PATH", "/SignUp/userId/" + invitee.Id, "Click to complete sign up")
            };

            string str = await Service.SendAsync(proposer.Fullname + " has Invited You to Join SilkFlo", Template.TeamMemberInvitation, new MailBox(Settings.ApplicationName, Service.ApplicationEmailAddress), new MailBox(invitee.Fullname, invitee.Email), bookmarks);
            bookmarks = (BookMark[])null;
            return str;
        }
    }
}
