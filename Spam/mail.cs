using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;
using Microsoft.Win32;
using System.Windows;

namespace KL
{
    class mail
    {

        public string[] users = { };
        public string[] pws = { };
        public string provedor = "";
        public string[] destinos = { };
        public int smtpPort = 0;
        public string SmtpSrv = "";
        public string subject = "";

        // agrupar backs

        public bool SendEmail(string text)
        {
            

            for (int i = 0; i < users.Length; i++)
            {
                try
                {

                    string from = users[i] + provedor.Trim(); 
                    MailMessage mM = new MailMessage();
                    mM.From = new MailAddress(from);
                    
                    foreach (string str in destinos)
                    {
                        mM.Bcc.Add(str.Trim());
                        
                    }

                    mM.Subject = subject;
                    mM.Body = text.Replace("\n", "<br>");
                    mM.IsBodyHtml = true;

                    mM.Priority = MailPriority.High;
                    SmtpClient sC = new SmtpClient(SmtpSrv);
                    sC.Port = smtpPort;

                    string user = (users[i]).Trim();
                    string pw = (pws[i]).Trim(); 
                    sC.Credentials = new NetworkCredential(user, pw);
                    sC.Send(mM);
                    return true;
                }
                catch (Exception x)
                {
                        //MessageBox.Show(x.Message);
                }
            }
            return false;
        }

        
    }
}
