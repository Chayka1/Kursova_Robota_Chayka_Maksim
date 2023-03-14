using System;
using System.Collections.Generic;

public class Car
{
    private string brand;
    private string model;
    private int price_per_hour;

    public Car(string brand, string model, int price_per_hour)
    {
        this.brand = brand;
        this.model = model;
        this.price_per_hour = price_per_hour;
    }

    public string Brand
    {
        get { return brand; }
        set { brand = value; }
    }

    public string Model
    {
        get { return model; }
        set { model = value; }
    }

    public int PricePerHour
    {
        get { return price_per_hour; }
        set { price_per_hour = value; }
    }
}

public class Logic
{
    private List<Car> cars;

    public Logic()
    {
        cars = new List<Car>();
    }

    public void AddCar()
    {
        Console.Write("Марка: ");
        string brand = Console.ReadLine();
        Console.Write("Модель: ");
        string model = Console.ReadLine();
        Console.Write("Цена за час в $: ");
        int price_per_hour = Convert.ToInt32(Console.ReadLine());
        Car car = new Car(brand, model, price_per_hour);
        cars.Add(car);
    }

    public void TakeACarToCarSharing()
    {
        Console.Write("Марка: ");
        string brand = Console.ReadLine();
        Console.Write("Модель: ");
        string model = Console.ReadLine();
        foreach (Car car in cars)
        {
            if (car.Brand == brand && car.Model == model)
            {
                Console.WriteLine($"Стоимость данной машины в час составит {car.PricePerHour}$");
                cars.Remove(car);
                return;
            }
        }
        Console.WriteLine("Машину не найдено. Введите существующую модель!");
    }

    public void ListOfAvailableCars()
    {
        foreach (Car car in cars)
        {
            Console.WriteLine($"{car.Brand}, {car.Model} : {car.PricePerHour}$/час");
        }
    }
}

public class Program
{
    private const int add_car = 1;
    private const int take_a_car_to_car_sharing = 2;
    private const int list_of_available_cars = 3;
    private const int quit = 4;

    private static Logic log = new Logic();

    private static int GetMenuChoice()
    {
        while (true)
        {
            try
            {
                Console.Write("Какую бы операцию вы хотели бы совершить:\n"
                                + "1. Добавить машину\n"
                                + "2. Взять машину в каршеринг\n"
                                + "3. Список доступных машин\n"
                                + "4. Выход\n");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == add_car || choice == take_a_car_to_car_sharing || choice == list_of_available_cars || choice == quit)
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Введите число от 1 до 4");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Введите число от 1 до 4");
            }
        }
    }

    public static void Main()
    {
        int choice = 0;
        while (choice != quit)
        {
            choice = GetMenuChoice();

            if (choice == add_car)
            {
                log.AddCar();
                Console.WriteLine();
            }
            else if (choice == take_a_car_to_car_sharing)
            {
                log.TakeACarToCarSharing();
                Console.WriteLine();
            }
            else if (choice == list_of_available_cars)
            {
                log.ListOfAvailableCars();
                Console.WriteLine();
            }
            else if (choice == quit)
            {
                break;
            }
        }
    }
}
