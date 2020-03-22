using System;
using System.Net.Mail;
using System.Text;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Concrete
{
    public class EmailInfo
    {
        public static string MailToAddress => "lnt397@gmail.com";
        public static string MailFromAddress => "msngomsn@gmail.com";
        public static string Username => "msngomsn@gmail.com";
        public static string Password => "***inger";
        public static string ServerName => "smtp.gmail.com";
        public static string Subject => "Order";
        public static int ServerPort => 587;
        public static bool UseSsl => true;
        public static bool SmtpUseCredentials => true;

        public static string GetMessage(Cart cart, Customer customer)
        {
            var body = new StringBuilder()
                .AppendLine(new string('=', 40) + "<br/>")
                .AppendLine("Games:<br/>");

            foreach (var line in cart.Lines)
            {
                var subtotal = line.Game.Price * line.Quantity;
                body.AppendFormat($"{line.Game.Name} x {line.Quantity} = ${subtotal:G} <br/>");
            }

            body.AppendFormat($"Total price: {cart.ComputeTotalValue()} <br/>")
                .AppendLine(new string('=', 40) + "<br/>")
                .AppendLine("Customer: <br/>")
                .AppendLine($"Name: {customer.Name} <br/>")
                .AppendLine($"Email: {customer.Email} <br/>")
                .AppendLine($"Phone: {customer.Phone} <br/>")
                .AppendLine(new string('=', 40));

            return body.ToString();
        }
    }
}

