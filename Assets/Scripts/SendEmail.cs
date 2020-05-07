using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
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
        // mail.From = new MailAddress("smart.sketch.services@gmail.com");
        mail.From = new MailAddress(recipentEmail.text);
        mail.To.Add("smart.sketch.services@gmail.com" + ", " + recipentEmail.text);
        mail.Subject = "Eroare in aplicatie semnalata de " + recipentEmail.text;
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
            SSTools.ShowMessage("Message sent successfully!", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
        catch (System.Exception e)
        {
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            SSTools.ShowMessage("Error! Email couldn't been sent! Please, try again later!", SSTools.Position.bottom, SSTools.Time.twoSecond);
        }
    }
}

