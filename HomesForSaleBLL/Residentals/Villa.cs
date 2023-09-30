using System;
namespace HomesForSaleBLL.Residentals
{
    //Concrete class
    //Inherits attributes from Estate and adds a specific attribute
    //Implements the abstract methods from Estate
    [Serializable]
    public class Villa : Residental
    {
        public bool garage;

        public Villa()
        {

        }

        public Villa(int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
           
        }

        public Villa(bool _garage, int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            this.garage = _garage;
        }

        public override string getEstateType()
        {
            return "Villa";
        }

        public override String toString()
        {
            String res = "Type: Villa" + "\n EstateId: " + EstateID
                + "\n LegalForm: " + LegalForm + "\n Street: " + EstateAdress.Street
                + "\n City: " + EstateAdress.City;

            return res;
        }
    }
}
