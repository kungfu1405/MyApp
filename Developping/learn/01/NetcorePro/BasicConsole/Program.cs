using BasicConsole.Enums;
using BasicConsole.Interfaces;
using BasicConsole.Structs;
using System;
using System.Collections;

namespace BasicConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Hashtable owner = new Hashtable();
            owner.Add("Bill", "Microsoft");
            owner.Add("Paul", "Microsoft");
            owner.Add("Steve", "Apple");
            owner.Add("Mark", "Facebook");
            foreach(DictionaryEntry item in owner)
            {

            }
            Queue days = new Queue();
            days.Enqueue("thuhai");
            days.Enqueue("Tue");
            days.Enqueue("Wed");
            days.Enqueue("Thu");
            days.Enqueue("Fri");
            days.Enqueue("Sat");
            days.Enqueue("Sun");

            Queue hashCode = new Queue();
            hashCode = (Queue)days.Clone();
            Console.WriteLine("Total elements in queue are {0}", hashCode.Count);
            hashCode.Dequeue();
            Console.WriteLine("Total elements in queue are {0}", hashCode.Count);
            Console.WriteLine("Total elements in queue are {0}", (string)days.Peek());
            object[] newarr = days.ToArray();            


        }
    }
    class MyClass:MyInterface
    {
        public  int add(int a , int b)
        {
            return a + b;
        }

    }
    class GenericClass<T>
    {
        private T genericField;

        public T GenericProperty { get => genericField; set => genericField = value; }

        public T GenericMethod(T genericP)
        {
            return this.genericField = genericP;
        }

    } 
        

}
