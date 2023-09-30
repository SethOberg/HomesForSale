using System;
namespace HomesForSaleBLL.Commercials
{
    //Concrete class
    //Inherits attributes from Estate and adds a specific attribute
    //Implements the abstract methods from Estate
    [Serializable]
    public class Shop : Commercial
    {
        private String shopName;

        public Shop()
        {
        }

        public Shop(int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            
        }

        public Shop(string _shopName, int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            this.shopName = _shopName;
        }

        public override string getEstateType()
        {
            return "Shop";
        }

        public override String toString()
        {
            String res = "Type: Shop" + "\n EstateId: " + EstateID
                + "\n LegalForm: " + LegalForm + "\n Street: " + EstateAdress.Street
                + "\n City: " + EstateAdress.City;

            return res;
        }
    }
}
