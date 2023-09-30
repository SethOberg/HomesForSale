using System;
namespace HomesForSaleBLL.Residentals
{
    //Concrete class
    //Inherits attributes from Estate and adds a specific attribute
    //Implements the abstract methods from Estate
    [Serializable]
    public class Townhouse : Residental
    {
        private bool garden;

        public Townhouse()
        {
        }

        public Townhouse(int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            
        }

        public Townhouse(bool _garden, int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            this.garden = _garden;
        }

        public override string getEstateType()
        {
            return "Townhouse";
        }

        public override String toString()
        {
            String res = "Type: Townhouse" + "\n EstateId: " + EstateID
                + "\n LegalForm: " + LegalForm + "\n Street: " + EstateAdress.Street
                + "\n City: " + EstateAdress.City;

            return res;
        }
    }
}
