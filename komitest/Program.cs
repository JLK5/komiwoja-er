using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

class City
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

class TravelingSalesman
{
    static void Main()
    {
        List<City> cities = LoadCitiesFromJson("data.json");
        List<City> shortestPath = FindShortestPath(cities);

        Console.WriteLine("Shortest Path:");
        foreach (City city in shortestPath)
        {
            Console.WriteLine(city.Name);
        }
    }

    static List<City> LoadCitiesFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<City>>(json);
    }

    static List<City> FindShortestPath(List<City> cities)
    {
        List<City> unvisitedCities = new List<City>(cities);
        List<City> shortestPath = new List<City>();
        City startCity = unvisitedCities[0]; // Assuming starting from the first city
        shortestPath.Add(startCity);
        unvisitedCities.Remove(startCity);

        while (unvisitedCities.Count > 0)
        {
            City currentCity = shortestPath[shortestPath.Count - 1];
            City nearestCity = FindNearestCity(currentCity, unvisitedCities);
            shortestPath.Add(nearestCity);
            unvisitedCities.Remove(nearestCity);
        }

        return shortestPath;
    }

    static City FindNearestCity(City fromCity, List<City> cities)
    {
        double minDistance = double.MaxValue;
        City nearestCity = null;

        foreach (var city in cities)
        {
            double distance = CalculateDistance(fromCity, city);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCity = city;
            }
        }

        return nearestCity;
    }

    static double CalculateDistance(City city1, City city2)
    {
        int dx = city1.X - city2.X;
        int dy = city1.Y - city2.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
