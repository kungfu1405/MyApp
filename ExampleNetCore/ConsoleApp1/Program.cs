using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public interface text
    {
        void print();
    }
    public class format:text
    {
        public void print()
        { Console.WriteLine(" here is text format"); }
    }
    // constructor injection
    public class constructorinjection
    {
        private text _text;
        public constructorinjection(text t1)
        {
            this._text = t1;
        }
        public void print()
        {
            _text.print();
        }
    }
        class Program
    {
        static void Main(string[] args)
        {
        }
    }
  
  
   
}
