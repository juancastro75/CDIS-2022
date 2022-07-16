using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService
{
    public static void SendMail()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Juan Castro", "juan.castrors07@gmail.com"));
        message.To.Add(new MailboxAddress("Admin", "juancho33452@gmail.com"));
        message.Subject = "BankConsole: Usuarios Nuevos";

        message.Body = new TextPart("plain")
        {
            Text = GetEmailText()
        };

        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("juan.castrors07@gmail.com", "frmhqpgtohifdxur");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    public static string GetEmailText()
    {
        List<User> newUsers = Storage.GetNewUsers();

        if(newUsers.Count == 0)
            return "No hay usuarios nuevos.";

        string emailText = "Usuarios agregados hoy:\n";

        foreach (User user in newUsers)
            emailText += "\t+ " + user.ShowData() + "\n";
        
        return emailText;
    }
}