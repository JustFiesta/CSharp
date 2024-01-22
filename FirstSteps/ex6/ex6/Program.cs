using System;

string[] file = System.IO.File.ReadAllLines(@"C:\Users\Masti\Desktop\Programowanie\C#\FirstSteps\ex6\ex6\iris.csv");

foreach (String line in file)
{
    Console.WriteLine(line);
}