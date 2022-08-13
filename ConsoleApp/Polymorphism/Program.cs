using System;

namespace Polymorphism
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle vh = new Bike();
            vh.Run();
        }
    }
    class Vehicle
    {
        public virtual void Run()
        {
            // message
            Console.WriteLine("Run Vehicle");
        }
    }
    class Bike:Vehicle
    {
        public override void Run()
        {
            base.Run();
            Console.WriteLine(" Bike Run ");
        }
    }
}
