﻿using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class SendEmail : MonoBehaviour
{
    public InputField recipentEmail;
    public InputField bodyMessage;

    public void Send()
    {
        // Create mail
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("smart.sketch.services@gmail.com");
        mail.To.Add("smart.sketch.services@gmail.com");
        mail.Subject = recipentEmail.text;
        mail.Body = bodyMessage.text;

        // Setup server 
        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential("smart.sketch.services@gmail.com", "Parola12345*") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        // Send mail to server, print results
        try
        {
            smtpServer.Send(mail);
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        }
        catch (System.Exception e)
        {
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        }
    }
}
