using System;
using System.Xml.Serialization;
using HomesForSaleBLL.Commercials;
using HomesForSaleBLL.Residentals;

namespace HomesForSaleBLL
{

    //Abstract class that both the commercial and residental buildings inherit from
    //Implements the IEstate interface as well as an abstract method toString 
    //that all concrete classes implement
    [XmlInclude(typeof(Apartment)), XmlInclude(typeof(House)), XmlInclude(typeof(Residental)),
        XmlInclude(typeof(Townhouse)), XmlInclude(typeof(Villa)), XmlInclude(typeof(Commercial)),
        XmlInclude(typeof(Warehouse)), XmlInclude(typeof(Shop))]
    [Serializable]
    public abstract class Estate : IEstate
    {
        private int estateID;
        private Adress adress;
        private LegalForm legalForm;


        public Estate()
        {

        }

        public Estate(int _estateID, Adress _adress, LegalForm _legalform)
        {
            this.estateID = _estateID;
            this.adress = _adress;
            this.legalForm = _legalform;
        }

        public int EstateID
        {
            get { return this.estateID; }
            set { this.estateID = value; }
        }

        public Adress EstateAdress {
            get { return this.adress;  }
            set { this.adress = value; }
        }

        public LegalForm LegalForm
        {
            get { return this.legalForm; }
            set { this.legalForm = value; }
        }

        public abstract String toString();

        public abstract String getEstateType();
    }

  
}
