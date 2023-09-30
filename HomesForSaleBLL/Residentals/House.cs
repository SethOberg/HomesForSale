using System;
namespace HomesForSaleBLL.Residentals
{
    //Concrete class
    //Inherits attributes from Estate and adds a specific attribute
    //Implements the abstract methods from Estate
    [Serializable]
    public class House : Residental
    {
        private int floors;

        public House() { }

        public House(int estateId, Adress adress, LegalForm legalForm) : base(estateId, adress, legalForm)
        {
        }

        public House(int floors, int estateId, Adress adress, LegalForm legalForm) : base(estateId, adress, legalForm)
        {
            this.floors = floors;
        }

        public override string getEstateType()
        {
            return "House";
        }

        public override String toString()
        {
            String res = "Type: House" + "\n EstateId: " + EstateID
                + "\n LegalForm: " + LegalForm + "\n Street: " + EstateAdress.Street
                + "\n City: " + EstateAdress.City;

            return res;
        }
    }

   
}
