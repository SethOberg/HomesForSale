using System;
using System.Collections.Generic;

namespace HomesForSaleBLL.Dictionary
{
    public class CityDictionary
    {
        //Dictionary with a key value string representing a city
        //And a value representing a list of all estates in the city
        private Dictionary<string, List<int>> cityAndEstates = new Dictionary<string, List<int>>();

        public CityDictionary()
        {
        }

        public void addToDictionary(string city, Estate estate)
        {
            //create a new list in the dictionary if there are
            //is no key value of the city
            if (!cityAndEstates.ContainsKey(city))
            {
                cityAndEstates.Add(city, new List<int>());
                cityAndEstates[city].Add(estate.EstateID);
            }
            else
            {
                cityAndEstates[city].Add(estate.EstateID);
            }
        }

        public void removeFromDictionary(string city, int id)
        {
            List<int> cityEstates = cityAndEstates[city];

            //iterate through the list matching the city in the dictionary
            //is list count is 0 after removing the key will be removed
            //from the dictionary
            for (int i = 0; i < cityEstates.Count; i++)
            {
                if (cityEstates[i] == id)
                {
                    cityEstates.RemoveAt(i);
                    Console.WriteLine("Estate borttagen från dictionary");
                }

            }

            //remove key value if list is empty in dictionary
            if (cityAndEstates[city].Count == 0)
            {
                cityAndEstates.Remove(city);
            }
        }


        public void initializeDictionary(List<Estate> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                addToDictionary(list[i].EstateAdress.City, list[i]);
            }
        }


        //If there are no estates in a city, a null list will be returned
        //Searches the dictionary for the key value of the string parameter city
        public List<int> EstatesFromCity(string city)
        {
            List<int> estatesInCity = null;

            if (cityAndEstates.ContainsKey(city))
            {
                estatesInCity = cityAndEstates[city];
            }

            return estatesInCity;
        }


        public bool cityExixtsInDictionary(string city)
        {
            return cityAndEstates.ContainsKey(city);
        }
    }
}
