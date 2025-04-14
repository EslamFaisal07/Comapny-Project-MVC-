using System.Net;
using System.Net.Mail;

namespace Route.Mvc.PL.Utilites
{
    public static class EmailSettings
    {


        public static void SendEmail(Email email)
        {

            var client = new SmtpClient("smtp.gmail.com" , 587 );
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("eslamfaisal963@gmail.com" , "cklptbvcddcixvlv") ;

            client.Send("eslamfaisal963@gmail.com" ,email.To , email.Subject , email.Body );



        }







    }
}
