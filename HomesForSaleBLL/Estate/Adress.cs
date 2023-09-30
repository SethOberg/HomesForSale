using System;
namespace HomesForSaleBLL
{
    //Class representing an adress
    [Serializable]
    public class Adress
    {
        private string street;
        private int zipCode;
        private string city;
        private Countries country;

        public Adress()
        {

        }

        public Adress(string _Street, int _ZipCode, string _city, Countries _country)
        {
            this.Street = _Street;
            this.ZipCode = _ZipCode;
            this.city = _city;
            this.country = _country;
        }

        public string Street
        {
            get { return this.street; }
            set { this.street = value; }
        }

        public int ZipCode
        {
            get { return this.zipCode; }
            set { this.zipCode = value; }
        }

        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }

        public Countries Country {
            get { return this.country;  }
            set { this.country = value; }
        }

    }
}
