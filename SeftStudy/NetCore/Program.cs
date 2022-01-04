using System;
using Microsoft.Extensions.Logging;
namespace NetCore
{
    class Program
    {

        static void Main(string[] args)
        {
            Cart cart = new Cart();
        }

    }

    public class Cart
    {
        private readonly IDatabase db2;
        private readonly IEmailSender email;
        public Cart()
        {
            db2 = new Database();
            email = new EmailSender();
        }
        public void Checkout(int user, int orderId)
        {
            //Database db = new Database();
            //db.Save(orderId);
            
            db2.Save(orderId);

            Logger log = new Logger();
            log.LogInfo();

            //EmailSender es = new EmailSender();
            //es.SendEmail(user);
             //email = new EmailSender();
            email.SendEmail(user);
        }
    }
    public interface IDatabase
    {
        void Save(int OrderId);
    }
    public class Database:IDatabase
    {
        public void Save(int Id)
        {

        }
    }
    public interface IEmailSender
    {
        public void SendEmail(int id);
    }
    public class EmailSender:IEmailSender
    {
        public void SendEmail(int id)
        {

        }
    }
    public class Logger // : ILogger
    {
        public void LogInfo()
        {

        }
    }
    public static class ServiceLocatior
    {

    }
}
