using System;
using System.Collections.Generic;
using System.Data.SQLite;

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
    private SQLiteConnection connection;

    public Logic()
    {
        cars = new List<Car>();
        connection = new SQLiteConnection("Data Source=Cars.sqlite;Version=3;");
        connection.Open();
        string createTableQuery = "CREATE TABLE IF NOT EXISTS Cars (Brand TEXT, Model TEXT, PricePerHour INTEGER)";
        SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection);
        createTableCommand.ExecuteNonQuery();
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
        string insertQuery = $"INSERT INTO Cars (Brand, Model, PricePerHour) VALUES ('{brand}', '{model}', {price_per_hour})";
        SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
        insertCommand.ExecuteNonQuery();
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
                string deleteQuery = $"DELETE FROM Cars WHERE Brand='{brand}' AND Model='{model}'";
                SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection);
                deleteCommand.ExecuteNonQuery();
                return;
            }
        }
        Console.WriteLine("Машину не найдено. Введите существующую модель!");
    }

    public void ListOfAvailableCars()
    {
        string selectQuery = "SELECT Brand, Model, PricePerHour FROM Cars";
        SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection);
        SQLiteDataReader reader = selectCommand.ExecuteReader();
        while (reader.Read())
        {
            string brand = reader.GetString(0);
            string model = reader.GetString(1);
            int price_per_hour = reader.GetInt32(2);
            Car car = new Car(brand, model, price_per_hour);
            cars.Add(car);
            Console.WriteLine($"{car.Brand}, {car.Model} : {car.PricePerHour}$/час");
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Logic logic = new Logic();
        Console.WriteLine("1. Добавить автомобиль");
        Console.WriteLine("2. Взять автомобиль в аренду");
        Console.WriteLine("3. Список доступных автомобилей");
        Console.WriteLine("4. Выход из программы");
        while (true)
        {
            Console.Write("Выберете номер действия: ");
            int key = Convert.ToInt32(Console.ReadLine());
            switch (key)
            {
                case 1:
                    logic.AddCar();
                    break;
                case 2:
                    logic.TakeACarToCarSharing();
                    break;
                case 3:
                    logic.ListOfAvailableCars();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Введите номер от 1 до 4!");
                    break;
            }
        }
    }
}
