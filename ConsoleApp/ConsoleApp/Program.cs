using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //dynamic price = "22.948675";
            //Console.WriteLine(price.GetType());
            //Myclass myclass = new Myclass();
            //myclass.Name = "my name is";
            //dynamic dyClass = myclass;
            //Console.WriteLine(dyClass.GetType());
            //int a = 93487;
            //a.isLessThan(834567);
            //string str = "djfgb";
            //string result = str.StringHelp("aaa");
            //ExtensionClass.StaticMethor();
            //Bike bike = new Bike();

            //string c = "abc";
            //string d = "abc";
            //// d += "bc";
            //bool test = ((object)c == (object)d);
            //bool test1 = (c == d);
            //bool test2 = ((object)c).Equals((object)d);

            //IVehicle motobike = new Motobike();
            //Console.WriteLine(motobike.Wheels);
            Distance distance = new Distance();
            distance.meter = 5;
            distance.meter++;
            distance++;
            Console.WriteLine(distance.meter);

        }
    }
    public static class ExtensionClass
    {
        public static bool isLessThan(this int origin, int compareValue)
        {
            //return true if origin value is less
            if (origin < compareValue)
                return true;
            else
                return false;
        }
        public static string StringHelp(this string origin, string input)
        {
            int inCommpare = input.CompareTo(origin);
            return inCommpare > 0 ? origin : input;
        }
        public static void StaticMethor()
        { }
    }
    class Myclass
    {
        string name;

        public string Name { get => name; set => name = value; }
        public Vector MyVector()
        {
            Vector vt = new Vector(22, 3);
            return vt;
        }
    }
    struct Vector
    {
        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        int x;
        int y;
    }
    abstract class Vehicle
    {
        protected int wheels;
        public int Wheels { get { return wheels; } }

        public string MyPro { get => myPro; set => myPro = value; }
        public int MyInt { get => myInt; set => myInt = value; }

        string myPro;
        private int myInt;
        protected abstract string Move();


    }
    class Bike : Vehicle
    {
        public Bike()
        {
            base.wheels = 2;
            Rose tree = new Rose();
            string outTree = tree.Color;
        }
        protected override string Move()
        {
            return "";
        }
    }
    public class Tree
    {
        public string Color { get; set; }
    }
    public class Rose : Tree
    {
        public Rose()
        {
            base.Color = "red";

            Temperature temp = new Temperature();
            float temp_0 = temp[0];
            temp[2] = 498.0F;
        }
    }
    class Temperature
    {
        private float[] weekTemp = { 47.5F, 40.0F, 52.5F, 45.5F, 48.0F, 38.0F, 35.7F };
        public float this[int index] { get { return weekTemp[index]; } set { weekTemp[index] = value; } }



    }
    interface IVehicle
    {
        int Wheels { get; }

    }
    class Motobike : IVehicle
    {
        private int wheels;

        public int Wheels { get { return wheels; } }
    }
    class Calculator
    {
        public int  Add(int a , int b)
        {
             int result = a + b;
            return result;
        }
        public void Add(string a , string b)
        {

        }
        public void Add(int a, string b, int c)
        {
            Student s1 = new Student { Marks = 23 };
            Student s2 = new Student { Marks = 34 };
            Student s3 = s1 + s2;

        }

    }
    public class Distance
    {
        public int meter { get; set; }
        public static Distance operator ++(Distance dis)
        {
            dis.meter += 3;
            return dis;
        }

    }
    class Student
    {
        public int Marks { get; set; }
        public static  Student operator + (Student s1, Student s2)
        {
            Student sdt = new Student();
            sdt.Marks = s1.Marks + s2.Marks;
            return sdt;

        }
    }   
}
