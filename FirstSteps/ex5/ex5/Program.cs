using System;
// import namespaces consisting structures
using System.Collections.Generic;
namespace Zadanie5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            // new Tab object
            var x = new Tab();
            
            // invoke method for Tab
            x.Add(5);
            x.Add(2);

            Console.WriteLine(x.Mean());
            Console.WriteLine(x.Max());
            Console.WriteLine(x.Min());
            x.Print();
        }
    }
    class Tab
    {
        // class field
        private List<int> _list = new List<int>();
        // constructor
        public Tab() { }
        public void Add(int number)
        {
            _list.Add(number);
        }
        public bool Contains(int number)
        {
            return _list.Contains(number);
        }
        public int Sum()
        {
            return _list.Sum();
        }
        public float Mean()
        {
            return (float) _list.Sum() / (float) _list.Count;
        }
        public int Min()
        {
            return _list.Min();
        }
        public int Max()
        {
            return _list.Max();
        }
        public void Print()
        {
            foreach (int num in _list)
            {
                Console.Write(num + " ");
            }
        }
    }
}