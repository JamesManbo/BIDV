using System.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Common.Utils
{
    public class EmailLib
    {
        public string FROM { get; set; }
        public string FROMNAME { get; set; }
        public string HOST { get; private set; }
        public int PORT { get; private set; }
        public string SMTP_USERNAME { get; private set; }
        public string SMTP_PASSWORD { get; private set; }
        public bool IS_USING_AMAZON_SYSTEM { get; private set; }

        private static string[] listMail = {
                    "mca11111@novaon.vn",
                    "mca22222@novaon.vn",
                    "mca33333@novaon.vn",
                    "mca44444@novaon.vn",
                };

        private static string SMTPUser = listMail[randInt()];
        private static string SMTPPassword = "1234567890";

        public EmailLib()
        {
            FROM = ConfigurationSettings.AppSettings["FROM"];
            FROMNAME = ConfigurationSettings.AppSettings["FROMNAME"];
            HOST = ConfigurationSettings.AppSettings["HOST"];
            PORT = int.Parse(ConfigurationSettings.AppSettings["PORT"]);
            SMTP_USERNAME = ConfigurationSettings.AppSettings["SMTP_USERNAME"];
            SMTP_PASSWORD = ConfigurationSettings.AppSettings["SMTP_PASSWORD"];
            IS_USING_AMAZON_SYSTEM = Convert.ToBoolean(ConfigurationSettings.AppSettings["IS_USING_AMAZON_SYSTEM"]);

        }
        //sent.SendMail(email, "", "Onfluencer - Bạn nhận được thông báo mới", textContent);
        //using System.Net.Mail;
        public string SendMail(string TO, string CC, string SUBJECT, string BODY)
        {
            string msg = "";
            if (IS_USING_AMAZON_SYSTEM == true)
            {
                var message = new MailMessage
                {
                    IsBodyHtml = true,
                    From = new MailAddress(FROM, FROMNAME),
                    Subject = SUBJECT,
                    Body = BODY
                };

                string[] s_To = TO.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                message.To.Clear();
                message.CC.Clear();
                foreach (var to in s_To)
                {
                    if (message.To.Where(x => x.Address == to).Count() == 0)//cmt
                    {
                        message.To.Add(to.ToString());
                    }
                }
                if (!string.IsNullOrEmpty(CC))
                {
                    string[] s_CC = CC.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var cc in s_CC)
                    {
                        if (cc.ToString() != "")
                        {
                            message.CC.Add(cc.ToString());
                        }
                    }
                }

                //leave as it is even if you are not sending HTML message
                message.IsBodyHtml = true;
                //set the priority of the mail message to normal
                message.Priority = MailPriority.Normal;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                // Comment or delete the next line if you are not using a configuration set
                //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
                using (var client = new SmtpClient(HOST, PORT))
                {
                    // Pass SMTP credentials
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                    // Enable SSL encryption
                    client.EnableSsl = true;
                    // Try to send the message
                    try
                    {
                        client.Send(message);
                    }
                    catch (Exception ex)
                    {
                        msg =  ex.Message;
                    }
                }
            }
            else
            {
                int sendResult = Send(TO, SUBJECT, BODY, out msg, out string mailSender);
            }
            return msg;
        }

        static bool MySslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // If there are no errors, then everything went smoothly.
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Note: MailKit will always pass the host name string as the `sender` argument.
            var host = (string)sender;

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNotAvailable) != 0)
            {
                // This means that the remote certificate is unavailable. Notify the user and return false.
                Console.WriteLine("The SSL certificate was not available for {0}", host);
                return false;
            }

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) != 0)
            {
                // This means that the server's SSL certificate did not match the host name that we are trying to connect to.
                var certificate2 = certificate as X509Certificate2;
                var cn = certificate2 != null ? certificate2.GetNameInfo(X509NameType.SimpleName, false) : certificate.Subject;

                Console.WriteLine("The Common Name for the SSL certificate did not match {0}. Instead, it was {1}.", host, cn);
                return false;
            }

            // The only other errors left are chain errors.
            Console.WriteLine("The SSL certificate for the server could not be validated for the following reasons:");

            // The first element's certificate will be the server's SSL certificate (and will match the `certificate` argument)
            // while the last element in the chain will typically either be the Root Certificate Authority's certificate -or- it
            // will be a non-authoritative self-signed certificate that the server admin created. 
            foreach (var element in chain.ChainElements)
            {
                // Each element in the chain will have its own status list. If the status list is empty, it means that the
                // certificate itself did not contain any errors.
                if (element.ChainElementStatus.Length == 0)
                    continue;

                Console.WriteLine("\u2022 {0}", element.Certificate.Subject);
                foreach (var error in element.ChainElementStatus)
                {
                    // `error.StatusInformation` contains a human-readable error string while `error.Status` is the corresponding enum value.
                    Console.WriteLine("\t\u2022 {0}", error.StatusInformation);
                }
            }

            return false;
        }

        public static int randInt()
        {
            Random rand = new Random();
            int dice = rand.Next(0, listMail.Length - 1);
            return dice;
        }
        /// <summary>
        ///     Hàm gửi mail tới 1 người nhận, không bao gồm CC và BCC
        /// </summary>
        /// <param name="to">Người nhận</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="body">Nội dung mail</param>
        /// <param name="msg">Nội dung lỗi nếu có</param>
        /// <param name="mailSender">Out ra mail được gửi từ email hệ thống nào</param>
        /// <returns>0: gửi mail không thành công; 1: gửi mail thành công</returns>
        public static int Send(string to, string subject, string body, out string msg, out string mailSender)
        {
            return Send(new[] { to }, null, null, subject, body, out msg, out mailSender);
        }

        /// <summary>
        ///     Hàm gửi mail tới list danh sách nhiều khách hàng, không bao gồm CC và BCC
        /// </summary>
        /// <param name="to">Danh sách ng nhận</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="content">Nội dung mail</param>
        /// <param name="msg">Nội dung lỗi nếu có</param>
        /// <param name="mailSender">Out ra mail được gửi từ email hệ thống nào</param>
        /// <returns>0: gửi mail không thành công; 1: gửi mail thành công</returns>
        public static int Send(string sender, string[] to, string subject, string content, out string msg, out string mailSender)
        {
            return Send(to, null, null, subject, content, out msg, out mailSender);
        }

        /// <summary>
        ///     Hàm gửi mail tới list danh sách nhiều khách hàng, kèm danh sách CC
        /// </summary>
        /// <param name="to">Danh sách ng nhận</param>
        /// <param name="cc">Danh sách CC</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="content">Nội dung mail</param>
        /// <param name="msg">Nội dung lỗi nếu có</param>
        /// <param name="mailSender">Out ra mail được gửi từ email hệ thống nào</param>
        /// <returns>0: gửi mail không thành công; 1: gửi mail thành công</returns>
        public static int Send(string[] to, string[] cc, string subject, string content, out string msg, out string mailSender)
        {
            return Send(to, cc, null, subject, content, out msg, out mailSender);
        }
        /// <summary>
        ///     Hàm gửi mail tới list danh sách nhiều khách hàng, kèm danh sách CC và BCC
        /// </summary>
        /// <param name="tolist">Danh sách ng nhận</param>
        /// <param name="ccList">Danh sách CC</param>
        /// <param name="bccList">Danh sách BCC</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="body">Nội dung mail</param>
        /// <param name="msg">Nội dung lỗi nếu có</param>
        /// <param name="mailSender">Out ra mail được gửi từ email hệ thống nào</param>
        /// <returns>0: gửi mail không thành công; 1: gửi mail thành công</returns>
        public static int Send(string[] tolist, string[] ccList, string[] bccList, string subject, string body, out string msg, out string mailSender)
        {
            try
            {
                //string SMTPUser = Utils.GetSetting<string>(MAIL_SENDER, MAIL_ADDRESS);
                //string SMTPPassword = Utils.GetSetting<string>(MAIL_PASSWORD, MAIL_PASS);
                SMTPUser = listMail[randInt()];
                //Now instantiate a new instance of MailMessage
                using (var mail = new MailMessage())
                {
                    //set the sender address of the mail message
                    mail.From = new MailAddress(SMTPUser, "Novaon Onfluencer");

                    //set the recepient addresses of the mail message
                    if (tolist != null && tolist.Any())
                        foreach (var to in tolist)
                        {
                            if (to != null)
                            {
                                string to1 = to.Trim();
                                if (to.Trim().EndsWith(","))
                                {
                                    to1 = to.Substring(0, to.Length - 1);
                                }
                                if (to1.Trim().Length > 0 && mail.To.Where(x => x.Address == to1).Count() == 0)
                                {
                                    mail.To.Add(to1.Trim());
                                }
                            }
                        }

                    //set the recepient addresses of the mail message
                    if (ccList != null && ccList.Any())
                        foreach (var cc in ccList)
                        {
                            if (cc != null)
                            {
                                string cc1 = cc.Trim();
                                if (cc.Trim().EndsWith(","))
                                {
                                    cc1 = cc.Substring(0, cc.Length - 1);
                                }
                                if (cc1.Trim().Length > 0)
                                {
                                    mail.CC.Add(cc1.Trim());
                                }
                            }
                        }

                    if (bccList != null && bccList.Any())
                        foreach (var bcc in bccList)
                        {
                            if (bcc != null)
                            {
                                mail.Bcc.Add(bcc);
                            }
                        }

                    //set the subject of the mail message
                    mail.Subject = subject;

                    //set the body of the mail message
                    mail.Body = body;

                    //leave as it is even if you are not sending HTML message
                    mail.IsBodyHtml = true;

                    //set the priority of the mail message to normal
                    mail.Priority = MailPriority.Normal;

                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.BodyEncoding = Encoding.UTF8;

                    //instantiate a new instance of SmtpClient
                    using (var smtp = new SmtpClient())
                    {
                        //if you are using your smtp server, then change your host like "smtp.yourdomain.com"

                        smtp.Host = "smtp.gmail.com";

                        //chnage your port for your host
                        smtp.Port = 587; //or you can also use port# 587

                        //provide smtp credentials to authenticate to your account
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);

                        //if you are using secure authentication using SSL/TLS then "true" else "false"
                        smtp.EnableSsl = true;

                        smtp.Send(mail);

                        msg = string.Empty;
                        mailSender = SMTPUser;
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger.Instance.Info(string.Format("Send email exception: {0}\t\n {1}", ex.Message, subject));
                msg = ex.ToString();
                mailSender = SMTPUser;
                return 0;
            }
        }

        /// <summary>
        ///     Hàm gửi mail tới list danh sách nhiều khách hàng, có đính kèm file
        /// </summary>
        /// <param name="tolist">Danh sách ng nhận</param>
        /// <param name="ccList">Danh sách CC</param>
        /// <param name="subject">Tiêu đề mail</param>
        /// <param name="body">Nội dung mail</param>
        /// <param name="Attachfile">Danh sách đường dẫn file đính kèm</param>
        /// <returns>0: gửi mail không thành công; 1: gửi mail thành công</returns>
        public static int Send(string[] tolist, string[] ccList, string subject, string body, string[] Attachfile)
        {
            try
            {
                //string SMTPUser = Utils.GetSetting<string>(MAIL_SENDER, MAIL_ADDRESS);
                //string SMTPPassword = Utils.GetSetting<string>(MAIL_PASSWORD, MAIL_PASS);
                SMTPUser = listMail[randInt()];
                //Now instantiate a new instance of MailMessage
                using (var mail = new MailMessage())
                {
                    //set the sender address of the mail message
                    mail.From = new MailAddress(SMTPUser, "NOVAON Onfluencer");

                    //set the recepient addresses of the mail message

                    if (tolist != null && tolist.Any())
                        foreach (var to in tolist)
                        {
                            if (to != null)
                            {
                                string to1 = to;
                                if (to.Trim().EndsWith(","))
                                {
                                    to1 = to.Substring(0, to.Length - 1);
                                }
                                if (to1.Trim().Length > 0)
                                {
                                    mail.To.Add(to1);
                                }
                            }
                        }

                    //set the recepient addresses of the mail message
                    if (ccList != null && ccList.Any())
                        foreach (var cc in ccList)
                        {
                            if (cc != null)
                            {
                                string cc1 = cc;
                                if (cc.Trim().EndsWith(","))
                                {
                                    cc1 = cc.Substring(0, cc.Length - 1);
                                }
                                if (cc1.Trim().Length > 0)
                                {
                                    mail.CC.Add(cc1);
                                }
                            }
                        }

                    //set the subject of the mail message
                    mail.Subject = subject;

                    //set the body of the mail message
                    mail.Body = body;

                    //leave as it is even if you are not sending HTML message
                    mail.IsBodyHtml = true;

                    //set the priority of the mail message to normal
                    mail.Priority = MailPriority.Normal;

                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.BodyEncoding = Encoding.UTF8;

                    //mail.Attachments.Add(new Attachment(_ms, "ABC-Certificate.Pdf", "application/pdf"));
                    System.Net.Mail.Attachment attachment;
                    foreach (var file in Attachfile)
                    {
                        if (file != null && file.Length > 1)
                        {
                            attachment = new System.Net.Mail.Attachment(file);
                            mail.Attachments.Add(attachment);
                        }
                    }

                    //instantiate a new instance of SmtpClient
                    using (var smtp = new SmtpClient())
                    {
                        //if you are using your smtp server, then change your host like "smtp.yourdomain.com"
                        smtp.Host = "smtp.gmail.com";

                        //chnage your port for your host
                        smtp.Port = 587; //or you can also use port# 587

                        //provide smtp credentials to authenticate to your account
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPassword);

                        //if you are using secure authentication using SSL/TLS then "true" else "false"
                        smtp.EnableSsl = true;

                        smtp.Send(mail);

                        return 1;
                    }
                }
            }
            catch (Exception)
            {
                //Logger.Instance.Info(string.Format("Send email exception: {0}\t\n {1}", ex.Message, subject));
                return 0;
            }
            finally
            {
                foreach (var file in Attachfile)
                {
                    if (file != null && file.Length > 1)
                    {
                        if (System.IO.File.Exists(file))
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                }
            }
        }
    }
}
