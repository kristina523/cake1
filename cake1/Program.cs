using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        CakeOrder order = new CakeOrder();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Кондитерская ===");
            Console.WriteLine("1. Заказать торт");
            Console.WriteLine("2. Посмотреть историю заказов");
            Console.WriteLine("3. Сохранить историю заказов в файл");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите пункт меню: ");

            int choice = ReadIntInput();

            switch (choice)
            {
                case 1:
                    order.PlaceOrder();
                    break;
                case 2:
                    order.ShowOrderHistory();
                    break;
                case 3:
                    Console.WriteLine("Введите путь к файлу для сохранения истории заказов:");
                    string filePath = Console.ReadLine();
                    order.SaveOrderHistoryToFile(filePath);
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static int ReadIntInput()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Неверный ввод. Попробуйте снова.");
            Console.Write("Выберите пункт меню: ");
        }
        return choice;
    }
}


class CakeOrder
{
    private List<Cake> orderedCakes;
    private static string orderHistoryFilePath = "order_history.txt";

    public CakeOrder()
    {
        orderedCakes = new List<Cake>();
    }

    public void PlaceOrder()
    {
        Cake cake = new Cake();
        Console.Clear();
        Console.WriteLine("=== Заказ торта ===");

        Console.WriteLine("Выберите форму торта:");
        string[] shapes = { "Круглая", "Прямоугольная", "Сердце" };
        int shapeChoice = Menu.DisplayMenuAndGetChoice(shapes);

        Console.WriteLine("Выберите размер торта:");
        string[] sizes = { "Маленький ($10)", "Средний ($20)", "Большой ($30)" };
        int sizeChoice = Menu.DisplayMenuAndGetChoice(sizes);

        Console.WriteLine("Выберите вкус торта:");
        string[] flavors = { "Шоколадный ($5)", "Ванильный ($3)", "Фруктовый ($8)" };
        int flavorChoice = Menu.DisplayMenuAndGetChoice(flavors);

        Console.WriteLine("Введите количество тортов:");
        int quantity = ReadIntInput();

        Console.WriteLine("Выберите глазурь:");
        string[] icings = { "Шоколадная", "Сливочная", "Фруктовая" };
        int icingChoice = Menu.DisplayMenuAndGetChoice(icings);

        Console.WriteLine("Выберите декор:");
        string[] decorations = { "Цветы", "Фигурки", "Надпись" };
        int decorationChoice = Menu.DisplayMenuAndGetChoice(decorations);

        cake.Shape = shapes[shapeChoice];
        cake.Size = sizes[sizeChoice];
        cake.Flavor = flavors[flavorChoice];
        cake.Quantity = quantity;
        cake.Icing = icings[icingChoice];
        cake.Decoration = decorations[decorationChoice];

        orderedCakes.Add(cake);

        Console.WriteLine("Торт успешно добавлен в заказ!");

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }

    public void SaveOrderHistoryToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Cake cake in orderedCakes)
                {
                    writer.WriteLine(cake.ToString());
                    decimal totalCost = CalculateTotalCost();
                    writer.WriteLine("Общая стоимость заказа: $" + totalCost);
                }
            }
            Console.WriteLine("История заказов успешно сохранена в файл!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при сохранении истории заказов: " + ex.Message);
        }

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }


    public void ShowOrderHistory()
    {
        Console.Clear();
        Console.WriteLine("=== История заказов ===");

        if (orderedCakes.Count == 0)
        {
            Console.WriteLine("История заказов пуста.");
        }
        else
        {
            foreach (Cake cake in orderedCakes)
            {
                Console.WriteLine(cake.ToString());
            }
        }

        decimal totalCost = CalculateTotalCost();
        Console.WriteLine("Общая стоимость заказа: $" + totalCost);

        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }



    private static int ReadIntInput()
    {
        int quantity;
        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.WriteLine("Неверный ввод. Попробуйте снова.");
            Console.Write("Введите количество тортов: ");
        }
        return quantity;
    }

    public void SaveOrderHistory()
    {
        using (StreamWriter writer = new StreamWriter(orderHistoryFilePath, true))
        {
            foreach (Cake cake in orderedCakes)
            {
                writer.WriteLine(cake.ToString());
            }
        }
    }

    private decimal CalculateTotalCost()
    {
        decimal total = 0;
        foreach (Cake cake in orderedCakes)
        {
            total += cake.GetCost();
        }
        return total;
    }
}


class Cake
{
    public string Shape { get; set; }
    public string Size { get; set; }
    public string Flavor { get; set; }
    public int Quantity { get; set; }
    public string Icing { get; set; }
    public string Decoration { get; set; }

    public decimal GetCost()
    {
        decimal baseCost = 0;

        if (Size == "Маленький ($10)")
        {
            baseCost = 10;
        }
        else if (Size == "Средний ($20)")
        {
            baseCost = 20;
        }
        else if (Size == "Большой ($30)")
        {
            baseCost = 30;
        }

        decimal flavorCost = 0;

        if (Flavor == "Шоколадный ($5)")
        {
            flavorCost = 5;
        }
        else if (Flavor == "Ванильный ($3)")
        {
            flavorCost = 3;
        }
        else if (Flavor == "Фруктовый ($8)")
        {
            flavorCost = 8;
        }

        return baseCost * Quantity + flavorCost * Quantity;
    }

    public override string ToString()
    {
        return $"Форма: {Shape}, Размер: {Size}, Вкус: {Flavor}, Количество: {Quantity}, Глазурь: {Icing}, Декор: {Decoration}";
    }


}

class Menu
{
    public static int DisplayMenuAndGetChoice(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }

        int choice = ReadIntInput();
        while (choice < 1 || choice > options.Length)
        {
            Console.WriteLine("Неверный выбор. Попробуйте снова.");
            choice = ReadIntInput();
        }

        return choice - 1;
    }

    private static int ReadIntInput()
    {
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Неверный ввод. Попробуйте снова.");
        }
        return choice;
    }
}