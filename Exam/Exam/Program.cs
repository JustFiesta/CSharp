using System;
using System.Reflection.PortableExecutable;
using static exam.Machines;

namespace exam
{
    class Program
    {

        public static void Main(string[] args)
        {
            Product product = new Product("Czekolada", 112337, new DateOnly(5104, 02, 02));
            //TEST DZIAŁANIA TOSTRING 
            Console.WriteLine(product);

            
            MiniMunchMachine miniMunchMachine = new MiniMunchMachine();

            Product chocolate = new Product("Czekolada", 2, new DateOnly(2024, 2, 17));
            Product pepsi = new Product("Pepsi", 3, new DateOnly(2024, 2, 17));
            Product sandwich = new Product("Kanapka", 4, new DateOnly(2024, 2, 17));
            Product chips = new Product("Chipsy", 2, new DateOnly(2024, 2, 17));
            Product water = new Product("Woda", 1, new DateOnly(2024, 2, 17));

            miniMunchMachine.AddProduct(chocolate, 'S');
            miniMunchMachine.AddProduct(pepsi, 'Z');
            miniMunchMachine.AddProduct(sandwich);
            miniMunchMachine.AddProduct(chips);
            miniMunchMachine.AddProduct(water);

            miniMunchMachine.BuyProduct('Z');

            Console.WriteLine(miniMunchMachine);
            miniMunchMachine.ShowProducts();


            while (true)
            {
                Console.WriteLine("A - pokaz produkt\nB - wrzuc monete\nC - kup produkt\nD - pokaz ilosc wrzuconych monet\nE - wyciagnij monety\nF - wyjscie");
                Console.WriteLine("Wybierz opcje");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "A":
                        miniMunchMachine.ShowProducts();
                        break;
                    case "B":
                        Console.WriteLine("Wrzuc monety");
                        int coin = 0;
                        try
                        {
                            coin = Int32.Parse(Console.ReadLine());
                            miniMunchMachine.AddMoney(coin);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Zła moneta");
                        }
                        break;
                    case "C":

                        break;
                    default:
                        Console.WriteLine("Nic nie wybrales");
                        break;
                }
            }


        }
    }
    class Product
    {
        private string _productName;

        public string productName
        {
            get
            {
                return _productName;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    _productName = value;
                }
                else
                {
                    throw new ArgumentException("Pole nazwy nie może być puste!!!");
                }
            }
        }

        private int _price;
        public int price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentException("Cena nie może być ujemna!!!");
                }
            }
        }

        private DateOnly _expirationDate;
        public DateOnly expirationDate
        {
            get
            {
                return _expirationDate;
            }
            set
            {
                if (value < DateOnly.MinValue)
                {
                    _expirationDate = value;
                }
                else
                {
                    throw new ArgumentException("Pole daty nie może być puste!!!");
                }
            }
        }

        public Product(String Name, int Price, DateOnly Date)
        {
            productName = Name;
            price = Price;
            _expirationDate = Date;
        }

        public override string ToString()
        {
            return $"Nazwa: {productName}, Cena: {price}, Data ważności: {expirationDate.ToString("yyyy-MM-dd")}";
        }
    }
    class Machines
    {
        //: IVendingMachine
        public class MiniMunchMachine : Machine
        {

            //0 wys, 1 szer, 2 gł
            static int[] dimensions = {74, 87, 94};
            static int maxCapacity = 20;
            //coin value
            int cash = 0;
            int previousStateOfcash = 0;
            private Dictionary<char, Product> products = new Dictionary<char, Product>();

            public override void AddMoney(int amount)
            {
                if (amount == 1 | amount == 2 | amount == 5)
                {
                    previousStateOfcash = cash;
                    cash += amount;
                }
                else
                {
                    throw new ArgumentException("Zła kwota! Przyjmuje nominały: 1, 2, 5 EUR");
                }
            }

            public override void AddProduct(Product product)
            {
                if (products.Count >= maxCapacity)
                {
                    throw new ArgumentException("Maszyna jest pełna!");
                }

                char productCode = 'A';
                while (products.ContainsKey(productCode))
                {
                    productCode++;
                    if (productCode > 'Z')
                    {
                        productCode = 'A';
                    }
                }
                products.Add(productCode, product);
            }

            public void AddProduct(Product product, char productCode)
            {
                if (products.Count >= maxCapacity)
                {
                    throw new ArgumentException("Maszyna jest pełna!");
                }

                if (!char.IsLetter(productCode) || products.ContainsKey(productCode))
                {
                    throw new ArgumentException("Nieprawidłowy kod produktu");
                }
                products.Add(productCode, product);
            }
            public void ShowProducts()
            {
                Console.WriteLine("Lista produktów w maszynie:");
                foreach (var entry in products)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value.productName} {entry.Value.price} EUR");
                }
            }

            public override void BuyProduct(char productCode)
            {
                if ((int)productCode >= 65 || (int)productCode <= 90)
                {
                    if (CheckProductState(productCode))
                    {

                        if(products.TryGetValue(productCode, out Product product))
                        {
                            if (cash >= product.price)
                            {
                                cash -= product.price;
                                RemoveProduct(productCode);
                                Console.WriteLine($"Zakupiono produkt: {product.productName} za kwotę: {product.price} EUR");
                            }
                            else
                            {
                                Console.WriteLine("Niewystarczająca kwota wrzucona do kasetki!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Podany produkt nie jest dostępny w maszynie!");
                    }
                }
                else
                {
                    Console.WriteLine("Wprowadzono zły znak! Wprowadź kod produktu od A do Z.");
                }
            }

            public void RemoveProduct(char productCode)
            {
                products.Remove(productCode);
            }

            public bool CheckProductState(char productCode)
            {
                if (products.ContainsKey(productCode))
                {
                    return true;
                }
                return false;
            }

            public override void ReturnCoins()
            {
                Console.WriteLine($"Zwracanie {cash} EUR...");
                cash = 0; // Resetowanie liczby monet
            }
            public override string ToString()
            {
                return $"NazwaAutomatu - szer. {dimensions[1]} cm, gł. {dimensions[2]} cm, wys. {dimensions[0]} cm";
            }
        }

        public class CompactCafe
        {

        }

        public class MicroMarket
        {

        }
    }
    abstract class Machine
    {
        //0 wys, 1 szer, 2 gł
        int[] _dimensions = new int[3];
        int capacity;
        //coin value
        int cash;

        abstract public void AddProduct(Product product);
        abstract public void AddMoney(int amount);
        abstract public void BuyProduct(char productCode);
        abstract public void ReturnCoins();
    }

    interface IVendingMachine
    {
        public void AddProduct(Product product);
        public void AddMoney(int amount);
        public void BuyProduct(char productCode);
        public void ReturnCoins();
    }
}
