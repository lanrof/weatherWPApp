using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;
using Weather.Model.SearchResponse;

namespace Weather.WP.Utilities
{
    public class StorageHelper
    {
        public static void AddCityToStorage(City city)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (!settings.Contains("cities"))
            {
                Dictionary<int, City> cities = new Dictionary<int, City>();
                cities[city.Id] = city;
                settings.Add("cities", cities);
            }
            else
            {
                Dictionary<int, City> cities = ((Dictionary<int, City>)settings["cities"]);
                cities[city.Id] = city;
            }
            settings.Save();
        }

        public static ObservableCollection<City> GetCities()
        {
            ObservableCollection<City> cities = new ObservableCollection<City>();
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("cities"))
            {
                Dictionary<int, City> citiesDict = ((Dictionary<int, City>)settings["cities"]);
                cities = new ObservableCollection<City>(citiesDict.Values);
            }

            return cities;
        }

        public static void RemoveCityFromStorage(City city)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains("cities"))
            {
                Dictionary<int, City> cities = ((Dictionary<int, City>)settings["cities"]);
                cities.Remove(city.Id);
                settings.Save();
            }            
        }

    }
}
