using System;
namespace HomesForSaleBLL.Commercials
{
    //Concrete class
    //Inherits attributes from Estate and adds a specific attribute
    //Implements the abstract methods from Estate
    [Serializable]
    public class Warehouse : Commercial
    {
        private bool loadingDock;

        public Warehouse()
        {
        }

        public Warehouse(int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            
        }

        public Warehouse(bool _loadingDock, int estateid, Adress adress, LegalForm legalForm) : base(estateid, adress, legalForm)
        {
            this.loadingDock = _loadingDock;
        }

        public override string getEstateType()
        {
            return "Warehouse";
        }

        public override String toString()
        {
            String res = "Type: Warehouse" + "\n EstateId: " + EstateID
                + "\n LegalForm: " + LegalForm + "\n Street: " + EstateAdress.Street
                + "\n City: " + EstateAdress.City;

            return res;
        }
    }
}
