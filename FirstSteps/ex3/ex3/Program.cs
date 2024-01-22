Console.WriteLine("input: ");
var userInput = Console.ReadLine();

Console.WriteLine(userInput.GetType());

Console.WriteLine("input num: ");
int userInt = Int32.Parse(Console.ReadLine());

if (userInt % 2 == 0)
{
    Console.WriteLine("even");
}
else
{
    Console.WriteLine("odd");
}