using System;
namespace HomesForSaleBLL.Residentals
{
    //Concrete class
    //Inherits attributes from Estate and adds a specific attribute
    //Implements the abstract methods from Estate
    [Serializable]
    public class Apartment : Residental
    {
        private bool balcony;

        public Apartment()
        {
        }

        public Apartment(int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
        }

        public Apartment(bool _balcony, int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            this.balcony = _balcony;
        }

        public override string getEstateType()
        {
            return "Apartment";
        }

        public override String toString()
        {
            String res = "Type: Apartment" + "\n EstateId: " + EstateID
                + "\n LegalForm: " + LegalForm + "\n Street: " + EstateAdress.Street
                + "\n City: " + EstateAdress.City;

            return res;
        }
    }
}
