using System;
using System.Net.Mail;
using log4net;
using System.IO;
using System.Collections.Generic;

namespace Library
{
    public class MailManager
    {
        protected static readonly ILog log = LogManager.GetLogger(typeof(MailManager));

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Account { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Stream FileStream { get; set; }
        public Dictionary<string, Stream> Files { get; set; }
        public bool UseDefaultCredentials { get; set; }

        public MailManager(string host = "", int port = 25, bool ssl = false, string from = "", string password = "", string account = "", bool useDefaultCredentials = false, string displayName = "")
        {
            this.EnableSSL = ssl;
            this.Port = port;
            this.UseDefaultCredentials = useDefaultCredentials;
            if (!string.IsNullOrEmpty(host)) this.Host = host;
            if (!string.IsNullOrEmpty(from)) this.From = from;
            if (!string.IsNullOrEmpty(password)) this.Password = password;
            if (!string.IsNullOrEmpty(account)) this.Account = account;
            if (!string.IsNullOrEmpty(displayName)) this.DisplayName = displayName;
            Files = new Dictionary<string, Stream>();
        }

        public void SendEmail()
        {
            string send = string.Empty;
            string[] arr = To.Replace(',', ';').Replace('|', ';').Split(';');

            MailMessage msg = new MailMessage();

            From = From.Replace('<', '|').Replace(">", string.Empty);
            if (From.Contains("|"))
            {
                var arrForm = From.Split('|');
                From = arrForm[1];
                DisplayName = arrForm[0];
            }

            if (!string.IsNullOrEmpty(this.DisplayName)) msg.From = new MailAddress(From, this.DisplayName);
            else msg.From = new MailAddress(From);
            msg.Subject = Title;
            msg.Body = Content;
            msg.IsBodyHtml = true;
            
            if (Files != null)
            {
                foreach (var file in Files)
                {
                        Attachment att = new Attachment(file.Value, file.Key);
                        msg.Attachments.Add(att);
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex =
                new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                bool result = regex.IsMatch(arr[i]);
                if (result == false) throw new Exception("Địa chỉ email không hợp lệ.");
                else msg.To.Add(arr[i]);
            }

            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = Host; //Sử dụng SMTP của gmail
                smtp.Port = Port;
                smtp.EnableSsl = EnableSSL;
                smtp.UseDefaultCredentials = UseDefaultCredentials;

                if (string.IsNullOrEmpty(Account)) Account = From;
                smtp.Credentials = new System.Net.NetworkCredential(Account, Password);
                smtp.Send(msg);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            finally
            {
                foreach (var file in Files)
                {
                    file.Value.Dispose();
                }

                if (msg.Attachments != null)
                {
                    for (var i = msg.Attachments.Count - 1; i >= 0; i--)
                    {
                        msg.Attachments[i].Dispose();
                    }
                    msg.Attachments.Clear();
                    msg.Attachments.Dispose();
                }
                msg.Dispose();
                msg = null;
            }
        }
    }
}
